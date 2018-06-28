using Dimol.dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Caja.dao
{
    public class CriterioImputacion
    {
        public static List<dto.CriterioImputacion> ListarCriteriosImputacionGrilla(int codemp, string where, string sidx, string sord)
        {
            List<dto.CriterioImputacion> lst = new List<dto.CriterioImputacion>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_CriteriosImputacion_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);

                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.CriterioImputacion()
                        {
                            Pclid = Int32.Parse(ds.Tables[0].Rows[i]["Pclid"].ToString()),
                            Cliente = ds.Tables[0].Rows[i]["Cliente"].ToString(),
                            Capital = int.Parse(ds.Tables[0].Rows[i]["Capital"].ToString()),
                            Interes = int.Parse(ds.Tables[0].Rows[i]["Interes"].ToString()),
                            Honorario = int.Parse(ds.Tables[0].Rows[i]["Honorario"].ToString()),
                            Row = Int32.Parse(ds.Tables[0].Rows[i]["row"].ToString())

                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dao.CriterioImputacion.ListarCriterioRemesaClienteGrilla", 0);
                return lst;
            }
        }
    }
}
