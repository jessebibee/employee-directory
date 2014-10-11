using EmployeeDirectory.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeDirectory.Web.Services.Domain
{
    public class EmployeeCreateResult
    {
        public EmployeeCreateResult(Employee employee, string password)
        {
            Employee = employee;
            Password = password;
        }
        
        public Employee Employee { get; set; }

        public string Password { get; set; }
    }
}