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

        public string LastName { get; set; }

        public string JobTitle { get; set; }

        public string Location { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        /// <summary>
        /// TODO - Determine type from ASP.NET Identity or use username, and consider a navigation property instead 
        /// </summary>
        //public string ApplicationUserName { get; set; }
    }
}