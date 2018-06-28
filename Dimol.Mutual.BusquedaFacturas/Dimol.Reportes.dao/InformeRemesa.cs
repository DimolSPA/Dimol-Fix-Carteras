using Dimol.dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Reportes.dao
{
    public class InformeRemesa
    {
        public static void TraeTitulo(dto.InformeRemesa obj)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_rpt_Trae_Historial_Gestiones_Titulo");
                sp.AgregarParametro("codemp", obj.Codemp);
                sp.AgregarParametro("pclid", DBNull.Value);
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
        public static void ListarDocumentosDetalle(dto.InformeRemesa obj)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Reporte_Inf_Remesa");
                sp.AgregarParametro("dcc_codemp", obj.Codemp);
                sp.AgregarParametro("dcc_sucid", obj.Sucid);
                sp.AgregarParametro("idi_idid", obj.Idioma);
                sp.AgregarParametro("dcc_tpcid", obj.TipoDocumento);
                sp.AgregarParametro("dcc_numero", obj.Numero);
                ds = sp.EjecutarProcedimiento();

                decimal tipoCambio;
                decimal capital;
                decimal interes;
                decimal honorario;
                decimal gastos;
                decimal recuperado;
                decimal comision;
                decimal uf;

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["cbc_feccpbt"].ToString() != "")
                        {
                            obj.NumeroRemesa = ds.Tables[0].Rows[0]["cbc_numprovcli"].ToString().PadLeft(10, '0');
                        }
                        else 
                        {
                            obj.NumeroRemesa = ds.Tables[0].Rows[0]["dcc_numero"].ToString().PadLeft(10, '0');
                        }
                        obj.RutCliente = Dimol.bcp.Funciones.formatearRut(ds.Tables[0].Rows[0]["pcl_rut"].ToString());
                        obj.NombreCliente = ds.Tables[0].Rows[0]["pcl_nombre"].ToString();
                        obj.FechaEmisionCortaStr = ds.Tables[0].Rows[0]["cbc_feccpbt"].ToString();
                        
                    }

                    var listaDeudores = ds.Tables[0].AsEnumerable()
                        .Select(row => new
                        {
                            NombreDeudor = row.Field<string>("ctc_nomfant"),
                            RutDeudor = row.Field<decimal>("ctc_numero"),
                            DVDeudor = row.Field<string>("ctc_digito"),
                            NombreSubcartera = row.Field<string>("sbc_nombre"),
                            RutSubCartera = row.Field<string>("sbc_rut")
                        })
                        .Distinct();

                    foreach (var deudor in listaDeudores) 
                    {
                        dto.InformeRemesaDeudor objDeudor = new dto.InformeRemesaDeudor
                        {
                            NombreFantasia = deudor.NombreDeudor,
                            RutDeudor = (int)deudor.RutDeudor,
                            DvDeudor = deudor.DVDeudor,
                            RutDeudorFormateado = Dimol.bcp.Funciones.formatearRut((int)deudor.RutDeudor + deudor.DVDeudor),
                            SubCartera = deudor.NombreSubcartera,
                            RutSubCartera = Dimol.bcp.Funciones.formatearRut(deudor.RutSubCartera)
                        };

                        var result = ds.Tables[0].AsEnumerable().Where( dr => dr.Field<decimal>( "ctc_numero" ) == deudor.RutDeudor && dr.Field<string>( "sbc_rut" ) == deudor.RutSubCartera );

                        foreach(DataRow dr in result)
                        {
                            dto.InformeRemesaDetalle objNew = new dto.InformeRemesaDetalle();
                            tipoCambio = decimal.Parse(dr["ddi_tipcambio"].ToString());
                            capital = decimal.Parse(dr["api_capital"].ToString()) * tipoCambio;
                            interes = decimal.Parse(dr["api_interes"].ToString()) * tipoCambio;
                            honorario = decimal.Parse(dr["api_honorario"].ToString()) * tipoCambio;
                            gastos = (decimal.Parse(dr["api_gastpre"].ToString()) + decimal.Parse(dr["api_gastjud"].ToString())) * tipoCambio;
                            recuperado = capital + interes + honorario + gastos;
                            uf = decimal.Parse(dr["dcc_porcfact"].ToString());
                            comision = recuperado * uf;

                            objNew.TipoDocumento = dr["tci_nombre"].ToString();
                            objNew.Numero = Int32.Parse(dr["ccb_numero"].ToString());
                            objNew.FechaPago = DateTime.Parse(dr["apl_fecapl"].ToString());

                            objNew.Capital = capital;
                            objNew.Interes = interes;
                            objNew.Honorario = honorario;
                            objNew.Gastos = gastos;
                            objNew.Recuperado = recuperado;
                            objNew.UF = uf * 100;
                            objNew.Comision = comision;

                            objNew.Negocio = dr["ccb_numesp"].ToString();
                            objNew.Comentario = dr["cbc_glosa"].ToString();
                            objDeudor.lstDetalles.Add(objNew);
                        }
                        objDeudor.Totales.Capital = (from od in objDeudor.lstDetalles
                                               select od.Capital).Sum();
                        objDeudor.Totales.Interes = (from od in objDeudor.lstDetalles
                                               select od.Interes).Sum();
                        objDeudor.Totales.Honorario = (from od in objDeudor.lstDetalles
                                                 select od.Honorario).Sum();
                        objDeudor.Totales.Gastos = (from od in objDeudor.lstDetalles
                                              select od.Gastos).Sum();
                        objDeudor.Totales.Recuperado = (from od in objDeudor.lstDetalles
                                                        select od.Recuperado).Sum();
                        objDeudor.Totales.Comision = (from od in objDeudor.lstDetalles
                                                select od.Comision).Sum();


                        obj.lstDocumentos.Add(objDeudor);

                        obj.Totales.Capital = obj.Totales.Capital + (from od in objDeudor.lstDetalles
                                                     select od.Capital).Sum();
                        obj.Totales.Interes = obj.Totales.Interes+(from od in objDeudor.lstDetalles
                                                     select od.Interes).Sum();
                        obj.Totales.Honorario = obj.Totales.Honorario+(from od in objDeudor.lstDetalles
                                                       select od.Honorario).Sum();
                        obj.Totales.Gastos = obj.Totales.Gastos+(from od in objDeudor.lstDetalles
                                                    select od.Gastos).Sum();
                        obj.Totales.Recuperado = obj.Totales.Recuperado+(from od in objDeudor.lstDetalles
                                                        select od.Recuperado).Sum();
                        obj.Totales.Comision = obj.Totales.Comision+(from od in objDeudor.lstDetalles
                                                      select od.Comision).Sum();
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
