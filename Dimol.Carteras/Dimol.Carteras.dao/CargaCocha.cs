﻿using Dimol.Carteras.dto;
using Dimol.dao;
using Dimol.dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Carteras.dao
{
    public class CargaCocha
    {
        public static List<dto.Comprobante> ListarDocumentosCocha(int codemp,int pclid, string ctcid, string numero,int tipo, string estcpbt)
        {
            List<dto.Comprobante> lst = new List<dto.Comprobante>();
            try
            {
                string StrSql = "select [CCB_CODEMP] Codemp,[CCB_PCLID] Pclid,[CCB_CTCID] Ctcid,[CCB_CCBID] Ccbid,[CCB_TPCID] TipoDocumento,[CCB_TIPCART] TipoCartera,[CCB_NUMERO] NumeroCpbt";
                StrSql = StrSql + " ,[CCB_FECING] FechaIngreso,[CCB_FECDOC] FechaDocumento,[CCB_FECVENC] FechaVencimiento,[CCB_FECULTGEST] FechaUltimaGestion,[CCB_FECPLAZO] FechaPlazo";
                StrSql = StrSql + " ,[CCB_FECCALCINT] FechaCalculoInteres,[CCB_FECCAST] FechaCastigo,[CCB_ESTID] EstadoCartera,[CCB_ESTCPBT] EstadoCpbt,[CCB_CODMON] CodigoMoneda";
                StrSql = StrSql + " ,[CCB_TIPCAMBIO] TipoCambio,[CCB_ASIGNADO] MontoAsignado,[CCB_MONTO] Monto,[CCB_SALDO] Saldo,[CCB_GASTJUD] GastoJudicial,[CCB_GASTOTRO] GastoOtros";
                StrSql = StrSql + "  ,[CCB_INTERESES] Intereses,[CCB_HONORARIOS] Honorarios,[CCB_CALCHON] CalculoHonorarios,[CCB_BCOID] NombreBanco,[CCB_RUTGIR] RutGirador";
                StrSql = StrSql + " ,[CCB_NOMGIR] NombreGirador,[CCB_MTCID] MotivoCobranza,[CCB_COMENTARIO] Comentario,[CCB_RETENT] Retent,[CCB_CODID] CodigoCarga";
                StrSql = StrSql + " ,[CCB_NUMESP] NumeroEspecial,[CCB_NUMAGRUPA] NumeroAgrupa,[CCB_CARTA] Carta,[CCB_COBRABLE] Cobrable,[CCB_CCTID] Contrato";
                StrSql = StrSql + " ,[CCB_SBCID] SubcarteraNombre,[CCB_DOCORI] Originales,[CCB_DOCANT] Antecedentes,[CCB_COMPROMISO] Compromiso";
                StrSql = StrSql + " from cartera_clientes_cpbt_doc  where ccb_codemp =" + codemp.ToString();
                StrSql = StrSql + " and ccb_pclid = " + pclid.ToString();
                StrSql = StrSql + " and ccb_tipcart =" + tipo.ToString();
                StrSql = StrSql + " and ccb_estcpbt in('"+ estcpbt+"')";
                if (!string.IsNullOrEmpty(ctcid))
                {
                    StrSql = StrSql + " and ccb_ctcid in (" + ctcid + "0)";
                }
                if (!string.IsNullOrEmpty(numero))
                {
                    StrSql = StrSql + " and ccb_numero in (" + numero + "'0')";
                }
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure(StrSql);
                //sp.AgregarParametro("codemp", codemp);
                //sp.AgregarParametro("where", where);
                //sp.AgregarParametro("sidx", sidx);
                //sp.AgregarParametro("sord", sord);
                //sp.AgregarParametro("inicio", inicio);
                //sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarScript();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.Comprobante()
                        {
                            Pclid = Int32.Parse(ds.Tables[0].Rows[i]["Pclid"].ToString()),
                            Ctcid = Int32.Parse(ds.Tables[0].Rows[i]["Ctcid"].ToString()),
                            Ccbid = Int32.Parse(ds.Tables[0].Rows[i]["Ccbid"].ToString()),
                            //RutCliente = ds.Tables[0].Rows[i]["RutCliente"].ToString() ?? "",
                            //NombreCliente = ds.Tables[0].Rows[i]["NombreCliente"].ToString() ?? "",
                            //RutDeudor = ds.Tables[0].Rows[i]["RutDeudor"].ToString() ?? "",
                            //NombreDeudor = ds.Tables[0].Rows[i]["NombreDeudor"].ToString() ?? "",
                            FechaDocumento = DateTime.Parse(ds.Tables[0].Rows[i]["FechaDocumento"].ToString()),
                            FechaVencimiento = DateTime.Parse(ds.Tables[0].Rows[i]["FechaVencimiento"].ToString()),
                            FechaIngreso = DateTime.Parse(ds.Tables[0].Rows[i]["FechaIngreso"].ToString()),
                            MontoAsignado = decimal.Parse(ds.Tables[0].Rows[i]["MontoAsignado"].ToString()),
                            Monto = decimal.Parse(ds.Tables[0].Rows[i]["Monto"].ToString()),
                            Saldo = decimal.Parse(ds.Tables[0].Rows[i]["Saldo"].ToString()),
                            //Numero = ds.Tables[0].Rows[i]["Numero"].ToString() ?? "",
                            TipoDocumento = ds.Tables[0].Rows[i]["TipoDocumento"].ToString() ?? ""
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




        public static List<AnularCargaMasiva> ListarCargasAnular(int codemp, int estid, string where, string sidx, string sord, int inicio, int limite)
        {
            List<AnularCargaMasiva> lst = new List<AnularCargaMasiva>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Anular_Carga_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("estid", estid);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new AnularCargaMasiva()
                        {
                            Codemp = Int32.Parse(ds.Tables[0].Rows[i]["Codemp"].ToString()),
                            Pclid = Int32.Parse(ds.Tables[0].Rows[i]["Pclid"].ToString()),
                            RutCliente = ds.Tables[0].Rows[i]["RutCliente"].ToString() ?? "",
                            NombreCliente = ds.Tables[0].Rows[i]["NombreCliente"].ToString() ?? "",
                            Fecha = DateTime.Parse(ds.Tables[0].Rows[i]["Fecha"].ToString()),
                            IdUsuario = Int32.Parse(ds.Tables[0].Rows[i]["Codemp"].ToString()),
                            Usuario = ds.Tables[0].Rows[i]["Usuario"].ToString() ?? ""
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

        public static int ListarCargasAnularCount(int codemp, int estid, string where, string sidx, string sord, int inicio, int limite)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Anular_Carga_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("estid", estid);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    return Int32.Parse(ds.Tables[0].Rows[0]["Count"].ToString());
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return 0; ;
            }
        }

        public static int BorrarCarga(string codemp, string pclid, string estid)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Delete_Cartera_Clientes_CpbtDoc_Cargas");
                sp.AgregarParametro("ccb_codemp", codemp);
                sp.AgregarParametro("ccb_pclid", pclid);
                sp.AgregarParametro("ccb_estid", estid);
                int error = sp.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static List<AprobarCargaMasiva> ListarCargasAprobar(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            List<AprobarCargaMasiva> lst = new List<AprobarCargaMasiva>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Aprobar_Carga_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new AprobarCargaMasiva()
                        {
                            Pclid = Int32.Parse(ds.Tables[0].Rows[i]["Pclid"].ToString()),
                            Ctcid = Int32.Parse(ds.Tables[0].Rows[i]["Ctcid"].ToString()),
                            Ccbid = Int32.Parse(ds.Tables[0].Rows[i]["Ccbid"].ToString()),
                            RutCliente = ds.Tables[0].Rows[i]["RutCliente"].ToString() ?? "",
                            NombreCliente = ds.Tables[0].Rows[i]["NombreCliente"].ToString() ?? "",
                            RutDeudor = ds.Tables[0].Rows[i]["RutDeudor"].ToString() ?? "",
                            NombreDeudor = ds.Tables[0].Rows[i]["NombreDeudor"].ToString() ?? "",
                            FechaDocumento = DateTime.Parse(ds.Tables[0].Rows[i]["FechaDocumento"].ToString()),
                            FechaVencimiento = DateTime.Parse(ds.Tables[0].Rows[i]["FechaVencimiento"].ToString()),
                            FechaIngreso = DateTime.Parse(ds.Tables[0].Rows[i]["FechaIngreso"].ToString()),
                            MontoAsignado = decimal.Parse(ds.Tables[0].Rows[i]["MontoAsignado"].ToString()),
                            Monto = decimal.Parse(ds.Tables[0].Rows[i]["Monto"].ToString()),
                            Saldo = decimal.Parse(ds.Tables[0].Rows[i]["Saldo"].ToString()),
                            Numero = ds.Tables[0].Rows[i]["Numero"].ToString() ?? "",
                            TipoDocumento = ds.Tables[0].Rows[i]["TipoDocumento"].ToString() ?? ""
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

        public static int ListarCargasAprobarCount(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Aprobar_Carga_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    return Int32.Parse(ds.Tables[0].Rows[0]["Count"].ToString());
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return 0; ;
            }
        }

        public static int GrabarCarteraClientesEstadosHistorialEspecial(dto.Comprobante obj, UserSession objSession, int nuevoEstado)
        {
            try
            {
                Funciones objFunc = new Funciones();
                DateTime fecha = DateTime.Parse(objFunc.FechaServer());
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Insertar_Cartera_Clientes_Estados_Historial_Especial");
                sp.AgregarParametro("ceh_codemp", objSession.CodigoEmpresa);
                sp.AgregarParametro("ceh_pclid", obj.Pclid);
                sp.AgregarParametro("ceh_ctcid", obj.Ctcid);
                sp.AgregarParametro("ceh_ccbid", obj.Ccbid);
                sp.AgregarParametro("ceh_fecha", fecha);
                sp.AgregarParametro("ceh_estid", nuevoEstado);
                sp.AgregarParametro("ceh_sucid", objSession.CodigoSucursal);
                sp.AgregarParametro("ceh_gesid", DBNull.Value);
                sp.AgregarParametro("ceh_ipred", objSession.IpRed);
                sp.AgregarParametro("ceh_ipmaquina", objSession.IpPc);
                sp.AgregarParametro("ceh_comentario", "");
                sp.AgregarParametro("ceh_monto", obj.Monto);
                sp.AgregarParametro("ceh_saldo", obj.Saldo);
                sp.AgregarParametro("ceh_usrid", objSession.UserId);


                int error = sp.EjecutarProcedimientoTrans();

                return error;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static int ActualizaCarteraEstados(int pclid, int ctcid, int ccbid, int estid, string estcpbt, UserSession objSession)
        {
            Dimol.dao.Utilidades util = new Dimol.dao.Utilidades(objSession.CodigoEmpresa, objSession.CodigoSucursal, objSession.UserId, objSession.IpRed, objSession.IpPc);
            return util.ActualizaCarteraEstados(objSession.CodigoEmpresa, pclid, ctcid, ccbid, estid, estcpbt);
        }

        public static Dimol.dao.Utilidades InstanciaUtilidades(UserSession objSession)
        {
            return new Dimol.dao.Utilidades(objSession.CodigoEmpresa, objSession.CodigoSucursal, objSession.UserId, objSession.IpRed, objSession.IpPc);
        }

        public static string[] TraeCuotaPago(string rutCliente, string rutDeudor, string numero)
        {
            string[] datosCuota = { "", "", "", "" };
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Cuota_Pago_CpbtDoc");
                sp.AgregarParametro("pcl_rut", rutCliente);
                sp.AgregarParametro("ctc_rut", rutDeudor);
                sp.AgregarParametro("ccb_numero", numero);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    datosCuota[0] = ds.Tables[0].Rows[0][0].ToString();
                    datosCuota[1] = ds.Tables[0].Rows[0][1].ToString();
                    datosCuota[2] = ds.Tables[0].Rows[0][2].ToString();
                    datosCuota[3] = ds.Tables[0].Rows[0][3].ToString();

                }
            }
            catch (Exception ex)
            {

            }
            return datosCuota;
        }
    }
}

