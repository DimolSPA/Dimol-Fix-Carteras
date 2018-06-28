using Dimol.dao;
using System;
using System.Collections.Generic;
using System.Data;

namespace Dimol.Carteras.dao
{
    public class Proceso
    {
        public static List<dto.Proceso> ListarProceso(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.Proceso> lst = new List<dto.Proceso>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Proceso_Grilla");
                sp.AgregarParametro("codemp", codemp);
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
                        lst.Add(new dto.Proceso()
                        {
                            Codemp = codemp,
                            IdProceso = Int32.Parse(ds.Tables[0].Rows[i]["IdProceso"].ToString()),
                            Nombre = ds.Tables[0].Rows[i]["Nombre"].ToString() ?? "",
                            Descripcion = ds.Tables[0].Rows[i]["Descripcion"].ToString() ?? "",
                            Servidor = ds.Tables[0].Rows[i]["Servidor"].ToString() ?? "",
                            //stado = ds.Tables[0].Rows[i]["Estado"].ToString() ?? "",
                            FechaIngreso = DateTime.Parse(ds.Tables[0].Rows[i]["FechaIngreso"].ToString()),
                            UsuarioIngreso = ds.Tables[0].Rows[i]["UsuarioIngreso"].ToString(),
                            UsuarioModificacion = ds.Tables[0].Rows[i]["UsuarioModificacion"].ToString(),
                            FechaModificacion = DateTime.Parse(ds.Tables[0].Rows[i]["FechaModificacion"].ToString()),
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                return lst;
            }
        }

        public static int ListarProcesoCount(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Proceso_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();

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
                return 0; ;
            }
        }
    }
}