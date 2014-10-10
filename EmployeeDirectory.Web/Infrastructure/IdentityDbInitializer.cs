using EmployeeDirectory.Web.Infrastructure.Repository;
using EmployeeDirectory.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EmployeeDirectory.Web.Infrastructure
{
    public class IdentityDbInitializer : DropCreateDatabaseAlways<IdentityContext>
    {
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
                Location = "Austin",
                Phone = "5121234567"
            };
            Employee scotteEmp = new Employee 
            {
                FirstName = "Scott",
                LastName = "Employee",
                Email = "scotte@acme.com",
                JobTitle = "Software Engineer",
                Location = "Seattle",
                Phone = "4257654321"
            };
            EmployeeContext employeeCtx = new EmployeeContext();
            employeeCtx.Employees.Add(scotteaEmp);
            employeeCtx.Employees.Add(scotteEmp);
            employeeCtx.SaveChanges();
            
            base.Seed(context);
        }
    }
}