using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WakeYourPC.WakeUpService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "UsersController",
                routeTemplate: "v1/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "MachinesController",
                routeTemplate: "v1/users/{id1}/{controller}/{id2}",
                defaults: new { id2 = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "WakeupController",
                routeTemplate: "v1/users/{id1}/{controller}",
                defaults: new {  }
            );


        }
    }
}
