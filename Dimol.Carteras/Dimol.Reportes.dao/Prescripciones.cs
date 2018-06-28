using Dimol.dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Reportes.dao
{
    public class Prescripciones
    {
        public static void TraeTitulo(dto.Prescripciones obj)
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
                

        public static void ListarDetallePrescripciones(dto.Prescripciones obj)
        {
            try
            {                
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Prescripciones");
                sp.AgregarParametro("rol_codemp", obj.Codemp);
                sp.AgregarParametro("diasprescr", obj.DiasPrescrip);

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    { 
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {                                                    

                            obj.lstPrescr.Add(new dto.PrescripcionesBruto
                            {
                                RutCliente = Dimol.bcp.Funciones.formatearRut(dr["RutCli"].ToString()),
                                NombreCliente = dr["NomCli"].ToString(),
                                RutDeudor = Dimol.bcp.Funciones.formatearRut(dr["RutDeu"].ToString()),
                                NombreDeudor = dr["NomDeu"].ToString(),
                                Rol = dr["Rol"].ToString(),
                                Tribunal = dr["Tribunal"].ToString(),
                                TipoCausa = dr["TipCausa"].ToString(),
                                FechaJudicial = DateTime.Parse(dr["FecJud"].ToString()),
                                MateriaJudicial = dr["MatJud"].ToString(),
                                EstadoRol = dr["EstRol"].ToString(),
                                Numero = dr["Numero"].ToString(),
                                FechaVencimiento = DateTime.Parse(dr["FecVenc"].ToString()),
                                FechaPrescripcion = DateTime.Parse(dr["FecPresc"].ToString()),
                                Demandado = decimal.Parse(dr["Demandado"].ToString()),
                                Saldo = decimal.Parse(dr["Saldo"].ToString()),
                                CodigoCarga = dr["CodCarg"].ToString(),
                                Asegurado = dr["Asegurado"].ToString(),
                                Abogado = dr["Abogado"].ToString()
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
