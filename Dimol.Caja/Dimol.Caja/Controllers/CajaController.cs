using Mvc.HtmlHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dimol.Carteras.bcp;
using System.Transactions;
using Dimol.Caja.Models;
using Newtonsoft.Json;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;
using System.Web.UI;

namespace Dimol.Caja.Controllers
{
    public class CajaController : Dimol.Controllers.BaseController
    {
        //
        // GET: /Caja/
        #region "Views"


        public ActionResult RecepcionDocumento()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            Comprobante objComprobante = new Comprobante();
            Direccion objDireccion = new Direccion();
            Deudor objDeudor = new Deudor();
            ViewBag.Moneda = new SelectList(objComprobante.ListarMonedas(objSession.CodigoEmpresa), "Value", "Text", "");
            ViewBag.IdPais = new SelectList(objDireccion.ListarPais(), "Value", "Text", 56);
            ViewBag.IdRegion = new SelectList(objDireccion.ListarRegion(56), "Value", "Text", 6);
            ViewBag.IdCiudad = new SelectList(objDireccion.ListarCiudad(6), "Value", "Text", 23);
            ViewBag.IdComuna = new SelectList(objDireccion.ListarComuna(23), "Value", "Text", "");
            return View();
        }
        public ActionResult TraspasoDocumento()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            Comprobante objComprobante = new Comprobante();
            Direccion objDireccion = new Direccion();
            ViewBag.Moneda = new SelectList(objComprobante.ListarMonedas(objSession.CodigoEmpresa), "Value", "Text", "");
            ViewBag.IdPais = new SelectList(objDireccion.ListarPais(), "Value", "Text", 56);
            ViewBag.IdRegion = new SelectList(objDireccion.ListarRegion(56), "Value", "Text", 6);
            ViewBag.IdCiudad = new SelectList(objDireccion.ListarCiudad(6), "Value", "Text", 23);
            ViewBag.IdComuna = new SelectList(objDireccion.ListarComuna(23), "Value", "Text", "");

           //carga masiva
            ViewBag.Contrato = new SelectList(objComprobante.ListarContrato(objSession.CodigoEmpresa, 0, 1, "Seleccione"), "Value", "Text", "");
            ViewBag.CodigoCarga = new SelectList(objComprobante.ListarCodigoCarga(objSession.CodigoEmpresa, 0, "Seleccione"), "Value", "Text", "");
            ViewBag.TipoCartera = new SelectList(objComprobante.ListarTipoCartera(objSession.Idioma), "Value", "Text", "");

