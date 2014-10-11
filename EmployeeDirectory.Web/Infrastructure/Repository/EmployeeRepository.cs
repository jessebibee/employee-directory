using EmployeeDirectory.Web.Models;
using EmployeeDirectory.Web.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EmployeeDirectory.Web.Infrastructure.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeContext _context;
        
        public EmployeeRepository()
        {
            _context = new EmployeeContext();
        }

        public Employee GetById(int employeeId)
        {
            return _context.Employees.FirstOrDefault(x => x.EmployeeId == employeeId);
        }

        public IQueryable<Employee> Get()
        {
            return _context.Employees;
        }

        public Employee Add(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return employee;
        }

        public void Update(Employee employee)
        {
            _context.Entry(employee).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(Employee employee)
        {
            _context.Employees.Remove(employee);
            _context.SaveChanges(); 
        }
    }
}