using Dimol.Caja.Models;
using Dimol.Carteras.bcp;
using Mvc.HtmlHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dimol.Caja.Controllers
{
    public class MantenedoresController : Dimol.Controllers.BaseController
    {
        //
        // GET: /Mantenedores/

        public ActionResult CriteriosRemesa()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            Comprobante objComprobante = new Comprobante();
            ViewBag.TipoConciliacion = new SelectList(bcp.Tesoreria.ListarTipoConciliacionMovimiento(), "Value", "Text", "");
            ViewBag.CodigoCarga = new SelectList(objComprobante.ListarCodigoCarga(objSession.CodigoEmpresa, 0, "Seleccione"), "Value", "Text", "");
            ViewBag.TipoRegion = new SelectList(bcp.Tesoreria.ListarTipoRegionRemesa(), "Value", "Text", "");
            ViewBag.TipoCambioCapital = new SelectList(bcp.Tesoreria.ListarCajaTipoCambioRemesa(), "Value", "Text", "");
            ViewBag.CondicionAnticipo = new SelectList(bcp.Tesoreria.ListarTipoCondicionFacturacionRemesa(), "Value", "Text", "");
            return View();
        }
        public ActionResult CriteriosDeImputacion()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        public ActionResult CriteriosDeFacturacion()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            ViewBag.CriterioAplicaSimbolo = new SelectList(bcp.Tesoreria.ListarCajaTipoCambioTodos(), "Value", "Text", "");
            ViewBag.Condicion = new SelectList(bcp.Tesoreria.ListarTipoCondicionFacturacion(), "Value", "Text", "");
            return View();
        }
        public JsonResult ListarCriteriosRemesaCliente(GridSettings gridSettings, int pclid)
        {
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;
            List<dto.CriterioRemesa> lst = bcp.CriterioRemesa.ListarCriterioRemesaClienteGrilla(objSession.CodigoEmpresa, pclid, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString());

            int totalRecords = lst.Count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (

                    from dto.CriterioRemesa item in lst
                    where item.Row >= startRow && item.Row <= endRow
                    select new
                    {

                        id = item.Id,
                        cell = new object[]
                        {
                           item.Id,
                           item.SimboloId,
                           item.CodigoCarga,
                           item.TipoConciliacionId,
                           item.ConcicionId,
                           item.DesdeDiasVencido,
                           item.HastaDiasVencido,
                           item.RegionMetropolitana,
                           item.CodigoDeCarga,
                           item.TipoCambioCapital,
                           item.Capital,
                           item.Interes,
                           item.Honorario,
                           item.TipoConciliacion,
                           item.CondicionAnticipo,
                           item.IsAnticipo
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarCriteriosImputacion(GridSettings gridSettings)
        {
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;
            List<dto.CriterioImputacion> lst = bcp.CriterioImputacion.ListarCriteriosImputacionGrilla(objSession.CodigoEmpresa, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString());

            int totalRecords = lst.Count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (

                    from dto.CriterioImputacion item in lst
                    where item.Row >= startRow && item.Row <= endRow
                    select new
                    {

                        id = item.Pclid,
                        cell = new object[]
                        {
                           item.Pclid,
                           item.Cliente,
                           item.Capital,
                           item.Interes,
                           item.Honorario
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarCriteriosFacturacion(GridSettings gridSettings)
        {
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;
            List<dto.CriterioFacturacion> lst = bcp.CriterioFacturacion.ListarCriteriosFacturacionGrilla(objSession.CodigoEmpresa, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString());

            int totalRecords = lst.Count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (

                    from dto.CriterioFacturacion item in lst
                    where item.Row >= startRow && item.Row <= endRow
                    select new
                    {

                        id = item.Pclid,
                        cell = new object[]
                        {
                           item.Pclid,
                           item.CondicionId,
                           item.Cliente,
                           item.Descripcion,
                           item.NoAplicaFactura,
                           item.Imputable,
                           item.Condicion,
                           item.AplicaRemesa
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult InsertUpdateCriterioRemesa(CriterioRemesaViewModel modl)
        {
            
            int result = -1;
            result = bcp.CriterioRemesa.InsertUpdateCriterioRemesa(modl.Id, objSession.CodigoEmpresa,int.Parse(modl.Pclid), modl.Desde, modl.Hasta, modl.TipoRegion,
                                                                    modl.CodigoCarga, modl.Capital, modl.Interes, modl.Honorario, modl.TipoCambioCapital, int.Parse(modl.TipoCambioCapitalId),
                                                                    int.Parse(modl.TipoConciliacion), modl.CondicionAnticipo, objSession.UserId);
            return Json(result, JsonRequestBehavior.AllowGet);
           
        }

        public ActionResult EmpresaIsAnticipo(int pclid)
        {
            return Json(bcp.CriterioRemesa.EmpresaFactura(objSession.CodigoEmpresa, pclid), JsonRequestBehavior.AllowGet);
        }
    }
}
