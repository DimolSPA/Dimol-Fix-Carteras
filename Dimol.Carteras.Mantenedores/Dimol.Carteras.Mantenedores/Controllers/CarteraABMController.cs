using Mvc.HtmlHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Dimol.Carteras.Mantenedores.Models;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using Dimol.dto;
using Dimol.Carteras.Mantenedores.bcp;
using System.Globalization;
using System.Threading;

namespace Dimol.Carteras.Mantenedores.Controllers
{
    public class CarteraABMController : Dimol.Controllers.BaseController
    {
        //
        // GET: /Mantenedor/

        public ActionResult Index()
        {
            //objSession.CodigoEmpresa = 1;
            //Session["Usuario"] = objSession;
            //GridModel model = new GridModel();
            //model.GridSelect = "1:USA;2:England;3:France;4:Germany";
            //ViewBag.Add = false;
            //ViewBag.Del = true;
            //ViewBag.Edit = false;
            //ViewBag.Menu = objSession.Menu;
            //CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            //TextInfo textInfo = cultureInfo.TextInfo;
            //ViewBag.UserName = textInfo.ToTitleCase(objSession.Nombre.ToLower());
            //ViewBag.Cargo = textInfo.ToTitleCase(objSession.Cargo.ToLower());
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

       

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken()]
        public ActionResult Index(GridModel model)
        {
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                List<MotivoCobranzaModel> lstMotivo = serializer.Deserialize<List<MotivoCobranzaModel>>(model.GridData);

                // Process returned data here
            }
            catch
            {
            }
            return RedirectToAction("Index");
        }
    
        #region "Acciones"

