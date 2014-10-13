using EmployeeDirectory.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDirectory.Web.Services
{
    public interface IEmployeeRepository
    {
        /// <summary>
        /// Get the employee by id
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns>Employee or null if the employee does not exist</returns>
        Employee GetById(int employeeId);

        /// <summary>
        /// Returns a queryable interface to query employees
        /// </summary>
        /// <returns></returns>
        IQueryable<Employee> AsQueryable();

        /// <summary>
        /// Persists the employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>Employee </returns>
        Employee Add(Employee employee);

        /// <summary>
        /// Updates the employee
        /// </summary>
        /// <param name="employee"></param>
        void Update(Employee employee);

        /// <summary>
        /// Deletes an employee from the persistence store (need to fetch it first)
        /// </summary>
        /// <param name="employee">Employee</param>
        void Delete(Employee employee);
    }
}
