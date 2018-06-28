using Dimol.dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Reportes.dao
{
    public class CastigoMasivo
    {
        public static List<dto.CastigoMasivo> ListarRutHtml()
        {
            List<dto.CastigoMasivo> lst = new List<dto.CastigoMasivo>();
            DateTime fecha = new DateTime();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Listar_Rut_Html");

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.CastigoMasivo()
                        {
                            Rut = Int32.Parse(ds.Tables[0].Rows[i]["rut"].ToString()),
                            Dv = ds.Tables[0].Rows[i]["dv"].ToString(),
                            Nombre = ds.Tables[0].Rows[i]["nombre"].ToString(),
                            Html = ds.Tables[0].Rows[i]["Html"].ToString(),
                            Ctcid = Int32.Parse(ds.Tables[0].Rows[i]["ctcid"].ToString()),
                            Numero = Int32.Parse(ds.Tables[0].Rows[i]["numero"].ToString()),
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Listar_Rut_Html: ", 0);
                return lst;
            }
        }

        public static List<dto.CastigoMasivo> ListarRutHtmlComplementaria()
        {
            List<dto.CastigoMasivo> lst = new List<dto.CastigoMasivo>();
            DateTime fecha = new DateTime();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Listar_Rut_Html_Complementaria");

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.CastigoMasivo()
                        {
                            Rut = Int32.Parse(ds.Tables[0].Rows[i]["rut"].ToString()),
                            Dv = ds.Tables[0].Rows[i]["dv"].ToString(),
                            Nombre = ds.Tables[0].Rows[i]["nombre"].ToString(),
                            Html = ds.Tables[0].Rows[i]["Html"].ToString(),
                            Ctcid = Int32.Parse(ds.Tables[0].Rows[i]["ctcid"].ToString()),
                            Numero = Int32.Parse(ds.Tables[0].Rows[i]["numero"].ToString()),
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Listar_Rut_Html: ", 0);
                return lst;
            }
        }

        public static List<dto.CastigoMasivo> ListarRutHtmlComplementariaMarzo2018()
        {
            List<dto.CastigoMasivo> lst = new List<dto.CastigoMasivo>();
            DateTime fecha = new DateTime();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Listar_Rut_Html_Complementaria_Marzo_2018");

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.CastigoMasivo()
                        {
                            Rut = Int32.Parse(ds.Tables[0].Rows[i]["rut"].ToString()),
                            Dv = ds.Tables[0].Rows[i]["dv"].ToString(),
                            Nombre = ds.Tables[0].Rows[i]["nombre"].ToString(),
                            Html = ds.Tables[0].Rows[i]["Html"].ToString(),
                            Ctcid = Int32.Parse(ds.Tables[0].Rows[i]["ctcid"].ToString()),
                            Numero = Int32.Parse(ds.Tables[0].Rows[i]["numero"].ToString()),
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Listar_Rut_Html: ", 0);
                return lst;
            }
        }
    }
}
