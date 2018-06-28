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
using Dimol.Finanzas.bcp;
using System.Globalization;
using System.Threading;
using Dimol.Finanzas.Models;
using System.Diagnostics;

namespace Dimol.Finanzas.Controllers
{
    public class FinanzasController : Dimol.Controllers.BaseController
    {

        string id = "";
       
        public ActionResult MaximaConvencional()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            GridModel model = new GridModel();
            model.GridSelect = "MaximaConvencional";
            ViewBag.Add = true;
            ViewBag.Del = true;
            ViewBag.Edit = true;
            ViewBag.Documento = bcp.MaximaConvencional.ListarTiposDocumentos(objSession.CodigoEmpresa, objSession.Idioma);
            ViewBag.Tipo = bcp.MaximaConvencional.ListarTipos(objSession.CodigoEmpresa, objSession.Idioma);
            ViewBag.Moneda = bcp.MaximaConvencional.ListarMonedas(objSession.CodigoEmpresa);
            ViewBag.Aplica = bcp.MaximaConvencional.ListarAplica(objSession.CodigoEmpresa, objSession.Idioma);

            return View(model);
        }

        public ActionResult CalculoComisiones()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            
            ViewBag.Anio = new SelectList(bcp.Comision.ListarAniosComision(objSession.CodigoEmpresa, objSession.CodigoSucursal, "Seleccione"), "Value", "Text", "");
            ViewBag.Mes = new SelectList(bcp.Comision.ListarMesesComision(objSession.Idioma, "Seleccione"), "Value", "Text", "");
            return View();
        }

        public ActionResult ClausulasContratoCartera()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            ViewBag.Edit = true;
            ViewBag.Tipo = new SelectList(bcp.ClausulaContratoCartera.ListarTipos(objSession.Idioma, "Seleccione"), "Value", "Text", "");
            ViewBag.TipoAplicacion = new SelectList(bcp.ClausulaContratoCartera.ListarTiposAplicacion(objSession.Idioma, "Seleccione"), "Value", "Text", "");
            ViewBag.Area = new SelectList(bcp.ClausulaContratoCartera.ListarAreas(objSession.Idioma, "Seleccione"), "Value", "Text", "");
            ViewBag.TipoRango = new SelectList(bcp.ClausulaContratoCartera.ListarTiposRango(objSession.Idioma, "Seleccione"), "Value", "Text", "");
            return View();
        }

        public ActionResult ContratosClienteCartera(string id)
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            ContratoCarteraModel model = new ContratoCarteraModel();
            ClausulaModel model2 = new ClausulaModel();
            var tuple = new Tuple<ContratoCarteraModel, ClausulaModel>(model,model2);
            //tuple.GridSelect = "ContratosClienteCartera";
            Debug.WriteLine("LA MUUYSDAODASD" + id);
            //ViewBag.Add = true;
            //ViewBag.Del = true;
            //ViewBag.Edit = true;
            ViewBag.Tipo = new SelectList(bcp.ContratoCartera.ListarTipos(objSession.Idioma, "Seleccione"), "Value", "Text", "");
            ViewBag.ClausulasTodas = bcp.Clausula.ListarClausulasTodas(objSession.CodigoEmpresa, objSession.Idioma);
            ViewBag.ClausulasTodas2 = new SelectList(bcp.Clausula.ListarClausulasTodas2(objSession.CodigoEmpresa, objSession.Idioma, "Seleccione"), "Value", "Text", "");
            return View(model);
        }

        public ActionResult Clausulas(string id)
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            ContratoCarteraModel model = new ContratoCarteraModel();
            ClausulaModel model2 = new ClausulaModel();
            var tuple = new Tuple<ContratoCarteraModel, ClausulaModel>(model, model2);
            
            //ViewBag.Add = true;
            //ViewBag.Del = true;
            //ViewBag.Edit = true;
            ViewBag.Tipo = new SelectList(bcp.ContratoCartera.ListarTipos(objSession.Idioma, "Seleccione"), "Value", "Text", "");
            ViewBag.ClausulasTodas = bcp.Clausula.ListarClausulasTodas(objSession.CodigoEmpresa, objSession.Idioma);
            
            return View(model);
        }

        public ActionResult NuevaClausula()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }

            ViewBag.ClausulasTodas2 = new SelectList(bcp.Clausula.ListarClausulasTodas2(objSession.CodigoEmpresa, objSession.Idioma, "Seleccione"), "Value", "Text", "");

            return View();
        }

        #region "Clausulas"
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetClausulas(string id, string idcct, string oper, bcp.Clausula model)
        {

            ViewBag.idCCT2 = id;
            //Debug.WriteLine("ID CLASULA " + id + "-" + ViewBag.idCCT2);
            //Debug.WriteLine("ID CONTRATO " + idCCT);
            //this.setId(id);
            int valor = 0;
            
            bcp.Clausula bcp = new Clausula();
            List<dto.Clausula> lst = new List<dto.Clausula>();

            if (idcct != null)
            {
                if (oper != null)
                {
                    
                    if (oper.Equals("eliminar"))
                    {
                        Debug.WriteLine("ENTRO A ELIMINAR");
                        model.OperAccion("del", Int32.Parse(id), objSession, Int32.Parse(idcct));
                        
                    }
                }
                if (oper != null)
                {

                    if (oper.Equals("agregar"))
                    {
                        Debug.WriteLine("ENTRO A AGREGAR");
                        model.OperAccion("add", Int32.Parse(id), objSession, Int32.Parse(idcct));

                    }
                }
                //ViewBag.ClausulasTodas = bcp.ListarClausulasTodas(objSession.CodigoEmpresa, objSession.Idioma, Int32.Parse(id));
                lst = bcp.ListarClausulasGrilla(objSession.CodigoEmpresa, objSession.Idioma, Int16.Parse(idcct.ToString()));

                var jsonData = new
                {
                    rows =
                    (
                        from dto.Clausula item in lst
                        select new
                        {
                            id = item.id,
                            cell = new object[]
                        {
                            item.id ,
                            item.cli_nombre,
                            item.idCCT
                            
                        }
                        }
                    ).ToArray()
                };
                
                Debug.WriteLine("JSON: " + jsonData);
                return Json(jsonData, JsonRequestBehavior.AllowGet);
                
            }

            else if (idcct == null)
            {
                if (oper != null)
                {
                    
                    if (oper.Equals("eliminar"))
                    {
                        Debug.WriteLine("ENTRO A ELIMINAR");
                        model.OperAccion("del", Int32.Parse(id), objSession, Int32.Parse(idcct));
                        
                    }
                }
                if (oper != null)
                {

                    if (oper.Equals("agregar"))
                    {
                        Debug.WriteLine("ENTRO A AGREGAR CON ID VACIO");
                        model.OperAccion("add", Int32.Parse(id), objSession, 0);

                    }
                }
                //ViewBag.ClausulasTodas = bcp.ListarClausulasTodas(objSession.CodigoEmpresa, objSession.Idioma, Int32.Parse(id));
                lst = bcp.ListarClausulasGrilla(objSession.CodigoEmpresa, objSession.Idioma, 0);

                var jsonData = new
                {
                    rows =
                    (
                        from dto.Clausula item in lst
                        select new
                        {
                            id = item.id,
                            cell = new object[]
                        {
                            item.id ,
                            item.cli_nombre,
                            item.idCCT
                            
                        }
                        }
                    ).ToArray()
                };
                
                Debug.WriteLine("JSON: " + jsonData);
                return Json(jsonData, JsonRequestBehavior.AllowGet);
                
            }
            else {
                return null;
            }
            
        }

        [HttpPost]
        [ValidateInput(false)]
        public string TestPost(FormCollection fc)
        {
            string res = "Form values (" + fc.Count + "):";
            foreach (var key in fc.AllKeys)
            {
                res += key + "=" + fc[key] + "|";
            }
            Debug.WriteLine(res);
            return res;
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult OperClausulas(string id, string idcct, string oper, bcp.Clausula model)
        {

            Debug.WriteLine("IDCCT" + idcct +  oper);
            //string idCCT = model2.idCCT.ToString();
            if(oper.Equals("eliminar")){
                Debug.WriteLine("ENTRO A ELIMINAR");
                model.OperAccion("del", Int32.Parse(id), objSession, Int32.Parse(idcct));
                return Json("OK");
            }
            //string _id = id.ToString();
            //string[] valores = _id.Split('-');
            //model.OperAccion(oper, valores[0], objSession, idCCT);
            return Json("OK");
        }
      
        #endregion

        #region "Acciones"
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetAcciones(GridSettings gridSettings)
        {
            // create json data 
            bcp.Accion bcpAccion = new Accion();
            List<dto.Accion> lstAccion = bcpAccion.ListarAccionesGrilla(1, 1, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), gridSettings.pageIndex, gridSettings.pageSize);
            
            int totalRecords = lstAccion.Count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;
           

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.Accion item in lstAccion
                    select new
                    {
                        id = item.IdAccion,
                        cell = new object[]
                        {
                            item.Codemp,
                            item.Idioma,
                            item.IdAccion,
                            item.Nombre,
                            item.Agrupa
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult OperAccion(bcp.Accion model, string oper, int? id)
        {
            model.OperAccion( oper, id, objSession);
            return Json("OK");
        }
        #endregion

        #region "CalculoComisiones"
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetComisiones(GridSettings gridSettings, int Anio, int Mes)
        {
            // create json data
            Debug.WriteLine("ANIO " + Anio + " - " + " Mes " + Mes);
            bcp.Comision bcp = new Comision();
            int totalRecords = bcp.ListarComisionesGrillaCount(objSession.CodigoEmpresa, objSession.CodigoSucursal, Anio, Mes, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder);
            Debug.WriteLine("TOTAL RECORDS" + totalRecords);
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.Comision> lst = bcp.ListarComisionesGrilla(objSession.CodigoEmpresa, objSession.CodigoSucursal, Anio, Mes, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.Comision item in lst
                    select new
                    {
                        //id = item.Id,
                        cell = new object[]
                        {
                            item.pcl_nomfant,
                            item.ctc_rut,
                            item.ctc_nomfant,
                            item.tci_nombre,
                            item.ddi_numcta,
                            item.FecCanc,
                            item.Capital,
                            item.Interes,
                            item.Honorario,
                            item.Total,
                            item.PorFact,
                            item.ges_nombre,
                            item.ComTotal
                            
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GuardarComisiones(string desde, string hasta)
        {
            Debug.WriteLine("Guardar Comision ");
            
            int valor = bcp.Comision.GrabarComision(objSession.CodigoEmpresa, objSession.Idioma, objSession.CodigoSucursal, desde, hasta);
            //objDeudor.GuardarDeudor(objSession.CodigoEmpresa, model.CodigoDeudor, model.Nombre, model.ApellidoPaterno, model.ApellidoMaterno, model.Rut, model.NombreFantasia, model.IdComuna, model.ParticularEmpresa, model.Direccion, model.IdSociedad.ToString(), quiebra, nacional, model.EstadoDireccion);
           

            return Json(valor);
        }
        #endregion

        #region "MaximaConvencional"
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetMaximaConvencional(GridSettings gridSettings, bcp.MaximaConvencional model)
        {
            // create json data 
            bcp.MaximaConvencional bcpAccion = new MaximaConvencional();

            int totalRecords = bcpAccion.ListarMaximaConvencionalGrillaCount(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder);
            Debug.WriteLine("TOTAL RECORDS " + totalRecords);
            //Aca se llena la lista que pobla la grilla de datos
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.MaximaConvencional> lstAccion = bcpAccion.ListarMaximaConvencionalGrilla(objSession.CodigoEmpresa, objSession.Idioma,  gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), startRow, endRow);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.MaximaConvencional item in lstAccion
                    select new
                    {
                        id = item.MXC_MXCID,
                        cell = new object[]
                        {                        
                            item.MXC_CODEMP,
                            item.MXC_MXCID,
                            item.TCI_NOMBRE,
                            item.MXC_TPCID,
                            item.MXC_TIPO,
                            item.MXC_APLICA,
                            item.MXC_CODMON,
                            item.MXC_DESDE,
                            item.MXC_HASTA,
                            item.MXC_VALOR
                            
                            
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult OperMaximaConvencional(bcp.MaximaConvencional model, string oper, int? id)
        {
            Debug.WriteLine("OPERACION SUMA ");
            model.OperAccion(oper, id, objSession);
            return Json("OK");
        }
        #endregion

        #region "ClausulasContratoCartera"
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetClausulasContratoCartera(GridSettings gridSettings, bcp.MaximaConvencional model)
        {
            // create json data 
            bcp.ClausulaContratoCartera bcpAccion = new ClausulaContratoCartera();

            int totalRecords = bcpAccion.ListarClausulaContratoCarteraGrillaCount(objSession.CodigoEmpresa, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder);
            Debug.WriteLine("TOTAL RECORDS " + totalRecords);
            //Aca se llena la lista que pobla la grilla de datos
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.ClausulaContratoCartera> lstAccion = bcpAccion.ListarClausulaContratoCarteraGrilla(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), startRow, endRow);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.ClausulaContratoCartera item in lstAccion
                    select new
                    {
                        id = item.clc_clcid,
                        cell = new object[]
                        {
                            item.clc_clcid,
                            item.clc_tipo,
                            item.TipoPorId,
                            item.TipoAplicacionPorId,
                            item.clc_nombre,
                            item.tipo,
                            item.Area,
                            item.clc_valor,
                            item.clc_rango,
                            item.ValorFijo,
                            item.Capital,
                            item.Interes,
                            item.Honorario,
                            item.GastoPrejudicial,
                            item.GastoJudicial,
                            item.AnulaMaximaConvencional,
                            item.tip_rango
                            
                        }

                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public void setId(string id){
            this.id = id;
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GetDatosClausula(string id)
        {
            Debug.WriteLine("ID CLASULA " + id);
            //this.setId(id);
            int valor = 0;
            bcp.ClausulaContratoCartera bcp = new ClausulaContratoCartera();
            List<dto.ClausulaContratoCartera> lst = bcp.ListarClausulaContratoCarteraPorID(objSession.CodigoEmpresa, objSession.Idioma, Int16.Parse(id.ToString()));
            
            var jsonData = new
            {
                rows =
                (
                    from dto.ClausulaContratoCartera item in lst
                    select new
                    {
                        id = item.clc_clcid,
                        cell = new object[]
                        {
                            item.clc_clcid,
                            item.clc_tipo,
                            item.TipoPorId,
                            item.clc_nombre,
                            item.tipo,
                            item.Area
                            
                        }
                    }
                ).ToArray()
            };
            Debug.WriteLine("JSON: " + jsonData);
            return Json(jsonData);
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GuardarClausulasContrato(string tipo, string nombre, string area, string tipoAplicacion, string valor
            , string rango, string valorFijo, string capital, string interes, string honorario, string gastoPrejudicial
            , string gastoJudicial, string anulaMaxima, string tipoRango, string nombreClonar, string clonar, int id)
        {
            
            Debug.WriteLine("Guardar CLAUSULA CONTRATO " + id + " CLONAR:" + clonar);
            dto.ClausulaContratoCartera ccc = new dto.ClausulaContratoCartera();
            ccc.AnulaMaximaConvencional = bool.Parse(anulaMaxima.ToString());
            if (area.Equals("1"))
            {
                ccc.Area = "P";
            }
            else if (area.Equals("2"))
            {
                ccc.Area = "J";
            }
            else if (area.Equals("3"))
            {
                ccc.Area = "A";
            }
            
            ccc.Capital = bool.Parse(capital.ToString());
            ccc.clc_clcid = id;
            ccc.clc_nombre = nombre;
            if (tipoAplicacion.Equals("1"))
            {
                ccc.clc_porcmon = "P";
            }
            else if (tipoAplicacion.Equals("2"))
            {
                ccc.clc_porcmon = "M";
            }
            else if (tipoAplicacion.Equals("3"))
            {
                ccc.clc_porcmon = "O";
            }
            
            ccc.clc_rango = bool.Parse(rango.ToString());
            ccc.clc_tipo = Int32.Parse(tipo.ToString());
            ccc.clc_valor = Decimal.Parse(valor.ToString());
            ccc.Clonar = bool.Parse(clonar.ToString());
            ccc.GastoJudicial = bool.Parse(gastoJudicial);
            ccc.GastoPrejudicial = bool.Parse(gastoPrejudicial.ToString());
            ccc.Honorario = bool.Parse(honorario.ToString());
            ccc.Interes = bool.Parse(interes.ToString());
            ccc.NombreClonar = nombreClonar;
            //ccc.tipo = tipo;
            ccc.TipoRango = tipoRango;
            ccc.ValorFijo = bool.Parse(valorFijo.ToString());

            int grabar = bcp.ClausulaContratoCartera.GrabarClausulaContratoCartera(objSession.CodigoEmpresa, objSession.Idioma, ccc);
            //objDeudor.GuardarDeudor(objSession.CodigoEmpresa, model.CodigoDeudor, model.Nombre, model.ApellidoPaterno, model.ApellidoMaterno, model.Rut, model.NombreFantasia, model.IdComuna, model.ParticularEmpresa, model.Direccion, model.IdSociedad.ToString(), quiebra, nacional, model.EstadoDireccion);


            return Json(grabar);
        }
        #endregion

        #region "ContratosClienteCarterra"
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetContratosCartera(string id, string oper, GridSettings gridSettings)
        {
            // create json data
            Debug.WriteLine("CONTRATOS CLIENTE CARTERA");
            bcp.ContratoCartera bcp = new ContratoCartera();
            if (id != null)
            {
                Debug.WriteLine("ENTRO A ELIMINAR CLIENTE CARTERA1");
                if (oper.Equals("eliminar"))
                {
                    Debug.WriteLine("ENTRO A ELIMINAR CLIENTE CARTERA2");
                    bcp.BorrarContratoCartera(objSession.CodigoEmpresa, Int32.Parse(id));
                }
            }
           
            int totalRecords = bcp.ListarContratoCarteraGrillaCount(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder);
            Debug.WriteLine("TOTAL RECORDS" + totalRecords);
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.ContratoCartera> lst = bcp.ListarContratoCarteraGrilla(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.ContratoCartera item in lst
                    select new
                    {
                        id = item.cct_cctid,
                        cell = new object[]
                        {
                            item.cct_cctid,
                            item.cct_nombre,
                            item.tipo
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetContratosCartera2(string id, string oper)
        {
            // create json data
            Debug.WriteLine("CONTRATOS CLIENTE CARTERA");
            bcp.ContratoCartera bcp = new ContratoCartera();
            if (id != null)
            {
                Debug.WriteLine("ENTRO A ELIMINAR CLIENTE CARTERA1");
                if (oper.Equals("eliminar"))
                {
                    Debug.WriteLine("ENTRO A ELIMINAR CLIENTE CARTERA2");
                    bcp.BorrarContratoCartera(objSession.CodigoEmpresa, Int32.Parse(id));
                }
            }
            return null;
        }

        
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GuardarTodoClausulas(string nom, string tipo)
        {
            // create json data
            Debug.WriteLine("CONTRATOS CLIENTE CARTERA GUARDAR NUEVO");
            bcp.ContratoCartera bcp = new ContratoCartera();
            if (nom != null)
            {


                bcp.GuardarTodoClausulas(objSession.CodigoEmpresa, nom, tipo);
                
            }
            return null;
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

