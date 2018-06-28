using Dimol.Slider.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mvc.HtmlHelpers;
using Dimol.Slider.dto;
using System.Globalization;

namespace Dimol.Slider.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Tasks()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Slide(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                s = "1";
            }
            ViewBag.Sucursal = s;

            return View();
        }

        public ActionResult Gestores(HttpPostedFileBase file, GestorCentralModel model)
        {
            if (file != null)
            {
                //string path = System.IO.Path.Combine(Server.MapPath("~/Res"), System.IO.Path.GetFileName(file.FileName));
                string path = System.IO.Path.Combine(Server.MapPath("~/Res/empleados"), model.Anexo.ToString() + System.IO.Path.GetExtension(file.FileName));
                file.SaveAs(path);
                ViewBag.Mensaje = "Imagen Cargada Correctamente";
            }
            return View();
        }

        public ActionResult RankingLlamadas(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                s = "1";
            }
            ViewBag.Sucursal = s;

            return View();
        }

        public ActionResult Ranking(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                s = "1";
            }
            ViewBag.Sucursal = s;

            return View();
        }

        public JsonResult GuardarGestor(GestorCentralModel model)
        {
            int salida = 0;

            ConexionSgd cng = new ConexionSgd();
            //DataSet ds = new DataSet();
             
            cng.Parametros.Add(new SqlParameter() { ParameterName = "@anexo", SqlDbType = SqlDbType.Int, Value = model.Anexo });
            cng.Parametros.Add(new SqlParameter() { ParameterName = "@nombre", SqlDbType = SqlDbType.VarChar, Value = model.Nombre });
            cng.Parametros.Add(new SqlParameter() { ParameterName = "@cartera", SqlDbType = SqlDbType.VarChar, Value = model.Cartera });
            cng.Parametros.Add(new SqlParameter() { ParameterName = "@sucursal", SqlDbType = SqlDbType.VarChar, Value = model.Sucursal });
            cng.Parametros.Add(new SqlParameter() { ParameterName = "@disponible", SqlDbType = SqlDbType.VarChar, Value = model.Disponible == "true" ? "ACTIVO":"INACTIVO" });
            salida = cng.ProcedimientoTran("_Insert_Gestor");
            
            return Json(salida);
        }

        public JsonResult EliminarGestor(int anexo)
        {
            int salida = 0;

            ConexionSgd cng = new ConexionSgd();
            //DataSet ds = new DataSet();

            cng.Parametros.Add(new SqlParameter() { ParameterName = "@anexo", SqlDbType = SqlDbType.Int, Value = anexo });
            salida = cng.ProcedimientoTran("_Delete_Gestor");
                       
            return Json(salida);
        }

        public JsonResult TraeGestor(int anexo)
        {

            GestorCentralModel gestor = new GestorCentralModel();

            ConexionSgd cng = new ConexionSgd();
            DataSet ds = new DataSet();

            cng.Parametros.Add(new SqlParameter() { ParameterName = "@anexo", SqlDbType = SqlDbType.Int, Value = anexo });            
            ds = cng.Procedimiento("_Trae_Gestor");

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        gestor.Anexo = Int32.Parse(dr["ANEXO"].ToString());
                        gestor.Nombre = dr["NOMBRE"].ToString();
                        gestor.Cartera = dr["CARTERA"].ToString();
                        gestor.Sucursal = dr["SUCURSAL"].ToString();
                        gestor.Disponible = dr["DISPONIBLE"].ToString();
                    }
                }                
            }

            if (System.IO.File.Exists(Server.MapPath("~/Res/empleados/" + gestor.Anexo + ".jpg"))) gestor.Existe = true;
            else gestor.Existe = false;

            return Json(gestor, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRankingGestores(string sucursal)
        {
            List<LlamadasModel> gestorParcial = new List<LlamadasModel>();
            List<LlamadasModel> gestorTotal = new List<LlamadasModel>();
            List<LlamadasModel> gestores = new List<LlamadasModel>();
            List<GestorCentralModel> gstCentral = new List<GestorCentralModel>();

            string query = "";
            int c;

            ConexionSgd cng = new ConexionSgd();

            
            ConexionElastic cnn = new ConexionElastic();
            DataSet ds = new DataSet();

            query = "SELECT * FROM CENTRAL_GESTOR (NOLOCK) where disponible = 'ACTIVO'";
            if(sucursal == "1") query += " and sucursal = 'SANTIAGO'";
            else if(sucursal == "2") query += " and sucursal = 'TALCA'";

            ds = cng.Registros(query);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        gstCentral.Add(new GestorCentralModel
                        {
                            Anexo = Int32.Parse(dr["ANEXO"].ToString()),
                            Nombre = dr["NOMBRE"].ToString(),
                            Cartera = dr["CARTERA"].ToString(),
                            Sucursal = dr["SUCURSAL"].ToString(),
                            Disponible = dr["DISPONIBLE"].ToString()
                        });
                    }
                }
            }

            query = "SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED; select case when locate('\"', clid) = 0 then 'N/A' else substring(clid, locate('\"', clid) + 1, locate('\"', clid, locate('\"', clid)+1)-2) end as Nombre_Agente, case when length(src) > 4 then dst else src end as Codigo_Agente, count(*) as Tot from asteriskcdrdb.cdr where cast(calldate as date) = cast(NOW() as date) and disposition = 'ANSWERED' and billsec >= 15 and (length(src) + length(dst)) > 8 group by Codigo_Agente; COMMIT;"; //Llamadas validas
            ds = cnn.Registros(query);


            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        gestorParcial.Add(new LlamadasModel
                        {
                            NombreGestor = dr["Nombre_Agente"].ToString(),
                            NumeroGestor = dr["Codigo_Agente"].ToString(),
                            Efectivas = Int32.Parse(dr["Tot"].ToString())
                        });
                    }
                }
            }

            query = "SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED; select case when locate('\"', clid) = 0 then 'N/A' else substring(clid, locate('\"', clid) + 1, locate('\"', clid, locate('\"', clid)+1)-2) end as Nombre_Agente, case when length(src) > 4 then dst else src end as Codigo_Agente, count(*) as Tot from asteriskcdrdb.cdr where cast(calldate as date) = cast(NOW() as date) and (length(src) + length(dst)) > 8 group by Codigo_Agente; COMMIT;"; //Totales                
            ds = cnn.Registros(query);

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {

                        gestorTotal.Add(new LlamadasModel
                        {
                            NombreGestor = dr["Nombre_Agente"].ToString(),
                            NumeroGestor = dr["Codigo_Agente"].ToString(),
                            Totales = Int32.Parse(dr["Tot"].ToString())
                        });
                    }
                }
            }
            
            foreach (LlamadasModel tot in gestorTotal)
            {
                c = 0;

                foreach (LlamadasModel item in gestorParcial)
                {
                    if (item.NumeroGestor == tot.NumeroGestor && gstCentral.Where(x => x.Anexo.ToString() == item.NumeroGestor).Count() > 0)
                    {
                        gestores.Add(new LlamadasModel
                        {
                            //NombreGestor = item.NombreGestor,
                            NombreGestor = gstCentral.Where(x => x.Anexo.ToString() == tot.NumeroGestor).Select(x => x.Nombre).FirstOrDefault(),
                            NumeroGestor = item.NumeroGestor,
                            Efectivas = item.Efectivas,
                            Totales = tot.Totales,
                            Promedio = ((decimal)item.Efectivas / tot.Totales) * 100,
                            Cartera = gstCentral.Where(x => x.Anexo.ToString() == tot.NumeroGestor).Select(x => x.Cartera).FirstOrDefault()
                        });

                        c = 1;
                    }
                }

                if (c == 0 && gstCentral.Where(x => x.Anexo.ToString() == tot.NumeroGestor).Count() > 0)
                {
                    gestores.Add(new LlamadasModel
                    {
                        //NombreGestor = tot.NombreGestor,
                        NombreGestor = gstCentral.Where(x => x.Anexo.ToString() == tot.NumeroGestor).Select(x=>x.Nombre).FirstOrDefault(),
                        NumeroGestor = tot.NumeroGestor,
                        Efectivas = 0,
                        Totales = tot.Totales,
                        Promedio = 0,
                        Cartera = gstCentral.Where(x => x.Anexo.ToString() == tot.NumeroGestor).Select(x => x.Cartera).FirstOrDefault()
                    });
                }

            }

            gestores = gestores.OrderByDescending(x => x.Efectivas).ThenByDescending(x => x.Promedio).ToList();
            ViewBag.Total = gestores.Count;

            var jsonData = new {

                rows =
                (
                    from LlamadasModel item in gestores
                    select new
                    {
                        id = item.NumeroGestor,
                        cell = new object[]
                        {
                            item.NombreGestor,
                            item.NumeroGestor,
                            item.Efectivas,
                            item.Totales,
                            item.PromedioStr,
                            item.Cartera
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTareas(GridSettings gridSettings)
        {

            int totalRecords = bcp.Tasks.ListarTareasCount(DateTime.Now.ToString("dd-MM-yyyy"), gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);

            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<Tarea> lst = bcp.Tasks.ListarTareas(DateTime.Now.ToString("dd-MM-yyyy"), gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from Tarea item in lst
                    select new
                    {
                        id = item.Id,
                        cell = new object[]
                        {
                            item.Id,
                            item.Nombre,
                            item.Observacion,
                            item.FechaTarea,
                            item.Completa,
                            item.Activa,
                            item.Lunes,
                            item.Martes,
                            item.Miercoles,
                            item.Jueves,
                            item.Viernes

                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CompletarTarea(int IdTarea, int Completa)
        {
            int exito;
           
            exito = bcp.Tasks.CompletarTarea(IdTarea, Completa);
            
            return Json(exito, JsonRequestBehavior.AllowGet);

        }

        public JsonResult ValidarFechaTarea(string fechaActual)
        {
            int exito;

            if (DateTime.ParseExact(fechaActual, "dd/MM/yyyy", CultureInfo.InvariantCulture) >= DateTime.Today)
            {
                exito = 1;
            }
            else
            {
                exito = 0;
            }

            return Json(exito, JsonRequestBehavior.AllowGet);

        }

        public JsonResult ValidarTareaCumplida(int idTarea)
        {
            int exito;

            exito = bcp.Tasks.ValidarTareaCumplida(idTarea);

            return Json(exito, JsonRequestBehavior.AllowGet);

        }        

        public JsonResult DesactivarTarea(string nombre)
        {
            int exito;

            exito = bcp.Tasks.DesactivarTarea(nombre);

            return Json(exito, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GuardarTarea(int id, string nombre, string obs, string fechaTarea, string dias)
        {
            int exito;

            if (id != 0)
            {
                exito = bcp.Tasks.ActualizarTarea(id, nombre.Replace(',', ' ').Replace(Convert.ToChar(10), ' '), obs.Replace(',',' ').Replace(Convert.ToChar(10), ' '), fechaTarea, dias);
            }
            else
            {
                exito = bcp.Tasks.GuardarTarea(nombre.Replace(',', ' ').Replace(Convert.ToChar(10), ' '), obs.Replace(',', ' ').Replace(Convert.ToChar(10), ' '), fechaTarea, dias);
            }

            return Json(exito, JsonRequestBehavior.AllowGet);

        }

        public JsonResult VerificarTareasSemanales()
        {
            int exito;
                       
            exito = bcp.Tasks.VerificarTareasSemanales();
            
            return Json(exito, JsonRequestBehavior.AllowGet);

        }

        public JsonResult EnviarEmailTareas()
        {
            bool exito;

            exito = bcp.Tasks.EnviarEmail();

            return Json(exito, JsonRequestBehavior.AllowGet);

        }

    }
}