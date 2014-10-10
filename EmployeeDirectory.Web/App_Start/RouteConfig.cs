using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EmployeeDirectory.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //route to account views but otherwise return the Home Index action
            routes.MapRoute(
                name: "Account",
                url: "Account/{action}/{id}",
                defaults: new { controller = "Account", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Application",
                  url: "{*url}",
                  defaults: new { controller = "Home", action = "Index" }
            );
        }
    }
}
