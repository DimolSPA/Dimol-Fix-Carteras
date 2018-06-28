using Dimol.dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Judicial.Mantenedores.dao
{
    public class Mandatos
    {
        public string ListarNotarias(int codEmp)
        {
            string salida = "";
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Notarias");
                sp.AgregarParametro("codemp", codEmp);
                ds = sp.EjecutarProcedimiento();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        salida += ds.Tables[0].Rows[i][0].ToString().Trim() + ":" + ds.Tables[0].Rows[i][1].ToString().Trim();
                    }
                    else
                    {
                        salida += ";" + ds.Tables[0].Rows[i][0].ToString().Trim() + ":" + ds.Tables[0].Rows[i][1].ToString().Trim();
                    }
                }
                salida = salida.Replace("\"", "'");
                return salida;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public string ListarClientes(int codEmp)
        {
            string salida = "";
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Clientes");
                sp.AgregarParametro("codemp", codEmp);
                ds = sp.EjecutarProcedimiento();
                for (int i = 1; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (i == 1)
                    {
                        salida += ds.Tables[0].Rows[i][0].ToString().Trim() + ":" + ds.Tables[0].Rows[i][1].ToString().Trim();
                    }
                    else
                    {
                        salida += ";" + ds.Tables[0].Rows[i][0].ToString().Trim() + ":" + ds.Tables[0].Rows[i][1].ToString().Trim();
                    }
                }
                salida = salida.Replace("\"", "'");
                return salida;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public List<dto.Mandatos> ListarMandatosGrilla(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.Mandatos> lstMandatos = new List<dto.Mandatos>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Mandatos_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idid", idioma);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lstMandatos.Add(new dto.Mandatos()
                        {
                            Codemp = Int16.Parse(ds.Tables[0].Rows[i]["codemp"].ToString()),
                            IdCliente = Int16.Parse(ds.Tables[0].Rows[i]["IdCliente"].ToString()),
                            IdNotaria = Int16.Parse(ds.Tables[0].Rows[i]["IdNotaria"].ToString()),
                            NumeroRepertorio = ds.Tables[0].Rows[i]["NumeroRepertorio"].ToString(),
                            RutCliente  = ds.Tables[0].Rows[i]["RutCliente"].ToString(),
                            NombreCliente = ds.Tables[0].Rows[i]["NombreCliente"].ToString(),
                            NombreNotaria = ds.Tables[0].Rows[i]["NombreNotaria"].ToString(),
                            FechaAsignacion = ds.Tables[0].Rows[i]["FechaAsignacion"].ToString(),
                            FechaVencimiento = ds.Tables[0].Rows[i]["FechaVencimiento"].ToString(),
                            Indefinido = ds.Tables[0].Rows[i]["Indefinido"].ToString().ToUpper() == "S" ? "ON" : "OFF"

                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lstMandatos;
        }

        public List<dto.Mandatos> ExportarExcel(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.Mandatos> lstMandatos = new List<dto.Mandatos>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Mandatos_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idid", idioma);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lstMandatos.Add(new dto.Mandatos()
                        {
                            Codemp = Int16.Parse(ds.Tables[0].Rows[i]["codemp"].ToString()),
                            IdCliente = Int16.Parse(ds.Tables[0].Rows[i]["IdCliente"].ToString()),
                            IdNotaria = Int16.Parse(ds.Tables[0].Rows[i]["IdNotaria"].ToString()),
                            NumeroRepertorio = ds.Tables[0].Rows[i]["NumeroRepertorio"].ToString(),
                            RutCliente = ds.Tables[0].Rows[i]["RutCliente"].ToString(),
                            NombreCliente = ds.Tables[0].Rows[i]["NombreCliente"].ToString(),
                            NombreNotaria = ds.Tables[0].Rows[i]["NombreNotaria"].ToString(),
                            FechaAsignacion = ds.Tables[0].Rows[i]["FechaAsignacion"].ToString(),
                            FechaVencimiento = ds.Tables[0].Rows[i]["FechaVencimiento"].ToString(),
                            Indefinido = ds.Tables[0].Rows[i]["Indefinido"].ToString().ToUpper() 

                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lstMandatos;
        }

        public static int ListarMandatosGrillaCount(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            int count = 0;

            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Mandatos_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idid", idioma);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    count = Int32.Parse(ds.Tables[0].Rows[0]["count"].ToString());
                }

                return count;
            }
            catch (Exception ex)
            {
                return count;
            }
        }

        public void InsertarMandatos(dto.Mandatos objAccion, int codemp, int idioma)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Insertar_ProvCli_Mandato");
                sp.AgregarParametro("pcm_codemp", codemp);
                sp.AgregarParametro("pcm_pclid", (object)objAccion.NombreCliente?? DBNull.Value);
                sp.AgregarParametro("pcm_notid", (object)objAccion.NombreNotaria ?? DBNull.Value);
                sp.AgregarParametro("pcm_numrep", (object)objAccion.NumeroRepertorio ?? DBNull.Value);
                sp.AgregarParametro("pcm_indefinido", objAccion.Indefinido.ToUpper() == "ON" || objAccion.Indefinido.ToUpper() == "YES" ? "S" : "N");
                sp.AgregarParametro("pcm_fecvenc", objAccion.Indefinido.ToUpper() == "ON" || objAccion.Indefinido.ToUpper() == "YES" ? DBNull.Value : (object)objAccion.FechaVencimiento);
                sp.AgregarParametro("pcm_fecasig", (object)objAccion.FechaAsignacion ?? DBNull.Value);
                int error = sp.EjecutarProcedimientoTrans();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void EditarMandatos(dto.Mandatos objAccion, int codemp, int idioma)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Update_ProvCli_Mandato");
                sp.AgregarParametro("pcm_codemp", codemp);
                sp.AgregarParametro("pcm_pclid", objAccion.NombreCliente);
                sp.AgregarParametro("pcm_notid", objAccion.NombreNotaria);
                sp.AgregarParametro("pcm_numrep", objAccion.NumeroRepertorio);
                sp.AgregarParametro("pcm_indefinido", objAccion.Indefinido.ToUpper() == "ON" || objAccion.Indefinido.ToUpper() == "YES" ? "S" : "N");
                sp.AgregarParametro("pcm_fecvenc", objAccion.Indefinido.ToUpper() == "ON" || objAccion.Indefinido.ToUpper() == "YES" ? DBNull.Value : (object)objAccion.FechaVencimiento.Replace("-","/"));
                sp.AgregarParametro("pcm_fecasig", (object)objAccion.FechaAsignacion.ToString().Replace("-","/") ?? DBNull.Value);
                int error = sp.EjecutarProcedimientoTrans();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void BorrarMandatos(int codemp, int idCliente,int idNotaria,string numRep)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Delete_ProvCli_Mandato");
                sp.AgregarParametro("pcm_codemp", codemp);
                sp.AgregarParametro("pcm_pclid", idCliente);
                sp.AgregarParametro("pcm_notid", idNotaria);
                sp.AgregarParametro("pcm_numrep", numRep);
                int error = sp.EjecutarProcedimientoTrans();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
