using System.Web.Mvc;
using System.Web.Routing;

namespace Blog.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "FailWhale",
                url: "FailWhale/{action}/{id}",
                defaults: new
                {
                    controller = "Error",
                    action = "FailWhale",
                    id = UrlParameter.Optional
                }
            );

            //For register user purpose
            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);

            routes.MapRoute(
              "Post",
              "Archive/{year}/{month}/{title}",
              new { controller = "Blog", action = "Post" }
            );

            routes.MapRoute(
              "Archive",
              "Archive/{year}/{month}",
              new { controller = "Blog", action = "Archive", year = UrlParameter.Optional, month = UrlParameter.Optional }
            );

            routes.MapRoute(
              "Category",
              "Category/{category}",
              new { controller = "Blog", action = "Category" }
            );

            routes.MapRoute(
              "Tag",
              "Tag/{tag}",
              new { controller = "Blog", action = "Tag" }
            );

            //routes.MapRoute(
            //  "Login",
            //  "Account/Login",
            //  new { area = "Admin", controller = "Account", action = "Login" }
            //);

            //routes.MapRoute(
            //  "Logout",
            //  "Logout",
            //  new { area = "Admin", controller = "Account", action = "Logout" }
            //);

            //routes.MapRoute(
            //  "Manage",
            //  "Manage",
            //  new { area = "Admin", controller = "Admin", action = "Index" }
            //);

            //routes.MapRoute(
            //  "Admin",
            //  "Admin/Admin/{action}",
            //  new { area = "Admin", controller = "Admin", action = "Login" }
            //);

            routes.MapRoute(
              "Action",
              "{action}",
              new { controller = "Blog", action = "Posts" }
            );
        }
    }
}