using Dimol.dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Caja.dao
{
    public class Reporte
    {
        public static List<dto.ReporteImputacion> ListarReporteImputacion(int codemp, int conciliacionId)
        {
            List<dto.ReporteImputacion> lst = new List<dto.ReporteImputacion>();
            DateTime fechaDoc = new DateTime();
            
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Caja_Reporte_ImputacionGeneral");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("conciliacionId", conciliacionId);

                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        fechaDoc = new DateTime();
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecDoc"].ToString(), out fechaDoc);
                        lst.Add(new dto.ReporteImputacion()
                        {
                            Fecha = fechaDoc,
                            TipoDocumento = ds.Tables[0].Rows[i]["TipoDocumento"].ToString(),
                            Capital = decimal.Parse(ds.Tables[0].Rows[i]["Capital"].ToString()),
                            Interes = decimal.Parse(ds.Tables[0].Rows[i]["Interes"].ToString()),
                            Honorario = decimal.Parse(ds.Tables[0].Rows[i]["Honorario"].ToString()),
                            GastoPreJudicial = decimal.Parse(ds.Tables[0].Rows[i]["GastoPre"].ToString()),
                            GastoJudicial = decimal.Parse(ds.Tables[0].Rows[i]["GastoJud"].ToString())

                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dao.Reporte.ListarReporteImputacion", 0);
                return lst;
            }
        }

        public static dto.ReporteCabecera obtenerCabecera(int codemp, int conciliacionId)
        {
            dto.ReporteCabecera obj = new dto.ReporteCabecera();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Caja_Reporte_ImputacionGeneralCabecera");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("conciliacionId", conciliacionId);
             
                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        obj.NumComprobante = ds.Tables[0].Rows[i]["NumComprobante"].ToString();
                        obj.RutCliente = ds.Tables[0].Rows[i]["RutCliente"].ToString();
                        obj.Cliente = ds.Tables[0].Rows[i]["Cliente"].ToString();
                        obj.RutDeudor = ds.Tables[0].Rows[i]["RutDeudor"].ToString();
                        obj.Deudor = ds.Tables[0].Rows[i]["Deudor"].ToString();
                        obj.Monto = Decimal.Parse(ds.Tables[0].Rows[i]["Monto"].ToString());
                        obj.FecDoc = DateTime.Parse(ds.Tables[0].Rows[i]["FecDoc"].ToString());
                        obj.Gestor = ds.Tables[0].Rows[i]["Gestor"].ToString();
                    }
                }

                return obj;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dto.Reporte.obtenerCabecera", 0);
                return obj;
            }
        }

        public static List<dto.ReporteImputacionDetail> ListarReporteImputacionDetail(int codemp, int conciliacionId)
        {
            List<dto.ReporteImputacionDetail> lst = new List<dto.ReporteImputacionDetail>();
            DateTime fechaDoc = new DateTime();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Caja_Reporte_ImputacionDetalle");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("conciliacionId", conciliacionId);

                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        fechaDoc = new DateTime();
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecDoc"].ToString(), out fechaDoc);
                        lst.Add(new dto.ReporteImputacionDetail()
                        {
                            Fecha = fechaDoc,
                            TipoDocumento = ds.Tables[0].Rows[i]["TipoDocumento"].ToString(),
                            NumDocumento = ds.Tables[0].Rows[i]["Numero"].ToString(),
                            Capital = decimal.Parse(ds.Tables[0].Rows[i]["Capital"].ToString()),
                            Interes = decimal.Parse(ds.Tables[0].Rows[i]["Interes"].ToString()),
                            Honorario = decimal.Parse(ds.Tables[0].Rows[i]["Honorario"].ToString()),
                            GastoPreJudicial = decimal.Parse(ds.Tables[0].Rows[i]["GastoPre"].ToString()),
                            GastoJudicial = decimal.Parse(ds.Tables[0].Rows[i]["GastoJud"].ToString()),
                            Total = decimal.Parse(ds.Tables[0].Rows[i]["Total"].ToString())
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dao.Reporte.ListarReporteImputacionDetail", 0);
                return lst;
            }
        }

        public static List<dto.DocumentoCustodiaReporte> ObtenerDocumentoCustodiaDetail(int codemp, int conciliacionId)
        {
            List<dto.DocumentoCustodiaReporte> lst = new List<dto.DocumentoCustodiaReporte>();
            DateTime fechaDoc = new DateTime();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Obtener_Documentos_Custodia");
                sp.AgregarParametro("conciliacionId", conciliacionId);

                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        fechaDoc = new DateTime();
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecDoc"].ToString(), out fechaDoc);
                        lst.Add(new dto.DocumentoCustodiaReporte()
                        {
                            FechaDoc = fechaDoc,
                            NumDoc = ds.Tables[0].Rows[i]["NumDoc"].ToString(),
                            MontoDoc = decimal.Parse(ds.Tables[0].Rows[i]["MontoDoc"].ToString()),
                            Banco = ds.Tables[0].Rows[i]["Banco"].ToString(),
                            GiradoA = ds.Tables[0].Rows[i]["GiradoA"].ToString()                         
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dao.Reporte.ListarReporteImputacionDetail", 0);
                return lst;
            }
        }
    }
}
