using Dimol.dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Carteras.Mantenedores.dao
{
    public class EstadoCartera
    {
        public string ListarTipoEstadoCartera(int idioma, int permiso)
        {
            string salida = "";
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Etiquetas_EstadoCartera");
                sp.AgregarParametro("idioma", idioma);
                sp.AgregarParametro("permiso", permiso);
                ds = sp.EjecutarProcedimiento();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    //salida += ds.Tables[0].Rows[i][0].ToString().Trim() + ":" + ds.Tables[0].Rows[i][1].ToString().Trim();

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

        public List<dto.EstadoCartera> ListarEstadoCarteraGrilla(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.EstadoCartera> lstEstadoCartera = new List<dto.EstadoCartera>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Estado_Cartera_Grilla");
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
                        lstEstadoCartera.Add(new dto.EstadoCartera()
                        {
                            Codemp = Int16.Parse(ds.Tables[0].Rows[i]["CODEMP"].ToString()),
                            Id = Int16.Parse(ds.Tables[0].Rows[i]["ID"].ToString()),
                            Nombre = ds.Tables[0].Rows[i]["NOMBRE"].ToString(),
                            IdAgrupa = Int16.Parse(ds.Tables[0].Rows[i]["IDAGRUPA"].ToString()),
                            Agrupa = ds.Tables[0].Rows[i]["AGRUPA"].ToString(),
                            Utiliza = ds.Tables[0].Rows[i]["UTILIZA"].ToString().ToUpper() == "D" ? "ON" : "OFF",
                            PredJu = ds.Tables[0].Rows[i]["PREJUD"].ToString().ToUpper() == "P" ? "ON" : "OFF",
                            SolFecha = ds.Tables[0].Rows[i]["SOLFECHA"].ToString().ToUpper() == "S" ? "ON" : "OFF",
                            GenRet = ds.Tables[0].Rows[i]["GENRET"].ToString().ToUpper() == "S" ? "ON" : "OFF",
                            Compromiso = ds.Tables[0].Rows[i]["COMPROMISO"].ToString().ToUpper() == "S" ? "ON" : "OFF",
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lstEstadoCartera;
        }

        public static int ListarEstadoCarteraGrillaCount(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            int count = 0;
            List<dto.EstadoCartera> lstEstadoCartera = new List<dto.EstadoCartera>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Estado_Cartera_Grilla_Count");
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

        public void InsertarEstadoCartera(dto.EstadoCartera objAccion, int codemp, int idioma)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Insertar_Estados_Cartera");
                sp.AgregarParametro("ect_codemp", codemp);
                sp.AgregarParametro("ect_nombre", (object)objAccion.Nombre.ToUpper() ?? DBNull.Value);
                sp.AgregarParametro("ect_agrupa", (object)objAccion.Agrupa ?? DBNull.Value);
                sp.AgregarParametro("ect_utiliza", objAccion.Utiliza.ToUpper() == "ON" || objAccion.Utiliza.ToUpper() == "YES" ? "D" : "R");
                sp.AgregarParametro("ect_prejud", objAccion.PredJu.ToUpper() == "ON" || objAccion.PredJu.ToUpper() == "YES" ? "P" : "A");
                sp.AgregarParametro("ect_solfecha", objAccion.SolFecha.ToUpper() == "ON" || objAccion.SolFecha.ToUpper() == "YES" ? "S" : "N");
                sp.AgregarParametro("ect_genret", objAccion.GenRet.ToUpper() == "ON" || objAccion.GenRet.ToUpper() == "YES" ? "S" : "N");
                sp.AgregarParametro("ect_compromiso", objAccion.Compromiso.ToUpper() == "ON" || objAccion.Compromiso.ToUpper() == "YES" ? "S" : "N");
                sp.AgregarParametro("eci_idid", idioma);
                int error = sp.EjecutarProcedimientoTrans();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EditarEstadoCartera(dto.EstadoCartera objAccion, int codemp, int idioma)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Update_Estados_Cartera");
                sp.AgregarParametro("ect_codemp", codemp);
                sp.AgregarParametro("ect_estid", (object)objAccion.Id ?? DBNull.Value);
                sp.AgregarParametro("ect_nombre", (object)objAccion.Nombre.ToUpper() ?? DBNull.Value);
                sp.AgregarParametro("ect_agrupa", (object)objAccion.Agrupa ?? DBNull.Value);
                sp.AgregarParametro("ect_utiliza", objAccion.Utiliza.ToUpper() == "ON" || objAccion.Utiliza.ToUpper() == "YES"  ? "D" : "R");
                sp.AgregarParametro("ect_prejud", objAccion.PredJu.ToUpper() == "ON" || objAccion.PredJu.ToUpper() == "YES" ? "P" : "A");
                sp.AgregarParametro("ect_solfecha", objAccion.SolFecha.ToUpper() == "ON" || objAccion.SolFecha.ToUpper() == "YES" ? "S" : "N");
                sp.AgregarParametro("ect_genret", objAccion.GenRet.ToUpper() == "ON" || objAccion.GenRet.ToUpper() == "YES" ? "S" : "N");
                sp.AgregarParametro("ect_compromiso", objAccion.Compromiso.ToUpper() == "ON" || objAccion.Compromiso.ToUpper() == "YES" ? "S" : "N");
                sp.AgregarParametro("eci_idid", idioma);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void BorrarEstadoCartera(int codemp, int id, int codid)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Delete_Estados_Cartera");
                sp.AgregarParametro("ect_codemp", codemp);
                sp.AgregarParametro("ect_estid", id);
                sp.AgregarParametro("eci_idid", codid);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<dto.EstadoCartera> ExportarExcel(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.EstadoCartera> lstEstadoCartera = new List<dto.EstadoCartera>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Estado_Cartera_Grilla");
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
                        lstEstadoCartera.Add(new dto.EstadoCartera()
                        {
                            Codemp = Int16.Parse(ds.Tables[0].Rows[i]["CODEMP"].ToString()),
                            Id = Int16.Parse(ds.Tables[0].Rows[i]["ID"].ToString()),
                            Nombre = ds.Tables[0].Rows[i]["NOMBRE"].ToString(),
                            IdAgrupa = Int16.Parse(ds.Tables[0].Rows[i]["IDAGRUPA"].ToString()),
                            Agrupa = ds.Tables[0].Rows[i]["AGRUPA"].ToString(),
                            Utiliza = ds.Tables[0].Rows[i]["UTILIZA"].ToString(),
                            PredJu = ds.Tables[0].Rows[i]["PREJUD"].ToString(),
                            SolFecha = ds.Tables[0].Rows[i]["SOLFECHA"].ToString(),
                            GenRet = ds.Tables[0].Rows[i]["GENRET"].ToString(),
                            Compromiso = ds.Tables[0].Rows[i]["COMPROMISO"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lstEstadoCartera;
        }

    }
}
