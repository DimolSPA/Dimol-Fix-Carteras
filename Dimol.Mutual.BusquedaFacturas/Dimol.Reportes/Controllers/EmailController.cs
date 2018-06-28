using Dimol.Reportes.Models;
using Mvc.HtmlHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dimol.Carteras.bcp;
using Newtonsoft.Json.Linq;
using System.Configuration;

namespace Dimol.Reportes.Controllers
{
    public class EmailController : Dimol.Controllers.BaseController
    {
        //
        // GET: /ReportesCartera/

        public ActionResult Cartera(string ctr, string pag)
        {
            this.SettingAccount();
            if (string.IsNullOrEmpty(pag))
            {
                pag = "1";
            }
            ViewBag.Codemp = objSession.CodigoEmpresa;
            ViewBag.Reporte = new SelectList(bcp.Cartera.ListarReportes(Int32.Parse(pag), objSession.Idioma, "Seleccione"), "Value", "Text", "");
            Comprobante objComprobante = new Comprobante();
            Deudor objDeudor = new Deudor();
            ViewBag.TipoCartera = new SelectList(objComprobante.ListarTipoCartera(objSession.Idioma), "Value", "Text", "3");
            ViewBag.CodigoCarga = new SelectList(objComprobante.ListarCodigoCarga(objSession.CodigoEmpresa, 1, "Seleccione"), "Value", "Text", "");
            ViewBag.GrupoCobranza = new SelectList(Email.bcp.Vista.ListarGrupoCobranza(objSession.CodigoEmpresa, objSession.CodigoSucursal, "Seleccione"), "Value", "Text", "");
            ViewBag.Estado = new SelectList(objDeudor.ListarEstado(objSession.Idioma), "Value", "Text", "");
            ViewBag.EstadoCartera = new SelectList(objComprobante.ListarEstadosCartera(objSession.CodigoEmpresa, objSession.Idioma), "Value", "Text", "");
            return View();
        }

        [HttpPost]
        public ActionResult Cartera(EmailModel obj)
        {
            
            return Json("");
        }

        public ActionResult GetDummy(GridSettings gridSettings)
        {
            // create json data 
            Dimol.dto.Combobox bcpDeudor = new Dimol.dto.Combobox();

            int totalRecords = 0;


            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<Dimol.dto.Combobox> lst = new List<Dimol.dto.Combobox>();


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from Dimol.dto.Combobox item in lst
                    select new
                    {
                        id = item.Value,
                        cell = new object[]
                        {
                            item.Value,
                            item.Text
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BuscarRutNombreCliente(string term)
        {
            Cliente objCliente = new Cliente();
            return Json(objCliente.ListarRutNombreCliente(term), JsonRequestBehavior.AllowGet);
        }

        public ActionResult BuscarRutNombreDeudor(string term)
        {
            Deudor obj = new Deudor();
            return Json(obj.ListarRutNombreDeudor(term), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetGestores(GridSettings gridSettings)
        {
            return GetGestor(gridSettings, 0, "0");
        }

        public JsonResult GetGestor(GridSettings gridSettings, int TipoCartera, string grupo)
        {
            // create json data 
            int grupoCobranza = 0;
            int gestor = 0;
            if (!string.IsNullOrEmpty(grupo))
            {
                grupoCobranza = Int32.Parse(grupo);
            }
            int totalRecords = Email.bcp.Vista.ListarGestoresCount(objSession.CodigoEmpresa, objSession.CodigoSucursal, TipoCartera, objSession.Permisos > 3? 0: objSession.Gestor, grupoCobranza, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);

            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<Dimol.dto.Combobox> lst = Email.bcp.Vista.ListarGestores(objSession.CodigoEmpresa, objSession.CodigoSucursal, TipoCartera, objSession.Permisos > 3 ? 0 : objSession.Gestor, grupoCobranza, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from Dimol.dto.Combobox item in lst
                    select new
                    {
                        id = item.Value,
                        cell = new object[]
                        {
                            item.Value,
                            item.Text
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEstados(GridSettings gridSettings)
        {
            // create json data 
            int totalRecords = Email.bcp.Vista.ListarEstadosCount(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);

            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<Dimol.dto.Combobox> lst = Email.bcp.Vista.ListarEstados(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from Dimol.dto.Combobox item in lst
                    select new
                    {
                        id = item.Value,
                        cell = new object[]
                        {
                            item.Value,
                            item.Text
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EnviarEmail(EmailModel model)
        {
            string lstEstados = "", lstGestores = "";
            var objects = JArray.Parse(model.Estados); // parse as array  
            foreach (string root in objects)
            {
                lstEstados += root + ",";
            }
            objects = JArray.Parse(model.Gestores); // parse as array  
            foreach (string root in objects)
            {
                lstGestores += root + ",";
            }
            lstEstados = lstEstados.Substring(0, lstEstados.Length - 1);
            lstGestores = lstGestores.Substring(0, lstGestores.Length - 1);

            bool s = Email.bcp.EnvioEmail.Enviar(Int32.Parse(model.Pclid), lstEstados, Int32.Parse(model.TipoCartera), lstGestores, model.EmailTodos, model.EmailContacto, objSession, ConfigurationManager.AppSettings["RutaReportesEmail"] , model.Email);


                return Json(s);
        }
    }
}
