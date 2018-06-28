using HiQPdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Transactions;
using System.Xml;
using System.Xml.Serialization;

namespace Dimol.Judicial.Mantenedores.bcp
{
    public class PanelDemanda
    {
        public static List<dto.PanelDemanda> ListarPanelDemandaInsertar(int codemp, int idioma, int pclid, int ctcid)
        {
            return dao.PanelDemanda.ListarPanelDemandaInsertar(codemp, idioma, pclid, ctcid);
        }
        public static List<dto.PanelDemandaDocumentos> ListarPanelDemandaDocumentosInsertar(int codemp, int idioma,
                                                                                           int pclid, int ctcid,
                                                                                           int sbcid, int tpcid)
        {
            return dao.PanelDemanda.ListarPanelDemandaDocumentosInsertar(codemp, idioma, pclid, ctcid, sbcid, tpcid);
        }
        public static int InsertarPanelDemandaOLD(int codemp, int idioma, int pclid, int ctcid, int user)
        {
            int panelId = -1;
            int procesoDocumento = -1;
            List<dto.PanelDemanda> lstpanel = dao.PanelDemanda.ListarPanelDemandaInsertar(codemp, idioma, pclid, ctcid);
            foreach (dto.PanelDemanda p in lstpanel)
            {
                //Ingreso panel Demanda Cabecera/ Llave principal
                panelId = dao.PanelDemanda.InsertarPanelDemanda(codemp, p.Pclid, p.Ctcid, p.Sbcid, p.Tpcid, user);
                if (panelId >= 0)
                {
                    List<dto.PanelDemandaDocumentos> lstpanelDocumentos = dao.PanelDemanda.ListarPanelDemandaDocumentosInsertar(codemp, idioma, pclid, ctcid, p.Sbcid, p.Tpcid);

                    foreach (dto.PanelDemandaDocumentos doc in lstpanelDocumentos)
                    {
                        //Ingreso panel Demanda los documentos del panel
                        procesoDocumento = dao.PanelDemanda.InsertarPanelDemandaDocumentos(panelId, codemp, p.Pclid, p.Ctcid, doc.Ccbid, user);
                    }
                }
            }

            return procesoDocumento;
        }
        public static int InsertarPanelDemanda(int codemp, int idioma, int pclid, int ctcid, int user, List<string> listaDocumentos)
        {
            int panelId = -1;
            int procesoDocumento = -1;
            //Ingreso panel Demanda Cabecera
            panelId = dao.PanelDemanda.InsertarPanelDemanda(codemp, pclid, ctcid, 0, 0, user);

            if (panelId >= 0)
            {
                foreach (string doc in listaDocumentos)
                {
                    //Ingreso panel Demanda los documentos del panel
                    procesoDocumento = dao.PanelDemanda.InsertarPanelDemandaDocumentos(panelId, codemp, pclid, ctcid, Int32.Parse(doc.Split('|')[2]), user);
                }
            }

            return procesoDocumento;
        }
        public static int InsertarPanelDemandaPrevisional(int codemp, int idioma, int pclid, int ctcid, int user, List<string> ListaResoluciones)
        {
            int panelId = -1;
            int procesoDocumento = -1;

            //Ingreso panel Demanda Cabecera
            panelId = dao.PanelDemanda.InsertarPanelDemandaPrevisional(codemp, pclid, ctcid, 0, 0, user);

            if (panelId >= 0)
            {
                foreach (var Resolucion in ListaResoluciones)
                {
                    List<dto.DocumentoRol>  DocumentosPorResolucíon = dao.Rol.ListarDocumentosPorNumeroResolucion(Resolucion);

                    foreach (var Doc in DocumentosPorResolucíon)
                    {
                        //Ingreso panel Demanda los documentos del panel
                        procesoDocumento = dao.PanelDemanda.InsertarPanelDemandaDocumentosPrevisional(panelId, codemp, pclid, ctcid, Doc.Ccbid, user);
                    }
                }
            }

            return procesoDocumento;
        }
        public static int ListarPanelDemandasCount(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.PanelDemanda.ListarPanelDemandasCount(codemp, where, sidx, sord, inicio, limite);
        }
        public static int ListarPanelDemandasPrevisionalCount(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.PanelDemanda.ListarPanelDemandasPrevisionalCount(codemp, where, sidx, sord, inicio, limite);
        }
        public static int ListarPanelDemandasMasivasCount(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.PanelDemanda.ListarPanelDemandasMasivasCount(codemp, where, sidx, sord, inicio, limite);
        }
        public static List<dto.PanelDemandaGet> ListarPanelDemandas(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.PanelDemanda.ListarPanelDemandas(codemp, where, sidx, sord, inicio, limite);
        }
        public static List<dto.PanelDemandaGet> ListarPanelDemandasPrevisionales(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.PanelDemanda.ListarPanelDemandasPrevisionales(codemp, where, sidx, sord, inicio, limite);
        }
        
