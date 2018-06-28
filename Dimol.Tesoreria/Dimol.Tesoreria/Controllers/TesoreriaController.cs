using Mvc.HtmlHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dimol.Carteras.bcp;
using Dimol.Tesoreria.Models;
using Dimol.Tesoreria.bcp;
using System.Transactions;

namespace Dimol.Tesoreria.Controllers
{
    public class TesoreriaController : Dimol.Controllers.BaseController
    {
        //
        // GET: /Tesoreria/
        #region "Views"

        public ActionResult BuscarCaja()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            ViewBag.Tipo = new SelectList(bcp.Caja.ListarTipo(objSession.Idioma,"Seleccione"), "Value", "Text", "");
            ViewBag.Empleado = new SelectList(bcp.Caja.ListarEmpleados(objSession.CodigoEmpresa, "Seleccione"), "Value", "Text", "");
            ViewBag.TipoDocumento = new SelectList(bcp.Caja.ListarTipoCpbt(objSession.CodigoEmpresa,"D",objSession.PrfId, objSession.Idioma,"N","", "Seleccione"), "Value", "Text", "");
            return View();
        }

        public ActionResult AnularPagos()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            ViewBag.Tipo = new SelectList(bcp.Caja.ListarTipo(objSession.Idioma, "Seleccione"), "Value", "Text", "");
            return View();
        }

        public ActionResult ModificarPagos()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            ViewBag.Tipo = new SelectList(bcp.Caja.ListarTipo(objSession.Idioma, "Seleccione"), "Value", "Text", "");
            ViewBag.Gestores = Dimol.Carteras.bcp.Gestor.ListarGestores(objSession.CodigoEmpresa, objSession.CodigoSucursal);
            return View();
        }

        public ActionResult Caja()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            Comprobante objComprobante = new Comprobante();
            ViewBag.Tipo = new SelectList(bcp.Caja.ListarTipo(objSession.Idioma, "Seleccione"), "Value", "Text", "");
            ViewBag.Negociacion = new SelectList(bcp.Caja.ListarEmpleados(objSession.CodigoEmpresa, "Seleccione"), "Value", "Text", "");
            ViewBag.TipoDocumento = new SelectList(bcp.Caja.ListarTipoCpbt(objSession.CodigoEmpresa, "D", objSession.PrfId, objSession.Idioma, "N", "", "Seleccione"), "Value", "Text", "");
            ViewBag.Moneda = new SelectList(objComprobante.ListarMonedas(objSession.CodigoEmpresa), "Value", "Text","");
            ViewBag.NegociacionNeg = new SelectList(bcp.Caja.ListarEmpleados(objSession.CodigoEmpresa, "Seleccione"), "Value", "Text", "");
            return View();
        }

        #endregion

        #region "Shared Actions"

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

        public JsonResult ListarTipoDocumento(string  tipo)
        {
            return Json(new SelectList(bcp.Caja.ListarTipoCpbt(objSession.CodigoEmpresa,"D",objSession.PrfId, objSession.Idioma,"N",tipo, "Seleccione"), "Value", "Text", ""));
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

        public JsonResult ListarNegociacion(int ctcid)
        {
            return Json(new SelectList(bcp.Caja.ListarNegociacionesCaja(objSession.CodigoEmpresa, ctcid, "Seleccione"), "Value", "Text", ""));
        }

        #endregion

        #region"Caja"

        #endregion

        #region "Buscar Caja"

        public ActionResult GetDocumentosCaja(GridSettings gridSettings, BuscarCajaModel model)
        {
            // create json data 
            int totalRecords = bcp.Caja.ListarDocumentosCajaCount(objSession.CodigoEmpresa, objSession.Idioma, model.Pclid, model.Ctcid, model.Tipo, model.TipoDocumento, objSession.EmplId.ToString(), model.Numero, model.MontoDesde, model.MontoHasta, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);

            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.BuscarDocumentoCaja> lst = bcp.Caja.ListarDocumentosCaja(objSession.CodigoEmpresa, objSession.Idioma, model.Pclid, model.Ctcid, model.Tipo, model.TipoDocumento, objSession.EmplId.ToString(), model.Numero, model.MontoDesde, model.MontoHasta, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.BuscarDocumentoCaja item in lst
                    select new
                    {
                        id = item.Anio + "|" ,
                        cell = new object[]
                        {
                            item.TipoMovimiento,
                            item.TipoDocumento,
                            item.Monto,
                            item.NumeroCuenta,
                            item.NombreCliente,
                            item.NombreDeudor ,
                            item.Empleado,
                            item.Estado
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region "Anular Pagos"

        public ActionResult GetAnularPagos(GridSettings gridSettings, BuscarCajaModel model)
        {
            // create json data 
            int totalRecords = bcp.Caja.ListarAnularPagoCount(objSession.CodigoEmpresa, objSession.CodigoSucursal, model.Pclid, model.Ctcid, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);

            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.BuscarAnularPago> lst = bcp.Caja.ListarAnularPago(objSession.CodigoEmpresa, objSession.CodigoSucursal, model.Pclid, model.Ctcid, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.BuscarAnularPago item in lst
                    select new
                    {
                        id = item.Anio + "|",
                        cell = new object[]
                        {
                            item.NombreCliente,
                            item.NombreDeudor,
                            item.TipoDocumento,
                            item.NumeroCuenta,
                            item.NumeroDocumento,
                            item.Fecha ,
                            item.Capital,
                            item.Interes ,
                            item.Honorario,
                            item.GastoPrejudicial,
                            item.GastoJudicial,
                            item.Total
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public void AnularPago()
        {
            using (TransactionScope scope = new TransactionScope())
            {

            }
        }

        #endregion

        #region "Modificar Pagos"

        public ActionResult GetModificarPagos(GridSettings gridSettings, BuscarCajaModel model)
        {
            // create json data 
            int totalRecords = bcp.Caja.ListarAnularPagoCount(objSession.CodigoEmpresa, objSession.CodigoSucursal, model.Pclid, model.Ctcid, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);

            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.BuscarAnularPago> lst = bcp.Caja.ListarAnularPago(objSession.CodigoEmpresa, objSession.CodigoSucursal, model.Pclid, model.Ctcid, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.BuscarAnularPago item in lst
                    select new
                    {
                        id = item.Anio + "|",
                        cell = new object[]
                        {
                            item.NombreCliente,
                            item.NombreDeudor,
                            item.TipoDocumento,
                            item.NumeroCuenta,
                            item.NumeroDocumento,
                            item.Fecha ,
                            item.Capital,
                            item.Interes ,
                            item.Honorario,
                            item.GastoPrejudicial,
                            item.GastoJudicial,
                            item.Total,
                            item.Gestor
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}
