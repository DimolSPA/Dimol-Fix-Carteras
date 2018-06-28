using Dimol.dao;
using Dimol.dto;
using System;
using System.Collections.Generic;
using System.Data;

namespace Dimol.Judicial.Mantenedores.dao
{
    public class TraspasoJudicial
    {
        public static List<dto.TraspasoJudicialCandidato> ListarTraspasosGrilla(int codemp, int idioma, int perfil, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.TraspasoJudicialCandidato> lst = new List<dto.TraspasoJudicialCandidato>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Traspaso_Judicial_Documentos_Grilla");
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
                        lst.Add(new dto.TraspasoJudicialCandidato()
                        {
                            Pclid = Int32.Parse(ds.Tables[0].Rows[i]["Pclid"].ToString()),
                            Ctcid = Int32.Parse(ds.Tables[0].Rows[i]["Ctcid"].ToString()),
                            Cliente = ds.Tables[0].Rows[i]["Cliente"].ToString(),
                            RutDeudor = ds.Tables[0].Rows[i]["RutDeudor"].ToString(),
                            Deudor = ds.Tables[0].Rows[i]["Deudor"].ToString(),
                            Ccbid = Int32.Parse(ds.Tables[0].Rows[i]["Ccbid"].ToString()),
                            Tipo = ds.Tables[0].Rows[i]["TipoDocumento"].ToString(),
                            Numero = ds.Tables[0].Rows[i]["Numero"].ToString(),
                            Monto = decimal.Parse(ds.Tables[0].Rows[i]["Monto"].ToString()),
                            Saldo = decimal.Parse(ds.Tables[0].Rows[i]["Saldo"].ToString()),
                            Estado = ds.Tables[0].Rows[i]["Estado"].ToString(),
                            EstadoCpbt = ds.Tables[0].Rows[i]["EstadoCpbt"].ToString(),
                            FechaVencimiento = DateTime.Parse(ds.Tables[0].Rows[i]["FechaVencimiento"].ToString()),
                            FechaAsignacion = DateTime.Parse(ds.Tables[0].Rows[i]["FechaAsignacion"].ToString()),
                            Asegurado = ds.Tables[0].Rows[i]["Asegurado"].ToString()
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.ListarTraspasosGrilla", 0);
                return lst;
            }
        }
        public static List<dto.TraspasoJudicialCandidato> ListarTraspasosGrillaPrevisional(int codemp, int idioma, int perfil, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.TraspasoJudicialCandidato> lst = new List<dto.TraspasoJudicialCandidato>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Traspaso_Judicial_Documentos_Grilla_Previsional");
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
                        lst.Add(new dto.TraspasoJudicialCandidato()
                        {
                            Pclid = Int32.Parse(ds.Tables[0].Rows[i]["Pclid"].ToString()),
                            Ctcid = Int32.Parse(ds.Tables[0].Rows[i]["Ctcid"].ToString()),
                            Cliente = ds.Tables[0].Rows[i]["Cliente"].ToString(),
                            RutDeudor = ds.Tables[0].Rows[i]["RutDeudor"].ToString(),
                            Deudor = ds.Tables[0].Rows[i]["Deudor"].ToString(),
                            //Ccbid = Int32.Parse(ds.Tables[0].Rows[i]["Ccbid"].ToString()),
                            //Tipo = ds.Tables[0].Rows[i]["TipoDocumento"].ToString(),
                            //Numero = ds.Tables[0].Rows[i]["Numero"].ToString(),
                            Monto = decimal.Parse(ds.Tables[0].Rows[i]["Monto"].ToString()),
                            Saldo = decimal.Parse(ds.Tables[0].Rows[i]["Saldo"].ToString()),
                            Estado = ds.Tables[0].Rows[i]["Estado"].ToString(),
                            EstadoCpbt = ds.Tables[0].Rows[i]["EstadoCpbt"].ToString(),
                            //FechaVencimiento = DateTime.Parse(ds.Tables[0].Rows[i]["FechaVencimiento"].ToString()),
                            //FechaAsignacion = DateTime.Parse(ds.Tables[0].Rows[i]["FechaAsignacion"].ToString()),
                            //Asegurado = ds.Tables[0].Rows[i]["Asegurado"].ToString()
                            NumResolucion = ds.Tables[0].Rows[i]["NumResolucion"].ToString(),
                            FecResolucion = DateTime.Parse(ds.Tables[0].Rows[i]["FecResolucion"].ToString())
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.ListarTraspasosGrillaPrevisional", 0);
                return lst;
            }
        }

        public static int ListarTraspasosGrillaCount(int codemp, int idioma, int perfil, string where, string sidx, string sord, int inicio, int limite)
        {
            int count = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Traspaso_Judicial_Documentos_Grilla_Count");
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
                    count = Int32.Parse(ds.Tables[0].Rows[0]["count"].ToString());
                }

                return count;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.ListarTraspasosGrillaCount", 0);
                return count;
            }
        }