        public static List<dto.PanelDemandaGet> ListarPanelDemandasMasivas(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.PanelDemanda.ListarPanelDemandasMasivas(codemp, where, sidx, sord, inicio, limite);
        }
        public static List<Dimol.dto.Autocomplete> BuscarNombreUsuario(string nombre, int codemp, int sucursal)
        {
            return dao.PanelDemanda.BuscarNombreUsuario(nombre, codemp, sucursal);
        }

        public static int GrabarAvanceDemanda(int panelId, int userEncargado, DateTime fecEnvio, DateTime? fecEntrega,
                                           DateTime? fecIngreso, string comentarios, int user, bool ingresarFechaEntreg)
        {
            int result = -1;
            result = dao.PanelDemanda.GrabarAvanceDemanda(panelId, userEncargado, fecEnvio, fecEntrega, fecIngreso, comentarios, user, ingresarFechaEntreg);

            return result;
        }

        public static int GrabarAvanceDemandaMasiva(int panelId, int userEncargado, DateTime fecEnvio, DateTime? fecEntrega,
                                          DateTime? fecIngreso, string comentarios, int user, bool ingresarFechaEntreg)
        {
            int result = -1;
            result = dao.PanelDemanda.GrabarAvanceDemandaMasiva(panelId, userEncargado, fecEnvio, fecEntrega, fecIngreso, comentarios, user, ingresarFechaEntreg);
            return result;
        }

        public static int GrabarAvanceDemandaPrevisional(int panelId, int userEncargado, DateTime fecEnvio, DateTime? fecEntrega,
                                           DateTime? fecIngreso, string comentarios, int user, bool ingresarFechaEntreg)
        {
            int result = -1;
            result = dao.PanelDemanda.GrabarAvanceDemandaPrevisional(panelId, userEncargado, fecEnvio, fecEntrega, fecIngreso, comentarios, user, ingresarFechaEntreg);
            return result;
        }

