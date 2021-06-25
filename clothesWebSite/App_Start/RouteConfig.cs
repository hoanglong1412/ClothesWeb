using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace clothesWebSite
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
               name: "ProductCategory",
               url: "product-category",
               defaults: new { controller = "Clothes", action = "ProductCategoty" }
           );
            routes.MapRoute(
               name: "ProductAll",
               url: "all-product",
               defaults: new { controller = "Clothes", action = "ProductAllCategoty" }
           );
            routes.MapRoute(
              name: "ProductDetail",
              url: "product-detail",
              defaults: new { controller = "Clothes", action = "ProductDetail"}
          );
            routes.MapRoute(
                name: "LoginRe",
                url: "login-and-register",
                defaults: new { controller = "User", action = "Login"}
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Clothes", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
