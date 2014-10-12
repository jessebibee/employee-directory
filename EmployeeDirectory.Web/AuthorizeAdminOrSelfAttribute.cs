using EmployeeDirectory.Web.Infrastructure.Repository;
using EmployeeDirectory.Web.Models;
using EmployeeDirectory.Web.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace EmployeeDirectory.Web
{
    /// <summary>
    /// Authorizes the user is an Admin or the Employee under action
    /// </summary>
    public class AuthorizeAdminOrSelfAttribute : AuthorizeAttribute
    {
        private readonly IEmployeeRepository _repo;
        private UserManager<ApplicationUser> _userMgr;
        
        public AuthorizeAdminOrSelfAttribute()
        {
            _repo = new EmployeeRepository();
            _userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new IdentityContext()));
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            string email = HttpContext.Current.User.Identity.Name;
            
            ApplicationUser user = _userMgr.Users.FirstOrDefault(x => x.Email == email);

            if (user != null)
            {
                if (_userMgr.GetRoles<ApplicationUser, string>(user.Id).Any(x => x == "Admin"))
                {
                    return true;
                }
                else
                {
                    Employee employee = _repo.Get().FirstOrDefault(x => x.Email == email);
                    return employee.EmployeeId == Convert.ToInt32(actionContext.RequestContext.RouteData.Values["id"]); //employee as self
                }
            }

            return false;
        }
    }
}