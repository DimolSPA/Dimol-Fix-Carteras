using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Dimol.Empresa
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                //defaults: new { controller = "Empresa", action = "Acciones", id = UrlParameter.Optional }
                //defaults: new { controller = "Empresa", action = "Idioma", id = UrlParameter.Optional }
                //defaults: new { controller = "Empresa", action = "Pais", id = UrlParameter.Optional }
                //defaults: new { controller = "Empresa", action = "Region", id = UrlParameter.Optional }
                //defaults: new { controller = "Empresa", action = "Ciudad", id = UrlParameter.Optional }
                //defaults: new { controller = "Empresa", action = "Comuna", id = UrlParameter.Optional }
                defaults: new { controller = "Empresa", action = "Moneda", id = UrlParameter.Optional }
                //defaults: new { controller = "Empresa", action = "Perfil", id = UrlParameter.Optional }
                //defaults: new { controller = "Empresa", action = "BuscarEmpleado", id = UrlParameter.Optional }
                 //defaults: new { controller = "Empresa", action = "Empresa", id = UrlParameter.Optional }
                
            );
        }
    }
}