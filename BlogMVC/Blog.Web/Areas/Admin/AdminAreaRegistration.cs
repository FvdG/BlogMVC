using System.Web.Mvc;

namespace Blog.Web.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { controller = "Admin", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
              "Login",
              "Admin/Login",
              new { area = "Admin", controller = "Admin", action = "Login" }
            );

            context.MapRoute(
              "Logout",
              "Admin/Logout",
              new { area = "Admin", controller = "Admin", action = "Logout" }
            );

            //context.MapRoute(
            //  "Admin",
            //  "Admin/Admin",
            //  new { area = "Admin", controller = "Admin", action = "Index" }
            //);

            context.MapRoute(
              "Admin",
              "Admin/{action}",
              new { area = "Admin", controller = "Admin", action = "Login" }
            );
        }
    }
}
