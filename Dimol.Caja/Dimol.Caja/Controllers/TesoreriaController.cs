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
using Dimol.bcp;
using System.Configuration;
using System.IO;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using Dimol.Caja.Reportes.Datasets;

namespace Dimol.Caja.Controllers
{
    public class TesoreriaController : Dimol.Controllers.BaseController
    {
        //
        // GET: /Tesoreria/

        public ActionResult ConciliacionBanco()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            ViewBag.Motivo = new SelectList(bcp.Tesoreria.ListarMotivoBanco(), "Value", "Text", "");
            ViewBag.TipoConciliacion = new SelectList(bcp.Tesoreria.ListarTipoConciliacionMovimiento(), "Value", "Text", "");
            ViewBag.Banco = new SelectList(bcp.Tesoreria.ListarBancos(objSession.CodigoEmpresa), "Value", "Text");
            ViewBag.TipoCuenta = new SelectList(bcp.Tesoreria.ListarTipoCuentaTesoreria(), "Value", "Text", "");
            ViewBag.EstadoDocumento = new SelectList(bcp.Tesoreria.ListarEstadoBanco(), "Value", "Text", "");

            return View();
        }
        public ActionResult Liquidacion()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }

            ViewBag.TipoConciliacion = new SelectList(bcp.Tesoreria.ListarTipoConciliacionMovimiento(), "Value", "Text", "");

            return View();
        }

        public ActionResult Remesa()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }
        public ActionResult ConsultaPago()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }
        public ActionResult EfectivoCustodia()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            ViewBag.Banco = new SelectList(bcp.Tesoreria.ListarBancos(objSession.CodigoEmpresa), "Value", "Text");
            ViewBag.TipoConciliacion = new SelectList(bcp.Tesoreria.ListarTipoConciliacionMovimiento(), "Value", "Text", "");
            ViewBag.EstadoDocumento = new SelectList(bcp.Tesoreria.ListarEstadoBanco(), "Value", "Text", "");
            ViewBag.Cuenta = new SelectList(bcp.Tesoreria.ListarCuentas(objSession.CodigoEmpresa), "Value", "Text");
            return View();
        }
        public ActionResult BuscarRutNombreCliente(string term)
        {
            Carteras.bcp.Cliente objCliente = new Carteras.bcp.Cliente();
            return Json(objCliente.ListarRutNombreCliente(term), JsonRequestBehavior.AllowGet);
        }

        public ActionResult BuscarRutNombreDeudor(string term)
        {
            Carteras.bcp.Deudor obj = new Carteras.bcp.Deudor();
            return Json(obj.ListarRutNombreDeudor(term), JsonRequestBehavior.AllowGet);
        }

        public ActionResult BuscarNombreGestor(string term)
        {

            return Json(bcp.Tesoreria.ListarNombreGestor(term), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetDummy(GridSettings gridSettings)
        {
            // create json data 

            int totalRecords = 0;


            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<string> lst = new List<string>();


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from string item in lst
                    select new
                    {
                        id = item,
                        cell = new object[]
                        {
                            item,
                            item
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ListarTesoreriaCuentasBancariasGrilla(GridSettings gridSettings)
        {
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;
            List<dto.CuentaBancaria> lst = bcp.Tesoreria.ListarTesoreriaCuentasBancariasGrilla(objSession.CodigoEmpresa, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString());

            int totalRecords = lst.Count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (

                    from dto.CuentaBancaria item in lst
                    where item.Row >= startRow && item.Row <= endRow
                    select new
                    {

                        id = item.NumCuenta,
                        cell = new object[]
                        {
                           item.NumCuenta,
                           item.TipoCuenta,
                           item.Banco,
                           item.MontoConciliar,
                           item.CuentaId
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ListarCartolaMovimientosGrilla(string numCuenta, GridSettings gridSettings)
        {
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;
            List<dto.CartolaMovimiento> lst = bcp.Tesoreria.ListarCartolaMovimientosGrilla(objSession.CodigoEmpresa, numCuenta, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString());

            int totalRecords = lst.Count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (

                    from dto.CartolaMovimiento item in lst
                    where item.Row >= startRow && item.Row <= endRow
                    select new
                    {

                        id = item.MovimientoId,
                        cell = new object[]
                        {
                           item.MovimientoId,
                           item.NumCuenta,
                           item.FecMovimiento,
                           item.Monto,
                           item.Motivo,
                           item.Sucursal,
                           item.Movimiento,
                           item.MotivoSistema,
                           item.Estado,
                           item.MotivoSistemaId,
                           item.EstadoId,
                           item.Observacion,
                           item.CuentaId,
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Upload(string tipo)
        {
            string fileName = "";
            string path = "";
            Funciones objFunc = new Funciones();

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
                        case "CargaCartola":
                            string ext = "";
                            fileName = "CartolaBanco_" + objSession.CodigoEmpresa + "_" + objSession.UserId + "_" + DateTime.Now.ToString("yyyyMMddHHmmss_");
                            path = ConfigurationManager.AppSettings["RutaArchivosCartolaBanco"];
                            ext = Path.GetExtension(file.FileName).ToLower();
                            fileName = fileName + file.FileName;
                            if (ext != ".pdf" && ext != ".xls" && ext != ".txt" && ext != ".xlsx")
                            {
                                return Json(-1);
                            }
                            else
                            {
                                objFunc.CreaCarpetas(path);

                                file.SaveAs(path + fileName);
                            }
                           
                            break;
                        case "CargaArchivoComprobante":
                            fileName = "Comprobante_" + objSession.CodigoEmpresa + "_" + objSession.UserId + "_" + DateTime.Now.ToString("yyyyMMddHHmmss_");
                            path = ConfigurationManager.AppSettings["RutaComprobanteConciliacion"];
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
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.TesoreriaController.Carga.Upload", 0);
                fileName = "";
            }
            return Json(fileName);
        }

        public JsonResult DescargarCartolaBanco(string ArchivoCartola, string numCuenta, GridSettings gridSettings)
        {
            try
            {
                List<dto.DatosCargaCartola> lstResult = new List<dto.DatosCargaCartola>();
                if (ArchivoCartola != "")
                {
                    List<dto.DatosCargaCartola> lst = bcp.Tesoreria.CargarDatosCartola(ArchivoCartola, numCuenta, objSession.UserId);

                    lstResult = bcp.Tesoreria.ProcesoCargaCartolaBanco(lst, numCuenta, objSession.CodigoEmpresa, objSession.UserId);
                }



                return Json(new { success = lstResult.Count > 0 ? false: true, data = lstResult }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Tesoreria/DescargarCartolaBanco", 150);
                return Json(new { success = false, data = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ListarEstadoBancoCombo(string estadoId)
        {

            return Json(new SelectList(bcp.Tesoreria.ListarEstadoBanco(), "Value", "Text", estadoId));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult ActualizarEstadoMovimientoCartola(int movimientoId, int cuentaId, int tipoEstadoId)
        {
            int result = -1;
            result = bcp.Tesoreria.ActualizarEstadoMovimientoCartola(objSession.CodigoEmpresa, movimientoId, cuentaId, tipoEstadoId, objSession.UserId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult ActualizarObservacionMovimientoCartola(int movimientoId, int cuentaId, int tipoEstadoId, string observacion)
        {
            int result = -1;
            result = bcp.Tesoreria.ActualizarObservacionMovimientoCartola(objSession.CodigoEmpresa, movimientoId, cuentaId, tipoEstadoId, observacion, objSession.UserId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult ExisteConciliacionComprobante(string numComprobante)
        {
            int result = -1;
            result = bcp.Tesoreria.ExistConciliacioncomprobante(numComprobante);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult InsertarConciliacionMovimiento(int movimientoId, string numComprobante, string custodiaId,
                                                        int pclid, int ctcid, int gestorId, int tipoConciliacion, string pathArchivo, string numCuenta)
        {
            int result = -1;
            //pathArchivo = ConfigurationManager.AppSettings["RutaComprobanteConciliacion"].Replace(@"\\", @"\") + pathArchivo;
            result = bcp.Tesoreria.InsertarConciliacionMovimiento(objSession.CodigoEmpresa,movimientoId, numComprobante, custodiaId, pclid, ctcid, gestorId, tipoConciliacion, pathArchivo, numCuenta, objSession.UserId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult TraspasoPanelProtestado(string ids, string numCuenta, int cuentaId)
        {
            string traspasos = "";
            List<string> lst = JsonConvert.DeserializeObject<List<string>>(ids);
            traspasos = bcp.Tesoreria.TraspasoPanelProtestado(lst, cuentaId, objSession.CodigoEmpresa, objSession.UserId);
            return Json(traspasos);
        }

        public ActionResult ExportToExcelPanelDemandas(string numCuenta)
        {

            var grid = new GridView();
            List<dto.CartolaMovimientoExcel> lst = bcp.Tesoreria.ListarCartolaMovimientosExcel(objSession.CodigoEmpresa, numCuenta);
            grid.DataSource = lst;
            grid.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=CartolaBanco.xls");
            Response.ContentType = "application/ms-excel";

            Response.Charset = "";
            StringWriter sw = new StringWriter();
            System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);

            grid.RenderControl(htw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return View("MyView");
        }

        public JsonResult ListarGestorConciliacion(int pclid, int ctcid)
        {

            return Json(new SelectList(bcp.Tesoreria.ListarGestorConciliacion(objSession.CodigoEmpresa, pclid, ctcid), "Value", "Text", ""));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult ExisteCuentaBancaria(string numCuenta)
        {
            int result = -1;
            result = bcp.Tesoreria.ExistCuentaBancaria(numCuenta);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult InsertarCuentaBancaria(string numCuenta, int bancoId, int tipoCuentaId)
        {
            int result = -1;
            result = bcp.Tesoreria.InsertarCuentaBancaria(objSession.CodigoEmpresa, numCuenta, bancoId, tipoCuentaId, objSession.UserId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SaveGridUpdate(FormCollection form)
        {
            string lll = form.ToString();
            
            //return Json(new[] { new
            //{
            //    numDoc = form["NumDoc"],
            //    montoDoc = form["MontoDoc"],
            //    fechaDoc = form["FechaDoc"],
            //    fechaProDoc = form["FechaProDoc"],
            //    id = form["id"]
            //}});
            return Json(new
            {
                numDoc = form["NumDoc"],
                montoDoc = form["MontoDoc"],
                fechaDoc = form["FechaDoc"],
                fechaProDoc = form["FechaProDoc"],
                id = form["id"]
            });
        }

        public JsonResult ListarDocumentosCustodiaGrilla(string numCuenta, GridSettings gridSettings)
        {
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;
            List<dto.DocumentoCustodia> lst = bcp.Tesoreria.ListarDocumentosCustodiaGrilla(objSession.CodigoEmpresa, numCuenta, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString());

            int totalRecords = lst.Count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (

                    from dto.DocumentoCustodia item in lst
                    where item.Row >= startRow && item.Row <= endRow
                    select new
                    {

                        id = item.CustodiaId,
                        cell = new object[]
                        {
                           item.CustodiaId,
                           item.FecDoc,
                           item.RutCliente,
                           item.Cliente,
                           item.RutDeudor,
                           item.Deudor,
                           item.Monto,
                           item.Gestor,
                           item.GiradoA,
                           item.TipoBanco,
                           item.NumDocumento,
                           item.Estado,
                           item.FecProrroga,
                           item.Pclid,
                           item.Ctcid,
                           item.GestorId,
                           item.EstadoId
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post), ValidateInput(false)]
        public JsonResult InsertarDocumentoCustodia(string documentos, string numCuenta, int pclid, int ctcid, int gestorId, string bancoId, 
                                                    string recibe, string gestor, string deudor, string cliente, string banco)
        {
            int result = 1;
            List <dto.DocumentoCustodiaGrid> lst = JsonConvert.DeserializeObject<List <dto.DocumentoCustodiaGrid>> (documentos);
            result = bcp.Tesoreria.InsertarDocumentoCustodia(objSession.CodigoEmpresa, numCuenta, pclid, ctcid, gestorId, recibe, bancoId,lst, objSession.UserId);

            var report = new LocalReport
            {
                ReportPath = Server.MapPath(@"~\Reportes\DocumentoEnCustodia.rdlc"),
            };
            ReportParameterCollection reportParameters = new ReportParameterCollection();
            reportParameters.Add(new ReportParameter("RutDeudor", bcp.Reporte.getRutFormato(deudor.Split('-')[0])));
            reportParameters.Add(new ReportParameter("RutCliente", bcp.Reporte.getRutFormato(cliente.Split('-')[0])));
            reportParameters.Add(new ReportParameter("Gestor", gestor));
            reportParameters.Add(new ReportParameter("Cliente", cliente.Split('-')[1]));
            reportParameters.Add(new ReportParameter("Deudor", deudor.Split('-')[1]));
            reportParameters.Add(new ReportParameter("Fecha", DateTime.Now.ToString()));
            reportParameters.Add(new ReportParameter("GiradoA", recibe));
            reportParameters.Add(new ReportParameter("Banco", banco));
            report.SetParameters(reportParameters);

            ReportDataSource rds = new ReportDataSource("dsCustodia", lst);
            report.DataSources.Clear();
            report.DataSources.Add(rds);
            byte[] pdf = report.Render("PDF");
            string strPdf = Convert.ToBase64String(pdf);
            var returnOjb = new { result = result, pdf = strPdf };

            return Json(returnOjb, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarCartolaMovimientosPendienteGrilla(string numCuenta, string fechaDocumento, string montoDocumento, GridSettings gridSettings)
        {
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;
            List<dto.CartolaMovimiento> lst = bcp.Tesoreria.ListarCartolaMovimientosPendienteGrilla(objSession.CodigoEmpresa, numCuenta,fechaDocumento,montoDocumento, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString());

            int totalRecords = lst.Count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (

                    from dto.CartolaMovimiento item in lst
                    where item.Row >= startRow && item.Row <= endRow
                    select new
                    {

                        id = item.MovimientoId,
                        cell = new object[]
                        {
                           item.MovimientoId,
                           item.NumCuenta,
                           item.FecMovimiento,
                           item.Monto,
                           item.Motivo,
                           item.Sucursal,
                           item.Movimiento,
                           item.MotivoSistema,
                           item.Estado,
                           item.MotivoSistemaId,
                           item.EstadoId
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult ActualizarEstadoDocumentoCustodia(int custodiaId, int tipoEstadoId)
        {
            int result = -1;
            result = bcp.Tesoreria.ActualizarEstadoDocumentoCustodia(objSession.CodigoEmpresa, custodiaId, tipoEstadoId, objSession.UserId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult InsertarConciliacionCustodia(int movimientoId, string numComprobante, string custodiaId,
                                                        int pclid, int ctcid, int gestorId, int tipoConciliacion, string pathArchivo, string numCuenta)
        {
            int result = -1;
            result = bcp.Tesoreria.InsertarConciliacionCustodia(objSession.CodigoEmpresa, movimientoId, numComprobante, custodiaId, pclid, ctcid, gestorId, tipoConciliacion, numCuenta, objSession.UserId);
            if (result > 0){
                //var report = new LocalReport
                //{
                //    ReportPath = Server.MapPath(@"~\Reportes\CustodiaConciliacion.rdlc"),
                //};
                //ReportParameterCollection reportParameters = new ReportParameterCollection();
                //var objCabecera = new dto.ReporteCabecera();
                //objCabecera = bcp.Reporte.obtenerCabecera(objSession.CodigoEmpresa, result);

                //reportParameters.Add(new ReportParameter("NumComprobante", objCabecera.NumComprobante));
                //reportParameters.Add(new ReportParameter("RutDeudor", bcp.Reporte.getRutFormato(objCabecera.RutDeudor)));
                //reportParameters.Add(new ReportParameter("RutCliente", bcp.Reporte.getRutFormato(objCabecera.RutCliente)));
                //reportParameters.Add(new ReportParameter("Gestor", objCabecera.Gestor));
                //reportParameters.Add(new ReportParameter("Cliente", objCabecera.Cliente));
                //reportParameters.Add(new ReportParameter("Deudor", objCabecera.Deudor));
                //reportParameters.Add(new ReportParameter("Fecha", objCabecera.FecDoc.ToString()));
                //report.SetParameters(reportParameters);


                //List<dto.DocumentoCustodiaReporte> lst = bcp.Reporte.ObtenerDocumentoCustodiaDetail(objSession.CodigoEmpresa, result);
                //ReportDataSource rds = new ReportDataSource("DsDocumentoCustodia", lst);
                //report.DataSources.Add(rds);
                //byte[] pdf = report.Render("PDF");
                //string strPdf = Convert.ToBase64String(pdf);
                var returnOjb = new { success = true, pdf = string.Empty };
                return Json(returnOjb, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarMovimientosProtestadosGrilla(string numCuenta, GridSettings gridSettings)
        {
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;
            List<dto.CartolaMovimiento> lst = bcp.Tesoreria.ListarMovimientosProtestadosGrilla(objSession.CodigoEmpresa, numCuenta, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString());

            int totalRecords = lst.Count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (

                    from dto.CartolaMovimiento item in lst
                    where item.Row >= startRow && item.Row <= endRow
                    select new
                    {

                        id = item.MovimientoId,
                        cell = new object[]
                        {
                           item.MovimientoId,
                           item.NumCuenta,
                           item.FecMovimiento,
                           item.Monto,
                           item.Motivo,
                           item.Sucursal,
                           item.Movimiento,
                           item.MotivoSistema,
                           item.Estado,
                           item.MotivoSistemaId,
                           item.EstadoId
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarMovimientosConciliadoGrilla(GridSettings gridSettings)
        {
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;
            List<dto.MovimientoConciliado> lst = bcp.Tesoreria.ListarMovimientosConciliadoGrilla(objSession.CodigoEmpresa, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString());

            var query = from dto.MovimientoConciliado item in lst
                        where item.Row >= startRow && item.Row <= endRow && item.EstadoLiquidacion == "Pendiente"
                        select new { item.ConciliacionId }.ConciliacionId.ToString().ToList();
            int totalRecords = query.ToList().Count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (

                    from dto.MovimientoConciliado item in lst
                    where item.Row >= startRow && item.Row <= endRow && item.EstadoLiquidacion == "Pendiente"
                    select new
                    {

                        id = item.ConciliacionId,
                        cell = new object[]
                        {
                           item.ConciliacionId,
                           item.MovimientoId,
                           item.CustodiaId,
                           item.Pclid,
                           item.Ctcid,
                           item.GestorId,
                           item.FechaConciliacion,
                           item.NumComprobante,
                           item.RutCliente,
                           item.Cliente,
                           item.RutDeudor,
                           item.Deudor,
                           item.MotivoSistema,
                           item.Monto,
                           item.Saldo,
                           item.Tipoconciliacion,
                           item.EstadoLiquidacion
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ListarMovimientosConciliadoPendienteGrilla(GridSettings gridSettings, string pclid, string ctcid, string fechaConciliacion, string numComprobante)
        {
            var fechaConcilia = new DateTime();
            DateTime.TryParse(fechaConciliacion, out fechaConcilia);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;
            List<dto.MovimientoConciliado> lst = bcp.Tesoreria.ListarMovimientosConciliadoAprobadosGrilla(objSession.CodigoEmpresa, fechaConcilia, pclid, ctcid, numComprobante, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString());
            var query = from dto.MovimientoConciliado item in lst
                        where item.Row >= startRow && item.Row <= endRow 
                        select new { item.ConciliacionId }.ConciliacionId.ToString().ToList();
            int totalRecords = query.ToList().Count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (

                    from dto.MovimientoConciliado item in lst
                    where item.Row >= startRow && item.Row <= endRow 
                    select new
                    {

                        id = item.ConciliacionId,
                        cell = new object[]
                        {
                            "comprobante",
                            "liquidacion",
                           item.ConciliacionId,
                           item.MovimientoId,
                           item.CustodiaId,
                           item.Pclid,
                           item.Ctcid,
                           item.GestorId,
                           item.FechaConciliacion,
                           item.NumComprobante,
                           item.RutCliente,
                           item.Cliente,
                           item.RutDeudor,
                           item.Deudor,
                           item.MotivoSistema,
                           item.Monto,
                           item.Saldo,
                           item.Tipoconciliacion,
                           item.EstadoLiquidacion,
                           item.Remesa
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult verFormularioLiquidacion(string conciliacionId, string pclid, string ctcid)
        {
            var model = new LiquidacionPorcentajeModel();
            var obj = new dto.FormLiquidacion();
            obj = bcp.Tesoreria.ListarFormLiquidacion(objSession.CodigoEmpresa, conciliacionId, pclid, ctcid);
            model.NumComprobante = obj.NumComprobante;
            model.RutCliente = obj.RutCliente;
            model.Cliente = obj.Cliente;
            model.RutDeudor = obj.RutDeudor;
            model.Deudor = obj.Deudor;
            model.Monto = obj.Monto.ToString("N2");
            model.Capital = obj.Capital.ToString("N2");
            model.Interes = obj.Interes.ToString("N2");
            model.Honorario = obj.Honorario.ToString("N2");
            model.CapitalPor = obj.CapitalPor.ToString() + "%";
            model.InteresPor = obj.InteresPor.ToString() + "%";
            model.HonorarioPor = obj.HonorarioPor.ToString() + "%";
            model.GastoJud = obj.GastoJud.ToString();
            model.GastoPre = obj.GastoPre.ToString();
            model.pclidLiqui = Int32.Parse(pclid);
            model.ctcidLiqui = Int32.Parse(ctcid);
            model.conciliacionId = Int32.Parse(conciliacionId);
            model.Saldo = obj.MontoRebajado.ToString("N2");
            model.SaldoRebajar = (obj.Monto - obj.MontoRebajado).ToString("N2");
            model.EstadoLiquidacionId = obj.EstadoLiquidacionId;
            return PartialView("_LiquidacionDocumentos", model);
        }

        public JsonResult ListarLiquidacionDocumentosDeudorGrilla(GridSettings gridSettings, int pclid, int ctcid)
        {
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;
            List<dto.DocumentoDeudor> lst = bcp.Tesoreria.ListarLiquidacionDocumentosDeudorGrilla(objSession.CodigoEmpresa, pclid, ctcid, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString());

            int totalRecords = lst.Count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (

                    from dto.DocumentoDeudor item in lst
                    where item.Row >= startRow && item.Row <= endRow
                    select new
                    {

                        id = item.Ccbid,
                        cell = new object[]
                        {
                          
                           item.Pclid,
                           item.Ctcid,
                           item.Ccbid,
                           item.Asegurado,
                           item.TipoDocumento,
                           item.Numero,
                           item.Estado,
                           item.FechaVencimiento,
                           item.Moneda,
                           item.Monto,
                           item.Saldo,
                           item.Intereses,
                           item.Honorarios,
                           item.GastoPrejudicial,
                           item.GastoJudicial,
                           item.TotalDeuda
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public PartialViewResult verFormularioImputacion(LiquidacionPorcentajeModel modl)
        {
            var model = new ImputacionPorcentajeModel();
            var obj = new dto.FormLiquidacion();
            obj = bcp.Tesoreria.ListarFormLiquidacion(objSession.CodigoEmpresa, modl.conciliacionId.ToString(), modl.pclidLiqui.ToString(), modl.ctcidLiqui.ToString());
            model.INumComprobante = obj.NumComprobante;
            model.IRutCliente = obj.RutCliente;
            model.ICliente = obj.Cliente;
            model.IRutDeudor = obj.RutDeudor;
            model.IDeudor = obj.Deudor;
            model.IMonto = obj.Monto.ToString("N2");
            model.ICapital = obj.Capital.ToString("N2");
            model.IInteres = obj.Interes.ToString("N2");
            model.IHonorario = obj.Honorario.ToString("N2");
            model.ICapitalPor = obj.CapitalPor.ToString() + "%";
            model.IInteresPor = obj.InteresPor.ToString() + "%";
            model.IHonorarioPor = obj.HonorarioPor.ToString() + "%";
            model.IGastoJud = obj.GastoJud.ToString();
            model.IGastoPre = obj.GastoPre.ToString();
            model.IpclidLiqui = modl.pclidLiqui;
            model.IctcidLiqui = modl.ctcidLiqui;
            model.IConciliacionId =  modl.conciliacionId;
            model.ISaldo = obj.MontoRebajado.ToString("N2");
            model.ISaldoRebajar = (obj.Monto - obj.MontoRebajado).ToString("N2");
            model.Documentos = modl.Docs;
            //if (!string.IsNullOrEmpty(modl.Docs))
            //    model.Documentos = string.Join(",", JsonConvert.DeserializeObject<List<string>>(modl.Docs));

            return PartialView("_ImputacionDocumento", model);
        }
        [HttpPost]
        public PartialViewResult verFormularioImputacionManual(ImputacionPorcentajeModel model)
        {
            model.ISaldoRebajar = (decimal.Parse(model.IMonto) - decimal.Parse(model.ISaldo)).ToString("N2");
            return PartialView("_ImputacionDocumento", model);
        }
        
        [HttpPost]
        public JsonResult ListarImputadosDocumentosDeudorGrilla(GridSettings gridSettings, ImputacionPorcentajeModel model, string montoCapital, string montoHonorario,
                                                                string montoInteres, string montoGastoPre, string montoGastoJud)
        {

            List<dto.DocumentoImputado> lst = bcp.Tesoreria.ListarImputacionDocumentosDeudorGrilla(objSession.CodigoEmpresa, model.IpclidLiqui, model.IctcidLiqui, model.Documentos, model.DocFinalizar, montoHonorario, montoInteres, montoCapital, montoGastoPre, montoGastoJud);

            int totalRecords = lst.Count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (

                    from dto.DocumentoImputado item in lst
                    select new
                    {

                        id = item.Ccbid,
                        cell = new object[]
                        {
                          
                           item.Pclid,
                           item.Ctcid,
                           item.Ccbid,
                           item.Asegurado,
                           item.TipoDocumento,
                           item.Numero,
                           item.Estado,
                           item.FechaVencimiento,
                           item.Moneda,
                           item.Monto,
                           item.Saldo,
                           item.CapitalDebitado,
                           item.Intereses,
                           item.InteresDebitado,
                           item.Honorarios,
                           item.HonorarioDebitado,
                           item.GastoPrejudicial,
                           item.PagoPreDebitado,
                           item.GastoJudicial,
                           item.PagoJudDebitado,
                           
                           item.TotalDeuda,
                           item.IndicaImputado
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post), ValidateInput(false)]
        public JsonResult InsertarDocumentoImputado(string docfinalizar, string documentos, int conciliacionId, int pclid, int ctcid)
        {
            int result = -1;
            
            List<dto.DocumentoPorImputar> lst = JsonConvert.DeserializeObject<List<dto.DocumentoPorImputar>>(documentos);
            List<dto.DocumentoPorFinalizar> lstfinalizar = new List<dto.DocumentoPorFinalizar>();
            if (!string.IsNullOrEmpty(docfinalizar))
            {
                lstfinalizar = JsonConvert.DeserializeObject<List<dto.DocumentoPorFinalizar>>(docfinalizar);
            }
            result = bcp.Tesoreria.InsertarDocumentoImputado(objSession.CodigoEmpresa, conciliacionId, pclid, ctcid, lst, lstfinalizar, objSession.UserId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post), ValidateInput(false)]
        public JsonResult FinalizarDocumentos(string docfinalizar, int conciliacionId, int pclid, int ctcid)
        {
            int result = -1;
            List<dto.DocumentoPorFinalizar> lstfinalizar = new List<dto.DocumentoPorFinalizar>();
            if (!string.IsNullOrEmpty(docfinalizar))
            {
                lstfinalizar = JsonConvert.DeserializeObject<List<dto.DocumentoPorFinalizar>>(docfinalizar);
            }
            result = bcp.Tesoreria.FinalizarDocumentos(objSession.CodigoEmpresa, pclid, ctcid, lstfinalizar, objSession.UserId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult InsertarPagoManual(int pclid, int ctcid, string fecha, string monto, int tipoConciliacion)
        {
            int result = -1;

            result = bcp.Tesoreria.InsertarPagoManual(objSession.CodigoEmpresa, pclid, ctcid, fecha, monto, tipoConciliacion, objSession.UserId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarMovimientosConciliadoAprobadoGrilla(GridSettings gridSettings, string pclid)
        {
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;
            List<dto.MovimientoConciliadoAprobado> lst = bcp.Tesoreria.ListarMovimientosConciliadoAprobadoGrilla(objSession.CodigoEmpresa, pclid, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString());

            int totalRecords = lst.Count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (

                    from dto.MovimientoConciliadoAprobado item in lst
                    where item.Row >= startRow && item.Row <= endRow
                    select new
                    {

                        id = item.ConciliacionId,
                        cell = new object[]
                        {
                            item.FechaConciliacion,
                            "comprobante",
                           item.ConciliacionId,
                           item.MovimientoId,
                           item.CustodiaId,
                           item.Pclid,
                           item.Ctcid,
                           item.GestorId,
                           item.NumComprobante,
                           item.RutCliente,
                           item.Cliente,
                           item.RutDeudor,
                           item.Deudor,
                           item.Capital,
                           item.Interes,
                           item.Honorarios,
                           item.OtrosGastos,
                           item.MontoRecuperado
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public PartialViewResult verCalculoRemesa(RemesaModel modl)
        {
            var model = new CalculoRemesaModel();
            model.IdsConciliacion = modl.IdsConciliacion;
          
            return PartialView("_CalculoRemesa", model);
        }
        [HttpPost]
        public JsonResult ListarCalculoRemesarGrilla(GridSettings gridSettings, CalculoRemesaModel model)
        {

            List<dto.ComprobanteRemesa> lst = bcp.Tesoreria.ListarRemesaGananciaGrilla(model.IdsConciliacion);

            int totalRecords = lst.Count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (

                    from dto.ComprobanteRemesa item in lst
                    select new
                    {

                        id = item.ImputacionId,
                        cell = new object[]
                        {
                           item.ImputacionId,
                           item.ConciliacionId,
                           item.Ccbid,
                           item.Pclid,
                           item.Ctcid,
                           item.NumComprobante,
                           item.Deudor,
                           item.Tipo,
                           item.NumDocumento,
                           item.Capital,
                           item.Interes,
                           item.Honorario,
                           item.RecuperadoGasto,
                           item.TotalRecuperado,
                           item.PorCapital,
                           item.PorInteres,
                           item.PorHonorario,
                           item.GananciaCapital,
                           item.GananciaInteres,
                           item.GananciaHonorario,
                           item.TotalGanancia,
                           item.TotalCliente,
                           item.Anticipo,
                           item.DocumentoId,
                           item.AnticipoDebitado,
                           item.ConciliacionTipoId,
                           item.ConciliacionTipo
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ListarRemesasGeneradasGrilla(GridSettings gridSettings)
        {
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;
            List<dto.Remesa> lst = bcp.Tesoreria.ListarRemesasGeneradasGrilla(gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString());

            int totalRecords = lst.Count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (

                    from dto.Remesa item in lst
                    select new
                    {

                        id = item.Id,
                        cell = new object[]
                        {
                           "informe",
                           item.Id,
                           item.Cliente,
                           item.CapitalRecuperado,
                           item.InteresRecuperado,
                           item.HonorarioRecuperado,
                           item.Capital,
                           item.Interes,
                           item.Honorario,
                           item.TotalFactura,
                           item.TotalDimol,
                           item.FechaRemesa
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post), ValidateInput(false)]
        public JsonResult InsertarRemesa(string documentos)
        {
            int result = -1;


            List<dto.ComprobanteRemesa> lstdocumentos = new List<dto.ComprobanteRemesa>();
            if (!string.IsNullOrEmpty(documentos))
            {
                lstdocumentos = JsonConvert.DeserializeObject<List<dto.ComprobanteRemesa>>(documentos);
            }
            result = bcp.Tesoreria.InsertarRemesa(objSession.CodigoEmpresa, lstdocumentos, objSession.UserId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarEfectivoCustodiaGrilla(GridSettings gridSettings)
        {
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;
            List<dto.EfectivoCustodia> lst = bcp.Tesoreria.ListarEfectivoCustodiaGrilla(objSession.CodigoEmpresa, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString());

            int totalRecords = lst.Count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (

                    from dto.EfectivoCustodia item in lst
                    where item.Row >= startRow && item.Row <= endRow
                    select new
                    {

                        id = item.CustodiaId,
                        cell = new object[]
                        {
                           item.CustodiaId,
                           item.ConciliacionId,
                           item.Pclid,
                           item.Ctcid,
                           item.NumComprobante,
                           item.FecDoc,
                           item.RutCliente,
                           item.Cliente,
                           item.RutDeudor,
                           item.Deudor,
                           item.Monto,
                           item.Saldo,
                           item.Gestor,
                           item.GiradoA,
                           item.TipoBanco,
                           item.NumDocumento,
                           item.Estado,
                           item.Tipoconciliacion,
                           item.EstadoLiquidacion,
                           item.FecProrroga,
                           item.Pclid,
                           item.Ctcid,
                           item.GestorId,
                           item.EstadoId,
                           item.EstadoConciliacionId
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public JsonResult InsertarEfectivoCustodia(int pclid, int ctcid, int gestorId, string bancoId, string recibe, string fechaDocumento, string monto)
        {
            int result = -1;

            result = bcp.Tesoreria.InsertarEfectivoCustodia(objSession.CodigoEmpresa, pclid, ctcid, gestorId, recibe, bancoId, fechaDocumento, monto, objSession.UserId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult verFormularioLiquidacionEfectivo(string conciliacionId, string pclid, string ctcid)
        {
            var model = new LiquidacionPorcentajeModel();
            var obj = new dto.FormLiquidacion();
            obj = bcp.Tesoreria.ListarFormLiquidacionEfectivo(objSession.CodigoEmpresa, conciliacionId, pclid, ctcid);
            model.NumComprobante = obj.NumComprobante;
            model.RutCliente = obj.RutCliente;
            model.Cliente = obj.Cliente;
            model.RutDeudor = obj.RutDeudor;
            model.Deudor = obj.Deudor;
            model.Monto = obj.Monto.ToString("N2");
            model.Capital = obj.Capital.ToString("N2");
            model.Interes = obj.Interes.ToString("N2");
            model.Honorario = obj.Honorario.ToString("N2");
            model.CapitalPor = obj.CapitalPor.ToString() + "%";
            model.InteresPor = obj.InteresPor.ToString() + "%";
            model.HonorarioPor = obj.HonorarioPor.ToString() + "%";
            model.GastoJud = obj.GastoJud.ToString();
            model.GastoPre = obj.GastoPre.ToString();
            model.pclidLiqui = Int32.Parse(pclid);
            model.ctcidLiqui = Int32.Parse(ctcid);
            model.conciliacionId = Int32.Parse(conciliacionId);
            model.Saldo = obj.MontoRebajado.ToString("N2");
            model.SaldoRebajar = (obj.Monto - obj.MontoRebajado).ToString("N2");
            model.EstadoLiquidacionId = obj.EstadoLiquidacionId;
            return PartialView("_LiquidacionDocumentosEfectivo", model);
        }
        [HttpPost]
        public PartialViewResult verFormularioImputacionEfectivo(LiquidacionPorcentajeModel modl)
        {
            var model = new ImputacionPorcentajeModel();
            var obj = new dto.FormLiquidacion();
            obj = bcp.Tesoreria.ListarFormLiquidacionEfectivo(objSession.CodigoEmpresa, modl.conciliacionId.ToString(), modl.pclidLiqui.ToString(), modl.ctcidLiqui.ToString());
            model.INumComprobante = obj.NumComprobante;
            model.IRutCliente = obj.RutCliente;
            model.ICliente = obj.Cliente;
            model.IRutDeudor = obj.RutDeudor;
            model.IDeudor = obj.Deudor;
            model.IMonto = obj.Monto.ToString("N2");
            model.ICapital = obj.Capital.ToString("N2");
            model.IInteres = obj.Interes.ToString("N2");
            model.IHonorario = obj.Honorario.ToString("N2");
            model.ICapitalPor = obj.CapitalPor.ToString() + "%";
            model.IInteresPor = obj.InteresPor.ToString() + "%";
            model.IHonorarioPor = obj.HonorarioPor.ToString() + "%";
            model.IGastoJud = obj.GastoJud.ToString();
            model.IGastoPre = obj.GastoPre.ToString();
            model.IpclidLiqui = modl.pclidLiqui;
            model.IctcidLiqui = modl.ctcidLiqui;
            model.IConciliacionId = modl.conciliacionId;
            model.ISaldo = obj.MontoRebajado.ToString("N2");
            model.ISaldoRebajar = (obj.Monto - obj.MontoRebajado).ToString("N2");
            model.Documentos = modl.Docs;
            //if (!string.IsNullOrEmpty(modl.Docs))
            //    model.Documentos = string.Join(",", JsonConvert.DeserializeObject<List<string>>(modl.Docs));

            return PartialView("_ImputacionDocumentoEfectivo", model);
        }

        [AcceptVerbs(HttpVerbs.Post), ValidateInput(false)]
        public JsonResult InsertarDocumentoImputadoEfectivo(string docfinalizar, string documentos, int conciliacionId, int pclid, int ctcid)
        {
            int result = -1;

            List<dto.DocumentoPorImputar> lst = JsonConvert.DeserializeObject<List<dto.DocumentoPorImputar>>(documentos);
            List<dto.DocumentoPorFinalizar> lstfinalizar = new List<dto.DocumentoPorFinalizar>();
            if (!string.IsNullOrEmpty(docfinalizar))
            {
                lstfinalizar = JsonConvert.DeserializeObject<List<dto.DocumentoPorFinalizar>>(docfinalizar);
            }
            result = bcp.Tesoreria.InsertarDocumentoImputadoSinMovimiento(objSession.CodigoEmpresa, conciliacionId, pclid, ctcid, lst, lstfinalizar, objSession.UserId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarDocumentoCustodiaProtestadosGrilla(string numCuenta, GridSettings gridSettings)
        {
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;
            List<dto.DocumentoCustodiaProtestado> lst = bcp.Tesoreria.ListarDocumentoCustodiaProtestadosGrilla(objSession.CodigoEmpresa, numCuenta, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString());

            int totalRecords = lst.Count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (

                    from dto.DocumentoCustodiaProtestado item in lst
                    where item.Row >= startRow && item.Row <= endRow
                    select new
                    {

                        id = item.CustodiaId,
                        cell = new object[]
                        {
                           item.CustodiaId,
                           item.FecDoc,
                           item.RutCliente,
                           item.Cliente,
                           item.RutDeudor,
                           item.Deudor,
                           item.Monto,
                           item.Gestor,
                           item.GiradoA,
                           item.TipoBanco,
                           item.NumDocumento,
                           item.Estado,
                           item.FecProrroga,
                           item.Pclid,
                           item.Ctcid,
                           item.GestorId
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarCartolaMovimientosLiberadosGrilla(string numCuenta, string fechaDocumento, string montoDocumento, GridSettings gridSettings)
        {
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;
            List<dto.CartolaMovimiento> lst = bcp.Tesoreria.ListarCartolaMovimientosLiberadosGrilla(objSession.CodigoEmpresa, numCuenta, fechaDocumento, montoDocumento, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString());

            int totalRecords = lst.Count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (

                    from dto.CartolaMovimiento item in lst
                    where item.Row >= startRow && item.Row <= endRow
                    select new
                    {

                        id = item.MovimientoId,
                        cell = new object[]
                        {
                           item.MovimientoId,
                           item.NumCuenta,
                           item.FecMovimiento,
                           item.Monto,
                           item.Motivo,
                           item.Sucursal,
                           item.Movimiento,
                           item.MotivoSistema,
                           item.Estado,
                           item.MotivoSistemaId,
                           item.EstadoId
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult ActualizarMovimientoConciliacionCustodia(int movimientoId, int custodiaId, int conciliacionId)
        {
            int result = -1;
            result = bcp.Tesoreria.ActualizarMovimientoConciliacionCustodia(objSession.CodigoEmpresa, custodiaId, movimientoId, conciliacionId, objSession.UserId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult ObtConciliacionNumComprobante()
        {
            int result = -1;
            result = bcp.Tesoreria.ObtConciliacionNumComprobante();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult ReversarImputacion(int conciliacionId)
        {
            int result = -1;
            result = bcp.Tesoreria.ReversarImputacion(objSession.CodigoEmpresa, conciliacionId, objSession.UserId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ConsultaDePagos(GridSettings gridSettings, string pclid, string ctcid, string fechaCancelacion, string numComprobante)
        {
            var fechaCancela = new DateTime();
            DateTime.TryParse(fechaCancelacion, out fechaCancela);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;
            List<dto.Pago> lst = bcp.Tesoreria.ConsultaDePagos(objSession.CodigoEmpresa, fechaCancela,pclid,ctcid, numComprobante, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString());

            int totalRecords = lst.Count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (

                    from dto.Pago item in lst
                    where item.Row >= startRow && item.Row <= endRow
                    select new
                    {

                        id = item.ConciliacionId,
                        cell = new object[]
                        {
                           item.ConciliacionId,
                           item.MovimientoId,
                           item.CustodiaId,
                           item.Pclid,
                           item.Ctcid,
                           item.GestorId,
                           item.RutCliente,
                           item.Cliente,
                           item.TipoConciliacion,
                           item.NumComprobante,
                           item.Moneda,
                           item.TipoCambio,
                           item.RutDeudor,
                           item.Deudor,
                           item.TipoDocumento,
                           item.Numero,
                           item.FechaAsignado,
                           item.Asignado,
                           item.Capital,
                           item.Interes,
                           item.Honorario,
                           item.GastoPre,
                           item.GastoJud,
                           item.RutAsegurado,
                           item.Asegurado,
                           item.Gestor,
                           item.FecCancela
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ExportToExcelPagos(string pclid, string ctcid, string fechaCancelacion, string numComprobante)
        {

            var grid = new GridView();
            var fechaCancela = new DateTime();
            DateTime.TryParse(fechaCancelacion, out fechaCancela);
                       
            List<dto.Pago> lst = bcp.Tesoreria.ConsultaDePagos(objSession.CodigoEmpresa, fechaCancela, pclid, ctcid, numComprobante, "", "ConciliacionId", "");
            grid.DataSource = lst;
            grid.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=Pagos.xls");
            Response.ContentType = "application/ms-excel";

            Response.Charset = "";
            StringWriter sw = new StringWriter();
            System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);

            grid.RenderControl(htw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return View("MyView");
        }
        #region "Reportes"
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult ComprobanteImputacion(int conciliacionId)
        {
            var report = new LocalReport
            {
                ReportPath = Server.MapPath(@"~\Reportes\ComprobanteImputacion.rdlc"),
            };
            ReportParameterCollection reportParameters = new ReportParameterCollection();
            var objCabecera = new dto.ReporteCabecera();
            objCabecera = bcp.Reporte.obtenerCabecera(objSession.CodigoEmpresa, conciliacionId);

            reportParameters.Add(new ReportParameter("NumComprobante", objCabecera.NumComprobante));
            reportParameters.Add(new ReportParameter("RutDeudor", bcp.Reporte.getRutFormato(objCabecera.RutDeudor)));
            reportParameters.Add(new ReportParameter("RutCliente", bcp.Reporte.getRutFormato(objCabecera.RutCliente)));
            reportParameters.Add(new ReportParameter("Gestor", objCabecera.Gestor));
            reportParameters.Add(new ReportParameter("Cliente", objCabecera.Cliente));
            reportParameters.Add(new ReportParameter("Deudor", objCabecera.Deudor));
            reportParameters.Add(new ReportParameter("Monto", objCabecera.Monto.ToString()));
            reportParameters.Add(new ReportParameter("Fecha", objCabecera.FecDoc.ToString()));
            report.SetParameters(reportParameters);

            List<dto.ReporteImputacion> lst = bcp.Reporte.ListarReporteImputacion(objSession.CodigoEmpresa, conciliacionId);
            ReportDataSource rds = new ReportDataSource("dsImputado", lst);
            report.DataSources.Clear();
            report.DataSources.Add(rds);
            byte[] pdf = report.Render("PDF");
            string strPdf = Convert.ToBase64String(pdf);
            var returnOjb = new { success = true, pdf = strPdf };
            return Json(returnOjb, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult ComprobanteDetalleImputacion(int conciliacionId)
        {
            var report = new LocalReport
            {
                ReportPath = Server.MapPath(@"~\Reportes\ComprobanteImputacionDetail.rdlc"),
            };
            ReportParameterCollection reportParameters = new ReportParameterCollection();
            var objCabecera = new dto.ReporteCabecera();
            objCabecera = bcp.Reporte.obtenerCabecera(objSession.CodigoEmpresa, conciliacionId);

            reportParameters.Add(new ReportParameter("NumComprobante", objCabecera.NumComprobante));
            reportParameters.Add(new ReportParameter("RutDeudor", bcp.Reporte.getRutFormato(objCabecera.RutDeudor)));
            reportParameters.Add(new ReportParameter("RutCliente", bcp.Reporte.getRutFormato(objCabecera.RutCliente)));
            reportParameters.Add(new ReportParameter("Gestor", objCabecera.Gestor));
            reportParameters.Add(new ReportParameter("Cliente", objCabecera.Cliente));
            reportParameters.Add(new ReportParameter("Deudor", objCabecera.Deudor));
            reportParameters.Add(new ReportParameter("Monto", objCabecera.Monto.ToString()));
            reportParameters.Add(new ReportParameter("Fecha", objCabecera.FecDoc.ToString()));
            report.SetParameters(reportParameters);

            List<dto.ReporteImputacionDetail> lst = bcp.Reporte.ListarReporteImputacionDetail(objSession.CodigoEmpresa, conciliacionId);
            ReportDataSource rds = new ReportDataSource("DataSetDetail", lst);
            report.DataSources.Clear();
            report.DataSources.Add(rds);
            byte[] pdf = report.Render("PDF");
            string strPdf = Convert.ToBase64String(pdf);
            var returnOjb = new { success = true, pdf = strPdf };
            return Json(returnOjb, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
