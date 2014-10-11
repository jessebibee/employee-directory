using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeDirectory.Web.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(1)]
        public string MiddleInitial { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(100)]
        public string JobTitle { get; set; }

        public Location? Location { get; set; }

        [Required]
        public string Email { get; set; }

        [StringLength(25)]
        [RegularExpression("^\\d{10,25}$")]
        public string Phone { get; set; }
    }
}