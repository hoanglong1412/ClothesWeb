using System.Web.Mvc;

namespace clothesWebSite.Areas.Administrator
{
    public class AdministratorAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Administrator";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
             name: "admin",
             url: "Admin",
             defaults: new { controller = "adm_MainPage", action = "Index", AreaName="Administrator"}
            );
            context.MapRoute(
             name: "login",
             url: "Admin/Login",
             defaults: new { controller = "adm_MainPage", action = "Login", AreaName="Administrator"}
            );
            context.MapRoute(
                "Administrator_default",
                "Administrator/{controller}/{action}/{id}",
                new { action = "Index",Controller= "adm_MainPage", id = UrlParameter.Optional }
            );
        }
    }
}