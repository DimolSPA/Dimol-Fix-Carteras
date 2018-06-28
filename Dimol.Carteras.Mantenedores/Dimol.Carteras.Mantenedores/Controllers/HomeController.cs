using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dimol.bcp;
using Dimol.dto;
using System.Globalization;
using System.Threading;

namespace Dimol.Controllers
{
    public class HomeController : Controller
    {
        UserSession objSession = new UserSession();
        Utilidades utl = new Utilidades();
        //bcp.Menu objMenu = new bcp.Menu();

        public HomeController()
        {
        }

        public ActionResult Index()
        {
            objSession = (UserSession)Session["Usuario"];
            ViewBag.Message = "Modifique esta plantilla para poner en marcha su aplicación ASP.NET MVC.";
            //CargaMenu();
            ViewBag.Menu = objSession.Menu;//objMenu.Cargar(objSession.UserId, objSession.Idioma, objSession.CodigoEmpresa);
            CargaDatosUsuario();
            return View();
        }

        private bool CargaMenu()
        {
            utl.Empresa = objSession.CodigoEmpresa;
            utl.Sucursal = objSession.CodigoSucursal;
            utl.Usuario = objSession.UserId;
            utl.IpRed = objSession.IpRed;
            utl.IpMaquina = objSession.IpPc;

            return utl.Revisiones();
        }

        public void CargaDatosUsuario()
        {
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;
            ViewBag.UserName = textInfo.ToTitleCase(objSession.Nombre.ToLower());
            ViewBag.Cargo = textInfo.ToTitleCase(objSession.Cargo.ToLower());
        }
    }
}
