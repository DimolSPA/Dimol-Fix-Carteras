using Dimol.dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Carteras.Mantenedores.dao
{
    public class MotivoCastigo
    {
        public List<dto.MotivoCastigo> ListarMotivoCastigoGrilla(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.MotivoCastigo> lstMotivoCastigo = new List<dto.MotivoCastigo>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Motivos_Castigo_Grilla");
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
                        lstMotivoCastigo.Add(new dto.MotivoCastigo()
                        {
                            Codemp = Int16.Parse(ds.Tables[0].Rows[i]["Codemp"].ToString()),
                            Id = Int16.Parse(ds.Tables[0].Rows[i]["Id"].ToString()),
                            Nombre = ds.Tables[0].Rows[i]["Nombre"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lstMotivoCastigo;
        }

        public List<dto.MotivoCastigo> ExportarExcel(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.MotivoCastigo> lstMotivoCastigo = new List<dto.MotivoCastigo>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Motivos_Castigo_Grilla");
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
                        lstMotivoCastigo.Add(new dto.MotivoCastigo()
                        {
                            Codemp = Int16.Parse(ds.Tables[0].Rows[i]["Codemp"].ToString()),
                            Id = Int16.Parse(ds.Tables[0].Rows[i]["Id"].ToString()),
                            Nombre = ds.Tables[0].Rows[i]["Nombre"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lstMotivoCastigo;
        }

        public static int ListarMotivoCastigoGrillaCount(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            int count = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Motivos_Castigo_Grilla_Count");
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
                    count = Int32.Parse(ds.Tables[0].Rows[0]["Count"].ToString());
                }

                return count;
            }
            catch (Exception ex)
            {
                return count;
            }
        }

        public void InsertarMotivoCastigo(dto.MotivoCastigo objAccion, int codemp, int idioma)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Insertar_MotivoCastigo");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("nombre", (object)objAccion.Nombre.ToUpper () ?? DBNull.Value);
                sp.AgregarParametro("idid", (object)idioma ?? DBNull.Value);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void EditarMotivoCastigo(dto.MotivoCastigo objAccion, int codemp, int idioma)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Update_Tipos_Motivos_Castigos");
                sp.AgregarParametro("codemp", (object)codemp ?? DBNull.Value);
                sp.AgregarParametro("id", (object)objAccion.Id ?? DBNull.Value);
                sp.AgregarParametro("nombre", (object)objAccion.Nombre.ToUpper() ?? DBNull.Value);
                sp.AgregarParametro("idid", (object)idioma ?? DBNull.Value);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void BorrarMotivoCastigo(int codemp, int id)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Delete_Tipos_Motivos_Castigos");
                sp.AgregarParametro("tmi_codemp", codemp);
                sp.AgregarParametro("tmi_tmcid", (object)id ?? DBNull.Value);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
