using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Dimol.Carteras.Mantenedores
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                //defaults: new { controller = "Mantenedor", action = "Acciones", id = UrlParameter.Optional }
                //defaults: new { controller = "Mantenedor", action = "MotivoCobranza", id = UrlParameter.Optional }
                //defaults: new { controller = "Mantenedor", action = "CodigoCarga", id = UrlParameter.Optional }
                //defaults: new { controller = "Mantenedor", action = "TipoRetiroEntrega", id = UrlParameter.Optional }
                //defaults: new { controller = "Mantenedor", action = "MotivoCastigo", id = UrlParameter.Optional }
                //defaults: new { controller = "Mantenedor", action = "TipoImagenDocumento", id = UrlParameter.Optional }
                //defaults: new { controller = "Mantenedor", action = "TipoDocumentoDeudor", id = UrlParameter.Optional }
                //defaults: new { controller = "Mantenedor", action = "GrupoCobranza", id = UrlParameter.Optional }
                defaults: new { controller = "CarteraABM", action = "EstadoCartera", id = UrlParameter.Optional }
                //defaults: new { controller = "CarteraABM", action = "Gestor", id = UrlParameter.Optional }

                
            );
        }
    }
}