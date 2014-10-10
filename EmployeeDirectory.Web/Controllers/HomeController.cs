using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeDirectory.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //Push down account/identity information into angular
            return View();
        }
    }
}