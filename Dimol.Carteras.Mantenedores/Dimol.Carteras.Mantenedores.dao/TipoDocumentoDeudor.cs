using Dimol.dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Carteras.Mantenedores.dao
{
    public class TipoDocumentoDeudor
    {

        public List<dto.TipoDocumentoDeudor> ListarTipoDocumentoDeudorGrilla(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.TipoDocumentoDeudor> lstTipoDocumentoDeudor = new List<dto.TipoDocumentoDeudor>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Tipos_Documentos_Deudores_Grilla");
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
                        lstTipoDocumentoDeudor.Add(new dto.TipoDocumentoDeudor()
                        {
                            Codemp = Int16.Parse(ds.Tables[0].Rows[i]["Codemp"].ToString()),
                            Id = Int16.Parse(ds.Tables[0].Rows[i]["Id"].ToString()),
                            Nombre = ds.Tables[0].Rows[i]["Nombre"].ToString(),
                            Tipo  = ds.Tables[0].Rows[i]["Tipo"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lstTipoDocumentoDeudor;
        }

        public List<dto.TipoDocumentoDeudor> ExportarExcel(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.TipoDocumentoDeudor> lstTipoDocumentoDeudor = new List<dto.TipoDocumentoDeudor>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Tipos_Documentos_Deudores_Grilla");
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
                        lstTipoDocumentoDeudor.Add(new dto.TipoDocumentoDeudor()
                        {
                            Codemp = Int16.Parse(ds.Tables[0].Rows[i]["Codemp"].ToString()),
                            Id = Int16.Parse(ds.Tables[0].Rows[i]["Id"].ToString()),
                            Nombre = ds.Tables[0].Rows[i]["Nombre"].ToString(),
                            Tipo = ds.Tables[0].Rows[i]["Tipo"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lstTipoDocumentoDeudor;
        }

        public static int ListarTipoDocumentoDeudorGrillaCount(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            int count = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Tipos_Documentos_Deudores_Grilla_Count");
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

        public void InsertarTipoDocumentoDeudor(dto.TipoDocumentoDeudor objAccion, int codemp, int idioma)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Insertar_tipos_documentos_deudores");
                sp.AgregarParametro("codemp", (object)codemp ?? DBNull.Value);
                sp.AgregarParametro("nombre", (object)objAccion.Nombre.ToUpper() ?? DBNull.Value);
                sp.AgregarParametro("idid", (object)idioma ?? DBNull.Value);
                sp.AgregarParametro("tipo", (object)objAccion.Tipo.ToUpper() ?? DBNull.Value);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EditarTipoDocumentoDeudor(dto.TipoDocumentoDeudor objAccion, int codemp, int idioma)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Update_Tipos_Documentos_Deudores");
                sp.AgregarParametro("tdd_codemp", (object)codemp ?? DBNull.Value);
                sp.AgregarParametro("tdd_tddid", (object)objAccion.Id ?? DBNull.Value);
                sp.AgregarParametro("tdd_nombre", (object)objAccion.Nombre.ToUpper() ?? DBNull.Value);
                sp.AgregarParametro("tdd_tipo", (object)objAccion.Tipo.ToUpper() ?? DBNull.Value);
                sp.AgregarParametro("tdi_idid", (object)idioma ?? DBNull.Value);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void BorrarTipoDocumentoDeudor(int codemp, int id)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Delete_Tipos_Documentos_Deudores");
                sp.AgregarParametro("tdd_codemp", codemp);
                sp.AgregarParametro("tdd_tddid", id);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
