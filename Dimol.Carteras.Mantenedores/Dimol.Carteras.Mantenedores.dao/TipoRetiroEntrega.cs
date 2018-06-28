using Dimol.dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Carteras.Mantenedores.dao
{
    public class TipoRetiroEntrega
    {

        public List<dto.TipoRetiroEntrega> ListarTipoRetiroEntregaGrilla(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.TipoRetiroEntrega> lstTipoRetiroEntrega = new List<dto.TipoRetiroEntrega>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Tipo_Retiro_Entrega_Grilla");
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
                        lstTipoRetiroEntrega.Add(new dto.TipoRetiroEntrega()
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
            return lstTipoRetiroEntrega;
        }

        public List<dto.TipoRetiroEntrega> ExportarExcel(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.TipoRetiroEntrega> lstTipoRetiroEntrega = new List<dto.TipoRetiroEntrega>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Tipo_Retiro_Entrega_Grilla");
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
                        lstTipoRetiroEntrega.Add(new dto.TipoRetiroEntrega()
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
            return lstTipoRetiroEntrega;
        }

        public static int ListarTipoRetiroEntregaGrillaCount(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            int count = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Tipo_Retiro_Entrega_Grilla_Count");
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

        public void InsertarTipoRetiroEntrega(dto.TipoRetiroEntrega objAccion, int codemp, int idioma)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Insertar_Tipos_Retiro_Entrega");
                sp.AgregarParametro("codemp", (object)codemp ?? DBNull.Value);
                sp.AgregarParametro("nombre", (object)objAccion.Nombre.ToUpper() ?? DBNull.Value);
                sp.AgregarParametro("idid", (object)idioma ?? DBNull.Value);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void EditarTipoRetiroEntrega(dto.TipoRetiroEntrega objAccion, int codemp, int idioma)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Update_Tipos_Retiro_Entrega");
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

        public void BorrarTipoRetiroEntrega(int codemp, int id)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Delete_Tipos_Retiro_Entrega");
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