        public static List<Dimol.dto.Autocomplete> BuscarTribunal(string nombre, int codemp)
        {
            return dao.PanelDemanda.BuscarTribunal(nombre, codemp);
        }
        public static Tuple<bool, int> InsertarAvanceDemandaRol(dto.Rol objAccion, Dimol.dto.UserSession objSession, int materiaJudicial,
                                               int panelId, DateTime? fechaTribunales, string quiebraDeudor, bool flagEnviaFechaEntrega, DateTime? fechaEntrega)
        {
            bool error = false;
            int rolid = 0;
            string rolNumero = "0";
            int existRolId = 0;

            using (TransactionScope scope = new TransactionScope())
            {
                rolNumero = bcp.DeudorQuiebra.TraeDeudorQuiebraRolNumero(objSession.CodigoEmpresa, objAccion.ctc_rut);
                if (objAccion.rol_numero.Trim() == rolNumero.Trim())
                {
                    existRolId = bcp.DeudorQuiebra.TraeDeudorQuiebraIdRol(objSession.CodigoEmpresa, objAccion.rol_pclid, objAccion.rol_ctcid, rolNumero);
                }
                if (existRolId > 0)
                    rolid = existRolId;
                else
                    rolid = dao.Rol.InsertarRol(objAccion, objSession);

                error = rolid <= 0;
                if (!error)
                {
                    error = bcp.PanelDemanda.InsertarAvanceDemandaDocumentos(objSession.CodigoEmpresa, rolid, panelId, objAccion.rol_pclid, objAccion.rol_ctcid) <= 0;
                }
                if (!error)
                {
                    error = bcp.PanelDemanda.GrabarAvanceDemandaRol(panelId, fechaTribunales, objAccion.rol_numero, rolid, flagEnviaFechaEntrega, fechaEntrega, objSession.UserId) <= 0;
                }
                if (existRolId == 0)
                {
                    if (!error)
                    {
                        error = bcp.Rol.InsertarEstadoRol(objSession.CodigoEmpresa, rolid, 99, materiaJudicial, objSession.UserId, objSession.IpRed, objSession.IpPc, "INGRESA DEMANDA A TRIBUNAL", DateTime.Now.AddMinutes(50), objSession.CodigoSucursal, objSession.Gestor) <= 0;
                    }
                }

                if (!error)
                {
                    error = bcp.PanelDemanda.InsertarAvanceDemandaEntes(objSession.CodigoEmpresa, rolid, objAccion.rol_trbid) <= 0;
                }
                if (!error)
                {
                    error = bcp.Rol.ActualilzarQuiebraDeudor(objSession.CodigoEmpresa, objAccion.rol_ctcid, quiebraDeudor) <= 0;
                }
                if (!error)
                {
                    error = bcp.DeudorQuiebra.ActualizarDeudorQuiebra(objSession.CodigoEmpresa, objAccion.ctc_rut, objAccion.rol_tcaid.ToString(), materiaJudicial.ToString()) <= 0;
                }
                if (materiaJudicial == 3 || materiaJudicial == 9 || materiaJudicial == 10)
                {
                    if (!error)
                    {
                        error = bcp.PanelQuiebra.InsertarPanelQuiebra(objSession.CodigoEmpresa, rolid, objSession.UserId) <= 0;
                    }
                }
                if (!error)
                {
                    scope.Complete();
                }
            }

            return Tuple.Create((!error ? true : false), rolid);
        }

        public static Tuple<bool, int> InsertarAvanceDemandaRolPrevisional(dto.Rol objAccion, Dimol.dto.UserSession objSession, int materiaJudicial,
                                               int panelId, DateTime? fechaTribunales, string quiebraDeudor, bool flagEnviaFechaEntrega, DateTime? fechaEntrega)
        {
            bool error = false;
            int rolid = 0;
            string rolNumero = "0";
            int existRolId = 0;
            using (TransactionScope scope = new TransactionScope())
            {
                rolNumero = bcp.DeudorQuiebra.TraeDeudorQuiebraRolNumero(objSession.CodigoEmpresa, objAccion.ctc_rut);
                if (objAccion.rol_numero.Trim() == rolNumero.Trim())
                {
                    existRolId = bcp.DeudorQuiebra.TraeDeudorQuiebraIdRol(objSession.CodigoEmpresa, objAccion.rol_pclid, objAccion.rol_ctcid, rolNumero);
                }
                if (existRolId > 0)
                    rolid = existRolId;
                else
                    rolid = dao.Rol.InsertarRol(objAccion, objSession);

                error = rolid <= 0;
                if (!error)
                {
                    error = bcp.PanelDemanda.InsertarAvanceDemandaDocumentosPrevisional(objSession.CodigoEmpresa, rolid, panelId, objAccion.rol_pclid, objAccion.rol_ctcid) <= 0;
                }
                if (!error)
                {
                    error = bcp.PanelDemanda.GrabarAvanceDemandaRolPrevisional(panelId, fechaTribunales, objAccion.rol_numero, rolid, flagEnviaFechaEntrega, fechaEntrega, objSession.UserId) <= 0;
                }
                if (existRolId == 0)
                {
                    if (!error)
                    {
                        error = bcp.Rol.InsertarEstadoRol(objSession.CodigoEmpresa, rolid, 99, materiaJudicial, objSession.UserId, objSession.IpRed, objSession.IpPc, "INGRESA DEMANDA A TRIBUNAL", DateTime.Now.AddMinutes(50), objSession.CodigoSucursal, objSession.Gestor) <= 0;
                    }
                }

                if (!error)
                {
                    error = bcp.PanelDemanda.InsertarAvanceDemandaEntes(objSession.CodigoEmpresa, rolid, objAccion.rol_trbid) <= 0;
                }
                if (!error)
                {
                    error = bcp.Rol.ActualilzarQuiebraDeudor(objSession.CodigoEmpresa, objAccion.rol_ctcid, quiebraDeudor) <= 0;
                }
                if (!error)
                {
                    error = bcp.DeudorQuiebra.ActualizarDeudorQuiebra(objSession.CodigoEmpresa, objAccion.ctc_rut, objAccion.rol_tcaid.ToString(), materiaJudicial.ToString()) <= 0;
                }
                if (materiaJudicial == 3 || materiaJudicial == 9 || materiaJudicial == 10)
                {
                    if (!error)
                    {
                        error = bcp.PanelQuiebra.InsertarPanelQuiebra(objSession.CodigoEmpresa, rolid, objSession.UserId) <= 0;
                    }
                }
                if (!error)
                {
                    scope.Complete();
                }
            }

            return Tuple.Create((!error ? true : false), rolid);
        }

