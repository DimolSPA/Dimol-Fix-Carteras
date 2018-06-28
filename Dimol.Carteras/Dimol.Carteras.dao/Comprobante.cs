using Dimol.Carteras.dto;
using Dimol.dao;
using Dimol.dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Dimol.Carteras.dao
{
    public class Comprobante
    {
        public static List<dto.Comprobante> TraeCarteraClienteComprobante(int codemp, int pclid, int ctcid, string estadoCPBT, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.Comprobante> lst = new List<dto.Comprobante>();
            DateTime fecha = new DateTime();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_CpbtDoc_Clientes_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("estcpbt", estadoCPBT);
                sp.AgregarParametro("idid", idioma);
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
                        lst.Add(new dto.Comprobante()
                        {
                            Id = Int32.Parse(ds.Tables[0].Rows[i]["row"].ToString()),
                            Pclid = Int32.Parse(ds.Tables[0].Rows[i]["pclid"].ToString()),
                            Ctcid = Int32.Parse(ds.Tables[0].Rows[i]["ctcid"].ToString()),
                            RutCliente = ds.Tables[0].Rows[i]["RutCliente"].ToString(),
                            NombreCliente = ds.Tables[0].Rows[i]["NombreCliente"].ToString(),
                            TipoCpbtNombre = ds.Tables[0].Rows[i]["TipoCpbtNombre"].ToString(),
                            Ccbid = Int32.Parse(ds.Tables[0].Rows[i]["ccbid"].ToString()),
                            NumeroCpbt = ds.Tables[0].Rows[i]["NumeroCpbt"].ToString(),
                            FechaIngreso = DateTime.TryParse(ds.Tables[0].Rows[i]["FechaIngreso"].ToString(), out fecha) ? DateTime.Parse(ds.Tables[0].Rows[i]["FechaIngreso"].ToString()) : new DateTime(),
                            FechaDocumento = DateTime.TryParse(ds.Tables[0].Rows[i]["FechaDocumento"].ToString(), out fecha) ? DateTime.Parse(ds.Tables[0].Rows[i]["FechaDocumento"].ToString()) : new DateTime(),
                            FechaVencimiento = DateTime.TryParse(ds.Tables[0].Rows[i]["FechaVencimiento"].ToString(), out fecha) ? DateTime.Parse(ds.Tables[0].Rows[i]["FechaVencimiento"].ToString()) : new DateTime(),
                            FechaUltimaGestion = DateTime.TryParse(ds.Tables[0].Rows[i]["FechaUltimaGestion"].ToString(), out fecha) ? DateTime.Parse(ds.Tables[0].Rows[i]["FechaUltimaGestion"].ToString()) : new DateTime(),
                            FechaPlazo = DateTime.TryParse(ds.Tables[0].Rows[i]["FechaPlazo"].ToString(), out fecha) ? DateTime.Parse(ds.Tables[0].Rows[i]["FechaPlazo"].ToString()) : new DateTime(),
                            FechaCalculoInteres = DateTime.TryParse(ds.Tables[0].Rows[i]["FechaCalculoInteres"].ToString(), out fecha) ? DateTime.Parse(ds.Tables[0].Rows[i]["FechaCalculoInteres"].ToString()) : new DateTime(),
                            FechaCastigo = DateTime.TryParse(ds.Tables[0].Rows[i]["FechaCastigo"].ToString(), out fecha) ? DateTime.Parse(ds.Tables[0].Rows[i]["FechaCastigo"].ToString()) : new DateTime(),
                            EstadoCartera = ds.Tables[0].Rows[i]["EstadoCartera"].ToString(),
                            EstadoJudicial = ds.Tables[0].Rows[i]["EstadoJudicial"].ToString(),
                            EstadoCpbt = ds.Tables[0].Rows[i]["EstadoCpbt"].ToString(),
                            CodigoMoneda = Int32.Parse(ds.Tables[0].Rows[i]["CodigoMoneda"].ToString()),
                            TipoCambio = decimal.Parse(ds.Tables[0].Rows[i]["TipoCambio"].ToString()),
                            MontoAsignado = decimal.Parse(ds.Tables[0].Rows[i]["MontoAsignado"].ToString()),
                            Monto = decimal.Parse(ds.Tables[0].Rows[i]["Monto"].ToString()),
                            Saldo = decimal.Parse(ds.Tables[0].Rows[i]["Saldo"].ToString()),
                            GastoJudicial = decimal.Parse(ds.Tables[0].Rows[i]["GastoJudicial"].ToString()),
                            GastoOtros = decimal.Parse(ds.Tables[0].Rows[i]["GastoOtros"].ToString()),
                            Intereses = decimal.Parse(ds.Tables[0].Rows[i]["Intereses"].ToString()),
                            Honorarios = decimal.Parse(ds.Tables[0].Rows[i]["Honorarios"].ToString()),
                            CalculoHonorarios = ds.Tables[0].Rows[i]["CalculoHonorarios"].ToString(),
                            NombreBanco = ds.Tables[0].Rows[i]["NombreBanco"].ToString(),
                            RutGirador = ds.Tables[0].Rows[i]["RutGirador"].ToString(),
                            NombreGirador = ds.Tables[0].Rows[i]["NombreGirador"].ToString(),
                            Comentario = ds.Tables[0].Rows[i]["Comentario"].ToString(),
                            Retent = ds.Tables[0].Rows[i]["retent"].ToString(),
                            NumeroEspecial = ds.Tables[0].Rows[i]["NumeroEspecial"].ToString(),
                            NumeroAgrupa = ds.Tables[0].Rows[i]["NumeroAgrupa"].ToString(),
                            Carta = Int32.Parse(ds.Tables[0].Rows[i]["Carta"].ToString()),
                            Cobrable = ds.Tables[0].Rows[i]["Cobrable"].ToString(),
                            Contrato = Int32.Parse(ds.Tables[0].Rows[i]["cctid"].ToString()),
                            SubcarteraRut = ds.Tables[0].Rows[i]["SubcarteraRut"].ToString(),
                            SubcarteraNombre = ds.Tables[0].Rows[i]["SubcarteraNombre"].ToString(),
                            Originales = ds.Tables[0].Rows[i]["DocumentoOrigen"].ToString(),
                            Antecedentes = ds.Tables[0].Rows[i]["docant"].ToString(),
                            TipoCartera = Int32.Parse(ds.Tables[0].Rows[i]["TipoCartera"].ToString()),
                            DiasVencido = Int32.Parse(ds.Tables[0].Rows[i]["DiasVencido"].ToString()),
                            Moneda = ds.Tables[0].Rows[i]["Moneda"].ToString(),
                            TotalDeuda = decimal.Parse(ds.Tables[0].Rows[i]["TotalDeuda"].ToString()),
                            Compromiso = decimal.Parse(ds.Tables[0].Rows[i]["Compromiso"].ToString()),
                            CodigoCarga = ds.Tables[0].Rows[i]["CodigoCargaNombre"].ToString(),
                            DemandaPendiente = Int32.Parse(ds.Tables[0].Rows[i]["DemandaPendiente"].ToString())

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

        public static List<dto.Comprobante> TraeCarteraClienteComprobanteTotal(int codemp, int pclid, int ctcid, string estadoCPBT, int idioma)
        {
            List<dto.Comprobante> lst = new List<dto.Comprobante>();
            DateTime fecha = new DateTime();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Cartera_Clientes_CpbtDoc_Totales");
                sp.AgregarParametro("ccb_codemp", codemp);
                sp.AgregarParametro("ccb_pclid", pclid);
                sp.AgregarParametro("ccb_ctcid", ctcid);
                sp.AgregarParametro("ccb_estcpbt", estadoCPBT);
                sp.AgregarParametro("idi_idid", idioma);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.Comprobante()
                        {
                            Id = codemp,
                            Pclid = pclid,
                            Ctcid = ctcid,
                            Ccbid = 1,
                            CodigoMoneda = 0,
                            TipoCambio = 0,
                            MontoAsignado = decimal.Parse(ds.Tables[0].Rows[i]["TotReal"].ToString()),
                            Monto = decimal.Parse(ds.Tables[0].Rows[i]["TotMonto"].ToString()),
                            Saldo = decimal.Parse(ds.Tables[0].Rows[i]["TotSaldo"].ToString()),
                            GastoJudicial = decimal.Parse(ds.Tables[0].Rows[i]["TotGjud"].ToString()),
                            GastoOtros = decimal.Parse(ds.Tables[0].Rows[i]["TotGPre"].ToString()),
                            Intereses = decimal.Parse(ds.Tables[0].Rows[i]["TotInte"].ToString()),
                            Honorarios = decimal.Parse(ds.Tables[0].Rows[i]["TotHono"].ToString()),
                            Carta = 0,
                            Contrato = 0,
                            TipoCartera = 0,
                            DiasVencido = 0,
                            TotalDeuda = decimal.Parse(ds.Tables[0].Rows[i]["Total"].ToString()),
                            Compromiso = decimal.Parse(ds.Tables[0].Rows[i]["TotComp"].ToString())
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

        public static List<dto.Comprobante> TraeCarteraClienteComprobanteTotalMoneda(int codemp, int pclid, int ctcid, string estadoCPBT, int idioma)
        {
            List<dto.Comprobante> lst = new List<dto.Comprobante>();
            DateTime fecha = new DateTime();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Cartera_Clientes_CpbtDoc_Totales_Mon");
                sp.AgregarParametro("ccb_codemp", codemp);
                sp.AgregarParametro("ccb_pclid", pclid);
                sp.AgregarParametro("ccb_ctcid", ctcid);
                sp.AgregarParametro("ccb_estcpbt", estadoCPBT);
                sp.AgregarParametro("idi_idid", idioma);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.Comprobante()
                        {
                            Id = codemp,
                            Pclid = pclid,
                            Ctcid = ctcid,
                            Ccbid = 1,
                            CodigoMoneda = 0,
                            TipoCambio = 0,
                            MontoAsignado = decimal.Parse(ds.Tables[0].Rows[i]["TotReal"].ToString()),
                            Monto = decimal.Parse(ds.Tables[0].Rows[i]["TotMonto"].ToString()),
                            Saldo = decimal.Parse(ds.Tables[0].Rows[i]["TotSaldo"].ToString()),
                            GastoJudicial = decimal.Parse(ds.Tables[0].Rows[i]["TotGjud"].ToString()),
                            GastoOtros = decimal.Parse(ds.Tables[0].Rows[i]["TotGPre"].ToString()),
                            Intereses = decimal.Parse(ds.Tables[0].Rows[i]["TotInte"].ToString()),
                            Honorarios = decimal.Parse(ds.Tables[0].Rows[i]["TotHono"].ToString()),
                            Carta = 0,
                            Contrato = 0,
                            TipoCartera = 0,
                            DiasVencido = 0,
                            TotalDeuda = decimal.Parse(ds.Tables[0].Rows[i]["Total"].ToString()),
                            Compromiso = decimal.Parse(ds.Tables[0].Rows[i]["TotComp"].ToString())
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

        public static dto.Comprobante TraeCpbt(int codemp, int pclid, int ctcid, int ccbid)
        {
            dto.Comprobante obj = new dto.Comprobante();
            DateTime fecha = new DateTime();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_CpbtDoc");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("ccbid", ccbid);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        obj = new dto.Comprobante()
                        {
                            Id = 0,
                            Pclid = Int32.Parse(ds.Tables[0].Rows[i]["pclid"].ToString()),
                            Ctcid = Int32.Parse(ds.Tables[0].Rows[i]["ctcid"].ToString()),
                            TipoDocumento = ds.Tables[0].Rows[i]["TipoDocumento"].ToString(),
                            Ccbid = Int32.Parse(ds.Tables[0].Rows[i]["ccbid"].ToString()),
                            NumeroCpbt = ds.Tables[0].Rows[i]["NumeroCpbt"].ToString(),
                            FechaIngreso = DateTime.TryParse(ds.Tables[0].Rows[i]["FechaIngreso"].ToString(), out fecha) ? DateTime.Parse(ds.Tables[0].Rows[i]["FechaIngreso"].ToString()) : new DateTime(),
                            FechaDocumento = DateTime.TryParse(ds.Tables[0].Rows[i]["FechaDocumento"].ToString(), out fecha) ? DateTime.Parse(ds.Tables[0].Rows[i]["FechaDocumento"].ToString()) : new DateTime(),
                            FechaVencimiento = DateTime.TryParse(ds.Tables[0].Rows[i]["FechaVencimiento"].ToString(), out fecha) ? DateTime.Parse(ds.Tables[0].Rows[i]["FechaVencimiento"].ToString()) : new DateTime(),
                            FechaUltimaGestion = DateTime.TryParse(ds.Tables[0].Rows[i]["FechaUltimaGestion"].ToString(), out fecha) ? DateTime.Parse(ds.Tables[0].Rows[i]["FechaUltimaGestion"].ToString()) : new DateTime(),
                            FechaPlazo = DateTime.TryParse(ds.Tables[0].Rows[i]["FechaPlazo"].ToString(), out fecha) ? DateTime.Parse(ds.Tables[0].Rows[i]["FechaPlazo"].ToString()) : new DateTime(),
                            FechaCalculoInteres = DateTime.TryParse(ds.Tables[0].Rows[i]["FechaCalculoInteres"].ToString(), out fecha) ? DateTime.Parse(ds.Tables[0].Rows[i]["FechaCalculoInteres"].ToString()) : new DateTime(),
                            FechaCastigo = DateTime.TryParse(ds.Tables[0].Rows[i]["FechaCastigo"].ToString(), out fecha) ? DateTime.Parse(ds.Tables[0].Rows[i]["FechaCastigo"].ToString()) : new DateTime(),
                            EstadoCartera = ds.Tables[0].Rows[i]["EstadoCartera"].ToString(),
                            EstadoCpbt = ds.Tables[0].Rows[i]["EstadoCpbt"].ToString(),
                            CodigoMoneda = Int32.Parse(ds.Tables[0].Rows[i]["CodigoMoneda"].ToString()),
                            TipoCambio = decimal.Parse(ds.Tables[0].Rows[i]["TipoCambio"].ToString()),
                            MontoAsignado = decimal.Parse(ds.Tables[0].Rows[i]["MontoAsignado"].ToString()),
                            Monto = decimal.Parse(ds.Tables[0].Rows[i]["Monto"].ToString()),
                            Saldo = decimal.Parse(ds.Tables[0].Rows[i]["Saldo"].ToString()),
                            GastoJudicial = decimal.Parse(ds.Tables[0].Rows[i]["GastoJudicial"].ToString()),
                            GastoOtros = decimal.Parse(ds.Tables[0].Rows[i]["GastoOtros"].ToString()),
                            Intereses = decimal.Parse(ds.Tables[0].Rows[i]["Intereses"].ToString()),
                            Honorarios = decimal.Parse(ds.Tables[0].Rows[i]["Honorarios"].ToString()),
                            CalculoHonorarios = ds.Tables[0].Rows[i]["CalculoHonorarios"].ToString(),
                            NombreBanco = ds.Tables[0].Rows[i]["NombreBanco"].ToString(),
                            RutGirador = ds.Tables[0].Rows[i]["RutGirador"].ToString(),
                            NombreGirador = ds.Tables[0].Rows[i]["NombreGirador"].ToString(),
                            Comentario = ds.Tables[0].Rows[i]["Comentario"].ToString(),
                            Retent = ds.Tables[0].Rows[i]["retent"].ToString(),
                            NumeroEspecial = ds.Tables[0].Rows[i]["NumeroEspecial"].ToString(),
                            NumeroAgrupa = ds.Tables[0].Rows[i]["NumeroAgrupa"].ToString(),
                            Carta = Int32.Parse(ds.Tables[0].Rows[i]["Carta"].ToString()),
                            Cobrable = ds.Tables[0].Rows[i]["Cobrable"].ToString(),
                            Contrato = Int32.Parse(ds.Tables[0].Rows[i]["Contrato"].ToString()),
                            Originales = ds.Tables[0].Rows[i]["Originales"].ToString(),
                            Antecedentes = ds.Tables[0].Rows[i]["Antecedentes"].ToString(),
                            TipoCartera = Int32.Parse(ds.Tables[0].Rows[i]["TipoCartera"].ToString()),
                            Compromiso = decimal.Parse(ds.Tables[0].Rows[i]["Compromiso"].ToString()),
                            CodigoCarga = ds.Tables[0].Rows[i]["CodigoCarga"].ToString(),
                            SubcarteraNombre = ds.Tables[0].Rows[i]["Subcartera"].ToString(),
                            SubcarteraRut = ds.Tables[0].Rows[i]["SubcarteraRutNombre"].ToString(),
                            MotivoCobranza = ds.Tables[0].Rows[i]["MotivoCobranza"].ToString(),
                            TerceroId = string.IsNullOrEmpty(ds.Tables[0].Rows[i]["tercero"].ToString()) ? 0 : Int32.Parse(ds.Tables[0].Rows[i]["tercero"].ToString()),
                            IdCuenta = ds.Tables[0].Rows[i]["IdCuenta"].ToString(),
                            DescripcionCuenta = ds.Tables[0].Rows[i]["DescCuenta"].ToString()

                        };
                    }
                }

                return obj;
            }
            catch (Exception ex)
            {
                return obj;
            }
        }

        public static dto.Comprobante TraeCpbtNumero(int codemp, int pclid, int ctcid, string numero)
        {
            dto.Comprobante obj = new dto.Comprobante();
            DateTime fecha = new DateTime();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_CpbtDoc_Numero");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("numero", numero);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        obj = new dto.Comprobante()
                        {
                            Id = 0,
                            Pclid = Int32.Parse(ds.Tables[0].Rows[i]["pclid"].ToString()),
                            Ctcid = Int32.Parse(ds.Tables[0].Rows[i]["ctcid"].ToString()),
                            TipoDocumento = ds.Tables[0].Rows[i]["TipoDocumento"].ToString(),
                            Ccbid = Int32.Parse(ds.Tables[0].Rows[i]["ccbid"].ToString()),
                            NumeroCpbt = ds.Tables[0].Rows[i]["NumeroCpbt"].ToString(),
                            FechaIngreso = DateTime.TryParse(ds.Tables[0].Rows[i]["FechaIngreso"].ToString(), out fecha) ? DateTime.Parse(ds.Tables[0].Rows[i]["FechaIngreso"].ToString()) : new DateTime(),
                            FechaDocumento = DateTime.TryParse(ds.Tables[0].Rows[i]["FechaDocumento"].ToString(), out fecha) ? DateTime.Parse(ds.Tables[0].Rows[i]["FechaDocumento"].ToString()) : new DateTime(),
                            FechaVencimiento = DateTime.TryParse(ds.Tables[0].Rows[i]["FechaVencimiento"].ToString(), out fecha) ? DateTime.Parse(ds.Tables[0].Rows[i]["FechaVencimiento"].ToString()) : new DateTime(),
                            FechaUltimaGestion = DateTime.TryParse(ds.Tables[0].Rows[i]["FechaUltimaGestion"].ToString(), out fecha) ? DateTime.Parse(ds.Tables[0].Rows[i]["FechaUltimaGestion"].ToString()) : new DateTime(),
                            FechaPlazo = DateTime.TryParse(ds.Tables[0].Rows[i]["FechaPlazo"].ToString(), out fecha) ? DateTime.Parse(ds.Tables[0].Rows[i]["FechaPlazo"].ToString()) : new DateTime(),
                            FechaCalculoInteres = DateTime.TryParse(ds.Tables[0].Rows[i]["FechaCalculoInteres"].ToString(), out fecha) ? DateTime.Parse(ds.Tables[0].Rows[i]["FechaCalculoInteres"].ToString()) : new DateTime(),
                            FechaCastigo = DateTime.TryParse(ds.Tables[0].Rows[i]["FechaCastigo"].ToString(), out fecha) ? DateTime.Parse(ds.Tables[0].Rows[i]["FechaCastigo"].ToString()) : new DateTime(),
                            EstadoCartera = ds.Tables[0].Rows[i]["EstadoCartera"].ToString(),
                            EstadoCpbt = ds.Tables[0].Rows[i]["EstadoCpbt"].ToString(),
                            CodigoMoneda = Int32.Parse(ds.Tables[0].Rows[i]["CodigoMoneda"].ToString()),
                            TipoCambio = decimal.Parse(ds.Tables[0].Rows[i]["TipoCambio"].ToString()),
                            MontoAsignado = decimal.Parse(ds.Tables[0].Rows[i]["MontoAsignado"].ToString()),
                            Monto = decimal.Parse(ds.Tables[0].Rows[i]["Monto"].ToString()),
                            Saldo = decimal.Parse(ds.Tables[0].Rows[i]["Saldo"].ToString()),
                            GastoJudicial = decimal.Parse(ds.Tables[0].Rows[i]["GastoJudicial"].ToString()),
                            GastoOtros = decimal.Parse(ds.Tables[0].Rows[i]["GastoOtros"].ToString()),
                            Intereses = decimal.Parse(ds.Tables[0].Rows[i]["Intereses"].ToString()),
                            Honorarios = decimal.Parse(ds.Tables[0].Rows[i]["Honorarios"].ToString()),
                            CalculoHonorarios = ds.Tables[0].Rows[i]["CalculoHonorarios"].ToString(),
                            NombreBanco = ds.Tables[0].Rows[i]["NombreBanco"].ToString(),
                            RutGirador = ds.Tables[0].Rows[i]["RutGirador"].ToString(),
                            NombreGirador = ds.Tables[0].Rows[i]["NombreGirador"].ToString(),
                            Comentario = ds.Tables[0].Rows[i]["Comentario"].ToString(),
                            Retent = ds.Tables[0].Rows[i]["retent"].ToString(),
                            NumeroEspecial = ds.Tables[0].Rows[i]["NumeroEspecial"].ToString(),
                            NumeroAgrupa = ds.Tables[0].Rows[i]["NumeroAgrupa"].ToString(),
                            Carta = Int32.Parse(ds.Tables[0].Rows[i]["Carta"].ToString()),
                            Cobrable = ds.Tables[0].Rows[i]["Cobrable"].ToString(),
                            Contrato = Int32.Parse(ds.Tables[0].Rows[i]["Contrato"].ToString()),
                            Originales = ds.Tables[0].Rows[i]["Originales"].ToString(),
                            Antecedentes = ds.Tables[0].Rows[i]["Antecedentes"].ToString(),
                            TipoCartera = Int32.Parse(ds.Tables[0].Rows[i]["TipoCartera"].ToString()),
                            Compromiso = decimal.Parse(ds.Tables[0].Rows[i]["Compromiso"].ToString()),
                            CodigoCarga = ds.Tables[0].Rows[i]["CodigoCarga"].ToString(),
                            SubcarteraRut = ds.Tables[0].Rows[i]["Contrato"].ToString(),
                            MotivoCobranza = ds.Tables[0].Rows[i]["MotivoCobranza"].ToString()

                        };
                    }
                }

                return obj;
            }
            catch (Exception ex)
            {
                return obj;
            }
        }

        public static dto.Comprobante TraeCpbtNumeroTipo(int codemp, int pclid, int ctcid, string numero, int tpcid)
        {
            dto.Comprobante obj = new dto.Comprobante();
            DateTime fecha = new DateTime();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_CpbtDoc_Numero");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("numero", numero);
                sp.AgregarParametro("tpcid", tpcid);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        obj = new dto.Comprobante()
                        {
                            Id = 0,
                            Pclid = Int32.Parse(ds.Tables[0].Rows[i]["pclid"].ToString()),
                            Ctcid = Int32.Parse(ds.Tables[0].Rows[i]["ctcid"].ToString()),
                            TipoDocumento = ds.Tables[0].Rows[i]["TipoDocumento"].ToString(),
                            Ccbid = Int32.Parse(ds.Tables[0].Rows[i]["ccbid"].ToString()),
                            NumeroCpbt = ds.Tables[0].Rows[i]["NumeroCpbt"].ToString(),
                            FechaIngreso = DateTime.TryParse(ds.Tables[0].Rows[i]["FechaIngreso"].ToString(), out fecha) ? DateTime.Parse(ds.Tables[0].Rows[i]["FechaIngreso"].ToString()) : new DateTime(),
                            FechaDocumento = DateTime.TryParse(ds.Tables[0].Rows[i]["FechaDocumento"].ToString(), out fecha) ? DateTime.Parse(ds.Tables[0].Rows[i]["FechaDocumento"].ToString()) : new DateTime(),
                            FechaVencimiento = DateTime.TryParse(ds.Tables[0].Rows[i]["FechaVencimiento"].ToString(), out fecha) ? DateTime.Parse(ds.Tables[0].Rows[i]["FechaVencimiento"].ToString()) : new DateTime(),
                            FechaUltimaGestion = DateTime.TryParse(ds.Tables[0].Rows[i]["FechaUltimaGestion"].ToString(), out fecha) ? DateTime.Parse(ds.Tables[0].Rows[i]["FechaUltimaGestion"].ToString()) : new DateTime(),
                            FechaPlazo = DateTime.TryParse(ds.Tables[0].Rows[i]["FechaPlazo"].ToString(), out fecha) ? DateTime.Parse(ds.Tables[0].Rows[i]["FechaPlazo"].ToString()) : new DateTime(),
                            FechaCalculoInteres = DateTime.TryParse(ds.Tables[0].Rows[i]["FechaCalculoInteres"].ToString(), out fecha) ? DateTime.Parse(ds.Tables[0].Rows[i]["FechaCalculoInteres"].ToString()) : new DateTime(),
                            FechaCastigo = DateTime.TryParse(ds.Tables[0].Rows[i]["FechaCastigo"].ToString(), out fecha) ? DateTime.Parse(ds.Tables[0].Rows[i]["FechaCastigo"].ToString()) : new DateTime(),
                            EstadoCartera = ds.Tables[0].Rows[i]["EstadoCartera"].ToString(),
                            EstadoCpbt = ds.Tables[0].Rows[i]["EstadoCpbt"].ToString(),
                            CodigoMoneda = Int32.Parse(ds.Tables[0].Rows[i]["CodigoMoneda"].ToString()),
                            TipoCambio = decimal.Parse(ds.Tables[0].Rows[i]["TipoCambio"].ToString()),
                            MontoAsignado = decimal.Parse(ds.Tables[0].Rows[i]["MontoAsignado"].ToString()),
                            Monto = decimal.Parse(ds.Tables[0].Rows[i]["Monto"].ToString()),
                            Saldo = decimal.Parse(ds.Tables[0].Rows[i]["Saldo"].ToString()),
                            GastoJudicial = decimal.Parse(ds.Tables[0].Rows[i]["GastoJudicial"].ToString()),
                            GastoOtros = decimal.Parse(ds.Tables[0].Rows[i]["GastoOtros"].ToString()),
                            Intereses = decimal.Parse(ds.Tables[0].Rows[i]["Intereses"].ToString()),
                            Honorarios = decimal.Parse(ds.Tables[0].Rows[i]["Honorarios"].ToString()),
                            CalculoHonorarios = ds.Tables[0].Rows[i]["CalculoHonorarios"].ToString(),
                            NombreBanco = ds.Tables[0].Rows[i]["NombreBanco"].ToString(),
                            RutGirador = ds.Tables[0].Rows[i]["RutGirador"].ToString(),
                            NombreGirador = ds.Tables[0].Rows[i]["NombreGirador"].ToString(),
                            Comentario = ds.Tables[0].Rows[i]["Comentario"].ToString(),
                            Retent = ds.Tables[0].Rows[i]["retent"].ToString(),
                            NumeroEspecial = ds.Tables[0].Rows[i]["NumeroEspecial"].ToString(),
                            NumeroAgrupa = ds.Tables[0].Rows[i]["NumeroAgrupa"].ToString(),
                            Carta = Int32.Parse(ds.Tables[0].Rows[i]["Carta"].ToString()),
                            Cobrable = ds.Tables[0].Rows[i]["Cobrable"].ToString(),
                            Contrato = Int32.Parse(ds.Tables[0].Rows[i]["Contrato"].ToString()),
                            Originales = ds.Tables[0].Rows[i]["Originales"].ToString(),
                            Antecedentes = ds.Tables[0].Rows[i]["Antecedentes"].ToString(),
                            TipoCartera = Int32.Parse(ds.Tables[0].Rows[i]["TipoCartera"].ToString()),
                            Compromiso = decimal.Parse(ds.Tables[0].Rows[i]["Compromiso"].ToString()),
                            CodigoCarga = ds.Tables[0].Rows[i]["CodigoCarga"].ToString(),
                            SubcarteraRut = ds.Tables[0].Rows[i]["Contrato"].ToString(),
                            MotivoCobranza = ds.Tables[0].Rows[i]["MotivoCobranza"].ToString()

                        };
                    }
                }

                return obj;
            }
            catch (Exception ex)
            {
                return obj;
            }
        }

        public static List<dto.Comprobante> ListaCpbtNumeroTipo(int codemp, int pclid, string ctcid, string numero, string estcpbt, int tipCart)
        {
            List<dto.Comprobante> obj = new List<dto.Comprobante>();
            DateTime fecha = new DateTime();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Documentos_Carga_Cocha");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("numero", numero);
                sp.AgregarParametro("tipCart", tipCart);
                sp.AgregarParametro("estcpbt", estcpbt);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        obj.Add(new dto.Comprobante()
                        {
                            Id = 0,
                            Pclid = Int32.Parse(ds.Tables[0].Rows[i]["pclid"].ToString()),
                            Ctcid = Int32.Parse(ds.Tables[0].Rows[i]["ctcid"].ToString()),
                            TipoDocumento = ds.Tables[0].Rows[i]["TipoDocumento"].ToString(),
                            Ccbid = Int32.Parse(ds.Tables[0].Rows[i]["ccbid"].ToString()),
                            NumeroCpbt = ds.Tables[0].Rows[i]["NumeroCpbt"].ToString(),
                            FechaIngreso = DateTime.TryParse(ds.Tables[0].Rows[i]["FechaIngreso"].ToString(), out fecha) ? DateTime.Parse(ds.Tables[0].Rows[i]["FechaIngreso"].ToString()) : new DateTime(),
                            FechaDocumento = DateTime.TryParse(ds.Tables[0].Rows[i]["FechaDocumento"].ToString(), out fecha) ? DateTime.Parse(ds.Tables[0].Rows[i]["FechaDocumento"].ToString()) : new DateTime(),
                            FechaVencimiento = DateTime.TryParse(ds.Tables[0].Rows[i]["FechaVencimiento"].ToString(), out fecha) ? DateTime.Parse(ds.Tables[0].Rows[i]["FechaVencimiento"].ToString()) : new DateTime(),
                            FechaUltimaGestion = DateTime.TryParse(ds.Tables[0].Rows[i]["FechaUltimaGestion"].ToString(), out fecha) ? DateTime.Parse(ds.Tables[0].Rows[i]["FechaUltimaGestion"].ToString()) : new DateTime(),
                            FechaPlazo = DateTime.TryParse(ds.Tables[0].Rows[i]["FechaPlazo"].ToString(), out fecha) ? DateTime.Parse(ds.Tables[0].Rows[i]["FechaPlazo"].ToString()) : new DateTime(),
                            FechaCalculoInteres = DateTime.TryParse(ds.Tables[0].Rows[i]["FechaCalculoInteres"].ToString(), out fecha) ? DateTime.Parse(ds.Tables[0].Rows[i]["FechaCalculoInteres"].ToString()) : new DateTime(),
                            FechaCastigo = DateTime.TryParse(ds.Tables[0].Rows[i]["FechaCastigo"].ToString(), out fecha) ? DateTime.Parse(ds.Tables[0].Rows[i]["FechaCastigo"].ToString()) : new DateTime(),
                            EstadoCartera = ds.Tables[0].Rows[i]["EstadoCartera"].ToString(),
                            EstadoCpbt = ds.Tables[0].Rows[i]["EstadoCpbt"].ToString(),
                            CodigoMoneda = Int32.Parse(ds.Tables[0].Rows[i]["CodigoMoneda"].ToString()),
                            TipoCambio = decimal.Parse(ds.Tables[0].Rows[i]["TipoCambio"].ToString()),
                            MontoAsignado = decimal.Parse(ds.Tables[0].Rows[i]["MontoAsignado"].ToString()),
                            Monto = decimal.Parse(ds.Tables[0].Rows[i]["Monto"].ToString()),
                            Saldo = decimal.Parse(ds.Tables[0].Rows[i]["Saldo"].ToString()),
                            GastoJudicial = decimal.Parse(ds.Tables[0].Rows[i]["GastoJudicial"].ToString()),
                            GastoOtros = decimal.Parse(ds.Tables[0].Rows[i]["GastoOtros"].ToString()),
                            Intereses = decimal.Parse(ds.Tables[0].Rows[i]["Intereses"].ToString()),
                            Honorarios = decimal.Parse(ds.Tables[0].Rows[i]["Honorarios"].ToString()),
                            CalculoHonorarios = ds.Tables[0].Rows[i]["CalculoHonorarios"].ToString(),
                            NombreBanco = ds.Tables[0].Rows[i]["NombreBanco"].ToString(),
                            RutGirador = ds.Tables[0].Rows[i]["RutGirador"].ToString(),
                            NombreGirador = ds.Tables[0].Rows[i]["NombreGirador"].ToString(),
                            Comentario = ds.Tables[0].Rows[i]["Comentario"].ToString(),
                            Retent = ds.Tables[0].Rows[i]["retent"].ToString(),
                            NumeroEspecial = ds.Tables[0].Rows[i]["NumeroEspecial"].ToString(),
                            NumeroAgrupa = ds.Tables[0].Rows[i]["NumeroAgrupa"].ToString(),
                            Carta = Int32.Parse(ds.Tables[0].Rows[i]["Carta"].ToString()),
                            Cobrable = ds.Tables[0].Rows[i]["Cobrable"].ToString(),
                            Contrato = Int32.Parse(ds.Tables[0].Rows[i]["Contrato"].ToString()),
                            Originales = ds.Tables[0].Rows[i]["Originales"].ToString(),
                            Antecedentes = ds.Tables[0].Rows[i]["Antecedentes"].ToString(),
                            TipoCartera = Int32.Parse(ds.Tables[0].Rows[i]["TipoCartera"].ToString()),
                            Compromiso = decimal.Parse(ds.Tables[0].Rows[i]["Compromiso"].ToString()),
                            CodigoCarga = ds.Tables[0].Rows[i]["CodigoCarga"].ToString(),
                            SubcarteraRut = ds.Tables[0].Rows[i]["Contrato"].ToString(),
                            MotivoCobranza = ds.Tables[0].Rows[i]["MotivoCobranza"].ToString()

                        });
                    }
                }

                return obj;
            }
            catch (Exception ex)
            {
                return obj;
            }
        }

        public static List<dto.Comprobante> TraeListaCpbt(CabeceraComprobante obj)
        {
            List<dto.Comprobante> lst = new List<dto.Comprobante>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Datos_CpbtDoc");
                sp.AgregarParametro("codemp", obj.Codemp);
                sp.AgregarParametro("tpcid", obj.TipoComprobante);
                sp.AgregarParametro("numero", obj.CabeceraId);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.Comprobante()
                        {
                            Pclid = Int32.Parse(ds.Tables[0].Rows[i]["rdc_pclid"].ToString()),
                            Ctcid = Int32.Parse(ds.Tables[0].Rows[i]["rdc_ctcid"].ToString()),
                            Ccbid = Int32.Parse(ds.Tables[0].Rows[i]["rdc_ccbid"].ToString()),
                            Monto = decimal.Parse(ds.Tables[0].Rows[i]["DCR_MONTO"].ToString())
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

        public static List<Dimol.dto.Combobox> ListarGrupoCpbt(int codemp, int pclid, int ctcid, string estadoCPBT, int idioma)
        {
            List<Dimol.dto.Combobox> lst = new List<Dimol.dto.Combobox>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Cartera_Clientes_CpbtDoc_Grupo");
                sp.AgregarParametro("ccb_codemp", codemp);
                sp.AgregarParametro("ccb_pclid", pclid);
                sp.AgregarParametro("ccb_ctcid", ctcid);
                sp.AgregarParametro("ccb_estcpbt", estadoCPBT);
                sp.AgregarParametro("idi_idid", idioma);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Dimol.dto.Combobox()
                        {
                            Text = ds.Tables[0].Rows[i]["ccb_numagrupa"].ToString(),
                            Value = ds.Tables[0].Rows[i]["ccb_numagrupa"].ToString()
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

        public static int TraeCarteraClienteComprobanteCount(int codemp, int pclid, int ctcid, string estadoCPBT, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            int count = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_CpbtDoc_Clientes_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("estcpbt", estadoCPBT);
                sp.AgregarParametro("idid", idioma);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    count = Int32.Parse(ds.Tables[0].Rows[0]["ctcid"].ToString());
                }

                return count;
            }
            catch (Exception ex)
            {
                return count;
            }
        }

        public static int TraeTipoCartera(int codemp, int pclid, int ctcid, string estadoCPBT)
        {
            int count = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Tipo_Cartera");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("estcpbt", estadoCPBT);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    count = Int32.Parse(ds.Tables[0].Rows[0]["cartera"].ToString());
                }

                return count;
            }
            catch (Exception ex)
            {
                return count;
            }
        }

        public static List<Combobox> ListarTipoCartera(int idioma)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Etiquetas");
                for (int i = 1; i < 4; i++)
                {
                    sp = new StoredProcedure("Trae_Etiquetas");
                    sp.AgregarParametro("codigo", "TipCart" + i);
                    sp.AgregarParametro("idioma", idioma);
                    ds = sp.EjecutarProcedimiento();

                    lst.Add(new Combobox()
                    {
                        Text = ds.Tables[0].Rows[0][0].ToString(),
                        Value = i.ToString()
                    });

                }
                return lst;
            }
            catch (Exception ex)
            {
                return lst;
            }

        }

        public static List<Combobox> ListarEstadosCartera(int codemp, int idioma)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Estados_Cartera");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idioma", idioma);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Dimol.dto.Combobox()
                        {
                            Text = ds.Tables[0].Rows[i]["Descripcion"].ToString(),
                            Value = ds.Tables[0].Rows[i]["Id"].ToString()
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

        public static List<Combobox> ListarMonedas(int codemp)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Monedas");
                sp.AgregarParametro("codemp", codemp);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Dimol.dto.Combobox()
                        {
                            Text = ds.Tables[0].Rows[i]["Descripcion"].ToString(),
                            Value = ds.Tables[0].Rows[i]["Id"].ToString()
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

        public static List<Combobox> ListarMotivoCobranza(int codemp, int idioma, string first)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Motivo_Cobranza");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idioma", idioma);
                ds = sp.EjecutarProcedimiento();
                lst.Add(new Combobox()
                {
                    Value = "",
                    Text = first
                });

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Dimol.dto.Combobox()
                        {
                            Text = ds.Tables[0].Rows[i]["Descripcion"].ToString(),
                            Value = ds.Tables[0].Rows[i]["Id"].ToString()
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

        public static List<Combobox> ListarAsociadoSubcartera(int codemp, string first)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Asociado_Subcartera");
                sp.AgregarParametro("codemp", codemp);
                ds = sp.EjecutarProcedimiento();
                lst.Add(new Combobox()
                {
                    Value = "",
                    Text = first
                });

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Dimol.dto.Combobox()
                        {
                            Text = ds.Tables[0].Rows[i]["Nombre"].ToString(),
                            Value = ds.Tables[0].Rows[i]["Id"].ToString()
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

        public static List<Combobox> ListarCodigoCarga(int codemp, int pclid, string first)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Codigo_Carga");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                ds = sp.EjecutarProcedimiento();
                lst.Add(new Combobox()
                {
                    Value = "",
                    Text = first
                });

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Dimol.dto.Combobox()
                        {
                            Text = ds.Tables[0].Rows[i]["Nombre"].ToString(),
                            Value = ds.Tables[0].Rows[i]["Id"].ToString()
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

        public static List<Combobox> ListarContrato(int codemp, int pclid, int tipoCartera, string first)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Contrato");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("tipoCartera", tipoCartera);
                ds = sp.EjecutarProcedimiento();
                lst.Add(new Combobox()
                {
                    Value = "",
                    Text = first
                });

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Dimol.dto.Combobox()
                        {
                            Text = ds.Tables[0].Rows[i]["Nombre"].ToString(),
                            Value = ds.Tables[0].Rows[i]["Id"].ToString()
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

        public static int GrabarPanelDemandaMasiva(int codemp, int pclid, int ctcid, int userId, int ccbid, int? sbcid, int? tpcid, dto.CargaJudicial datos)
        {
            int result = -1;

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Insertar_Panel_Demanda_Masiva");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("sbcid", (sbcid == 0) ? DBNull.Value : (object)sbcid);
                sp.AgregarParametro("trbid", (datos.IdTribunal == 0) ? DBNull.Value : (object)datos.IdTribunal);
                sp.AgregarParametro("tpcid", DBNull.Value);
                sp.AgregarParametro("user", userId);
                sp.AgregarParametro("ccbid", ccbid);
                sp.AgregarParametro("fecdem", datos.FechaDemanda == new DateTime() ? DBNull.Value : (object)datos.FechaDemanda);
                sp.AgregarParametro("cuodem", datos.NumeroCuotasDemanda);
                sp.AgregarParametro("mondem", datos.MontoDemanda);
                sp.AgregarParametro("monpcuodem", datos.MontoCuotasDemanda);
                sp.AgregarParametro("monucoudem", datos.MontoUltimaCuotaDemanda);
                sp.AgregarParametro("fecpcoudem", datos.FechaPrimeraCuotaDemanda == new DateTime() ? DBNull.Value : (object)datos.FechaPrimeraCuotaDemanda);
                sp.AgregarParametro("fecucoudem", datos.FechaUltimaCuotaDemanda == new DateTime() ? DBNull.Value : (object)datos.FechaUltimaCuotaDemanda);
                sp.AgregarParametro("intdem", datos.InteresDemanda);
                sp.AgregarParametro("fecave", datos.FechaAvenimiento == new DateTime() ? DBNull.Value : (object)datos.FechaAvenimiento);
                sp.AgregarParametro("cuoave", datos.NumeroCuotasAvenimiento);
                sp.AgregarParametro("monave", datos.MontoAvenimiento);
                sp.AgregarParametro("monpcouave", datos.MontoCuotasAvenimiento);
                sp.AgregarParametro("monucouave", datos.MontoCuotasAvenimiento);
                sp.AgregarParametro("fecpcouave", datos.FechaAvenimiento == new DateTime() ? DBNull.Value : (object)datos.FechaAvenimiento);
                sp.AgregarParametro("fecucouave", datos.FechaPrimeraCuotaAvenimiento == new DateTime() ? DBNull.Value : (object)datos.FechaPrimeraCuotaAvenimiento);
                sp.AgregarParametro("intave", datos.InteresDemanda);

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    result = Int32.Parse(ds.Tables[0].Rows[0]["panelId"].ToString());
                }
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.Comprobante.GrabarPanelDemandaMasiva", userId);
                return -1;
            }

            return result;
        }

        public static int GrabarPanelDemandaMasivaDetalle(int panelId, string userEncargado, DateTime fecEnvio, DateTime fecEntrega, DateTime? fecIngreso, string comentarios, int userId)
        {
            int result = -1;

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Insertar_Panel_Demanda_Masiva_Detalle");
                sp.AgregarParametro("panelId", panelId);
                sp.AgregarParametro("userEncargado", userEncargado);
                sp.AgregarParametro("fecEnvio", fecEnvio.ToString("yyyy-MM-dd HH:mm:ss"));
                sp.AgregarParametro("fecEntrega", fecEntrega.ToString("yyyy-MM-dd HH:mm:ss"));
                if (fecIngreso != null)
                {
                    DateTime fecIngresoDT = (DateTime) fecIngreso;
                    sp.AgregarParametro("fecIngreso", fecIngresoDT.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                sp.AgregarParametro("comentarios", comentarios);
                sp.AgregarParametro("user", userId);
                sp.AgregarParametro("ingresarFechaEntrega", "N");

                result = sp.EjecutarProcedimientoTrans();
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.Comprobante.GrabarPanelDemandaMasivaDetalle", userId);
                return -1;
            }

            return result;
        }

        public static int GrabarPanelDemandaMasivaCorreccionHistorial(int panelId, int usrId)
        {
            int result = -1;

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_InsertarPanelDemandaMasivaCorreccionHistorial");
                sp.AgregarParametro("panelId", panelId);
                sp.AgregarParametro("fechaEntrega", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                sp.AgregarParametro("usrId", usrId);
                sp.AgregarParametro("fechaRegistro", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                result = sp.EjecutarProcedimientoTrans();
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.Comprobante.GrabarPanelDemandaMasivaCorreccionHistorial", usrId);
                return -1;
            }

            return result;
        }

        public static int GrabarDocumento(dto.Comprobante obj, int codemp)
        {
            int ccbid = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Guardar_Cartera_Clientes_Cpbt_Doc");
                sp.AgregarParametro("ccb_codemp", codemp);
                sp.AgregarParametro("ccb_pclid", obj.Pclid);
                sp.AgregarParametro("ccb_ctcid", obj.Ctcid);
                sp.AgregarParametro("ccb_ccbid", obj.Ccbid);

                sp.AgregarParametro("ccb_tpcid", Int32.Parse(obj.TipoDocumento));
                sp.AgregarParametro("ccb_tipcart", obj.TipoCartera);
                sp.AgregarParametro("ccb_numero", obj.NumeroCpbt);

                sp.AgregarParametro("ccb_fecdoc", obj.FechaDocumento);
                sp.AgregarParametro("ccb_fecvenc", obj.FechaVencimiento);
                sp.AgregarParametro("ccb_estid", Int32.Parse(obj.EstadoCartera));
                sp.AgregarParametro("ccb_estcpbt", obj.EstadoCpbt);
                sp.AgregarParametro("ccb_codmon", obj.CodigoMoneda);
                sp.AgregarParametro("ccb_tipcambio", decimal.Parse(obj.TipoCambio.ToString()));
                sp.AgregarParametro("ccb_asignado", decimal.Parse(obj.MontoAsignado.ToString()));
                sp.AgregarParametro("ccb_monto", decimal.Parse(obj.Monto.ToString()));
                sp.AgregarParametro("ccb_saldo", decimal.Parse(obj.Saldo.ToString()));
                sp.AgregarParametro("ccb_gastjud", decimal.Parse(obj.GastoJudicial.ToString()));
                sp.AgregarParametro("ccb_gastotro", decimal.Parse(obj.GastoOtros.ToString()));
                sp.AgregarParametro("ccb_bcoid", string.IsNullOrEmpty(obj.NombreBanco) || obj.NombreBanco == "0" ? DBNull.Value : (object)obj.NombreBanco);
                if (obj.RutGirador != null)
                {
                    sp.AgregarParametro("ccb_rutgir", obj.RutGirador);
                    sp.AgregarParametro("ccb_nomgir", obj.NombreGirador);
                }
                else
                {
                    sp.AgregarParametro("ccb_rutgir", DBNull.Value);
                    sp.AgregarParametro("ccb_nomgir", DBNull.Value);
                }
                sp.AgregarParametro("ccb_mtcid", Int32.Parse(obj.MotivoCobranza));
                sp.AgregarParametro("ccb_comentario", string.IsNullOrEmpty(obj.Comentario) ? "" : obj.Comentario);
                sp.AgregarParametro("ccb_retent", DBNull.Value);
                sp.AgregarParametro("ccb_codid", string.IsNullOrEmpty(obj.CodigoCarga) ? DBNull.Value : (object)Int32.Parse(obj.CodigoCarga));
                sp.AgregarParametro("ccb_numesp", string.IsNullOrEmpty(obj.NumeroEspecial) ? "" : obj.NumeroEspecial);
                sp.AgregarParametro("ccb_numagrupa", string.IsNullOrEmpty(obj.NumeroAgrupa) ? "" : obj.NumeroAgrupa);
                sp.AgregarParametro("ccb_cctid", obj.Contrato == 0 ? 1 : obj.Contrato);

                if (obj.Sbcid == 0)
                {
                    sp.AgregarParametro("ccb_sbcid", DBNull.Value);
                }
                else
                {
                    sp.AgregarParametro("ccb_sbcid", obj.Sbcid);
                }

                sp.AgregarParametro("ccb_docori", obj.Originales);
                sp.AgregarParametro("ccb_docant", obj.Antecedentes);
                //Tercero
                if (obj.TerceroId == 0)
                {
                    sp.AgregarParametro("terceroId", DBNull.Value);
                }
                else
                {
                    sp.AgregarParametro("terceroId", obj.TerceroId);
                }
                //Cuenta
                if (string.IsNullOrEmpty(obj.IdCuenta))
                {
                    sp.AgregarParametro("CCB_IDCUENTA", DBNull.Value);
                }
                else
                {
                    sp.AgregarParametro("CCB_IDCUENTA", obj.IdCuenta);
                }
                if (string.IsNullOrEmpty(obj.DescripcionCuenta))
                {
                    sp.AgregarParametro("CCB_DESCCUENTA", DBNull.Value);
                }
                else
                {
                    sp.AgregarParametro("CCB_DESCCUENTA", obj.DescripcionCuenta);
                }

                ds = sp.EjecutarProcedimiento();
                //int error = sp.EjecutarProcedimientoTrans();

                if (ds.Tables.Count > 0)
                {
                    ccbid = Int32.Parse(ds.Tables[0].Rows[0]["ccbid"].ToString());
                    obj.Ccbid = ccbid;
                    if (!string.IsNullOrEmpty(obj.NumeroResolucion) || !string.IsNullOrEmpty(obj.NombreRepresentante1))
                    {
                        GrabarDocumentoExtension(obj, codemp);
                    }
                }
                else
                {
                    ccbid = -1;
                }
            }
            catch (Exception ex)
            {
                ccbid = -1;
            }

            return ccbid;
        }

        public static int GrabarDocumentoCarga(dto.Comprobante obj, int codemp)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Guardar_Cartera_Clientes_Cpbt_Doc");
                sp.AgregarParametro("ccb_codemp", codemp);
                sp.AgregarParametro("ccb_pclid", obj.Pclid);
                sp.AgregarParametro("ccb_ctcid", obj.Ctcid);
                sp.AgregarParametro("ccb_ccbid", obj.Ccbid);


                sp.AgregarParametro("ccb_tpcid", Int32.Parse(obj.TipoDocumento));
                sp.AgregarParametro("ccb_tipcart", obj.TipoCartera);
                sp.AgregarParametro("ccb_numero", obj.NumeroCpbt);

                sp.AgregarParametro("ccb_fecdoc", obj.FechaDocumento);
                sp.AgregarParametro("ccb_fecvenc", obj.FechaVencimiento);
                sp.AgregarParametro("ccb_estid", Int32.Parse(obj.EstadoCartera));
                sp.AgregarParametro("ccb_estcpbt", obj.EstadoCpbt);
                sp.AgregarParametro("ccb_codmon", obj.CodigoMoneda);
                sp.AgregarParametro("ccb_tipcambio", decimal.Parse(obj.TipoCambio.ToString()));
                sp.AgregarParametro("ccb_asignado", decimal.Parse(obj.MontoAsignado.ToString()));
                sp.AgregarParametro("ccb_monto", decimal.Parse(obj.Monto.ToString()));
                sp.AgregarParametro("ccb_saldo", decimal.Parse(obj.Saldo.ToString()));
                sp.AgregarParametro("ccb_gastjud", decimal.Parse(obj.GastoJudicial.ToString()));
                sp.AgregarParametro("ccb_gastotro", decimal.Parse(obj.GastoOtros.ToString()));
                sp.AgregarParametro("ccb_bcoid", (object)obj.NombreBanco ?? DBNull.Value);
                if (obj.RutGirador != null)
                {
                    sp.AgregarParametro("ccb_rutgir", obj.RutGirador);
                    sp.AgregarParametro("ccb_nomgir", obj.NombreGirador);
                }
                else
                {
                    sp.AgregarParametro("ccb_rutgir", DBNull.Value);
                    sp.AgregarParametro("ccb_nomgir", DBNull.Value);
                }
                sp.AgregarParametro("ccb_mtcid", Int32.Parse(obj.MotivoCobranza));
                sp.AgregarParametro("ccb_comentario", obj.Comentario);
                sp.AgregarParametro("ccb_retent", DBNull.Value);
                sp.AgregarParametro("ccb_codid", Int32.Parse(obj.CodigoCarga));
                sp.AgregarParametro("ccb_numesp", obj.NumeroEspecial);
                sp.AgregarParametro("ccb_numagrupa", obj.NumeroAgrupa);
                sp.AgregarParametro("ccb_cctid", obj.Contrato);
                sp.AgregarParametro("ccb_sbcid", (object)obj.SubcarteraRut ?? DBNull.Value);
                sp.AgregarParametro("ccb_docori", obj.Originales);
                sp.AgregarParametro("ccb_docant", obj.Antecedentes);
                //Tercero
                if (obj.TerceroId == 0)
                {
                    sp.AgregarParametro("terceroId", DBNull.Value);
                }
                else
                {
                    sp.AgregarParametro("terceroId", obj.TerceroId);
                }
                int error = sp.EjecutarProcedimientoTrans();

                if (ds.Tables.Count > 0)
                {
                    return Int32.Parse(ds.Tables[0].Rows[0]["ccbid"].ToString());
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }


        public static int EliminarDocumento(dto.Comprobante obj, int codemp)
        {
            try
            {
                DataSet ds = new DataSet();
                int error = 0;

                StoredProcedure sp = new StoredProcedure("Delete_Cartera_Clientes_Cpbt_Doc");
                sp.AgregarParametro("ccb_codemp", codemp);
                sp.AgregarParametro("ccb_pclid", obj.Pclid);
                sp.AgregarParametro("ccb_ctcid", obj.Ctcid);
                sp.AgregarParametro("ccb_ccbid", obj.Ccbid);
                error = sp.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                return -1;
            }

        }

        public static int EliminarSubcartera(int id, int codemp)
        {
            try
            {
                DataSet ds = new DataSet();
                int error = 0;

                StoredProcedure sp = new StoredProcedure("_Delete_Subcartera");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("id", id);
                error = sp.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                return -1;
            }

        }

        public static int DescartarDocumento(dto.Comprobante obj, int codemp)
        {
            try
            {
                DataSet ds = new DataSet();
                int error = 0;
                StoredProcedure sp = new StoredProcedure("Update_Cartera_Clientes_Cpbt_Doc_Elimina");
                sp.AgregarParametro("ccb_codemp", codemp);
                sp.AgregarParametro("ccb_pclid", obj.Pclid);
                sp.AgregarParametro("ccb_ctcid", obj.Ctcid);
                sp.AgregarParametro("ccb_ccbid", obj.Ccbid);
                error = sp.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                return -1;
            }

        }

        public static List<Combobox> ListarTipoDocumento(int codemp, int idioma)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Tipos_Documentos_Deudor");
                sp.AgregarParametro("clb_codemp", codemp);
                sp.AgregarParametro("tci_idid", idioma);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Dimol.dto.Combobox()
                        {
                            Text = ds.Tables[0].Rows[i]["tci_nombre"].ToString(),
                            Value = ds.Tables[0].Rows[i]["tci_tpcid"].ToString()
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


        public static List<Dimol.dto.Combobox> ListarImagenesCpbt(int codemp, int pclid, int ctcid)
        {
            List<Dimol.dto.Combobox> lst = new List<Dimol.dto.Combobox>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Imagenes_Deudor_Cliente");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Dimol.dto.Combobox()
                        {
                            Value = ds.Tables[0].Rows[i]["rutaArchivo"].ToString(),
                            Text = ds.Tables[0].Rows[i]["texto"].ToString()
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

        public static int BuscarMotivoCobranza(int codemp, int idioma, string motivo)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Buscar_Motivo_Cobranza");

                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idioma", idioma);
                sp.AgregarParametro("nombre", motivo.ToUpper());
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    return Int32.Parse(ds.Tables[0].Rows[0]["mtcid"].ToString());
                }
                else
                {
                    return -1;
                }

            }
            catch (Exception ex)
            {
                return -1;
            }

        }

        public static List<dto.DocumentoAnterior> ListarDocumentosAnteriores(int codemp, int pclid, int ctcid)
        {
            List<dto.DocumentoAnterior> lst = new List<dto.DocumentoAnterior>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Ultimo_Dcto");
                sp.AgregarParametro("ccb_codemp", codemp);
                sp.AgregarParametro("ccb_pclid", pclid);
                sp.AgregarParametro("ccb_ctcid", ctcid);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.DocumentoAnterior()
                        {
                            Ccbid = Int32.Parse(ds.Tables[0].Rows[i]["ccb_ccbid"].ToString()),
                            Numero = ds.Tables[0].Rows[i]["ccb_numero"].ToString(),
                            EstadoCpbt = ds.Tables[0].Rows[i]["ccb_estcpbt"].ToString(),
                            CodigoCarga = ds.Tables[0].Rows[i]["ccb_codid"].ToString(),
                            EstadoId = Int32.Parse(ds.Tables[0].Rows[i]["ccb_estid"].ToString())
                        });
                    }
                }
                else
                {
                    lst.Add(new dto.DocumentoAnterior()
                    {
                        Ccbid = 0,
                        Numero = "0",
                        EstadoCpbt = "V",
                        CodigoCarga = null,
                        EstadoId = 1
                    });
                }

                return lst;
            }
            catch (Exception ex)
            {
                return lst;
            }

        }

        public static int ActualizarDocumento(dto.DocumentoAnterior obj, int codemp, int pclid, int ctcid)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Update_Cartera_Clientes_Cpbt_Doc_Cod_Carga");
                sp.AgregarParametro("ccb_codemp", codemp);
                sp.AgregarParametro("ccb_pclid", pclid);
                sp.AgregarParametro("ccb_ctcid", ctcid);
                sp.AgregarParametro("ccb_ccbid", obj.Ccbid);
                sp.AgregarParametro("ccb_estid", obj.EstadoId);
                sp.AgregarParametro("ccb_codid", Int32.Parse(obj.CodigoCarga));
                sp.AgregarParametro("ccb_estcpbt", "V");
                int error = sp.EjecutarProcedimientoTrans();

                return error;


            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static int ActualizarDocumentoFinalizado(dto.DocumentoAnterior obj, int codemp, int pclid, int ctcid)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Update_Cartera_Clientes_Cpbt_Doc_Cod_Carga_F");
                sp.AgregarParametro("ccb_codemp", codemp);
                sp.AgregarParametro("ccb_pclid", pclid);
                sp.AgregarParametro("ccb_ctcid", ctcid);
                sp.AgregarParametro("ccb_ccbid", obj.Ccbid);
                sp.AgregarParametro("ccb_estid", obj.EstadoId);
                sp.AgregarParametro("ccb_codid", Int32.Parse(obj.CodigoCarga));
                sp.AgregarParametro("ccb_estcpbt", "V");
                int error = sp.EjecutarProcedimientoTrans();

                return error;


            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static int InsertarHistorial(int codemp, int pclid, int ctcid, int ccbid, int tipoDocumento, int codsuc, int gesid, string ipRed, string ipMaquina, string comentario, decimal monto, decimal saldo, int usuario, DateTime fechaVencimiento)
        {
            Funciones objFunc = new Funciones();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Insertar_Cartera_Clientes_Estados_Historial");
                sp.AgregarParametro("ceh_codemp", codemp);
                sp.AgregarParametro("ceh_pclid", pclid);
                sp.AgregarParametro("ceh_ctcid", ctcid);
                sp.AgregarParametro("ceh_ccbid", ccbid);
                if (tipoDocumento == 1)
                {
                    if (fechaVencimiento > DateTime.Parse(objFunc.FechaServer()))
                    {
                        sp.AgregarParametro("ceh_estid", objFunc.ConfiguracionEmpNum(codemp, 100));
                    }
                    else
                    {
                        sp.AgregarParametro("ceh_estid", objFunc.ConfiguracionEmpNum(codemp, 94));
                    }
                }
                else
                {
                    sp.AgregarParametro("ceh_estid", objFunc.ConfiguracionEmpNum(codemp, 17));
                }
                sp.AgregarParametro("ceh_sucid", codsuc);
                sp.AgregarParametro("ceh_gesid", DBNull.Value);
                sp.AgregarParametro("ceh_ipred", ipRed);
                sp.AgregarParametro("ceh_ipmaquina", ipMaquina);
                sp.AgregarParametro("ceh_comentario", comentario);
                sp.AgregarParametro("ceh_monto", monto);
                sp.AgregarParametro("ceh_saldo", saldo);
                sp.AgregarParametro("ceh_usrid", usuario);
                int error = sp.EjecutarProcedimientoTrans();

                return error;


            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static int InsertarHistorialCarga(int codemp, int pclid, int ctcid, int ccbid, int estid, int codsuc, int? gesid, string ipRed, string ipMaquina, string comentario, decimal monto, decimal saldo, int usuario, DateTime fechaVencimiento)
        {
            Funciones objFunc = new Funciones();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Insertar_Cartera_Clientes_Estados_Historial");
                sp.AgregarParametro("ceh_codemp", codemp);
                sp.AgregarParametro("ceh_pclid", pclid);
                sp.AgregarParametro("ceh_ctcid", ctcid);
                sp.AgregarParametro("ceh_ccbid", ccbid);
                sp.AgregarParametro("ceh_estid", estid);
                sp.AgregarParametro("ceh_sucid", codsuc);
                sp.AgregarParametro("ceh_gesid", gesid == null ? DBNull.Value : (object)gesid);
                sp.AgregarParametro("ceh_ipred", ipRed);
                sp.AgregarParametro("ceh_ipmaquina", ipMaquina);
                sp.AgregarParametro("ceh_comentario", comentario);
                sp.AgregarParametro("ceh_monto", monto);
                sp.AgregarParametro("ceh_saldo", saldo);
                sp.AgregarParametro("ceh_usrid", usuario);
                int error = sp.EjecutarProcedimientoTrans();

                return error;


            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static int BuscarCarteraCliente(int codemp, int pclid, int ctcid)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Find_Cartera_Clientes");

                sp.AgregarParametro("ctc_codemp", codemp);
                sp.AgregarParametro("ctc_pclid", pclid);
                sp.AgregarParametro("ctc_ctcid", ctcid);

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        return Int32.Parse(ds.Tables[0].Rows[0][0].ToString());
                    }
                    else { return 0; }
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public static int InsertarCarteraCliente(int codemp, int pclid, int ctcid)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Insertar_Cartera_Clientes");

                sp.AgregarParametro("ctc_codemp", codemp);
                sp.AgregarParametro("ctc_pclid", pclid);
                sp.AgregarParametro("ctc_ctcid", ctcid);
                int error = sp.EjecutarProcedimientoTrans();

                return error;


            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static int TraeBanco(int codemp, string nombre)
        {
            int id = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Banco");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("nombre", nombre);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    id = Int32.Parse(ds.Tables[0].Rows[0]["bcoid"].ToString());
                }

                return id;
            }
            catch (Exception ex)
            {
                return id;
            }
        }

        public static int TraeCodigoCarga(int codemp, int pclid, string codigoCarga)
        {
            int id = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Codigo_Carga");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("codigo_carga", codigoCarga);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    id = Int32.Parse(ds.Tables[0].Rows[0]["codid"].ToString());
                }

                return id;
            }
            catch (Exception ex)
            {
                return id;
            }
        }

        public static int BuscarSubcartera(int codemp, string rut)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Buscar_Subcartera");

                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("rut", rut);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    return Int32.Parse(ds.Tables[0].Rows[0]["sbcid"].ToString());
                }
                else
                {
                    return -1;
                }

            }
            catch (Exception ex)
            {
                return -1;
            }

        }

        public static int BuscarTribunal(int codemp, string rut)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Buscar_Tribunal");

                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("rut", rut);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    return Int32.Parse(ds.Tables[0].Rows[0]["trbid"].ToString());
                }
                else
                {
                    return -1;
                }

            }
            catch (Exception ex)
            {
                return -1;
            }

        }

        public static int BuscarTipoCausa(int codemp, string tipoCausa)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Buscar_Tipo_Causa");

                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("tipo_causa", tipoCausa);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    return Int32.Parse(ds.Tables[0].Rows[0]["tcaid"].ToString());
                }
                else
                {
                    return -1;
                }

            }
            catch (Exception ex)
            {
                return -1;
            }

        }

        public static int BuscarEnteJudicial(int codemp, string rut)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Buscar_Ente_Judicial");

                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("rut", rut);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    return Int32.Parse(ds.Tables[0].Rows[0][0].ToString());
                }
                else
                {
                    return -1;
                }

            }
            catch (Exception ex)
            {
                return -1;
            }

        }

        public static int TraeUltimoRol(int codemp)
        {
            int id = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("UltNum_Rol");
                sp.AgregarParametro("rol_codemp", codemp);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    id = Int32.Parse(ds.Tables[0].Rows[0][0].ToString());
                }

                return id;
            }
            catch (Exception ex)
            {
                return id;
            }
        }

        public static int TraeRolClienteDeudor(int codemp, int pclid, int ctcid)
        {
            int id = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Buscar_Rol_Cliente_Deudor");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    id = Int32.Parse(ds.Tables[0].Rows[0]["rolid"].ToString());
                }

                return id;
            }
            catch (Exception ex)
            {
                return id;
            }
        }

        public static int InsertarRol(int codemp, int rolid, int pclid, int ctcid, int tribunal, int tipoCausa, int estid, DateTime fechaDemanda, DateTime fechaRol, string comentario, int materiaJudicial, DateTime fechaJudicial)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Insertar_Rol");

                sp.AgregarParametro("rol_codemp", codemp);
                sp.AgregarParametro("rol_rolid", rolid);
                sp.AgregarParametro("rol_pclid", pclid);
                sp.AgregarParametro("rol_ctcid", ctcid);
                sp.AgregarParametro("rol_trbid", tribunal);
                sp.AgregarParametro("rol_tcaid", tipoCausa);
                sp.AgregarParametro("rol_estid", estid);
                sp.AgregarParametro("rol_fecdem", fechaDemanda == new DateTime() ? DBNull.Value : (object)fechaDemanda);
                sp.AgregarParametro("rol_fecrol", fechaRol == new DateTime() ? DBNull.Value : (object)fechaRol);
                sp.AgregarParametro("rol_comentario", comentario);
                sp.AgregarParametro("rol_esjid", materiaJudicial);
                sp.AgregarParametro("rol_fecjud", fechaJudicial);
                sp.AgregarParametro("rol_numero", "PRO" + rolid.ToString());
                int error = sp.EjecutarProcedimientoTrans();

                return error;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static int InsertarRolEstados(int codemp, int rolid, int estid, int materiaJudicial, int usuario, string ipRed, string ipMaquina, string comentario, DateTime fechaJudicial)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Insertar_Rol_Estados");
                sp.AgregarParametro("rle_codemp", codemp);
                sp.AgregarParametro("rle_rolid", rolid);
                sp.AgregarParametro("rle_estid", estid);
                sp.AgregarParametro("rle_esjid", materiaJudicial);
                sp.AgregarParametro("rle_usrid", usuario);
                sp.AgregarParametro("rle_ipred", ipRed);
                sp.AgregarParametro("rle_ipmaquina", ipMaquina);
                sp.AgregarParametro("rle_comentario", comentario);
                sp.AgregarParametro("rle_fecjud", fechaJudicial);
                int error = sp.EjecutarProcedimientoTrans();

                return error;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static int InsertarDocumentosRol(int codemp, int rolid, int pclid, int ctcid, int ccbid, decimal monto, decimal saldo)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Insertar_Rol_Documentos");
                sp.AgregarParametro("rdc_codemp", codemp);
                sp.AgregarParametro("rdc_rolid", rolid);
                sp.AgregarParametro("rdc_pclid", pclid);
                sp.AgregarParametro("rdc_ctcid", ctcid);
                sp.AgregarParametro("rdc_ccbid", ccbid);
                sp.AgregarParametro("rdc_monto", monto);
                sp.AgregarParametro("rdc_saldo", saldo);
                int error = sp.EjecutarProcedimientoTrans();

                return error;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static int ExisteEnteRol(int codemp, int rolid, int etjid)
        {
            int id = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Existe_Ente_Rol");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("rolid", rolid);
                sp.AgregarParametro("etjid", etjid);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    id = Int32.Parse(ds.Tables[0].Rows[0][0].ToString());
                }

                return id;
            }
            catch (Exception ex)
            {
                return id;
            }
        }

        public static int InsertarEnteRol(int codemp, int rolid, int etjid)
        {
            int error = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Insertar_EnteJud_Rol");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("rolid", rolid);
                sp.AgregarParametro("etjid", etjid);
                error = sp.EjecutarProcedimientoTrans();

                return error;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static int ExisteDemandaAvenimientoRol(int codemp, int rolid)
        {
            int id = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Existe_Aven_Dem_Rol");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("rolid", rolid);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    id = Int32.Parse(ds.Tables[0].Rows[0][0].ToString());
                }

                return id;
            }
            catch (Exception ex)
            {
                return id;
            }
        }

        public static int InsertarDemandaAvenimientoRol(dto.DemandaAvenimientoRol obj)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Insertar_Rol_AveDem");
                sp.AgregarParametro("rad_codemp", obj.Codemp);
                sp.AgregarParametro("rad_rolid", obj.Rolid);

                sp.AgregarParametro("rad_fecdem", obj.FechaDemanda == new DateTime() ? DBNull.Value : (object)obj.FechaDemanda);
                sp.AgregarParametro("rad_cuodem", obj.CuotasDemanda);
                sp.AgregarParametro("rad_mondem", obj.MontoDemanda);
                sp.AgregarParametro("rad_monpcuodem", obj.MontoPrimeraCuotaDemanda);
                sp.AgregarParametro("rad_monucoudem", obj.MontoUltimaCuotaDemanda);
                sp.AgregarParametro("rad_fecpcoudem", obj.FechaPrimeraCuotaDemanda == new DateTime() ? DBNull.Value : (object)obj.FechaPrimeraCuotaDemanda);
                sp.AgregarParametro("rad_fecucoudem", obj.FechaUltimaCuotaDemanda == new DateTime() ? DBNull.Value : (object)obj.FechaUltimaCuotaDemanda);
                sp.AgregarParametro("rad_intdem", obj.InteresDemanda);
                sp.AgregarParametro("rad_fecave", obj.FechaAvenimiento == new DateTime() ? DBNull.Value : (object)obj.FechaAvenimiento);
                sp.AgregarParametro("rad_cuoave", obj.CuotasAvenimiento);
                sp.AgregarParametro("rad_monave", obj.MontoAvenimiento);
                sp.AgregarParametro("rad_monpcouave", obj.MontoPrimeraCuotaAvenimiento);
                sp.AgregarParametro("rad_monucouave", obj.MontoUltimaCuotaAvenimiento);
                sp.AgregarParametro("rad_fecpcouave", obj.FechaPrimeraCuotaAvenimiento == new DateTime() ? DBNull.Value : (object)obj.FechaPrimeraCuotaAvenimiento);
                sp.AgregarParametro("rad_fecucouave", obj.FechaUltimaCuotaAvenimiento == new DateTime() ? DBNull.Value : (object)obj.FechaUltimaCuotaAvenimiento);
                sp.AgregarParametro("rad_intave", obj.InteresAvenimiento);

                int error = sp.EjecutarProcedimientoTrans();

                return error;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        #region "Carga Masiva Pagos"

        public static int ActualizarCpbtValoresEspecial(dto.Comprobante cpbt, int codemp)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Update_Cartera_Clientes_Cpbt_Doc_Valores_Especial");
                sp.AgregarParametro("ccb_codemp", codemp);
                sp.AgregarParametro("ccb_pclid", cpbt.Pclid);
                sp.AgregarParametro("ccb_ctcid", cpbt.Ctcid);
                sp.AgregarParametro("ccb_ccbid", cpbt.Ccbid);
                sp.AgregarParametro("ccb_saldo", cpbt.Saldo);
                sp.AgregarParametro("ccb_gastjud", cpbt.GastoJudicial);
                sp.AgregarParametro("ccb_gastotro", cpbt.GastoOtros);
                sp.AgregarParametro("ccb_intereses", cpbt.Intereses);
                sp.AgregarParametro("ccb_honorarios", cpbt.Honorarios);
                sp.AgregarParametro("ccb_estid", cpbt.EstadoCartera);
                if (cpbt.FechaCalculoInteres == new DateTime() || cpbt.FechaCalculoInteres == null)
                {
                    sp.AgregarParametro("ccb_feccalcint", cpbt.FechaCalculoInteres);
                }
                else
                {
                    sp.AgregarParametro("ccb_feccalcint", DBNull.Value);
                }
                sp.AgregarParametro("ccb_estcpbt", cpbt.EstadoCpbt);
                sp.AgregarParametro("ccb_tipcambio", cpbt.TipoCambio);
                sp.AgregarParametro("ccb_codid", cpbt.CodigoCarga);
                sp.AgregarParametro("ccb_fecvenc", cpbt.FechaVencimiento);
                int error = sp.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        #endregion

        #region "Comprobante"

        public static List<Combobox> ListarTipoDocumento(int codemp, int idioma, int perfil, string tipo, string first)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure();
                switch (tipo)
                {
                    case "CC":
                        sp = new StoredProcedure("Trae_Tipos_Cpbt_Perfil_Tipo");
                        sp.AgregarParametro("clb_codemp", codemp);
                        sp.AgregarParametro("clb_tipcpbtdoc", "V");
                        sp.AgregarParametro("pfc_prfid", perfil);
                        sp.AgregarParametro("tci_idid", idioma);
                        sp.AgregarParametro("clb_cartcli", "S");
                        break;
                    case "V":
                        sp = new StoredProcedure("Trae_Tipos_Cpbt_Perfil_Tipo_Especial");
                        sp.AgregarParametro("clb_codemp", codemp);
                        sp.AgregarParametro("clb_tipcpbtdoc", "V");
                        sp.AgregarParametro("pfc_prfid", perfil);
                        sp.AgregarParametro("tci_idid", idioma);
                        sp.AgregarParametro("clb_cartcli", "N");
                        break;
                    case "C":
                        sp = new StoredProcedure("Trae_Tipos_Cpbt_Perfil_Tipo_Especial");
                        sp.AgregarParametro("clb_codemp", codemp);
                        sp.AgregarParametro("clb_tipcpbtdoc", "C");
                        sp.AgregarParametro("pfc_prfid", perfil);
                        sp.AgregarParametro("tci_idid", idioma);
                        sp.AgregarParametro("clb_cartcli", "N");
                        break;
                    case "G":
                        sp = new StoredProcedure("_Trae_Tipos_Cpbt_Perfil_Tipo_Cast_Dev");
                        sp.AgregarParametro("clb_codemp", codemp);
                        sp.AgregarParametro("clb_tipcpbtdoc", "V");
                        sp.AgregarParametro("pfc_prfid", perfil);
                        sp.AgregarParametro("tci_idid", idioma);
                        sp.AgregarParametro("clb_cartcli", "S");
                        break;
                }
                ds = sp.EjecutarProcedimiento();
                if (!string.IsNullOrEmpty(first))
                {
                    lst.Add(new Combobox()
                    {
                        Value = "",
                        Text = first
                    });
                }
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Dimol.dto.Combobox()
                        {
                            Text = ds.Tables[0].Rows[i]["tci_nombre"].ToString(),
                            Value = ds.Tables[0].Rows[i]["tpc_tpcid"].ToString()
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

        public static List<Combobox> ListarEstadosComprobante(int idioma)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Etiquetas");
                for (int i = 1; i < 7; i++)
                {
                    sp = new StoredProcedure("Trae_Etiquetas");
                    sp.AgregarParametro("codigo", "EstCab" + i);
                    sp.AgregarParametro("idioma", idioma);
                    ds = sp.EjecutarProcedimiento();

                    lst.Add(new Combobox()
                    {
                        Text = ds.Tables[0].Rows[0][0].ToString(),
                        Value = i.ToString()
                    });

                }
                return lst;
            }
            catch (Exception ex)
            {
                return lst;
            }

        }

        public static List<Combobox> ListarEstadosComprobante(int idioma, string first)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {
                if (!string.IsNullOrEmpty(first))
                {
                    lst.Add(new Combobox()
                    {
                        Text = first,
                        Value = ""
                    });
                }
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Etiquetas");
                for (int i = 1; i < 7; i++)
                {
                    sp = new StoredProcedure("Trae_Etiquetas");
                    sp.AgregarParametro("codigo", "EstCab" + i);
                    sp.AgregarParametro("idioma", idioma);
                    ds = sp.EjecutarProcedimiento();

                    lst.Add(new Combobox()
                    {
                        Text = ds.Tables[0].Rows[0][0].ToString(),
                        Value = i.ToString()
                    });

                }
                return lst;
            }
            catch (Exception ex)
            {
                return lst;
            }

        }

        public static List<Combobox> ListarMonedas(int codemp, string first)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {
                if (!string.IsNullOrEmpty(first))
                {
                    lst.Add(new Combobox()
                    {
                        Text = first,
                        Value = ""
                    });
                }
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Monedas");
                sp.AgregarParametro("codemp", codemp);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Dimol.dto.Combobox()
                        {
                            Text = ds.Tables[0].Rows[i]["Descripcion"].ToString(),
                            Value = ds.Tables[0].Rows[i]["Id"].ToString()
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

        public static List<dto.BuscarComprobante> ListarComprobanteCartera(int codemp, int idioma, int codsuc, int tipoDocumento, int numero, int pclid, string emisionDesde, string emisionHasta, DateTime? vencimientoDesde, DateTime? vencimientoHasta, int montoDesde, int montoHasta, string rut, string nombreFantasia, string telefono, string email, string direccion, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.BuscarComprobante> lst = new List<dto.BuscarComprobante>();
            DateTime fecha = new DateTime();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Comprobante_Cartera_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idioma", idioma);
                sp.AgregarParametro("codsuc", codsuc);
                sp.AgregarParametro("tipo", tipoDocumento);
                sp.AgregarParametro("numero", numero);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("emision_desde", emisionDesde == null ? DBNull.Value : (object)emisionDesde);
                sp.AgregarParametro("emision_hasta", emisionHasta == null ? DBNull.Value : (object)emisionHasta);
                sp.AgregarParametro("vencimiento_desde", vencimientoDesde == null ? DBNull.Value : (object)vencimientoDesde);
                sp.AgregarParametro("vencimiento_hasta", vencimientoHasta == null ? DBNull.Value : (object)vencimientoHasta);
                sp.AgregarParametro("monto_desde", montoDesde);
                sp.AgregarParametro("monto_hasta", montoHasta);
                sp.AgregarParametro("rut", (object)rut ?? DBNull.Value);
                sp.AgregarParametro("nombre_fantasia", (object)nombreFantasia ?? DBNull.Value);
                sp.AgregarParametro("telefono", (object)telefono ?? DBNull.Value);
                sp.AgregarParametro("email", (object)email ?? DBNull.Value);
                sp.AgregarParametro("direccion", (object)direccion ?? DBNull.Value);
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
                        lst.Add(new dto.BuscarComprobante()
                        {
                            IdTipoDocumento = Int32.Parse(ds.Tables[0].Rows[i]["IdTipoDocumento"].ToString()),
                            Numero = Int32.Parse(ds.Tables[0].Rows[i]["Numero"].ToString()),
                            Rut = ds.Tables[0].Rows[i]["Rut"].ToString(),
                            NombreFantasia = ds.Tables[0].Rows[i]["NombreFantasia"].ToString(),
                            TipoDocumento = ds.Tables[0].Rows[i]["TipoDocumento"].ToString(),
                            NumeroCliente = ds.Tables[0].Rows[i]["NumeroCliente"].ToString(),
                            Final = decimal.Parse(ds.Tables[0].Rows[i]["Final"].ToString()),
                            IdEstado = ds.Tables[0].Rows[i]["IdEstado"].ToString(),
                            NumFin = Int32.Parse(ds.Tables[0].Rows[i]["NumFin"].ToString()),
                            Saldo = decimal.Parse(ds.Tables[0].Rows[i]["Saldo"].ToString()),
                            Estado = ds.Tables[0].Rows[i]["Estado"].ToString(),
                            FechaEmision = DateTime.TryParse(ds.Tables[0].Rows[i]["FechaEmision"].ToString(), out fecha) ? DateTime.Parse(ds.Tables[0].Rows[i]["FechaEmision"].ToString()) : new DateTime(),
                            FechaVencimiento = DateTime.TryParse(ds.Tables[0].Rows[i]["FechaVencimiento"].ToString(), out fecha) ? DateTime.Parse(ds.Tables[0].Rows[i]["FechaVencimiento"].ToString()) : new DateTime()
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

        public static int ListarComprobanteCarteraCount(int codemp, int idioma, int codsuc, int tipoDocumento, int numero, int pclid, string emisionDesde, string emisionHasta, DateTime? vencimientoDesde, DateTime? vencimientoHasta, int montoDesde, int montoHasta, string rut, string nombreFantasia, string telefono, string email, string direccion, string where, string sidx, string sord, int inicio, int limite)
        {
            int count = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Comprobante_Cartera_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idioma", idioma);
                sp.AgregarParametro("codsuc", codsuc);
                sp.AgregarParametro("tipo", tipoDocumento);
                sp.AgregarParametro("numero", numero);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("emision_desde", emisionDesde == null ? DBNull.Value : (object)emisionDesde);
                sp.AgregarParametro("emision_hasta", emisionHasta == null ? DBNull.Value : (object)emisionHasta);
                sp.AgregarParametro("vencimiento_desde", vencimientoDesde == null ? DBNull.Value : (object)vencimientoDesde);
                sp.AgregarParametro("vencimiento_hasta", vencimientoHasta == null ? DBNull.Value : (object)vencimientoHasta);
                sp.AgregarParametro("monto_desde", montoDesde);
                sp.AgregarParametro("monto_hasta", montoHasta);

                sp.AgregarParametro("rut", (object)rut ?? DBNull.Value);
                sp.AgregarParametro("nombre_fantasia", (object)nombreFantasia ?? DBNull.Value);
                sp.AgregarParametro("telefono", (object)telefono ?? DBNull.Value);
                sp.AgregarParametro("email", (object)email ?? DBNull.Value);
                sp.AgregarParametro("direccion", (object)direccion ?? DBNull.Value);

                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    count = Int32.Parse(ds.Tables[0].Rows[0]["count"].ToString());
                }

                return count;
            }
            catch (Exception ex)
            {
                return count;
            }
        }

        public static List<dto.BuscarComprobante> ListarComprobanteCompra(int codemp, int idioma, int codsuc, int tipoDocumento, int numero, int pclid, string emisionDesde, string emisionHasta, string estado, int ctcid, int trbid, string rol, string moneda, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.BuscarComprobante> lst = new List<dto.BuscarComprobante>();
            DateTime fecha = new DateTime();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Comprobante_Compra_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idioma", idioma);
                sp.AgregarParametro("codsuc", codsuc);
                sp.AgregarParametro("tipo", tipoDocumento);
                sp.AgregarParametro("numero", numero == 0 ? DBNull.Value : (object)numero);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("emision_desde", emisionDesde == null ? DBNull.Value : (object)emisionDesde);
                sp.AgregarParametro("emision_hasta", emisionHasta == null ? DBNull.Value : (object)emisionHasta);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("trbid", trbid);
                sp.AgregarParametro("rol", rol == null ? DBNull.Value : (object)rol);
                //sp.AgregarParametro("vencimiento_desde", vencimientoDesde == null ? DBNull.Value : (object)vencimientoDesde);
                //sp.AgregarParametro("vencimiento_hasta", vencimientoHasta == null ? DBNull.Value : (object)vencimientoHasta);
                //sp.AgregarParametro("monto_desde", montoDesde);
                //sp.AgregarParametro("monto_hasta", montoHasta);
                sp.AgregarParametro("estado", (object)estado ?? DBNull.Value);
                //sp.AgregarParametro("moneda", (object)moneda ?? DBNull.Value);
                //sp.AgregarParametro("numero_interno", (object)numeroInterno ?? DBNull.Value);
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
                        lst.Add(new dto.BuscarComprobante()
                        {
                            IdTipoDocumento = Int32.Parse(ds.Tables[0].Rows[i]["IdTipoDocumento"].ToString()),
                            Numero = Int32.Parse(ds.Tables[0].Rows[i]["Numero"].ToString()),
                            Rut = ds.Tables[0].Rows[i]["Rut"].ToString(),
                            NombreFantasia = ds.Tables[0].Rows[i]["NombreFantasia"].ToString(),
                            TipoDocumento = ds.Tables[0].Rows[i]["TipoDocumento"].ToString(),
                            NumeroCliente = ds.Tables[0].Rows[i]["NumeroCliente"].ToString(),
                            Final = decimal.Parse(ds.Tables[0].Rows[i]["Final"].ToString()),
                            IdEstado = ds.Tables[0].Rows[i]["IdEstado"].ToString(),
                            NumFin = Int32.Parse(ds.Tables[0].Rows[i]["NumFin"].ToString()),
                            Saldo = decimal.Parse(ds.Tables[0].Rows[i]["Saldo"].ToString()),
                            Estado = ds.Tables[0].Rows[i]["Estado"].ToString(),
                            FechaEmision = DateTime.TryParse(ds.Tables[0].Rows[i]["FechaEmision"].ToString(), out fecha) ? DateTime.Parse(ds.Tables[0].Rows[i]["FechaEmision"].ToString()) : new DateTime(),
                            FechaVencimiento = DateTime.TryParse(ds.Tables[0].Rows[i]["FechaVencimiento"].ToString(), out fecha) ? DateTime.Parse(ds.Tables[0].Rows[i]["FechaVencimiento"].ToString()) : new DateTime(),
                            RutDeudor = ds.Tables[0].Rows[i]["RutDeudor"].ToString(),
                            NombreDeudor = ds.Tables[0].Rows[i]["NombreDeudor"].ToString(),
                            Rol = ds.Tables[0].Rows[i]["Rol"].ToString(),
                            Tribunal = ds.Tables[0].Rows[i]["Tribunal"].ToString()
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

        public static int ListarComprobanteCompraCount(int codemp, int idioma, int codsuc, int tipoDocumento, int numero, int pclid, string emisionDesde, string emisionHasta, string estado, int ctcid, int trbid, string rol, string moneda, string where, string sidx, string sord, int inicio, int limite)
        {
            int count = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Comprobante_Compra_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idioma", idioma);
                sp.AgregarParametro("codsuc", codsuc);
                sp.AgregarParametro("tipo", tipoDocumento);
                sp.AgregarParametro("numero", numero == 0 ? DBNull.Value : (object)numero);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("emision_desde", emisionDesde == null ? DBNull.Value : (object)emisionDesde);
                sp.AgregarParametro("emision_hasta", emisionHasta == null ? DBNull.Value : (object)emisionHasta);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("trbid", trbid);
                sp.AgregarParametro("rol", rol == null ? DBNull.Value : (object)rol);
                //sp.AgregarParametro("vencimiento_desde", vencimientoDesde == null ? DBNull.Value : (object)vencimientoDesde);
                //sp.AgregarParametro("vencimiento_hasta", vencimientoHasta == null ? DBNull.Value : (object)vencimientoHasta);
                //sp.AgregarParametro("monto_desde", montoDesde);
                //sp.AgregarParametro("monto_hasta", montoHasta);

                sp.AgregarParametro("estado", (object)estado ?? DBNull.Value);
                //sp.AgregarParametro("moneda", (object)moneda ?? DBNull.Value);
                //sp.AgregarParametro("numero_interno", (object)numeroInterno ?? DBNull.Value);

                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    count = Int32.Parse(ds.Tables[0].Rows[0]["count"].ToString());
                }

                return count;
            }
            catch (Exception ex)
            {
                return count;
            }
        }

        public static List<dto.BuscarComprobante> ListarComprobanteVenta(int codemp, int idioma, int codsuc, int tipoDocumento, int numero, int pclid, string emisionDesde, string emisionHasta, DateTime? vencimientoDesde, DateTime? vencimientoHasta, int montoDesde, int montoHasta, string estado, string moneda, string numeroInterno, string producto, string comentario, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.BuscarComprobante> lst = new List<dto.BuscarComprobante>();
            DateTime fecha = new DateTime();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Comprobante_Venta_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idioma", idioma);
                sp.AgregarParametro("codsuc", codsuc);
                sp.AgregarParametro("tipo", tipoDocumento);
                sp.AgregarParametro("numero", numero);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("emision_desde", emisionDesde == null ? DBNull.Value : (object)emisionDesde);
                sp.AgregarParametro("emision_hasta", emisionHasta == null ? DBNull.Value : (object)emisionHasta);
                sp.AgregarParametro("vencimiento_desde", vencimientoDesde == null ? DBNull.Value : (object)vencimientoDesde);
                sp.AgregarParametro("vencimiento_hasta", vencimientoHasta == null ? DBNull.Value : (object)vencimientoHasta);
                sp.AgregarParametro("monto_desde", montoDesde);
                sp.AgregarParametro("monto_hasta", montoHasta);
                sp.AgregarParametro("estado", (object)estado ?? DBNull.Value);
                sp.AgregarParametro("moneda", (object)moneda ?? DBNull.Value);
                sp.AgregarParametro("numero_interno", (object)numeroInterno ?? DBNull.Value);
                sp.AgregarParametro("producto", (object)producto ?? DBNull.Value);
                sp.AgregarParametro("comentario", (object)comentario ?? DBNull.Value);
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
                        lst.Add(new dto.BuscarComprobante()
                        {
                            IdTipoDocumento = Int32.Parse(ds.Tables[0].Rows[i]["IdTipoDocumento"].ToString()),
                            Numero = Int32.Parse(ds.Tables[0].Rows[i]["Numero"].ToString()),
                            Rut = ds.Tables[0].Rows[i]["Rut"].ToString(),
                            NombreFantasia = ds.Tables[0].Rows[i]["NombreFantasia"].ToString(),
                            TipoDocumento = ds.Tables[0].Rows[i]["TipoDocumento"].ToString(),
                            NumeroCliente = ds.Tables[0].Rows[i]["NumeroCliente"].ToString(),
                            Final = decimal.Parse(ds.Tables[0].Rows[i]["Final"].ToString()),
                            IdEstado = ds.Tables[0].Rows[i]["IdEstado"].ToString(),
                            NumFin = Int32.Parse(ds.Tables[0].Rows[i]["NumFin"].ToString()),
                            Saldo = decimal.Parse(ds.Tables[0].Rows[i]["Saldo"].ToString()),
                            Estado = ds.Tables[0].Rows[i]["Estado"].ToString(),
                            FechaEmision = DateTime.TryParse(ds.Tables[0].Rows[i]["FechaEmision"].ToString(), out fecha) ? DateTime.Parse(ds.Tables[0].Rows[i]["FechaEmision"].ToString()) : new DateTime(),
                            FechaVencimiento = DateTime.TryParse(ds.Tables[0].Rows[i]["FechaVencimiento"].ToString(), out fecha) ? DateTime.Parse(ds.Tables[0].Rows[i]["FechaVencimiento"].ToString()) : new DateTime()
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

        public static int ListarComprobanteVentaCount(int codemp, int idioma, int codsuc, int tipoDocumento, int numero, int pclid, string emisionDesde, string emisionHasta, DateTime? vencimientoDesde, DateTime? vencimientoHasta, int montoDesde, int montoHasta, string estado, string moneda, string numeroInterno, string producto, string comentario, string where, string sidx, string sord, int inicio, int limite)
        {
            int count = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Comprobante_Venta_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idioma", idioma);
                sp.AgregarParametro("codsuc", codsuc);
                sp.AgregarParametro("tipo", tipoDocumento);
                sp.AgregarParametro("numero", numero);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("emision_desde", emisionDesde == null ? DBNull.Value : (object)emisionDesde);
                sp.AgregarParametro("emision_hasta", emisionHasta == null ? DBNull.Value : (object)emisionHasta);
                sp.AgregarParametro("vencimiento_desde", vencimientoDesde == null ? DBNull.Value : (object)vencimientoDesde);
                sp.AgregarParametro("vencimiento_hasta", vencimientoHasta == null ? DBNull.Value : (object)vencimientoHasta);
                sp.AgregarParametro("monto_desde", montoDesde);
                sp.AgregarParametro("monto_hasta", montoHasta);

                sp.AgregarParametro("estado", (object)estado ?? DBNull.Value);
                sp.AgregarParametro("moneda", (object)moneda ?? DBNull.Value);
                sp.AgregarParametro("numero_interno", (object)numeroInterno ?? DBNull.Value);
                sp.AgregarParametro("producto", (object)producto ?? DBNull.Value);
                sp.AgregarParametro("comentario", (object)comentario ?? DBNull.Value);

                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    count = Int32.Parse(ds.Tables[0].Rows[0]["count"].ToString());
                }

                return count;
            }
            catch (Exception ex)
            {
                return count;
            }
        }

        public static List<dto.BuscarAceptarComprobante> ListarAceptarComprobante(int codemp, int idioma, int codsuc, string tipo, string estado, string cartera, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.BuscarAceptarComprobante> lst = new List<dto.BuscarAceptarComprobante>();
            DateTime fecha = new DateTime();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Comprobante_Aceptar_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idioma", idioma);
                sp.AgregarParametro("codsuc", codsuc);
                sp.AgregarParametro("tipo", tipo);
                sp.AgregarParametro("estado", estado);
                sp.AgregarParametro("cartera", cartera);
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
                        lst.Add(new dto.BuscarAceptarComprobante()
                        {
                            IdTipoDocumento = Int32.Parse(ds.Tables[0].Rows[i]["IdTipoDocumento"].ToString()),
                            Numero = Int32.Parse(ds.Tables[0].Rows[i]["Numero"].ToString()),
                            Rut = ds.Tables[0].Rows[i]["Rut"].ToString(),
                            NombreFantasia = ds.Tables[0].Rows[i]["NombreFantasia"].ToString(),
                            TipoDocumento = ds.Tables[0].Rows[i]["TipoDocumento"].ToString(),
                            NumeroCliente = ds.Tables[0].Rows[i]["NumeroCliente"].ToString(),
                            Monto = decimal.Parse(ds.Tables[0].Rows[i]["Monto"].ToString()),
                            Moneda = ds.Tables[0].Rows[i]["Moneda"].ToString(),
                            FinDeuda = ds.Tables[0].Rows[i]["FinDeuda"].ToString(),
                            Contable = ds.Tables[0].Rows[i]["Contable"].ToString(),
                            TipoCambio = decimal.Parse(ds.Tables[0].Rows[i]["TipoCambio"].ToString()),
                            Cartera = ds.Tables[0].Rows[i]["Cartera"].ToString(),
                            Fecha = DateTime.TryParse(ds.Tables[0].Rows[i]["Fecha"].ToString(), out fecha) ? DateTime.Parse(ds.Tables[0].Rows[i]["Fecha"].ToString()) : new DateTime(),
                            FechaContable = DateTime.TryParse(ds.Tables[0].Rows[i]["FechaContable"].ToString(), out fecha) ? DateTime.Parse(ds.Tables[0].Rows[i]["FechaContable"].ToString()) : new DateTime(),
                            Gestion = ds.Tables[0].Rows[i]["Gestion"].ToString(),
                            Usuario = ds.Tables[0].Rows[i]["Usuario"].ToString(),
                            Deudor = ds.Tables[0].Rows[i]["Deudor"].ToString(),
                            Rol = ds.Tables[0].Rows[i]["Rol"].ToString(),
                            Tribunal = ds.Tables[0].Rows[i]["Tribunal"].ToString(),
                            Asegurado = ds.Tables[0].Rows[i]["Asegurado"].ToString(),
                            Pclid = Int32.Parse(ds.Tables[0].Rows[i]["PCLID"].ToString()),
                            Ctcid = Int32.Parse(ds.Tables[0].Rows[i]["CTCID"].ToString()),
                            Rolid = Int32.Parse(ds.Tables[0].Rows[i]["ROLID"].ToString())
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

        public static int ListarAceptarComprobanteCount(int codemp, int idioma, int codsuc, string tipo, string estado, string cartera, string where, string sidx, string sord, int inicio, int limite)
        {
            int count = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Comprobante_Aceptar_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idioma", idioma);
                sp.AgregarParametro("codsuc", codsuc);
                sp.AgregarParametro("tipo", tipo);
                sp.AgregarParametro("estado", estado);
                sp.AgregarParametro("cartera", cartera);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    count = Int32.Parse(ds.Tables[0].Rows[0]["count"].ToString());
                }

                return count;
            }
            catch (Exception ex)
            {
                return count;
            }
        }

        public static int AceptarComprobante(AceptarComprobante obj, UserSession objSesion)
        {
            int salida = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Aceptar_Comprobante");
                sp.AgregarParametro("codemp", objSesion.CodigoEmpresa);
                sp.AgregarParametro("codsuc", objSesion.CodigoSucursal);
                sp.AgregarParametro("idioma", objSesion.Idioma);
                sp.AgregarParametro("tipo_documento", obj.IdTipoDocumento);
                sp.AgregarParametro("numero", obj.Numero);
                sp.AgregarParametro("fecha", obj.Fecha);
                sp.AgregarParametro("fecha_contable", obj.FechaContable);
                sp.AgregarParametro("usuario", objSesion.UserId);
                sp.AgregarParametro("ip_red", objSesion.IpRed);
                sp.AgregarParametro("ip_pc", objSesion.IpPc);
                int error = sp.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.Comprobante.AceptarComprobante", obj.Numero);
                throw ex;
            }
        }

        public static int ContabilizarComprobante(AceptarComprobante obj, UserSession objSesion)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Contabilizar_Comprobante");
                sp.AgregarParametro("codemp", objSesion.CodigoEmpresa);
                sp.AgregarParametro("codsuc", objSesion.CodigoSucursal);
                sp.AgregarParametro("tipo_documento", obj.IdTipoDocumento);
                sp.AgregarParametro("numero", obj.Numero);
                sp.AgregarParametro("usuario", objSesion.UserId);
                sp.AgregarParametro("ip_red", objSesion.IpRed);
                sp.AgregarParametro("ip_pc", objSesion.IpPc);
                int error = sp.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.Comprobante._Contabilizar_Comprobante", obj.Numero);
                throw ex;
            }
        }

        public static int FacturarComprobante(AceptarComprobante obj, UserSession objSesion)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Facturar_Comprobante");
                sp.AgregarParametro("codemp", objSesion.CodigoEmpresa);
                sp.AgregarParametro("codsuc", objSesion.CodigoSucursal);
                sp.AgregarParametro("tipo_documento", obj.IdTipoDocumento);
                sp.AgregarParametro("numero", obj.Numero);
                sp.AgregarParametro("usuario", objSesion.UserId);
                sp.AgregarParametro("ip_red", objSesion.IpRed);
                sp.AgregarParametro("ip_pc", objSesion.IpPc);
                int error = sp.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.Comprobante._Facturar_Comprobante", obj.Numero);
                throw ex;
            }
        }

        public static int ModificarComprobanteEstado(CabeceraComprobante obj, UserSession objSesion)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Update_Cabecera_Comprobantes_Estado");
                sp.AgregarParametro("cbc_codemp", objSesion.CodigoEmpresa);
                sp.AgregarParametro("cbc_sucid", objSesion.CodigoSucursal);
                sp.AgregarParametro("cbc_tpcid", obj.TipoComprobante);
                sp.AgregarParametro("cbc_numero", obj.CabeceraId);
                sp.AgregarParametro("cbt_estado", obj.Estado);
                sp.AgregarParametro("cbc_saldo", obj.Saldo);
                int error = sp.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.Comprobante.Update_Cabecera_Comprobantes_Estado", objSesion.UserId);
                throw ex;
            }
        }

        public static int ModificarGastoJudicial(dto.Comprobante obj, UserSession objSesion, int total)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Update_Cartera_Clientes_Cpbt_Doc_GastJud");
                sp.AgregarParametro("codemp", objSesion.CodigoEmpresa);
                sp.AgregarParametro("pclid", obj.Pclid);
                sp.AgregarParametro("ctcid", obj.Ctcid);
                sp.AgregarParametro("ccbid", obj.Ccbid);
                sp.AgregarParametro("gastjud", obj.Monto / total);
                int error = sp.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.Comprobante._Update_Cartera_Clientes_Cpbt_Doc_GastJud", objSesion.UserId);
                throw ex;
            }
        }

        public static List<dto.BuscarEstadoComprobante> ListarEstadoComprobante(int codemp, int idioma, int codsuc, string tipo, string estado, DateTime? desde, DateTime? hasta, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.BuscarEstadoComprobante> lst = new List<dto.BuscarEstadoComprobante>();
            DateTime fecha = new DateTime();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Comprobante_Estado_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idioma", idioma);
                sp.AgregarParametro("codsuc", codsuc);
                sp.AgregarParametro("tipo", tipo);
                sp.AgregarParametro("estado", estado);
                sp.AgregarParametro("desde", desde == null ? DBNull.Value : (object)desde);
                sp.AgregarParametro("hasta", hasta == null ? DBNull.Value : (object)hasta);
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
                        lst.Add(new dto.BuscarEstadoComprobante()
                        {
                            IdTipoDocumento = Int32.Parse(ds.Tables[0].Rows[i]["IdTipoDocumento"].ToString()),
                            Numero = Int32.Parse(ds.Tables[0].Rows[i]["Numero"].ToString()),
                            Rut = ds.Tables[0].Rows[i]["Rut"].ToString(),
                            NombreFantasia = ds.Tables[0].Rows[i]["NombreFantasia"].ToString(),
                            TipoDocumento = ds.Tables[0].Rows[i]["TipoDocumento"].ToString(),
                            NumeroCliente = ds.Tables[0].Rows[i]["NumeroCliente"].ToString(),
                            Bruto = decimal.Parse(ds.Tables[0].Rows[i]["Bruto"].ToString()),
                            Retenido = decimal.Parse(ds.Tables[0].Rows[i]["Retenido"].ToString()),
                            Monto = decimal.Parse(ds.Tables[0].Rows[i]["Monto"].ToString()),
                            Moneda = ds.Tables[0].Rows[i]["Moneda"].ToString(),
                            FinDeuda = ds.Tables[0].Rows[i]["FinDeuda"].ToString(),
                            Contable = ds.Tables[0].Rows[i]["Contable"].ToString(),
                            TipoCambio = decimal.Parse(ds.Tables[0].Rows[i]["TipoCambio"].ToString()),
                            Cartera = ds.Tables[0].Rows[i]["Cartera"].ToString(),
                            Fecha = DateTime.TryParse(ds.Tables[0].Rows[i]["Fecha"].ToString(), out fecha) ? DateTime.Parse(ds.Tables[0].Rows[i]["Fecha"].ToString()) : new DateTime(),
                            FechaContable = DateTime.TryParse(ds.Tables[0].Rows[i]["FechaContable"].ToString(), out fecha) ? DateTime.Parse(ds.Tables[0].Rows[i]["FechaContable"].ToString()) : new DateTime(),
                            Cliente = ds.Tables[0].Rows[i]["Cliente"].ToString(),
                            Usuario = ds.Tables[0].Rows[i]["Usuario"].ToString(),
                            Pclid = Int32.Parse(ds.Tables[0].Rows[i]["PCLID"].ToString()),
                            Ctcid = Int32.Parse(ds.Tables[0].Rows[i]["CTCID"].ToString()),
                            Rolid = Int32.Parse(ds.Tables[0].Rows[i]["ROLID"].ToString())
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

        public static List<string> ListarLinkRutasEstampes(int codemp, int pclid, int ctcid, int rolid, int tpcid, int numero)
        {
            List<string> lst = new List<string>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Link_Rutas_Estampes");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("rolid", rolid);
                sp.AgregarParametro("tpcid", tpcid);
                sp.AgregarParametro("numero", numero);

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(ds.Tables[0].Rows[i]["ARCHIVO"].ToString());
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                return lst;
            }
        }

        public static int ListarEstadoComprobanteCount(int codemp, int idioma, int codsuc, string tipo, string estado, DateTime? desde, DateTime? hasta, string where, string sidx, string sord, int inicio, int limite)
        {
            int count = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Comprobante_Estado_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idioma", idioma);
                sp.AgregarParametro("codsuc", codsuc);
                sp.AgregarParametro("tipo", tipo);
                sp.AgregarParametro("estado", estado);
                sp.AgregarParametro("desde", desde == null ? DBNull.Value : (object)desde);
                sp.AgregarParametro("hasta", hasta == null ? DBNull.Value : (object)hasta);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    count = Int32.Parse(ds.Tables[0].Rows[0]["count"].ToString());
                }

                return count;
            }
            catch (Exception ex)
            {
                return count;
            }
        }

        public static List<dto.BoletaHonorarioSalida> ListarBHContabilizadas(int codemp, DateTime desde, DateTime hasta)
        {
            List<dto.BoletaHonorarioSalida> lst = new List<dto.BoletaHonorarioSalida>();
            DateTime fecha = new DateTime();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_BH_Salida_Contabilidad");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("desde", desde);
                sp.AgregarParametro("hasta", hasta);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.BoletaHonorarioSalida()
                        {
                            AnalisisPorClasificadorN1ValorBruto = ds.Tables[0].Rows[i]["AnalisisPorClasificadorN1ValorBruto"].ToString(),
                            AnalisisPorClasificadorN1ValorRetencion = ds.Tables[0].Rows[i]["AnalisisPorClasificadorN1ValorRetencion"].ToString(),
                            AnalisisPorClasificadorN1ValorTotal = ds.Tables[0].Rows[i]["AnalisisPorClasificadorN1ValorTotal"].ToString(),
                            AnalisisPorClasificadorN2ValorBruto = ds.Tables[0].Rows[i]["AnalisisPorClasificadorN2ValorBruto"].ToString(),
                            AnalisisPorClasificadorN2ValorRetencion = ds.Tables[0].Rows[i]["AnalisisPorClasificadorN2ValorRetencion"].ToString(),
                            AnalisisPorClasificadorN2ValorTotal = ds.Tables[0].Rows[i]["AnalisisPorClasificadorN2ValorTotal"].ToString(),
                            AnalisisPorFichaValorBruto = ds.Tables[0].Rows[i]["AnalisisPorFichaValorBruto"].ToString(),
                            AnalisisPorFichaValorRetencion = ds.Tables[0].Rows[i]["AnalisisPorFichaValorRetencion"].ToString(),
                            AnalisisPorFichaValorTotal = ds.Tables[0].Rows[i]["AnalisisPorFichaValorTotal"].ToString(),
                            AnalisisPorMonedaValorBruto = ds.Tables[0].Rows[i]["AnalisisPorMonedaValorBruto"].ToString(),
                            AnalisisPorMonedaValorRetencion = ds.Tables[0].Rows[i]["AnalisisPorMonedaValorRetencion"].ToString(),
                            AnalisisPorMonedaValorTotal = ds.Tables[0].Rows[i]["AnalisisPorMonedaValorTotal"].ToString(),
                            AnalisisTasaDeCambioBruto = ds.Tables[0].Rows[i]["AnalisisTasaDeCambioBruto"].ToString(),
                            AnalisisTasaDeCambioRetencion = ds.Tables[0].Rows[i]["AnalisisTasaDeCambioRetencion"].ToString(),
                            AnalisisTasaDeCambioTotal = ds.Tables[0].Rows[i]["AnalisisTasaDeCambioTotal"].ToString(),
                            BoletaAnulada = ds.Tables[0].Rows[i]["BoletaAnulada"].ToString(),
                            BoletaDirector = ds.Tables[0].Rows[i]["BoletaDirector"].ToString(),
                            Ciudad = ds.Tables[0].Rows[i]["Ciudad"].ToString(),
                            CodigoLegal = ds.Tables[0].Rows[i]["CodigoLegal"].ToString(),
                            Comentario = ds.Tables[0].Rows[i]["Comentario"].ToString(),
                            DireccionPersona = ds.Tables[0].Rows[i]["DireccionPersona"].ToString(),
                            FechaDeContabilizacion = ds.Tables[0].Rows[i]["FechaDeContabilizacion"].ToString(),
                            FechaDelDocumento = ds.Tables[0].Rows[i]["FechaDelDocumento"].ToString(),
                            FechaLibro = ds.Tables[0].Rows[i]["FechaLibro"].ToString(),
                            IdentificadorCentroDeNegocios = ds.Tables[0].Rows[i]["IdentificadorCentroDeNegocios"].ToString(),
                            IdentificadorPrestadorEmisor = ds.Tables[0].Rows[i]["IdentificadorPrestadorEmisor"].ToString(),
                            MontoBruto = ds.Tables[0].Rows[i]["MontoBruto"].ToString(),
                            MontoRetencion = ds.Tables[0].Rows[i]["MontoRetencion"].ToString(),
                            NombrePersona = ds.Tables[0].Rows[i]["NombrePersona"].ToString(),
                            NumeroDelDocumento = ds.Tables[0].Rows[i]["NumeroDelDocumento"].ToString(),
                            TipoDeBoleta = ds.Tables[0].Rows[i]["TipoDeBoleta"].ToString(),
                            TipoDeDocumento = ds.Tables[0].Rows[i]["TipoDeDocumento"].ToString(),
                            TipoDeHonorario = ds.Tables[0].Rows[i]["TipoDeHonorario"].ToString(),
                            TipoRetencion = ds.Tables[0].Rows[i]["TipoRetencion"].ToString(),
                            Total = ds.Tables[0].Rows[i]["Total"].ToString()

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

        public static List<dto.BoletaHonorarioSalida> ListarBHFacturadas(int codemp, DateTime desde, DateTime hasta)
        {
            List<dto.BoletaHonorarioSalida> lst = new List<dto.BoletaHonorarioSalida>();
            DateTime fecha = new DateTime();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_BH_Salida_Facturacion");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("desde", desde);
                sp.AgregarParametro("hasta", hasta);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.BoletaHonorarioSalida()
                        {
                            AnalisisPorClasificadorN1ValorBruto = ds.Tables[0].Rows[i]["AnalisisPorClasificadorN1ValorBruto"].ToString(),
                            AnalisisPorClasificadorN1ValorRetencion = ds.Tables[0].Rows[i]["AnalisisPorClasificadorN1ValorRetencion"].ToString(),
                            AnalisisPorClasificadorN1ValorTotal = ds.Tables[0].Rows[i]["AnalisisPorClasificadorN1ValorTotal"].ToString(),
                            AnalisisPorClasificadorN2ValorBruto = ds.Tables[0].Rows[i]["AnalisisPorClasificadorN2ValorBruto"].ToString(),
                            AnalisisPorClasificadorN2ValorRetencion = ds.Tables[0].Rows[i]["AnalisisPorClasificadorN2ValorRetencion"].ToString(),
                            AnalisisPorClasificadorN2ValorTotal = ds.Tables[0].Rows[i]["AnalisisPorClasificadorN2ValorTotal"].ToString(),
                            AnalisisPorFichaValorBruto = ds.Tables[0].Rows[i]["AnalisisPorFichaValorBruto"].ToString(),
                            AnalisisPorFichaValorRetencion = ds.Tables[0].Rows[i]["AnalisisPorFichaValorRetencion"].ToString(),
                            AnalisisPorFichaValorTotal = ds.Tables[0].Rows[i]["AnalisisPorFichaValorTotal"].ToString(),
                            AnalisisPorMonedaValorBruto = ds.Tables[0].Rows[i]["AnalisisPorMonedaValorBruto"].ToString(),
                            AnalisisPorMonedaValorRetencion = ds.Tables[0].Rows[i]["AnalisisPorMonedaValorRetencion"].ToString(),
                            AnalisisPorMonedaValorTotal = ds.Tables[0].Rows[i]["AnalisisPorMonedaValorTotal"].ToString(),
                            AnalisisTasaDeCambioBruto = ds.Tables[0].Rows[i]["AnalisisTasaDeCambioBruto"].ToString(),
                            AnalisisTasaDeCambioRetencion = ds.Tables[0].Rows[i]["AnalisisTasaDeCambioRetencion"].ToString(),
                            AnalisisTasaDeCambioTotal = ds.Tables[0].Rows[i]["AnalisisTasaDeCambioTotal"].ToString(),
                            BoletaAnulada = ds.Tables[0].Rows[i]["BoletaAnulada"].ToString(),
                            BoletaDirector = ds.Tables[0].Rows[i]["BoletaDirector"].ToString(),
                            Ciudad = ds.Tables[0].Rows[i]["Ciudad"].ToString(),
                            CodigoLegal = ds.Tables[0].Rows[i]["CodigoLegal"].ToString(),
                            Comentario = ds.Tables[0].Rows[i]["Comentario"].ToString(),
                            DireccionPersona = ds.Tables[0].Rows[i]["DireccionPersona"].ToString(),
                            FechaDeContabilizacion = ds.Tables[0].Rows[i]["FechaDeContabilizacion"].ToString(),
                            FechaDelDocumento = ds.Tables[0].Rows[i]["FechaDelDocumento"].ToString(),
                            FechaLibro = ds.Tables[0].Rows[i]["FechaLibro"].ToString(),
                            IdentificadorCentroDeNegocios = ds.Tables[0].Rows[i]["IdentificadorCentroDeNegocios"].ToString(),
                            IdentificadorPrestadorEmisor = ds.Tables[0].Rows[i]["IdentificadorPrestadorEmisor"].ToString(),
                            MontoBruto = ds.Tables[0].Rows[i]["MontoBruto"].ToString(),
                            MontoRetencion = ds.Tables[0].Rows[i]["MontoRetencion"].ToString(),
                            NombrePersona = ds.Tables[0].Rows[i]["NombrePersona"].ToString(),
                            NumeroDelDocumento = ds.Tables[0].Rows[i]["NumeroDelDocumento"].ToString(),
                            TipoDeBoleta = ds.Tables[0].Rows[i]["TipoDeBoleta"].ToString(),
                            TipoDeDocumento = ds.Tables[0].Rows[i]["TipoDeDocumento"].ToString(),
                            TipoDeHonorario = ds.Tables[0].Rows[i]["TipoDeHonorario"].ToString(),
                            TipoRetencion = ds.Tables[0].Rows[i]["TipoRetencion"].ToString(),
                            Total = ds.Tables[0].Rows[i]["Total"].ToString()

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

        #endregion

        #region "Sub Cartera"

        public static List<dto.BuscarSubCartera> ListarSubCarteras(int codemp, string nombre, string rut, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.BuscarSubCartera> lst = new List<dto.BuscarSubCartera>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Subcartera_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("nombre", (object)nombre ?? DBNull.Value);
                sp.AgregarParametro("rut", (object)rut ?? DBNull.Value);
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
                        lst.Add(new dto.BuscarSubCartera()
                        {
                            Sbcid = Int32.Parse(ds.Tables[0].Rows[i]["Sbcid"].ToString()),
                            Rut = ds.Tables[0].Rows[i]["Rut"].ToString(),
                            Nombre = ds.Tables[0].Rows[i]["Nombre"].ToString()
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

        public static int ListarSubCarterasCount(int codemp, string nombre, string rut, string where, string sidx, string sord, int inicio, int limite)
        {
            int count = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Subcartera_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("nombre", (object)nombre ?? DBNull.Value);
                sp.AgregarParametro("rut", (object)rut ?? DBNull.Value);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    count = Int32.Parse(ds.Tables[0].Rows[0]["count"].ToString());
                }

                return count;
            }
            catch (Exception ex)
            {
                return count;
            }
        }

        public static dto.SubCartera TraeSubCartera(int codemp, int sbcid)
        {
            dto.SubCartera obj = new dto.SubCartera();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Subcartera");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("sbcid", sbcid);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        obj = new dto.SubCartera()
                        {
                            Codemp = Int32.Parse(ds.Tables[0].Rows[i]["Codemp"].ToString()),
                            Sbcid = Int32.Parse(ds.Tables[0].Rows[i]["Sbcid"].ToString()),
                            Rut = ds.Tables[0].Rows[i]["Rut"].ToString(),
                            Nombre = ds.Tables[0].Rows[i]["Nombre"].ToString(),
                            Direccion = ds.Tables[0].Rows[i]["Direccion"].ToString(),
                            Telefono = ds.Tables[0].Rows[i]["Telefono"].ToString(),
                            Comuna = Int32.Parse(ds.Tables[0].Rows[i]["Comuna"].ToString()),
                            Ciudad = Int32.Parse(ds.Tables[0].Rows[i]["Ciudad"].ToString()),
                            Region = Int32.Parse(ds.Tables[0].Rows[i]["Region"].ToString()),
                            Pais = Int32.Parse(ds.Tables[0].Rows[i]["Pais"].ToString())
                        };
                    }
                }

                return obj;
            }
            catch (Exception ex)
            {
                return obj;
            }
        }

        public static int InsertarSubcartera(SubCartera obj)
        {
            int error = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Insertar_SubCarteras");
                sp.AgregarParametro("codemp", obj.Codemp);
                sp.AgregarParametro("rut", obj.Rut);
                sp.AgregarParametro("nombre", obj.Nombre);
                sp.AgregarParametro("comid", obj.Comuna);
                sp.AgregarParametro("direccion", obj.Direccion);
                sp.AgregarParametro("telefono", (object)obj.Telefono ?? DBNull.Value);
                error = sp.EjecutarProcedimientoTrans();

                return error;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static int ModificarSubcartera(SubCartera obj)
        {
            int error = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Update_SubCarteras");
                sp.AgregarParametro("sbc_codemp", obj.Codemp);
                sp.AgregarParametro("sbc_sbcid", obj.Sbcid);
                sp.AgregarParametro("sbc_rut", obj.Rut);
                sp.AgregarParametro("sbc_nombre", obj.Nombre);
                sp.AgregarParametro("sbc_comid", obj.Comuna);
                sp.AgregarParametro("sbc_direccion", obj.Direccion);
                sp.AgregarParametro("sbc_telefono", (object)obj.Telefono ?? DBNull.Value);
                error = sp.EjecutarProcedimientoTrans();

                return error;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static List<Autocomplete> ListarRutNombreAsegurado(string nombre)
        {
            List<Autocomplete> lst = new List<Autocomplete>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Rut_Nombre_Asegurado");
                sp.AgregarParametro("texto", nombre);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Autocomplete()
                        {
                            label = ds.Tables[0].Rows[i][0].ToString(),
                            value = ds.Tables[0].Rows[i][1].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lst;
        }

        #endregion

        #region "Contabilidad"



        #endregion

        #region "Agregar Historial"

        public static List<Combobox> ListarEstadosHistorial(int codemp, int grupo, int idioma, string tipo, string estadoXDoc, int perfil)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Estados_Historial");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("grupo", grupo);
                sp.AgregarParametro("idioma", idioma);
                sp.AgregarParametro("tipo", tipo);
                sp.AgregarParametro("estadoXDoc", estadoXDoc);
                sp.AgregarParametro("perfil", perfil);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Dimol.dto.Combobox()
                        {
                            Text = ds.Tables[0].Rows[i]["Nombre"].ToString(),
                            Value = ds.Tables[0].Rows[i]["ID"].ToString()
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
        public static List<Combobox> ListarEstadosCobranzaClientePerfil(int codemp, int grupo, int idioma, int pclid, string estadoXDoc, int perfil)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Estados_Cliente_Perfil");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("grupo", grupo);
                sp.AgregarParametro("idioma", idioma);
                sp.AgregarParametro("estadoXDoc", estadoXDoc);
                sp.AgregarParametro("perfil", perfil);
                sp.AgregarParametro("pclid", pclid);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Dimol.dto.Combobox()
                        {
                            Text = ds.Tables[0].Rows[i]["Nombre"].ToString(),
                            Value = ds.Tables[0].Rows[i]["ID"].ToString()
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
        public static List<dto.Comprobante> ListarDocumentosHistorial(int codemp, int pclid, int ctcid, string estadoCPBT, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.Comprobante> lst = new List<dto.Comprobante>();
            DateTime fecha = new DateTime();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Documentos_Historial_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("estado", estadoCPBT);
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
                        lst.Add(new dto.Comprobante()
                        {
                            Id = Int32.Parse(ds.Tables[0].Rows[i]["row"].ToString()),
                            Pclid = pclid,
                            Ctcid = ctcid,
                            //RutCliente = ds.Tables[0].Rows[i]["RutCliente"].ToString(),
                            //NombreCliente = ds.Tables[0].Rows[i]["NombreCliente"].ToString(),
                            TipoCpbtNombre = ds.Tables[0].Rows[i]["TipoCpbtNombre"].ToString(),
                            Ccbid = Int32.Parse(ds.Tables[0].Rows[i]["Ccbid"].ToString()),
                            NumeroCpbt = ds.Tables[0].Rows[i]["NumeroCpbt"].ToString(),
                            //FechaIngreso = DateTime.TryParse(ds.Tables[0].Rows[i]["FechaIngreso"].ToString(), out fecha) ? DateTime.Parse(ds.Tables[0].Rows[i]["FechaIngreso"].ToString()) : new DateTime(),
                            FechaDocumento = DateTime.TryParse(ds.Tables[0].Rows[i]["FechaDocumento"].ToString(), out fecha) ? DateTime.Parse(ds.Tables[0].Rows[i]["FechaDocumento"].ToString()) : new DateTime(),
                            FechaVencimiento = DateTime.TryParse(ds.Tables[0].Rows[i]["FechaVencimiento"].ToString(), out fecha) ? DateTime.Parse(ds.Tables[0].Rows[i]["FechaVencimiento"].ToString()) : new DateTime(),
                            //FechaUltimaGestion = DateTime.TryParse(ds.Tables[0].Rows[i]["FechaUltimaGestion"].ToString(), out fecha) ? DateTime.Parse(ds.Tables[0].Rows[i]["FechaUltimaGestion"].ToString()) : new DateTime(),
                            //FechaPlazo = DateTime.TryParse(ds.Tables[0].Rows[i]["FechaPlazo"].ToString(), out fecha) ? DateTime.Parse(ds.Tables[0].Rows[i]["FechaPlazo"].ToString()) : new DateTime(),
                            //FechaCalculoInteres = DateTime.TryParse(ds.Tables[0].Rows[i]["FechaCalculoInteres"].ToString(), out fecha) ? DateTime.Parse(ds.Tables[0].Rows[i]["FechaCalculoInteres"].ToString()) : new DateTime(),
                            //FechaCastigo = DateTime.TryParse(ds.Tables[0].Rows[i]["FechaCastigo"].ToString(), out fecha) ? DateTime.Parse(ds.Tables[0].Rows[i]["FechaCastigo"].ToString()) : new DateTime(),
                            //EstadoCartera = ds.Tables[0].Rows[i]["EstadoCarteraDescripcion"].ToString(),
                            EstadoCpbt = estadoCPBT,
                            //CodigoMoneda = Int32.Parse(ds.Tables[0].Rows[i]["CodigoMoneda"].ToString()),
                            //TipoCambio = decimal.Parse(ds.Tables[0].Rows[i]["TipoCambio"].ToString()),
                            //MontoAsignado = decimal.Parse(ds.Tables[0].Rows[i]["MontoAsignado"].ToString()),
                            Monto = decimal.Parse(ds.Tables[0].Rows[i]["Monto"].ToString()),
                            Saldo = decimal.Parse(ds.Tables[0].Rows[i]["Saldo"].ToString()),
                            //GastoJudicial = decimal.Parse(ds.Tables[0].Rows[i]["GastoJudicial"].ToString()),
                            //GastoOtros = decimal.Parse(ds.Tables[0].Rows[i]["GastoOtros"].ToString()),
                            //Intereses = decimal.Parse(ds.Tables[0].Rows[i]["Intereses"].ToString()),
                            //Honorarios = decimal.Parse(ds.Tables[0].Rows[i]["Honorarios"].ToString()),
                            //CalculoHonorarios = ds.Tables[0].Rows[i]["CalculoHonorarios"].ToString(),
                            //NombreBanco = ds.Tables[0].Rows[i]["NombreBanco"].ToString(),
                            //RutGirador = ds.Tables[0].Rows[i]["RutGirador"].ToString(),
                            //NombreGirador = ds.Tables[0].Rows[i]["NombreGirador"].ToString(),
                            //Comentario = ds.Tables[0].Rows[i]["Comentario"].ToString(),
                            //Retent = ds.Tables[0].Rows[i]["retent"].ToString(),
                            //NumeroEspecial = ds.Tables[0].Rows[i]["NumeroEspecial"].ToString(),
                            //NumeroAgrupa = ds.Tables[0].Rows[i]["NumeroAgrupa"].ToString(),
                            //Carta = Int32.Parse(ds.Tables[0].Rows[i]["Carta"].ToString()),
                            //Cobrable = ds.Tables[0].Rows[i]["Cobrable"].ToString(),
                            //Contrato = Int32.Parse(ds.Tables[0].Rows[i]["cctid"].ToString()),
                            //SubcarteraRut = ds.Tables[0].Rows[i]["SubcarteraRut"].ToString(),
                            //SubcarteraNombre = ds.Tables[0].Rows[i]["SubcarteraNombre"].ToString(),
                            // Originales = ds.Tables[0].Rows[i]["DocumentoOrigen"].ToString(),
                            //Antecedentes = ds.Tables[0].Rows[i]["docant"].ToString(),
                            //TipoCartera = Int32.Parse(ds.Tables[0].Rows[i]["TipoCartera"].ToString()),
                            //DiasVencido = Int32.Parse(ds.Tables[0].Rows[i]["DiasVencido"].ToString()),
                            //Moneda = ds.Tables[0].Rows[i]["Moneda"].ToString(),
                            //TotalDeuda = decimal.Parse(ds.Tables[0].Rows[i]["TotalDeuda"].ToString()),
                            Compromiso = decimal.Parse(ds.Tables[0].Rows[i]["Compromiso"].ToString()),
                            //CodigoCarga = ds.Tables[0].Rows[i]["CodigoCargaNombre"].ToString()

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

        public static int ListarDocumentosHistorialCount(int codemp, int pclid, int ctcid, string estadoCPBT, string where, string sidx, string sord, int inicio, int limite)
        {
            int count = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Documentos_Historial_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("estado", estadoCPBT);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    count = Int32.Parse(ds.Tables[0].Rows[0]["ccbid"].ToString());
                }

                return count;
            }
            catch (Exception ex)
            {
                return count;
            }
        }

        public static dto.DetalleEstados TraeDetalleEstado(int codemp, int estid)
        {
            dto.DetalleEstados obj = new dto.DetalleEstados();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Revisa_Estados");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("estid", estid);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        obj = new dto.DetalleEstados()
                        {
                            Agrupa = Int32.Parse(ds.Tables[0].Rows[i]["Agrupa"].ToString()),
                            Compromiso = ds.Tables[0].Rows[i]["Compromiso"].ToString(),
                            GeneraRetiro = ds.Tables[0].Rows[i]["GeneraRetiro"].ToString(),
                            Prejudicial = ds.Tables[0].Rows[i]["Prejudicial"].ToString(),
                            SolicitaFecha = ds.Tables[0].Rows[i]["SolicitaFecha"].ToString(),
                            Utiliza = ds.Tables[0].Rows[i]["Utiliza"].ToString()
                        };
                    }
                }

                return obj;
            }
            catch (Exception ex)
            {
                return obj;
            }
        }

        public static int InsertarCarteraClientesEstadosAcciones(int codemp, int pclid, int ctcid, int accid, int codsuc, int gesid, string contacto, string ipRed, string ipMaquina, int usuario, string comentario, int ddcid, long telefono)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Insertar_Cartera_Clientes_Estados_Acciones");
                sp.AgregarParametro("cea_codemp", codemp);
                sp.AgregarParametro("cea_pclid", pclid);
                sp.AgregarParametro("cea_ctcid", ctcid);
                sp.AgregarParametro("cea_accid", accid);
                sp.AgregarParametro("cea_sucid", codsuc);
                sp.AgregarParametro("cea_gesid", gesid == 0 ? DBNull.Value : (object)gesid);
                sp.AgregarParametro("cea_contacto", contacto);
                sp.AgregarParametro("cea_ipred", ipRed);
                sp.AgregarParametro("cea_ipmaquina", ipMaquina);
                sp.AgregarParametro("cea_comentario", string.IsNullOrEmpty(comentario) ? " " : comentario);
                sp.AgregarParametro("cea_estado", "S");
                sp.AgregarParametro("cea_usrid", usuario);
                sp.AgregarParametro("cea_ddcid", ddcid == 0 ? DBNull.Value : (object)ddcid);
                sp.AgregarParametro("cea_telefono", telefono == 0 ? DBNull.Value : (object)telefono);

                int error = sp.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.InsertarCarteraClientesEstadosAcciones", usuario);
                throw ex;
            }

        }

        public static int ActualizarCarteraClientesCompromiso(int codemp, int pclid, int ctcid, int ccbid, decimal compromiso, DateTime? fecha)
        {
            try
            {
                DateTime fechaPlazo;
                DateTime fechaFinal;
                if (DateTime.TryParse(fecha.ToString(), out fechaPlazo))
                {
                    TimeSpan hora = new TimeSpan(0, 23, 59, 59, 0);
                    fechaFinal = fechaPlazo.Add(hora);
                }
                else
                {
                    fechaFinal = new DateTime();
                }

                StoredProcedure sp = new StoredProcedure("Update_Cartera_Clientes_Cpbt_Doc_Compromiso");
                sp.AgregarParametro("ccb_codemp", codemp);
                sp.AgregarParametro("ccb_pclid", pclid);
                sp.AgregarParametro("ccb_ctcid", ctcid);
                sp.AgregarParametro("ccb_ccbid", ccbid);
                sp.AgregarParametro("ccb_compromiso", compromiso);
                sp.AgregarParametro("ccb_fecplazo", fechaFinal == new DateTime() ? DBNull.Value : (object)fechaFinal);

                int error = sp.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.ActualizarCarteraClientesCompromiso", 0);
                throw ex;
            }

        }

        public static int InsertarCarteraClientesEstadosHistorialEspecial(int codemp, int pclid, int ctcid, int ccbid, DateTime fecha, int estid, int codsuc, int gesid, string ipRed, string ipMaquina, string comentario, decimal monto, decimal saldo, int usuario)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Insertar_Cartera_Clientes_Estados_Historial_Especial");
                sp.AgregarParametro("ceh_codemp", codemp);
                sp.AgregarParametro("ceh_pclid", pclid);
                sp.AgregarParametro("ceh_ctcid", ctcid);
                sp.AgregarParametro("ceh_ccbid", ccbid);
                sp.AgregarParametro("ceh_fecha", fecha);
                sp.AgregarParametro("ceh_estid", estid);
                sp.AgregarParametro("ceh_sucid", codsuc);
                sp.AgregarParametro("ceh_gesid", gesid == 0 ? DBNull.Value : (object)gesid);
                sp.AgregarParametro("ceh_ipred", ipRed);
                sp.AgregarParametro("ceh_ipmaquina", ipMaquina);
                sp.AgregarParametro("ceh_comentario", string.IsNullOrEmpty(comentario) ? " " : comentario);
                sp.AgregarParametro("ceh_monto", monto);
                sp.AgregarParametro("ceh_saldo", saldo);
                sp.AgregarParametro("ceh_usrid", usuario);

                int error = sp.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.InsertarCarteraClientesEstadosHistorialEspecial", usuario);
                throw ex;
            }

        }
        public static int ActualizarCarteraClientesUltimaGestion(int codemp, int pclid, int ctcid, string estCpbt)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Update_Cartera_Clientes_Cpbt_Doc_AccEst");
                sp.AgregarParametro("ccb_codemp", codemp);
                sp.AgregarParametro("ccb_pclid", pclid);
                sp.AgregarParametro("ccb_ctcid", ctcid);
                sp.AgregarParametro("ccb_estcpbt", estCpbt);

                int error = sp.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.ActualizarCarteraClientesUltimaGestion", pclid);
                throw ex;
            }

        }

        public static int ActualizarEstadoCarteraClientesTodos(int codemp, int pclid, int ctcid, int estid, string estCpbt)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Update_Cartera_Clientes_Cpbt_Doc_Estados_Todos");
                sp.AgregarParametro("ccb_codemp", codemp);
                sp.AgregarParametro("ccb_pclid", pclid);
                sp.AgregarParametro("ccb_ctcid", ctcid);
                sp.AgregarParametro("ccb_estid", estid);
                sp.AgregarParametro("ccb_estcpbt", estCpbt);

                int error = sp.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.ActualizarEstadoCarteraClientesTodos", pclid);
                throw ex;
            }

        }

        public static int ActualizarEstadoCarteraClientesCastigoDevolucion(int codemp, int pclid, int ctcid, int estid, string estCpbt, string comentario)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Update_Cartera_Clientes_Castigo_Devolucion");
                sp.AgregarParametro("ccb_codemp", codemp);
                sp.AgregarParametro("ccb_pclid", pclid);
                sp.AgregarParametro("ccb_ctcid", ctcid);
                sp.AgregarParametro("ccb_estid", estid);
                sp.AgregarParametro("ccb_estcpbt", estCpbt);
                sp.AgregarParametro("comentario", comentario);

                int error = sp.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.ActualizarEstadoCarteraClientesCastigoDevolucion", pclid);
                throw ex;
            }

        }

        public static int InsertarCarteraDemandaPendiente(int codemp, int pclid, int ctcid, int ccbid, int usuario)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Insertar_Cartera_Demanda_Pendiente");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("ccbid", ccbid);
                sp.AgregarParametro("usrid", usuario);

                int error = sp.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.InsertarCarteraDemandaPendiente", usuario);
                throw ex;
            }

        }

        public static int EliminarCarteraDemandaPendiente(int codemp, int pclid, int ctcid, int ccbid)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Delete_Cartera_Demanda_Pendiente");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("ccbid", ccbid);

                int error = sp.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.InsertarCarteraDemandaPendiente", ccbid);
                throw ex;
            }

        }

        #endregion

        #region "Comprobantes"

        public static List<Combobox> ListarFormasPago(int codemp, int idioma, string first)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Formas_De_Pago");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idioma", idioma);
                ds = sp.EjecutarProcedimiento();
                if (!string.IsNullOrEmpty(first))
                {
                    lst.Add(new Combobox()
                    {
                        Value = "",
                        Text = first
                    });
                }
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Dimol.dto.Combobox()
                        {
                            Text = ds.Tables[0].Rows[i]["Nombre"].ToString(),
                            Value = ds.Tables[0].Rows[i]["ID"].ToString()
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

        public static List<Combobox> ListarSucursales(int codemp, int pclid, string first)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_ProvCli_Sucursales");
                sp.AgregarParametro("pcs_codemp", codemp);
                sp.AgregarParametro("pcs_pclid", pclid);
                ds = sp.EjecutarProcedimiento();
                if (!string.IsNullOrEmpty(first))
                {
                    lst.Add(new Combobox()
                    {
                        Value = "",
                        Text = first
                    });
                }
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Dimol.dto.Combobox()
                        {
                            Text = ds.Tables[0].Rows[i]["pcs_nombre"].ToString(),
                            Value = ds.Tables[0].Rows[i]["pcs_pcsid"].ToString()
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

        public static ClasificacionComprobante TraeClasificacionComprobante(int codemp, int tpcid)
        {
            ClasificacionComprobante obj = new ClasificacionComprobante();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Clasificacion_Comprobante");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("tpcid", tpcid);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        obj = new ClasificacionComprobante()
                        {
                            Tipprod = Int32.Parse(ds.Tables[0].Rows[0]["clb_tipprod"].ToString()),
                            Libcompra = Int32.Parse(ds.Tables[0].Rows[0]["clb_libcompra"].ToString()),
                            Tipdig = Int32.Parse(ds.Tables[0].Rows[0]["tpc_tipdig"].ToString()),
                            Aplica = ds.Tables[0].Rows[0]["clb_aplica"].ToString(),
                            Cambiodoc = ds.Tables[0].Rows[0]["clb_cambiodoc"].ToString(),
                            Cancela = ds.Tables[0].Rows[0]["clb_cancela"].ToString(),
                            Cartcli = ds.Tables[0].Rows[0]["clb_cartcli"].ToString(),
                            Contable = ds.Tables[0].Rows[0]["clb_contable"].ToString(),
                            Costos = ds.Tables[0].Rows[0]["clb_costos"].ToString(),
                            Cptoctbl = ds.Tables[0].Rows[0]["clb_cptoctbl"].ToString(),
                            Findeuda = ds.Tables[0].Rows[0]["clb_findeuda"].ToString(),
                            Forpag = ds.Tables[0].Rows[0]["clb_forpag"].ToString(),
                            Ordcomp = ds.Tables[0].Rows[0]["clb_ordcomp"].ToString(),
                            Remesa = ds.Tables[0].Rows[0]["clb_remesa"].ToString(),
                            Selapl = ds.Tables[0].Rows[0]["clb_selapl"].ToString(),
                            Selcpbt = ds.Tables[0].Rows[0]["clb_selcpbt"].ToString(),
                            Tipcpbtdoc = ds.Tables[0].Rows[0]["clb_tipcpbtdoc"].ToString(),
                            Clbid = Int32.Parse(ds.Tables[0].Rows[0]["clb_clbid"].ToString()),
                            Sinimp = ds.Tables[0].Rows[0]["clb_sinimp"].ToString()

                        };
                    }
                }

                return obj;
            }
            catch (Exception ex)
            {
                return obj;
            }
        }

        public static List<Combobox> ListarSituacionCartera(int idioma)
        {
            List<Combobox> lst = new List<Combobox>();
            string val = "";
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Etiquetas");
                for (int i = 1; i < 5; i++)
                {
                    sp = new StoredProcedure("Trae_Etiquetas");
                    sp.AgregarParametro("codigo", "TipArea" + i);
                    sp.AgregarParametro("idioma", idioma);
                    ds = sp.EjecutarProcedimiento();
                    switch (i)
                    {
                        case 1:
                            val = "P";
                            break;
                        case 2:
                            val = "J";
                            break;
                        case 3:
                            val = "A";
                            break;
                        case 4:
                            val = "T";
                            break;
                    }

                    lst.Add(new Combobox()
                    {
                        Text = ds.Tables[0].Rows[0][0].ToString(),
                        Value = val
                    });

                }
                return lst;
            }
            catch (Exception ex)
            {
                return lst;
            }

        }

        public static List<Combobox> ListarGastosComprobante(int idioma)
        {
            List<Combobox> lst = new List<Combobox>();
            string val = "";
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Etiquetas");
                for (int i = 1; i < 4; i++)
                {
                    sp = new StoredProcedure("Trae_Etiquetas");
                    sp.AgregarParametro("codigo", "Gast" + i);
                    sp.AgregarParametro("idioma", idioma);
                    ds = sp.EjecutarProcedimiento();
                    switch (i)
                    {
                        case 1:
                            val = "N";
                            break;
                        case 2:
                            val = "P";
                            break;
                        case 3:
                            val = "J";
                            break;
                    }

                    lst.Add(new Combobox()
                    {
                        Text = ds.Tables[0].Rows[0][0].ToString(),
                        Value = val
                    });

                }
                return lst;
            }
            catch (Exception ex)
            {
                return lst;
            }

        }

        public static int TraeNumeroComprobante(int codemp, int codsuc, int tpcid, int pclid, string cpbt, int numero)
        {
            int salida = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Cabecera_Existe_Numero");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("codsuc", codsuc);
                sp.AgregarParametro("tpcid", tpcid);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("cpbt", string.IsNullOrEmpty(cpbt) ? DBNull.Value : (object)cpbt);
                sp.AgregarParametro("numero", numero);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        salida = Int32.Parse(ds.Tables[0].Rows[0]["Numero"].ToString());
                    }
                }

                return salida;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static string TraeCabeceraComprobanteEstado(CabeceraComprobante obj)
        {
            string salida = "";
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Cabecera_Comprobante_Estado");
                sp.AgregarParametro("codemp", obj.Codemp);
                sp.AgregarParametro("tpcid", obj.TipoComprobante);
                sp.AgregarParametro("numero", obj.CabeceraId);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        salida = ds.Tables[0].Rows[0]["CBE_ESTADO"].ToString();
                    }
                }

                return salida;
            }
            catch (Exception ex)
            {
                return "-1";
            }
        }

        public static int TraeUltimoNumeroComprobante(int codemp, int codsuc, int tpcid)
        {
            int salida = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("UltNum_Cabacera_Comprobantes");
                sp.AgregarParametro("cbc_codemp", codemp);
                sp.AgregarParametro("cbc_sucid", codsuc);
                sp.AgregarParametro("cbc_tpcid", tpcid);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        salida = Int32.Parse(ds.Tables[0].Rows[0][0].ToString());
                    }
                }

                return salida;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static int ActualizarGlosaCabecera(CabeceraComprobante obj)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Update_Cabecera_Comprobantes_Glosa");
                sp.AgregarParametro("cbc_codemp", obj.Codemp);
                sp.AgregarParametro("cbc_sucid", obj.Codsuc);
                sp.AgregarParametro("cbc_tpcid", obj.TipoComprobante);
                sp.AgregarParametro("cbc_numero", obj.CabeceraId);
                sp.AgregarParametro("cbc_glosa", obj.Glosa);

                int error = sp.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.ActualizarGlosaCabecera", obj.CabeceraId);
                throw ex;
            }
        }

        public static int InsertarCabecera(CabeceraComprobante obj)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Insertar_Cabecera_Comprobantes");
                sp.AgregarParametro("cbc_codemp", obj.Codemp);
                sp.AgregarParametro("cbc_sucid", obj.Codsuc);
                sp.AgregarParametro("cbc_tpcid", obj.TipoComprobante);
                sp.AgregarParametro("cbc_numero", obj.CabeceraId);
                sp.AgregarParametro("cbc_numprovcli", string.IsNullOrEmpty(obj.Numero) ? (object)obj.CabeceraId : obj.Numero);
                sp.AgregarParametro("cbc_pclid", obj.Pclid);
                sp.AgregarParametro("cbc_feccpbt", obj.FechaDocumento);
                sp.AgregarParametro("cbc_fecvenc", obj.FechaVencimiento);
                sp.AgregarParametro("cbc_fecent", obj.FechaEntrega == new DateTime() ? DBNull.Value : (object)obj.FechaEntrega);
                sp.AgregarParametro("cbc_codmon", obj.Moneda);
                sp.AgregarParametro("cbc_tipcambio", obj.TipoCambio.ToString().Replace(".", ""));
                sp.AgregarParametro("cbc_frpid", obj.FormaPago);
                sp.AgregarParametro("cbc_anio", DateTime.Today.Year);
                sp.AgregarParametro("cbc_mes", DateTime.Today.Month);
                //sp.AgregarParametro("cbc_glosa", string.IsNullOrEmpty(obj.Glosa) ? DBNull.Value : (object) obj.NumeroOC);
                sp.AgregarParametro("cbc_glosa", string.IsNullOrEmpty(obj.Glosa) ? DBNull.Value : (string.IsNullOrEmpty(obj.NumeroOC) ? DBNull.Value : (object)obj.NumeroOC.ToUpper()));
                //paso = Replace(RnT_Desc.Text, ".", ",");
                sp.AgregarParametro("cbc_porcdesc", 0);
                sp.AgregarParametro("cbc_ordcomp", string.IsNullOrEmpty(obj.NumeroOC) ? DBNull.Value : (object)obj.NumeroOC.ToUpper());
                sp.AgregarParametro("cbt_gastjud", string.IsNullOrEmpty(obj.TipoGasto) ? DBNull.Value : (object)obj.TipoGasto);
                sp.AgregarParametro("cbt_vdeid", DBNull.Value);
                sp.AgregarParametro("cbt_tntid", DBNull.Value);
                sp.AgregarParametro("cbt_tgdid", DBNull.Value);
                sp.AgregarParametro("cbt_ttlid", DBNull.Value);
                sp.AgregarParametro("cbc_exento", 0);
                sp.AgregarParametro("cbc_pcsid", string.IsNullOrEmpty(obj.Sucursal) ? DBNull.Value : (object)obj.Sucursal);
                sp.AgregarParametro("cbc_feccont", obj.FechaContabilizacion == new DateTime() ? DBNull.Value : (object)obj.FechaContabilizacion);
                sp.AgregarParametro("cbc_fecoc", obj.FechaOrdenCompra == new DateTime() ? DBNull.Value : (object)obj.FechaOrdenCompra);

                int error = sp.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.InsertarCarteraClientesEstadosAcciones", obj.CabeceraId);
                throw ex;
            }
        }

        public static int InsertarCabeceraOP(CabeceraComprobante obj)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Insertar_Cabecera_Comprobantes_OP");
                sp.AgregarParametro("cbo_codemp", obj.Codemp);
                sp.AgregarParametro("cbo_sucid", obj.Codsuc);
                sp.AgregarParametro("cbo_tpcid", obj.TipoComprobante);
                sp.AgregarParametro("cbo_numero", obj.CabeceraId);

                int error = sp.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.InsertarCarteraClientesEstadosAcciones", obj.CabeceraId);
                throw ex;
            }
        }

        public static int InsertarCabeceraEstados(CabeceraComprobante obj, UserSession objSession)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Insertar_Cabecera_Comprobantes_Estados");
                sp.AgregarParametro("cbe_codemp", obj.Codemp);
                sp.AgregarParametro("cbe_sucid", obj.Codsuc);
                sp.AgregarParametro("cbe_tpcid", obj.TipoComprobante);
                sp.AgregarParametro("cbe_numero", obj.CabeceraId);
                sp.AgregarParametro("cbe_estado", obj.Estado);
                sp.AgregarParametro("cbe_fecha", DateTime.Now);
                sp.AgregarParametro("cbe_usrid", objSession.UserId);
                sp.AgregarParametro("cbe_ippc", objSession.IpPc);
                sp.AgregarParametro("cbe_ipred", objSession.IpRed);
                sp.AgregarParametro("cbe_comentario", "");

                int error = sp.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.InsertarCarteraClientesEstadosAcciones", obj.CabeceraId);
                throw ex;
            }
        }

        public static int ActualizarCabecera(CabeceraComprobante obj)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Insertar_Cabecera_Comprobantes");
                sp.AgregarParametro("cbc_codemp", obj.Codemp);
                sp.AgregarParametro("cbc_sucid", obj.Codsuc);
                sp.AgregarParametro("cbc_tpcid", obj.TipoComprobante);
                sp.AgregarParametro("cbc_numero", obj.CabeceraId);
                sp.AgregarParametro("cbc_numprovcli", string.IsNullOrEmpty(obj.Numero) ? (object)obj.CabeceraId : obj.Numero);
                sp.AgregarParametro("cbc_pclid", obj.Pclid);
                sp.AgregarParametro("cbc_feccpbt", obj.FechaDocumento);
                sp.AgregarParametro("cbc_fecvenc", obj.FechaVencimiento);
                sp.AgregarParametro("cbc_fecent", obj.FechaEntrega == new DateTime() ? DBNull.Value : (object)obj.FechaEntrega);
                sp.AgregarParametro("cbc_codmon", obj.Moneda);
                sp.AgregarParametro("cbc_tipcambio", obj.TipoCambio.ToString().Replace(".", ""));
                sp.AgregarParametro("cbc_frpid", obj.FormaPago);
                sp.AgregarParametro("cbc_anio", DateTime.Today.Year);
                sp.AgregarParametro("cbc_mes", DateTime.Today.Month);
                sp.AgregarParametro("cbc_glosa", obj.Glosa + ", " + obj.RutCliente + ", " + obj.TipoComprobanteDesc + ", " + "Numero" + ":" + obj.CabeceraId);
                //paso = Replace(RnT_Desc.Text, ".", ",");
                sp.AgregarParametro("cbc_porcdesc", 0);
                sp.AgregarParametro("cbc_ordcomp", string.IsNullOrEmpty(obj.NumeroOC) ? DBNull.Value : (object)obj.NumeroOC.ToUpper());
                sp.AgregarParametro("cbt_gastjud", obj.TipoGasto);
                sp.AgregarParametro("cbt_vdeid", DBNull.Value);
                sp.AgregarParametro("cbt_tntid", DBNull.Value);
                sp.AgregarParametro("cbt_tgdid", DBNull.Value);
                sp.AgregarParametro("cbt_ttlid", DBNull.Value);
                //sp.AgregarParametro("cbc_exento", 0);
                sp.AgregarParametro("cbc_pcsid", string.IsNullOrEmpty(obj.Sucursal) ? DBNull.Value : (object)obj.Sucursal);
                sp.AgregarParametro("cbc_feccont", obj.FechaContabilizacion == new DateTime() ? DBNull.Value : (object)obj.FechaContabilizacion);
                sp.AgregarParametro("cbc_fecoc", obj.FechaOrdenCompra == new DateTime() ? DBNull.Value : (object)obj.FechaOrdenCompra);

                int error = sp.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.InsertarCarteraClientesEstadosAcciones", obj.CabeceraId);
                throw ex;
            }
        }

        public static int EliminarDetalleCabecera(DetalleCabeceraComprobante obj)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Delete_Detalle_Comprobantes");
                sp.AgregarParametro("dcc_codemp", obj.Codemp);
                sp.AgregarParametro("dcc_sucid", obj.Codsuc);
                sp.AgregarParametro("dcc_tpcid", obj.TipoComprobante);
                sp.AgregarParametro("dcc_numero", obj.CabeceraId);
                sp.AgregarParametro("dcc_item", obj.Item);

                int error = sp.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.EliminarDetalleCabecera", obj.CabeceraId);
                throw ex;
            }
        }

        public static int InsertarDetalleCabecera(DetalleCabeceraComprobante obj)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Insertar_Detalle_Comprobantes");
                sp.AgregarParametro("dcc_codemp", obj.Codemp);
                sp.AgregarParametro("dcc_sucid", obj.Codsuc);
                sp.AgregarParametro("dcc_tpcid", obj.TipoComprobante);
                sp.AgregarParametro("dcc_numero", obj.CabeceraId);
                sp.AgregarParametro("dcc_item", obj.Item);
                sp.AgregarParametro("dcc_insid", obj.Insid == 0 ? DBNull.Value : (object)obj.Insid);
                sp.AgregarParametro("dcc_prodid", obj.Prodid == 0 ? DBNull.Value : (object)obj.Prodid);
                sp.AgregarParametro("dcc_pclid", obj.Pclid == 0 ? DBNull.Value : (object)obj.Pclid);
                sp.AgregarParametro("dcc_ctcid", obj.Ctcid == 0 ? DBNull.Value : (object)obj.Ctcid);
                sp.AgregarParametro("dcc_ccbid", obj.Ccbid == 0 ? DBNull.Value : (object)obj.Ccbid);
                sp.AgregarParametro("dcc_prereal", obj.PrecioReal);
                sp.AgregarParametro("dcc_precio", obj.Precio);

                sp.AgregarParametro("dcc_cantidad", obj.Cantidad);
                sp.AgregarParametro("dcc_saldo", obj.Saldo);
                sp.AgregarParametro("dcc_neto", obj.Neto);
                sp.AgregarParametro("dcc_impuesto", obj.Impuesto);
                sp.AgregarParametro("dcc_retenido", obj.Retenido);
                sp.AgregarParametro("dcc_total", obj.Total);
                sp.AgregarParametro("dcc_interes", obj.Interes);
                sp.AgregarParametro("dcc_honorario", obj.Honorario);
                sp.AgregarParametro("dcc_gastpre", obj.GastoPrejudicial);
                sp.AgregarParametro("dcc_gastjud", obj.GastoJudicial);
                sp.AgregarParametro("dcc_porcfact", obj.PorcFact);
                sp.AgregarParametro("dcc_porchon", obj.PorcHono);

                sp.AgregarParametro("dcc_bodid", obj.Bodid == 0 ? DBNull.Value : (object)obj.Bodid);
                sp.AgregarParametro("dcc_bdsid", obj.Bdsid == 0 ? DBNull.Value : (object)obj.Bdsid);
                sp.AgregarParametro("dcc_posicion", obj.Posicion == 0 ? DBNull.Value : (object)obj.Posicion);
                sp.AgregarParametro("dcc_tpcidpad", obj.Tpcidpad == 0 ? DBNull.Value : (object)obj.Tpcidpad);
                sp.AgregarParametro("dcc_numeropad", obj.Numeropad == 0 ? DBNull.Value : (object)obj.Numeropad);
                sp.AgregarParametro("dcc_itempad", obj.Itempad == 0 ? DBNull.Value : (object)obj.Itempad);
                sp.AgregarParametro("dcc_bodiddes", obj.Bodiddes == 0 ? DBNull.Value : (object)obj.Bodiddes);
                sp.AgregarParametro("dcc_bdsiddes", obj.Bdsiddes == 0 ? DBNull.Value : (object)obj.Bdsiddes);
                sp.AgregarParametro("dcc_posiciondes", obj.Posiciondes == 0 ? DBNull.Value : (object)obj.Posiciondes);
                sp.AgregarParametro("dcc_numserie", obj.NumeroSerie);
                sp.AgregarParametro("dcc_numserieprov", string.IsNullOrEmpty(obj.NumeroSerieProv) ? DBNull.Value : (object)obj.NumeroSerieProv);
                sp.AgregarParametro("dcc_cantebj", obj.Cantebj);

                sp.AgregarParametro("dcc_ltpid", string.IsNullOrEmpty(obj.Ltpid) ? DBNull.Value : (object)obj.Ltpid);
                sp.AgregarParametro("dcc_bscid", string.IsNullOrEmpty(obj.Bscid) ? DBNull.Value : (object)obj.Bscid);
                sp.AgregarParametro("dcc_bsciddes", string.IsNullOrEmpty(obj.Bsciddes) ? DBNull.Value : (object)obj.Bsciddes);
                sp.AgregarParametro("dcc_anio", string.IsNullOrEmpty(obj.Anio) ? DBNull.Value : (object)obj.Anio);
                sp.AgregarParametro("dcc_numapl", string.IsNullOrEmpty(obj.NumApl) ? DBNull.Value : (object)obj.NumApl);
                sp.AgregarParametro("dcc_itemapl", string.IsNullOrEmpty(obj.ItemApl) ? DBNull.Value : (object)obj.ItemApl);
                sp.AgregarParametro("dcc_valrem", string.IsNullOrEmpty(obj.ValRem) ? DBNull.Value : (object)obj.ValRem);
                sp.AgregarParametro("dcc_comentario", string.IsNullOrEmpty(obj.Comentario) ? DBNull.Value : (object)obj.Comentario);
                sp.AgregarParametro("dcc_subitem", string.IsNullOrEmpty(obj.Subitem) ? DBNull.Value : (object)obj.Subitem);
                sp.AgregarParametro("dcc_exento", obj.Exento);

                int error = sp.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.EliminarDetalleCabecera", obj.CabeceraId);
                throw ex;
            }
        }

        public static int ActualizarCabeceraTotales(CabeceraComprobante obj)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Update_Cabecera_Comprobantes_Totales");
                sp.AgregarParametro("cbc_codemp", obj.Codemp);
                sp.AgregarParametro("cbc_sucid", obj.Codsuc);
                sp.AgregarParametro("cbc_tpcid", obj.TipoComprobante);
                sp.AgregarParametro("cbc_numero", obj.CabeceraId);
                sp.AgregarParametro("cbc_neto", obj.Neto);
                sp.AgregarParametro("cbc_impuestos", obj.Impuestos);
                sp.AgregarParametro("cbc_retenido", obj.Retenido);
                sp.AgregarParametro("cbc_descuentos", obj.Descuento);
                sp.AgregarParametro("cbc_final", obj.Final);
                sp.AgregarParametro("cbc_saldo", obj.Saldo);
                sp.AgregarParametro("cbc_exento", obj.Exento);

                int error = sp.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.InsertarCarteraClientesEstadosAcciones", obj.CabeceraId);
                throw ex;
            }
        }

        public static decimal TraeMontoCabeceraTotales(CabeceraComprobante obj)
        {
            decimal salida = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Monto_Cabecera_Comprobantes_Totales");
                sp.AgregarParametro("cbc_codemp", obj.Codemp);
                sp.AgregarParametro("cbc_sucid", obj.Codsuc);
                sp.AgregarParametro("cbc_tpcid", obj.TipoComprobante);
                sp.AgregarParametro("cbc_numero", obj.CabeceraId);

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        salida = decimal.Parse(ds.Tables[0].Rows[0][0].ToString());
                    }
                }

                return salida;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.TraeMontoCabeceraTotales", obj.CabeceraId);
                return -1;
            }
        }

        public static List<Autocomplete> ListarItemComprobante(string nombre, int codemp, int tipprod, int pclid, string gasto)
        {
            List<Autocomplete> lst = new List<Autocomplete>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Item_Detalle_Boleta");
                sp.AgregarParametro("texto", nombre);
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("tipprod", tipprod);
                sp.AgregarParametro("provcli", pclid);
                sp.AgregarParametro("gastjud", gasto);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Autocomplete()
                        {
                            label = ds.Tables[0].Rows[i][0].ToString(),
                            value = ds.Tables[0].Rows[i][1].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lst;
        }

        public static CabeceraComprobante TraeRolCabeceraComprobante(CabeceraComprobante obj)
        {
            int salida = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Rol_Comprobante");
                sp.AgregarParametro("codemp", obj.Codemp);
                sp.AgregarParametro("numero", obj.NumeroRol);
                sp.AgregarParametro("tipo", obj.TipoRol);
                sp.AgregarParametro("tribunal", obj.Tribunal);

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        obj.Pclid = Int32.Parse(ds.Tables[0].Rows[0]["Pclid"].ToString());
                        obj.Ctcid = Int32.Parse(ds.Tables[0].Rows[0]["Ctcid"].ToString());
                        obj.Rolid = Int32.Parse(ds.Tables[0].Rows[0]["Rolid"].ToString());
                        obj.NombreCliente = ds.Tables[0].Rows[0]["NombreCliente"].ToString();
                        obj.Sucursal = ds.Tables[0].Rows[0]["IdSucursal"].ToString();
                        obj.NombreSucursal = ds.Tables[0].Rows[0]["Sucursal"].ToString();
                        obj.RutDeudor = ds.Tables[0].Rows[0]["RutDeudor"].ToString();
                        obj.NombreDeudor = ds.Tables[0].Rows[0]["NombreDeudor"].ToString();
                        obj.NumeroRol = ds.Tables[0].Rows[0]["NumeroRol"].ToString();
                        obj.Asegurados = ds.Tables[0].Rows[0]["Asegurados"].ToString();
                    }
                }

                return obj;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.TraeMontoCabeceraTotales", obj.CabeceraId);
                return new dto.CabeceraComprobante();
            }
        }

        public static ClasificacionInsumo TraeClasificacionInsumo(int codemp, int insid)
        {
            ClasificacionInsumo obj = new ClasificacionInsumo();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Clasificacion_Insumo");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("insid", insid);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        obj = new ClasificacionInsumo()
                        {
                            TipoStock = Int32.Parse(ds.Tables[0].Rows[0]["ins_tipo"].ToString()),
                            PorcentajeArancel = decimal.Parse(ds.Tables[0].Rows[0]["ins_porcaran"].ToString()),
                            Exento = ds.Tables[0].Rows[0]["ins_exento"].ToString(),
                            Arancel = ds.Tables[0].Rows[0]["ins_arancel"].ToString(),
                            ImputableCliente = ds.Tables[0].Rows[0]["ins_impcli"].ToString()
                        };
                    }
                }

                return obj;
            }
            catch (Exception ex)
            {
                return obj;
            }
        }

        public static ImpuestoComprobante TraeImpuestoComprobante(int codemp, int sucid, int tpcid, int numero, string retenido) //@codemp int,@sucid int, @tpcid int, @numero int,@retenido
        {
            ImpuestoComprobante obj = new ImpuestoComprobante();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Impuesto_Comprobante");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("sucid", sucid);
                sp.AgregarParametro("tpcid", tpcid);
                sp.AgregarParametro("numero", numero);
                sp.AgregarParametro("retenido", retenido);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        obj = new ImpuestoComprobante()
                        {
                            Nombre = ds.Tables[0].Rows[0]["Nombre"].ToString(),
                            Porcentaje = decimal.Parse(ds.Tables[0].Rows[0]["Porcentaje"].ToString()),
                            Retenido = ds.Tables[0].Rows[0]["Retenido"].ToString()
                        };
                    }
                }

                return obj;
            }
            catch (Exception ex)
            {
                return obj;
            }
        }

        public static int InsertarDetalleComprobanteRol(DetalleCabeceraComprobante obj)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Insertar_Detalle_Comprobantes_Rol");
                sp.AgregarParametro("dcr_codemp", obj.Codemp);
                sp.AgregarParametro("dcr_sucid", obj.Codsuc);
                sp.AgregarParametro("dcr_tpcid ", obj.TipoComprobante);
                sp.AgregarParametro("dcr_numero", obj.CabeceraId);
                sp.AgregarParametro("dcr_item", obj.Item);
                sp.AgregarParametro("dcr_rolid", obj.Rolid);
                sp.AgregarParametro("dcr_monto", obj.Monto);

                int error = sp.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.InsertarDetalleComprobanteRol", obj.CabeceraId);
                throw ex;
            }
        }

        public static int InsertarDetalleComprobanteProvCli(DetalleCabeceraComprobante obj)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Insertar_Detalle_Comprobantes_Provcli");
                sp.AgregarParametro("dbp_codemp", obj.Codemp);
                sp.AgregarParametro("dbp_sucid", obj.Codsuc);
                sp.AgregarParametro("dbp_tpcid ", obj.TipoComprobante);
                sp.AgregarParametro("dbp_numero", obj.CabeceraId);
                sp.AgregarParametro("dbp_item", obj.Item);
                sp.AgregarParametro("dbp_pclid", obj.Pclid);
                sp.AgregarParametro("dbp_ctcid", obj.Ctcid);
                sp.AgregarParametro("dbp_monto", obj.Monto);

                int error = sp.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.InsertarDetalleComprobanteRol", obj.CabeceraId);
                throw ex;
            }
        }

        public static int ActualizarDetalleCabecera(DetalleCabeceraComprobante obj)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Update_Detalle_Comprobantes");
                sp.AgregarParametro("dcc_codemp", obj.Codemp);
                sp.AgregarParametro("dcc_sucid", obj.Codsuc);
                sp.AgregarParametro("dcc_tpcid", obj.TipoComprobante);
                sp.AgregarParametro("dcc_numero", obj.CabeceraId);
                sp.AgregarParametro("dcc_item", obj.Item);
                sp.AgregarParametro("dcc_insid", obj.Insid);
                sp.AgregarParametro("dcc_prodid", obj.Prodid == 0 ? DBNull.Value : (object)obj.Prodid);
                sp.AgregarParametro("dcc_pclid", obj.Pclid == 0 ? DBNull.Value : (object)obj.Pclid);
                sp.AgregarParametro("dcc_ctcid", obj.Ctcid == 0 ? DBNull.Value : (object)obj.Ctcid);
                sp.AgregarParametro("dcc_ccbid", obj.Ccbid == 0 ? DBNull.Value : (object)obj.Ccbid);
                sp.AgregarParametro("dcc_prereal", obj.PrecioReal);
                sp.AgregarParametro("dcc_precio", obj.Precio);

                sp.AgregarParametro("dcc_cantidad", obj.Cantidad);
                sp.AgregarParametro("dcc_saldo", obj.Saldo);
                sp.AgregarParametro("dcc_neto", obj.Neto);
                sp.AgregarParametro("dcc_impuesto", obj.Impuesto);
                sp.AgregarParametro("dcc_retenido", obj.Retenido);
                sp.AgregarParametro("dcc_total", obj.Total);
                sp.AgregarParametro("dcc_interes", obj.Interes);
                sp.AgregarParametro("dcc_honorario", obj.Honorario);
                sp.AgregarParametro("dcc_gastpre", obj.GastoPrejudicial);
                sp.AgregarParametro("dcc_gastjud", obj.GastoJudicial);
                sp.AgregarParametro("dcc_porcfact", obj.PorcFact);
                sp.AgregarParametro("dcc_porchon", obj.PorcHono);

                sp.AgregarParametro("dcc_bodid", obj.Bodid == 0 ? DBNull.Value : (object)obj.Bodid);
                sp.AgregarParametro("dcc_bdsid", obj.Bdsid == 0 ? DBNull.Value : (object)obj.Bdsid);
                sp.AgregarParametro("dcc_posicion", obj.Posicion == 0 ? DBNull.Value : (object)obj.Posicion);
                sp.AgregarParametro("dcc_tpcidpad", obj.Tpcidpad == 0 ? DBNull.Value : (object)obj.Tpcidpad);
                sp.AgregarParametro("dcc_numeropad", obj.Numeropad == 0 ? DBNull.Value : (object)obj.Numeropad);
                sp.AgregarParametro("dcc_itempad", obj.Itempad == 0 ? DBNull.Value : (object)obj.Itempad);
                sp.AgregarParametro("dcc_bodiddes", obj.Bodiddes == 0 ? DBNull.Value : (object)obj.Bodiddes);
                sp.AgregarParametro("dcc_bdsiddes", obj.Bdsiddes == 0 ? DBNull.Value : (object)obj.Bdsiddes);
                sp.AgregarParametro("dcc_posiciondes", obj.Posiciondes == 0 ? DBNull.Value : (object)obj.Posiciondes);
                sp.AgregarParametro("dcc_numserie", obj.NumeroSerie);
                sp.AgregarParametro("dcc_numserieprov", string.IsNullOrEmpty(obj.NumeroSerieProv) ? DBNull.Value : (object)obj.NumeroSerieProv);
                sp.AgregarParametro("dcc_cantebj", obj.Cantebj);

                sp.AgregarParametro("dcc_ltpid", string.IsNullOrEmpty(obj.Ltpid) ? DBNull.Value : (object)obj.Ltpid);
                sp.AgregarParametro("dcc_bscid", string.IsNullOrEmpty(obj.Bscid) ? DBNull.Value : (object)obj.Bscid);
                sp.AgregarParametro("dcc_bsciddes", string.IsNullOrEmpty(obj.Bsciddes) ? DBNull.Value : (object)obj.Bsciddes);
                sp.AgregarParametro("dcc_anio", string.IsNullOrEmpty(obj.Anio) ? DBNull.Value : (object)obj.Anio);
                sp.AgregarParametro("dcc_numapl", string.IsNullOrEmpty(obj.NumApl) ? DBNull.Value : (object)obj.NumApl);
                sp.AgregarParametro("dcc_itemapl", string.IsNullOrEmpty(obj.ItemApl) ? DBNull.Value : (object)obj.ItemApl);
                sp.AgregarParametro("dcc_valrem", string.IsNullOrEmpty(obj.ValRem) ? DBNull.Value : (object)obj.ValRem);
                sp.AgregarParametro("dcc_comentario", string.IsNullOrEmpty(obj.Comentario) ? DBNull.Value : (object)obj.Comentario);
                sp.AgregarParametro("dcc_subitem", string.IsNullOrEmpty(obj.Subitem) ? DBNull.Value : (object)obj.Subitem);
                sp.AgregarParametro("dcc_exento", obj.Exento);

                int error = sp.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.EliminarDetalleCabecera", obj.CabeceraId);
                throw ex;
            }
        }

        public static int TraePadreDetalleComprobante(DetalleCabeceraComprobante obj) //int codemp, int sucid, int tpcid, int numero, string item) //@codemp int,@sucid int, @tpcid int, @numero int,@retenido
        {
            //ImpuestoComprobante obj = new ImpuestoComprobante();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Padre_Detalle_Comprobante");
                sp.AgregarParametro("codemp", obj.Codemp);
                sp.AgregarParametro("sucid", obj.Codsuc);
                sp.AgregarParametro("tpcid", obj.TipoComprobante);
                sp.AgregarParametro("numero", obj.CabeceraId);
                sp.AgregarParametro("item", obj.Item);
                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        obj.Tpcidpad = Int32.Parse(ds.Tables[0].Rows[0]["tpcidpad"].ToString());
                        obj.Numeropad = Int32.Parse(ds.Tables[0].Rows[0]["numeropad"].ToString());
                        obj.Itempad = Int32.Parse(ds.Tables[0].Rows[0]["itempad"].ToString());
                        return 1;
                    }
                    else
                    {
                        obj.Tpcidpad = 0;
                        obj.Numeropad = 0;
                        obj.Itempad = 0;
                        return 0;
                    }

                }
                else
                {
                    obj.Tpcidpad = 0;
                    obj.Numeropad = 0;
                    obj.Itempad = 0;
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.TraePadreDetalleComprobante", obj.CabeceraId);
                return -1;
                //throw ex;
            }
        }

        public static int CuadrarDetalleCabeceraComprobante(CabeceraComprobante obj)
        {
            try
            {
                DetalleCabeceraComprobante d = obj.DetalleC.FirstOrDefault();
                StoredProcedure sp = new StoredProcedure("_Comprobante_Cuadrar_Detalle");
                sp.AgregarParametro("codemp", d.Codemp);
                sp.AgregarParametro("sucid", d.Codsuc);
                sp.AgregarParametro("tpcid", d.TipoComprobante);
                sp.AgregarParametro("numero", d.CabeceraId);
                sp.AgregarParametro("item", d.Item);
                sp.AgregarParametro("tipo_cambio", obj.TipoCambio);
                sp.AgregarParametro("precio", d.PrecioReal);
                sp.AgregarParametro("cantidad", d.Cantidad);

                int error = sp.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.CuadrarDetalleCabeceraComprobante", obj.CabeceraId);
                throw ex;
            }
        }


        public static int BuscarUltimoItemDetalle(int codemp, int sucid, int tpcid, int numero)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("UltNum_Detalle_Comprobantes_Item");
                sp.AgregarParametro("dcc_codemp", codemp);
                sp.AgregarParametro("dcc_sucid", sucid);
                sp.AgregarParametro("dcc_tpcid", tpcid);
                sp.AgregarParametro("dcc_numero", numero);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        return Int32.Parse(ds.Tables[0].Rows[0][0].ToString());
                    }
                    else
                    {
                        return -1;
                    }
                }
                else
                {
                    return -1;
                }

            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.BuscarUltimoItemDetalle", numero);
                return -1;
            }

        }

        public static List<dto.DetalleComprobanteCompra> ListarDetalleComprobanteCompra(int codemp, int sucid, int tpcid, int numero, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.DetalleComprobanteCompra> lst = new List<dto.DetalleComprobanteCompra>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Detalle_Comprobante_Compra_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("sucid", sucid);
                sp.AgregarParametro("tpcid", tpcid);
                sp.AgregarParametro("numero", numero);
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
                        lst.Add(new dto.DetalleComprobanteCompra()
                        {
                            Item = Int32.Parse(ds.Tables[0].Rows[i]["Item"].ToString()),
                            Insid = Int32.Parse(ds.Tables[0].Rows[i]["Insid"].ToString()),
                            Codigo = ds.Tables[0].Rows[i]["Codigo"].ToString(),
                            Nombre = ds.Tables[0].Rows[i]["Nombre"].ToString(),
                            Abreviado = ds.Tables[0].Rows[i]["Abreviado"].ToString(),
                            PrecioReal = decimal.Parse(ds.Tables[0].Rows[i]["PrecioReal"].ToString()),
                            Precio = decimal.Parse(ds.Tables[0].Rows[i]["Precio"].ToString()),
                            Cantidad = decimal.Parse(ds.Tables[0].Rows[i]["Cantidad"].ToString()),
                            Neto = decimal.Parse(ds.Tables[0].Rows[i]["Neto"].ToString()),
                            Total = decimal.Parse(ds.Tables[0].Rows[i]["Total"].ToString()),
                            Impuesto = decimal.Parse(ds.Tables[0].Rows[i]["Impuesto"].ToString()),
                            Retenido = ds.Tables[0].Rows[i]["Retenido"].ToString(),
                            TotalNeto = decimal.Parse(ds.Tables[0].Rows[i]["TotalNeto"].ToString()),
                            NombreBodega = ds.Tables[0].Rows[i]["NombreBodega"].ToString(),
                            NombreSectorBodega = ds.Tables[0].Rows[i]["NombreSectorBodega"].ToString(),
                            ArchivoEstampe = ds.Tables[0].Rows[i]["ArchivoEstampe"].ToString(),
                            NombreArchivo = ds.Tables[0].Rows[i]["NombreArchivo"].ToString(),
                            FecJud = ds.Tables[0].Rows[i]["FecJud"].ToString()
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.ListarDetalleComprobanteCompra", numero);
                return lst;
            }
        }

        public static int ListarDetalleComprobanteCompraCount(int codemp, int sucid, int tpcid, int numero, string where, string sidx, string sord, int inicio, int limite)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Detalle_Comprobante_Compra_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("sucid", sucid);
                sp.AgregarParametro("tpcid", tpcid);
                sp.AgregarParametro("numero", numero);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        return Int32.Parse(ds.Tables[0].Rows[0]["count"].ToString());
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.ListarDetalleComprobanteCompraCount", numero);
                return -1;
            }
        }

        public static CabeceraComprobante BuscarComprobante(int codemp, int sucid, int tpcid, int numero)
        {
            CabeceraComprobante obj = new CabeceraComprobante();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Buscar_Comprobante");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("sucid", sucid);
                sp.AgregarParametro("tpcid", tpcid);
                sp.AgregarParametro("numero", numero);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        obj.Codemp = Int32.Parse(ds.Tables[0].Rows[0]["cbc_codemp"].ToString());
                        obj.Sucursal = ds.Tables[0].Rows[0]["cbc_sucid"].ToString();
                        obj.TipoComprobante = Int32.Parse(ds.Tables[0].Rows[0]["cbc_tpcid"].ToString());
                        obj.CabeceraId = Int32.Parse(ds.Tables[0].Rows[0]["cbc_numero"].ToString());
                        obj.Numero = ds.Tables[0].Rows[0]["cbc_numprovcli"].ToString();
                        obj.Pclid = Int32.Parse(ds.Tables[0].Rows[0]["cbc_pclid"].ToString());
                        obj.FechaIngreso = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["cbc_fecemi"].ToString()) ? new DateTime() : DateTime.Parse(ds.Tables[0].Rows[0]["cbc_fecemi"].ToString());
                        obj.FechaDocumento = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["cbc_feccpbt"].ToString()) ? new DateTime() : DateTime.Parse(ds.Tables[0].Rows[0]["cbc_feccpbt"].ToString());
                        obj.FechaVencimiento = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["cbc_fecvenc"].ToString()) ? new DateTime() : DateTime.Parse(ds.Tables[0].Rows[0]["cbc_fecvenc"].ToString());
                        obj.FechaEntrega = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["cbc_fecent"].ToString()) ? new DateTime() : DateTime.Parse(ds.Tables[0].Rows[0]["cbc_fecent"].ToString());
                        obj.FechaOrdenCompra = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["cbc_fecoc"].ToString()) ? new DateTime() : DateTime.Parse(ds.Tables[0].Rows[0]["cbc_fecoc"].ToString());
                        obj.FechaContabilizacion = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["cbc_feccont"].ToString()) ? new DateTime() : DateTime.Parse(ds.Tables[0].Rows[0]["cbc_feccont"].ToString());
                        obj.Moneda = ds.Tables[0].Rows[0]["cbc_codmon"].ToString();
                        obj.TipoCambio = decimal.Parse(ds.Tables[0].Rows[0]["cbc_tipcambio"].ToString());
                        obj.TipoGasto = ds.Tables[0].Rows[0]["cbt_gastjud"].ToString();
                        obj.Glosa = ds.Tables[0].Rows[0]["cbc_glosa"].ToString();
                        obj.Estado = ds.Tables[0].Rows[0]["cbt_estado"].ToString();
                        obj.FormaPago = ds.Tables[0].Rows[0]["cbc_frpid"].ToString();
                        obj.Descuento = decimal.Parse(ds.Tables[0].Rows[0]["cbc_descuentos"].ToString());
                        obj.Neto = decimal.Parse(ds.Tables[0].Rows[0]["cbc_neto"].ToString());
                        obj.Impuestos = decimal.Parse(ds.Tables[0].Rows[0]["cbc_impuestos"].ToString());
                        obj.Retenido = decimal.Parse(ds.Tables[0].Rows[0]["cbc_retenido"].ToString());
                        obj.Final = decimal.Parse(ds.Tables[0].Rows[0]["cbc_final"].ToString());
                        obj.Saldo = decimal.Parse(ds.Tables[0].Rows[0]["cbc_saldo"].ToString());
                        obj.Ordcomp = ds.Tables[0].Rows[0]["cbc_ordcomp"].ToString();
                        obj.RutCliente = ds.Tables[0].Rows[0]["pcl_rut"].ToString() + " - " + ds.Tables[0].Rows[0]["pcl_nomfant"].ToString();


                    }
                }
                sp = new StoredProcedure("_Buscar_Comprobante_rol");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("sucid", sucid);
                sp.AgregarParametro("tpcid", tpcid);
                sp.AgregarParametro("numero", numero);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        obj.Rolid = Int32.Parse(ds.Tables[0].Rows[0]["rol_rolid"].ToString());
                        obj.Tribunal = ds.Tables[0].Rows[0]["rol_trbid"].ToString();
                        obj.NumeroRol = ds.Tables[0].Rows[0]["rol_numero"].ToString();
                        obj.TipoRol = ds.Tables[0].Rows[0]["rol_tipo_rol"].ToString();
                        obj.NombreTribunal = ds.Tables[0].Rows[0]["trb_nombre"].ToString();
                        obj.Ctcid = Int32.Parse(ds.Tables[0].Rows[0]["rol_ctcid"].ToString());
                    }
                }

                return obj;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.BuscarComprobante", obj.CabeceraId);
                return obj;
            }
        }

        public static string AnularComprobante(CabeceraComprobante obj)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Comprobante_Anular");
                sp.AgregarParametro("codemp", obj.Codemp);
                sp.AgregarParametro("sucid", obj.Codsuc);
                sp.AgregarParametro("tpcid", obj.TipoComprobante);
                sp.AgregarParametro("numero", obj.CabeceraId);
                sp.AgregarParametro("tipo_cambio", obj.TipoCambio);

                int error = sp.EjecutarProcedimientoTrans();
                if (error > 0)
                {
                    return "";
                }
                else
                {
                    return error.ToString();
                }
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.CuadrarDetalleCabeceraComprobante", obj.CabeceraId);
                return "El Comprobante no ha sido anulado.";
            }
        }

        public static string EliminarComprobante(CabeceraComprobante obj)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Comprobante_Eliminar");
                sp.AgregarParametro("codemp", obj.Codemp);
                sp.AgregarParametro("tpcid", obj.TipoComprobante);
                sp.AgregarParametro("numero", obj.CabeceraId);

                int error = sp.EjecutarProcedimientoTrans();
                if (error > 0)
                {
                    return "";
                }
                else
                {
                    return error.ToString();
                }
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.CuadrarDetalleCabeceraComprobante", obj.CabeceraId);
                return "El Comprobante no ha sido anulado.";
            }
        }

        public static List<Combobox> TraeSubCarteraComprobante(int codemp, int rolid)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Subcartera_por_Rol");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("rolid", rolid);
                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lst.Add(new Combobox()
                        {
                            Text = ds.Tables[0].Rows[0][1].ToString(),
                            Value = ds.Tables[0].Rows[0][0].ToString()
                        });
                        lst.Add(new Combobox()
                        {
                            Text = ds.Tables[0].Rows[0][3].ToString(),
                            Value = ds.Tables[0].Rows[0][2].ToString()
                        });
                        lst.Add(new Combobox()
                        {
                            Text = ds.Tables[0].Rows[0][5].ToString(),
                            Value = ds.Tables[0].Rows[0][4].ToString()
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

        public static List<Autocomplete> ListarTribunalAuto(string nombre)
        {
            List<Autocomplete> lst = new List<Autocomplete>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Tribunal_Auto");
                sp.AgregarParametro("nombre", nombre);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Autocomplete()
                        {
                            label = ds.Tables[0].Rows[i][1].ToString(),
                            value = ds.Tables[0].Rows[i][0].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lst;
        }

        #endregion
        #region "Terceros"
        public static List<dto.TerceroDocumento> ListarTercerosDocumentos(int codemp, int pclid, int ctcid, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.TerceroDocumento> lst = new List<dto.TerceroDocumento>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_TercerosSocios_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
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
                        lst.Add(new dto.TerceroDocumento()
                        {
                            TerceroId = Int32.Parse(ds.Tables[0].Rows[i]["terceroid"].ToString()),
                            Rut = ds.Tables[0].Rows[i]["rut"].ToString(),
                            Nombre = ds.Tables[0].Rows[i]["Nombre"].ToString()
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carterera..dao.ListarTercerosDocumentos", 0);
                return lst;
            }
        }
        #endregion

        #region "Motivos Castigos Devolucion"
        public static List<dto.MotivoCastigoDevolucion> ListarMotivoCastigoDevolucion(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.MotivoCastigoDevolucion> lst = new List<dto.MotivoCastigoDevolucion>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Tipo_Motivos_Castigo");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idioma", idioma);
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
                        lst.Add(new dto.MotivoCastigoDevolucion()
                        {
                            TipoId = Int32.Parse(ds.Tables[0].Rows[i]["TIPOID"].ToString()),
                            Nombre = ds.Tables[0].Rows[i]["NOMBRE"].ToString()
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Cartera..dao.ListarMotivoCastigoDevolucion", 0);
                return lst;
            }
        }

        public static int GrabarMotivoComprobante(int codemp, int codSucursal, int tipoComprobante, int folio, int codigoMotivo, int ctcid)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Insertar_Cabecera_Comprobantes_Motivos_Castigos");
                sp.AgregarParametro("cbm_codemp", codemp);
                sp.AgregarParametro("cbm_sucid", codSucursal);
                sp.AgregarParametro("cbm_tpcid", tipoComprobante);
                sp.AgregarParametro("cbm_numero", folio);
                sp.AgregarParametro("cbm_tmcid", codigoMotivo);
                sp.AgregarParametro("cbm_ctcid", ctcid);

                int error = sp.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.GrabarMotivoComprobante", folio);
                throw ex;
            }
        }
        #endregion

        public static int ActualizarCabeceraTotales(CabeceraComprobante obj, decimal monto)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Update_Cabecera_Comprobantes_Totales");
                sp.AgregarParametro("cbc_codemp", obj.Codemp);
                sp.AgregarParametro("cbc_sucid", obj.Codsuc);
                sp.AgregarParametro("cbc_tpcid", obj.TipoComprobante);
                sp.AgregarParametro("cbc_numero", obj.CabeceraId);
                sp.AgregarParametro("cbc_neto", monto);
                sp.AgregarParametro("cbc_impuestos", obj.Impuestos);
                sp.AgregarParametro("cbc_retenido", obj.Retenido);
                sp.AgregarParametro("cbc_descuentos", obj.Descuento);
                sp.AgregarParametro("cbc_final", monto);
                sp.AgregarParametro("cbc_saldo", monto);
                sp.AgregarParametro("cbc_exento", obj.Exento);

                int error = sp.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.ActualizarCabeceraTotales", obj.CabeceraId);
                throw ex;
            }
        }

        public static List<dto.ComprobanteCabecera> ListarComprobantesAprobarGrilla(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.ComprobanteCabecera> lst = new List<dto.ComprobanteCabecera>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_PanelAprobar_CastigoDevolucion_Grilla");
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
                        lst.Add(new dto.ComprobanteCabecera()
                        {
                            Pclid = Int32.Parse(ds.Tables[0].Rows[i]["Pclid"].ToString()),
                            Tpcid = Int32.Parse(ds.Tables[0].Rows[i]["tpcid"].ToString()),
                            TipoComprobante = ds.Tables[0].Rows[i]["TipoComprobante"].ToString(),
                            Folio = Int32.Parse(ds.Tables[0].Rows[i]["Folio"].ToString()),
                            Cliente = ds.Tables[0].Rows[i]["Cliente"].ToString(),
                            FecEmision = DateTime.Parse(ds.Tables[0].Rows[i]["FecEmision"].ToString()),
                            CbtEstado = ds.Tables[0].Rows[i]["CbtEstado"].ToString(),
                            Estado = ds.Tables[0].Rows[i]["Estado"].ToString(),
                            Saldo = decimal.Parse(ds.Tables[0].Rows[i]["Saldo"].ToString()),
                            Neto = decimal.Parse(ds.Tables[0].Rows[i]["Neto"].ToString()),
                            Row = Int32.Parse(ds.Tables[0].Rows[i]["row"].ToString())
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.ListarComprobantesAprobarGrilla", 0);
                return lst;
            }
        }

        public static List<dto.ComprobanteCabeceraDetalle> ListarComprobantesAprobarDetalleGrilla(int codemp, int tipoComprobante, int folio, int pclid,
                                                                                            string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.ComprobanteCabeceraDetalle> lst = new List<dto.ComprobanteCabeceraDetalle>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_PanelAprobar_CastigoDevolucion_Documentos_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("tipoComprobante", tipoComprobante);
                sp.AgregarParametro("folio", folio);
                sp.AgregarParametro("pclid", pclid);
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
                        lst.Add(new dto.ComprobanteCabeceraDetalle()
                        {
                            RutDeudor = ds.Tables[0].Rows[i]["RutDeudor"].ToString(),
                            Deudor = ds.Tables[0].Rows[i]["Deudor"].ToString(),
                            Tipo = ds.Tables[0].Rows[i]["Tipo"].ToString(),
                            Numero = ds.Tables[0].Rows[i]["Numero"].ToString(),
                            FechaAsignacion = DateTime.Parse(ds.Tables[0].Rows[i]["FechaAsignacion"].ToString()),
                            Monto = decimal.Parse(ds.Tables[0].Rows[i]["Monto"].ToString()),
                            Saldo = decimal.Parse(ds.Tables[0].Rows[i]["Saldo"].ToString()),
                            Asignado = decimal.Parse(ds.Tables[0].Rows[i]["Asignado"].ToString()),
                            UltimoEstado = ds.Tables[0].Rows[i]["UltimoEstado"].ToString(),
                            Estado = ds.Tables[0].Rows[i]["Estado"].ToString(),
                            FechaVencimiento = DateTime.Parse(ds.Tables[0].Rows[i]["FechaVencimiento"].ToString()),
                            CbtEstado = ds.Tables[0].Rows[i]["EstadoCpbt"].ToString(),
                            Asegurado = ds.Tables[0].Rows[i]["Asegurado"].ToString(),
                            Pclid = Int32.Parse(ds.Tables[0].Rows[i]["pclid"].ToString()),
                            Ctcid = Int32.Parse(ds.Tables[0].Rows[i]["ctcid"].ToString()),
                            Ccbid = Int32.Parse(ds.Tables[0].Rows[i]["ccbid"].ToString()),
                            RolNumero = ds.Tables[0].Rows[i]["RolNumero"].ToString(),
                            RolId = string.IsNullOrEmpty(ds.Tables[0].Rows[i]["RolId"].ToString()) ? default(int) : Int32.Parse(ds.Tables[0].Rows[i]["RolId"].ToString()),
                            Row = Int32.Parse(ds.Tables[0].Rows[i]["row"].ToString())

                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.ListarComprobantesAprobarDetalleGrilla", 0);
                return lst;
            }
        }
        public static int AprobarCastigoDevolucionComprobante(int codemp, int pclid, int ctcid, int ccbid, decimal monto, string estcpbt, int nuevoEstado, string comentario, int user, string ipRed, string ipMaquina)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Aprobar_CastigoDevolucion");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("ccbid", ccbid);
                sp.AgregarParametro("monto", monto);
                sp.AgregarParametro("estcpbt", estcpbt);
                sp.AgregarParametro("nuevo_estcpbt", "F");
                sp.AgregarParametro("nuevoEstado", nuevoEstado);
                sp.AgregarParametro("comentario", comentario);
                sp.AgregarParametro("user", user);
                sp.AgregarParametro("ip_red", ipRed);
                sp.AgregarParametro("ip_maquina", ipMaquina);
                int error = sp.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.AprobarCastigoDevolucionComprobante", user);
                throw ex;
            }
        }

        public static int AprobarCastigoDevolucionComprobanteRol(int codemp, int rolId, int nuevoEstado, string comentario, int user, string ipRed, string ipMaquina)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Aprobar_CastigoDevolucion_Rol");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("rolid ", rolId);
                sp.AgregarParametro("nuevoEstado", nuevoEstado);
                sp.AgregarParametro("comentario", comentario);
                sp.AgregarParametro("user", user);
                sp.AgregarParametro("ip_red", ipRed);
                sp.AgregarParametro("ip_maquina", ipMaquina);
                int error = sp.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.AprobarCastigoDevolucionComprobanteRol", user);
                throw ex;
            }
        }
        public static int AprobarRechazarCastigoDevolucionComprobante(int codemp, int folio, int ComprobanteId, int pclid, string estadoUpdate)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Aprobar_CastigoDevolucion_Cabecera");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("folio ", folio);
                sp.AgregarParametro("tipoComprobanteid", ComprobanteId);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("estado", estadoUpdate);

                int error = sp.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.AprobarRechazarCastigoDevolucionComprobante", 1);
                throw ex;
            }
        }
        public static int InsertCastigoDevolucionMotivoRechazo(int codemp, int folio, int ComprobanteId, int pclid, string motivo, int user)
        {
            int id = -1;

            try
            {

                Funciones func = new Funciones();
                StoredProcedure sp = new StoredProcedure("_Insert_Motivo_Rechazo_Comprobante");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("tipoComprobante ", ComprobanteId);
                sp.AgregarParametro("folio", folio);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("motivo", motivo);
                sp.AgregarParametro("user", user);

                id = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.Comprobante.InsertCastigoDevolucionMotivoRechazo", 0);
                return id;
            }
            return id;
        }
        public static List<dto.Comprobante> ListarPanelAvenimientoNuevosDocumentos(int codemp, int pclid, int ctcid, int ccbid, string numDocumento,
                                                                                    string fechaCuota, int numCuotas, string montoCuota)
        {
            List<Dimol.Carteras.dto.Comprobante> lst = new List<Dimol.Carteras.dto.Comprobante>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Panel_Avenimiento_Nuevos_Documentos");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("ccbid", ccbid);
                sp.AgregarParametro("numeroDocumento", numDocumento);
                sp.AgregarParametro("fechaCuota", string.IsNullOrEmpty(fechaCuota) ? DBNull.Value : (object)DateTime.Parse(fechaCuota));
                sp.AgregarParametro("numCuotas", numCuotas);
                sp.AgregarParametro("montoCuota", string.IsNullOrEmpty(montoCuota) ? DBNull.Value : (object)Decimal.Parse(montoCuota));
                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.Comprobante()
                        {
                            Pclid = pclid,
                            Ctcid = ctcid,
                            CalculoHonorarios = "S",
                            TipoDocumento = ds.Tables[0].Rows[i]["Tpcid"].ToString(),
                            TipoCartera = Int32.Parse(ds.Tables[0].Rows[i]["TipCart"].ToString()),
                            NumeroCpbt = ds.Tables[0].Rows[i]["Numero"].ToString(),
                            FechaDocumento = DateTime.Parse(ds.Tables[0].Rows[i]["FechaDocumento"].ToString()),
                            FechaVencimiento = DateTime.Parse(ds.Tables[0].Rows[i]["FechaDocumento"].ToString()),
                            EstadoCartera = ds.Tables[0].Rows[i]["Estid"].ToString(),
                            EstadoCpbt = ds.Tables[0].Rows[i]["Estcpbt"].ToString(),
                            CodigoMoneda = Int32.Parse(ds.Tables[0].Rows[i]["CodMon"].ToString()),
                            TipoCambio = decimal.Parse(ds.Tables[0].Rows[i]["TipCambio"].ToString()),
                            MontoAsignado = decimal.Parse(ds.Tables[0].Rows[i]["Monto"].ToString()),
                            Monto = decimal.Parse(ds.Tables[0].Rows[i]["Monto"].ToString()),
                            Saldo = decimal.Parse(ds.Tables[0].Rows[i]["Saldo"].ToString()),
                            GastoJudicial = decimal.Parse(ds.Tables[0].Rows[i]["GastJud"].ToString()),
                            GastoOtros = decimal.Parse(ds.Tables[0].Rows[i]["GastOtro"].ToString()),
                            //NombreBanco = ds.Tables[0].Rows[i]["BcoId"].ToString(),
                            //RutGirador = ds.Tables[0].Rows[i]["RutGir"].ToString(),
                            //NombreGirador = ds.Tables[0].Rows[i]["NomGir"].ToString(),
                            MotivoCobranza = ds.Tables[0].Rows[i]["MtcId"].ToString(),
                            CodigoCarga = ds.Tables[0].Rows[i]["CodId"].ToString(),
                            //NumeroEspecial = ds.Tables[0].Rows[i]["NumEsp"].ToString(),
                            //NumeroAgrupa = ds.Tables[0].Rows[i]["NumAgrupa"].ToString(),
                            Contrato = Int32.Parse(ds.Tables[0].Rows[i]["Cctid"].ToString()),
                            Sbcid = string.IsNullOrEmpty(ds.Tables[0].Rows[i]["SBCID"].ToString()) ? 0 : Int32.Parse(ds.Tables[0].Rows[i]["SBCID"].ToString()),
                            Originales = ds.Tables[0].Rows[i]["DocOri"].ToString(),
                            Antecedentes = ds.Tables[0].Rows[i]["DocAnt"].ToString(),
                            TerceroId = Int32.Parse(ds.Tables[0].Rows[i]["TERCEROID"].ToString()),
                            IdCuenta = ds.Tables[0].Rows[i]["IdCuenta"].ToString(),
                            DescripcionCuenta = ds.Tables[0].Rows[i]["DescCuenta"].ToString()
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.PanelAvenimiento.ListarPanelAvenimientoNuevosDocumentos", 0);
                return lst;
            }
        }
        #region "Carga Cocha"

        public static int ExisteCpbtNumeroTipo(int codemp, int pclid, int ctcid, int tpcid, string numero)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Find_Cartera_Clientes_Cpbt_Doc_Numero");
                sp.AgregarParametro("ccb_codemp", codemp);
                sp.AgregarParametro("ccb_pclid", pclid);
                sp.AgregarParametro("ccb_ctcid", ctcid);
                sp.AgregarParametro("ccb_tpcid", tpcid);
                sp.AgregarParametro("ccb_numero", numero);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        return Int32.Parse(ds.Tables[0].Rows[0][0].ToString());
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.ExisteCpbtNumeroTipo", ctcid);
                return -1;
            }
        }

        #endregion

        #region "Carga Previsional"
        public static int GrabarDocumentoExtension(dto.Comprobante obj, int codemp)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Insertar_Cartera_Clientes_Cpbt_Doc_Extension");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", obj.Pclid);
                sp.AgregarParametro("ctcid", obj.Ctcid);
                sp.AgregarParametro("ccbid", obj.Ccbid);
                sp.AgregarParametro("RUT_REPRESENTANTE_1", obj.RutRepresentante1);
                sp.AgregarParametro("NOM_REPRESENTANTE_1", obj.NombreRepresentante1);
                sp.AgregarParametro("RUT_REPRESENTANTE_2", obj.RutRepresentante2);
                sp.AgregarParametro("NOM_REPRESENTANTE_2", obj.NombreRepresentante2);
                sp.AgregarParametro("RUT_REPRESENTANTE_3", obj.RutRepresentante3);
                sp.AgregarParametro("NOM_REPRESENTANTE_3", obj.NombreRepresentante3);
                sp.AgregarParametro("NUM_RESOLUCION", obj.NumeroResolucion);
                sp.AgregarParametro("FEC_RESOLUCION", obj.FechaResolucion);
                int error = sp.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        #endregion
    }
}
