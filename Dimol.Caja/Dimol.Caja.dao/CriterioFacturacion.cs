using Dimol.dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Caja.dao
{
    public class CriterioFacturacion
    {
        public static List<dto.CriterioFacturacion> ListarCriteriosFacturacionGrilla(int codemp, string where, string sidx, string sord)
        {
            List<dto.CriterioFacturacion> lst = new List<dto.CriterioFacturacion>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_CriteriosFacturacion_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);

                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.CriterioFacturacion()
                        {
                            Pclid = Int32.Parse(ds.Tables[0].Rows[i]["Pclid"].ToString()),
                            Cliente = ds.Tables[0].Rows[i]["Cliente"].ToString(),
                            CondicionId = string.IsNullOrEmpty(ds.Tables[0].Rows[i]["CondicionId"].ToString()) ? (int?)null : int.Parse(ds.Tables[0].Rows[i]["CondicionId"].ToString()),
                            Descripcion = ds.Tables[0].Rows[i]["Descripcion"].ToString(),
                            NoAplicaFactura = ds.Tables[0].Rows[i]["NoAplicaFactura"].ToString() == "S"? true : false,
                            AplicaAprobacion = ds.Tables[0].Rows[i]["AplicaAprobacion"].ToString() == "S" ? true : false,
                            Imputable = ds.Tables[0].Rows[i]["Imputable"].ToString() == "S" ? true : false,
                            Condicion = ds.Tables[0].Rows[i]["Condicion"].ToString(),
                            AplicaRemesa = ds.Tables[0].Rows[i]["AplicaRemesa"].ToString() == "S" ? true : false,
                            AplicaCriterio = ds.Tables[0].Rows[i]["AplicaCriterio"].ToString() == "S" ? true : false,
                            SimboloId = string.IsNullOrEmpty(ds.Tables[0].Rows[i]["SimboloId"].ToString()) ? (int?)null : int.Parse(ds.Tables[0].Rows[i]["SimboloId"].ToString()),
                            CriterioAplicaSimbolo = ds.Tables[0].Rows[i]["CriterioAplicaSimbolo"].ToString(),
                            ValorCriterio = string.IsNullOrEmpty(ds.Tables[0].Rows[i]["ValorCriterio"].ToString()) ? (int?)null : int.Parse(ds.Tables[0].Rows[i]["ValorCriterio"].ToString()),
                            Row = Int32.Parse(ds.Tables[0].Rows[i]["row"].ToString())

                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dao.CriterioFacturacion.ListarCriteriosFacturacionGrilla", 0);
                return lst;
            }
        }
    }
}