        public static int ListarTraspasosGrillaCountPrevisional(int codemp, int idioma, int perfil, string where, string sidx, string sord, int inicio, int limite)
        {
            int count = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Traspaso_Judicial_Documentos_Grilla_Count_Previsional");
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
                    count = Int32.Parse(ds.Tables[0].Rows[0]["count"].ToString());
                }

                return count;
            } catch (Exception ex) {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.ListarTraspasosGrillaCountPrevisional", 0);

                return count;
            }
        }

        public static List<dto.TraspasoJudicialPendiente> ListarTraspasosPendientesGrilla(int codemp, int codsuc, int idioma, DateTime fechaDesde, DateTime fechaHasta, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.TraspasoJudicialPendiente> lst = new List<dto.TraspasoJudicialPendiente>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Traspaso_Pendiente_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("codsuc", codsuc);
                sp.AgregarParametro("idioma", idioma);
                sp.AgregarParametro("fecha_desde", fechaDesde);
                sp.AgregarParametro("fecha_hasta", fechaHasta);
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
                        lst.Add(new dto.TraspasoJudicialPendiente()
                        {
                            Tpcid = Int32.Parse(ds.Tables[0].Rows[i]["Tpcid"].ToString()),
                            Numero = Int32.Parse(ds.Tables[0].Rows[i]["Numero"].ToString()),
                            Cliente = ds.Tables[0].Rows[i]["Cliente"].ToString(),
                            Tipo = ds.Tables[0].Rows[i]["Tipo"].ToString(),
                            NumeroProveedor = ds.Tables[0].Rows[i]["NumeroProveedor"].ToString(),
                            Fecha = DateTime.Parse(ds.Tables[0].Rows[i]["Fecha"].ToString())

                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.ListarTraspasosPendientesGrilla", 0);
                return lst;
            }
        }

        public static int ListarTraspasosPendientesGrillaCount(int codemp, int codsuc, int idioma, DateTime fechaDesde, DateTime fechaHasta, string where, string sidx, string sord, int inicio, int limite)
        {
            int count = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Traspaso_Pendiente_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("codsuc", codsuc);
                sp.AgregarParametro("idioma", idioma);
                sp.AgregarParametro("fecha_desde", fechaDesde);
                sp.AgregarParametro("fecha_hasta", fechaHasta);
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
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.ListarTraspasosPendientesGrillaCount", 0);
                return count;
            }
        }

        public static int NuevoNumeroComprobante(int codemp, int codsuc, int tpcid)
        {
            int count = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Numero_Cabecera_Comprobante");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("tpcid", tpcid);
                sp.AgregarParametro("sucid", codsuc);
                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    count = Int32.Parse(ds.Tables[0].Rows[0][0].ToString());
                    //count[1] = Int32.Parse(ds.Tables[0].Rows[0][1].ToString());

                }

                return count;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.NuevoNumeroComprobante", 0);
                return count;
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

        public static int ActualizarCarteraClientesEstadosTraspasoJudicial(int codemp, int pclid, int ctcid, int ccbid, int estid, int usuario)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Update_Cartera_Clientes_Cpbt_Doc_Estados_TJ");
                sp.AgregarParametro("ccb_codemp", codemp);
                sp.AgregarParametro("ccb_pclid", pclid);
                sp.AgregarParametro("ccb_ctcid", ctcid);
                sp.AgregarParametro("ccb_ccbid", ccbid);
                sp.AgregarParametro("ccb_estid", estid);
                sp.AgregarParametro("ccb_estcpbt", "J");

                int error = sp.EjecutarProcedimientoTrans();

                return error;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.ActualizarCarteraClientesEstadosTraspasoJudicial", usuario);
                throw ex;
            }
        }

        public static List<dto.DocumentoTraspasar> ListarDocumentosTraspasar(int codemp, int idioma, int pclid, int ctcid)
        {
            List<dto.DocumentoTraspasar> lst = new List<dto.DocumentoTraspasar>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Cpbt_A_Traspasar");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idioma", idioma);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.DocumentoTraspasar()
                        {
                            Ccbid = Int32.Parse(ds.Tables[0].Rows[i]["Ccbid"].ToString()),
                            Monto = decimal.Parse(ds.Tables[0].Rows[i]["Monto"].ToString()),
                            Saldo = decimal.Parse(ds.Tables[0].Rows[i]["Saldo"].ToString())
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.ListarDocumentosTraspasar", 0);
                return lst;
            }
        }

        public static List<dto.TraspasoJudicialHecho> ListarTraspasosHechosGrilla(int codemp,DateTime fechaDesde, DateTime fechaHasta, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.TraspasoJudicialHecho> lst = new List<dto.TraspasoJudicialHecho>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Traspaso_Hecho_Documentos_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("fecha_desde", fechaDesde);
                sp.AgregarParametro("fecha_hasta", fechaHasta);
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
                        lst.Add(new dto.TraspasoJudicialHecho()
                        {
                            Pclid = Int32.Parse(ds.Tables[0].Rows[i]["Pclid"].ToString()),
                            Ctcid = Int32.Parse(ds.Tables[0].Rows[i]["Ctcid"].ToString()),
                            Cliente = ds.Tables[0].Rows[i]["Cliente"].ToString(),
                            RutDeudor = ds.Tables[0].Rows[i]["RutDeudor"].ToString(),
                            Deudor = ds.Tables[0].Rows[i]["Deudor"].ToString(),
                            Fecha = ds.Tables[0].Rows[i]["Fecha"].ToString(),
                            Ccbid = Int32.Parse(ds.Tables[0].Rows[i]["Ccbid"].ToString()),
                            Tipo = ds.Tables[0].Rows[i]["TipoDocumento"].ToString(),
                            Numero = ds.Tables[0].Rows[i]["Numero"].ToString(),
                            Monto = decimal.Parse(ds.Tables[0].Rows[i]["Monto"].ToString()),
                            Saldo = decimal.Parse(ds.Tables[0].Rows[i]["Saldo"].ToString()),
                            Estado = ds.Tables[0].Rows[i]["Estado"].ToString(),
                            EstadoCpbt = ds.Tables[0].Rows[i]["EstadoCpbt"].ToString(),
                            FechaVencimiento = DateTime.Parse(ds.Tables[0].Rows[i]["FechaVencimiento"].ToString()),
                            FechaAsignacion = DateTime.Parse(ds.Tables[0].Rows[i]["FechaAsignacion"].ToString())
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.ListarTraspasosHechosGrilla", 0);
                return lst;
            }
        }
        public static List<dto.TraspasoJudicialHecho> ListarTraspasosHechosGrillaPrevisional(int codemp, DateTime fechaDesde, DateTime fechaHasta, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.TraspasoJudicialHecho> lst = new List<dto.TraspasoJudicialHecho>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Traspaso_Hecho_Documentos_Grilla_Previsional");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("fecha_desde", fechaDesde);
                sp.AgregarParametro("fecha_hasta", fechaHasta);
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
                        lst.Add(new dto.TraspasoJudicialHecho()
                        {
                            Pclid = Int32.Parse(ds.Tables[0].Rows[i]["Pclid"].ToString()),
                            Ctcid = Int32.Parse(ds.Tables[0].Rows[i]["Ctcid"].ToString()),
                            Cliente = ds.Tables[0].Rows[i]["Cliente"].ToString(),
                            RutDeudor = ds.Tables[0].Rows[i]["RutDeudor"].ToString(),
                            Deudor = ds.Tables[0].Rows[i]["Deudor"].ToString(),
                            NumResolucion = ds.Tables[0].Rows[i]["NumResolucion"].ToString(),
                            FecResolucion = DateTime.Parse(ds.Tables[0].Rows[i]["FecResolucion"].ToString()),
                            //Fecha = ds.Tables[0].Rows[i]["Fecha"].ToString(),
                            //Ccbid = Int32.Parse(ds.Tables[0].Rows[i]["Ccbid"].ToString()),
                            //Tipo = ds.Tables[0].Rows[i]["TipoDocumento"].ToString(),
                            //Numero = ds.Tables[0].Rows[i]["Numero"].ToString(),
                            Monto = decimal.Parse(ds.Tables[0].Rows[i]["Monto"].ToString()),
                            Saldo = decimal.Parse(ds.Tables[0].Rows[i]["Saldo"].ToString()),
                            Estado = ds.Tables[0].Rows[i]["Estado"].ToString(),
                            EstadoCpbt = ds.Tables[0].Rows[i]["EstadoCpbt"].ToString(),
                            //FechaVencimiento = DateTime.Parse(ds.Tables[0].Rows[i]["FechaVencimiento"].ToString()),
                            //FechaAsignacion = DateTime.Parse(ds.Tables[0].Rows[i]["FechaAsignacion"].ToString())
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.ListarTraspasosHechosGrillaPrevisional", 0);
                return lst;
            }
        }

        public static List<dto.TraspasoJudicialHecho> ListarTraspasosHechosDeudorGrilla(int codemp, int ctcid, string where, string sidx, string sord)
        {
            List<dto.TraspasoJudicialHecho> lst = new List<dto.TraspasoJudicialHecho>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Traspaso_Hecho_Documentos_Deudor_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
               
                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.TraspasoJudicialHecho()
                        {
                            Pclid = Int32.Parse(ds.Tables[0].Rows[i]["Pclid"].ToString()),
                            Ctcid = Int32.Parse(ds.Tables[0].Rows[i]["Ctcid"].ToString()),
                            Cliente = ds.Tables[0].Rows[i]["Cliente"].ToString(),
                            RutDeudor = ds.Tables[0].Rows[i]["RutDeudor"].ToString(),
                            Deudor = ds.Tables[0].Rows[i]["Deudor"].ToString(),
                            Fecha = ds.Tables[0].Rows[i]["Fecha"].ToString(),
                            Ccbid = Int32.Parse(ds.Tables[0].Rows[i]["Ccbid"].ToString()),
                            Tipo = ds.Tables[0].Rows[i]["TipoDocumento"].ToString(),
                            Numero = ds.Tables[0].Rows[i]["Numero"].ToString(),
                            Monto = decimal.Parse(ds.Tables[0].Rows[i]["Monto"].ToString()),
                            Saldo = decimal.Parse(ds.Tables[0].Rows[i]["Saldo"].ToString()),
                            Estado = ds.Tables[0].Rows[i]["Estado"].ToString(),
                            EstadoCpbt = ds.Tables[0].Rows[i]["EstadoCpbt"].ToString(),
                            FechaVencimiento = DateTime.Parse(ds.Tables[0].Rows[i]["FechaVencimiento"].ToString()),
                            FechaAsignacion = DateTime.Parse(ds.Tables[0].Rows[i]["FechaAsignacion"].ToString()),
                            Row = Int32.Parse(ds.Tables[0].Rows[i]["row"].ToString())
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.ListarTraspasosHechosGrilla", 0);
                return lst;
            }
        }

        public static int ListarTraspasosHechosGrillaCount(int codemp, DateTime fechaDesde, DateTime fechaHasta, string where, string sidx, string sord, int inicio, int limite)
        {
            int count = 0;

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Traspaso_Hecho_Documentos_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("fecha_desde", fechaDesde);
                sp.AgregarParametro("fecha_hasta", fechaHasta);
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
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.ListarTraspasosHechosGrillaCount", 0);
                return count;
            }
        }
        public static int ListarTraspasosHechosGrillaCountPrevisional(int codemp, DateTime fechaDesde, DateTime fechaHasta, string where, string sidx, string sord, int inicio, int limite)
        {
            int count = 0;

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Traspaso_Hecho_Documentos_Grilla_Count_Previsional");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("fecha_desde", fechaDesde);
                sp.AgregarParametro("fecha_hasta", fechaHasta);
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
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.ListarTraspasosHechosGrillaCountPrevisional", 0);
                return count;
            }
        }

        public static List<dto.DocumentoReversar> ListarDocumentosReversaGrilla(int codemp, int pclid, int ctcid, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.DocumentoReversar> lst = new List<dto.DocumentoReversar>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Documentos_Reversa_Grilla");
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
                        lst.Add(new dto.DocumentoReversar()
                        {
                            Pclid = Int32.Parse(ds.Tables[0].Rows[i]["Pclid"].ToString()),
                            Ctcid = Int32.Parse(ds.Tables[0].Rows[i]["Ctcid"].ToString()),
                            Ccbid = Int32.Parse(ds.Tables[0].Rows[i]["Ccbid"].ToString()),
                            Tipo = ds.Tables[0].Rows[i]["Tipo"].ToString(),
                            Numero = ds.Tables[0].Rows[i]["Numero"].ToString(),
                            Monto = decimal.Parse( ds.Tables[0].Rows[i]["Monto"].ToString()),
                            Saldo =decimal.Parse(  ds.Tables[0].Rows[i]["Saldo"].ToString()),
                            UltimoEstado = ds.Tables[0].Rows[i]["UltimoEstado"].ToString(),
                            Estado = ds.Tables[0].Rows[i]["Estado"].ToString(),
                            FechaVencimiento = DateTime.Parse(ds.Tables[0].Rows[i]["FechaVencimiento"].ToString())
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.ListarDocumentosReversaGrilla", 0);
                return lst;
            }
        }

        public static int ListarDocumentosReversaGrillaCount(int codemp, int pclid, int ctcid, string where, string sidx, string sord, int inicio, int limite)
        {
            int count = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Documentos_Reversa_Grilla_Count");
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
                    count = Int32.Parse(ds.Tables[0].Rows[0]["count"].ToString());
                }

                return count;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.ListarDocumentosReversaGrillaCount", 0);
                return count;
            }
        }

        public static List<Combobox> ListarEstadosReversa(int codemp, int idioma, string first)
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
                StoredProcedure sp = new StoredProcedure("_Listar_Estados_Cartera_Reversa_Traspaso_Judicial");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idioma", idioma);
                ds = sp.EjecutarProcedimiento();

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

        public static int ReversaEstadoTraspasoJudicial(int codemp, int pclid, int ctcid, int ccbid, int estid, int usuario, decimal saldo)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Update_Cartera_Clientes_Reversa_TJ");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("ccbid", ccbid);
                sp.AgregarParametro("estid", estid);
                sp.AgregarParametro("estcpbt", "V");
                sp.AgregarParametro("saldo", saldo);
                int error = sp.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.ReversaEstadoTraspasoJudicial", usuario);
                throw ex;
            }

        }

        public static decimal TraeUltimoSaldoReversa(int codemp, int pclid, int ctcid, int ccbid)
        {
            decimal saldo = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Ultimo_Saldo_Reversa_Traspaso");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("ccbid", ccbid);
                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    saldo = decimal.Parse(ds.Tables[0].Rows[0]["saldo"].ToString());
                }

                return saldo;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.TraeUltimoSaldoReversa", 0);
                return saldo;
            }
        }

        public static int ActualizarCarteraClientesComentario(int codemp, int pclid, int ctcid, int ccbid, string comentario, int usuario)
        {
            try
            {
               
                StoredProcedure sp = new StoredProcedure("_Update_Cartera_Clientes_Cpbt_Doc_Comentario");
                sp.AgregarParametro("ccb_codemp", codemp);
                sp.AgregarParametro("ccb_pclid", pclid);
                sp.AgregarParametro("ccb_ctcid", ctcid);
                sp.AgregarParametro("ccb_ccbid", ccbid);
                sp.AgregarParametro("ccb_comentario", comentario);
  
                int error = sp.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.ActualizarCarteraClientesComentario", usuario);
                throw ex;
            }

        }
    }
}
