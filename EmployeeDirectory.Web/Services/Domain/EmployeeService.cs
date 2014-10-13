using EmployeeDirectory.Web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;

namespace EmployeeDirectory.Web.Services.Domain
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repo;
        private readonly UserManager<ApplicationUser> _userMgr;

        public EmployeeService(IEmployeeRepository repo, UserManager<ApplicationUser> userManager)
        {
            _repo = repo;
            _userMgr = userManager;
        }

        public async Task<EmployeeCreateResult> CreateEmployee(Employee employee)
        {
            ApplicationUser user = new ApplicationUser { UserName = employee.Email, Email = employee.Email };
            string password = Guid.NewGuid().ToString("d").Substring(1, 7);

            IdentityResult result = await _userMgr.CreateAsync(user, password);
            if (result.Succeeded)
            {
                result = _userMgr.AddToRole<ApplicationUser, string>(user.Id, "Employee");
                
                if (result.Succeeded)
                {
                    employee = _repo.Add(employee);
                    return EmployeeCreateResult.Success(employee, password);
                }
            }

            //Does user already exist?
            if (_userMgr.Users.Any(x => x.Email == employee.Email))
            {
                return EmployeeCreateResult.UserExists;
            }

            return EmployeeCreateResult.IdentityError(result.Errors);
        }

        public void DeleteEmployee(Employee employee)
        {
            _repo.Delete(employee);
            ApplicationUser user = _userMgr.FindByEmail<ApplicationUser, string>(employee.Email);
            _userMgr.Delete<ApplicationUser, string>(user); 
        }
    }
}