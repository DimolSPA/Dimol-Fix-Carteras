using Dimol.Slider.dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Slider.dao
{
    public class Tasks
    {
        public static List<Tarea> ListarTareas(string fecha, string where, string sidx, string sord, int inicio, int limite)
        {
            List<Tarea> lst = new List<Tarea>();
            try
            {
                ConexionSgd cng = new ConexionSgd();
                DataSet ds = new DataSet();

                cng.Parametros.Add(new SqlParameter() { ParameterName = "@fecha", SqlDbType = SqlDbType.VarChar, Value = fecha });
                cng.Parametros.Add(new SqlParameter() { ParameterName = "@where", SqlDbType = SqlDbType.VarChar, Value = where });
                cng.Parametros.Add(new SqlParameter() { ParameterName = "@sidx", SqlDbType = SqlDbType.VarChar, Value = sidx });
                cng.Parametros.Add(new SqlParameter() { ParameterName = "@sord", SqlDbType = SqlDbType.VarChar, Value = sord });
                cng.Parametros.Add(new SqlParameter() { ParameterName = "@inicio", SqlDbType = SqlDbType.Int, Value = inicio });
                cng.Parametros.Add(new SqlParameter() { ParameterName = "@limite", SqlDbType = SqlDbType.Int, Value = limite });
                
                ds = cng.Procedimiento("_Get_Tasks_By_Date");

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Tarea()
                        {
                            Id = Int32.Parse(ds.Tables[0].Rows[i]["ID"].ToString()),
                            Nombre = ds.Tables[0].Rows[i]["NOMBRE"].ToString(),
                            Observacion = ds.Tables[0].Rows[i]["OBSERVACION"].ToString(),
                            FechaIngreso = ds.Tables[0].Rows[i]["FECHA_INGRESO"].ToString(),
                            FechaTarea = ds.Tables[0].Rows[i]["FECHA_TAREA"].ToString(),
                            FechaCompleta = ds.Tables[0].Rows[i]["FECHA_COMPLETA"].ToString(),
                            Activa = Int32.Parse(ds.Tables[0].Rows[i]["ACTIVA"].ToString()),
                            Completa = Int32.Parse(ds.Tables[0].Rows[i]["COMPLETA"].ToString()),
                            Lunes = Int32.Parse(ds.Tables[0].Rows[i]["LUNES"].ToString()),
                            Martes = Int32.Parse(ds.Tables[0].Rows[i]["MARTES"].ToString()),
                            Miercoles = Int32.Parse(ds.Tables[0].Rows[i]["MIERCOLES"].ToString()),
                            Jueves = Int32.Parse(ds.Tables[0].Rows[i]["JUEVES"].ToString()),
                            Viernes = Int32.Parse(ds.Tables[0].Rows[i]["VIERNES"].ToString())
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                //Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Reportes.dao.ArbolJudicial.ListarAbogados", 0);
                return lst;
            }
        }

        public static int ListarTareasCount(string fecha, string where, string sidx, string sord, int inicio, int limite)
        {
            List<Tarea> lst = new List<Tarea>();
            try
            {
                ConexionSgd cng = new ConexionSgd();
                DataSet ds = new DataSet();

                cng.Parametros.Add(new SqlParameter() { ParameterName = "@fecha", SqlDbType = SqlDbType.VarChar, Value = fecha });
                cng.Parametros.Add(new SqlParameter() { ParameterName = "@where", SqlDbType = SqlDbType.VarChar, Value = where });
                cng.Parametros.Add(new SqlParameter() { ParameterName = "@sidx", SqlDbType = SqlDbType.VarChar, Value = sidx });
                cng.Parametros.Add(new SqlParameter() { ParameterName = "@sord", SqlDbType = SqlDbType.VarChar, Value = sord });
                cng.Parametros.Add(new SqlParameter() { ParameterName = "@inicio", SqlDbType = SqlDbType.Int, Value = inicio });
                cng.Parametros.Add(new SqlParameter() { ParameterName = "@limite", SqlDbType = SqlDbType.Int, Value = limite });

                ds = cng.Procedimiento("_Get_Tasks_By_Date_count");

                if (ds.Tables.Count > 0)
                {
                    return Int32.Parse(ds.Tables[0].Rows[0]["count"].ToString());
                }
                else
                {
                    return 0;
                }
                
            }
            catch (Exception ex)
            {
                //Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Reportes.dao.ArbolJudicial.ListarAbogadosCount", 0);
                return 0;
            }
        }

        public static int CompletarTarea(int IdTarea, int Completa)
        {
            int result = -1;
            try
            {
                ConexionSgd cng = new ConexionSgd();
                DataSet ds = new DataSet();

                cng.Parametros.Add(new SqlParameter() { ParameterName = "@id", SqlDbType = SqlDbType.Int, Value = IdTarea });
                cng.Parametros.Add(new SqlParameter() { ParameterName = "@done", SqlDbType = SqlDbType.Int, Value = Completa });
                
                result = cng.ProcedimientoTran("_Update_Task_Complete");
            }
            catch (Exception ex)
            {
                //Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.dao.BloquearRol", rolId);
                return result;
            }
            return result;
        }

        public static int ValidarTareaCumplida(int idTarea)
        {
            int result = -1;
            try
            {
                ConexionSgd cng = new ConexionSgd();
                DataSet ds = new DataSet();

                cng.Parametros.Add(new SqlParameter() { ParameterName = "@id", SqlDbType = SqlDbType.Int, Value = idTarea });

                ds = cng.Procedimiento("_Val_Task_Complete");

                if (ds.Tables.Count > 0)
                {
                    result = Int32.Parse(ds.Tables[0].Rows[0]["cont"].ToString());
                }
                else
                {
                    result = 0;
                }
            }
            catch (Exception ex)
            {
                //Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.dao.BloquearRol", rolId);
                return result;
            }
            return result;
        }

        public static string GenerarEmailBody()
        {
            string result = "";
            try
            {
                ConexionSgd cng = new ConexionSgd();
                DataSet ds = new DataSet();
                
                ds = cng.Procedimiento("_Mail_Tareas_Diarias");

                if (ds.Tables.Count > 0)
                {
                    result = ds.Tables[0].Rows[0]["BodyHtml"].ToString();
                }
                else
                {
                    result = "";
                }
            }
            catch (Exception ex)
            {                
                return result;
            }
            return result;
        }

        public static int DesactivarTarea(string nombre)
        {
            int result = -1;
            try
            {
                ConexionSgd cng = new ConexionSgd();
                DataSet ds = new DataSet();
              
                cng.Parametros.Add(new SqlParameter() { ParameterName = "@nombre", SqlDbType = SqlDbType.VarChar, Value = nombre });

                result = cng.ProcedimientoTran("_Update_Task_Inactive");
            }
            catch (Exception ex)
            {
                //Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.dao.BloquearRol", rolId);
                return result;
            }
            return result;
        }

        public static int GuardarTarea(string nombre, string obs, string fechaTarea, string dias)
        {
            int result = -1;
            try
            {
                ConexionSgd cng = new ConexionSgd();
                DataSet ds = new DataSet();

                cng.Parametros.Add(new SqlParameter() { ParameterName = "@nombre", SqlDbType = SqlDbType.VarChar, Value = nombre });
                cng.Parametros.Add(new SqlParameter() { ParameterName = "@obs", SqlDbType = SqlDbType.VarChar, Value = obs });
                cng.Parametros.Add(new SqlParameter() { ParameterName = "@fecha", SqlDbType = SqlDbType.VarChar, Value = fechaTarea });
                cng.Parametros.Add(new SqlParameter() { ParameterName = "@lu", SqlDbType = SqlDbType.Int, Value = Int32.Parse(dias.Split('|')[0]) });
                cng.Parametros.Add(new SqlParameter() { ParameterName = "@ma", SqlDbType = SqlDbType.Int, Value = Int32.Parse(dias.Split('|')[1]) });
                cng.Parametros.Add(new SqlParameter() { ParameterName = "@mi", SqlDbType = SqlDbType.Int, Value = Int32.Parse(dias.Split('|')[2]) });
                cng.Parametros.Add(new SqlParameter() { ParameterName = "@ju", SqlDbType = SqlDbType.Int, Value = Int32.Parse(dias.Split('|')[3]) });
                cng.Parametros.Add(new SqlParameter() { ParameterName = "@vi", SqlDbType = SqlDbType.Int, Value = Int32.Parse(dias.Split('|')[4]) });

                result = cng.ProcedimientoTran("_Insert_Task");
            }
            catch (Exception ex)
            {
                //Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.dao.BloquearRol", rolId);
                return result;
            }
            return result;
        }

        public static int VerificarTareasSemanales()
        {
            int result = -1;
            try
            {
                ConexionSgd cng = new ConexionSgd();
                DataSet ds = new DataSet();
                
                result = cng.ProcedimientoTran("_Verificar_Tareas_Semanales");
            }
            catch (Exception ex)
            {
                //Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.dao.BloquearRol", rolId);
                return result;
            }
            return result;
        }

        public static int ActualizarTarea(int id, string nombre, string obs, string fechaTarea, string dias)
        {
            int result = -1;
            try
            {
                ConexionSgd cng = new ConexionSgd();
                DataSet ds = new DataSet();

                cng.Parametros.Add(new SqlParameter() { ParameterName = "@id", SqlDbType = SqlDbType.Int, Value = id });
                cng.Parametros.Add(new SqlParameter() { ParameterName = "@nombre", SqlDbType = SqlDbType.VarChar, Value = nombre });
                cng.Parametros.Add(new SqlParameter() { ParameterName = "@obs", SqlDbType = SqlDbType.VarChar, Value = obs });
                cng.Parametros.Add(new SqlParameter() { ParameterName = "@fecha", SqlDbType = SqlDbType.VarChar, Value = fechaTarea });
                cng.Parametros.Add(new SqlParameter() { ParameterName = "@lu", SqlDbType = SqlDbType.Int, Value = Int32.Parse(dias.Split('|')[0]) });
                cng.Parametros.Add(new SqlParameter() { ParameterName = "@ma", SqlDbType = SqlDbType.Int, Value = Int32.Parse(dias.Split('|')[1]) });
                cng.Parametros.Add(new SqlParameter() { ParameterName = "@mi", SqlDbType = SqlDbType.Int, Value = Int32.Parse(dias.Split('|')[2]) });
                cng.Parametros.Add(new SqlParameter() { ParameterName = "@ju", SqlDbType = SqlDbType.Int, Value = Int32.Parse(dias.Split('|')[3]) });
                cng.Parametros.Add(new SqlParameter() { ParameterName = "@vi", SqlDbType = SqlDbType.Int, Value = Int32.Parse(dias.Split('|')[4]) });

                result = cng.ProcedimientoTran("_Update_Task");
            }
            catch (Exception ex)
            {
                //Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.dao.BloquearRol", rolId);
                return result;
            }
            return result;
        }
    }
}
