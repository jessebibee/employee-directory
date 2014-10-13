using EmployeeDirectory.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDirectory.Web.Services.Domain
{
    /// <summary>
    /// Implements the workflows to create and delete employees
    /// </summary>
    public interface IEmployeeService
    {
        /// <summary>
        /// Creates a new employee and their identity account returning the details
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>Complex type containing the Employee and and account information - 
        /// it is still possible it will throw exceptions as errors are only captured 
        /// from ASP.NET Identity's Result object that eats exceptions</returns>
        Task<EmployeeCreateResult> CreateEmployee(Employee employee);

        /// <summary>
        /// Deletes an existing employee and their identity account
        /// </summary>
        /// <param name="employee"></param>
        void DeleteEmployee(Employee employee);
    }
}
