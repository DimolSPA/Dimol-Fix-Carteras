using Dimol.dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Dimol.Reportes.dao
{
    public class CastigoPrejudicialManual
    {
        public static void TraeTitulo(dto.CastigoPrejudicialManualCliente obj)
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

        public static void TraeSucursalCliente(dto.CastigoPrejudicialManualCliente obj)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_ProvCli_Sucursal");
                sp.AgregarParametro("pcs_codemp", obj.Codemp);
                sp.AgregarParametro("pcs_pclid", obj.Pclid);
                sp.AgregarParametro("pcs_pcsid", obj.Pcsid);
                ds = sp.EjecutarProcedimiento();

                /*obj.SucursalCliente = new dto.CabeceraSucursalCliente
                {
                    Direccion = "AV. CENTRAL 700",
                    Comuna = "QUILICURA",
                    Ciudad = "SANTIAGO"
                };*/

                if (ds.Tables.Count > 0)
                 {
                     for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                     {
                         obj.SucursalCliente = new dto.CabeceraSucursalCliente
                         {
                             Direccion = ds.Tables[0].Rows[i]["pcs_direccion"].ToString(),   //"EL GOLF 99",
                             Comuna = ds.Tables[0].Rows[i]["com_nombre"].ToString(),   //"LAS CONDES",
                             Ciudad = ds.Tables[0].Rows[i]["ciu_nombre"].ToString()   //"SANTIAGO"
                         };
                     }
                 }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void ListarCastigosDetalle(dto.CastigoPrejudicialManualCliente obj, List<dto.CastigoPrejudicialManualCliente> lstDevDocs)
        {
            try
            {
                List<dto.CastigoPrejudicialManualBruto> lstBruto = new List<dto.CastigoPrejudicialManualBruto>();

                DataSet ds = new DataSet();
                //StoredProcedure sp = new StoredProcedure("_Trae_Reporte_Devolucion_Documentos_Deudor");
                StoredProcedure sp = new StoredProcedure("_Trae_Reporte_Torta_Generica2");
                sp.AgregarParametro("cbc_codemp", obj.Codemp);
                sp.AgregarParametro("ccb_pclid", obj.CcbPclid);
                sp.AgregarParametro("ccb_estcpbt", obj.CcbEstcpbt);
                //sp.AgregarParametro("pcc_codigo", obj.PccCodigo);
                /* sp.AgregarParametro("cbc_sucid", obj.Codsuc);
                 sp.AgregarParametro("cbc_tpcid", obj.Tpcid);
                 sp.AgregarParametro("cbc_numero", obj.Cbcnumero);*/
                sp.AgregarParametro("idi_idid", obj.Idioma);


                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    { 
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            obj.FechaComprobante = DateTime.Now; //DateTime.Parse("11-05-2017");
                            //obj.FechaComprobante = DateTime.Parse(dr["cbc_feccpbt"].ToString());
                            obj.RutUsuario = Dimol.bcp.Funciones.formatearRut(dr["pcl_rut"].ToString()); //"96.670.840-9";//"87.670.500-1";//obj.RutUsuario = "87.747.300-7";
                            obj.NombreUsuario = dr["pcl_nomfant"].ToString();  //"DIMERC S.A.";//"SOCOMISCH Y CIA. LTDA."; //obj.NombreUsuario = "ANDINA DEL SUD LTDA.";
                            obj.Pclid = Int32.Parse(dr["ccb_pclid"].ToString());
                            //obj.Pcsid = Int32.Parse(dr["cbc_pcsid"].ToString());

                            lstBruto.Add(new dto.CastigoPrejudicialManualBruto
                            {
                                CtcRut = Dimol.bcp.Funciones.formatearRut(dr["ctc_rut"].ToString()),
                                CtcNombre = dr["ctc_nomfant"].ToString(),
                                TipoDoc = dr["tip_doc"].ToString(),
                                NroDoc = dr["ccb_numero"].ToString(),
                                Monto = dr["ccb_monto"].ToString(),
                                Saldo = dr["ccb_saldo"].ToString(),
                                FechaEmision = DateTime.Parse(dr["ccb_fecdoc"].ToString()),
                                FechaVcto = DateTime.Parse(dr["ccb_fecvenc"].ToString()),
                                Negocio = dr["ccb_numesp"].ToString(),
                                Moneda = dr["mon_nombre"].ToString(),
                                DetalleCtcId = Int32.Parse(dr["ccb_ctcid"].ToString()),
                               // Ori = dr["ccb_docori"].ToString(),
                               // Ant = dr["ccb_docant"].ToString(),
                                NombreAsegurado = dr["sbc_nombre"].ToString(),
                                Direccion = dr["ctc_direccion"].ToString(),
                                Comuna = dr["com_nombre"].ToString(),
                                Ciudad = dr["ciu_nombre"].ToString()
                            });                        
                        }

                        TraeSucursalCliente(obj);

                        List<string> lstdeudores = lstBruto.Select(o => o.CtcRut).Distinct().ToList();
                        int ctcid = 0;
                        int numReg = 0;

                        foreach (string rut in lstdeudores)
                        {
                            dto.CastigoPrejudicialManualDeudor objDeu = new dto.CastigoPrejudicialManualDeudor();
                            
                            numReg++;         
                                          
                            foreach (var item in lstBruto.Where(o =>o.CtcRut == rut))
                            {
                                objDeu.lstDocs.Add(new dto.CastigoPrejudicialManualDetalle
                                {
                                    TipoDocumento = item.TipoDoc,
                                    NroDocumento = item.NroDoc,
                                    DeudorCtcId = item.DetalleCtcId,
                                    FechaEmision = item.FechaEmision,
                                    FechaVcto = item.FechaVcto,
                                    Moneda = item.Moneda,
                                    Monto = decimal.Parse(item.Monto),
                                    Saldo = decimal.Parse(item.Saldo),
                                    Negocio = item.Negocio,
                                    DocOrigen = item.Ori,
                                    DocCantidad = item.Ant
                                });
                                objDeu.NombreDeudor = item.CtcNombre;
                                ctcid = item.DetalleCtcId;
                                objDeu.NombreAsegurado = item.NombreAsegurado;
                                objDeu.Direccion = item.Direccion;
                                objDeu.Comuna = item.Comuna;
                                objDeu.Ciudad = item.Ciudad;
                            }
                            
                            objDeu.RutDeudor = Dimol.bcp.Funciones.formatearRut(rut);
                            
                            objDeu.Totales.Total = lstBruto.Where(o => o.CtcRut == rut).Select(o => decimal.Parse(o.Saldo)).Sum();
                            objDeu.Totales.Cantidad = lstBruto.Where(o => o.CtcRut == rut).Count();
                            
                            try
                            {
                                DataSet dst = new DataSet();
                                StoredProcedure spr = new StoredProcedure("Trae_Reporte_Cabecera_Comprobantes_Motivos");
                                spr.AgregarParametro("cbm_codemp", obj.Codemp);
                                spr.AgregarParametro("cbm_sucid", obj.Codsuc);
                                spr.AgregarParametro("cbm_tpcid", obj.Tpcid);
                                spr.AgregarParametro("cbm_numero", obj.Cbcnumero);
                                spr.AgregarParametro("tmi_idid", obj.Idioma);
                                spr.AgregarParametro("cbm_ctcid", ctcid);
                                dst = spr.EjecutarProcedimiento();

                                if (dst.Tables.Count > 0)
                                {
                                    for (int i = 0; i < dst.Tables[0].Rows.Count; i++)
                                    {
                                        objDeu.lstMotivos.Add(new dto.CabeceraMotivoCastigo
                                        {
                                            Motivo = dst.Tables[0].Rows[i]["tmi_nombre"].ToString()
                                        });

                                    }
                                }

                              /*  if(rut == "76.773.020-9")
                                {
                                    objDeu.lstMotivos.Add(new dto.CabeceraMotivoCastigo
                                    {
                                        Motivo = "SIN INTENCION DE PAGO"
                                    });
                                }
                                else
                                { */
                                    objDeu.lstMotivos.Add(new dto.CabeceraMotivoCastigo
                                    {
                                        Motivo = "ANTIECONOMICO DEMANDAR"
                                    });

                                    objDeu.lstMotivos.Add(new dto.CabeceraMotivoCastigo
                                    {
                                        Motivo = "INUBICABLE"
                                    });
                               // }
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                                                       
                            lstDevDocs.Add(new dto.CastigoPrejudicialManualCliente
                            {
                                Deudor = objDeu,
                                FechaComprobante = obj.FechaComprobante,
                                RutUsuario = obj.RutUsuario,
                                NombreUsuario = obj.NombreUsuario,
                                Pclid = obj.Pclid,
                                Pcsid = obj.Pcsid,
                                NumRegistro = numReg,
                                Codemp = obj.Codemp,
                                Codsuc = obj.Codsuc,
                                Tpcid = obj.Tpcid,
                                Cbcnumero = obj.Cbcnumero,
                                Empresa = obj.Empresa,
                                Idioma = obj.Idioma,
                                FechaReporte = obj.FechaReporte,
                                Encabezado = obj.Encabezado,
                                Titulo = obj.Titulo,
                                SucursalCliente = obj.SucursalCliente
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
