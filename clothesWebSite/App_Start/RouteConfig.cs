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

            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
             name: "Cart",
             url: "your-cart",
             defaults: new { controller = "Cart", action = "viewCart" }
         );
            routes.MapRoute(
             name: "Search",
             url: "search-by-key",
             defaults: new { controller = "Clothes", action = "FindProduct" }
         );
            routes.MapRoute(
             name: "Faq",
             url: "faq",
             defaults: new { controller = "User", action = "Faq" }
         );
            routes.MapRoute(
              name: "TextPage",
              url: "text-page",
              defaults: new { controller = "User", action = "TextPage" }
          );
            routes.MapRoute(
              name: "Contact",
              url: "contact-us",
              defaults: new { controller = "User", action = "Contact" }
          );
            routes.MapRoute(
               name: "PostDetail",
               url: "post-detail",
               defaults: new { controller = "Clothes", action = "PostDeitail" }
           );
            routes.MapRoute(
               name: "AllPost",
               url: "all-post",
               defaults: new { controller = "Clothes", action = "AllPost" }
           );
            routes.MapRoute(
               name: "ProductCategory",
               url: "product-category",
               defaults: new { controller = "Clothes", action = "ProductCategoty" }
           );
            routes.MapRoute(
               name: "ProductAll",
               url: "all-product",
               defaults: new { controller = "Clothes", action = "AllProduct" }
           );

            routes.MapRoute(
               name: "ProductSale",
               url: "product-sale",
               defaults: new { controller = "Clothes", action = "ProductSale" }
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
                defaults: new { controller = "Clothes", action="Index", id = UrlParameter.Optional }
            );
        }
    }
}