            return View();
        }
        public ActionResult TraspasoComercialDocumento()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
           // ViewBag.CajaCriterioFacturacion = bcp.Documento.ListarCajaCriterioFacturacion(objSession.CodigoEmpresa, 90);
            return View();
        }
        public ActionResult TraspasoFinanzasDocumento()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
        public ActionResult PanelAprobacion()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
        #endregion
        public JsonResult ListarCajaIngresoDocumentosGrilla(GridSettings gridSettings)
        {
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;
            List<dto.Documento> lst = bcp.Documento.ListarCajaIngresoDocumentosGrilla(objSession.CodigoEmpresa, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString());

            int totalRecords = lst.Count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (

                    from dto.Documento item in lst
                    where item.Row >= startRow && item.Row <= endRow
                    select new
                    {

                        id = item.DocumentoId,
                        cell = new object[]
                        {
                           item.NumeroDocumento,
                           item.RutCliente,
                           item.Cliente,
                           item.RutDedor,
                           item.Deudor,
                           item.RutAsegurado,
                           item.Asegurado,
                           item.FecIngreso,
                           item.Moneda,
                           item.ValorIngreso,
                           item.MontoIngreso,
                           item.DocumentoId,
                           item.pclid,
                           item.ctcid,
                           item.sbcid,
                           item.Codmon,
                           item.EstatusId
                           
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        

        public ActionResult BuscarRutNombreCliente(string term)
        {
            Carteras.bcp.Cliente objCliente = new Carteras.bcp.Cliente();
            return Json(objCliente.ListarRutNombreCliente(term), JsonRequestBehavior.AllowGet);
        }

        public ActionResult BuscarRutNombreDeudor(string term)
        {
            
            return Json(bcp.Documento.ListarRutNombreDeudor(term), JsonRequestBehavior.AllowGet);
        }

        public ActionResult BuscarRutNombreAsegurado(string term)
        {
            Carteras.bcp.Comprobante obj = new Carteras.bcp.Comprobante();
            return Json(obj.ListarRutNombreAsegurado(term), JsonRequestBehavior.AllowGet);
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GuardarDocumentoCajaRecepcion(RecepcionDocumentoModel documento)
        {
            int result = -1;
            result = bcp.Documento.InsertUpdateDocumentoCaja(documento.DocumentoId, objSession.CodigoEmpresa, documento.NumeroDocumento, 
                                                            Int32.Parse(documento.Pclid), Int32.Parse(documento.Ctcid), documento.Sbcid,
                                                            Int32.Parse(documento.Moneda),documento.MontoIngreso, 1, objSession.UserId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarCajaTraspasoDocumentosGrilla(GridSettings gridSettings)
        {
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;
            List<dto.Documento> lst = bcp.Documento.ListarCajaTraspasoDocumentosGrilla(objSession.CodigoEmpresa, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString());

            int totalRecords = lst.Count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (

                    from dto.Documento item in lst
                    where item.Row >= startRow && item.Row <= endRow
                    select new
                    {

                        id = item.DocumentoId + "|" + item.EstatusId + "|" + item.StatusProceso + "|" + item.RutDedor,
                        cell = new object[]
                        {
                           item.NumeroDocumento,
                           item.RutCliente,
                           item.Cliente,
                           item.RutDedor,
                           item.Deudor,
                           item.RutAsegurado,
                           item.Asegurado,
                           item.FecIngreso,
                           item.FecStatusProceso,
                           item.Moneda,
                           item.ValorIngreso,
                           item.MontoIngreso,
                           item.DocumentoId,
                           item.pclid,
                           item.ctcid,
                           item.sbcid,
                           item.Codmon,
                           item.Estatus,
                           item.EstatusId,
                           item.StatusProceso
                           
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult TraspasarComercial(string ids)
        {
            string traspasos = "";
            List<string> lst = JsonConvert.DeserializeObject<List<string>>(ids);
            traspasos = bcp.Documento.TraspasoComercial(lst, objSession.CodigoEmpresa, objSession.UserId);
            return Json(traspasos);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult TraspasoComercialIngresado(string ids)
        {
            string traspasos = "";
            List<string> lst = JsonConvert.DeserializeObject<List<string>>(ids);
            traspasos = bcp.Documento.TraspasoComercialIngresado(lst, objSession.CodigoEmpresa, objSession.UserId);
            return Json(traspasos);
        }

        public JsonResult ListarCajaTraspasoComercialDocumentosGrilla(GridSettings gridSettings)
        {
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;
            List<dto.Documento> lst = bcp.Documento.ListarCajaTraspasoComercialDocumentosGrilla(objSession.CodigoEmpresa, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString());

            int totalRecords = lst.Count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (

                    from dto.Documento item in lst
                    where item.Row >= startRow && item.Row <= endRow
                    select new
                    {

                        id = item.DocumentoId + "|" + item.EstatusId + "|" + item.CriterioId,
                        cell = new object[]
                        {
                           item.NumeroDocumento,
                           item.RutCliente,
                           item.Cliente,
                           item.RutDedor,
                           item.Deudor,
                           item.RutAsegurado,
                           item.Asegurado,
                           item.FecIngreso,
                           item.Moneda,
                           item.ValorIngreso,
                           item.MontoIngreso,
                           item.DocumentoId,
                           item.pclid,
                           item.ctcid,
                           item.sbcid,
                           item.EstatusId,
                           item.CriterioId,
                           item.MontoFacturar,
                           item.Observaciones
                           
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarCajaCriterioFacturacionCombo(int pclid, string criterioId)
        {

            return Json(new SelectList(bcp.Documento.ListarCajaCriterioFacturacionCombo(objSession.CodigoEmpresa, pclid), "Value", "Text", criterioId));
        }
        public ActionResult SiAplicaCriterio(int documentoId, int criterioId)
        {

            return Json(bcp.Documento.SiAplicaCriterio(documentoId, criterioId), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DefineCriterio(int documentoId, int criterioId)
        {
            List<dto.DocumentoCriterio> lst = bcp.Documento.TraeCajaRecepcionDocumentosCriterio(documentoId, criterioId);
            foreach (dto.DocumentoCriterio p in lst)
            {
                ViewBag.MontoFacturar = p.MontoFacturar.ToString("N2");
                ViewBag.Observaciones = p.Observaciones;
                ViewBag.IsEditable = p.IsEditable;

            }
            return Json(new
            {
                MontoFacturar = ViewBag.MontoFacturar,
                Observaciones = ViewBag.Observaciones,
                IsEditable = ViewBag.IsEditable
            }, JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GuardarCajaRecepcionDocumentosCriterio(int documentoId, int criterioId, string montoFacturar, string observaciones)
        {
            int result = -1;
            result = bcp.Documento.GuardarCajaRecepcionDocumentosCriterio(documentoId, criterioId, montoFacturar, observaciones, objSession.UserId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult TraspasoFinanzas(string ids)
        {
            string traspasos = "";
            List<string> lst = JsonConvert.DeserializeObject<List<string>>(ids);
            traspasos = bcp.Documento.TraspasoFinanzas(lst, objSession.CodigoEmpresa, objSession.UserId);
            return Json(traspasos);
        }

        public JsonResult ListarCajaTraspasoFinanzasDocumentosGrilla(GridSettings gridSettings)
        {
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;
            List<dto.Documento> lst = bcp.Documento.ListarCajaTraspasoFinanzasDocumentosGrilla(objSession.CodigoEmpresa, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString());

            int totalRecords = lst.Count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (

                    from dto.Documento item in lst
                    where item.Row >= startRow && item.Row <= endRow
                    select new
                    {

                        id = item.DocumentoId + "|" + item.EstatusId + "|" + item.CriterioId + "|" + item.MontoFacturar,
                        cell = new object[]
                        {
                           item.NumeroDocumento,
                           item.RutCliente,
                           item.Cliente,
                           item.RutDedor,
                           item.Deudor,
                           item.RutAsegurado,
                           item.Asegurado,
                           item.FecIngreso,
                           item.Moneda,
                           item.ValorIngreso,
                           item.MontoIngreso,
                           item.DocumentoId,
                           item.pclid,
                           item.ctcid,
                           item.sbcid,
                           item.EstatusId,
                           item.CriterioId,
                           item.MontoFacturar,
                           item.Observaciones
                           
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public JsonResult TraspasoFacturacion(string ids, string factura, string observaciones)
        {
            string traspasos = "";
            List<string> lst = JsonConvert.DeserializeObject<List<string>>(ids);
            traspasos = bcp.Documento.TraspasoFacturacion(lst, objSession.CodigoEmpresa, factura, observaciones, objSession.UserId);
            return Json(traspasos);
        }

        public ActionResult CriterioPorDefecto(int pclid, int documentoId)
        {

            return Json(bcp.Documento.CriterioPorDefecto(objSession.CodigoEmpresa,pclid, documentoId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarCiudad(int region)
        {
            Direccion objDireccion = new Direccion();
            return Json(new SelectList(objDireccion.ListarCiudad(region), "Value", "Text"));
        }

        public JsonResult ListarComuna(int ciudad)
        {
            Direccion objDireccion = new Direccion();
            return Json(new SelectList(objDireccion.ListarComuna(ciudad), "Value", "Text"));
        }

        public JsonResult ListarRegion(int pais)
        {
            Direccion objDireccion = new Direccion();
            return Json(new SelectList(objDireccion.ListarRegion(pais), "Value", "Text"));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GuardarDeudor(string Rut, string Nombre, string ApellidoPaterno, string ApellidoMaterno, string NombreFantasia, int IdComuna, string ParticularEmpresa, string Direccion)
        {
            Deudor objDeudor = new Deudor();
            int CodigoDeudor = 0;
            int ctcid = objDeudor.BuscarIdDeudor(Rut, objSession.CodigoEmpresa);
            if (ctcid != 0 && ctcid != objDeudor.CodigoDeudor)
            {
                CodigoDeudor = ctcid;
            }
            string nacional = "N";
            
            string quiebra = "N";
           
            if (string.IsNullOrEmpty(ApellidoMaterno))
            {
                ApellidoMaterno = string.Empty;
            }

            if (string.IsNullOrEmpty(NombreFantasia))
            {
                NombreFantasia = string.Concat(Nombre.TrimEnd(), " ", ApellidoPaterno.TrimEnd(), " ", ApellidoMaterno);
                NombreFantasia = NombreFantasia.TrimEnd();
            }

            ctcid = objDeudor.GuardarDeudor(objSession.CodigoEmpresa, CodigoDeudor, Nombre, ApellidoPaterno, ApellidoMaterno, Rut, NombreFantasia, IdComuna, ParticularEmpresa, Direccion, "", quiebra, nacional, 1, false);
                       
            return Json(ctcid);
        }

        public ActionResult ExportToExcelDocumentosFinanzas()
        {

            var grid = new GridView();
            List<dto.DocumentoExcelFinanza> lst = bcp.Documento.ListarCajaTraspasoFinanzasDocumentosExcel(objSession.CodigoEmpresa);
            grid.DataSource = lst;
            grid.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=DocumentosFinanzas.xls");
            Response.ContentType = "application/ms-excel";

            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return View("MyView");
        }

        [HttpPost]
        public JsonResult Upload(string tipo, string rut, string Ctcid, string Pclid, string TipoDocumento)
        {
            string fileName = "";
            string archivosItau = "";
            string path = "";
            int id = 0;
            Dimol.bcp.Funciones objFunc = new Dimol.bcp.Funciones();

            try
            {

                for (int i = 0; i < Request.Files.Count; i++)
                {
                    fileName = "";
                    HttpPostedFileBase file = Request.Files[i]; //Uploaded file
                    int fileSize = file.ContentLength;
                    string mimeType = file.ContentType;
                    System.IO.Stream fileContent = file.InputStream;
                    switch (tipo)
                    {
                        
                        case "Carga":
                            id = 15;
                            fileName = objSession.CodigoEmpresa + "_" + objSession.UserId + "_";
                            path = ConfigurationManager.AppSettings["RutaArchivos"] + objFunc.Configuracion_Str(id) + "\\";
                            //Use the following properties to get file's name, size and MIMEType
                            fileName = fileName + file.FileName;
                            objFunc.CreaCarpetas(path);

                            //To save file, use SaveAs method
                            file.SaveAs(path + fileName); //File will be saved in application root
                            break;
                                   
                    }
                }

            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.CarteraController.Carga.Upload", 0);
                fileName = "";
            }
            if (tipo == "CargaItau")
            {
                return Json(archivosItau);
            }
            else
            {
                return Json(fileName);
            }

        }

        public JsonResult ProcesoCargaMasiva(CargaMasivaModel model, string ids, GridSettings gridSettings)
        {
            try
            {
                bool error = false;
                string[] rut = model.RutCliente.Split('-');
                Dimol.Carteras.dto.CargaMasiva objCarga = new Dimol.Carteras.dto.CargaMasiva();
                objCarga.Archivo = model.Archivo;
                objCarga.ArchivoQuiebra = model.ArchivoQuiebra;
                objCarga.ArchivoJudicial = model.CargaJudicial;
                objCarga.CodigoCarga = model.CodigoCarga;
                objCarga.Contrato = model.Contrato;
                objCarga.NombreCliente = rut[1].Trim();
                objCarga.Pclid = model.Pclid;
                objCarga.RutCliente = rut[0].Trim();
                objCarga.TipoCartera = model.TipoCartera;

                List<string> lstDocumentos = new List<string>();
                List<string> Deudoreslist = new List<string>();
                if (!string.IsNullOrEmpty(ids))
                {
                    lstDocumentos = JsonConvert.DeserializeObject<List<string>>(ids);
                }
               
                if (model.Archivo != "")
                {

                    if (model.CargaJudicial && !model.ArchivoQuiebra)
                    {
                        List<Dimol.Carteras.dto.CargaJudicial> lst = bcp.CargaMasiva.CargarDatosJudicial(model.Archivo);
                        Dimol.Carteras.bcp.CargaMasiva.ProcesoCargaJudicial(lst, objCarga, objSession);
                    }
                    if (model.ArchivoQuiebra && !model.CargaJudicial)
                    {
                        List<Dimol.Carteras.dto.DatosCarga> lst = bcp.CargaMasiva.CargarDatos(model.Archivo);
                        Dimol.Carteras.bcp.CargaMasiva.ProcesoCargaQuiebra(lst, objCarga, objSession);
                    }
                    if (!model.ArchivoQuiebra && !model.CargaJudicial)
                    {
                        List<Dimol.Carteras.dto.DatosCarga> lst;
                        switch (objCarga.Pclid)
                        {
                            case 424:
                                break;
                        }
                        if (objCarga.Pclid == 424)
                        {
                            lst = bcp.CargaMasiva.CargarDatosOriencoop(model.Archivo, objSession.CodigoEmpresa, objCarga.Pclid, Int32.Parse(objCarga.CodigoCarga));
                        }
                        else
                        {
                            lst = bcp.CargaMasiva.CargarDatos(model.Archivo);
                        }
                        switch (objCarga.Pclid)
                        {
                            case 559: //mutual ley previsional
                                error =  Dimol.Carteras.bcp.CargaMasiva.ProcesoCargaMutualLey(lst, objCarga, objSession);
                                break;
                            default:
                                error = Dimol.Carteras.bcp.CargaMasiva.ProcesoCarga(lst, objCarga, objSession);
                                break;
                        }

                        //Validar que el deudor tiene datos para cargar
                        if (lst.Count > 0)
                        {
                            string[] id;
                            
                            foreach (string doc in lstDocumentos)
                            {
                                id = doc.Split('|');
                                //for (int i = 0; i < lst.Count() - 1; i++)
                                foreach (Dimol.Carteras.dto.DatosCarga docarga in lst)
                                {
                                    if ((docarga.Rut + docarga.Dv) == id[3])
                                  {
                                     //lst[i].IsChecked = true;
                                      Deudoreslist.Add(id[0] + "|" + id[1] + "|" + id[2] + "|" + id[3]);
                                  }
                                }
                                
                            }
                        }

                    }

                }

                if (objCarga.ListaErrores.Count > 0)
                {
                    string[] id;

                    foreach (string docItem in Deudoreslist)
                    {
                        id = docItem.Split('|');
                        //for (int i = 0; i < lst.Count() - 1; i++)
                        foreach (Dimol.Carteras.dto.ErrorCarga doerror in (objCarga.ListaErrores))
                        {
                            if (doerror.Rut == id[3])
                            {
                                //lst[i].IsChecked = true;
                                Deudoreslist.Remove(docItem);
                            }
                        }

                    }
                }

                bcp.Documento.TraspasoComercialIngresado(Deudoreslist, objSession.CodigoEmpresa, objSession.UserId);

                int totalPages = (int)Math.Ceiling((float)objCarga.ListaErrores.Count / (float)gridSettings.pageSize);

                var jsonData = new
                {
                    total = totalPages,
                    page = gridSettings.pageIndex,
                    records = objCarga.ListaErrores.Count,
                    rows =
                    (
                        from Dimol.Carteras.dto.ErrorCarga item in objCarga.ListaErrores
                        select new
                        {
                            id = 1,
                            cell = new object[]
                        {
                            item.Rut+"-"+item.Dv,
                            item.Nombre,
                            item.Numero,
                            item.TipoDocumento,
                            item.TipoError
                        }
                        }
                    ).ToArray()
                };

                return Json(new { success = true, data = objCarga.ListaErrores }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Caja/ProcesoCargaMasiva", 150);
                return Json(new { success = false, data = ex.Message }, JsonRequestBehavior.AllowGet);

            }

        }

        public JsonResult ListarCodigoCarga(int codemp, int pclid)
        {
            Comprobante obj = new Comprobante();
            return Json(new SelectList(obj.ListarCodigoCarga(codemp, pclid, "Seleccione"), "Value", "Text"));
        }

        public JsonResult ListarContrato(int codemp, int pclid, int tipoCartera)
        {
            Comprobante obj = new Comprobante();
            return Json(new SelectList(obj.ListarContrato(codemp, pclid, tipoCartera, "Seleccione"), "Value", "Text"));
        }

        public ActionResult ExportToExcelDocumentosControlGestion(GridSettings gridSettings)
        {

            var grid = new GridView();
            List<dto.DocumentoExcelControlGestion> lst = bcp.Documento.ListarCajaTraspasoDocumentosExcel(objSession.CodigoEmpresa, gridSettings.where.groupOp, "DocumentoId", gridSettings.sortOrder.ToString());
            grid.DataSource = lst;
            grid.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=DocumentosCargaMasiva.xls");
            Response.ContentType = "application/ms-excel";

            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return View("MyView");
        }

        public JsonResult ListarCajaPanelAprobacionDocumentosGrilla(GridSettings gridSettings)
        {
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;
            List<dto.Documento> lst = bcp.Documento.ListarCajaPanelAprobacionDocumentosGrilla(objSession.CodigoEmpresa, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString());

            int totalRecords = lst.Count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (

                    from dto.Documento item in lst
                    where item.Row >= startRow && item.Row <= endRow
                    select new
                    {

                        id = item.DocumentoId + "|" + item.EstatusId + "|" + item.CriterioId + "|" + item.MontoFacturar,
                        cell = new object[]
                        {
                           item.NumeroDocumento,
                           item.RutCliente,
                           item.Cliente,
                           item.RutDedor,
                           item.Deudor,
                           item.RutAsegurado,
                           item.Asegurado,
                           item.FecIngreso,
                           item.Moneda,
                           item.ValorIngreso,
                           item.MontoIngreso,
                           item.DocumentoId,
                           item.pclid,
                           item.ctcid,
                           item.sbcid,
                           item.EstatusId,
                           item.CriterioId,
                           item.Criterio,
                           item.MontoFacturar,
                           item.Observaciones
                           
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult AprobacionTraspasoFinanzas(string ids)
        {
            string traspasos = "";
            List<string> lst = JsonConvert.DeserializeObject<List<string>>(ids);
            traspasos = bcp.Documento.AprobacionTraspasoFinanzas(lst, objSession.CodigoEmpresa, objSession.UserId);
            return Json(traspasos);
        }
        public ActionResult obtieneEstatusDocumento(int documentoId)
        {

            return Json(bcp.Documento.obtieneEstatusDocumento(documentoId), JsonRequestBehavior.AllowGet);
        }
    }
}
