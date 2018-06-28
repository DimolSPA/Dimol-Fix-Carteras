using Mvc.HtmlHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using Dimol.dto;
using Dimol.Contabilidad.Mantenedores.bcp;
using System.Globalization;
using System.Threading;
using Dimol.Contabilidad.Mantenedores.Models;
using System.Diagnostics;


namespace Dimol.Contabilidad.Mantenedores.Controllers
{
    public class ContabilidadController : Dimol.Controllers.BaseController
    {

        public ActionResult ClasificacionDocumentos()
        {
            //ActivarSession();
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            GridModel model = new GridModel();
            model.GridSelect = "ClasificacionDocumentos";
            ViewBag.Add = true;
            ViewBag.Del = true;
            ViewBag.Edit = true;
            ViewBag.TipoComprobante = bcp.ClasificacionDocumentos.ListarTipoComprobante(objSession.CodigoEmpresa, objSession.Idioma);
            ViewBag.TipoProducto = bcp.ClasificacionDocumentos.ListarTipoProducto(objSession.CodigoEmpresa, objSession.Idioma);
            ViewBag.Concepto = bcp.ClasificacionDocumentos.ListarConcepto(objSession.CodigoEmpresa, objSession.Idioma);
            ViewBag.Movimiento = bcp.ClasificacionDocumentos.ListarMovimiento(objSession.CodigoEmpresa, objSession.Idioma);
            ViewBag.Cuenta = bcp.ClasificacionDocumentos.ListarCuentas(objSession.CodigoEmpresa);
            ViewBag.Stock = bcp.ClasificacionDocumentos.ListarStock();
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GetTalonario(string id)
        {
            
            Debug.WriteLine("ID TALONARIO " + id);
            ViewBag.idTalonario = id;
            dto.Talonario tal = bcp.Talonario.getTalonarioPorId(objSession.CodigoEmpresa, Int32.Parse(id));

            Debug.WriteLine("NUM TALONARIO " + tal.tac_numero);
            Debug.WriteLine("NOM TALONARIO " + tal.tac_nombre);

            return Json(tal);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetSinAsignar(GridSettings gridSettings, string id)
        {

            Debug.WriteLine("ID TALONARIO SIN ASIG" + id);

            List<dto.Talonario> lstAccion = bcp.Talonario.ListarTalonariosSinAsignar(objSession.CodigoEmpresa, Int32.Parse(id), objSession.CodigoSucursal, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString());


            var jsonData = new
            {
                //total = totalPages,
                page = gridSettings.pageIndex,
                //records = totalRecords,

                rows =
                (
                    from dto.Talonario item in lstAccion
                    select new
                    {
                        id = item.tac_tacid,
                        cell = new object[]
                        {
                            item.Codemp,
                            item.tac_tacid,
                            item.tac_nombre,
                            item.tpc_talonario
                            
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetAsignados(GridSettings gridSettings, string id)
        {

            Debug.WriteLine("ID TALONARIO ASIGNADA" + id);

            List<dto.Talonario> lstAccion = bcp.Talonario.ListarTalonariosAsignados(objSession.CodigoEmpresa, Int32.Parse(id), objSession.CodigoSucursal, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString());


            var jsonData = new
            {
                //total = totalPages,
                page = gridSettings.pageIndex,
                //records = totalRecords,

                rows =
                (
                    from dto.Talonario item in lstAccion
                    select new
                    {
                        id = item.tac_tacid,
                        cell = new object[]
                        {
                            item.Codemp,
                            item.tac_tacid,
                            item.tac_nombre,
                            item.tpc_talonario
                            
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GuardarTalonario(string id, string nombre, string num)
        {
            int val = 0;
            //Debug.WriteLine("ID TALONARIO  A GUARDAR" + id);
            dto.Talonario tal = new dto.Talonario();
            if (id != null)
            {
                //Debug.WriteLine("TENGO ID");
                tal.tac_tacid = Int32.Parse(id);
            }
            else
            {
                //Debug.WriteLine("ENTRO A ID VACIO");
                tal.tac_tacid = 0;
            }
            
            tal.tac_nombre = nombre;
            tal.tac_numero = Int32.Parse(num);

            val = bcp.Talonario.Insertar(tal, objSession.CodigoEmpresa, objSession.CodigoSucursal);

            //Debug.WriteLine("NUM TALONARIO " + tal.tac_numero);
            //Debug.WriteLine("NOM TALONARIO " + tal.tac_nombre);

            return Json(val);
        }

        public ActionResult Talonario()
        {
            int id = 0;
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            //ViewBag.TiposInsumo = bcp.Insumo.ListarTiposInsumos(objSession.CodigoEmpresa, objSession.Idioma);
            //ViewBag.TiposInsumo = new SelectList(bcp.Insumo.ListarTiposInsumos(objSession.CodigoEmpresa));

            ViewBag.Talonarios = new SelectList(bcp.Talonario.ListarTalonarios(objSession.CodigoEmpresa, "Seleccione"), "Value", "Text", "");
            ViewBag.idTalonario = id;
            return View();
        }

        public ActionResult TiposDocumentos()
        {
            //ActivarSession();
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            GridModel model = new GridModel();
            model.GridSelect = "TiposDocumentos";
            ViewBag.Add = true;
            ViewBag.Del = true;
            ViewBag.Edit = true;
            ViewBag.TipoComprobante = bcp.TiposDocumentos.ListarTipoComprobante(objSession.CodigoEmpresa);
            ViewBag.TipoDocDigital = bcp.TiposDocumentos.ListarTipoDocCaja(objSession.CodigoEmpresa, objSession.Idioma);
            return View(model);
        }

        public ActionResult FormasDePago()
        {
            //ActivarSession();
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            GridModel model = new GridModel();
            model.GridSelect = "FormasDePago";
            ViewBag.Add = true;
            ViewBag.Del = true;
            ViewBag.Edit = true;
            /*
            List<string> lista = bcp.FormasDePago.ListarTiposDocCaja(objSession.CodigoEmpresa);
            for (int i = 0; i < lista.Count; i++)
            {
                Debug.WriteLine("LISTA TIPOS " + lista[i]);
                ViewBag.Tipo = lista[i];
            }
            /*ViewBag.Tipo = new List<string>()
            {
                "USA",
                "INDIA",
                "UK"
            };*/
            //Debug.WriteLine("NUEVA LISTA TIPOS " + bcp.FormasDePago.ListarTiposDocCaja2(objSession.CodigoEmpresa));
            
            ViewBag.Tipo = bcp.FormasDePago.ListarTiposDocCaja2(objSession.CodigoEmpresa);
             
            return View(model);
        }

        public ActionResult TiposReporte()
        {
            //ActivarSession();
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            GridModel model = new GridModel();
            model.GridSelect = "TiposReporte";
            ViewBag.Add = true;
            ViewBag.Del = true;
            ViewBag.Edit = true;
            /*
            List<dto.TiposComprobante> lista = bcp.TiposReporte.ListarTiposComprobante(objSession.CodigoEmpresa, objSession.Idioma);
            for (int i = 0; i < lista.Count; i++)
            {
                Debug.WriteLine("valor " + lista[i].Nombre);
                //ViewBag.nombre = lista[i].Nombre;
            }
            ViewBag.Agrupa = lista[1].Nombre;
             */
            ViewBag.Agrupa = bcp.TiposReporte.ListarTiposComprobante2(objSession.CodigoEmpresa, objSession.Idioma);
            ViewBag.ReportePadre = bcp.TiposReporte.ListarReportePadre(objSession.CodigoEmpresa, objSession.Idioma);
            return View(model);
        }

        public ActionResult TiposTraslados()
        {
            //ActivarSession();
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            GridModel model = new GridModel();
            model.GridSelect = "TiposTraslados";
            ViewBag.Add = true;
            ViewBag.Del = true;
            ViewBag.Edit = true;
            ///ViewBag.Agrupa = bcp.Accion.ListarGrupoAcciones(objSession.Idioma);
            return View(model);
        }

        public ActionResult TiposCausaNotas()
        {
            //ActivarSession();
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            GridModel model = new GridModel();
            model.GridSelect = "TiposCausaNotas";
            ViewBag.Add = true;
            ViewBag.Del = true;
            ViewBag.Edit = true;
            ///ViewBag.Agrupa = bcp.Accion.ListarGrupoAcciones(objSession.Idioma);
            return View(model);
        }

        public ActionResult TiposCausaGuias()
        {
            //ActivarSession();
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            GridModel model = new GridModel();
            model.GridSelect = "TiposCausaGuias";
            ViewBag.Add = true;
            ViewBag.Del = true;
            ViewBag.Edit = true;
            ///ViewBag.Agrupa = bcp.Accion.ListarGrupoAcciones(objSession.Idioma);
            return View(model);
        }

        public ActionResult MonedasValores()
        {
            //ActivarSession();
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            GridModel model = new GridModel();
            model.GridSelect = "MonedasValores";
            ViewBag.Add = true;
            ViewBag.Del = true;
            ViewBag.Edit = true;
            ViewBag.Nombre = bcp.MonedaValor.ListarMonedas(objSession.CodigoEmpresa);
            return View(model);
        }

        public ActionResult Impuesto()
        {
            //ActivarSession();
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            GridModel model = new GridModel();
            model.GridSelect = "Impuesto";
            ViewBag.Add = true;
            ViewBag.Del = true;
            ViewBag.Edit = true;
            /*
            List<string> salida = bcp.Impuesto.ListarCuentasContables(objSession.Idioma);
            for (int i = 0; i < salida.Count; i++)
            {
                Debug.WriteLine("valor " + salida[i]);
                ViewBag.nombrePlanDeCuentas = salida[i];
            }
            
             */
            ViewBag.nombrePlanDeCuentas = bcp.Impuesto.ListarCuentasContables(objSession.CodigoEmpresa);
            return View(model);
        }

        public ActionResult PeriodosContables()
        {
            //ActivarSession();
            Debug.WriteLine("ACTION RESULT ");
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            GridModel model = new GridModel();
            model.GridSelect = "PeriodosContables";
            ViewBag.Add = true;
            ViewBag.Del = true;
            ViewBag.Edit = true;
            
            ///ViewBag.Agrupa = bcp.Accion.ListarGrupoAcciones(objSession.Idioma);
            return View(model);
        }

        public ActionResult PeriodosContablesMensuales()
        {
            //ActivarSession();
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            GridModel model = new GridModel();
            model.GridSelect = "PeriodosContablesMensuales";
            //ViewBag.Add = true;
            ViewBag.Del = true;
            ViewBag.Edit = true;
            ///ViewBag.Agrupa = bcp.Accion.ListarGrupoAcciones(objSession.Idioma);
            return View(model);
        }

        public ActionResult MotivoCobranza()
        {
            GridModel model = new GridModel();
            model.GridSelect = "MotivoCobrnaza";
            ViewBag.Add = true;
            ViewBag.Del = true;
            ViewBag.Edit = true;
            return View(model);
        }

        public ActionResult CodigoCarga()
        {
            //ActivarSession();
            GridModel model = new GridModel();
            model.GridSelect = "CodigoCarga";
            ViewBag.Add = true;
            ViewBag.Del = true;
            ViewBag.Edit = true;
          //// string ddd = bcp.CodigoCarga.ListarClientesCodigoCarga(objSession.CodigoEmpresa);
          //  ViewBag.Clientes = ddd;
            return View(model);
        }

        public ActionResult MotivoCastigo()
        {
           // ActivarSession();
            GridModel model = new GridModel();
            model.GridSelect = "Acciones";
            ViewBag.Add = true;
            ViewBag.Del = true;
            ViewBag.Edit = true;
            ViewBag.Agrupa = bcp.Accion.ListarGrupoAcciones(objSession.Idioma);
            return View(model);
        }

        //[AcceptVerbs(HttpVerbs.Post)]
        //[ValidateAntiForgeryToken()]
        //public ActionResult Index(GridModel model)
        //{
        //    try
        //    {
        //        JavaScriptSerializer serializer = new JavaScriptSerializer();
        //        List<MotivoCobranzaModel> lstMotivo = serializer.Deserialize<List<MotivoCobranzaModel>>(model.GridData);

        //        // Process returned data here
        //    }
        //    catch
        //    {
        //    }
        //    return RedirectToAction("Index");
        //}
        #region "Motivo Cobranza"
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetMotivoCobranza(GridSettings gridSettings)
        {
            // create json data
            int totalRecords = 1000;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<MotivoCobranzaModel> list = new List<MotivoCobranzaModel>();
            for (int iX = startRow; iX < endRow; iX++)
            {
                MotivoCobranzaModel motivo = new MotivoCobranzaModel();
                motivo.Id = iX;
                motivo.Nombre = "Line# " + iX.ToString();
                list.Add(motivo);
            }

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from MotivoCobranzaModel item in list
                    select new
                    {
                        id = item.Id,
                        cell = new object[]
                        {
                            item.Id,
                            item.Nombre
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        //[AcceptVerbs(HttpVerbs.Post)]
        //public JsonResult OperMotivoCobranza(bcp.MotivoCobranza model, string oper, int? id)
        //{
        //    model.OperMotivoCobranza(oper, id, objSession);
        //    return Json("OK");
        //}

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult EditMotivoCobranza(FormCollection form)
        {
            string lll = form.ToString();
            return Json(lll);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult DelMotivoCobranza(FormCollection form)
        {
            string lll = form.ToString();
            return Json(lll);
        }
        #endregion

        #region "PeriodosContables"
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetPeriodosContables(GridSettings gridSettings)
        {
            // create json data 
            bcp.PeriodoContable bcpAccion = new PeriodoContable();

            int totalRecords = bcpAccion.ListarPeriodoContableCount(objSession.CodigoEmpresa, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);
            Debug.WriteLine("TOTAL RECORDS " + totalRecords);
            //Aca se llena la lista que pobla la grilla de datos
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.PeriodoContable> lstAccion = bcpAccion.ListarPeriodosGrilla(1, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), startRow, endRow);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.PeriodoContable item in lstAccion
                    select new
                    {
                        id = item.IdPeriodo,
                        cell = new object[]
                        {
                            item.Codemp,
                            item.Ano,
                            item.Habilitado,
                            item.Finalizado,
                            item.IdPeriodo
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult OperPeriodoContable(bcp.PeriodoContable model, string oper, int? id)
        {
            Debug.WriteLine("AÑO " + model.Ano + "EMPRESA" + objSession.CodigoEmpresa + "HAB" + model.Habilitado + "ID" + id );
            if (model.Finalizado == true)
            {
                
            }
            model.OperPeriodoContable(oper, id, objSession);
            return Json("OK");
        }
        #endregion


        #region "PeriodosContablesMensuales"
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetPeriodosContablesMensuales(GridSettings gridSettings)
        {
            // create json data 
            bcp.PeriodoContableMes bcpAccion = new PeriodoContableMes();

            int totalRecords = bcpAccion.ListarPeriodoContableMesCount(objSession.CodigoEmpresa, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);
            Debug.WriteLine("TOTAL RECORDS " + totalRecords);
            //Aca se llena la lista que pobla la grilla de datos
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.PeriodoContableMes> lstAccion = bcpAccion.ListarPeriodosGrilla(1, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), startRow, endRow);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.PeriodoContableMes item in lstAccion
                    select new
                    {
                        id = item.IdPeriodoMensual,
                        cell = new object[]
                        {
                            item.Codemp,
                            item.Ano,
                            item.Mes,
                            item.Inicio,
                            item.Fin,
                            item.Habilitado,
                            item.Finalizado,
                            item.IdPeriodoMensual
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult OperPeriodoContableMes(bcp.PeriodoContableMes model, string oper, int? id)
        {
            Debug.WriteLine("OPERACION SUMA ");
            model.OperAccion(oper, id, objSession);
            return Json("OK");
        }
        #endregion

        #region "Impuesto"
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetImpuestos(GridSettings gridSettings)
        {
            // create json data 
            bcp.Impuesto bcpAccion = new Impuesto();
            int totalRecords = bcpAccion.ListarCount(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);
            //Console.WriteLine("TOTAL RECORDS " + totalRecords);
            Debug.WriteLine("TOTAL RECORDS " + totalRecords);
            //Aca se llena la lista que pobla la grilla de datos
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.Impuesto> lstAccion = bcpAccion.ListarGrilla(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), startRow, endRow);

            
            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.Impuesto item in lstAccion
                    select new
                    {
                        id = item.Id,
                        cell = new object[]
                        {
                            item.Codemp,
                            item.Nombre,
                            item.NombreCorto,
                            item.idPlanDeCuentas,
                            item.NombreCC,
                            item.Retenido,
                            item.Monto,
                            item.Id
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult OperImpuesto(bcp.Impuesto model, string oper, int? id)
        {
            Debug.WriteLine("OPERACION SUMA ");
            model.OperAccion(oper, id, objSession, model.idPlanDeCuentas);
            return Json("OK");
        }
        #endregion

        #region "MonedasValores"
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetMonedasValores(GridSettings gridSettings, bcp.MonedaValor model)
        {
            // create json data 
            bcp.MonedaValor bcpAccion = new MonedaValor();

            int totalRecords = bcpAccion.ListarMonedasValoresCount(objSession.CodigoEmpresa, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);
            Debug.WriteLine("TOTAL RECORDS " + totalRecords);
            //Aca se llena la lista que pobla la grilla de datos
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.MonedaValor> lstAccion = bcpAccion.ListarGrilla(1, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), startRow, endRow);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.MonedaValor item in lstAccion
                    select new
                    {
                        id = item.Id,
                        cell = new object[]
                        {
                            item.Codemp,
                            item.Nombre,
                            item.Fecha,
                            item.Valor,
                            item.codMoneda,
                            item.Id
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult OperMonedaValor(bcp.MonedaValor model, string oper, int? id)
        {
            Debug.WriteLine("OPERACION SUMA ");
            model.OperAccion(oper, id, objSession);
            return Json("OK");
        }
        #endregion

        #region "TiposCausaGuias"
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetTiposCausaGuias(GridSettings gridSettings, bcp.TiposCausaGuias model)
        {
            // create json data 
            bcp.TiposCausaGuias bcpAccion = new TiposCausaGuias();

            int totalRecords = bcpAccion.ListarTiposCausaGuiasCount(objSession.CodigoEmpresa, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);
            Debug.WriteLine("TOTAL RECORDS " + totalRecords);
            //Aca se llena la lista que pobla la grilla de datos
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.TiposCausaGuias> lstAccion = bcpAccion.ListarGrilla(1, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), startRow, endRow);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.TiposCausaGuias item in lstAccion
                    select new
                    {
                        id = item.Id,
                        cell = new object[]
                        {
                            item.Codemp,
                            item.Codigo,
                            item.Nombre,
                            item.Id
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult OperTiposCausaGuias(bcp.TiposCausaGuias model, string oper, int? id)
        {
            Debug.WriteLine("OPERACION SUMA ");
            model.OperAccion(oper, id, objSession);
            return Json("OK");
        }
        #endregion

        #region "TiposCausaNotas"
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetTiposCausaNotas(GridSettings gridSettings, bcp.TiposCausaGuias model)
        {
            // create json data 
            bcp.TiposCausaNotas bcpAccion = new TiposCausaNotas();

            int totalRecords = bcpAccion.ListarTiposCausaNotasCount(objSession.CodigoEmpresa, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);
            Debug.WriteLine("TOTAL RECORDS " + totalRecords);
            //Aca se llena la lista que pobla la grilla de datos
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.TiposCausaNotas> lstAccion = bcpAccion.ListarGrilla(1, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), startRow, endRow);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.TiposCausaNotas item in lstAccion
                    select new
                    {
                        id = item.Id,
                        cell = new object[]
                        {
                            item.Codemp,
                            item.Codigo,
                            item.Nombre,
                            item.Id
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult OperTiposCausaNotas(bcp.TiposCausaNotas model, string oper, int? id)
        {
            Debug.WriteLine("OPERACION SUMA ");
            model.OperAccion(oper, id, objSession);
            return Json("OK");
        }
        #endregion

        #region "TiposTraslados"
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetTiposTraslados(GridSettings gridSettings, bcp.TiposTraslado model)
        {
            // create json data 
            bcp.TiposTraslado bcpAccion = new TiposTraslado();

            int totalRecords = bcpAccion.ListarTiposTrasladoCount(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);
            Debug.WriteLine("TOTAL RECORDS " + totalRecords);
            //Aca se llena la lista que pobla la grilla de datos
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.TiposTraslado> lstAccion = bcpAccion.ListarGrilla(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), startRow, endRow);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.TiposTraslado item in lstAccion
                    select new
                    {
                        id = item.Id,
                        cell = new object[]
                        {
                            item.Codemp,
                            item.Codigo,
                            item.Nombre,
                            item.Id
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult OperTiposTraslados(bcp.TiposTraslado model, string oper, int? id)
        {
            Debug.WriteLine("OPERACION SUMA ");
            model.OperAccion(oper, id, objSession);
            return Json("OK");
        }
        #endregion

        #region "TiposReporte"
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetTiposReporte(GridSettings gridSettings, bcp.TiposReporte model)
        {
            // create json data 
            bcp.TiposReporte bcpAccion = new TiposReporte();

            int totalRecords = bcpAccion.ListarTiposReporteCount(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);
            //Debug.Write("TOTAL RECORDS " + totalRecords);
            //Aca se llena la lista que pobla la grilla de datos
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.TiposReporte> lstAccion = bcpAccion.ListarGrilla(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), startRow, endRow);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.TiposReporte item in lstAccion
                    select new
                    {
                        id = item.IdTiposReporte,
                        cell = new object[]
                        {
                            item.Agrupa,
                            item.Codemp,
                            item.Idioma,
                            item.Codigo,
                            item.Tipo,                            
                            item.Nombre,
                            item.Reporte,
                            item.ReportePadre,
                            item.Id,
                            item.IdTiposReporte
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult OperTiposReporte(bcp.TiposReporte model, string oper, int? id)
        {
            Debug.WriteLine("OPERACION SUMA " + id);
            model.OperAccion(oper, id, objSession);
            return Json("OK");
        }
        #endregion

        #region "FormasDePago"
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetFormasDePago(GridSettings gridSettings, bcp.FormasDePago model)
        {
            // create json data 
            bcp.FormasDePago bcpAccion = new FormasDePago();

            int totalRecords = bcpAccion.ListarFormasDePagoCount(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);
            Debug.WriteLine("TOTAL RECORDS " + totalRecords);
            //Aca se llena la lista que pobla la grilla de datos
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.FormasDePago> lstAccion = bcpAccion.ListarGrilla(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), startRow, endRow);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.FormasDePago item in lstAccion
                    select new
                    {
                        id = item.IdFP,
                        cell = new object[]
                        {
                            item.Codemp,
                            item.Idioma,
                            item.IdFP,
                            item.Nombre,
                            item.DiasVenc,
                            item.IngFV,
                            item.IngCuotas,
                            item.Tipo
                            
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult OperFormasDePago(bcp.FormasDePago model, string oper, int? id)
        {
            Debug.WriteLine("OPERACION SUMA ");
            model.OperAccion(oper, id, objSession);
            return Json("OK");
        }
        #endregion

        #region "TiposDocumentos"
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetTiposDocumentos(GridSettings gridSettings, bcp.TiposDocumentos model)
        {
            // create json data 
            bcp.TiposDocumentos bcpAccion = new TiposDocumentos();

            int totalRecords = bcpAccion.ListarTiposDocumentosCount(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);
            Debug.WriteLine("TOTAL RECORDS " + totalRecords);
            //Aca se llena la lista que pobla la grilla de datos
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.TiposDocumentos> lstAccion = bcpAccion.ListarGrilla(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), startRow, endRow);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.TiposDocumentos item in lstAccion
                    select new
                    {
                        id = item.Id,
                        cell = new object[]
                        {
                            item.Codemp,
                            item.TipoComprobante,
                            item.Codigo,
                            item.CodigoNumero,
                            item.Tipo,
                            item.Nombre, 
                            item.Talonario,
                            item.UltimoNumero,
                            item.LineasXReporte,
                            item.TipoDocDigital,
                            item.Id
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult OperTiposDocumentos(bcp.TiposDocumentos model, string oper, int? id)
        {
            Debug.WriteLine("OPERACION SUMA ");
            model.OperAccion(oper, id, objSession);
            return Json("OK");
        }
        #endregion

        #region "ClasificacionDocumentos"
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetClasificacionDocumentos(GridSettings gridSettings, bcp.ClasificacionDocumentos model)
        {
            // create json data 
            bcp.ClasificacionDocumentos bcpAccion = new ClasificacionDocumentos();

            int totalRecords = bcpAccion.ListarClasificacionDocumentosCount(objSession.CodigoEmpresa, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);
            Debug.WriteLine("TOTAL RECORDS " + totalRecords);
            //Aca se llena la lista que pobla la grilla de datos
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.ClasificacionDocumentos> lstAccion = bcpAccion.ListarGrilla(objSession.CodigoEmpresa, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), startRow, endRow);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.ClasificacionDocumentos item in lstAccion
                    select new
                    {
                        id = item.Id,
                        cell = new object[]
                        {
                            item.Codemp,
                            item.Codigo,
                            item.Tipo,
                            item.TipoComprobante,
                            item.TipoProducto,
                            item.CostosSN,
                            item.SeleccionOtroComprobanteSN,
                            item.CarteraClientesSN,
                            item.FinalizaDeudaSN,
                            item.ContableSN,
                            item.CambiaDocumentoSN,
                            item.AnulaImpuestoSN,
                            item.FormaPagoSN,
                            item.OrdenCompraSN,
                            item.Concepto,
                            item.Movimiento,
                            item.MostrarEnLibrosSN,
                            item.HonorariosSN,
                            item.Cuenta,
                            item.Stock,
                            item.SaldosSN,
                            item.ReservaSN,
                            item.TransitoSN,                            
                            item.Id
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult OperClasificacionDocumentos(bcp.ClasificacionDocumentos model, string oper, int? id)
        {
            Debug.WriteLine("OPERACION SUMA ");
            model.OperAccion(oper, id, objSession);
            return Json("OK");
        }
        #endregion

        public ActionResult ExportToExcel(GridSettings gridSettings)
        {
            var products = new System.Data.DataTable("teste");
            products.Columns.Add("col1", typeof(int));
            products.Columns.Add("col2", typeof(string));

            products.Rows.Add(1, "product 1");
            products.Rows.Add(2, "product 2");
            products.Rows.Add(3, "product 3");
            products.Rows.Add(4, "product 4");
            products.Rows.Add(5, "product 5");
            products.Rows.Add(6, "product 6");
            products.Rows.Add(7, "product 7");

            bcp.Accion objAccion = new Accion();


            var grid = new GridView();
            grid.DataSource = objAccion.ExportarExcel(1, 1, gridSettings.where.groupOp, "Nombre", gridSettings.sortOrder.ToString(), 1, 100000000); ;
            grid.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=MyExcelFile.xls");
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
    }
}

