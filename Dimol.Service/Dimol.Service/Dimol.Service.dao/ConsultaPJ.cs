using Dimol.dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Service.dao
{
    public class ConsultaPJ
    {
        public static List<dto.ConsultaPJ> ConsultarPorRut(string rut)
        {
            List<dto.ConsultaPJ> lst = new List<dto.ConsultaPJ>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Listar_Causa_Rut");
                sp.AgregarParametro("rut", rut);
                ds = sp.EjecutarProcedimiento();
                Random rnd = new Random();
                int day = rnd.Next(1,29);
                int month = rnd.Next(1,13);

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.ConsultaPJ()
                        {
                            Anio = Int32.Parse(ds.Tables[0].Rows[i]["Anio"].ToString()),
                            Demandado = ds.Tables[0].Rows[i]["Demandado"].ToString(),
                            Demandante = ds.Tables[0].Rows[i]["Demandante"].ToString(),
                            FechaIngreso = string.IsNullOrEmpty( ds.Tables[0].Rows[i]["FechaIngreso"].ToString()) ? new DateTime(Int32.Parse(ds.Tables[0].Rows[i]["Anio"].ToString()),month,day) :  DateTime.Parse(ds.Tables[0].Rows[i]["FechaIngreso"].ToString()),
                            Numero = Int32.Parse(ds.Tables[0].Rows[i]["Numero"].ToString()),
                            Tipo = ds.Tables[0].Rows[i]["Tipo"].ToString(),
                            Tribunal = ds.Tables[0].Rows[i]["Tribunal"].ToString().Trim(),
                            RutaDemanda = ds.Tables[0].Rows[i]["RutaDemanda"].ToString(),
                            Url = ds.Tables[0].Rows[i]["Url"].ToString(),
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
    }
}
