using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeDirectory.Web.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        
        public string FirstName { get; set; }

        public string MiddleInitial { get; set; }

        public string LastName { get; set; }

        public string JobTitle { get; set; }

        public Location? Location { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }
    }
}