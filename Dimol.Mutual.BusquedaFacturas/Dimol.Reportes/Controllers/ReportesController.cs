using Dimol.Reportes.Models;
using Mvc.HtmlHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dimol.Carteras.bcp;
using Dimol.Reportes.dto;
using System.Configuration;

namespace Dimol.Reportes.Controllers
{
    public class ReportesController : Dimol.Controllers.BaseController
    {
        //
        // GET: /ReportesCartera/

        public ActionResult Cartera(string ctr, string pag)
        {
            this.SettingAccount();
            if (string.IsNullOrEmpty(pag))
            {
                pag = "357";
            }
            ViewBag.Codemp = objSession.CodigoEmpresa;
            ViewBag.Reporte = new SelectList(bcp.Cartera.ListarReportes(Int32.Parse(pag), objSession.Idioma, "Seleccione"), "Value", "Text", "");
            Comprobante objComprobante = new Comprobante();
            Deudor objDeudor = new Deudor();
            ViewBag.TipoCartera = new SelectList(objComprobante.ListarTipoCartera(objSession.Idioma), "Value", "Text", "");
            ViewBag.CodigoCarga = new SelectList(objComprobante.ListarCodigoCarga(objSession.CodigoEmpresa, 1, "Seleccione"), "Value", "Text", "");
            ViewBag.Agrupa = new SelectList(objComprobante.ListarCodigoCarga(objSession.CodigoEmpresa, 1, "Seleccione"), "Value", "Text", "");
            ViewBag.Estado = new SelectList(objDeudor.ListarEstado(objSession.Idioma), "Value", "Text", "");
            ViewBag.EstadoCartera = new SelectList(objComprobante.ListarEstadosCartera(objSession.CodigoEmpresa, objSession.Idioma), "Value", "Text", "");
            return View();
        }

        public ActionResult Predefinidos(string ctr, string pag)
        {
            this.SettingAccount();
            if (string.IsNullOrEmpty(pag))
            {
                pag = "357";
            }
            if (objSession.ClienteAsociado > 0)
            {
                ViewBag.Pclid = objSession.ClienteAsociado;
                ViewBag.NombreRutCliente = objSession.RutClienteAsociado + " - " + objSession.NombreClienteAsociado;
            }
            ViewBag.Cartera = ctr;
            ViewBag.Codemp = objSession.CodigoEmpresa;
            ViewBag.Reporte = new SelectList(bcp.Cartera.ListarReportes(Int32.Parse(pag), objSession.Idioma, "Seleccione"), "Value", "Text", "");
            Comprobante objComprobante = new Comprobante();
            Deudor objDeudor = new Deudor();
            ViewBag.TipoCartera = new SelectList(objComprobante.ListarTipoCartera(objSession.Idioma), "Value", "Text", "");
            ViewBag.CodigoCarga = new SelectList(objComprobante.ListarCodigoCarga(objSession.CodigoEmpresa, 1, "Seleccione"), "Value", "Text", "");
            ViewBag.pag = pag;
            return View();
        }

        [HttpPost]
        public ActionResult Cartera(CarteraModel obj)
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

        public JsonResult GeneraReporteTorta(int pclid,  int tipo, int rep, string cartera)
        {
            TortaAgrupada obj = new TortaAgrupada();
            obj.Codemp = objSession.CodigoEmpresa;
            obj.Pclid = pclid;
            obj.TipoCartera = tipo;
            obj.EstadoCpbt = cartera;
            obj.Idioma = objSession.Idioma;
            obj.Sucid = objSession.CodigoSucursal;
            obj.FechaReporte = DateTime.Now;
            obj.PathArchivo = @"C:\Archivos\Documentos\torta_" + objSession.CodigoEmpresa + "_" + pclid + ".pdf";
            
            bool ruta = Dimol.Reportes.bcp.Cartera.TraeTortaAgrorama(obj);
            System.IO.File.Delete(obj.PathArchivo + ".fo");
            if (ruta)
            {
                return Json(Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host + (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port) + "/Documentos/torta_" + +objSession.CodigoEmpresa + "_" + obj.Pclid + ".pdf");
            }
            else
            {
                return Json("");
            }


        }

