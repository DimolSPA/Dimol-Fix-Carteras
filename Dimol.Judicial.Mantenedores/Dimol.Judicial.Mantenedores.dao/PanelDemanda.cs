using Dimol.dao;
using Dimol.dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;

namespace Dimol.Judicial.Mantenedores.dao
{
    public class PanelDemanda
    {
        public static List<dto.PanelDemanda> ListarPanelDemandaInsertar(int codemp, int idioma, int pclid, int ctcid)
        {
            List<dto.PanelDemanda> lst = new List<dto.PanelDemanda>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Panel_Demanda_GrupoByLlave");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idioma", idioma);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.PanelDemanda()
                        {
                            Pclid = Int32.Parse(ds.Tables[0].Rows[i]["CCB_PCLID"].ToString()),
                            Ctcid = Int32.Parse(ds.Tables[0].Rows[i]["CCB_CTCID"].ToString()),
                            Sbcid = string.IsNullOrEmpty(ds.Tables[0].Rows[i]["CCB_SBCID"].ToString()) ? default(int) : Int32.Parse(ds.Tables[0].Rows[i]["CCB_SBCID"].ToString()),
                            Tpcid = Int32.Parse(ds.Tables[0].Rows[i]["CCB_TPCID"].ToString())
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.PanelDemanda.ListarPanelDemandaInsertar", 0);
                return lst;
            }
        }

