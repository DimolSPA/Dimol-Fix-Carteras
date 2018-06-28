using Dimol.dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Judicial.Mantenedores.dao
{
    public class TiposCausa
    {
        public List<dto.TiposCausa> ListarTiposCausaGrilla(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.TiposCausa> lstTiposCausa = new List<dto.TiposCausa>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Tipos_Causas_Grilla");
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
                        lstTiposCausa.Add(new dto.TiposCausa()
                        {
                            Codemp = Int16.Parse(ds.Tables[0].Rows[i]["CODEMP"].ToString()),
                            Id = Int16.Parse(ds.Tables[0].Rows[i]["ID"].ToString()),
                            Nombre = ds.Tables[0].Rows[i]["NOMBRE"].ToString(),
                          
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lstTiposCausa;
        }

        public List<dto.TiposCausa> ExportarExcel(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.TiposCausa> lstTiposCausa = new List<dto.TiposCausa>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Tipos_Causas_Grilla");
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
                        lstTiposCausa.Add(new dto.TiposCausa()
                        {
                            Codemp = Int16.Parse(ds.Tables[0].Rows[i]["CODEMP"].ToString()),
                            Id = Int16.Parse(ds.Tables[0].Rows[i]["ID"].ToString()),
                            Nombre = ds.Tables[0].Rows[i]["NOMBRE"].ToString(),

                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lstTiposCausa;
        }

        public static int ListarTiposCausaGrillaCount(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            int count = 0;
           
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Tipos_Causas_Grilla_Count");
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

        public void InsertarTiposCausa(dto.TiposCausa objAccion, int codemp, int idioma)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Insertar_Tipos_Causa");
                sp.AgregarParametro("tca_codemp", codemp);
                sp.AgregarParametro("tca_nombre", objAccion.Nombre.ToUpper());
                sp.AgregarParametro("idid", idioma);
                int error = sp.EjecutarProcedimientoTrans();
           }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void EditarTiposCausa(dto.TiposCausa objAccion, int codemp, int idioma)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Update_Tipos_Causa");
                sp.AgregarParametro("tca_codemp", codemp);
                sp.AgregarParametro("tca_tcaid", objAccion.Id);
                sp.AgregarParametro("tca_nombre", objAccion.Nombre.ToUpper());
                sp.AgregarParametro("idid", idioma);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void BorrarTiposCausa(int codemp, int id)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Delete_Tipos_Causa");
                sp.AgregarParametro("tca_codemp", codemp);
                sp.AgregarParametro("tca_tcaid", id);
                int error = sp.EjecutarProcedimientoTrans();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
