using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Pastebook
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "ViewProfile",
                url: "Pastebook/ViewProfile/{profileUsername}",
                defaults: new { controller = "UserProfile", action = "ViewProfile"}
            );


            routes.MapRoute(
                name: "ViewPost",
                url: "Pastebook/Posts/{id}",
                defaults: new { controller = "Post", action = "Posts" }
            );

            routes.MapRoute(
                name: "LoginRegister",
                url: "Pastebook/Login/",
                defaults: new { controller = "LoginRegister", action = "Login"}
                );

            routes.MapRoute(
                name: "RegisterValidation",
                url: "Pastebook/ValidateRegistration/",
                defaults: new { controller = "LoginRegister", action = "ValidateRegistration" }
                );

            routes.MapRoute(
                name: "Home",
                url: "Pastebook/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
