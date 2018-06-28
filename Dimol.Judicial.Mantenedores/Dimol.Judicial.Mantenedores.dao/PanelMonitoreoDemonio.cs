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
    public class PanelMonitoreoDemonio
    {
        public static List<dto.MonitoreoExternoCabecera> ListarPanelMonitoreoExternoCabecera()
        {
            List<dto.MonitoreoExternoCabecera> lst = new List<dto.MonitoreoExternoCabecera>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Panel_Monitoreo_Externo_Cabecera");
                
                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.MonitoreoExternoCabecera()
                        {
                            Recolecto = ds.Tables[0].Rows[i]["Recolecto"].ToString(),
                            CantCausas = Int32.Parse(ds.Tables[0].Rows[i]["CantCausas"].ToString()),
                            CantMesActual = Int32.Parse(ds.Tables[0].Rows[i]["CantMesActual"].ToString())
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.ListarPanelMonitoreoExternoCabecera", 0);
                return lst;
            }
        }
        public static List<dto.MonitoreoExternoDemanda> ListarPanelMonitoreoExternoDemandas(string where, string sidx, string sord)
        {
            List<dto.MonitoreoExternoDemanda> lst = new List<dto.MonitoreoExternoDemanda>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Panel_Monitoreo_Externo_ClientesDemandas_Grilla");
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                
                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.MonitoreoExternoDemanda()
                        {
                            Pclid = Int32.Parse(ds.Tables[0].Rows[i]["PCLID"].ToString()),
                            Cliente = ds.Tables[0].Rows[i]["CLIENTE"].ToString(),
                            SaldoCartera = decimal.Parse(ds.Tables[0].Rows[i]["SaldoCartera"].ToString()),
                            SaldoSinDemanda = decimal.Parse(ds.Tables[0].Rows[i]["SaldoSinDemanda"].ToString()),
                            PorSaldoSinDemanda = ds.Tables[0].Rows[i]["PorSaldoSinDemanda"].ToString(),
                            SaldoDemandado = decimal.Parse(ds.Tables[0].Rows[i]["SaldoDemandado"].ToString()),
                            PorSaldoDemandado = ds.Tables[0].Rows[i]["PorSaldoDemandado"].ToString(),
                            SaldoDemandadoDosAnios = decimal.Parse(ds.Tables[0].Rows[i]["SaldoDemandadoDosAnios"].ToString()),
                            PorSaldoDemandadoDosAnios = ds.Tables[0].Rows[i]["PorSaldoDemandadoDosAnios"].ToString(),
                            Row = Int32.Parse(ds.Tables[0].Rows[i]["row"].ToString())
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.ListarPanelMonitoreoExternoDemandas", 0);
                return lst;
            }
        }
        public static List<dto.MonitoreoExternoRolBuscado> ListarPanelMonitoreoExternoRol(int codemp, int zonaId, string where, string sidx, string sord)
        {
            List<dto.MonitoreoExternoRolBuscado> lst = new List<dto.MonitoreoExternoRolBuscado>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Panel_Monitoreo_Externo_RolBusqueda_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("zonaId", zonaId);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);

                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.MonitoreoExternoRolBuscado()
                        {
                            TribunalId = Int32.Parse(ds.Tables[0].Rows[i]["TribunalId"].ToString()),
                            Tribunal = ds.Tables[0].Rows[i]["Tribunal"].ToString(),
                            Anio = Int32.Parse(ds.Tables[0].Rows[i]["Anio"].ToString()),
                            MinRol = Int32.Parse(ds.Tables[0].Rows[i]["MinRol"].ToString()),
                            MaxRol = Int32.Parse(ds.Tables[0].Rows[i]["MaxRol"].ToString()),
                            Encontrados = Int32.Parse(ds.Tables[0].Rows[i]["Encontrados"].ToString()),
                            NoEncontrados = Int32.Parse(ds.Tables[0].Rows[i]["NoEncontrados"].ToString()),
                            Porcentaje = ds.Tables[0].Rows[i]["Porcentaje"].ToString(),
                            Row = Int32.Parse(ds.Tables[0].Rows[i]["row"].ToString())
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.ListarPanelMonitoreoExternoDemandas", 0);
                return lst;
            }
        }
        public static List<Combobox> ListarZonasTribunales(int codemp, string first)
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
                StoredProcedure sp = new StoredProcedure("_Listar_Zonas_Tribunales");
                sp.AgregarParametro("codemp", codemp);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Dimol.dto.Combobox()
                        {
                            Text = ds.Tables[0].Rows[i]["ZONA"].ToString(),
                            Value = ds.Tables[0].Rows[i]["ZONAID"].ToString()
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

        public static List<dto.MonitoreoSiiCabecera> ListarPanelMonitoreoSiiCabecera()
        {
            List<dto.MonitoreoSiiCabecera> lst = new List<dto.MonitoreoSiiCabecera>();
            DateTime fechaUtimaActualizacion = new DateTime();
            DateTime fechaProximaActualizacion = new DateTime();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Panel_Monitoreo_Sii_Cabecera");

                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecUtimaActualizacion"].ToString(), out fechaUtimaActualizacion);
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecProximaActualizacion"].ToString(), out fechaProximaActualizacion);
                        lst.Add(new dto.MonitoreoSiiCabecera()
                        {
                            Recolecto = ds.Tables[0].Rows[i]["Recolecto"].ToString(),
                            CantRut = Int32.Parse(ds.Tables[0].Rows[i]["CantRut"].ToString()),
                            CantMesActual = Int32.Parse(ds.Tables[0].Rows[i]["CantMesActual"].ToString()),
                            Acumulativas = Int32.Parse(ds.Tables[0].Rows[i]["Acumulativas"].ToString()),
                            FecUtimaActualizacion = fechaUtimaActualizacion,
                            FecProximaActualizacion = fechaProximaActualizacion
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.ListarPanelMonitoreoSiiCabecera", 0);
                return lst;
            }
        }
        public static List<dto.MonitoreoSiiCliente> ListarPanelMonitoreoSiiClientes(string where, string sidx, string sord)
        {
            List<dto.MonitoreoSiiCliente> lst = new List<dto.MonitoreoSiiCliente>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Panel_Monitoreo_SII_Clientes_Grilla");
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);

                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.MonitoreoSiiCliente()
                        {
                            Pclid = Int32.Parse(ds.Tables[0].Rows[i]["PCLID"].ToString()),
                            Cliente = ds.Tables[0].Rows[i]["CLIENTE"].ToString(),
                            SaldoCartera = decimal.Parse(ds.Tables[0].Rows[i]["SaldoCartera"].ToString()),
                            SaldoVerde = decimal.Parse(ds.Tables[0].Rows[i]["SaldoVerde"].ToString()),
                            PorSaldoVerde = ds.Tables[0].Rows[i]["PorSaldoVerde"].ToString(),
                            SaldoAmarillo = decimal.Parse(ds.Tables[0].Rows[i]["SaldoAmarillo"].ToString()),
                            PorSaldoAmarillo = ds.Tables[0].Rows[i]["PorSaldoAmarillo"].ToString(),
                            SaldoRojo = decimal.Parse(ds.Tables[0].Rows[i]["SaldoRojo"].ToString()),
                            PorSaldoRojo = ds.Tables[0].Rows[i]["PorSaldoRojo"].ToString(),
                            Row = Int32.Parse(ds.Tables[0].Rows[i]["row"].ToString())
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.ListarPanelMonitoreoSiiClientes", 0);
                return lst;
            }
        }
        public static List<dto.MonitoreoInternoCabecera> ListarPanelMonitoreoInternoCabecera()
        {
            List<dto.MonitoreoInternoCabecera> lst = new List<dto.MonitoreoInternoCabecera>();
            DateTime fechaUtimaActualizacion = new DateTime();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Panel_Monitoreo_Interno_Cabecera");

                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FechaActualizacion"].ToString(), out fechaUtimaActualizacion);
                        lst.Add(new dto.MonitoreoInternoCabecera()
                        {
                            CantCausasDiaAnterior = Int32.Parse(ds.Tables[0].Rows[i]["CantCausasDiaAnterior"].ToString()),
                            CantDeudoresDiaAnterior = Int32.Parse(ds.Tables[0].Rows[i]["CantDeudoresDiaAnterior"].ToString()),
                            SaldoDiaAnterior = Decimal.Parse(ds.Tables[0].Rows[i]["SaldoDiaAnterior"].ToString()),

                            CantDeudoresJudicializado = Int32.Parse(ds.Tables[0].Rows[i]["CantDeudoresJudicializado"].ToString()),
                            CantCausasJudicializadas = Int32.Parse(ds.Tables[0].Rows[i]["CantCausasJudicializadas"].ToString()),
                            SaldoJudicializado = Decimal.Parse(ds.Tables[0].Rows[i]["SaldoJudicializado"].ToString()),

                            CantDeudoresCausasActivas = Int32.Parse(ds.Tables[0].Rows[i]["CantDeudoresCausasActivas"].ToString()),
                            CantCausasActivas = Int32.Parse(ds.Tables[0].Rows[i]["CantCausasActivas"].ToString()),
                            SaldoCausaActiva = Decimal.Parse(ds.Tables[0].Rows[i]["SaldoCausaActiva"].ToString()),

                            CantDeudoresCausasArchivadas = Int32.Parse(ds.Tables[0].Rows[i]["CantDeudoresCausasArchivadas"].ToString()),
                            CantCausasArchivadas = Int32.Parse(ds.Tables[0].Rows[i]["CantCausasArchivadas"].ToString()),
                            SaldoCausaArchivada = Decimal.Parse(ds.Tables[0].Rows[i]["SaldoCausaArchivada"].ToString()),

                            CantDeudoresCausasArchivadas7dias = Int32.Parse(ds.Tables[0].Rows[i]["CantDeudoresCausasArchivadas7dias"].ToString()),
                            CantCausaArchivada7Dias = Int32.Parse(ds.Tables[0].Rows[i]["CantCausaArchivada7Dias"].ToString()),
                            SaldoCausaArchivada7Dias = Decimal.Parse(ds.Tables[0].Rows[i]["SaldoCausaArchivada7Dias"].ToString()),
                            FecUtimaActualizacion = fechaUtimaActualizacion
                           
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.ListarPanelMonitoreoInternoCabecera", 0);
                return lst;
            }
        }

        public static List<dto.MonitoreoInternoCliente> ListarPanelMonitoreoInternoClientes(string where, string sidx, string sord)
        {
            List<dto.MonitoreoInternoCliente> lst = new List<dto.MonitoreoInternoCliente>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Panel_Monitoreo_Interno_ClientesDemandas_Grilla");
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);

                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.MonitoreoInternoCliente()
                        {
                            Pclid = Int32.Parse(ds.Tables[0].Rows[i]["PCLID"].ToString()),
                            Cliente = ds.Tables[0].Rows[i]["CLIENTE"].ToString(),
                            TotalCausas = Int32.Parse(ds.Tables[0].Rows[i]["TotalCausas"].ToString()),
                            ACount = Int32.Parse(ds.Tables[0].Rows[i]["ACount"].ToString()),
                            BCount = Int32.Parse(ds.Tables[0].Rows[i]["BCount"].ToString()),
                            CCount = Int32.Parse(ds.Tables[0].Rows[i]["CCount"].ToString()),
                            DCount = Int32.Parse(ds.Tables[0].Rows[i]["DCount"].ToString()),
                            ActualizadasCount = Int32.Parse(ds.Tables[0].Rows[i]["ActualizadasCount"].ToString()),
                            NoActualizadasCount = Int32.Parse(ds.Tables[0].Rows[i]["NoActualizadasCount"].ToString()),
                            Porcentaje = ds.Tables[0].Rows[i]["Porcentaje"].ToString(),
                            Row = Int32.Parse(ds.Tables[0].Rows[i]["row"].ToString())
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.ListarPanelMonitoreoSiiClientes", 0);
                return lst;
            }
        }
    }
}
