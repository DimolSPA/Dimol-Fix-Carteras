using Dimol.dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Carteras.Mantenedores.dao
{
    public class CodigoCarga
    {

        public string ListarClientesCodigoCarga(int codEmp)
        {
            string salida = "";
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Nombre_Cliente_Todos");
                sp.AgregarParametro("codemp", codEmp);
                ds = sp.EjecutarProcedimiento();
                for (int i = 1; i < ds.Tables[0].Rows .Count ; i++)
                {
                   //salida += ds.Tables[0].Rows[i][0].ToString().Trim() + ":" + ds.Tables[0].Rows[i][1].ToString().Trim();

                    if (i == 1)
                    {
                        salida += ds.Tables[0].Rows[i][1].ToString().Trim() + ":" + ds.Tables[0].Rows[i][0].ToString().Trim(); 
                    }
                    else
                    {
                        salida += ";" + ds.Tables[0].Rows[i][1].ToString().Trim() + ":" + ds.Tables[0].Rows[i][0].ToString().Trim();
                    }
                }
                salida = salida.Replace("\"","'");
                return salida;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public List<dto.CodigoCarga> ListarCodigoCargaGrilla(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.CodigoCarga> lstCodigoCarga = new List<dto.CodigoCarga>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Codigo_Carga_Grilla");
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
                        lstCodigoCarga.Add(new dto.CodigoCarga()
                        {
                            Codemp = Int16.Parse(ds.Tables[0].Rows[i]["Codemp"].ToString()),
                            Pclid = Int16.Parse(ds.Tables[0].Rows[i]["Pclid"].ToString()),
                            Correlativo= Int16.Parse(ds.Tables[0].Rows[i]["Correlativo"].ToString()),
                            NombreCliente = ds.Tables[0].Rows[i]["NombreCliente"].ToString(),
                            Codigo = ds.Tables[0].Rows[i]["Codigo"].ToString(),
                            Nombre = ds.Tables[0].Rows[i]["Nombre"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lstCodigoCarga;
        }

        public static int ListarCodigoCargaGrillaCount(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            int count = 0;
            List<dto.CodigoCarga> lstCodigoCarga = new List<dto.CodigoCarga>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Codigo_Carga_Grilla_Count");
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
            }
            return count;
        }

        public List<dto.CodigoCarga> ExportarExcel(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.CodigoCarga> lstCodigoCarga = new List<dto.CodigoCarga>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Codigo_Carga_Grilla");
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
                        lstCodigoCarga.Add(new dto.CodigoCarga()
                        {
                            Codemp = Int16.Parse(ds.Tables[0].Rows[i]["Codemp"].ToString()),
                            Pclid = Int16.Parse(ds.Tables[0].Rows[i]["Pclid"].ToString()),
                            Correlativo = Int16.Parse(ds.Tables[0].Rows[i]["Correlativo"].ToString()),
                            NombreCliente = ds.Tables[0].Rows[i]["NombreCliente"].ToString(),
                            Codigo = ds.Tables[0].Rows[i]["Codigo"].ToString(),
                            Nombre = ds.Tables[0].Rows[i]["Nombre"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lstCodigoCarga;
        }

        public void InsertarCodigoCarga(dto.CodigoCarga objAccion, int codemp)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Insertar_Codigo_Carga");
                sp.AgregarParametro("pcc_codemp", codemp);
                sp.AgregarParametro("pcc_pclid",  (object)objAccion.NombreCliente ?? DBNull.Value);
                sp.AgregarParametro("pcc_codigo", (object)objAccion.Codigo.ToUpper() ?? DBNull.Value);
                sp.AgregarParametro("pcc_nombre", (object)objAccion.Nombre.ToUpper() ?? DBNull.Value);
                int error = sp.EjecutarProcedimientoTrans();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EditarCodigoCarga(dto.CodigoCarga objAccion, int codemp)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Update_ProvCli_Codigo_Carga");
                sp.AgregarParametro("pcc_codemp", codemp);
                sp.AgregarParametro("pcc_pclid", (object)objAccion.NombreCliente ?? DBNull.Value);
                sp.AgregarParametro("pcc_codid", (object)objAccion.Correlativo ?? DBNull.Value);
                sp.AgregarParametro("pcc_codigo", (object)objAccion.Codigo.ToUpper() ?? DBNull.Value);
                sp.AgregarParametro("pcc_nombre", (object)objAccion.Nombre.ToUpper() ?? DBNull.Value);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void BorrarCodigoCarga( int codemp,int pcliid,int codid)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Delete_ProvCli_Codigo_Carga");
                sp.AgregarParametro("pcc_codemp", codemp);
                sp.AgregarParametro("pcc_pclid", pcliid);
                sp.AgregarParametro("pcc_codid", codid);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