        public static Tuple<bool, int> InsertarAvanceDemandaMasivaRol(dto.Rol objAccion, Dimol.dto.UserSession objSession, int materiaJudicial,
                                                int panelId, DateTime? fechaTribunales, string quiebraDeudor, bool flagEnviaFechaEntrega, DateTime? fechaEntrega)
        {
            bool error = false;
            int rolid = 0;
            string rolNumero = "0";
            int existRolId = 0;

            using (TransactionScope scope = new TransactionScope())
            {
                rolNumero = bcp.DeudorQuiebra.TraeDeudorQuiebraRolNumero(objSession.CodigoEmpresa, objAccion.ctc_rut);
                if (objAccion.rol_numero.Trim() == rolNumero.Trim())
                {
                    existRolId = bcp.DeudorQuiebra.TraeDeudorQuiebraIdRol(objSession.CodigoEmpresa, objAccion.rol_pclid, objAccion.rol_ctcid, rolNumero);
                }
                if (existRolId > 0)
                    rolid = existRolId;
                else
                    rolid = dao.Rol.InsertarRol(objAccion, objSession);

                error = rolid <= 0;
                if (!error)
                {
                    error = bcp.PanelDemanda.InsertarAvanceDemandaMasivaDocumentos(objSession.CodigoEmpresa, rolid, panelId, objAccion.rol_pclid, objAccion.rol_ctcid) <= 0;
                }
                if (!error)
                {
                    error = bcp.PanelDemanda.InsertarAvenimientoDemandaPanelMasivo(objSession.CodigoEmpresa, rolid, panelId) <= 0;
                }
                if (!error)
                {
                    error = bcp.PanelDemanda.GrabarAvanceDemandaMasivaRol(panelId, fechaTribunales, objAccion.rol_numero, rolid, flagEnviaFechaEntrega, fechaEntrega, objSession.UserId) <= 0;
                }
                if (existRolId == 0)
                {
                    if (!error)
                    {
                        error = bcp.Rol.InsertarEstadoRol(objSession.CodigoEmpresa, rolid, 99, materiaJudicial, objSession.UserId, objSession.IpRed, objSession.IpPc, "INGRESA DEMANDA A TRIBUNAL", DateTime.Now.AddMinutes(50), objSession.CodigoSucursal, objSession.Gestor) <= 0;
                    }
                }

                if (!error)
                {
                    error = bcp.PanelDemanda.InsertarAvanceDemandaEntes(objSession.CodigoEmpresa, rolid, objAccion.rol_trbid) <= 0;
                }
                if (!error)
                {
                    error = bcp.Rol.ActualilzarQuiebraDeudor(objSession.CodigoEmpresa, objAccion.rol_ctcid, quiebraDeudor) <= 0;
                }
                if (!error)
                {
                    error = bcp.DeudorQuiebra.ActualizarDeudorQuiebra(objSession.CodigoEmpresa, objAccion.ctc_rut, objAccion.rol_tcaid.ToString(), materiaJudicial.ToString()) <= 0;
                }
                if (materiaJudicial == 3 || materiaJudicial == 9 || materiaJudicial == 10)
                {
                    if (!error)
                    {
                        error = bcp.PanelQuiebra.InsertarPanelQuiebra(objSession.CodigoEmpresa, rolid, objSession.UserId) <= 0;
                    }
                }
                if (!error)
                {
                    scope.Complete();
                }
            }

            return Tuple.Create((!error ? true : false), rolid);
        }

