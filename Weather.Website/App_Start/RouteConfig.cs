using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Weather.Website
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "DetailsCityIdDateParam",
                url: "city-{cityId}/{date}/{param}",
                defaults: new { controller = "City", action = "Details", param = UrlParameter.Optional },
                constraints: new { cityId = @"\d+", date = @"\d{4}-\d{2}-\d{2}" }
            );

            routes.MapRoute(
                name: "DetailsCityId",
                url: "city-{cityId}",
                defaults: new { controller = "City", action = "Details" },
                constraints: new { cityId = @"\d+" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
