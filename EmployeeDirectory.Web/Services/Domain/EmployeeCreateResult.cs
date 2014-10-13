using EmployeeDirectory.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeDirectory.Web.Services.Domain
{
    public class EmployeeCreateResult
    {
        private EmployeeCreateResult()
        {
        }

        /// <summary>
        /// Another user with the same email already exists
        /// </summary>
        public bool UserAlreadyExists { get; private set; }

        /// <summary>
        /// Errors creating the user or assigning them to a role
        /// </summary>
        public IEnumerable<string> IdentitySetupError { get; private set; }

        public Employee Employee { get; private set; }

        public string Password { get; private set; }

        public bool Succeeded { get; private set; }

        /// <summary>
        /// Successfully created the User and Employee
        /// </summary>
        public static EmployeeCreateResult Success(Employee employee, string password)
        {
            return new EmployeeCreateResult() { Employee = employee, Password = password, Succeeded = true };
        }

        /// <summary>
        /// Another user with the same email already exists
        /// </summary>
        public static EmployeeCreateResult UserExists { get { return new EmployeeCreateResult() { UserAlreadyExists = true }; } }

        /// <summary>
        /// Errors creating the user or assigning them to a role
        /// </summary>
        public static EmployeeCreateResult IdentityError(IEnumerable<string> identityError)
        {
            return new EmployeeCreateResult() { IdentitySetupError = identityError };
        }
    }
}