using EmployeeDirectory.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EmployeeDirectory.Web.Infrastructure.Repository
{
    public class EmployeeContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public EmployeeContext()
            : base("DefaultConnection")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Directory");
            var empConfig = modelBuilder.Entity<Employee>();
            empConfig.ToTable("Employee");
            empConfig.HasKey(x => x.EmployeeId);
        }
    }
}