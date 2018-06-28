using Dimol.dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Carteras.Mantenedores.dao
{
    public class MotivoCobranza
    {
        public List<dto.MotivoCobranza> ListarMotivoCobranzaGrilla(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.MotivoCobranza> lstMotivoCobranza = new List<dto.MotivoCobranza>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Motivos_Cobranza_Grilla");
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
                        lstMotivoCobranza.Add(new dto.MotivoCobranza()
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
            return lstMotivoCobranza;
        }

        public List<dto.MotivoCobranza> ExportarExcel(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.MotivoCobranza> lstMotivoCobranza = new List<dto.MotivoCobranza>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Motivos_Cobranza_Grilla");
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
                        lstMotivoCobranza.Add(new dto.MotivoCobranza()
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
            return lstMotivoCobranza;
        }

        public static int ListarMotivoCobranzaGrillaCount(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            int count = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Motivos_Cobranza_Grilla_Count");
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
                    count = Int32.Parse(ds.Tables[0].Rows[0]["total"].ToString());
                }

                return count;
            }
            catch (Exception ex)
            {
                return count;
            }
        }

        public void InsertarMotivoCobranza(dto.MotivoCobranza objAccion, int codemp)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Insertar_MotivoCobranza");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("nombre", objAccion.Nombre.ToUpper());
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void EditarMotivoCobranza(dto.MotivoCobranza objAccion, int codemp)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Update_MotivoCobranza");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("id", objAccion.Id);
                sp.AgregarParametro("nombre", objAccion.Nombre.ToUpper());
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void BorrarMotivoCobranza(int codemp, int id)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Delete_MotivoCobranza");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("id", id);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