        public static int InsertarAvanceDemandaDocumentos(int codemp, int rolId, int panelId, int pclid, int ctcid)
        {
            int procesoDocumento = -1;
            List<dto.PanelDemandaDocumentoAsignar> lstDocumentos = dao.PanelDemanda.ListarPanelDemandaDocumentoAsignar(codemp, panelId, pclid, ctcid);
            foreach (dto.PanelDemandaDocumentoAsignar doc in lstDocumentos)
            {
                procesoDocumento = bcp.Rol.InsertarDocumentosRol(codemp, rolId, pclid, ctcid, doc.Ccbid, doc.Monto, doc.Saldo);
            }

            return procesoDocumento;
        }

        public static int InsertarAvanceDemandaDocumentosPrevisional(int codemp, int rolId, int panelId, int pclid, int ctcid)
        {
            int procesoDocumento = -1;
            List<dto.PanelDemandaDocumentoAsignar> lstDocumentos = dao.PanelDemanda.ListarPanelDemandaDocumentoAsignarPrevisional(codemp, panelId, pclid, ctcid);

            foreach (dto.PanelDemandaDocumentoAsignar doc in lstDocumentos)
            {
                procesoDocumento = bcp.Rol.InsertarDocumentosRol(codemp, rolId, pclid, ctcid, doc.Ccbid, doc.Monto, doc.Saldo);
            }

            return procesoDocumento;
        }

        public static int InsertarAvanceDemandaMasivaDocumentos(int codemp, int rolId, int panelId, int pclid, int ctcid)
        {
            int procesoDocumento = -1;
            List<dto.PanelDemandaDocumentoAsignar> lstDocumentos = dao.PanelDemanda.ListarPanelDemandaMasivaDocumentoAsignar(codemp, panelId, pclid, ctcid);
            foreach (dto.PanelDemandaDocumentoAsignar doc in lstDocumentos)
            {
                procesoDocumento = bcp.Rol.InsertarDocumentosRol(codemp, rolId, pclid, ctcid, doc.Ccbid, doc.Monto, doc.Saldo);
            }
            return procesoDocumento;

        }

        public static int InsertarAvenimientoDemandaPanelMasivo(int codemp, int rolId, int panelId)
        {
            int result = -1;
            result = dao.PanelDemanda.InsertarAvenimientoDemandaPanelMasivo(codemp, rolId, panelId);
            return result;
        }

        public static int GrabarAvanceDemandaRol(int panelId, DateTime? fecIngreso, string rolAdjudicado, int rolId, bool flagEnviaFechaEntrega, DateTime? fechaEntrega, int user)
        {
            int result = -1;
            result = dao.PanelDemanda.GrabarAvanceDemandaRol(panelId, fecIngreso, rolAdjudicado, rolId, flagEnviaFechaEntrega, fechaEntrega, user);
            return result;
        }

        public static int GrabarAvanceDemandaRolPrevisional(int panelId, DateTime? fecIngreso, string rolAdjudicado, int rolId, bool flagEnviaFechaEntrega, DateTime? fechaEntrega, int user)
        {
            int result = -1;
            result = dao.PanelDemanda.GrabarAvanceDemandaRolPrevisional(panelId, fecIngreso, rolAdjudicado, rolId, flagEnviaFechaEntrega, fechaEntrega, user);
            return result;
        }

