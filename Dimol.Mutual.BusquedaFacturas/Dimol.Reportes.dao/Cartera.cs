using Dimol.dao;
using Dimol.dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Reportes.dao
{
    public class Cartera
    {
        public static List<Combobox> ListarReportes(int modulo, int idioma, string first)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Reportes_Modulo");
                sp.AgregarParametro("rpt_trvid", modulo);
                sp.AgregarParametro("tpt_tipo", "C");
                sp.AgregarParametro("rti_idid", idioma);
                ds = sp.EjecutarProcedimiento();

                lst.Add(new Combobox()
                {
                    Value = "",
                    Text = first
                });

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Combobox()
                        {
                            Value = ds.Tables[0].Rows[i]["rti_rptid"].ToString(),
                            Text = ds.Tables[0].Rows[i]["rti_nombre"].ToString()
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

        public static string RutaReportes(int pagina, int reporte, int idioma)
        {
            string ruta = "";
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Ruta_Reporte");
                sp.AgregarParametro("pagina", pagina);
                sp.AgregarParametro("reporte", reporte);
                sp.AgregarParametro("idioma", idioma);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    if( ds.Tables[0].Rows.Count>0)
                    {
                        ruta = ds.Tables[0].Rows[0]["ruta"].ToString();
                    }
                }
                return ruta;
            }
            catch (Exception ex)
            {
                return ruta;
            }

        }
    }
}
