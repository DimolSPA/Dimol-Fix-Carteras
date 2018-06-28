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
using Dimol.Empresa.bcp;
using System.Globalization;
using System.Threading;
using Dimol.Empresa.Models;

namespace Dimol.Empresa.Controllers
{
    public class EmpresaController : Dimol.Controllers.BaseController
    {

        #region Idioma

        public ActionResult Idioma()
        {
            //ActivarSession();
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            GridModel model = new GridModel();
            model.GridSelect = "Idioma";
            ViewBag.Add = true;
            ViewBag.Del = true;
            ViewBag.Edit = true;
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetIdioma(GridSettings gridSettings)
        {
            // create json data 
            bcp.Idioma bcpIdioma = new Idioma();
            
            int totalRecords = bcp.Idioma.ListarIdiomaGrillaCount(1, 1, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), gridSettings.pageIndex, gridSettings.pageSize);
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;
            List<dto.Idioma> lstIdioma = bcpIdioma.ListarIdiomaGrilla(1, 1, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), startRow, endRow);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.Idioma item in lstIdioma
                    select new
                    {
                        id = item.Id,
                        cell = new object[]
                        {
                            item.Id,
                            item.Nombre,
                            item.Recurso
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult OperIdioma(bcp.Idioma model, string oper, int? id)
        {
            model.OperIdioma(oper, id, objSession);
            return Json("OK");
        }

        public ActionResult ExportToExcelIdioma(GridSettings gridSettings)
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

            bcp.Idioma objIdioma = new Idioma();

            var grid = new GridView();
            grid.DataSource = objIdioma.ExportarExcel(1, 1, gridSettings.where.groupOp, "Nombre", gridSettings.sortOrder.ToString(), 1, 100000000); ;
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

        #region Pais

        public ActionResult Pais()
        {
            //ActivarSession();
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            GridModel model = new GridModel();
            model.GridSelect = "Pais";
            ViewBag.Add = true;
            ViewBag.Del = true;
            ViewBag.Edit = true;
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetPais(GridSettings gridSettings)
        {
            // create json data 
            bcp.Pais bcpPais = new Pais();
            
            int totalRecords = bcp.Pais.ListarPaisGrillaCount(1, 1, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), gridSettings.pageIndex, gridSettings.pageSize);
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;
            List<dto.Pais> lstPais = bcpPais.ListarPaisGrilla(1, 1, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), startRow, endRow);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.Pais item in lstPais
                    select new
                    {
                        id = item.Id,
                        cell = new object[]
                        {
                            item.Id,
                            item.Nombre,
                            item.Codigo
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult OperPais(bcp.Pais model, string oper, int? id)
        {
            model.OperPais(oper, id, objSession);
            return Json("OK");
        }

        public ActionResult ExportToExcelPais(GridSettings gridSettings)
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

            bcp.Pais objPais = new Pais();

            var grid = new GridView();
            grid.DataSource = objPais.ExportarExcel(1, 1, gridSettings.where.groupOp, "Nombre", gridSettings.sortOrder.ToString(), 1, 100000000); ;
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

        #region Region

        public ActionResult Region()
        {
            //ActivarSession();
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            GridModel model = new GridModel();
            model.GridSelect = "Region";
            ViewBag.Add = true;
            ViewBag.Del = true;
            ViewBag.Edit = true;
            ViewBag.Region = bcp.Region.ListarPaises();
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetRegion(GridSettings gridSettings)
        {
            // create json data 
            bcp.Region bcpRegion = new Region();
            
            int totalRecords =bcp.Region.ListarRegionGrillaCount(1, 1, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), gridSettings.pageIndex, gridSettings.pageSize);
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;
            List<dto.Region> lstRegion = bcpRegion.ListarRegionGrilla(1, 1, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), startRow, endRow);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.Region item in lstRegion
                    select new
                    {
                        id = item.IdRegion,
                        cell = new object[]
                        {
                            item.IdPais,
                            item.NombrePais,
                            item.IdRegion,
                            item.NombreRegion,
                            item.Orden
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult OperRegion(bcp.Region model, string oper, int? id)
        {
            model.OperRegion(oper, id, objSession);
            return Json("OK");
        }

        public ActionResult ExportToExcelRegion(GridSettings gridSettings)
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

            bcp.Region objRegion = new Region();

            var grid = new GridView();
            grid.DataSource = objRegion.ExportarExcel(1, 1, gridSettings.where.groupOp, "NombrePais,Orden,NombreRegion", gridSettings.sortOrder.ToString(), 1, 100000000); ;
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

        #region Ciudad

        public ActionResult Ciudad()
        {
            //ActivarSession();
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            GridModel model = new GridModel();
            model.GridSelect = "Ciudad";
            ViewBag.Add = true;
            ViewBag.Del = true;
            ViewBag.Edit = true;
            ViewBag.Pais = bcp.Ciudad.ListarPaises();
            ViewBag.Region = bcp.Ciudad.ListarRegiones();
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetCiudad(GridSettings gridSettings)
        {
            // create json data 
            bcp.Ciudad bcpCiudad = new Ciudad();
            
            int totalRecords = bcp.Ciudad.ListarCiudadGrillaCount(1, 1, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), gridSettings.pageIndex, gridSettings.pageSize);
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;
            List<dto.Ciudad> lstCiudad = bcpCiudad.ListarCiudadGrilla(1, 1, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), startRow, endRow);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.Ciudad item in lstCiudad
                    select new
                    {
                        id = item.IdCiudad,
                        cell = new object[]
                        {
                            item.IdPais,
                            item.NombrePais,
                            item.IdRegion,
                            item.NombreRegion,
                            item.IdCiudad,
                            item.NombreCiudad,
                            item.CodigoArea
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult OperCiudad(bcp.Ciudad model, string oper, int? id)
        {
            model.OperCiudad(oper, id, objSession);
            return Json("OK");
        }

        public ActionResult ExportToExcelCiudad(GridSettings gridSettings)
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

            bcp.Ciudad objCiudad = new Ciudad();

            var grid = new GridView();
            grid.DataSource = objCiudad.ExportarExcel(1, 1, gridSettings.where.groupOp, "NombrePais,NombreRegion,NombreCiudad", gridSettings.sortOrder.ToString(), 1, 100000000); ;
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

        #region Comuna

        public ActionResult Comuna()
        {
            //ActivarSession();
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            GridModel model = new GridModel();
            model.GridSelect = "Comuna";
            ViewBag.Add = true;
            ViewBag.Del = true;
            ViewBag.Edit = true;
            ViewBag.Pais = bcp.Comuna.ListarPaises();
            ViewBag.Region = bcp.Comuna.ListarRegiones();
            ViewBag.Ciudad = bcp.Comuna.ListarCiudades();
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetComuna(GridSettings gridSettings)
        {
            // create json data 
            bcp.Comuna bcpComuna = new Comuna();
            
            int totalRecords = bcp.Comuna.ListarComunaGrillaCount(1, 1, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), gridSettings.pageIndex, gridSettings.pageSize);
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;
            List<dto.Comuna> lstComuna = bcpComuna.ListarComunaGrilla(1, 1, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), gridSettings.pageIndex, gridSettings.pageSize);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.Comuna item in lstComuna
                    select new
                    {
                        id = item.IdComuna,
                        cell = new object[]
                        {
                            item.IdPais,
                            item.NombrePais,
                            item.IdRegion,
                            item.NombreRegion,
                            item.IdCiudad,
                            item.NombreCiudad,
                            item.IdComuna,
                            item.NombreComuna,
                            item.CodigoPostal
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult OperComuna(bcp.Comuna model, string oper, int? id)
        {
            model.OperComuna(oper, id, objSession);
            return Json("OK");
        }

        public ActionResult ExportToExcelComuna(GridSettings gridSettings)
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

            bcp.Comuna objComuna = new Comuna();

            var grid = new GridView();
            grid.DataSource = objComuna.ExportarExcel(1, 1, gridSettings.where.groupOp, "NombrePais,NombreRegion,NombreCiudad", gridSettings.sortOrder.ToString(), 1, 100000000); ;
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

        #region Moneda

        public ActionResult Moneda()
        {
            //ActivarSession();
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            GridModel model = new GridModel();
            model.GridSelect = "Moneda";
            ViewBag.Add = true;
            ViewBag.Del = true;
            ViewBag.Edit = true;
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetMoneda(GridSettings gridSettings)
        {
            // create json data 
            bcp.Moneda bcpMoneda = new Moneda();
           
            int totalRecords = bcp.Moneda.ListarMonedaGrillaCount(1, 1, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), gridSettings.pageIndex, gridSettings.pageSize);
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;
            List<dto.Moneda> lstMoneda = bcpMoneda.ListarMonedasGrilla(1, 1, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), startRow, endRow);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.Moneda item in lstMoneda
                    select new
                    {
                        id = item.Id,
                        cell = new object[]
                        {
                            item.CodEmp,
                            item.Id,
                            item.Nombre,
                            item.Simbolo,
                            item.MonedaDefault,
                            item.Porcentaje,
                            item.Decimales,
                           
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult OperMoneda(bcp.Moneda model, string oper, int? id)
        {
            model.OperMoneda(oper, id, objSession);
            return Json("OK");
        }

        public ActionResult ExportToExcelMoneda(GridSettings gridSettings)
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

            bcp.Moneda objMoneda = new Moneda();

            var grid = new GridView();
            grid.DataSource = objMoneda.ExportarExcel(1, 1, gridSettings.where.groupOp, "Nombre", gridSettings.sortOrder.ToString(), 1, 100000000); ;
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

        #region Moneda

        public ActionResult Perfil()
        {
            //ActivarSession();
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            GridModel model = new GridModel();
            model.GridSelect = "Perfil";
            ViewBag.Add = true;
            ViewBag.Del = true;
            ViewBag.Edit = true;
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetPerfil(GridSettings gridSettings)
        {
            // create json data 
            bcp.Perfil bcpPerfil = new Perfil();
           
            int totalRecords = bcp.Perfil.ListarPerfilGrillaCount(1, 1, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), gridSettings.pageIndex, gridSettings.pageSize);
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;
            List<dto.Perfil> lstPerfil = bcpPerfil.ListarPerfilGrilla(1, 1, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), startRow, endRow);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.Perfil item in lstPerfil
                    select new
                    {
                        id = item.Id,
                        cell = new object[]
                        {
                            item.CodEmp,
                            item.Id,
                            item.Nombre,
                            item.Administrador,
                           
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult OperPerfil(bcp.Perfil model, string oper, int? id)
        {
            model.OperPerfil(oper, id, objSession);
            return Json("OK");
        }

        public ActionResult ExportToExcelPerfil(GridSettings gridSettings)
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

            bcp.Perfil objPerfil = new Perfil();

            var grid = new GridView();
            grid.DataSource = objPerfil.ExportarExcel(1, 1, gridSettings.where.groupOp, "Nombre", gridSettings.sortOrder.ToString(), 1, 100000000); ;
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

        #region Buscar Empleados

        public ActionResult BuscarEmpleado()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            ViewBag.TipoEstado = new SelectList(bcp.Empleado.ListarEstadosEmpleado(objSession.CodigoEmpresa), "Value", "Text"); 
            return View();
        }

        public ActionResult GetEmpleado(GridSettings gridSettings, BuscarEmpleadoModel model)
        {
            // create json data 
            bcp.Empleado bcpEmpleado = new Empleado();
            
            int totalRecords = bcp.Empleado.ListarEmpleadoGrillaCount(1, 1, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), gridSettings.pageIndex, gridSettings.pageSize, model.Nombre, model.ApellidoPaterno, model.ApellidoMaterno, model.Rut, model.Estado);
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;
            List<dto.Empleado> lstEmpleado = bcpEmpleado.ListarEmpleadoGrilla(1, 1, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), startRow, endRow, model.Nombre, model.ApellidoPaterno, model.ApellidoMaterno, model.Rut, model.Estado);      
           
            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.Empleado item in lstEmpleado
                    select new
                    {
                        id = item.Id,
                        cell = new object[]
                        {
                            item.CodEmp,
                            item.Id,
                            item.Foto,
                            item.Rut,
                            item.Nombre,
                            item.ApellidoPaterno,
                            item.ApellidoMaterno,
                            item.Estado ,
                            item.DescripcionEstado,
                          
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetEmpleadoVacio(GridSettings gridSettings)
        {
            // create json data 
            bcp.Empleado bcpEmpleado = new Empleado();

            int totalRecords = 0;


            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.Empleado> lst = new List<dto.Empleado>();


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.Empleado item in lst
                    select new
                    {
                        id = item.Id,
                        cell = new object[]
                        {
                            item.CodEmp,
                            item.Id,
                            item.Rut,
                            item.Nombre,
                            item.ApellidoPaterno,
                            item.ApellidoMaterno,
                            item.Foto ,
                            item.Estado 
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Empresa
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Empresa()

        {
            //ActivarSession();
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }

            dto.Empresa objEmpresa = new dto.Empresa();
            objEmpresa = bcp.Empresa.DatosEmpresa(objSession.CodigoEmpresa );
            EmpresaModel model = new EmpresaModel();
            model.Rut = objEmpresa.Rut;
            model.Nombre = objEmpresa.Nombre;
            model.NombreRepresentanteLegal = objEmpresa.NombreRepresentanteLegal;
            model.RutRepresentanteLegal = objEmpresa.RutRepresentanteLegal;
            model.Giro = objEmpresa.Giro;

            ViewBag.Comunas = bcp.EmpresaSucursal.ListaComunas();
            ViewBag.Add = true;
            ViewBag.Del = true;
            ViewBag.Edit = true;
            return View(model);
        }

        public JsonResult EditarEmpresa(GridSettings gridSettings, EmpresaModel model)
        {

            dto.Empresa empresa = new dto.Empresa();
            empresa.CodEmp = objSession.CodigoEmpresa;
            empresa.Rut = model.Rut;
            empresa.Nombre = model.Nombre;
            empresa.RutRepresentanteLegal = model.RutRepresentanteLegal;
            empresa.NombreRepresentanteLegal = model.NombreRepresentanteLegal;
            empresa.Giro = model.Giro;
            bcp.Empresa.EditarDatosEmpresa(empresa, objSession.CodigoEmpresa);
            return Json("OK");
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetSucurales(GridSettings gridSettings)
        {
            // create json data 
            bcp.EmpresaSucursal bcpEmpresaSucursal = new EmpresaSucursal();
           
            int totalRecords = bcp.EmpresaSucursal.ListarEmpresaSucursalGrillaCount(1, 1, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), gridSettings.pageIndex, gridSettings.pageSize);
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;
            List<dto.EmpresaSucursal> lstEmpresaSucursal = bcpEmpresaSucursal.ListarEmpresaSucursalGrilla(1, 1, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(),startRow,endRow);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.EmpresaSucursal item in lstEmpresaSucursal
                    select new
                    {
                        id = item.Id,
                        cell = new object[]
                        {
                            item.CodEmp,
                            item.Id,
                            item.Nombre,
                            item.IdComuna, 
                            item.Comuna,
	                        item.Direccion,
	                        item.Telefono,
	                        item.Fax,
	                        item.Email,
	                        item.Css,
                            item.Matriz,
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult OperEmpresaSucursal(bcp.EmpresaSucursal model, string oper, int? id)
        {
            model.OperEmpresaSucursal(oper, id, objSession);
            return Json("OK");
        }

        public ActionResult ExportToExcelEmpresaSucursal(GridSettings gridSettings)
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

            bcp.EmpresaSucursal objEmpresaSucursal = new EmpresaSucursal();

            var grid = new GridView();
            grid.DataSource = objEmpresaSucursal.ExportarExcel(1, 1, gridSettings.where.groupOp, "Nombre", gridSettings.sortOrder.ToString(), 1, 100000000); ;
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


        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetConfiguracion(GridSettings gridSettings)
        {
            // create json data 
            bcp.EmpresaConfiguracion bcpEmpresaConfiguracion = new EmpresaConfiguracion();
            
            int totalRecords = bcp.EmpresaConfiguracion.ListarEmpresaConfiguracionGrillaCount(1, 1, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), gridSettings.pageIndex, gridSettings.pageSize);
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;
            List<dto.EmpresaConfiguracion> lstEmpresaConfiguracion = bcpEmpresaConfiguracion.ListarEmpresaConfiguracionGrilla(1, 1, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), startRow, endRow);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.EmpresaConfiguracion item in lstEmpresaConfiguracion
                    select new
                    {
                        id = item.Id,
                        cell = new object[]
                        {
                            item.CodEmp,
                            item.Id,
                            item.Nombre,
                            item.ValorNumerico,
                            item.ValorTexto,
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

       
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult OperEmpresaConfiguracion(bcp.EmpresaConfiguracion model, string oper, int? id)
        {
            model.OperEmpresaConfiguracion(oper, id, objSession);
            return Json("OK");
        }

        public ActionResult ExportToExcelConfiguracion(GridSettings gridSettings)
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

            bcp.EmpresaConfiguracion objEmpresaConfiguracion = new EmpresaConfiguracion();

            var grid = new GridView();
            grid.DataSource = objEmpresaConfiguracion.ExportarExcel(1, 1, gridSettings.where.groupOp, "Nombre", gridSettings.sortOrder.ToString(), 1, 100000000); ;
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

