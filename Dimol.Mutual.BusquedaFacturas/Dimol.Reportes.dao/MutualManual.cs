using Dimol.dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Reportes.dao
{
    public class MutualManual
    {
        public static void TraeTitulo(dto.ResumenGestiones obj)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_rpt_Trae_Historial_Gestiones_Titulo");
                sp.AgregarParametro("codemp", obj.Codemp);
                sp.AgregarParametro("pclid", obj.Pclid);
                sp.AgregarParametro("ctcid", obj.Ctcid);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        obj.Titulo = new dto.TituloReporte
                        {
                            Cliente = ds.Tables[0].Rows[i]["Cliente"].ToString(),
                            Deudor = ds.Tables[0].Rows[i]["Deudor"].ToString(),
                            RutDeudor =Dimol.bcp.Funciones.formatearRut(  ds.Tables[0].Rows[i]["RutDeudor"].ToString())
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void ListarDocumentosDetalle(dto.MutualManual obj)
        {
            try
            {
                List<dto.MutualManualBruto> lstBruto = new List<dto.MutualManualBruto>();

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Reporte_Mutual_Manual");
           
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lstBruto.Add(new dto.MutualManualBruto
                        {
                            Rut = ds.Tables[0].Rows[i]["rut"].ToString() + "-" + ds.Tables[0].Rows[i]["dv"].ToString(),
                            Nombre = ds.Tables[0].Rows[i]["Nombre"].ToString(),
                            Factura = ds.Tables[0].Rows[i]["Numero"].ToString(),
                            Fecha = DateTime.Parse(ds.Tables[0].Rows[i]["FECHA_DOC"].ToString()),
                            Prestacion = ds.Tables[0].Rows[i]["prestacion"].ToString(),
                            Agencia = ds.Tables[0].Rows[i]["agencia"].ToString(),
                            Monto = decimal.Parse(ds.Tables[0].Rows[i]["Saldo"].ToString())
                        });
                    }

                    List<string> clientes = lstBruto.Select(o => o.Rut).Distinct().ToList();

                    foreach (var item in clientes)
                    {
                        dto.MutualManualCliente objCli = new dto.MutualManualCliente();
                        
                        foreach (var det in lstBruto.Where(o => o.Rut == item))
                        {
                            objCli.lstDetalle.Add(new dto.MutualManualDetalle
                            {
                                Factura = det.Factura,
                                Fecha = det.Fecha,
                                Prestacion = det.Prestacion,
                                Agencia = det.Agencia,
                                Monto = det.Monto
                            });
                            objCli.Rut = det.Rut;
                            objCli.Nombre = det.Nombre;
                        }
                        objCli.Totales.Total = lstBruto.Where(o => o.Rut == item).Select(o => o.Monto).Sum();
                        obj.lstCli.Add(objCli);
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
