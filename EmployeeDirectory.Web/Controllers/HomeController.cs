using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using EmployeeDirectory.Web.Models;
using Microsoft.Owin.Security;
using EmployeeDirectory.Web.Services;

namespace EmployeeDirectory.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ApplicationUserManager _userManager;
        private readonly IEmployeeRepository _repo;

        public HomeController()
        {
            _repo = new EmployeeDirectory.Web.Infrastructure.Repository.EmployeeRepository();
        }

        public HomeController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        
        public ActionResult Index()
        {
            //Push down account/identity information into front-end app
            ApplicationUser user = UserManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);

            if (user == null)
            {
                //in rare cases may not exist, they could be removed and still have a persistent auth cookie for example
                AuthenticationManager.SignOut();
                return RedirectToAction("Login", "Account");
            }

            Employee employee = _repo.Get().FirstOrDefault(x => x.Email == user.Email);

            UserViewModel vm = new UserViewModel
            {
                Email = user.Email,
                Roles = UserManager.GetRolesAsync(user.Id).Result //check for 0 roles empty array and not null
            };

            if (employee != null)
            {
                vm.FirstName = employee.FirstName;
                vm.LastName = employee.LastName;
                vm.EmployeeId = employee.EmployeeId;
            }

            return View(vm);
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
    }
}