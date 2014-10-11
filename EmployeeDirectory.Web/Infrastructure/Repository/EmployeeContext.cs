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
            empConfig.Property(x => x.FirstName).IsRequired().HasMaxLength(50);
            empConfig.Property(x => x.MiddleInitial).IsFixedLength().HasMaxLength(1);
            empConfig.Property(x => x.LastName).IsRequired().HasMaxLength(50);
            empConfig.Property(x => x.Email).IsRequired().HasMaxLength(256); //must match ASP.NET Identity's size
            empConfig.Property(x => x.JobTitle).HasMaxLength(100);
            empConfig.Property(x => x.Location).HasColumnName("LocationId");
            empConfig.Property(x => x.Phone).HasMaxLength(25);
        }
    }
}