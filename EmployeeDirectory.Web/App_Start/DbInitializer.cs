using EmployeeDirectory.Web.Infrastructure.Repository;
using EmployeeDirectory.Web.Models;
using Foundation.ObjectHydrator.Generators;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EmployeeDirectory.Web.App_Start
{
    public class DbInitializer : DropCreateDatabaseAlways<IdentityContext>
    {
        private readonly int _count;

        public DbInitializer()
        {
            _count = 0;
        }

        public DbInitializer(int employeeCount)
        {
            _count = employeeCount;
        }

        public override void InitializeDatabase(IdentityContext context)
        {
            base.InitializeDatabase(context);

            //Create Location lookup table
            context.Database.ExecuteSqlCommand("CREATE TABLE [Directory].[Location] (LocationId tinyint NOT NULL PRIMARY KEY, Name nvarchar(50) NOT NULL)");
            foreach (Location location in Enum.GetValues(typeof(Location)))
            {
                context.Database.ExecuteSqlCommand(String.Format("INSERT INTO [Directory].[Location] VALUES ({0}, '{1}')", (byte)location, location.ToString()));
            }
            context.Database.ExecuteSqlCommand("ALTER TABLE [Directory].[Employee] ADD CONSTRAINT Employee_LocationId FOREIGN KEY (LocationId) REFERENCES [Directory].[Location](LocationId)");

            //manually hack in a FK across Context's (yes this defeats the purpose of "bounded" contexts) - but want to seperate EmployeeContext from all the cruft of ASP.NET Identity's context
            context.Database.ExecuteSqlCommand("CREATE UNIQUE NONCLUSTERED INDEX IX_UQ_NC_Email ON [dbo].[AspNetUsers] (Email)");
            context.Database.ExecuteSqlCommand("ALTER TABLE [Directory].[Employee] ADD CONSTRAINT Employee_AspNetUser_Email FOREIGN KEY (Email) REFERENCES [dbo].[AspNetUsers](Email)");

            //Indexes (searchable columns)
            context.Database.ExecuteSqlCommand("CREATE NONCLUSTERED INDEX IX_NC_FirstName ON [Directory].[Employee] (FirstName)");
            context.Database.ExecuteSqlCommand("CREATE NONCLUSTERED INDEX IX_NC_LastName ON [Directory].[Employee] (LastName)");
            context.Database.ExecuteSqlCommand("CREATE NONCLUSTERED INDEX IX_NC_Email ON [Directory].[Employee] (Email)");
            //TODO - add location
        }

        protected override void Seed(IdentityContext context)
        {
            var roleMgr = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var roleResult1 = roleMgr.Create(new IdentityRole("Admin"));
            var roleResult2 = roleMgr.Create(new IdentityRole("Employee"));

            var scottea = new ApplicationUser { UserName = "scottea@acme.com", Email = "scottea@acme.com" };
            var scotte = new ApplicationUser { UserName = "scotte@acme.com", Email = "scotte@acme.com" };
            var scottc = new ApplicationUser { UserName = "scotta@acme.com", Email = "scotta@acme.com" }; //scottc is a contractor, not in the employee directory, but has a login to access it

            var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var result1 = userMgr.CreateAsync(scottea, "scottea").Result;
            var result2 = userMgr.CreateAsync(scotte, "scotte").Result;
            var result3 = userMgr.CreateAsync(scottc, "scottc").Result;

            //attach users to roles
            userMgr.AddToRole<ApplicationUser, string>(scottea.Id, "Admin");
            userMgr.AddToRole<ApplicationUser, string>(scottea.Id, "Employee");
            userMgr.AddToRole<ApplicationUser, string>(scotte.Id, "Employee");

            //add employees?
            Employee scotteaEmp = new Employee
            {
                FirstName = "Scott",
                LastName = "Employee-Admin",
                Email = "scottea@acme.com",
                JobTitle = "Master Employee",
                Location = Location.Austin,
                Phone = "5121234567"
            };
            Employee scotteEmp = new Employee
            {
                FirstName = "Scott",
                LastName = "Employee",
                Email = "scotte@acme.com",
                JobTitle = "Software Engineer",
                Location = Location.Dallas,
                Phone = "4257654321"
            };
            EmployeeContext employeeCtx = new EmployeeContext();
            employeeCtx.Employees.Add(scotteaEmp);
            employeeCtx.Employees.Add(scotteEmp);
            employeeCtx.SaveChanges();

            Random random = new Random();
            string[] titles = new string[] { "Software Engineer", "DevOps Engineer", "Business Analyst", "Scrum Master", "QA", "SDET", "Controller", "BI Developer", null, null };

            if (_count > 0)
            {
                for (int i = 1; i <= _count; i++)
                {
                    string firstName = new FirstNameGenerator().Generate();
                    string lastName = new LastNameGenerator().Generate();
                    string email = firstName + lastName + i.ToString() + "@acme.com";
                    ApplicationUser user = new ApplicationUser { UserName = email, Email = email };
                    var result = userMgr.CreateAsync(user, firstName + "_pass").Result;
                    userMgr.AddToRole<ApplicationUser, string>(user.Id, "Employee"); //everyone is going to be employee except scott contractor above
                    if (i % 5 == 0)
                    {
                        userMgr.AddToRole<ApplicationUser, string>(user.Id, "Admin");
                    }

                    Employee emp = new Employee();
                    emp.FirstName = firstName;
                    emp.MiddleInitial = (i % 2 == 0) ? "M" : null;
                    emp.LastName = lastName;
                    emp.JobTitle = titles[random.Next(0, 9)];
                    emp.Email = email;
                    if ((i % 2 == 0) || (i % 3 == 0))
                    {
                        emp.Phone = new AmericanPhoneGenerator().Generate().Replace("(", "").Replace(")", "").Replace("-", "");
                    }
                    if (i % 3 == 0)
                    {
                        emp.Location = Location.Austin;
                    }
                    else if (i % 2 == 0)
                    {
                        emp.Location = Location.Dallas;
                    }
                    else if (i % 5 == 0)
                    {
                        emp.Location = Location.Houston;
                    }
                    employeeCtx.Employees.Add(emp);
                }

                employeeCtx.SaveChanges();
            }

            base.Seed(context);
        }
    }
}