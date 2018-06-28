using Dimol.dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Carteras.Mantenedores.dao
{
    public class GrupoCobranza
    {
        public string ListarEmpleadosGrupoCobranza(int codEmp)
        {
            string salida = "";
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Nombre_Empleados_Todos");
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

        public List<dto.GrupoCobranza> ListarGrupoCobranzaGrilla(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite, int idsuc)
        {
            List<dto.GrupoCobranza> lstGrupoCobranza = new List<dto.GrupoCobranza>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Grupos_Cobranzas_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idid", idioma);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                sp.AgregarParametro("idsuc", idsuc);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lstGrupoCobranza.Add(new dto.GrupoCobranza()
                        {
                            Codemp = Int16.Parse(ds.Tables[0].Rows[i]["Codemp"].ToString()),
                            CodSucursal = Int16.Parse(ds.Tables[0].Rows[i]["CodSucursal"].ToString()),
                            Id = Int16.Parse(ds.Tables[0].Rows[i]["Id"].ToString()),
                            Nombre = ds.Tables[0].Rows[i]["Nombre"].ToString(),
                            CodEmpleado = ds.Tables[0].Rows[i]["CodEmpleado"].ToString(),
                            NombreEmpleado = ds.Tables[0].Rows[i]["NombreEmpleado"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lstGrupoCobranza;
        }

        public List<dto.GrupoCobranza> ExportarExcel(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite, int idsuc)
        {
            List<dto.GrupoCobranza> lstGrupoCobranza = new List<dto.GrupoCobranza>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Grupos_Cobranzas_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idid", idioma);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                sp.AgregarParametro("idsuc", idsuc);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lstGrupoCobranza.Add(new dto.GrupoCobranza()
                        {
                            Codemp = Int16.Parse(ds.Tables[0].Rows[i]["Codemp"].ToString()),
                            CodSucursal = Int16.Parse(ds.Tables[0].Rows[i]["CodSucursal"].ToString()),
                            Id = Int16.Parse(ds.Tables[0].Rows[i]["Id"].ToString()),
                            Nombre = ds.Tables[0].Rows[i]["Nombre"].ToString(),
                            CodEmpleado = ds.Tables[0].Rows[i]["CodEmpleado"].ToString(),
                            NombreEmpleado = ds.Tables[0].Rows[i]["NombreEmpleado"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lstGrupoCobranza;
        }

        public static int ListarGrupoCobranzaGrillaCount(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite, int idsuc)
        {
            int count = 0;
            List<dto.GrupoCobranza> lstGrupoCobranza = new List<dto.GrupoCobranza>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Grupos_Cobranzas_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idid", idioma);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                sp.AgregarParametro("idsuc", idsuc);
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

        public void InsertarGrupoCobranza(dto.GrupoCobranza objAccion, int codemp, int idsuc)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Insertar_Grupos_Cobranzas");
                sp.AgregarParametro("codemp", (object)codemp ?? DBNull.Value);
                sp.AgregarParametro("sucid", (object)idsuc ?? DBNull.Value);
                sp.AgregarParametro("nombre", (object)objAccion.Nombre.ToUpper() ?? DBNull.Value);
                sp.AgregarParametro("emplid", (object)objAccion.NombreEmpleado ?? DBNull.Value);
                int error = sp.EjecutarProcedimientoTrans();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EditarGrupoCobranza(dto.GrupoCobranza objAccion, int codemp, int idsuc)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Update_Grupos_Cobranza");
                sp.AgregarParametro("grc_codemp", (object)codemp ?? DBNull.Value);
                sp.AgregarParametro("grc_sucid", (object)idsuc ?? DBNull.Value);
                sp.AgregarParametro("grc_grcid", (object)objAccion.Id ?? DBNull.Value);
                sp.AgregarParametro("grc_nombre", (object)objAccion.Nombre.ToUpper() ?? DBNull.Value);
                sp.AgregarParametro("grc_emplid", (object)objAccion.NombreEmpleado ?? DBNull.Value);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void BorrarGrupoCobranza(int codemp, int? id, int idsuc)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Delete_Grupos_Cobranza");
                sp.AgregarParametro("grc_codemp", codemp);
                sp.AgregarParametro("grc_sucid", idsuc);
                sp.AgregarParametro("grc_grcid", id);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
