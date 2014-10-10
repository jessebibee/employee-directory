using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using EmployeeDirectory.Web.Models;

namespace EmployeeDirectory.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ApplicationUserManager _userManager;

        public HomeController()
        {
        }

        public HomeController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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
            //Push down account/identity information into angular
            //can pull current employee by username (email) - may or may not exist - if it does take first/last name
            ApplicationUser user = UserManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            UserViewModel vm = new UserViewModel
            {
                Email = user.Email,
                Roles = UserManager.GetRolesAsync(user.Id).Result //check for 0 roles empty array and not null
            };
            return View(vm);
        }
    }
}