        public ActionResult Acciones()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            } 
            ViewBag.Agrupa = bcp.Accion.ListarGrupoAcciones(objSession.Idioma);
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetAcciones(GridSettings gridSettings)
        {
            // create json data 
            bcp.Accion bcpAccion = new Accion();


            int totalRecords = bcpAccion.ListarAccionesGrillaCount(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), gridSettings.pageIndex, gridSettings.pageSize); ;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;
            List<dto.Accion> lstAccion = bcpAccion.ListarAccionesGrilla(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), startRow, endRow);

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

        #region "Motivo Cobranza"

        public ActionResult MotivoCobranza()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetMotivoCobranza(GridSettings gridSettings)
        {
            // create json data 
            bcp.MotivoCobranza bcpMotivoCobranza = new MotivoCobranza();


            int totalRecords = bcp.MotivoCobranza.ListarMotivoCobranzaGrillaCount(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), gridSettings.pageIndex, gridSettings.pageSize);
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.MotivoCobranza> lstMotivoCobranza = bcpMotivoCobranza.ListarMotivoCobranzaGrilla(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), startRow, endRow);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.MotivoCobranza item in lstMotivoCobranza
                    select new
                    {
                        id = item.Id,
                        cell = new object[]
                        {
                            item.Id,
                            item.Nombre,
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult OperMotivoCobranza(bcp.MotivoCobranza model, string oper, int? id)
        {
            model.OperMotivoCobranza(oper, id, objSession);
            return Json("OK");
        }

        #endregion
   
        #region "Código de Carga"

        public ActionResult CodigoCarga()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            string ddd = bcp.CodigoCarga.ListarClientesCodigoCarga(objSession.CodigoEmpresa);
            ViewBag.Clientes = ddd;
            return View();
        }
        
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetCodigoCarga(GridSettings gridSettings)
        {
            // create json data
            bcp.CodigoCarga bcpCodigoCarga = new CodigoCarga();
            

            int totalRecords = bcp.CodigoCarga.ListarCodigoCargaGrillaCount(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), gridSettings.pageIndex, gridSettings.pageSize);
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;
List<dto.CodigoCarga> lstCodigoCarga = bcpCodigoCarga.ListarCodigoCargaGrilla(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), startRow, endRow);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.CodigoCarga item in lstCodigoCarga
                    select new
                    {
                        id = item.Codemp + "|" + item.Pclid + "|" + item.Correlativo,
                        cell = new object[]
                        {
                            item.Codemp,
                            item.Pclid,
                            item.NombreCliente,
                            item.Correlativo,
                            item.Codigo,
                            item.Nombre
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult OperCodigoCarga(bcp.CodigoCarga model, string oper, string  id)
        {
            model.OperCodigoCarga(oper, id, objSession);
            return Json("OK");
        }
        
        #endregion

        #region "Motivo Castigo"
        public ActionResult MotivoCastigo()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetMotivoCastigo(GridSettings gridSettings)
        {
            // create json data 
            bcp.MotivoCastigo bcpMotivoCastigo = new MotivoCastigo();
            

            int totalRecords = bcp.MotivoCastigo.ListarMotivoCastigoGrillaCount(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), gridSettings.pageIndex, gridSettings.pageSize);
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

List<dto.MotivoCastigo> lstMotivoCastigo = bcpMotivoCastigo.ListarMotivoCastigoGrilla(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), startRow, endRow);
            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.MotivoCastigo item in lstMotivoCastigo
                    select new
                    {
                        id = item.Id,
                        cell = new object[]
                        {
                            item.Id,
                            item.Nombre,
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult OperMotivoCastigo(bcp.MotivoCastigo model, string oper, int? id)
        {
            model.OperMotivoCastigo(oper, id, objSession);
            return Json("OK");
        }

        #endregion

        #region "Tipo Retiro entrega"
        public ActionResult TipoRetiroEntrega()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult OperTipoRetiroEntrega(bcp.TipoRetiroEntrega model, string oper, int? id)
        {
            model.OperTipoRetiroEntrega(oper, id, objSession);
            return Json("OK");
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetTipoRetiroEntrega(GridSettings gridSettings)
        {
            // create json data 
            bcp.TipoRetiroEntrega bcpTipoRetiroEntrega = new TipoRetiroEntrega();
            

            int totalRecords = bcp.TipoRetiroEntrega.ListarTipoRetiroEntregaGrillaCount(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), gridSettings.pageIndex, gridSettings.pageSize);
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;
List<dto.TipoRetiroEntrega> lstTipoRetiroEntrega = bcpTipoRetiroEntrega.ListarTipoRetiroEntregaGrilla(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), startRow, endRow);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.TipoRetiroEntrega item in lstTipoRetiroEntrega
                    select new
                    {
                        id = item.Id,
                        cell = new object[]
                        {
                            item.Id,
                            item.Nombre,
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region "Tipos Imagenes Documento"

        public ActionResult TipoImagenDocumento()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            //GridModel model = new GridModel();
            //model.GridSelect = "TipoImagenDocumento";
            //ViewBag.Add = true;
            //ViewBag.Del = true;
            //ViewBag.Edit = true;
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetTipoImagenDocumento(GridSettings gridSettings)
        {
            // create json data 
            bcp.TipoImagenDocumento bcpTipoImagenDocumento = new TipoImagenDocumento();
            

            int totalRecords = bcp.TipoImagenDocumento.ListarTipoImagenDocumentoGrillaCount(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), gridSettings.pageIndex, gridSettings.pageSize);
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;
List<dto.TipoImagenDocumento> lstTipoImagenDocumento = bcpTipoImagenDocumento.ListarTipoImagenDocumentoGrilla(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), startRow, endRow);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.TipoImagenDocumento item in lstTipoImagenDocumento
                    select new
                    {
                        id = item.Id,
                        cell = new object[]
                        {
                            item.Id,
                            item.Nombre,
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult OperTipoImagenDocumento(bcp.TipoImagenDocumento model, string oper, int? id)
        {
            model.OperTipoImagenDocumento(oper, id, objSession);
            return Json("OK");
        }


        #endregion

        #region "Tipos Documentos Deudor"

        public ActionResult TipoDocumentoDeudor()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            ViewBag.Tipo = "C:Cliente;R:Rut";
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetTipoDocumentoDeudor(GridSettings gridSettings)
        {
            // create json data 
            bcp.TipoDocumentoDeudor bcpTipoDocumentoDeudor = new TipoDocumentoDeudor();
            

            int totalRecords = bcp.TipoDocumentoDeudor.ListarTipoDocumentoDeudorGrillaCount(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), gridSettings.pageIndex, gridSettings.pageSize);
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;
            List<dto.TipoDocumentoDeudor> lstTipoDocumentoDeudor = bcpTipoDocumentoDeudor.ListarTipoDocumentoDeudorGrilla(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), startRow, endRow);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.TipoDocumentoDeudor item in lstTipoDocumentoDeudor
                    select new
                    {
                        id = item.Id,
                        cell = new object[]
                        {
                            item.Codemp ,
                            item.Id,
                            item.Nombre,
                            item.Tipo
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult OperTipoDocumentoDeudor(bcp.TipoDocumentoDeudor model, string oper, int? id)
        {
            model.OperTipoDocumentoDeudor(oper, id, objSession);
            return Json("OK");
        }


        #endregion

        #region "Grupo Cobranza"

        public ActionResult GrupoCobranza()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
           
            string ddd = bcp.GrupoCobranza.ListarEmpleadosGrupoCobranza(objSession.CodigoEmpresa);
            ViewBag.Empleados = ddd;
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetGrupoCobranza(GridSettings gridSettings)
        {
            // create json data 
            bcp.GrupoCobranza bcpGrupoCobranza = new GrupoCobranza();
            

            int totalRecords = bcp.GrupoCobranza.ListarGrupoCobranzaGrillaCount(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), gridSettings.pageIndex, gridSettings.pageSize, 1);
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;
List<dto.GrupoCobranza> lstGrupoCobranza = bcpGrupoCobranza.ListaGrupoCobranzaGrilla(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(),startRow, endRow, objSession.CodigoSucursal);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.GrupoCobranza item in lstGrupoCobranza
                    select new
                    {
                        id = item.Id,
                        cell = new object[]
                        {
                            item.Codemp ,
                            item.CodSucursal,
                            item.Id,
                            item.Nombre,
                            item.CodEmpleado,
                            item.NombreEmpleado
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult OperGrupoCobranza(bcp.GrupoCobranza model, string oper, int? id)
        {
            model.OperGrupoCobranza(oper, id, objSession);
            return Json("OK");
        }


        #endregion

        #region "Estado Cartera"
        public ActionResult EstadoCartera()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            
            string ddd = bcp.EstadoCartera.ListarTipoEstadoCartera(objSession.CodigoEmpresa,objSession.Permisos );
            ViewBag.Tipo = ddd;
            return View();
        }
        

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetEstadoCartera(GridSettings gridSettings)
        {

            //// create json data 
            //int totalRecords = objDeudor.ListarContactosCount(objSession.CodigoEmpresa, ctcid, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);

            //int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            //int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            //int endRow = startRow + gridSettings.pageSize;

            //List<dto.Contacto> lst = objDeudor.ListarContactos(objSession.CodigoEmpresa, ctcid, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);
            // create json data 
            bcp.EstadoCartera bcpEstadoCartera = new EstadoCartera();
            

            int totalRecords = bcp.EstadoCartera.ListarEstadoCarteraGrillaCount(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), gridSettings.pageIndex, gridSettings.pageSize);
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;
List<dto.EstadoCartera> lstEstadoCartera = bcpEstadoCartera.ListarEstadoCarteraGrilla(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), startRow, endRow);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.EstadoCartera item in lstEstadoCartera
                    select new
                    {
                        id = item.Id,
                        cell = new object[]
                        {
                            item.Codemp ,
                            item.Id,
                            item.Agrupa,
                            item.Nombre,
                            item.Utiliza,
                            item.PredJu,
                            item.SolFecha,
                            item.GenRet,
                            item.Compromiso
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult OperEstadoCartera(bcp.EstadoCartera model, string oper, int? id)
        {
            model.OperEstadoCartera(oper, id, objSession);
            return Json("OK");
        }

        #endregion

        #region "Gestor"
        public ActionResult Gestor()
        {

            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            string ddd = bcp.Gestor.ListarTipoCartera(objSession.Idioma);
            ViewBag.Tipo = ddd;
            string grupo = bcp.Gestor.ListarGrupos(objSession.CodigoEmpresa, objSession.CodigoSucursal);
            ViewBag.Grupo = grupo;
            string empleado = bcp.Gestor.ListarEmpleados(objSession.CodigoEmpresa);
            ViewBag.Empleado = empleado;
            
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetGestor(GridSettings gridSettings)
        {
            // create json data 
            bcp.Gestor bcpGestor = new Gestor();
            

            int totalRecords = bcp.Gestor.ListarGestorGrillaCount(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), gridSettings.pageIndex, gridSettings.pageSize, objSession.CodigoSucursal);
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;
            List<dto.Gestor> lstGestor = bcpGestor.ListarGestorGrilla(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), startRow, endRow, objSession.CodigoSucursal);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.Gestor item in lstGestor
                    select new
                    {
                        id = item.Id,
                        cell = new object[]
                        {
                            item.Codemp ,
                            item.CodSucursal,
                            item.Id,
                            item.Nombre,
                            item.Telefono,
                            item.Email,
                            item.TipoCartera,
                            item.ComKi ,
                            item.ComHon ,
                            item.ComJKi ,
                            item.ComJHon,
                            item.Remoto,
                            item.Estado ,
                            item.Empleado,
                            item.Grupo
                            
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult OperGestor(bcp.Gestor model, string oper, int? id)
        {
            model.OperGestor(oper, id, objSession);
            return Json("OK");
        }

        #endregion

        #region Exportar a Excel

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
            grid.DataSource = objAccion.ExportarExcel(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, "Nombre", gridSettings.sortOrder.ToString(), 1, 100000000); ;
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

        public ActionResult ExportToExcelMotivoCobranza(GridSettings gridSettings)
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

            bcp.MotivoCobranza objAccion = new MotivoCobranza();


            var grid = new GridView();
            grid.DataSource = objAccion.ExportarExcel(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, "Nombre", gridSettings.sortOrder.ToString(), 1, 100000000); ;
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

        public ActionResult ExportToExcelTipoRetiroEntrega(GridSettings gridSettings)
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

            bcp.TipoRetiroEntrega objAccion = new TipoRetiroEntrega();


            var grid = new GridView();
            grid.DataSource = objAccion.ExportarExcel(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, "Nombre", gridSettings.sortOrder.ToString(), 1, 100000000); ;
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

        public ActionResult ExportToExcelCodigoCarga(GridSettings gridSettings)
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

            bcp.CodigoCarga objCodigoCarga = new CodigoCarga();


            var grid = new GridView();
            grid.DataSource = objCodigoCarga.ExportarExcel(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, "Nombre", gridSettings.sortOrder.ToString(), 1, 100000000); ;
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

        public ActionResult ExportToExcelMotivoCastigo(GridSettings gridSettings)
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

            bcp.MotivoCastigo objMotivoCastigo = new MotivoCastigo();

            var grid = new GridView();
            grid.DataSource = objMotivoCastigo.ExportarExcel(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, "Nombre", gridSettings.sortOrder.ToString(), 1, 100000000); ;
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

        public ActionResult ExportToExcelTipoImagenDocumento(GridSettings gridSettings)
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

            bcp.TipoImagenDocumento objTipoImagenDocumento = new TipoImagenDocumento();

            var grid = new GridView();
            grid.DataSource = objTipoImagenDocumento.ExportarExcel(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, "Nombre", gridSettings.sortOrder.ToString(), 1, 100000000); ;
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

        public ActionResult ExportToExcelTipoDocumentoDeudor(GridSettings gridSettings)
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

            bcp.TipoDocumentoDeudor objTipoDocumentoDeudor = new TipoDocumentoDeudor();

            var grid = new GridView();
            grid.DataSource = objTipoDocumentoDeudor.ExportarExcel(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, "Nombre", gridSettings.sortOrder.ToString(), 1, 100000000); ;
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

        public ActionResult ExportToExcelGrupoCobranza(GridSettings gridSettings)
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

            bcp.GrupoCobranza objGrupoCobranza = new GrupoCobranza();

            var grid = new GridView();
            grid.DataSource = objGrupoCobranza.ExportarExcel(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, "Nombre", gridSettings.sortOrder.ToString(), 1, 100000000, 1); ;
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

        public ActionResult ExportToExcelEstadoCartera(GridSettings gridSettings)
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

            bcp.EstadoCartera objEstadoCartera = new EstadoCartera();

            var grid = new GridView();
            grid.DataSource = objEstadoCartera.ExportarExcel(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, "Nombre", gridSettings.sortOrder.ToString(), 1, 100000000); 
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

        public ActionResult ExportToExcelGestor(GridSettings gridSettings)
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

            bcp.Gestor objGestor = new Gestor();

            var grid = new GridView();
            grid.DataSource = objGestor.ExportarExcel(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, "Nombre", gridSettings.sortOrder.ToString(), 1, 100000000, objSession.CodigoSucursal); 
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

        #endregion
    }
}