        public static List<dto.PanelDemandaDocumentos> ListarPanelDemandaDocumentosInsertar(int codemp, int idioma,
                                                                                            int pclid, int ctcid,
                                                                                            int? sbcid, int tpcid)
        {
            List<dto.PanelDemandaDocumentos> lst = new List<dto.PanelDemandaDocumentos>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Panel_Demanda_GrupoByDocumentos");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idioma", idioma);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("sbcid", sbcid);
                sp.AgregarParametro("tpcid", tpcid);
                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.PanelDemandaDocumentos()
                        {
                            Pclid = Int32.Parse(ds.Tables[0].Rows[i]["CCB_PCLID"].ToString()),
                            Ctcid = Int32.Parse(ds.Tables[0].Rows[i]["CCB_CTCID"].ToString()),
                            Ccbid = Int32.Parse(ds.Tables[0].Rows[i]["CCB_CCBID"].ToString()),
                            Sbcid = string.IsNullOrEmpty(ds.Tables[0].Rows[i]["CCB_SBCID"].ToString()) ? default(int) : Int32.Parse(ds.Tables[0].Rows[i]["CCB_SBCID"].ToString()),
                            Tpcid = Int32.Parse(ds.Tables[0].Rows[i]["CCB_TPCID"].ToString())
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.PanelDemanda.ListarPanelDemandaDocumentosInsertar", 0);
                return lst;
            }
        }

        public static int InsertarPanelDemanda(int codemp, int pclid, int ctcid, int? sbcid, int? tpcid, int userId)
        {
            int result = -1;

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Insertar_Panel_Demanda");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("sbcid", (sbcid == 0) ? DBNull.Value : (object)sbcid);
                sp.AgregarParametro("tpcid", DBNull.Value);
                sp.AgregarParametro("user", userId);

                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    result = Int32.Parse(ds.Tables[0].Rows[0]["panelId"].ToString());
                }

            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.PanelDemanda.InsertarPanelDemanda", userId);
                return -1;
            }
            return result;
        }

        public static int InsertarPanelDemandaPrevisional(int codemp, int pclid, int ctcid, int? sbcid, int? tpcid, int userId)
        {
            int result = -1;

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Insertar_Panel_Demanda_Previsional");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("sbcid", (sbcid == 0) ? DBNull.Value : (object)sbcid);
                sp.AgregarParametro("tpcid", DBNull.Value);
                sp.AgregarParametro("user", userId);

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    result = Int32.Parse(ds.Tables[0].Rows[0]["panelId"].ToString());
                }
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.PanelDemanda.InsertarPanelDemandaPrevisional", userId);
                return -1;
            }

            return result;
        }

        public static int InsertarPanelDemandaDocumentos(int panelId, int codemp, int pclid, int ctcid, int ccbid, int userId)
        {
            int result = -1;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Insertar_Panel_Demanda_Documentos");
                sp.AgregarParametro("panelId", panelId);
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("ccbid", ccbid);
                sp.AgregarParametro("user", userId);

                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    result = Int32.Parse(ds.Tables[0].Rows[0]["panelId"].ToString());
                }

            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.PanelDemanda.InsertarPanelDemandaDocumentos", userId);
                return -1;
            }
            return result;
        }
        public static int InsertarPanelDemandaDocumentosPrevisional(int panelId, int codemp, int pclid, int ctcid, int ccbid, int userId)
        {
            int result = -1;

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Insertar_Panel_Demanda_Previsional_Documentos");
                sp.AgregarParametro("panelId", panelId);
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("ccbid", ccbid);
                sp.AgregarParametro("user", userId);

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    result = Int32.Parse(ds.Tables[0].Rows[0]["panelId"].ToString());
                }
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.PanelDemanda.InsertarPanelDemandaDocumentosPrevisional", userId);
                return -1;
            }

            return result;
        }

        public static int ListarPanelDemandasCount(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            int count = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Panel_Demandas_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
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
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.PanelDemanda.ListarVisitaTerrenoGenerarCount", 0);
                return count;
            }
        }
        public static int ListarPanelDemandasPrevisionalCount(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            int count = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Panel_Demandas_Previsional_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
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
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.PanelDemanda.ListarPanelDemandasPrevisionalCount", 0);
                return count;
            }
        }
        public static int ListarPanelDemandasMasivasCount(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            int count = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Panel_Demandas_Masivas_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
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
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.PanelDemanda.ListarPanelDemandasMasivasCount", 0);
                return count;
            }
        }
        public static List<dto.PanelDemandaGet> ListarPanelDemandas(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.PanelDemandaGet> lst = new List<dto.PanelDemandaGet>();

            try
            {
                DateTime fechaAsignacion = new DateTime();
                DateTime fechaAprobacionTraspaso = new DateTime();
                DateTime fechaIngresaJudicial = new DateTime();
                DateTime fechaEnvioConfeccion = new DateTime();
                DateTime fechaEntrega = new DateTime();
                DateTime fechaIngresoTribunal = new DateTime();
                DataSet ds = new DataSet();

                StoredProcedure sp = new StoredProcedure("_Listar_Panel_Demandas_Grilla");
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
                        fechaAsignacion = new DateTime();
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FechaAsignacion"].ToString(), out fechaAsignacion);
                        fechaAprobacionTraspaso = new DateTime();
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FechaAprobacionTraspaso"].ToString(), out fechaAprobacionTraspaso);
                        fechaIngresaJudicial = new DateTime();
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FechaIngresaJudicial"].ToString(), out fechaIngresaJudicial);
                        fechaEnvioConfeccion = new DateTime();
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FechaEnvioConfeccion"].ToString(), out fechaEnvioConfeccion);
                        fechaEntrega = new DateTime();
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FechaEntrega"].ToString(), out fechaEntrega);
                        fechaIngresoTribunal = new DateTime();
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FechaIngresoTribunal"].ToString(), out fechaIngresoTribunal);

                        lst.Add(
                            new dto.PanelDemandaGet()
                            {
                                PanelId = int.Parse(ds.Tables[0].Rows[i]["PanelId"].ToString()),
                                Procesada = ds.Tables[0].Rows[i]["PROCESADA"].ToString(),
                                FechaAsignacion = fechaAsignacion,
                                FechaAprobacionTraspaso = fechaAprobacionTraspaso,
                                FechaIngresaJudicial = fechaIngresaJudicial,
                                Cliente = ds.Tables[0].Rows[i]["Cliente"].ToString(),
                                RutDeudor = ds.Tables[0].Rows[i]["RutDeudor"].ToString(),
                                Deudor = ds.Tables[0].Rows[i]["Deudor"].ToString(),
                                Asegurado = ds.Tables[0].Rows[i]["Asegurado"].ToString(),
                                TipoDocumento = ds.Tables[0].Rows[i]["TipoDocumento"].ToString(),
                                Comuna = ds.Tables[0].Rows[i]["Comuna"].ToString(),
                                Region = ds.Tables[0].Rows[i]["Region"].ToString(),
                                UsridEncargado = string.IsNullOrEmpty(ds.Tables[0].Rows[i]["UsridEncargado"].ToString()) ? 0 : Int32.Parse(ds.Tables[0].Rows[i]["UsridEncargado"].ToString()),
                                EncargadoCofeccion = ds.Tables[0].Rows[i]["EncargadoCofeccion"].ToString(),
                                FechaEnvioConfeccion = fechaEnvioConfeccion == new DateTime() ? (DateTime?)null : fechaEnvioConfeccion,
                                FechaEntrega = fechaEntrega == new DateTime() ? (DateTime?)null : fechaEntrega,
                                FechaIngresoTribunal = fechaIngresoTribunal == new DateTime() ? (DateTime?)null : fechaIngresoTribunal,
                                Comentarios = ds.Tables[0].Rows[i]["COMENTARIOS"].ToString(),
                                Pclid = int.Parse(ds.Tables[0].Rows[i]["PCLID"].ToString()),
                                Ctcid = int.Parse(ds.Tables[0].Rows[i]["CTCID"].ToString()),
                                RutCliente = ds.Tables[0].Rows[i]["PCL_RUT"].ToString(),
                                CountFechaEntrega = int.Parse(ds.Tables[0].Rows[i]["countFechaEntrega"].ToString()),
                                Correcciones = ds.Tables[0].Rows[i]["Correcciones"].ToString(),
                                CountCorrecciones = int.Parse(ds.Tables[0].Rows[i]["countCorrecciones"].ToString()),
                                Cursodemanda = ds.Tables[0].Rows[i]["CURSODEMANDA"].ToString(),
                                CountCursodemanda = int.Parse(ds.Tables[0].Rows[i]["CountCursoDemanda"].ToString()),
                                Responsable = ds.Tables[0].Rows[i]["Responsable"].ToString(),

                                RolNumero = ds.Tables[0].Rows[i]["ROL_NUMERO"].ToString(),
                                TribunalNombre = ds.Tables[0].Rows[i]["TRB_NOMBRE"].ToString()
                            });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.PanelDemanda.ListarPanelDemandas", 0);
                return lst;
            }
        }
        public static List<dto.PanelDemandaGet> ListarPanelDemandasPrevisionales(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.PanelDemandaGet> lst = new List<dto.PanelDemandaGet>();

            try {
                DateTime fechaAsignacion = new DateTime();
                DateTime fechaAprobacionTraspaso = new DateTime();
                DateTime fechaIngresaJudicial = new DateTime();
                DateTime fechaEnvioConfeccion = new DateTime();
                DateTime fechaEntrega = new DateTime();
                DateTime fechaIngresoTribunal = new DateTime();
                DataSet ds = new DataSet();

                StoredProcedure sp = new StoredProcedure("_Listar_Panel_Demandas_Previsionales_Grilla");
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
                        fechaAsignacion = new DateTime();
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FechaAsignacion"].ToString(), out fechaAsignacion);
                        fechaAprobacionTraspaso = new DateTime();
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FechaAprobacionTraspaso"].ToString(), out fechaAprobacionTraspaso);
                        fechaIngresaJudicial = new DateTime();
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FechaIngresaJudicial"].ToString(), out fechaIngresaJudicial);
                        fechaEnvioConfeccion = new DateTime();
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FechaEnvioConfeccion"].ToString(), out fechaEnvioConfeccion);
                        fechaEntrega = new DateTime();
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FechaEntrega"].ToString(), out fechaEntrega);
                        fechaIngresoTribunal = new DateTime();
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FechaIngresoTribunal"].ToString(), out fechaIngresoTribunal);

                        lst.Add(
                            new dto.PanelDemandaGet()
                            {
                                PanelId = int.Parse(ds.Tables[0].Rows[i]["PanelId"].ToString()),
                                Procesada = ds.Tables[0].Rows[i]["PROCESADA"].ToString(),
                                FechaAsignacion = fechaAsignacion,
                                FechaAprobacionTraspaso = fechaAprobacionTraspaso,
                                FechaIngresaJudicial = fechaIngresaJudicial,
                                Cliente = ds.Tables[0].Rows[i]["Cliente"].ToString(),
                                RutDeudor = ds.Tables[0].Rows[i]["RutDeudor"].ToString(),
                                Deudor = ds.Tables[0].Rows[i]["Deudor"].ToString(),
                                Asegurado = ds.Tables[0].Rows[i]["Asegurado"].ToString(),
                                TipoDocumento = ds.Tables[0].Rows[i]["TipoDocumento"].ToString(),
                                Comuna = ds.Tables[0].Rows[i]["Comuna"].ToString(),
                                Region = ds.Tables[0].Rows[i]["Region"].ToString(),
                                UsridEncargado = string.IsNullOrEmpty(ds.Tables[0].Rows[i]["UsridEncargado"].ToString()) ? 0 : Int32.Parse(ds.Tables[0].Rows[i]["UsridEncargado"].ToString()),
                                EncargadoCofeccion = ds.Tables[0].Rows[i]["EncargadoCofeccion"].ToString(),
                                FechaEnvioConfeccion = fechaEnvioConfeccion == new DateTime() ? (DateTime?)null : fechaEnvioConfeccion,
                                FechaEntrega = fechaEntrega == new DateTime() ? (DateTime?)null : fechaEntrega,
                                FechaIngresoTribunal = fechaIngresoTribunal == new DateTime() ? (DateTime?)null : fechaIngresoTribunal,
                                Comentarios = ds.Tables[0].Rows[i]["COMENTARIOS"].ToString(),
                                Pclid = int.Parse(ds.Tables[0].Rows[i]["PCLID"].ToString()),
                                Ctcid = int.Parse(ds.Tables[0].Rows[i]["CTCID"].ToString()),
                                RutCliente = ds.Tables[0].Rows[i]["PCL_RUT"].ToString(),
                                CountFechaEntrega = int.Parse(ds.Tables[0].Rows[i]["countFechaEntrega"].ToString()),
                                Correcciones = ds.Tables[0].Rows[i]["Correcciones"].ToString(),
                                CountCorrecciones = int.Parse(ds.Tables[0].Rows[i]["countCorrecciones"].ToString()),
                                Cursodemanda = ds.Tables[0].Rows[i]["CURSODEMANDA"].ToString(),
                                CountCursodemanda = int.Parse(ds.Tables[0].Rows[i]["CountCursoDemanda"].ToString()),
                                Responsable = ds.Tables[0].Rows[i]["Responsable"].ToString(),

                                RolNumero = ds.Tables[0].Rows[i]["ROL_NUMERO"].ToString(),
                                TribunalNombre = ds.Tables[0].Rows[i]["TRB_NOMBRE"].ToString()
                            });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.PanelDemanda.ListarPanelDemandas", 0);
                return lst;
            }
        }
        public static List<dto.PanelDemandaGet> ListarPanelDemandasMasivas(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.PanelDemandaGet> lst = new List<dto.PanelDemandaGet>();
            try
            {
                DateTime fechaAsignacion = new DateTime();
                DateTime fechaAprobacionTraspaso = new DateTime();
                DateTime fechaIngresaJudicial = new DateTime();
                DateTime fechaEnvioConfeccion = new DateTime();
                DateTime fechaIngresoTribunal = new DateTime();
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Panel_Demandas_Masivas_Grilla");
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
                        fechaAsignacion = new DateTime();
                        //DateTime.TryParseExact(ds.Tables[0].Rows[i]["FechaAsignacion"].ToString(), "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out fechaAsignacion);
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FechaAsignacion"].ToString(), out fechaAsignacion);
                        fechaAprobacionTraspaso = new DateTime();
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FechaAprobacionTraspaso"].ToString(), out fechaAprobacionTraspaso);
                        fechaIngresaJudicial = new DateTime();
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FechaIngresaJudicial"].ToString(), out fechaIngresaJudicial);
                        fechaEnvioConfeccion = new DateTime();
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FechaEnvioConfeccion"].ToString(), out fechaEnvioConfeccion);
                        var provDate = new DateTime();
                        var date = ds.Tables[0].Rows[i]["FechaEntrega"].ToString();
                        var parseResult = DateTime.TryParse(ds.Tables[0].Rows[i]["FechaEntrega"].ToString(), out provDate);
                        fechaIngresoTribunal = new DateTime();
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FechaIngresoTribunal"].ToString(), out fechaIngresoTribunal);

                        var d = new dto.PanelDemandaGet()
                            {
                                PanelId = Int32.Parse(ds.Tables[0].Rows[i]["PanelId"].ToString()),
                                Procesada = ds.Tables[0].Rows[i]["PROCESADA"].ToString(),
                                FechaAsignacion = fechaAsignacion,
                                FechaAprobacionTraspaso = fechaAprobacionTraspaso,
                                FechaIngresaJudicial = fechaIngresaJudicial,
                                Cliente = ds.Tables[0].Rows[i]["Cliente"].ToString(),
                                RutDeudor = ds.Tables[0].Rows[i]["RutDeudor"].ToString(),
                                Deudor = ds.Tables[0].Rows[i]["Deudor"].ToString(),
                                Asegurado = ds.Tables[0].Rows[i]["Asegurado"].ToString(),
                                TipoDocumento = ds.Tables[0].Rows[i]["TipoDocumento"].ToString(),
                                Comuna = ds.Tables[0].Rows[i]["Comuna"].ToString(),
                                Region = ds.Tables[0].Rows[i]["Region"].ToString(),
                                UsridEncargado = String.IsNullOrEmpty(ds.Tables[0].Rows[i]["UsridEncargado"].ToString()) ? 0 : Int32.Parse(ds.Tables[0].Rows[i]["UsridEncargado"].ToString()),
                                EncargadoCofeccion = ds.Tables[0].Rows[i]["EncargadoCofeccion"].ToString(),
                                FechaEnvioConfeccion = fechaEnvioConfeccion == new DateTime() ? (DateTime?)null : fechaEnvioConfeccion,
                                //FechaEntrega = fechaEntrega == null ? (DateTime?)null : fechaEntrega,
                                FechaIngresoTribunal = fechaIngresoTribunal == new DateTime() ? (DateTime?)null : fechaIngresoTribunal,
                                Comentarios = ds.Tables[0].Rows[i]["COMENTARIOS"].ToString(),
                                Pclid = Int32.Parse(ds.Tables[0].Rows[i]["PCLID"].ToString()),
                                Ctcid = Int32.Parse(ds.Tables[0].Rows[i]["CTCID"].ToString()),
                                RutCliente = ds.Tables[0].Rows[i]["PCL_RUT"].ToString(),
                                CountFechaEntrega = Int32.Parse(ds.Tables[0].Rows[i]["countFechaEntrega"].ToString()),
                                Correcciones = ds.Tables[0].Rows[i]["Correcciones"].ToString(),
                                CountCorrecciones = Int32.Parse(ds.Tables[0].Rows[i]["countCorrecciones"].ToString()),
                                Cursodemanda = ds.Tables[0].Rows[i]["CURSODEMANDA"].ToString(),
                                CountCursodemanda = Int32.Parse(ds.Tables[0].Rows[i]["CountCursoDemanda"].ToString()),
                                Responsable = ds.Tables[0].Rows[i]["Responsable"].ToString(),
                                TribunalNombre = ds.Tables[0].Rows[i]["TRB_NOMBRE"].ToString(),
                                RolNumero = ds.Tables[0].Rows[i]["ROL_NUMERO"].ToString()
                            };
                        if (String.IsNullOrEmpty(ds.Tables[0].Rows[i]["FechaEntrega"].ToString()))
                        {
                            d.FechaEntrega = null;
                        }
                        else
                        {
                            d.FechaEntrega = provDate;
                        }
                        lst.Add(d);
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.PanelDemanda.ListarPanelDemandasMasivas", 0);
                return lst;
            }
        }
        public static List<Autocomplete> BuscarNombreUsuario(string nombre, int codemp, int sucursal)
        {
            List<Autocomplete> lst = new List<Autocomplete>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Panel_Demanda_Usuarios");
                sp.AgregarParametro("texto", nombre);
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("sucursal", sucursal);
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

        public static int GrabarAvanceDemanda(int panelId, int userEncargado, DateTime fecEnvio, DateTime? fecEntrega,
                                            DateTime? fecIngreso, string comentarios, int user, bool ingresarFechaEntreg)
        {
            int result = -1;

            try
            {
                string format = "yyyy-MM-dd HH:mm:ss";

                StoredProcedure sp = new StoredProcedure("_Insertar_Panel_Demanda_Detalle");
                sp.AgregarParametro("panelId", panelId);
                sp.AgregarParametro("userEncargado", userEncargado);
                sp.AgregarParametro("fecEnvio", fecEnvio.ToString(format, CultureInfo.InvariantCulture));
                sp.AgregarParametro("fecEntrega", fecEntrega == null ? DBNull.Value : (object)fecEntrega.GetValueOrDefault().ToString(format, CultureInfo.InvariantCulture));
                sp.AgregarParametro("fecIngreso", fecIngreso == null ? DBNull.Value : (object)fecIngreso.GetValueOrDefault().ToString(format, CultureInfo.InvariantCulture));
                sp.AgregarParametro("comentarios", comentarios);
                sp.AgregarParametro("user", user);
                sp.AgregarParametro("ingresarFechaEntrega", ingresarFechaEntreg == true ? "S" : "N");
                result = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.PanelDemanda.GrabarAvanceDemanda", user);
                return -1;
            }

            return result;
        }

        public static int GrabarAvanceDemandaMasiva(int panelId, int userEncargado, DateTime fecEnvio, DateTime? fecEntrega,
                                            DateTime? fecIngreso, string comentarios, int user, bool ingresarFechaEntreg)
        {
            int result = -1;
            try
            {
                string format = "yyyy-MM-dd HH:mm:ss";

                
                StoredProcedure sp = new StoredProcedure("_Insertar_Panel_Demanda_Masiva_Detalle");
                sp.AgregarParametro("panelId", panelId);
                sp.AgregarParametro("userEncargado", userEncargado);
                sp.AgregarParametro("fecEnvio", fecEnvio.ToString(format, CultureInfo.InvariantCulture));
                sp.AgregarParametro("fecEntrega", fecEntrega == null ? DBNull.Value :  (object)fecEntrega.GetValueOrDefault().ToString(format, CultureInfo.InvariantCulture));
                sp.AgregarParametro("fecIngreso", fecIngreso == null ? DBNull.Value : (object)fecIngreso.GetValueOrDefault().ToString(format, CultureInfo.InvariantCulture));
                sp.AgregarParametro("comentarios", comentarios);
                sp.AgregarParametro("user", user);
                sp.AgregarParametro("ingresarFechaEntrega", ingresarFechaEntreg == true ? "S" : "N");
                result = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.PanelDemanda.GrabarAvanceDemandaMasiva", user);
                return -1;
            }
            return result;
        }

        public static int GrabarAvanceDemandaPrevisional(int panelId, int userEncargado, DateTime fecEnvio, DateTime? fecEntrega,
                                            DateTime? fecIngreso, string comentarios, int user, bool ingresarFechaEntreg)
        {
            int result = -1;
            try
            {
                string format = "yyyy-MM-dd HH:mm:ss";

                StoredProcedure sp = new StoredProcedure("_Insertar_Panel_Demanda_Previsional_Detalle");
                sp.AgregarParametro("panelId", panelId);
                sp.AgregarParametro("userEncargado", userEncargado);
                sp.AgregarParametro("fecEnvio", fecEnvio.ToString(format, CultureInfo.InvariantCulture));
                sp.AgregarParametro("fecEntrega", fecEntrega == null ? DBNull.Value : (object)fecEntrega.GetValueOrDefault().ToString(format, CultureInfo.InvariantCulture));
                sp.AgregarParametro("fecIngreso", fecIngreso == null ? DBNull.Value : (object)fecIngreso.GetValueOrDefault().ToString(format, CultureInfo.InvariantCulture));
                sp.AgregarParametro("comentarios", comentarios);
                sp.AgregarParametro("user", user);
                sp.AgregarParametro("ingresarFechaEntrega", ingresarFechaEntreg == true ? "S" : "N");
                result = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.PanelDemanda.GrabarAvanceDemandaPrevisional", user);
                return -1;
            }
            return result;
        }

        public static List<Autocomplete> BuscarTribunal(string nombre, int codemp)
        {
            List<Autocomplete> lst = new List<Autocomplete>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Tribunales_Prefijo");
                sp.AgregarParametro("texto", nombre);
                sp.AgregarParametro("codemp", codemp);

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Autocomplete()
                        {
                            label = ds.Tables[0].Rows[i]["trb_nombre"].ToString(),
                            value = ds.Tables[0].Rows[i]["trb_trbid"].ToString()
                        });
                    }
                }
            } catch (Exception ex) {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.PanelDemanda.BuscarTribunal", 0);
            }

            return lst;
        }
        public static List<dto.PanelDemandaDocumentoAsignar> ListarPanelDemandaDocumentoAsignar(int codemp, int panelId, int pclid, int ctcid)
        {
            List<dto.PanelDemandaDocumentoAsignar> lst = new List<dto.PanelDemandaDocumentoAsignar>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Panel_Demanda_Documentos_Asignar");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("panelId", panelId);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.PanelDemandaDocumentoAsignar()
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
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.ListarPanelDemandaDocumentoAsignar", 0);
                return lst;
            }
        }
        public static List<dto.PanelDemandaDocumentoAsignar> ListarPanelDemandaDocumentoAsignarPrevisional(int codemp, int panelId, int pclid, int ctcid)
        {
            List<dto.PanelDemandaDocumentoAsignar> lst = new List<dto.PanelDemandaDocumentoAsignar>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Panel_Demanda_Documentos_Asignar_Previsional");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("panelId", panelId);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.PanelDemandaDocumentoAsignar()
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
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.ListarPanelDemandaDocumentoAsignarPrevisional", 0);
                return lst;
            }
        }
        
        public static List<dto.PanelDemandaDocumentoAsignar> ListarPanelDemandaMasivaDocumentoAsignar(int codemp, int panelId, int pclid, int ctcid)
        {
            List<dto.PanelDemandaDocumentoAsignar> lst = new List<dto.PanelDemandaDocumentoAsignar>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Panel_Demanda_Masiva_Documentos_Asignar");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("panelId", panelId);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.PanelDemandaDocumentoAsignar()
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
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.ListarPanelDemandaDocumentoAsignar", 0);
                return lst;
            }
        }

        public static int InsertarAvenimientoDemandaPanelMasivo(int codemp, int rolId, int panelId)
        {
            int result = -1;
            try
            {

                StoredProcedure sp = new StoredProcedure("_Insertar_Rol_AveDem_Panel_Masivo");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("rolid", rolId);
                sp.AgregarParametro("panelid", panelId);

                result = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.PanelDemanda.InsertarAvenimientoDemandaPanelMasivo", 0);
                return -1;
            }
            return result;
        }

        public static int GrabarAvanceDemandaRol(int panelId, DateTime? fecIngreso, string rolAdjudicado, int rolId,
                                                bool flagEnviaFechaEntrega, DateTime? fechaEntrega, int user)
        {
            int result = -1;
            try
            {
                string format = "yyyy-MM-dd HH:mm:ss";

                StoredProcedure sp = new StoredProcedure("_Insertar_Panel_Demanda_Tribunal_Rol");
                sp.AgregarParametro("panelId", panelId);
                sp.AgregarParametro("fecIngresoTribunal", fecIngreso == null ? DBNull.Value : (object)fecIngreso.GetValueOrDefault().ToString(format, CultureInfo.InvariantCulture));
                sp.AgregarParametro("RolAdjudicado", rolAdjudicado);
                sp.AgregarParametro("RolId", rolId);
                sp.AgregarParametro("ingresarFechaEntrega", flagEnviaFechaEntrega == true ? "S" : "N");
                sp.AgregarParametro("fecEntrega", fechaEntrega == null ? DBNull.Value : (object)fechaEntrega.GetValueOrDefault().ToString(format, CultureInfo.InvariantCulture));
                sp.AgregarParametro("user", user);
                result = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.PanelDemanda.GrabarAvanceDemandaRol", 0);
                return -1;
            }
            return result;
        }

        public static int GrabarAvanceDemandaRolPrevisional(int panelId, DateTime? fecIngreso, string rolAdjudicado, int rolId,
                                                bool flagEnviaFechaEntrega, DateTime? fechaEntrega, int user)
        {
            int result = -1;
            try
            {
                string format = "yyyy-MM-dd HH:mm:ss";

                StoredProcedure sp = new StoredProcedure("_Insertar_Panel_Demanda_Tribunal_Rol_Previsional");
                sp.AgregarParametro("panelId", panelId);
                sp.AgregarParametro("fecIngresoTribunal", fecIngreso == null ? DBNull.Value : (object)fecIngreso.GetValueOrDefault().ToString(format, CultureInfo.InvariantCulture));
                sp.AgregarParametro("RolAdjudicado", rolAdjudicado);
                sp.AgregarParametro("RolId", rolId);
                sp.AgregarParametro("ingresarFechaEntrega", flagEnviaFechaEntrega == true ? "S" : "N");
                sp.AgregarParametro("fecEntrega", fechaEntrega == null ? DBNull.Value : (object)fechaEntrega.GetValueOrDefault().ToString(format, CultureInfo.InvariantCulture));
                sp.AgregarParametro("user", user);
                result = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.PanelDemanda.GrabarAvanceDemandaRolPrevisional", 0);
                return -1;
            }

            return result;
        }

        public static int GrabarAvanceDemandaMasivaRol(int panelId, DateTime? fecIngreso, string rolAdjudicado, int rolId,
                                                bool flagEnviaFechaEntrega, DateTime? fechaEntrega, int user)
        {
            int result = -1;
            try
            {
                string format = "yyyy-MM-dd HH:mm:ss";

                StoredProcedure sp = new StoredProcedure("_Insertar_Panel_Demanda_Masiva_Tribunal_Rol");
                sp.AgregarParametro("panelId", panelId);
                sp.AgregarParametro("fecIngresoTribunal", fecIngreso == null ? DBNull.Value : (object)fecIngreso.GetValueOrDefault().ToString(format, CultureInfo.InvariantCulture));
                sp.AgregarParametro("RolAdjudicado", rolAdjudicado);
                sp.AgregarParametro("RolId", rolId);
                sp.AgregarParametro("ingresarFechaEntrega", flagEnviaFechaEntrega == true ? "S" : "N");
                sp.AgregarParametro("fecEntrega", fechaEntrega == null ? DBNull.Value : (object)fechaEntrega.GetValueOrDefault().ToString(format, CultureInfo.InvariantCulture));
                sp.AgregarParametro("user", user);
                result = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.PanelDemanda.GrabarAvanceDemandaRol", 0);
                return -1;
            }
            return result;
        }

        public static List<dto.PanelDemandaEntesAsignar> ListarPanelDemandaEntesAsignar(int codemp, int tribunalId)
        {
            List<dto.PanelDemandaEntesAsignar> lst = new List<dto.PanelDemandaEntesAsignar>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Tribunal_Entes");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("tribunalId", tribunalId);

                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.PanelDemandaEntesAsignar()
                        {
                            Etjid = Int32.Parse(ds.Tables[0].Rows[i]["ETJID"].ToString()),

                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.ListarPanelDemandaEntesAsignar", 0);
                return lst;
            }
        }
        public static List<dto.OrgChartPanelDemanda> ListarPanelDemandaControlGestion(int codemp)
        {
            List<dto.OrgChartPanelDemanda> lst = new List<dto.OrgChartPanelDemanda>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Panel_Demandas_Control_Gestion");
                sp.AgregarParametro("codemp", codemp);
                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.OrgChartPanelDemanda()
                        {
                            Id = ds.Tables[0].Rows[i]["ID"].ToString(),
                            Total = ds.Tables[0].Rows[i]["TOTAL"].ToString(),
                            Item = ds.Tables[0].Rows[i]["ITEM"].ToString(),
                            Parent = ds.Tables[0].Rows[i]["PARENT"].ToString()
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.PanelDemanda.ListarPanelDemandaInsertar", 0);
                return lst;
            }
        }
        public static List<dto.OrgChartPanelDemanda> ListarPanelDemandaControlGestionMasivas(int codemp)
        {
            List<dto.OrgChartPanelDemanda> lst = new List<dto.OrgChartPanelDemanda>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Panel_Demandas_Control_Gestion_Masivas");
                sp.AgregarParametro("codemp", codemp);
                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.OrgChartPanelDemanda()
                        {
                            Id = ds.Tables[0].Rows[i]["ID"].ToString(),
                            Total = ds.Tables[0].Rows[i]["TOTAL"].ToString(),
                            Item = ds.Tables[0].Rows[i]["ITEM"].ToString(),
                            Parent = ds.Tables[0].Rows[i]["PARENT"].ToString()
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.PanelDemanda.ListarPanelDemandaInsertar", 0);
                return lst;
            }
        }
        public static List<dto.PanelDemandaReporte> ListarPanelDemandaReporte(int tipoReporte, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.PanelDemandaReporte> lst = new List<dto.PanelDemandaReporte>();
            try
            {
                DateTime fechaAsignacion = new DateTime();
                DateTime fechaAprobacionTraspaso = new DateTime();
                DateTime fechaIngresaJudicial = new DateTime();
                DateTime fechaEnvioConfeccion = new DateTime();
                DateTime fechaEntrega = new DateTime();
                DateTime fechaIngresoTribunal = new DateTime();
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_PanelDemandaReporteTipo");
                sp.AgregarParametro("tipoReporte", tipoReporte);
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
                        fechaAsignacion = new DateTime();
                        //DateTime.TryParseExact(ds.Tables[0].Rows[i]["FechaAsignacion"].ToString(), "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out fechaAsignacion);
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FechaAsignacion"].ToString(), out fechaAsignacion);
                        fechaAprobacionTraspaso = new DateTime();
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FechaAprobacionTraspaso"].ToString(), out fechaAprobacionTraspaso);
                        fechaIngresaJudicial = new DateTime();
                        DateTime.TryParse(ds.Tables[0].Rows[i]["IngresoJudicial"].ToString(), out fechaIngresaJudicial);
                        fechaEnvioConfeccion = new DateTime();
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FechaEnvio"].ToString(), out fechaEnvioConfeccion);
                        fechaEntrega = new DateTime();
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FechaEntrega"].ToString(), out fechaEntrega);
                        fechaIngresoTribunal = new DateTime();
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FechaIngresoTribunal"].ToString(), out fechaIngresoTribunal);
                        lst.Add(
                            new dto.PanelDemandaReporte()
                            {
                                PanelId = Int32.Parse(ds.Tables[0].Rows[i]["PANEL_ID"].ToString()),
                                FechaAsignacion = fechaAsignacion == new DateTime() ? (DateTime?)null : fechaAsignacion,
                                FechaAprobacionTraspaso = fechaAprobacionTraspaso == new DateTime() ? (DateTime?)null : fechaAprobacionTraspaso,
                                FechaEnvio = fechaEnvioConfeccion == new DateTime() ? (DateTime?)null : fechaEnvioConfeccion,
                                FechaEntrega = fechaEntrega == new DateTime() ? (DateTime?)null : fechaEntrega,
                                FechaIngresoTribunal = fechaIngresoTribunal == new DateTime() ? (DateTime?)null : fechaIngresoTribunal,
                                IngresoJudicial = fechaIngresaJudicial == new DateTime() ? (DateTime?)null : fechaIngresaJudicial,

                                Deudor = ds.Tables[0].Rows[i]["Deudor"].ToString(),
                                Asegurado = ds.Tables[0].Rows[i]["Asegurado"].ToString(),
                                Encargado = ds.Tables[0].Rows[i]["Encargado"].ToString(),
                                Correcciones = ds.Tables[0].Rows[i]["Correcciones"].ToString(),
                                CountCorrecciones = Int32.Parse(ds.Tables[0].Rows[i]["CountCorrecciones"].ToString()),
                                DiasTranscurso = Int32.Parse(ds.Tables[0].Rows[i]["DiasTranscurso"].ToString()),
                                //DiasTranscurso2 = Int32.Parse(ds.Tables[0].Rows[i]["DiasTranscurso2"].ToString()),
                                //Comentarios = ds.Tables[0].Rows[i]["COMENTARIOS"].ToString(),
                                //TrackingDemanda = Int32.Parse(ds.Tables[0].Rows[i]["TrackingDemanda"].ToString())
                            });

                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.PanelDemanda.ListarPanelDemandaReporte", 0);
                return lst;
            }
        }

        public static int ListarPanelDemandaReporteCount(int tipoReporte, string where, string sidx, string sord, int inicio, int limite)
        {
            int count = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_PanelDemandaReporteTipoCount");
                sp.AgregarParametro("tipoReporte", tipoReporte);
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
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.PanelDemanda.ListarPanelDemandaReporteCount", 0);
                return count;
            }
        }
        public static List<dto.PanelDemandaReporte> ListarPanelDemandaReporteOrgChartItem(int tipoReporte)
        {
            List<dto.PanelDemandaReporte> lst = new List<dto.PanelDemandaReporte>();
            try
            {
                DateTime fechaAsignacion = new DateTime();
                DateTime fechaAprobacionTraspaso = new DateTime();
                DateTime fechaIngresaJudicial = new DateTime();
                DateTime fechaEnvioConfeccion = new DateTime();
                DateTime fechaEntrega = new DateTime();
                DateTime fechaIngresoTribunal = new DateTime();
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_PanelDemandaReporte");
                sp.AgregarParametro("tipoReporte", tipoReporte);

                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        fechaAsignacion = new DateTime();
                        //DateTime.TryParseExact(ds.Tables[0].Rows[i]["FechaAsignacion"].ToString(), "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out fechaAsignacion);
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FechaAsignacion"].ToString(), out fechaAsignacion);
                        fechaAprobacionTraspaso = new DateTime();
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FechaAprobacionTraspaso"].ToString(), out fechaAprobacionTraspaso);
                        fechaIngresaJudicial = new DateTime();
                        DateTime.TryParse(ds.Tables[0].Rows[i]["IngresoJudicial"].ToString(), out fechaIngresaJudicial);
                        fechaEnvioConfeccion = new DateTime();
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FechaEnvio"].ToString(), out fechaEnvioConfeccion);
                        fechaEntrega = new DateTime();
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FechaEntrega"].ToString(), out fechaEntrega);
                        fechaIngresoTribunal = new DateTime();
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FechaIngresoTribunal"].ToString(), out fechaIngresoTribunal);
                        lst.Add(
                            new dto.PanelDemandaReporte()
                            {
                                PanelId = Int32.Parse(ds.Tables[0].Rows[i]["PANEL_ID"].ToString()),
                                FechaAsignacion = fechaAsignacion == new DateTime() ? (DateTime?)null : fechaAsignacion,
                                FechaAprobacionTraspaso = fechaAprobacionTraspaso == new DateTime() ? (DateTime?)null : fechaAprobacionTraspaso,
                                FechaEnvio = fechaEnvioConfeccion == new DateTime() ? (DateTime?)null : fechaEnvioConfeccion,
                                FechaEntrega = fechaEntrega == new DateTime() ? (DateTime?)null : fechaEntrega,
                                FechaIngresoTribunal = fechaIngresoTribunal == new DateTime() ? (DateTime?)null : fechaIngresoTribunal,
                                IngresoJudicial = fechaIngresaJudicial == new DateTime() ? (DateTime?)null : fechaIngresaJudicial,
                                Cliente = ds.Tables[0].Rows[i]["CLIENTE"].ToString(),
                                RutDeudor = ds.Tables[0].Rows[i]["RutDeudor"].ToString(),
                                Deudor = ds.Tables[0].Rows[i]["Deudor"].ToString(),
                                Asegurado = ds.Tables[0].Rows[i]["Asegurado"].ToString(),
                                Encargado = ds.Tables[0].Rows[i]["Encargado"].ToString(),
                                Correcciones = ds.Tables[0].Rows[i]["Correcciones"].ToString(),
                                CountCorrecciones = Int32.Parse(ds.Tables[0].Rows[i]["CountCorrecciones"].ToString()),
                                DiasTranscurso = Int32.Parse(ds.Tables[0].Rows[i]["DiasTranscurso"].ToString()),
                                DiasTranscurso2 = Int32.Parse(ds.Tables[0].Rows[i]["DiasTranscurso2"].ToString()),
                                Comentarios = ds.Tables[0].Rows[i]["COMENTARIOS"].ToString(),
                                TrackingDemanda = Int32.Parse(ds.Tables[0].Rows[i]["TrackingDemanda"].ToString())
                            });

                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.PanelDemanda.ListarPanelDemandaReporte", 0);
                return lst;
            }
        }

        public static List<dto.PanelDemandaReporte> ListarPanelDemandaMasivaReporteOrgChartItem(int tipoReporte)
        {
            List<dto.PanelDemandaReporte> lst = new List<dto.PanelDemandaReporte>();
            try
            {
                DateTime fechaAsignacion = new DateTime();
                DateTime fechaAprobacionTraspaso = new DateTime();
                DateTime fechaIngresaJudicial = new DateTime();
                DateTime fechaEnvioConfeccion = new DateTime();
                DateTime fechaEntrega = new DateTime();
                DateTime fechaIngresoTribunal = new DateTime();
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_PanelDemandaMasivaReporte");
                sp.AgregarParametro("tipoReporte", tipoReporte);

                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        fechaAsignacion = new DateTime();
                        //DateTime.TryParseExact(ds.Tables[0].Rows[i]["FechaAsignacion"].ToString(), "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out fechaAsignacion);
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FechaAsignacion"].ToString(), out fechaAsignacion);
                        fechaAprobacionTraspaso = new DateTime();
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FechaAprobacionTraspaso"].ToString(), out fechaAprobacionTraspaso);
                        fechaIngresaJudicial = new DateTime();
                        DateTime.TryParse(ds.Tables[0].Rows[i]["IngresoJudicial"].ToString(), out fechaIngresaJudicial);
                        fechaEnvioConfeccion = new DateTime();
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FechaEnvio"].ToString(), out fechaEnvioConfeccion);
                        fechaEntrega = new DateTime();
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FechaEntrega"].ToString(), out fechaEntrega);
                        fechaIngresoTribunal = new DateTime();
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FechaIngresoTribunal"].ToString(), out fechaIngresoTribunal);
                        lst.Add(
                            new dto.PanelDemandaReporte()
                            {
                                PanelId = Int32.Parse(ds.Tables[0].Rows[i]["ID_PANEL_MASIVO"].ToString()),
                                FechaAsignacion = fechaAsignacion == new DateTime() ? (DateTime?)null : fechaAsignacion,
                                FechaAprobacionTraspaso = fechaAprobacionTraspaso == new DateTime() ? (DateTime?)null : fechaAprobacionTraspaso,
                                FechaEnvio = fechaEnvioConfeccion == new DateTime() ? (DateTime?)null : fechaEnvioConfeccion,
                                FechaEntrega = fechaEntrega == new DateTime() ? (DateTime?)null : fechaEntrega,
                                FechaIngresoTribunal = fechaIngresoTribunal == new DateTime() ? (DateTime?)null : fechaIngresoTribunal,
                                IngresoJudicial = fechaIngresaJudicial == new DateTime() ? (DateTime?)null : fechaIngresaJudicial,
                                Cliente = ds.Tables[0].Rows[i]["CLIENTE"].ToString(),
                                RutDeudor = ds.Tables[0].Rows[i]["RutDeudor"].ToString(),
                                Deudor = ds.Tables[0].Rows[i]["Deudor"].ToString(),
                                Asegurado = ds.Tables[0].Rows[i]["Asegurado"].ToString(),
                                Encargado = ds.Tables[0].Rows[i]["Encargado"].ToString(),
                                Correcciones = ds.Tables[0].Rows[i]["Correcciones"].ToString(),
                                CountCorrecciones = Int32.Parse(ds.Tables[0].Rows[i]["CountCorrecciones"].ToString()),
                                DiasTranscurso = Int32.Parse(ds.Tables[0].Rows[i]["DiasTranscurso"].ToString()),
                                DiasTranscurso2 = Int32.Parse(ds.Tables[0].Rows[i]["DiasTranscurso2"].ToString()),
                                Comentarios = ds.Tables[0].Rows[i]["COMENTARIOS"].ToString(),
                                TrackingDemanda = Int32.Parse(ds.Tables[0].Rows[i]["TrackingDemanda"].ToString())
                            });

                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.PanelDemanda.ListarPanelDemandaReporte", 0);
                return lst;
            }
        }

        public static List<dto.PanelDemandaExcel> ListarPanelDemandasExcel(int codemp)
        {
            List<dto.PanelDemandaExcel> lst = new List<dto.PanelDemandaExcel>();

            try
            {
                DateTime fechaAsignacion = new DateTime();
                DateTime fechaAprobacionTraspaso = new DateTime();
                DateTime fechaIngresaJudicial = new DateTime();
                DateTime fechaEnvioConfeccion = new DateTime();
                DateTime fechaEntrega = new DateTime();
                DateTime fechaIngresoTribunal = new DateTime();
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Panel_Demandas_Excel");
                sp.AgregarParametro("codemp", codemp);

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        fechaAsignacion = new DateTime();
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FechaAsignacion"].ToString(), out fechaAsignacion);
                        fechaAprobacionTraspaso = new DateTime();
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FechaAprobacionTraspaso"].ToString(), out fechaAprobacionTraspaso);
                        fechaIngresaJudicial = new DateTime();
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FechaIngresaJudicial"].ToString(), out fechaIngresaJudicial);
                        fechaEnvioConfeccion = new DateTime();
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FechaEnvioConfeccion"].ToString(), out fechaEnvioConfeccion);
                        fechaEntrega = new DateTime();
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FechaEntrega"].ToString(), out fechaEntrega);
                        fechaIngresoTribunal = new DateTime();
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FechaIngresoTribunal"].ToString(), out fechaIngresoTribunal);
                        lst.Add(
                            new dto.PanelDemandaExcel()
                            {
                                Responsable = ds.Tables[0].Rows[i]["Responsable"].ToString(),
                                FechaAsignacion = fechaAsignacion,
                                FechaAprobacionTraspaso = fechaAprobacionTraspaso,
                                FechaIngresaJudicial = fechaIngresaJudicial,
                                Cliente = ds.Tables[0].Rows[i]["Cliente"].ToString(),
                                RutDeudor = ds.Tables[0].Rows[i]["RutDeudor"].ToString(),
                                Deudor = ds.Tables[0].Rows[i]["Deudor"].ToString(),
                                Asegurado = ds.Tables[0].Rows[i]["Asegurado"].ToString(),
                                TipoDocumento = ds.Tables[0].Rows[i]["TipoDocumento"].ToString(),
                                Comuna = ds.Tables[0].Rows[i]["Comuna"].ToString(),
                                Region = ds.Tables[0].Rows[i]["Region"].ToString(),
                                EncargadoCofeccion = ds.Tables[0].Rows[i]["EncargadoCofeccion"].ToString(),
                                FechaEnvioConfeccion = fechaEnvioConfeccion == new DateTime() ? (DateTime?)null : fechaEnvioConfeccion,
                                FechaEntrega = fechaEntrega == new DateTime() ? (DateTime?)null : fechaEntrega,
                                FechaIngresoTribunal = fechaIngresoTribunal == new DateTime() ? (DateTime?)null : fechaIngresoTribunal,
                                Comentarios = ds.Tables[0].Rows[i]["COMENTARIOS"].ToString(),
                                Correcciones = ds.Tables[0].Rows[i]["Correcciones"].ToString(),
                                CountCorrecciones = Int32.Parse(ds.Tables[0].Rows[i]["countCorrecciones"].ToString()),

                                RolNumero = ds.Tables[0].Rows[i]["ROL_NUMERO"].ToString(),
                                TribunalNombre = ds.Tables[0].Rows[i]["TRB_NOMBRE"].ToString()
                            });
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.PanelDemanda.ListarPanelDemandasExcel", 0);
                return lst;
            }
        }
        public static List<dto.PanelDemandaMasivaExcel> ListarPanelDemandasExcelMasiva(int codemp)
        {
            List<dto.PanelDemandaMasivaExcel> lst = new List<dto.PanelDemandaMasivaExcel>();

            try
            {
                DateTime fechaAsignacion = new DateTime();
                DateTime fechaAprobacionTraspaso = new DateTime();
                DateTime fechaIngresaJudicial = new DateTime();
                DateTime fechaEnvioConfeccion = new DateTime();
                DateTime fechaEntrega = new DateTime();
                DateTime fechaIngresoTribunal = new DateTime();
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Panel_Demandas_Masivas_Excel");
                sp.AgregarParametro("codemp", codemp);

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        fechaAsignacion = new DateTime();
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FechaAsignacion"].ToString(), out fechaAsignacion);
                        fechaAprobacionTraspaso = new DateTime();
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FechaAprobacionTraspaso"].ToString(), out fechaAprobacionTraspaso);
                        fechaIngresaJudicial = new DateTime();
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FechaIngresaJudicial"].ToString(), out fechaIngresaJudicial);
                        fechaEnvioConfeccion = new DateTime();
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FechaEnvioConfeccion"].ToString(), out fechaEnvioConfeccion);
                        fechaEntrega = new DateTime();
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FechaEntrega"].ToString(), out fechaEntrega);
                        fechaIngresoTribunal = new DateTime();
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FechaIngresoTribunal"].ToString(), out fechaIngresoTribunal);
                        lst.Add(
                            new dto.PanelDemandaMasivaExcel()
                            {
                                IdRol = String.IsNullOrEmpty(ds.Tables[0].Rows[i]["IdRol"].ToString()) ? "" : ds.Tables[0].Rows[i]["IdRol"].ToString(),
                                Responsable = ds.Tables[0].Rows[i]["Responsable"].ToString(),
                                FechaAsignacion = fechaAsignacion,
                                FechaAprobacionTraspaso = fechaAprobacionTraspaso,
                                FechaIngresaJudicial = fechaIngresaJudicial,
                                Cliente = ds.Tables[0].Rows[i]["Cliente"].ToString(),
                                RutDeudor = ds.Tables[0].Rows[i]["RutDeudor"].ToString(),
                                Deudor = ds.Tables[0].Rows[i]["Deudor"].ToString(),
                                Asegurado = ds.Tables[0].Rows[i]["Asegurado"].ToString(),
                                TipoDocumento = ds.Tables[0].Rows[i]["TipoDocumento"].ToString(),
                                Comuna = ds.Tables[0].Rows[i]["Comuna"].ToString(),
                                Region = ds.Tables[0].Rows[i]["Region"].ToString(),
                                //EncargadoCofeccion = ds.Tables[0].Rows[i]["EncargadoCofeccion"].ToString(),
                                FechaEnvioConfeccion = fechaEnvioConfeccion == new DateTime() ? (DateTime?)null : fechaEnvioConfeccion,
                                FechaEntrega = fechaEntrega == new DateTime() ? (DateTime?)null : fechaEntrega,
                                FechaIngresoTribunal = fechaIngresoTribunal == new DateTime() ? (DateTime?)null : fechaIngresoTribunal,
                                Comentarios = ds.Tables[0].Rows[i]["COMENTARIOS"].ToString(),
                                Correcciones = ds.Tables[0].Rows[i]["Correcciones"].ToString(),
                                CountCorrecciones = Int32.Parse(ds.Tables[0].Rows[i]["CountCorrecciones"].ToString()),
                                RolNumero = (String.IsNullOrEmpty(ds.Tables[0].Rows[i]["RolNumero"].ToString()) ? "" : "ROL " + ds.Tables[0].Rows[i]["RolNumero"].ToString()),
                                TribunalNombre = ds.Tables[0].Rows[i]["TribunalNombre"].ToString(),
                                CantidadNoCurso = Int32.Parse(ds.Tables[0].Rows[i]["CantidadNoCurso"].ToString())
                            });
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.PanelDemanda.ListarPanelDemandasExcelMasiva", 0);
                return lst;
            }
        }
        public static List<dto.PanelDemandaExcel> ListarPanelDemandasExcelPrevisional(int codemp)
        {
            List<dto.PanelDemandaExcel> lst = new List<dto.PanelDemandaExcel>();

            try
            {
                DateTime fechaAsignacion = new DateTime();
                DateTime fechaAprobacionTraspaso = new DateTime();
                DateTime fechaIngresaJudicial = new DateTime();
                DateTime fechaEnvioConfeccion = new DateTime();
                DateTime fechaEntrega = new DateTime();
                DateTime fechaIngresoTribunal = new DateTime();
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Panel_Demandas_Previsional_Excel");
                sp.AgregarParametro("codemp", codemp);

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        fechaAsignacion = new DateTime();
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FechaAsignacion"].ToString(), out fechaAsignacion);
                        fechaAprobacionTraspaso = new DateTime();
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FechaAprobacionTraspaso"].ToString(), out fechaAprobacionTraspaso);
                        fechaIngresaJudicial = new DateTime();
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FechaIngresaJudicial"].ToString(), out fechaIngresaJudicial);
                        fechaEnvioConfeccion = new DateTime();
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FechaEnvioConfeccion"].ToString(), out fechaEnvioConfeccion);
                        fechaEntrega = new DateTime();
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FechaEntrega"].ToString(), out fechaEntrega);
                        fechaIngresoTribunal = new DateTime();
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FechaIngresoTribunal"].ToString(), out fechaIngresoTribunal);
                        lst.Add(
                            new dto.PanelDemandaExcel()
                            {
                                Responsable = ds.Tables[0].Rows[i]["Responsable"].ToString(),
                                FechaAsignacion = fechaAsignacion,
                                FechaAprobacionTraspaso = fechaAprobacionTraspaso,
                                FechaIngresaJudicial = fechaIngresaJudicial,
                                Cliente = ds.Tables[0].Rows[i]["Cliente"].ToString(),
                                RutDeudor = ds.Tables[0].Rows[i]["RutDeudor"].ToString(),
                                Deudor = ds.Tables[0].Rows[i]["Deudor"].ToString(),
                                Asegurado = ds.Tables[0].Rows[i]["Asegurado"].ToString(),
                                TipoDocumento = ds.Tables[0].Rows[i]["TipoDocumento"].ToString(),
                                Comuna = ds.Tables[0].Rows[i]["Comuna"].ToString(),
                                Region = ds.Tables[0].Rows[i]["Region"].ToString(),
                                EncargadoCofeccion = ds.Tables[0].Rows[i]["EncargadoCofeccion"].ToString(),
                                FechaEnvioConfeccion = fechaEnvioConfeccion == new DateTime() ? (DateTime?)null : fechaEnvioConfeccion,
                                FechaEntrega = fechaEntrega == new DateTime() ? (DateTime?)null : fechaEntrega,
                                FechaIngresoTribunal = fechaIngresoTribunal == new DateTime() ? (DateTime?)null : fechaIngresoTribunal,
                                Comentarios = ds.Tables[0].Rows[i]["COMENTARIOS"].ToString(),
                                Correcciones = ds.Tables[0].Rows[i]["Correcciones"].ToString(),
                                CountCorrecciones = Int32.Parse(ds.Tables[0].Rows[i]["countCorrecciones"].ToString()),

                                RolNumero = ds.Tables[0].Rows[i]["ROL_NUMERO"].ToString(),
                                TribunalNombre = ds.Tables[0].Rows[i]["TRB_NOMBRE"].ToString()
                            });
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.PanelDemanda.ListarPanelDemandasExcelPrevisional", 0);
                return lst;
            }
        }

        public static int GrabarCursoDemanda(int codemp, int panelId, int cursoDemanda, string motivo, int user)
        {
            int result = -1;
            try
            {
                StoredProcedure sp = new StoredProcedure("_Insertar_Panel_Demanda_CursoDemanda");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("panelId", panelId);
                sp.AgregarParametro("cursoDemanda", cursoDemanda == 1 ? "SI" : "NO");
                sp.AgregarParametro("motivo", string.IsNullOrEmpty(motivo) ? DBNull.Value : (object)motivo);
                sp.AgregarParametro("user", user);
                result = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.PanelDemanda.GrabarCursoDemanda", user);
                return -1;
            }
            return result;
        }
        public static int GrabarCursoDemandaPrevisional(int codemp, int panelId, int cursoDemanda, string motivo, int user)
        {
            int result = -1;
            try
            {
                StoredProcedure sp = new StoredProcedure("_Insertar_Panel_Demanda_Previsional_CursoDemanda");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("panelId", panelId);
                sp.AgregarParametro("cursoDemanda", cursoDemanda == 1 ? "SI" : "NO");
                sp.AgregarParametro("motivo", string.IsNullOrEmpty(motivo) ? DBNull.Value : (object)motivo);
                sp.AgregarParametro("user", user);
                result = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.PanelDemanda.GrabarCursoDemandaPrevisional", user);
                return -1;
            }
            return result;
        }
        

        public static int GrabarCursoDemandaMasiva(int codemp, int panelId, int cursoDemanda, string motivo, int user)
        {
            int result = -1;
            try
            {
                StoredProcedure sp = new StoredProcedure("_Insertar_Panel_Demanda_Masiva_CursoDemanda");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("panelId", panelId);
                sp.AgregarParametro("cursoDemanda", cursoDemanda == 1 ? "SI" : "NO");
                sp.AgregarParametro("motivo", string.IsNullOrEmpty(motivo) ? DBNull.Value : (object)motivo);
                sp.AgregarParametro("user", user);
                result = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.PanelDemanda.GrabarCursoDemandaMasiva", user);
                return -1;
            }
            return result;
        }

        public static List<dto.DocumentosPanel> ListarDocumentosPanelId(int codemp, int panelId, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.DocumentosPanel> lst = new List<dto.DocumentosPanel>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Panel_Demandas_Documentos_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("panelId", panelId);
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
                        lst.Add(new dto.DocumentosPanel()
                        {
                            PanelId = Int32.Parse(ds.Tables[0].Rows[i]["PANELID"].ToString()),
                            Pclid = Int32.Parse(ds.Tables[0].Rows[i]["Pclid"].ToString()),
                            Ctcid = Int32.Parse(ds.Tables[0].Rows[i]["Ctcid"].ToString()),
                            Ccbid = Int32.Parse(ds.Tables[0].Rows[i]["Ccbid"].ToString()),
                            TipoDocumento = ds.Tables[0].Rows[i]["TipoDocumento"].ToString(),
                            Numero = ds.Tables[0].Rows[i]["Numero"].ToString(),
                            Monto = decimal.Parse(ds.Tables[0].Rows[i]["Monto"].ToString()),
                            Saldo = decimal.Parse(ds.Tables[0].Rows[i]["Saldo"].ToString()),
                            Estado = ds.Tables[0].Rows[i]["Estado"].ToString(),
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
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.ListarDocumentosPanelId", 0);
                return lst;
            }
        }

        public static List<dto.DocumentosPanel> ListarDocumentosPanelPrevisionalId(int codemp, int panelId, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.DocumentosPanel> lst = new List<dto.DocumentosPanel>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Panel_Demandas_Previsional_Documentos_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("panelId", panelId);
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
                        lst.Add(new dto.DocumentosPanel()
                        {
                            PanelId = Int32.Parse(ds.Tables[0].Rows[i]["PANELID"].ToString()),
                            Pclid = Int32.Parse(ds.Tables[0].Rows[i]["PCLID"].ToString()),
                            Ctcid = Int32.Parse(ds.Tables[0].Rows[i]["CTCID"].ToString()),
                            NumResolucion = ds.Tables[0].Rows[i]["NUM_RESOLUCION"].ToString(),
                            FecResolucion = DateTime.Parse(ds.Tables[0].Rows[i]["FEC_RESOLUCION"].ToString()),
                            Monto = decimal.Parse(ds.Tables[0].Rows[i]["MONTO"].ToString()),
                            Saldo = decimal.Parse(ds.Tables[0].Rows[i]["SALDO"].ToString()),
                            Estado = ds.Tables[0].Rows[i]["ESTADO"].ToString()
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.ListarDocumentosPanelPrevisionalId", 0);
                return lst;
            }
        }
        
        public static List<dto.DocumentosPanel> ListarDocumentosMasivosPanelId(int codemp, int panelId, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.DocumentosPanel> lst = new List<dto.DocumentosPanel>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Panel_Demandas_Masivas_Documentos_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("panelId", panelId);
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
                        lst.Add(new dto.DocumentosPanel()
                        {
                            PanelId = Int32.Parse(ds.Tables[0].Rows[i]["PANELID"].ToString()),
                            Pclid = Int32.Parse(ds.Tables[0].Rows[i]["Pclid"].ToString()),
                            Ctcid = Int32.Parse(ds.Tables[0].Rows[i]["Ctcid"].ToString()),
                            Ccbid = Int32.Parse(ds.Tables[0].Rows[i]["Ccbid"].ToString()),
                            TipoDocumento = ds.Tables[0].Rows[i]["TipoDocumento"].ToString(),
                            Numero = ds.Tables[0].Rows[i]["Numero"].ToString(),
                            Monto = decimal.Parse(ds.Tables[0].Rows[i]["Monto"].ToString()),
                            Saldo = decimal.Parse(ds.Tables[0].Rows[i]["Saldo"].ToString()),
                            Estado = ds.Tables[0].Rows[i]["Estado"].ToString(),
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
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.ListarDocumentosMasivosPanelId", 0);
                return lst;
            }
        }

        public static dto.ConfeccionDemanda Demanda(int codemp, int idpanel)
        {
            dto.ConfeccionDemanda lst = new dto.ConfeccionDemanda();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Datos_Confeccion_Demanda");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idpanel", idpanel);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst = new dto.ConfeccionDemanda()
                        {
                            Rolid = 1,
                            //TipoRol = ds.Tables[0].Rows[i]["tipo_rol"].ToString(),
                            Rol = ds.Tables[0].Rows[i]["rol_numero"].ToString(),
                            Anio = DateTime.Today.Year,
                            RutCliente = Dimol.bcp.Funciones.formatearRut(ds.Tables[0].Rows[i]["pcl_rut"].ToString()),
                            Cliente = ds.Tables[0].Rows[i]["pcl_nomfant"].ToString(),
                            RutDeudor = Dimol.bcp.Funciones.formatearRut(ds.Tables[0].Rows[i]["ctc_numero"].ToString() + ds.Tables[0].Rows[i]["ctc_digito"].ToString()),
                            NombreFantasiaDeudor = ds.Tables[0].Rows[i]["ctc_nomfant"].ToString(),

                            PaisDeudor = ds.Tables[0].Rows[i]["pai_nombre"].ToString(),
                            RegionDeudor = ds.Tables[0].Rows[i]["reg_nombre"].ToString(),
                            CiudadDeudor = ds.Tables[0].Rows[i]["ciu_nombre"].ToString(),
                            ComunaDeudor = ds.Tables[0].Rows[i]["com_nombre"].ToString(),
                            CodigoPostalDeudor = ds.Tables[0].Rows[i]["com_codpost"].ToString(),

                            DireccionDeudor = ds.Tables[0].Rows[i]["ctc_direccion"].ToString(),

                            RutEmpleado = Dimol.bcp.Funciones.formatearRut(ds.Tables[0].Rows[i]["epl_rut"].ToString()),
                            NombreEmpleado = ds.Tables[0].Rows[i]["epl_nombre"].ToString(),
                            PaternoEmpleado = ds.Tables[0].Rows[i]["epl_apepat"].ToString(),
                            MaternoEmpleado = ds.Tables[0].Rows[i]["epl_apemat"].ToString(),
                            Tribunal = ds.Tables[0].Rows[i]["trb_nombre"].ToString(),
                            TelefonoEmpleado = ds.Tables[0].Rows[i]["epl_telefono"].ToString(),
                            MailEmpleado = ds.Tables[0].Rows[i]["epl_mail"].ToString(),

                            FechaDemanda = DateTime.Parse(ds.Tables[0].Rows[i]["fecdem"].ToString()),
                            Cuotas = Int32.Parse(ds.Tables[0].Rows[i]["cuodem"].ToString()),
                            Monto = decimal.Parse(ds.Tables[0].Rows[i]["mondem"].ToString()),
                            MontoPrimeraCuota = decimal.Parse(ds.Tables[0].Rows[i]["monpcuodem"].ToString()),
                            MontoUltimaCuota = decimal.Parse(ds.Tables[0].Rows[i]["monucoudem"].ToString()),
                            FechaPrimeraCuota = DateTime.Parse(ds.Tables[0].Rows[i]["fecpcoudem"].ToString()),
                            FechaUltimaCuota = DateTime.Parse(ds.Tables[0].Rows[i]["fecucoudem"].ToString()),
                            Interes = decimal.Parse(ds.Tables[0].Rows[i]["intdem"].ToString()),


                            NombreCliente = ds.Tables[0].Rows[i]["pcl_nombre"].ToString(),
                            PaternoCliente = ds.Tables[0].Rows[i]["pcl_apepat"].ToString(),
                            MaternoCliente = ds.Tables[0].Rows[i]["pcl_apemat"].ToString(),

                            MateriaJudicial = ds.Tables[0].Rows[i]["mji_nombre"].ToString(),
                            TipoCausa = ds.Tables[0].Rows[i]["tci_nombre"].ToString(),
                            Estado = ds.Tables[0].Rows[i]["eci_nombre"].ToString(),

                            RepresentanteLegal = ds.Tables[0].Rows[i]["pcl_replegal"].ToString(),
                            RutRepresentanteLegal = Dimol.bcp.Funciones.formatearRut(ds.Tables[0].Rows[i]["pcl_rutlegal"].ToString()),

                            Numero = ds.Tables[0].Rows[i]["ccb_numero"].ToString(),
                            FechaVencimiento = DateTime.Parse(ds.Tables[0].Rows[i]["ccb_fecvenc"].ToString()),

                            Saldo = decimal.Parse(ds.Tables[0].Rows[i]["rdc_saldo"].ToString()),

                            DireccionEmpresa = ds.Tables[0].Rows[i]["esu_direccion"].ToString(),
                            TelefonoEmpresa = ds.Tables[0].Rows[i]["esu_telefono"].ToString(),
                            PaisEmpresa = ds.Tables[0].Rows[i][38].ToString(),
                            RegionEmpresa = ds.Tables[0].Rows[i][39].ToString(),
                            CiudadEmpresa = ds.Tables[0].Rows[i][40].ToString(),
                            ComunaEmpresa = ds.Tables[0].Rows[i][41].ToString(),

                            NombreDeudor = ds.Tables[0].Rows[i]["ctc_nombre"].ToString(),
                            PaternoDeudor = ds.Tables[0].Rows[i]["ctc_apepat"].ToString(),
                            MaternoDeudor = ds.Tables[0].Rows[i]["ctc_apemat"].ToString(),

                            DiaPago = DateTime.Parse(ds.Tables[0].Rows[i]["fecpcoudem"].ToString()).Day,

                            MontoDemandaEscrito = NumeroEnletras(ds.Tables[0].Rows[i]["mondem"].ToString()),
                            MontoPCuotaDemandaEscrito = NumeroEnletras(ds.Tables[0].Rows[i]["monpcuodem"].ToString()),
                            MontoUCuotaDemandaEscrito = NumeroEnletras(ds.Tables[0].Rows[i]["monucoudem"].ToString()),
                            MontoSaldoDemandaEscrito = NumeroEnletras(ds.Tables[0].Rows[i]["rdc_saldo"].ToString())

                        };
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                return lst;
            }
        }

        public static string NumeroEnletras(string num)
        {
            string res, dec = "";
            Int64 entero;
            int decimales;
            double nro;
            try
            {
                nro = Convert.ToDouble(num);
            }
            catch
            {
                return "";
            }
            entero = Convert.ToInt64(Math.Truncate(nro));
            decimales = Convert.ToInt32(Math.Round((nro - entero) * 100, 2));
            if (decimales > 0)
            {
                dec = " CON " + decimales.ToString() + "/100";
            }
            res = toText(Convert.ToDouble(entero)) + dec;
            return res;
        }

        private static string toText(double value)
        {
            string Num2Text = "";
            value = Math.Truncate(value);
            if (value == 0) Num2Text = "CERO";

            else if (value == 1) Num2Text = "UNO";

            else if (value == 2) Num2Text = "DOS";

            else if (value == 3) Num2Text = "TRES";

            else if (value == 4) Num2Text = "CUATRO";

            else if (value == 5) Num2Text = "CINCO";

            else if (value == 6) Num2Text = "SEIS";

            else if (value == 7) Num2Text = "SIETE";

            else if (value == 8) Num2Text = "OCHO";

            else if (value == 9) Num2Text = "NUEVE";

            else if (value == 10) Num2Text = "DIEZ";

            else if (value == 11) Num2Text = "ONCE";

            else if (value == 12) Num2Text = "DOCE";

            else if (value == 13) Num2Text = "TRECE";

            else if (value == 14) Num2Text = "CATORCE";

            else if (value == 15) Num2Text = "QUINCE";

            else if (value < 20) Num2Text = "DIECI" + toText(value - 10);

            else if (value == 20) Num2Text = "VEINTE";

            else if (value < 30) Num2Text = "VEINTI" + toText(value - 20);

            else if (value == 30) Num2Text = "TREINTA";

            else if (value == 40) Num2Text = "CUARENTA";

            else if (value == 50) Num2Text = "CINCUENTA";

            else if (value == 60) Num2Text = "SESENTA";

            else if (value == 70) Num2Text = "SETENTA";

            else if (value == 80) Num2Text = "OCHENTA";

            else if (value == 90) Num2Text = "NOVENTA";

            else if (value < 100) Num2Text = toText(Math.Truncate(value / 10) * 10) + " Y " + toText(value % 10);

            else if (value == 100) Num2Text = "CIEN";

            else if (value < 200) Num2Text = "CIENTO " + toText(value - 100);

            else if ((value == 200) || (value == 300) || (value == 400) || (value == 600) || (value == 800)) Num2Text = toText(Math.Truncate(value / 100)) + "CIENTOS";

            else if (value == 500) Num2Text = "QUINIENTOS";

            else if (value == 700) Num2Text = "SETECIENTOS";

            else if (value == 900) Num2Text = "NOVECIENTOS";

            else if (value < 1000) Num2Text = toText(Math.Truncate(value / 100) * 100) + " " + toText(value % 100);

            else if (value == 1000) Num2Text = "MIL";

            else if (value < 2000) Num2Text = "MIL " + toText(value % 1000);

            else if (value < 1000000)
            {

                Num2Text = toText(Math.Truncate(value / 1000)) + " MIL";

                if ((value % 1000) > 0) Num2Text = Num2Text + " " + toText(value % 1000);

            }

            else if (value == 1000000) Num2Text = "UN MILLON";

            else if (value < 2000000) Num2Text = "UN MILLON " + toText(value % 1000000);

            else if (value < 1000000000000)
            {

                Num2Text = toText(Math.Truncate(value / 1000000)) + " MILLONES ";

                if ((value - Math.Truncate(value / 1000000) * 1000000) > 0) Num2Text = Num2Text + " " + toText(value - Math.Truncate(value / 1000000) * 1000000);

            }

            else if (value == 1000000000000) Num2Text = "UN BILLON";

            else if (value < 2000000000000) Num2Text = "UN BILLON " + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000);

            else
            {

                Num2Text = toText(Math.Truncate(value / 1000000000000)) + " BILLONES";

                if ((value - Math.Truncate(value / 1000000000000) * 1000000000000) > 0) Num2Text = Num2Text + " " + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000);

            }

            return Num2Text;

        }

        public static string TraeXSLTemplate(int idTemplate)
        {
            string xslTemplate = "";

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Template_Demanda_Masiva");
                sp.AgregarParametro("pdmcid", idTemplate);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    xslTemplate = ds.Tables[0].Rows[0]["PDMC_TEMPLATE"].ToString();
                }

                return xslTemplate;
            }
            catch (Exception ex)
            {
                return xslTemplate;
            }
        }

        public static bool EliminarPanelDemanda(int IdPanelDemanda)
        {
            try {
                StoredProcedure sp = new StoredProcedure("_Eliminar_PanelDemandaPorId");
                sp.AgregarParametro("idPanel", IdPanelDemanda);

                if (sp.EjecutarProcedimientoTrans() > 0) {
                    return true;
                } else {
                    return false;
                }
            } catch (Exception ex) {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.PanelDemanda.EliminarPanelDemanda", 0);
                return false;
            }
        }

        public static bool EliminarPanelDemandaPrevisional(int IdPanelDemanda)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Eliminar_PanelDemandaPrevisionalPorId");
                sp.AgregarParametro("idPanel", IdPanelDemanda);

                if (sp.EjecutarProcedimientoTrans() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.PanelDemanda.EliminarPanelDemandaPrevisional", 0);
                return false;
            }
        }
    }
}
