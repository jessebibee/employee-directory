using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeDirectory.Web.Models
{
    public class UserViewModel
    {
        /// <summary>
        /// Used as the username in .NET Identity
        /// </summary>
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int EmployeeId { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}