        public static int GrabarAvanceDemandaMasivaRol(int panelId, DateTime? fecIngreso, string rolAdjudicado, int rolId, bool flagEnviaFechaEntrega, DateTime? fechaEntrega, int user)
        {
            int result = -1;
            result = dao.PanelDemanda.GrabarAvanceDemandaMasivaRol(panelId, fecIngreso, rolAdjudicado, rolId, flagEnviaFechaEntrega, fechaEntrega, user);
            return result;
        }

        public static int InsertarAvanceDemandaEntes(int codemp, int rolId, int tribunalId)
        {
            int procesoEnte = -1;
            List<dto.PanelDemandaEntesAsignar> lstEntes = dao.PanelDemanda.ListarPanelDemandaEntesAsignar(codemp, tribunalId);
            foreach (dto.PanelDemandaEntesAsignar ente in lstEntes)
            {
                procesoEnte = bcp.EnteJudicial.InsertarEnteJudicialRol(codemp, ente.Etjid, rolId);
            }
            return procesoEnte;

        }
        public static List<dto.OrgChartPanelDemanda> ListarPanelDemandaControlGestion(int codemp)
        {
            return dao.PanelDemanda.ListarPanelDemandaControlGestion(codemp);
        }

        public static List<dto.OrgChartPanelDemanda> ListarPanelDemandaControlGestionMasivas(int codemp)
        {
            return dao.PanelDemanda.ListarPanelDemandaControlGestionMasivas(codemp);
        }
        public static int ListarPanelDemandaReporteCount(int tipoReporte, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.PanelDemanda.ListarPanelDemandaReporteCount(tipoReporte, where, sidx, sord, inicio, limite);
        }
        public static List<dto.PanelDemandaReporte> ListarPanelDemandaReporte(int tipoReporte, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.PanelDemanda.ListarPanelDemandaReporte(tipoReporte, where, sidx, sord, inicio, limite);
        }
        public static List<dto.PanelDemandaReporte> ListarPanelDemandaReporteOrgChartItem(int tipoReporte)
        {
            return dao.PanelDemanda.ListarPanelDemandaReporteOrgChartItem(tipoReporte);
        }

        public static List<dto.PanelDemandaReporte> ListarPanelDemandaMasivaReporteOrgChartItem(int tipoReporte)
        {
            return dao.PanelDemanda.ListarPanelDemandaMasivaReporteOrgChartItem(tipoReporte);
        }

        public static List<dto.PanelDemandaExcel> ListarPanelDemandasExcel(int codemp)
        {
            return dao.PanelDemanda.ListarPanelDemandasExcel(codemp);
        }
        public static List<dto.PanelDemandaMasivaExcel> ListarPanelDemandasExcelMasiva(int codemp)
        {
            return dao.PanelDemanda.ListarPanelDemandasExcelMasiva(codemp);
        }
        public static List<dto.PanelDemandaExcel> ListarPanelDemandasExcelPrevisional(int codemp)
        {
            return dao.PanelDemanda.ListarPanelDemandasExcelPrevisional(codemp);
        }
        
        public static int GrabarCursoDemanda(int codemp, int panelId, int cursoDemanda, string motivo, int user)
        {
            int result = -1;
            result = dao.PanelDemanda.GrabarCursoDemanda(codemp, panelId, cursoDemanda, motivo, user);
            return result;
        }
        public static int GrabarCursoDemandaPrevisional(int codemp, int panelId, int cursoDemanda, string motivo, int user)
        {
            int result = -1;
            result = dao.PanelDemanda.GrabarCursoDemandaPrevisional(codemp, panelId, cursoDemanda, motivo, user);
            return result;
        }

        public static int GrabarCursoDemandaMasiva(int codemp, int panelId, int cursoDemanda, string motivo, int user)
        {
            int result = -1;
            result = dao.PanelDemanda.GrabarCursoDemandaMasiva(codemp, panelId, cursoDemanda, motivo, user);
            return result;
        }

