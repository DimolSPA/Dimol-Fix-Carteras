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
using Newtonsoft.Json.Linq;
using Dimol.dto;
using Dimol.bcp;

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
            ViewBag.SituacionCartera = new SelectList(objComprobante.ListarSituacionCartera(objSession.Idioma), "Value", "Text", "");

            List<Combobox> lstEstadoDocumento = new List<Combobox>();
            lstEstadoDocumento.Add(new Combobox() { Text = "-- Seleccione --", Value = "" });
            lstEstadoDocumento.Add(new Combobox() { Text = "Vigente", Value = "V" });
            lstEstadoDocumento.Add(new Combobox() { Text = "Judicial", Value = "J" });

            ViewBag.EstadoDocumento = new SelectList(lstEstadoDocumento, "Value", "Text", "");

            Funciones objFunc = new Funciones();
            bool perfilGestor = false;

            foreach (string id in objFunc.ConfiguracionEmpStr(objSession.CodigoEmpresa, 143).Split(','))
            {
                if (objSession.PrfId.ToString() == id) perfilGestor = true;
            }

            if (perfilGestor)
            {
                ViewBag.Gestor = new SelectList(Dimol.Carteras.bcp.Gestor.ListarGestorCombo(objSession.CodigoEmpresa, objSession.CodigoSucursal, objSession.Email), "Value", "Text", "");
            }
            else
            {
                ViewBag.Gestor = new SelectList(Dimol.Carteras.bcp.Gestor.ListarGestoresCombo(objSession.CodigoEmpresa, objSession.CodigoSucursal), "Value", "Text", "");
            }

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
            
            bool ruta = Dimol.Reportes.bcp.Cartera.TraeTortaAgrupada(obj);
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

        public void GenerarReporteXLS(int? pclid, int? ctcid, string tipo, int rep, int? pag, string abogados, int? diasPre)
        {
            string ruta = "";
            if (pag == null)
            {
                pag = 357;
            }
            string timestamp = DateTime.Now.ToString("yyyyMMddhhmmss");
            string ubicacion = ConfigurationManager.AppSettings["RutaArchivos"];
            string filename = "";

            if (pag == 358)
            {
                switch (rep)
                {
                    case 1:
                        break;
                    case 2:
                        break;
                    case 4:
                        break;
                    case 14:
                        dto.ArbolJudicial obj = new dto.ArbolJudicial();
                        obj.Codemp = objSession.CodigoEmpresa;
                        obj.Idioma = objSession.Idioma;
                        obj.Codsuc = objSession.CodigoSucursal;
                        obj.Abogados = JArray.Parse(abogados).ToObject<int[]>();

                        obj.FechaReporte = DateTime.Now;
                        //obj.PathArchivo = ubicacion + "Documentos\\" + objSession.CodigoEmpresa + "\\arbol_judicial_global_" + objSession.CodigoEmpresa + "_" + timestamp + ".pdf";
                        obj.PathArchivo = ubicacion + "Documentos\\" + objSession.CodigoEmpresa + "\\arbol_judicial_global_" + objSession.CodigoEmpresa + "_" + timestamp + ".xls";
                        obj.IdReporte = rep;
                        obj.Pagina = (int)pag;

                        ruta = Dimol.Reportes.bcp.Judicial.TraeArbolJudicialXLS(obj);
                        filename = @"arbol_judicial_global_" + objSession.CodigoEmpresa + "_" + obj.Ctcid;
                        Response.Clear();
                        Response.AddHeader("Content-Disposition", "attachment;filename=" + filename + ".xls");
                        Response.ContentType = "application/vnd.xls";
                        Response.Cache.SetCacheability(HttpCacheability.NoCache); // not necessarily required
                        Response.Charset = "";
                        Response.Output.Write(ruta);
                        Response.End();
                        break;
                    case 15:
                        dto.Prescripciones objPrsc = new dto.Prescripciones();
                        objPrsc.Codemp = objSession.CodigoEmpresa;
                        objPrsc.Idioma = objSession.Idioma;
                        objPrsc.Codsuc = objSession.CodigoSucursal;
                        objPrsc.DiasPrescrip = (int)diasPre;

                        objPrsc.FechaReporte = DateTime.Now;                        
                        //objPrsc.PathArchivo = ubicacion + "Documentos\\" + objSession.CodigoEmpresa + "\\prescripciones_" + objSession.CodigoEmpresa + "_" + timestamp + ".xls";
                        objPrsc.IdReporte = rep;
                        objPrsc.Pagina = (int)pag;

                        ruta = Dimol.Reportes.bcp.Judicial.TraePrescripciones(objPrsc);
                        filename = @"prescripciones_" + objSession.CodigoEmpresa + "_" + objPrsc.Ctcid;
                        Response.Clear();
                        Response.AddHeader("Content-Disposition", "attachment;filename=" + filename + ".xls");
                        Response.ContentType = "application/vnd.xls";
                        Response.Cache.SetCacheability(HttpCacheability.NoCache); // not necessarily required
                        Response.Charset = "";
                        Response.Output.Write(ruta);
                        Response.End();
                        break;
                    default:

                        break;

                }

            }
            else if(pag == 357)
            {
                switch (rep)
                {
                    case 23:
                        dto.InformeBajas obj = new dto.InformeBajas();
                        obj.Codemp = objSession.CodigoEmpresa;
                        obj.Idioma = objSession.Idioma;
                        obj.Codsuc = objSession.CodigoSucursal;
                        obj.Pclid = (int)pclid;

                        obj.FechaReporte = DateTime.Now;
                        //objPrsc.PathArchivo = ubicacion + "Documentos\\" + objSession.CodigoEmpresa + "\\prescripciones_" + objSession.CodigoEmpresa + "_" + timestamp + ".xls";
                        obj.IdReporte = rep;
                        obj.Pagina = (int)pag;

                        ruta = Dimol.Reportes.bcp.Cartera.TraeInformeBajas(obj);
                        filename = @"informe_bajas_" + objSession.CodigoEmpresa + "_" + obj.Pclid;
                        Response.Clear();
                        Response.AddHeader("Content-Disposition", "attachment;filename=" + filename + ".xls");
                        Response.ContentType = "application/vnd.xls";
                        Response.Cache.SetCacheability(HttpCacheability.NoCache); // not necessarily required
                        Response.Charset = "";
                        Response.Output.Write(ruta);
                        Response.End();
                        break;
                    default:

                        break;
                }
            }
        }
        
        public JsonResult GeneraReporteArbolJudicial(string abogados, int rep, int? pag)
        {
            bool ruta = false;
            if (pag == null)
            {
                pag = 358;
            }
            string timestamp = DateTime.Now.ToString("yyyyMMddhhmmss");
            string ubicacion = ConfigurationManager.AppSettings["RutaArchivos"];

            if (pag == 358)
            {
                switch (rep)
                {
                    case 14:
                        dto.ArbolJudicial obj = new dto.ArbolJudicial();
                        obj.Codemp = objSession.CodigoEmpresa;
                        obj.Idioma = objSession.Idioma;
                        obj.Codsuc = objSession.CodigoSucursal;
                        obj.Abogados = JArray.Parse(abogados).ToObject<int[]>();

                        obj.FechaReporte = DateTime.Now;
                        //obj.PathArchivo = ubicacion + "Documentos\\" + objSession.CodigoEmpresa + "\\arbol_judicial_global_" + objSession.CodigoEmpresa + "_" + timestamp + ".pdf";
                        obj.PathArchivo = ubicacion + "Documentos\\" + objSession.CodigoEmpresa + "\\arbol_judicial_global_" + objSession.CodigoEmpresa + "_" + timestamp + ".pdf";
                        obj.IdReporte = rep;
                        obj.Pagina = (int)pag;
                        ruta = Dimol.Reportes.bcp.Judicial.TraeArbolJudicial(obj);
                        System.IO.File.Delete(obj.PathArchivo + ".fo");
                        if (ruta)
                        {
                            return Json(Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host + (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port) + "/Documentos/" + objSession.CodigoEmpresa + "/arbol_judicial_global_" + +objSession.CodigoEmpresa + "_" + timestamp + ".pdf");
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

        public JsonResult GeneraReporte(int pclid, int? ctcid, int tipoCartera, int? codigoCarga, string tipo, int rep, int? pag, int? gestor, string desde, string hasta, bool? vencidos, string rol)
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
                    case 22: // informe prejudicial 80/20
                        //bool ruta = false;
                        objLiq.Codemp = objSession.CodigoEmpresa;
                        objLiq.Pclid = pclid;
                        //objLiq.Ctcid = ctcid;// 1203973;// 1202065;//7598;
                        objLiq.TipoCartera = tipoCartera;
                        objLiq.EstadoCpbt = tipo;
                        objLiq.Idioma = objSession.Idioma;
                        objLiq.Sucid = objSession.CodigoSucursal;
                        if (!codigoCarga.HasValue) objLiq.Codid = 0;
                        else objLiq.Codid = (int)codigoCarga;
                        objLiq.FechaReporte = DateTime.Now;
                        objLiq.PathArchivo = ubicacion + "Documentos\\" + objSession.CodigoEmpresa + "\\informe_prejudicial_ochenta_" + objSession.CodigoEmpresa + "_" + objLiq.Pclid + "_" + timestamp + ".pdf";
                        objLiq.IdReporte = rep;
                        objLiq.Pagina = (int)pag;

                        ruta = Dimol.Reportes.bcp.Cartera.TraeInformePrejudicialOchenta(objLiq);
                        System.IO.File.Delete(objLiq.PathArchivo + ".fo");
                        if (ruta)
                        {
                            return Json(Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host + (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port) + "/Documentos/" + objSession.CodigoEmpresa + "/informe_prejudicial_ochenta_" + +objSession.CodigoEmpresa + "_" + objLiq.Pclid + "_" + timestamp + ".pdf");
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
                    case 1: // torta
                        dto.TortaAgrupada obj = new dto.TortaAgrupada();
                        obj.Codemp = objSession.CodigoEmpresa;
                        obj.Pclid = pclid;
                        obj.TipoCartera = tipoCartera;
                        obj.EstadoCpbt = tipo;
                        obj.Idioma = objSession.Idioma;
                        obj.Sucid = objSession.CodigoSucursal;

                        if (!gestor.HasValue) obj.CodGestor = 0;
                        else obj.CodGestor = (int)gestor;

                        if (!codigoCarga.HasValue) obj.CodigoCarga = 0;
                        else obj.CodigoCarga = (int)codigoCarga;

                        obj.FechaReporte = DateTime.Now;
                        obj.PathArchivo = ubicacion + "Documentos\\" + objSession.CodigoEmpresa + "\\torta_" + objSession.CodigoEmpresa + "_" + obj.Pclid + "_" + timestamp + ".pdf";
                        obj.IdReporte = rep;
                        obj.Pagina = (int)pag;
                                                
                        ruta = Dimol.Reportes.bcp.Cartera.TraeTortaAgrupada(obj);
                        System.IO.File.Delete(obj.PathArchivo + ".fo");
                        if (ruta)
                        {
                            return Json(Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host + (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port) + "/Documentos/" + objSession.CodigoEmpresa + "/torta_" + +objSession.CodigoEmpresa + "_" + obj.Pclid + "_" + timestamp + ".pdf");
                        }
                        else
                        {
                            return Json("");
                        }
                        break;
                    case 7: // recepcion documentos
                        dto.RecepcionDocumentos objR = new dto.RecepcionDocumentos();
                        objR.Codemp = objSession.CodigoEmpresa;
                        objR.Pclid = pclid;
                        //objLiq.Ctcid = ctcid;// 1203973;// 1202065;//7598;
                        objR.TipoCartera = tipoCartera;
                        objR.Estcpbt = tipo;
                        objR.Idioma = objSession.Idioma;
                        //if (desde.HasValue) 
                        objR.FechaDesde = DateTime.Parse(desde);
                        //if (hasta.HasValue) 
                        objR.FechaHasta = DateTime.Parse(hasta);
                        objR.Codsuc = objSession.CodigoSucursal;
                        objR.FechaReporte = DateTime.Now;
                        objR.PathArchivo = ubicacion + "Documentos\\" + objSession.CodigoEmpresa + "\\recepcion_documentos_" + objSession.CodigoEmpresa + "_" + objR.Pclid + "_" + timestamp + ".pdf";
                        objR.IdReporte = rep;
                        objR.Pagina = (int)pag;

                        ruta = Dimol.Reportes.bcp.Cartera.TraeRecepcionDocumentos(objR);
                        System.IO.File.Delete(objR.PathArchivo + ".fo");
                        if (ruta)
                        {
                            return Json(Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host + (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port) + "/Documentos/" + objSession.CodigoEmpresa + "/recepcion_documentos_" + +objSession.CodigoEmpresa + "_" + objR.Pclid + "_" + timestamp + ".pdf");
                        }
                        else
                        {
                            return Json("");
                        }
                        break;
                    case 21: //Trekking Cartera
                        dto.TrekkingCartera objTrekking = new dto.TrekkingCartera();
                        objTrekking.Codemp = objSession.CodigoEmpresa;
                        objTrekking.Pclid = pclid;
                        objTrekking.TipoCartera = tipoCartera;
                        objTrekking.Idioma = objSession.Idioma;
                        objTrekking.Sucid = objSession.CodigoSucursal;

                        if (!vencidos.HasValue || vencidos == false) objTrekking.Vencidos = 0;
                        else objTrekking.Vencidos = 1;

                        if (!gestor.HasValue) objTrekking.CodGestor = 0;
                        else objTrekking.CodGestor = (int)gestor;

                        objTrekking.FechaReporte = DateTime.Now;
                        objTrekking.FechaEmisionCorta = DateTime.Now;
                        objTrekking.PathArchivo = ubicacion + "Documentos\\" + objSession.CodigoEmpresa + "\\trekking_cartera_" + objSession.CodigoEmpresa + "_" + objTrekking.Pclid + "_" + timestamp + ".pdf";
                        objTrekking.IdReporte = rep;
                        objTrekking.Pagina = (int)pag;
                                                
                        ruta = Dimol.Reportes.bcp.Cartera.TraeTrekkingCartera(objTrekking);
                        System.IO.File.Delete(objTrekking.PathArchivo + ".fo");
                        if (ruta)
                        {
                            return Json(Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host + (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port) + "/Documentos/" + objSession.CodigoEmpresa + "/trekking_cartera_" + +objSession.CodigoEmpresa + "_" + objTrekking.Pclid + "_" + timestamp + ".pdf");
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
                    case 9:
                        dto.HojaTramite obj = new dto.HojaTramite();
                        obj.Codemp = objSession.CodigoEmpresa;
                        obj.Pclid = pclid;
                        obj.Idioma = objSession.Idioma;
                        obj.IdSuc = objSession.CodigoSucursal;
                        obj.EstadoCpbt = tipo;
                        if (!ctcid.HasValue) obj.Ctcid = 0;
                        else obj.Ctcid = (int)ctcid;
                        obj.Rol = rol;
                        obj.FechaReporte = DateTime.Now;
                        obj.PathArchivo = ubicacion + "Documentos\\" + objSession.CodigoEmpresa + "\\hoja_tramite_" + objSession.CodigoEmpresa + "_" + objLiq.Pclid + "_" + timestamp + ".pdf";
                        obj.IdReporte = rep;
                        obj.Pagina = (int)pag;
                        ruta = Dimol.Reportes.bcp.Judicial.TraeHojaTramiteCliente(obj);
                        System.IO.File.Delete(obj.PathArchivo + ".fo");
                        if (ruta)
                        {
                            return Json(Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host + (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port) + "/Documentos/" + objSession.CodigoEmpresa + "/hoja_tramite_" + +objSession.CodigoEmpresa + "_" + inf.Pclid + "_" + timestamp + ".pdf");
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
                    case 16: // informe judicial 80/20
                        inf.Codemp = objSession.CodigoEmpresa;
                        inf.Pclid = pclid;

                        inf.TipoCartera = tipoCartera;
                        inf.EstadoCpbt = tipo;
                        inf.Idioma = objSession.Idioma;
                        inf.Sucid = objSession.CodigoSucursal;
                        if (!codigoCarga.HasValue) inf.Codid = 0;
                        else inf.Codid = (int)codigoCarga;
                        inf.FechaReporte = DateTime.Now;
                        inf.PathArchivo = ubicacion + "Documentos\\" + objSession.CodigoEmpresa + "\\informe_judicial_ochenta_" + objSession.CodigoEmpresa + "_" + inf.Pclid + "_" + timestamp + ".pdf";
                        inf.IdReporte = rep;
                        inf.Pagina = (int)pag;

                        ruta = Dimol.Reportes.bcp.Judicial.TraeInformeJudicialOchenta(inf);
                        System.IO.File.Delete(inf.PathArchivo + ".fo");
                        if (ruta)
                        {
                            return Json(Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host + (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port) + "/Documentos/" + objSession.CodigoEmpresa + "/informe_judicial_ochenta_" + +objSession.CodigoEmpresa + "_" + inf.Pclid + "_" + timestamp + ".pdf");
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

        public JsonResult GetAbogados(GridSettings gridSettings)
        {

            int totalRecords = Dimol.Reportes.bcp.Judicial.ListarAbogadosCount(objSession.CodigoEmpresa, objSession.CodigoSucursal);

            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<Dimol.dto.Combobox> lst = Dimol.Reportes.bcp.Judicial.ListarAbogados(objSession.CodigoEmpresa, objSession.CodigoSucursal);

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
    }
}
