using Dimol.dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Reportes.dao
{
    public class RecepcionDocumentos
    {
        public static void TraeTitulo(dto.RecepcionDocumentos obj)
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
        public static void ListarDocumentosDetalle(dto.RecepcionDocumentos obj)
        {
            try
            {
                List<dto.RecepcionDocumentosDetalle> lstBruto = new List<dto.RecepcionDocumentosDetalle>();

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Cartera_Clientes_CpbtDoc_Ingresos");
                sp.AgregarParametro("ccb_codemp", obj.Codemp);
                sp.AgregarParametro("ccb_pclid", obj.Pclid);
                //sp.AgregarParametro("ccb_ctcid", obj.Ctcid);
                //sp.AgregarParametro("ccb_estcpbt", obj.Estcpbt);
                sp.AgregarParametro("idi_idid", obj.Idioma);
                sp.AgregarParametro("desde", obj.FechaDesde);
                sp.AgregarParametro("hasta", obj.FechaHasta);
                sp.AgregarParametro("ccb_tipcart", obj.TipoCartera); 

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lstBruto.Add(new dto.RecepcionDocumentosDetalle
                        {
                            RutCliente = Dimol.bcp.Funciones.formatearRut( ds.Tables[0].Rows[i]["pcl_rut"].ToString()),
                            NombreCliente = ds.Tables[0].Rows[i]["pcl_nombre"].ToString(),
                            RutDeudor = Int32.Parse(ds.Tables[0].Rows[i]["ctc_numero"].ToString()),
                            DvDeudor = ds.Tables[0].Rows[i]["ctc_digito"].ToString(),
                            RutDeudorFormateado= Dimol.bcp.Funciones.formatearRut( ds.Tables[0].Rows[i]["ctc_numero"].ToString() + ds.Tables[0].Rows[i]["ctc_digito"].ToString()),
                            //NombreFantasia = ds.Tables[0].Rows[i]["ctc_nomfant"].ToString(),
                            TipoDocumento = ds.Tables[0].Rows[i]["tci_nombre"].ToString(),
                            Numero = ds.Tables[0].Rows[i]["ccb_numero"].ToString(),
                            FechaDocumento = DateTime.Parse(ds.Tables[0].Rows[i]["ccb_fecdoc"].ToString()),
                            FechaVencimiento = DateTime.Parse(ds.Tables[0].Rows[i]["ccb_fecvenc"].ToString()),
                            Moneda = ds.Tables[0].Rows[i]["mon_nombre"].ToString(),
                            Monto = decimal.Parse(ds.Tables[0].Rows[i]["ccb_monto"].ToString()),
                            Asignado = decimal.Parse(ds.Tables[0].Rows[i]["ccb_asignado"].ToString()),
                            RutSubCartera = Dimol.bcp.Funciones.formatearRut( ds.Tables[0].Rows[i]["sbc_rut"].ToString()),
                            SubCartera = ds.Tables[0].Rows[i]["sbc_nombre"].ToString(),
                            CodigoCarga = ds.Tables[0].Rows[i]["pcc_nombre"].ToString(),
                            MotivoCobranza = ds.Tables[0].Rows[i]["mci_nombre"].ToString(),
                            DocumentoOriginal = ds.Tables[0].Rows[i]["ccb_docori"].ToString(),
                            Saldo = decimal.Parse(ds.Tables[0].Rows[i]["ccb_saldo"].ToString()),
                            Diferencia = decimal.Parse(ds.Tables[0].Rows[i]["dif"].ToString()),
                            FechaIngreso = DateTime.Parse(ds.Tables[0].Rows[i]["ccb_fecing"].ToString())
                        });
                    }

                    List<string> agrupaFecIng = lstBruto.OrderBy(o=> o.FechaIngresoStr).Select(o => o.FechaIngresoStr).Distinct().ToList();

                    Dimol.dto.Indicadores objInd = new Dimol.dto.Indicadores();
                    Funciones.TraeDolarUFHoy(obj.Codemp, objInd);

                    foreach (var item in agrupaFecIng)
                    {
                        dto.RecepcionDocumentosPorIngreso objIngr = new dto.RecepcionDocumentosPorIngreso();

                        objIngr.lstDocumentos = lstBruto.Where(o => item == o.FechaIngresoStr).ToList();
                        objIngr.FechaIngresoStr = item;
                        //objIngr.Totales.Total = (from od in objIngr.lstDocumentos
                        //select od.Asignado).Sum();
                        objIngr.Totales.Total = objIngr.lstDocumentos.Where(o => o.Moneda == "PESOS").Select(o => o.Asignado).Sum();
                        objIngr.Totales.Total += objIngr.lstDocumentos.Where(o => o.Moneda == "UF").Select(o => o.Asignado * objInd.UF).Sum();
                        objIngr.Totales.Total += objIngr.lstDocumentos.Where(o => o.Moneda == "DOLAR").Select(o => o.Asignado * objInd.DolarObservado).Sum();

                        obj.lstDocsPorIngr.Add(objIngr);
                    }
                    
                }
                //obj.Totales.Total =  (from od in obj.lstDocumentos
                 //                    select od.Asignado).Sum();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
