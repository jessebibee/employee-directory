using EmployeeDirectory.Web.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeDirectory.Web.Infrastructure.Repository
{
    public class IdentityContext : ApplicationDbContext
    {
        public IdentityContext() : base()
        {

        }

        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            //modelBuilder.HasDefaultSchema("Identity"); //why does it create tables under dbo as well????
            ////rename & remove tables??
            base.OnModelCreating(modelBuilder);
        }
    }

    /// <summary>
    /// ASP.NET Idendity generated
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}