        public static List<dto.DocumentosPanel> ListarDocumentosPanelId(int codemp, int panelId, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.PanelDemanda.ListarDocumentosPanelId(codemp, panelId, where, sidx, sord, inicio, limite);
        }
        public static List<dto.DocumentosPanel> ListarDocumentosPanelPrevisionalId(int codemp, int panelId, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.PanelDemanda.ListarDocumentosPanelPrevisionalId(codemp, panelId, where, sidx, sord, inicio, limite);
        }
        public static List<dto.DocumentosPanel> ListarDocumentosMasivosPanelId(int codemp, int panelId, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.PanelDemanda.ListarDocumentosMasivosPanelId(codemp, panelId, where, sidx, sord, inicio, limite);
        }

        public static byte[] ExportToPDFDemandaMasiva(int codemp, int IdDM, string Template)
        {
            try
            {
                return bcp.PanelDemandaMasiva.GeneraPDFPorHtmlLegal(Template);
            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.bcp.PanelDemanda.ExportToPDFDemandaMasiva", IdDM);
                throw ex;
            }
        }

        public static string TransformXSLToHTML(object data, string template)
        {
            try
            {
                XmlSerializer xs = new XmlSerializer(data.GetType());
                string xmlString;
                string strXSLT = template;
                using (StringWriter swr = new StringWriter())
                {
                    xs.Serialize(swr, data);
                    xmlString = swr.ToString();
                }

                var xd = new XmlDocument();
                xd.LoadXml(xmlString);

                var xslt = new System.Xml.Xsl.XslCompiledTransform();
                xslt.Load(new XmlTextReader(new StringReader(strXSLT)));
                var stm = new MemoryStream();
                xslt.Transform(xd, null, stm);
                stm.Position = 0;
                var sr = new StreamReader(stm);
                //xtr.Close();
                return sr.ReadToEnd();

                //System.IO.File.WriteAllText(output, sr.ReadToEnd());
            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.bcp.TransformXSLToHTML", 0);
                throw ex;
            }
        }

        public static void GeneraPDFSII(string html, string ruta, string fileName)
        {
            try
            {
                if (html != "")
                {
                    // create an empty PDF document
                    PdfDocument document = new PdfDocument();
                    // set a demo serial number
                    document.SerialNumber = "5q6Pt7aC-gKqPhJSH-lJ/X1sjW-xtfG1Mbf-397G1dfI-19TI39/f-3w==";
                    document.Pages.Remove(0);
                    PdfDocument docPorRut = new PdfDocument();
                    docPorRut.SerialNumber = "5q6Pt7aC-gKqPhJSH-lJ/X1sjW-xtfG1Mbf-397G1dfI-19TI39/f-3w==";

                    // layout the HTML from URL 1
                    System.Drawing.PointF location1 = System.Drawing.PointF.Empty;
                    PdfHtml html1 = new PdfHtml(location1.X, location1.Y, html, "");

                    PdfPage page1 = document.AddPage(PdfPageSize.Letter, new PdfDocumentMargins(2), PdfPageOrientation.Portrait);
                    html1.WaitBeforeConvert = 2;
                    PdfLayoutInfo html1LayoutInfo = page1.Layout(html1);

                    docPorRut.AddPageAtIndex(docPorRut.Pages.Count, page1);

                    // write the PDF document to a memory buffer
                    byte[] pdfBufferRut = docPorRut.WriteToMemory();

                    var rutFile = Path.Combine(ruta, fileName);
                    if (System.IO.File.Exists(rutFile))
                    {
                        File.Delete(rutFile);

                    }
                    System.IO.File.WriteAllBytes(rutFile, pdfBufferRut);
                    docPorRut.Close();
                    document.Close();
                }
            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.bcp.PanelDemanda.GeneraPDFSII", 0);
                throw ex;
            }
            //return salida;
        }

        public static bool EliminarPanelDemanda(int IdPanelDemanda)
        {
            return dao.PanelDemanda.EliminarPanelDemanda(IdPanelDemanda);
        }
        public static bool EliminarPanelDemandaPrevisional(int IdPanelDemanda)
        {
            return dao.PanelDemanda.EliminarPanelDemandaPrevisional(IdPanelDemanda);
        }
    }
}