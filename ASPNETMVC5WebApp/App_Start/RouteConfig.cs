using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ASPNETMVC5WebApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //// can define custom Route here "controller/action/{p1}/{p2}"
            //routes.MapRoute(
            //    "StudentByPasingYear",
            //    "student/bypassingyear/{mth}/{yr}",
            //    new { controller = "Student", action = "ByPassingYear"}
            //);

            // Enable Attribute Routing
            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