        public JsonResult GeneraReporte(int pclid, int? ctcid, int tipoCartera, int? codigoCarga, string tipo, int rep, int? pag)
        {
            bool ruta = false;
            if (pag == null)
            {
                pag = 357;
            }
            string timestamp = DateTime.Now.ToString("yyyyMMddhhmmss");

            dto.InformePrejudicial objLiq = new InformePrejudicial();
            string ubicacion = ConfigurationManager.AppSettings["RutaArchivos"];
            if (pag == 357)
            {
                switch (rep)
                {
                    case 6: // informe prejudicial
                        //bool ruta = false;
                        objLiq.Codemp = objSession.CodigoEmpresa;
                        objLiq.Pclid = pclid;
                        //objLiq.Ctcid = ctcid;// 1203973;// 1202065;//7598;
                        objLiq.TipoCartera = tipoCartera;
                        objLiq.EstadoCpbt = tipo;
                        objLiq.Idioma = objSession.Idioma;
                        objLiq.Sucid = objSession.CodigoSucursal;
                        objLiq.FechaReporte = DateTime.Now;
                        objLiq.PathArchivo = ubicacion + "Documentos\\" + objSession.CodigoEmpresa + "\\informe_prejudicial_" + objSession.CodigoEmpresa + "_" + objLiq.Pclid + "_" + timestamp + ".pdf";
                        objLiq.IdReporte = rep;
                        objLiq.Pagina = (int)pag;

                        ruta = Dimol.Reportes.bcp.Cartera.TraeInformePrejudicial(objLiq);
                        System.IO.File.Delete(objLiq.PathArchivo + ".fo");
                        if (ruta)
                        {
                            return Json(Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host + (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port) + "/Documentos/" + objSession.CodigoEmpresa + "/informe_prejudicial_" + +objSession.CodigoEmpresa + "_" + objLiq.Pclid + "_" + timestamp + ".pdf");
                        }
                        else
                        {
                            return Json("");
                        }
                        break;
                    case 16: //informe prej codigo carga
                        //InformePrejudicial objLiqDura = new InformePrejudicial();
                        objLiq.Codemp = objSession.CodigoEmpresa;
                        objLiq.Pclid = pclid;
                        //objLiqDura.Ctcid = ctcid;// 1203973;// 1202065;//7598;
                        objLiq.TipoCartera = tipoCartera;
                        objLiq.EstadoCpbt = tipo;
                        objLiq.Idioma = objSession.Idioma;
                        objLiq.Sucid = objSession.CodigoSucursal;
                        objLiq.FechaReporte = DateTime.Now;
                        objLiq.PathArchivo = ubicacion + "Documentos\\" + objSession.CodigoEmpresa + "\\informe_prejudicial_codigo_carga_" + objSession.CodigoEmpresa + "_" + objLiq.Pclid + "_" + timestamp + ".pdf";
                        objLiq.IdReporte = rep;
                        objLiq.Pagina = (int)pag;
                        objLiq.Codid = (int)codigoCarga;



                        ruta = Dimol.Reportes.bcp.Cartera.TraeInformePrejudicialCodigoCarga(objLiq);
                        System.IO.File.Delete(objLiq.PathArchivo + ".fo");
                        if (ruta)
                        {
                            return Json(Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host + (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port) + "/Documentos/" + objSession.CodigoEmpresa + "/informe_prejudicial_codigo_carga_" + +objSession.CodigoEmpresa + "_" + objLiq.Pclid + "_" + timestamp + ".pdf");
                        }
                        else
                        {
                            return Json("");
                        }
                        break;
                    case 19: //informe prej asegurado
                        objLiq.Codemp = objSession.CodigoEmpresa;
                        objLiq.Pclid = pclid;
                        //objLiq.Ctcid = ctcid;// 1203973;// 1202065;//7598;
                        objLiq.TipoCartera = tipoCartera;
                        objLiq.EstadoCpbt = tipo;
                        objLiq.Idioma = objSession.Idioma;
                        objLiq.Sucid = objSession.CodigoSucursal;
                        objLiq.FechaReporte = DateTime.Now;
                        objLiq.PathArchivo = ubicacion + "Documentos\\" + objSession.CodigoEmpresa + "\\informe_prejudicial_asegurado_" + objSession.CodigoEmpresa + "_" + objLiq.Pclid + "_" + timestamp + ".pdf";
                        objLiq.IdReporte = rep;
                        objLiq.Pagina = (int)pag;

                        ruta = Dimol.Reportes.bcp.Cartera.TraeInformePrejudicialAsegurado(objLiq);
                        System.IO.File.Delete(objLiq.PathArchivo + ".fo");
                        if (ruta)
                        {
                            return Json(Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host + (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port) + "/Documentos/" + objSession.CodigoEmpresa + "/informe_prejudicial_asegurado_" + +objSession.CodigoEmpresa + "_" + objLiq.Pclid + "_" + timestamp + ".pdf");
                        }
                        else
                        {
                            return Json("");
                        }
                        break;
                    default:
                        return Json("");
                        break;
                }
            }
            else if (pag == 358)
            {
                dto.InformeJudicial inf = new InformeJudicial();
                switch (rep)
                {
                    case 1: // informe judicial
                        inf.Codemp = objSession.CodigoEmpresa;
                        inf.Pclid = pclid;

                        inf.TipoCartera = tipoCartera;
                        inf.EstadoCpbt = tipo;
                        inf.Idioma = objSession.Idioma;
                        inf.Sucid = objSession.CodigoSucursal;
                        inf.FechaReporte = DateTime.Now;
                        inf.PathArchivo = ubicacion + "Documentos\\" + objSession.CodigoEmpresa + "\\informe_judicial_" + objSession.CodigoEmpresa + "_" + inf.Pclid + "_" + timestamp + ".pdf";
                        inf.IdReporte = rep;
                        inf.Pagina = (int)pag;

                        ruta = Dimol.Reportes.bcp.Judicial.TraeInformeJudicial(inf);
                        System.IO.File.Delete(inf.PathArchivo + ".fo");
                        if (ruta)
                        {
                            return Json(Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host + (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port) + "/Documentos/" + objSession.CodigoEmpresa + "/informe_judicial_" + +objSession.CodigoEmpresa + "_" + inf.Pclid + "_" + timestamp + ".pdf");
                        }
                        else
                        {
                            return Json("");
                        }
                        break;
                    case 2: //informe judicial asegurado
                        inf.Codemp = objSession.CodigoEmpresa;
                        inf.Pclid = pclid;
                        inf.TipoCartera = tipoCartera;
                        inf.EstadoCpbt = tipo;
                        inf.Idioma = objSession.Idioma;
                        inf.Sucid = objSession.CodigoSucursal;
                        inf.FechaReporte = DateTime.Now;
                        inf.PathArchivo = ubicacion + "Documentos\\" + objSession.CodigoEmpresa + "\\informe_judicial_asegurado_" + objSession.CodigoEmpresa + "_" + inf.Pclid + "_" + timestamp + ".pdf";
                        inf.IdReporte = rep;
                        inf.Pagina = (int)pag;



                        ruta = Dimol.Reportes.bcp.Judicial.TraeInformeJudicial(inf);
                        System.IO.File.Delete(inf.PathArchivo + ".fo");
                        if (ruta)
                        {
                            return Json(Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host + (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port) + "/Documentos/" + objSession.CodigoEmpresa + "/informe_judicial_asegurado_" + +objSession.CodigoEmpresa + "_" + inf.Pclid + "_" + timestamp + ".pdf");
                        }
                        else
                        {
                            return Json("");
                        }
                        break;
                    case 3: //informe judicial codigo carga
                        inf.Codemp = objSession.CodigoEmpresa;
                        inf.Pclid = pclid;

                        inf.TipoCartera = tipoCartera;
                        inf.EstadoCpbt = tipo;
                        inf.Idioma = objSession.Idioma;
                        inf.Sucid = objSession.CodigoSucursal;
                        inf.FechaReporte = DateTime.Now;
                        inf.PathArchivo = ubicacion + "Documentos\\" + objSession.CodigoEmpresa + "\\informe_judicial_codigo_carga_" + objSession.CodigoEmpresa + "_" + inf.Pclid + "_" + timestamp + ".pdf";
                        inf.IdReporte = rep;
                        inf.Pagina = (int)pag;
                        inf.Codid = (int)codigoCarga;

                        ruta = Dimol.Reportes.bcp.Judicial.TraeInformeJudicialCodigoCarga(inf);
                        System.IO.File.Delete(inf.PathArchivo + ".fo");
                        if (ruta)
                        {
                            return Json(Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host + (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port) + "/Documentos/" + objSession.CodigoEmpresa + "/informe_judicial_codigo_carga_" + +objSession.CodigoEmpresa + "_" + inf.Pclid + "_" + timestamp + ".pdf");
                        }
                        else
                        {
                            return Json("");
                        }
                        break;
                    case 4: //informe judicial codigo carga asegurado
                        inf.Codemp = objSession.CodigoEmpresa;
                        inf.Pclid = pclid;

                        inf.TipoCartera = tipoCartera;
                        inf.EstadoCpbt = tipo;
                        inf.Idioma = objSession.Idioma;
                        inf.Sucid = objSession.CodigoSucursal;
                        inf.FechaReporte = DateTime.Now;
                        inf.PathArchivo = ubicacion + "Documentos\\" + objSession.CodigoEmpresa + "\\informe_judicial_codigo_carga_asegurado_" + objSession.CodigoEmpresa + "_" + inf.Pclid + "_" + timestamp + ".pdf";
                        inf.IdReporte = rep;
                        inf.Pagina = (int)pag;
                        inf.Codid = (int)codigoCarga;

                        ruta = Dimol.Reportes.bcp.Judicial.TraeInformeJudicialCodigoCarga(inf);
                        System.IO.File.Delete(inf.PathArchivo + ".fo");
                        if (ruta)
                        {
                            return Json(Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host + (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port) + "/Documentos/" + objSession.CodigoEmpresa + "/informe_judicial_codigo_carga_asegurado_" + +objSession.CodigoEmpresa + "_" + inf.Pclid + "_" + timestamp + ".pdf");
                        }
                        else
                        {
                            return Json("");
                        }
                        break;
                    case 12: //informe judicial incluye asegurado
                        inf.Codemp = objSession.CodigoEmpresa;
                        inf.Pclid = pclid;

                        inf.TipoCartera = tipoCartera;
                        inf.EstadoCpbt = tipo;
                        inf.Idioma = objSession.Idioma;
                        inf.Sucid = objSession.CodigoSucursal;
                        inf.FechaReporte = DateTime.Now;
                        inf.PathArchivo = ubicacion + "Documentos\\" + objSession.CodigoEmpresa + "\\informe_judicial_incluye_asegurado_" + objSession.CodigoEmpresa + "_" + inf.Pclid + "_" + timestamp + ".pdf";
                        inf.IdReporte = rep;
                        inf.Pagina = (int)pag;
   
                        ruta = Dimol.Reportes.bcp.Judicial.TraeInformeJudicialIncluyeAsegurado(inf);
                        System.IO.File.Delete(inf.PathArchivo + ".fo");
                        if (ruta)
                        {
                            return Json(Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host + (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port) + "/Documentos/" + objSession.CodigoEmpresa + "/informe_judicial_incluye_asegurado_" + +objSession.CodigoEmpresa + "_" + inf.Pclid + "_" + timestamp + ".pdf");
                        }
                        else
                        {
                            return Json("");
                        }
                        break;
                    case 13: //informe judicial quiebra incluye asegurado
                        inf.Codemp = objSession.CodigoEmpresa;
                        inf.Pclid = pclid;

                        inf.TipoCartera = tipoCartera;
                        inf.EstadoCpbt = tipo;
                        inf.Idioma = objSession.Idioma;
                        inf.Sucid = objSession.CodigoSucursal;
                        inf.FechaReporte = DateTime.Now;
                        inf.PathArchivo = ubicacion + "Documentos\\" + objSession.CodigoEmpresa + "\\informe_judicial_quiebra_incluye_asegurado_" + objSession.CodigoEmpresa + "_" + inf.Pclid + "_" + timestamp + ".pdf";
                        inf.IdReporte = rep;
                        inf.Pagina = (int)pag;

                        ruta = Dimol.Reportes.bcp.Judicial.TraeInformeJudicialQuiebra(inf);
                        System.IO.File.Delete(inf.PathArchivo + ".fo");
                        if (ruta)
                        {
                            return Json(Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host + (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port) + "/Documentos/" + objSession.CodigoEmpresa + "/informe_judicial_quiebra_incluye_asegurado_" + +objSession.CodigoEmpresa + "_" + inf.Pclid + "_" + timestamp + ".pdf");
                        }
                        else
                        {
                            return Json("");
                        }
                        break;
                    default:
                        return Json("");
                        break;
                }
            }
            else
            {
                return Json("");
            }
        }

        public JsonResult ListarCodigoCarga(int codemp, int pclid)
        {
            Comprobante obj = new Comprobante();
            return Json(new SelectList(obj.ListarCodigoCarga(codemp, pclid, "Seleccione"), "Value", "Text"));
        }
    }
}
