using Dimol.dao;
using Dimol.dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Dimol.Judicial.Mantenedores.dao
{
    public class PanelAlerta
    {
        public static List<dto.PanelAlertaAnalisisCliente> ListarPanelAlertaAnalisisCliente(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.PanelAlertaAnalisisCliente> lst = new List<dto.PanelAlertaAnalisisCliente>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_PanelAlerta_AnalisisCliente_Grilla");
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
                        lst.Add(new dto.PanelAlertaAnalisisCliente()
                        {
                            Pclid = Int32.Parse(ds.Tables[0].Rows[i]["PCLID"].ToString()),
                            Cliente = ds.Tables[0].Rows[i]["CLIENTE"].ToString(),
                            CantDemandas = Int32.Parse(ds.Tables[0].Rows[i]["CANTDEMANDAS"].ToString()),
                            Saldo = decimal.Parse(ds.Tables[0].Rows[i]["Saldo"].ToString()),
                            Percentage = Int32.Parse(ds.Tables[0].Rows[i]["Percentage"].ToString()),
                            Porcentaje = ds.Tables[0].Rows[i]["PORCENTAJE"].ToString()
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.ListarPanelAlertaAnalisisCliente", 0);
                return lst;
            }
        }

        public static int ListarPanelAlertaAnalisisClienteCount(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            int count = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_PanelAlerta_AnalisisCliente_Grilla_Count");
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
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.PanelAlerta.ListarPanelAlertaAnalisisClienteCount", 0);
                return count;
            }
        }

        public static List<dto.PanelAlerta> ListarPanelAlerta(int codemp)
        {
            List<dto.PanelAlerta> lst = new List<dto.PanelAlerta>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_PanelAlerta");
                sp.AgregarParametro("codemp", codemp);
               
                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.PanelAlerta()
                        {
                            DemandasProceso = Int32.Parse(ds.Tables[0].Rows[i]["DemandasProceso"].ToString()),
                            PromedioConfeccionDias = ds.Tables[0].Rows[i]["PromedioConfeccionDias"].ToString(),
                            CantCorrecciones = Int32.Parse(ds.Tables[0].Rows[i]["CantCorrecciones"].ToString()),
                            PromedioCorrecciones = ds.Tables[0].Rows[i]["PromedioCorrecciones"].ToString(),
                            TraspasosAprobados = Int32.Parse(ds.Tables[0].Rows[i]["TotalTraspaso"].ToString()),
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.ListarPanelAlerta", 0);
                return lst;
            }
        }

        public static List<dto.PanelAlerta> ListarPanelAlertaMasiva(int codemp)
        {
            List<dto.PanelAlerta> lst = new List<dto.PanelAlerta>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_PanelAlertaMasiva");
                sp.AgregarParametro("codemp", codemp);

                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.PanelAlerta()
                        {
                            DemandasProceso = Int32.Parse(ds.Tables[0].Rows[i]["DemandasProceso"].ToString()),
                            PromedioConfeccionDias = ds.Tables[0].Rows[i]["PromedioConfeccionDias"].ToString(),
                            TraspasosAprobados = Int32.Parse(ds.Tables[0].Rows[i]["TotalTraspaso"].ToString()),
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.ListarPanelAlertaMasiva", 0);
                return lst;
            }
        }

        public static List<dto.PanelAlertaEncargado> ListarPanelAlertaEncargado(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.PanelAlertaEncargado> lst = new List<dto.PanelAlertaEncargado>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_PanelAlerta_Encargados_Grilla");
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
                        lst.Add(new dto.PanelAlertaEncargado()
                        {
                            UsrId = Int32.Parse(ds.Tables[0].Rows[i]["USR"].ToString()),
                            Encargado = ds.Tables[0].Rows[i]["Encargado"].ToString(),
                            CantDemandas = Int32.Parse(ds.Tables[0].Rows[i]["CANTDEMANDAS"].ToString()),
                            Saldo = decimal.Parse(ds.Tables[0].Rows[i]["Saldo"].ToString()),
                            CantCorrecciones = Int32.Parse(ds.Tables[0].Rows[i]["CantCorrecciones"].ToString()),
                            TiempoEntrega = ds.Tables[0].Rows[i]["TiempoEntrega"].ToString()
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.ListarPanelAlertaEncargado", 0);
                return lst;
            }
        }

        public static int ListarPanelAlertaEncargadoCount(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            int count = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_PanelAlerta_Encargados_Grilla_Count");
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
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.PanelAlerta.ListarPanelAlertaEncargadoCount", 0);
                return count;
            }
        }

        public static List<dto.PanelAlertaTipo> ListarPanelAlertaTipo(int codemp)
        {
            List<dto.PanelAlertaTipo> lst = new List<dto.PanelAlertaTipo>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_PanelAlerta_Tipo");
                sp.AgregarParametro("codemp", codemp);

                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.PanelAlertaTipo()
                        {
                            Id = Int32.Parse(ds.Tables[0].Rows[i]["ID"].ToString()),
                            Item = ds.Tables[0].Rows[i]["Item"].ToString(),
                            PromedioDias = Int32.Parse(ds.Tables[0].Rows[i]["PromedioDias"].ToString()),
                            CantCasos = Int32.Parse(ds.Tables[0].Rows[i]["CantCasos"].ToString()),
                            Atraso = ds.Tables[0].Rows[i]["Atraso"].ToString()
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.ListarPanelAlerta", 0);
                return lst;
            }
        }

        public static List<dto.PanelAlertaTipo> ListarPanelAlertaTipoMasiva(int codemp)
        {
            List<dto.PanelAlertaTipo> lst = new List<dto.PanelAlertaTipo>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_PanelAlerta_Tipo_Masiva");
                sp.AgregarParametro("codemp", codemp);

                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.PanelAlertaTipo()
                        {
                            Id = Int32.Parse(ds.Tables[0].Rows[i]["ID"].ToString()),
                            Item = ds.Tables[0].Rows[i]["Item"].ToString(),
                            PromedioDias = Int32.Parse(ds.Tables[0].Rows[i]["PromedioDias"].ToString()),
                            CantCasos = Int32.Parse(ds.Tables[0].Rows[i]["CantCasos"].ToString()),
                            Atraso = ds.Tables[0].Rows[i]["Atraso"].ToString()
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.ListarPanelAlerta", 0);
                return lst;
            }
        }

        public static List<dto.PanelAlertaTipoReporte> ListarPanelAlertaTipoReporte(int codemp, int tipoReporte, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.PanelAlertaTipoReporte> lst = new List<dto.PanelAlertaTipoReporte>();
            try
            {
                DateTime fechaAprobacionTraspaso = new DateTime();
                DateTime fechaIngresaJudicial = new DateTime();
                DateTime fechaEnvioConfeccion = new DateTime();
                DateTime fechaEntrega = new DateTime();
                DateTime fechaIngresoTribunal = new DateTime();
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_PanelAlerta_TipoReporte_Grilla");
                sp.AgregarParametro("codemp", codemp);
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
                            new dto.PanelAlertaTipoReporte()
                            {
                                PanelId = Int32.Parse(ds.Tables[0].Rows[i]["PANELID"].ToString()),
                                FechaAprobacionTraspaso = fechaAprobacionTraspaso == new DateTime() ? (DateTime?)null : fechaAprobacionTraspaso,
                                FechaEnvio = fechaEnvioConfeccion == new DateTime() ? (DateTime?)null : fechaEnvioConfeccion,
                                FechaEntrega = fechaEntrega == new DateTime() ? (DateTime?)null : fechaEntrega,
                                FechaIngresoTribunal = fechaIngresoTribunal == new DateTime() ? (DateTime?)null : fechaIngresoTribunal,
                                IngresoJudicial = fechaIngresaJudicial == new DateTime() ? (DateTime?)null : fechaIngresaJudicial,
                                Cliente = ds.Tables[0].Rows[i]["CLIENTE"].ToString(),
                                Deudor = ds.Tables[0].Rows[i]["Deudor"].ToString(),
                                Asegurado = ds.Tables[0].Rows[i]["Asegurado"].ToString(),
                                Encargado = ds.Tables[0].Rows[i]["Encargado"].ToString(),
                                Correcciones = ds.Tables[0].Rows[i]["Correcciones"].ToString(),
                                CountCorrecciones = Int32.Parse(ds.Tables[0].Rows[i]["CountCorrecciones"].ToString()),
                                DiasTranscurso = Int32.Parse(ds.Tables[0].Rows[i]["DiasTranscurso"].ToString()),
                                Comentarios = ds.Tables[0].Rows[i]["COMENTARIOS"].ToString(),
                                DiasAtraso = Int32.Parse(ds.Tables[0].Rows[i]["DiasAtraso"].ToString())
                            });

                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.PanelAlerta.ListarPanelAlertaTipoReporte", 0);
                return lst;
            }
        }

        public static List<dto.PanelAlertaReporteAnalisisCliente> ListarPanelAlertaReporteAnalisisCliente(int codemp, int pclid, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.PanelAlertaReporteAnalisisCliente> lst = new List<dto.PanelAlertaReporteAnalisisCliente>();
            try
            {
                DateTime fechaAprobacionTraspaso = new DateTime();
                DateTime fechaIngresaJudicial = new DateTime();
                DateTime fechaEnvioConfeccion = new DateTime();
                DateTime fechaEntrega = new DateTime();
                DateTime fechaIngresoTribunal = new DateTime();
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_PanelAlerta_ReporteAnalisisCliente_Grilla");
                sp.AgregarParametro("codemp", codemp);
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
                            new dto.PanelAlertaReporteAnalisisCliente()
                            {
                                PanelId = Int32.Parse(ds.Tables[0].Rows[i]["PANELID"].ToString()),
                                Pclid = Int32.Parse(ds.Tables[0].Rows[i]["PCLID"].ToString()),
                                FechaAprobacionTraspaso = fechaAprobacionTraspaso == new DateTime() ? (DateTime?)null : fechaAprobacionTraspaso,
                                FechaEnvio = fechaEnvioConfeccion == new DateTime() ? (DateTime?)null : fechaEnvioConfeccion,
                                FechaEntrega = fechaEntrega == new DateTime() ? (DateTime?)null : fechaEntrega,
                                FechaIngresoTribunal = fechaIngresoTribunal == new DateTime() ? (DateTime?)null : fechaIngresoTribunal,
                                IngresoJudicial = fechaIngresaJudicial == new DateTime() ? (DateTime?)null : fechaIngresaJudicial,
                                Cliente = ds.Tables[0].Rows[i]["CLIENTE"].ToString(),
                                Deudor = ds.Tables[0].Rows[i]["Deudor"].ToString(),
                                Asegurado = ds.Tables[0].Rows[i]["Asegurado"].ToString(),
                                Encargado = ds.Tables[0].Rows[i]["Encargado"].ToString(),
                                Correcciones = ds.Tables[0].Rows[i]["Correcciones"].ToString(),
                                CountCorrecciones = Int32.Parse(ds.Tables[0].Rows[i]["CountCorrecciones"].ToString()),
                                Comentarios = ds.Tables[0].Rows[i]["COMENTARIOS"].ToString(),
                                TipoDocumento = ds.Tables[0].Rows[i]["TipoDocumento"].ToString(),
                                Saldo = decimal.Parse(ds.Tables[0].Rows[i]["SALDO"].ToString())
                            });

                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.PanelAlerta.ListarPanelAlertaTipoReporte", 0);
                return lst;
            }
        }
    }
}
