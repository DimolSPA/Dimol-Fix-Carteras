using Dimol.dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Reportes.dao
{
    public class InformeBajas
    {
        public static void TraeTitulo(dto.InformeBajas obj)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_rpt_Trae_Historial_Gestiones_Titulo");
                sp.AgregarParametro("codemp", obj.Codemp);
                sp.AgregarParametro("pclid", obj.Pclid);
                sp.AgregarParametro("ctcid", DBNull.Value);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        obj.Titulo = new dto.TituloReporte
                        {
                            Cliente = ds.Tables[0].Rows[i]["Cliente"].ToString(),
                            Deudor = ds.Tables[0].Rows[i]["Deudor"].ToString(),
                            RutDeudor = ds.Tables[0].Rows[i]["RutDeudor"].ToString()
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
                

        public static void ListarDetalleBajas(dto.InformeBajas obj)
        {
            try
            {                
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Informe_Bajas");
                sp.AgregarParametro("pclid", obj.Pclid);

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    { 
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {                                                    

                            obj.lstBajas.Add(new dto.InformeBajasBruto
                            {
                                FechaReclamo = DateTime.Parse(dr["FECHA_RECLAMO"].ToString()),
                                Rut = dr["RUT"].ToString(),
                                Empresa = dr["EMPRESA"].ToString(),
                                Factura = dr["FACTURA"].ToString(),
                                Monto = decimal.Parse(dr["MONTO"].ToString()),
                                Gestor = dr["GESTOR"].ToString(),
                                FechaPago = DateTime.Parse(dr["FECHA_PAGO"].ToString()),
                                Banco = dr["BANCO"].ToString(),
                                Cuenta = dr["CUENTA"].ToString(),
                                Comentario = dr["COMENTARIO"].ToString(),
                                Historial = dr["HISTORIAL"].ToString()
                            });                        
                        }                                 
                     
                    }
                }
              
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
