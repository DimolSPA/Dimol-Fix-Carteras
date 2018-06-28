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
    public class PanelQuiebra
    {
        public static List<dto.LiquidacionRol> ListarRolLiquidacionGrilla(int codemp, int rolId, string where, string sidx, string sord)
        {
            List<dto.LiquidacionRol> lst = new List<dto.LiquidacionRol>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Panel_Quiebra_Rol_Liquidacion_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("rolId", rolId);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                
                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.LiquidacionRol()
                        {
                            Quiebra_Id = Int32.Parse(ds.Tables[0].Rows[i]["QUIEBRA_ID"].ToString()),
                            RolId = Int32.Parse(ds.Tables[0].Rows[i]["ROLID"].ToString()),
                            Rol = ds.Tables[0].Rows[i]["Rol"].ToString(),
                            Nombre = ds.Tables[0].Rows[i]["Nombre"].ToString(),
                            Cuantia = string.IsNullOrEmpty(ds.Tables[0].Rows[i]["Cuantia"].ToString()) ? 0 : decimal.Parse(ds.Tables[0].Rows[i]["Cuantia"].ToString()),
                            Materia = string.IsNullOrEmpty(ds.Tables[0].Rows[i]["Materia"].ToString()) ? 0 : Int32.Parse(ds.Tables[0].Rows[i]["Materia"].ToString()),
                            row = Int32.Parse(ds.Tables[0].Rows[i]["row"].ToString())

                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.ListarRolLiquidacionGrilla", 0);
                return lst;
            }
        }

        public static dto.PanelQuiebraAvance BuscarDetalleQuiebra(int quiebraId)
        {
            dto.PanelQuiebraAvance obj = new dto.PanelQuiebraAvance();

            DateTime fechaCostasPersonales = new DateTime();
            DateTime fechaIngresoSolicitud = new DateTime();
            DateTime fechaNotificacionSolicitud = new DateTime();
            DateTime fechaAudienciaInicial = new DateTime();
            DateTime fechaAudienciaPrueba = new DateTime();
            DateTime fechaAudienciaFallo = new DateTime();
            DateTime fechaResolucionLiquidacion = new DateTime();
            DateTime fechaResolucionLiquidacionBC = new DateTime();
            DateTime fechaResolucionReorganizacionBC = new DateTime();
            DateTime fechaVerificacion = new DateTime();
            DateTime fechaAcreditacionPoder = new DateTime();
            DateTime fechaJuntaConstitutiva = new DateTime();
            DateTime fechaJuntaDeliberativa = new DateTime();
            DateTime fechaAcuerdo = new DateTime();
            DateTime fechaVerificadoAcreditado = new DateTime();
            DateTime fechaNomCreditoVerificado = new DateTime();
            DateTime fechaImpugnacion = new DateTime();
            DateTime fechaNomCreditoReconocido = new DateTime();
            DateTime fechaSolicitudAntecedente = new DateTime();
            DateTime fechaRecepcionAntecedente = new DateTime();
            DateTime fechaEnvioAntecedente = new DateTime();
            DateTime fechaEmisionND = new DateTime();
            DateTime fechaRepartos = new DateTime();
            DateTime fechaDevolucion = new DateTime();
            DateTime fechaPgoCostasPersonales = new DateTime();
            DateTime fechaAprobacionCtaFinal = new DateTime();
            DateTime fechaCertificadoIncobrable = new DateTime();
        
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Panel_Quiebra_Rol_Detalle");
                sp.AgregarParametro("QuiebraId", quiebraId);
               
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecCostasPersonales"].ToString(), out fechaCostasPersonales);
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecIngresoSolicitud"].ToString(), out fechaIngresoSolicitud);
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecNotificacionSolicitud"].ToString(), out fechaNotificacionSolicitud);
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecAudienciaInicial"].ToString(), out fechaAudienciaInicial);
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecAudienciaPrueba"].ToString(), out fechaAudienciaPrueba);
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecAudienciaFallo"].ToString(), out fechaAudienciaFallo);
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecResolucionLiquidacion"].ToString(), out fechaResolucionLiquidacion);
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecResolucionLiquidacionBC"].ToString(), out fechaResolucionLiquidacionBC);
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecResolucionReorganizacionBC"].ToString(), out fechaResolucionReorganizacionBC);
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecVerificacion"].ToString(), out fechaVerificacion);
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecAcreditacionPoder"].ToString(), out fechaAcreditacionPoder);
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecJuntaConstitutiva"].ToString(), out fechaJuntaConstitutiva);
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecJuntaDeliberativa"].ToString(), out fechaJuntaDeliberativa);
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecAcuerdo"].ToString(), out fechaAcuerdo);
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecVerificadoAcreditado"].ToString(), out fechaVerificadoAcreditado);
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecNomCreditoVerificado"].ToString(), out fechaNomCreditoVerificado);
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecImpugnacion"].ToString(), out fechaImpugnacion);
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecNomCreditoReconocido"].ToString(), out fechaNomCreditoReconocido);
                        
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecSolicitudAntecedente"].ToString(), out fechaSolicitudAntecedente);
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecRecepcionAntecedente"].ToString(), out fechaRecepcionAntecedente);
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecEnvioAntecedente"].ToString(), out fechaEnvioAntecedente);
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecEmisionND"].ToString(), out fechaEmisionND);
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecRepartos"].ToString(), out fechaRepartos);
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecDevolucion"].ToString(), out fechaDevolucion);
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecPgoCostasPersonales"].ToString(), out fechaPgoCostasPersonales);
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecAprobacionCtaFinal"].ToString(), out fechaAprobacionCtaFinal);
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecCertificadoIncobrable"].ToString(), out fechaCertificadoIncobrable);
    
                        obj.QuiebraId = Int32.Parse(ds.Tables[0].Rows[i]["QuiebraId"].ToString());
                        obj.Solicitante = ds.Tables[0].Rows[i]["Solicitante"].ToString();
                        obj.MtoCostasPersonales = string.IsNullOrEmpty(ds.Tables[0].Rows[i]["MtoCostasPersonales"].ToString()) ? 0: decimal.Parse(ds.Tables[0].Rows[i]["MtoCostasPersonales"].ToString());
                        obj.FecCostasPersonales = fechaCostasPersonales;
                        obj.FecIngresoSolicitud = fechaIngresoSolicitud;
                        obj.FecNotificacionSolicitud = fechaNotificacionSolicitud;
                        obj.FecAudienciaInicial = fechaAudienciaInicial;
                        obj.FecAudienciaPrueba = fechaAudienciaPrueba;
                        obj.FecAudienciaFallo = fechaAudienciaFallo;
                        obj.FecResolucionLiquidacion = fechaResolucionLiquidacion;
                        obj.FecResolucionLiquidacionBC = fechaResolucionLiquidacionBC;
                        obj.FecResolucionReorganizacionBC = fechaResolucionReorganizacionBC;
                        obj.FecVerificacion = fechaVerificacion;
                        obj.FecAcreditacionPoder = fechaAcreditacionPoder;
                        obj.FecJuntaConstitutiva = fechaJuntaConstitutiva;
                        obj.FecJuntaDeliberativa = fechaJuntaDeliberativa;
                        obj.StatusAcuerdo = ds.Tables[0].Rows[i]["StatusAcuerdo"].ToString();
                        obj.FecAcuerdo = fechaAcuerdo;
                        obj.FecVerificadoAcreditado = fechaVerificadoAcreditado;
                        obj.FecNomCreditoVerificado = fechaNomCreditoVerificado;
                        obj.FecImpugnacion = fechaImpugnacion;
                        obj.FecNomCreditoReconocido = fechaNomCreditoReconocido;
                        obj.FecSolicitudAntecedente = fechaSolicitudAntecedente;
                        obj.FecRecepcionAntecedente = fechaRecepcionAntecedente;
                        obj.FecEnvioAntecedente = fechaEnvioAntecedente;
                        obj.FecEmisionND = fechaEmisionND;
                        obj.MtoEmision = string.IsNullOrEmpty(ds.Tables[0].Rows[i]["MtoEmision"].ToString()) ? 0 : decimal.Parse(ds.Tables[0].Rows[i]["MtoEmision"].ToString());
                        obj.FecRepartos = fechaRepartos;
                        obj.MtoRepartos = string.IsNullOrEmpty (ds.Tables[0].Rows[i]["MtoRepartos"].ToString()) ? 0: decimal.Parse(ds.Tables[0].Rows[i]["MtoRepartos"].ToString());
                        obj.FecDevolucion = fechaDevolucion;
                        obj.PgoCostasPersonales = string.IsNullOrEmpty(ds.Tables[0].Rows[i]["PgoCostasPersonales"].ToString()) ? 0: decimal.Parse(ds.Tables[0].Rows[i]["PgoCostasPersonales"].ToString());
                        obj.FecPgoCostasPersonales = fechaPgoCostasPersonales;
                        obj.FecAprobacionCtaFinal = fechaAprobacionCtaFinal;
                        obj.FecCertificadoIncobrable = fechaCertificadoIncobrable;
                    }
                }

                return obj;
            }
            catch (Exception ex)
            {
                return obj;
            }
        }
  


        public static int InsertUpdateAvancePanelQuiebra(int quiebraId, string solicitante, string mtoCostasPersonales, string fecCostasPersonales, string fecIngrSolicitud, string fecNotSolicitud,
                                                        string fecAudienciaIni, string fecAudienciaPrueba, string fecAudienciaFallo, string fecResolLiqui, string fecResolLiquiBC,
                                                        string fecResolReorgBC, string fecVerificacion, string fecAcreditaPoder, string fecJuntaConsti, string fecJuntaDelibe, 
                                                        string statusAcuerdo, string fecAcuerdo, string fecVerificaAcredita, string fecNomCreditoVeri, string fecImpugnacion, string fecNomCreditoRec,
                                                        string fecSolAntecedente, string fecRecepAntecedente, string fecEnvAntecedente, string fecEmisionND, string mtoEmisionND, 
                                                        string fecRepartos, string MtoRepartos, string fecDevolucion, string pgoCostPersonales, string fecpgoCostPersonales,
                                                        string fecAprobCtaFinal, string fecCertiIncobrable, int user)
        {
            int id = -1;

            try
            {
                
                Funciones func = new Funciones();
                StoredProcedure sp = new StoredProcedure("_Guardar_Panel_Quiebra_Rol_Detalle");
                sp.AgregarParametro("quiebraId", quiebraId);
                sp.AgregarParametro("solicitante", (object)solicitante ?? DBNull.Value);
                sp.AgregarParametro("mtoCostasPersonales", string.IsNullOrEmpty(mtoCostasPersonales) ? DBNull.Value : (object)Decimal.Parse(mtoCostasPersonales));
                sp.AgregarParametro("fecCostasPersonales", string.IsNullOrEmpty(fecCostasPersonales) ? DBNull.Value : (object)DateTime.Parse(fecCostasPersonales));
                sp.AgregarParametro("fecIngrSolicitud", string.IsNullOrEmpty(fecIngrSolicitud) ? DBNull.Value : (object)DateTime.Parse(fecIngrSolicitud));
                sp.AgregarParametro("fecNotSolicitud", string.IsNullOrEmpty(fecNotSolicitud) ? DBNull.Value : (object)DateTime.Parse(fecNotSolicitud));
                sp.AgregarParametro("fecAudienciaIni", string.IsNullOrEmpty(fecAudienciaIni) ? DBNull.Value : (object)DateTime.Parse(fecAudienciaIni));
                sp.AgregarParametro("fecAudienciaPrueba", string.IsNullOrEmpty(fecAudienciaPrueba) ? DBNull.Value : (object)DateTime.Parse(fecAudienciaPrueba));
                sp.AgregarParametro("fecAudienciaFallo", string.IsNullOrEmpty(fecAudienciaFallo) ? DBNull.Value : (object)DateTime.Parse(fecAudienciaFallo));
                sp.AgregarParametro("fecResolLiqui", string.IsNullOrEmpty(fecResolLiqui) ? DBNull.Value : (object)DateTime.Parse(fecResolLiqui));
                sp.AgregarParametro("fecResolLiquiBC", string.IsNullOrEmpty(fecResolLiquiBC) ? DBNull.Value : (object)DateTime.Parse(fecResolLiquiBC));
                sp.AgregarParametro("fecResolReorgBC", string.IsNullOrEmpty(fecResolReorgBC) ? DBNull.Value : (object)DateTime.Parse(fecResolReorgBC));
                sp.AgregarParametro("fecVerificacion", string.IsNullOrEmpty(fecVerificacion) ? DBNull.Value : (object)DateTime.Parse(fecVerificacion));
                sp.AgregarParametro("fecAcreditaPoder", string.IsNullOrEmpty(fecAcreditaPoder) ? DBNull.Value : (object)DateTime.Parse(fecAcreditaPoder));
                sp.AgregarParametro("fecJuntaConsti", string.IsNullOrEmpty(fecJuntaConsti) ? DBNull.Value : (object)DateTime.Parse(fecJuntaConsti));
                sp.AgregarParametro("fecJuntaDelibe", string.IsNullOrEmpty(fecJuntaDelibe) ? DBNull.Value : (object)DateTime.Parse(fecJuntaDelibe));
                sp.AgregarParametro("statusAcuerdo", string.IsNullOrEmpty(statusAcuerdo) ? DBNull.Value : (object)statusAcuerdo);
                sp.AgregarParametro("fecAcuerdo", string.IsNullOrEmpty(fecAcuerdo) ? DBNull.Value : (object)DateTime.Parse(fecAcuerdo));
                sp.AgregarParametro("fecVerificaAcredita", string.IsNullOrEmpty(fecVerificaAcredita) ? DBNull.Value : (object)DateTime.Parse(fecVerificaAcredita));
                sp.AgregarParametro("fecNomCreditoVeri", string.IsNullOrEmpty(fecNomCreditoVeri) ? DBNull.Value : (object)DateTime.Parse(fecNomCreditoVeri));            
                
                sp.AgregarParametro("fecImpugnacion", string.IsNullOrEmpty(fecImpugnacion) ? DBNull.Value : (object)DateTime.Parse(fecImpugnacion));   
                sp.AgregarParametro("fecNomCreditoRec", string.IsNullOrEmpty(fecNomCreditoRec) ? DBNull.Value : (object)DateTime.Parse(fecNomCreditoRec));   
                sp.AgregarParametro("fecSolAntecedente", string.IsNullOrEmpty(fecSolAntecedente) ? DBNull.Value : (object)DateTime.Parse(fecSolAntecedente));   
                sp.AgregarParametro("fecRecepAntecedente", string.IsNullOrEmpty(fecRecepAntecedente) ? DBNull.Value : (object)DateTime.Parse(fecRecepAntecedente));   
                sp.AgregarParametro("fecEnvAntecedente", string.IsNullOrEmpty(fecEnvAntecedente) ? DBNull.Value : (object)DateTime.Parse(fecEnvAntecedente));  
 
                sp.AgregarParametro("fecEmisionND", string.IsNullOrEmpty(fecEmisionND) ? DBNull.Value : (object)DateTime.Parse(fecEmisionND)); 
                sp.AgregarParametro("mtoEmisionND", string.IsNullOrEmpty(mtoEmisionND) ? DBNull.Value : (object)Decimal.Parse(mtoEmisionND)); 
                sp.AgregarParametro("fecRepartos", string.IsNullOrEmpty(fecRepartos) ? DBNull.Value : (object)DateTime.Parse(fecRepartos)); 
                sp.AgregarParametro("MtoRepartos", string.IsNullOrEmpty(MtoRepartos) ? DBNull.Value : (object)Decimal.Parse(MtoRepartos)); 
                sp.AgregarParametro("fecDevolucion", string.IsNullOrEmpty(fecDevolucion) ? DBNull.Value : (object)DateTime.Parse(fecDevolucion)); 
                sp.AgregarParametro("pgoCostPersonales", string.IsNullOrEmpty(pgoCostPersonales) ? DBNull.Value : (object)Decimal.Parse(pgoCostPersonales)); 
                sp.AgregarParametro("fecpgoCostPersonales", string.IsNullOrEmpty(fecpgoCostPersonales) ? DBNull.Value : (object)DateTime.Parse(fecpgoCostPersonales)); 
                sp.AgregarParametro("fecAprobCtaFinal", string.IsNullOrEmpty(fecAprobCtaFinal) ? DBNull.Value : (object)DateTime.Parse(fecAprobCtaFinal)); 
                sp.AgregarParametro("fecCertiIncobrable", string.IsNullOrEmpty(fecCertiIncobrable) ? DBNull.Value : (object)DateTime.Parse(fecCertiIncobrable)); 

                sp.AgregarParametro("userId", user);
                
                id = sp.EjecutarProcedimientoTrans();
               
            }
            catch (Exception ex)
            {
                return id;
            }
            return id;
        }
        
        public static List<dto.PanelQuiebra> ListarPanelQuiebraGrilla(int codemp, int idioma,
                                                                      string where, string sidx, string sord)
        {
            List<dto.PanelQuiebra> lst = new List<dto.PanelQuiebra>();
            DateTime fechaIngresoQuiebra = new DateTime();
            DateTime fechaCostasPersonales = new DateTime();
            DateTime fechaIngresoSolicitud = new DateTime();
            DateTime fechaNotificacionSolicitud = new DateTime();
            DateTime fechaAudienciaInicial = new DateTime();
            DateTime fechaAudienciaPrueba = new DateTime();
            DateTime fechaAudienciaFallo = new DateTime();
            DateTime fechaResolucionLiquidacion = new DateTime();
            DateTime fechaResolucionLiquidacionBC = new DateTime();
            DateTime fechaResolucionReorganizacionBC = new DateTime();
            DateTime fechaVerificacion = new DateTime();
            DateTime fechaAcreditacionPoder = new DateTime();
            DateTime fechaJuntaConstitutiva = new DateTime();
            DateTime fechaJuntaDeliberativa = new DateTime();
            DateTime fechaAcuerdo = new DateTime();
            DateTime fechaVerificadoAcreditado = new DateTime();
            DateTime fechaNomCreditoVerificado = new DateTime();
            DateTime fechaImpugnacion = new DateTime();
            DateTime fechaNomCreditoReconocido = new DateTime();
            DateTime fechaSolicitudAntecedente = new DateTime();
            DateTime fechaRecepcionAntecedente = new DateTime();
            DateTime fechaEnvioAntecedente = new DateTime();
            DateTime fechaEmisionND = new DateTime();
            DateTime fechaRepartos = new DateTime();
            DateTime fechaDevolucion = new DateTime();
            DateTime fechaPgoCostasPersonales = new DateTime();
            DateTime fechaAprobacionCtaFinal = new DateTime();
            DateTime fechaCertificadoIncobrable = new DateTime();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_PanelQuiebra_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idioma", idioma);
     
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
   
                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FechaIngresoQuiebra"].ToString(), out fechaIngresoQuiebra);
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecCostasPersonales"].ToString(), out fechaCostasPersonales);
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecIngresoSolicitud"].ToString(), out fechaIngresoSolicitud);
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecNotificacionSolicitud"].ToString(), out fechaNotificacionSolicitud);
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecAudienciaInicial"].ToString(), out fechaAudienciaInicial);
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecAudienciaPrueba"].ToString(), out fechaAudienciaPrueba);
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecAudienciaFallo"].ToString(), out fechaAudienciaFallo);
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecResolucionLiquidacion"].ToString(), out fechaResolucionLiquidacion);
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecResolucionLiquidacionBC"].ToString(), out fechaResolucionLiquidacionBC);
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecResolucionReorganizacionBC"].ToString(), out fechaResolucionReorganizacionBC);
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecVerificacion"].ToString(), out fechaVerificacion);
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecAcreditacionPoder"].ToString(), out fechaAcreditacionPoder);
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecJuntaConstitutiva"].ToString(), out fechaJuntaConstitutiva);
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecJuntaDeliberativa"].ToString(), out fechaJuntaDeliberativa);
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecAcuerdo"].ToString(), out fechaAcuerdo);
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecVerificadoAcreditado"].ToString(), out fechaVerificadoAcreditado);
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecNomCreditoVerificado"].ToString(), out fechaNomCreditoVerificado);
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecImpugnacion"].ToString(), out fechaImpugnacion);
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecNomCreditoReconocido"].ToString(), out fechaNomCreditoReconocido);
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecSolicitudAntecedente"].ToString(), out fechaSolicitudAntecedente);
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecRecepcionAntecedente"].ToString(), out fechaRecepcionAntecedente);
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecEnvioAntecedente"].ToString(), out fechaEnvioAntecedente);
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecEmisionND"].ToString(), out fechaEmisionND);
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecRepartos"].ToString(), out fechaRepartos);
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecDevolucion"].ToString(), out fechaDevolucion);
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecPgoCostasPersonales"].ToString(), out fechaPgoCostasPersonales);
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecAprobacionCtaFinal"].ToString(), out fechaAprobacionCtaFinal);
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecCertificadoIncobrable"].ToString(), out fechaCertificadoIncobrable);
                        lst.Add(new dto.PanelQuiebra()
                        {
                           QuiebraId = Int32.Parse(ds.Tables[0].Rows[i]["QuiebraId"].ToString()),
                           RolId = Int32.Parse(ds.Tables[0].Rows[i]["ROLID"].ToString()),
                           NombreTribunal = String.IsNullOrEmpty(ds.Tables[0].Rows[i]["Tribunal"].ToString()) ? "" : ds.Tables[0].Rows[i]["Tribunal"].ToString(),
                           Sbcid = string.IsNullOrEmpty(ds.Tables[0].Rows[i]["SBCID"].ToString()) ? 0 : Int32.Parse(ds.Tables[0].Rows[i]["SBCID"].ToString()),
                           RolNumero = ds.Tables[0].Rows[i]["ROLNUMERO"].ToString(),
                           Asegurado = ds.Tables[0].Rows[i]["Asegurado"].ToString(),
                           Cliente = ds.Tables[0].Rows[i]["Cliente"].ToString(),
                           RutDeudor = String.IsNullOrEmpty(ds.Tables[0].Rows[i]["RUT_Deudor"].ToString()) ? "" : ds.Tables[0].Rows[i]["RUT_Deudor"].ToString(),
                           Deudor = ds.Tables[0].Rows[i]["Deudor"].ToString(),
                           Cuantia = string.IsNullOrEmpty(ds.Tables[0].Rows[i]["CUANTIA"].ToString()) ? 0 : decimal.Parse(ds.Tables[0].Rows[i]["CUANTIA"].ToString()),
                           FechaIngresoQuiebra = fechaIngresoQuiebra == new DateTime() ? (DateTime?)null : fechaIngresoQuiebra,
                           Materia = ds.Tables[0].Rows[i]["Materia"].ToString(),
                           Solicitante = ds.Tables[0].Rows[i]["Solicitante"].ToString(),
                           MtoCostasPersonales = string.IsNullOrEmpty(ds.Tables[0].Rows[i]["MtoCostasPersonales"].ToString()) ? 0 : decimal.Parse(ds.Tables[0].Rows[i]["MtoCostasPersonales"].ToString()),
                           FecCostasPersonales = fechaCostasPersonales == new DateTime() ? (DateTime?)null : fechaCostasPersonales,
                           FecIngresoSolicitud = fechaIngresoSolicitud == new DateTime() ? (DateTime?)null : fechaIngresoSolicitud,
                           FecNotificacionSolicitud = fechaNotificacionSolicitud == new DateTime() ? (DateTime?)null : fechaNotificacionSolicitud,
                           FecAudienciaInicial = fechaAudienciaInicial == new DateTime() ? (DateTime?)null : fechaAudienciaInicial,
                           FecAudienciaPrueba = fechaAudienciaPrueba == new DateTime() ? (DateTime?)null : fechaAudienciaPrueba,
                           FecAudienciaFallo = fechaAudienciaFallo == new DateTime() ? (DateTime?)null : fechaAudienciaFallo,
                           FecResolucionLiquidacion = fechaResolucionLiquidacion == new DateTime() ? (DateTime?)null : fechaResolucionLiquidacion,
                           FecResolucionLiquidacionBC = fechaResolucionLiquidacionBC == new DateTime() ? (DateTime?)null : fechaResolucionLiquidacionBC,
                           FecResolucionReorganizacionBC = fechaResolucionReorganizacionBC == new DateTime() ? (DateTime?)null : fechaResolucionReorganizacionBC,
                           FecVerificacion = fechaVerificacion == new DateTime() ? (DateTime?)null : fechaVerificacion,
                           FecAcreditacionPoder = fechaAcreditacionPoder == new DateTime() ? (DateTime?)null : fechaAcreditacionPoder,
                           FecJuntaConstitutiva = fechaJuntaConstitutiva == new DateTime() ? (DateTime?)null : fechaJuntaConstitutiva,
                           FecJuntaDeliberativa = fechaJuntaDeliberativa == new DateTime() ? (DateTime?)null : fechaJuntaDeliberativa,
                           StatusAcuerdo = ds.Tables[0].Rows[i]["StatusAcuerdo"].ToString(),
                           FecAcuerdo = fechaAcuerdo== new DateTime() ? (DateTime?)null : fechaAcuerdo,
                           FecVerificadoAcreditado = fechaVerificadoAcreditado == new DateTime() ? (DateTime?)null : fechaVerificadoAcreditado,
                           FecNomCreditoVerificado = fechaNomCreditoVerificado == new DateTime() ? (DateTime?)null : fechaNomCreditoVerificado,
                           FecImpugnacion = fechaImpugnacion == new DateTime() ? (DateTime?)null : fechaImpugnacion,
                           FecNomCreditoReconocido = fechaNomCreditoReconocido == new DateTime() ? (DateTime?)null : fechaNomCreditoReconocido,
                           FecSolicitudAntecedente = fechaSolicitudAntecedente == new DateTime() ? (DateTime?)null : fechaSolicitudAntecedente,
                           FecRecepcionAntecedente = fechaRecepcionAntecedente == new DateTime() ? (DateTime?)null : fechaRecepcionAntecedente,
                           FecEnvioAntecedente = fechaEnvioAntecedente == new DateTime() ? (DateTime?)null : fechaEnvioAntecedente,
                           FecEmisionND = fechaEmisionND == new DateTime() ? (DateTime?)null :fechaEmisionND, 
                           MtoEmision = string.IsNullOrEmpty(ds.Tables[0].Rows[i]["MtoEmision"].ToString()) ? 0 : decimal.Parse(ds.Tables[0].Rows[i]["MtoEmision"].ToString()),
                           FecRepartos = fechaRepartos == new DateTime() ? (DateTime?)null : fechaRepartos,
                           MtoRepartos = string.IsNullOrEmpty(ds.Tables[0].Rows[i]["MtoRepartos"].ToString()) ? 0 : decimal.Parse(ds.Tables[0].Rows[i]["MtoRepartos"].ToString()),
                           FecDevolucion = fechaDevolucion == new DateTime() ? (DateTime?)null : fechaDevolucion,
                           PgoCostasPersonales = string.IsNullOrEmpty(ds.Tables[0].Rows[i]["PgoCostasPersonales"].ToString()) ? 0 : decimal.Parse(ds.Tables[0].Rows[i]["PgoCostasPersonales"].ToString()),
                           FecPgoCostasPersonales = fechaPgoCostasPersonales == new DateTime() ? (DateTime?)null : fechaPgoCostasPersonales,
                           FecAprobacionCtaFinal = fechaAprobacionCtaFinal == new DateTime() ? (DateTime?)null : fechaAprobacionCtaFinal,
                           FecCertificadoIncobrable = fechaCertificadoIncobrable == new DateTime() ? (DateTime?)null : fechaCertificadoIncobrable,
                           Row = Int32.Parse(ds.Tables[0].Rows[i]["row"].ToString())
                           
                          
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.ListarPanelQuiebraGrilla", 0);
                return lst;
            }
        }

        public static List<dto.LiquidacionRolQuiebra> ListarLiquidacionRolQuiebra(int codemp, int rolId)
        {
            List<dto.LiquidacionRolQuiebra> lst = new List<dto.LiquidacionRolQuiebra>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Roles_Liquidacion");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("rolId", rolId);
             
                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.LiquidacionRolQuiebra()
                        {
                            Rol = ds.Tables[0].Rows[i]["Rol"].ToString(),
                            Sbcid = string.IsNullOrEmpty(ds.Tables[0].Rows[i]["SBCID"].ToString()) ? default(int) : Int32.Parse(ds.Tables[0].Rows[i]["SBCID"].ToString()),
                            Cuantia = string.IsNullOrEmpty(ds.Tables[0].Rows[i]["Cuantia"].ToString()) ? 0 : decimal.Parse(ds.Tables[0].Rows[i]["Cuantia"].ToString()),
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.PanelQuiebra.ListarLiquidacionRolQuiebra", 0);
                return lst;
            }
        }
        public static List<dto.LiquidacionRolDocumentos> ListarLiquidacionRolDocumentos(int codemp, int rolId,
                                                                                            int? sbcid)
        {
            List<dto.LiquidacionRolDocumentos> lst = new List<dto.LiquidacionRolDocumentos>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Roles_Liquidacion_Documentos");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("rolId", rolId);
                sp.AgregarParametro("sbcid", sbcid);
       
                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.LiquidacionRolDocumentos()
                        {
                            Pclid = Int32.Parse(ds.Tables[0].Rows[i]["pclid"].ToString()),
                            Ctcid = Int32.Parse(ds.Tables[0].Rows[i]["ctcid"].ToString()),
                            Ccbid = Int32.Parse(ds.Tables[0].Rows[i]["ccbid"].ToString())
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.PanelQuiebra.ListarLiquidacionRolDocumentos", 0);
                return lst;
            }
        }
        public static int InsertarPanelQuiebra(int codemp, int rolId, string rolNumero, int? sbcid, decimal cuantia, int userId)
        {
            int result = -1;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Insertar_Panel_Quiebra");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("rolId", rolId);
                sp.AgregarParametro("rolNumero", rolNumero);
                sp.AgregarParametro("sbcid", (sbcid == 0) ? DBNull.Value : (object)sbcid);
                sp.AgregarParametro("cuantia", cuantia); 
                sp.AgregarParametro("user", userId);

                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        result = Int32.Parse(ds.Tables[0].Rows[0]["panelId"].ToString());
                    }
                

            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.PanelQuiebra.InsertarPanelDemanda", userId);
                return -1;
            }
            return result;
        }

        public static int InsertarPanelQuiebraDocumentos(int panelId, int codemp, int pclid, int ctcid, int ccbid, int userId)
        {
            int result = -1;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Insertar_Panel_Quiebra_Documentos");
                sp.AgregarParametro("panelId", panelId);
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("ccbid", ccbid);
                sp.AgregarParametro("user", userId);

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        result = Int32.Parse(ds.Tables[0].Rows[0]["panelId"].ToString());
                    }
                
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.PanelQuiebra.InsertarPanelQuiebraDocumentos", userId);
                return -1;
            }
            return result;
        }
        public static string getPanelQuiebraSindico(int rolId)
        {
            string sindico = string.Empty;
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Panel_Quiebra_Sindico");
                sp.AgregarParametro("rolId", rolId);
                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        sindico = ds.Tables[0].Rows[0]["SINDICO"].ToString();
                    }
                
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.PanelQuiebra.getPanelQuiebraSindico", 0);
                return string.Empty;
            }
            return sindico;
        }
        public static string getPanelQuiebraVeedor(int rolId)
        {
            string veedor = string.Empty;
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Panel_Quiebra_Sindico");
                sp.AgregarParametro("rolId", rolId);

                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        veedor = ds.Tables[0].Rows[0]["VEEDOR"].ToString();
                    }
                
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.PanelQuiebra.getPanelQuiebraVeedor", 0);
                return string.Empty;
            }
            return veedor;
        }
        public static string getPanelQuiebraInterventor(int rolId)
        {
            string interventor = string.Empty;
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Panel_Quiebra_Sindico");
                sp.AgregarParametro("rolId", rolId);

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        interventor = ds.Tables[0].Rows[0]["INTERVENTOR"].ToString();
                    }
                
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.PanelQuiebra.getPanelQuiebraInterventor", 0);
                return string.Empty;
            }
            return interventor;
        }
        public static int InsertUpdatePanelQuiebraSindico(int codemp, int rolId, string sindico, string veedor, string interventor, int user)
        {
            int id = -1;

            try
            {

                Funciones func = new Funciones();
                StoredProcedure sp = new StoredProcedure("_Insert_Update_Panel_Quiebra_Sindico");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("rolId", rolId);
                sp.AgregarParametro("sindico", (object)sindico ?? DBNull.Value);
                sp.AgregarParametro("veedor", (object)veedor ?? DBNull.Value);
                sp.AgregarParametro("interventor", (object)interventor ?? DBNull.Value);
                sp.AgregarParametro("user", user);

                id = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.PanelQuiebra.InsertUpdatePanelQuiebraSindico", 0);
                return id;
            }
            return id;
        }

        public static int InsertPanelQuiebraReparto(int quiebraId, string fecReparto, string MtoReparto, int user)
        {
            int id = -1;

            try
            {

                Funciones func = new Funciones();
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Insertar_Panel_Quiebra_Reparto");
                sp.AgregarParametro("quiebraId", quiebraId);
                sp.AgregarParametro("fecReparto", string.IsNullOrEmpty(fecReparto) ? DBNull.Value : (object)DateTime.Parse(fecReparto));
                sp.AgregarParametro("mtoReparto", string.IsNullOrEmpty(MtoReparto) ? DBNull.Value : (object)Decimal.Parse(MtoReparto));
                sp.AgregarParametro("user", user);

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        id = Int32.Parse(ds.Tables[0].Rows[0]["quiebraId"].ToString());
                    }
                
            }
            catch (Exception ex)
            {
                return id;
            }
            return id;
        }

        public static List<dto.PanelQuiebraReparto> ListarPanelQuiebraRepartos(int quiebraId, string where, string sidx, string sord)
        {
            List<dto.PanelQuiebraReparto> lst = new List<dto.PanelQuiebraReparto>();
            DateTime fechaReparto = new DateTime();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_PanelQuiebra_Repartos_Grilla");
                sp.AgregarParametro("quiebraId", quiebraId);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);

                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecReparto"].ToString(), out fechaReparto);
                        lst.Add(new dto.PanelQuiebraReparto()
                        {
                            QuiebraId = Int32.Parse(ds.Tables[0].Rows[i]["QuiebraId"].ToString()),
                            RepartoId = Int32.Parse(ds.Tables[0].Rows[i]["RepartoId"].ToString()),
                            MtoReparto = string.IsNullOrEmpty(ds.Tables[0].Rows[i]["MtoReparto"].ToString()) ? 0 : decimal.Parse(ds.Tables[0].Rows[i]["MtoReparto"].ToString()),
                            FecReparto = fechaReparto == new DateTime() ? (DateTime?)null : fechaReparto,
                            Row = Int32.Parse(ds.Tables[0].Rows[i]["row"].ToString())
                        });
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.ListarPanelQuiebraRepartos", 0);
                return lst;
            }
        }

        public static int InsertAvancePanelQuiebraReglas(int rolId, int quiebraId, string solicitante, 
                                                                string fecResolLiqui, string fecJuntaConsti, 
                                                                string fecNomCreditoVeri, string fecAprobCtaFinal, int user)
        {
            int id = -1;

            try
            {

                Funciones func = new Funciones();
                StoredProcedure sp = new StoredProcedure("_Guardar_Panel_Quiebra_Rol_Detalle_Reglas");
                sp.AgregarParametro("rolId", rolId);
                sp.AgregarParametro("quiebraId", quiebraId);
                sp.AgregarParametro("solicitante", (object)solicitante ?? DBNull.Value);
                sp.AgregarParametro("fecResolLiqui", string.IsNullOrEmpty(fecResolLiqui) ? DBNull.Value : (object)DateTime.Parse(fecResolLiqui));
                sp.AgregarParametro("fecJuntaConsti", string.IsNullOrEmpty(fecJuntaConsti) ? DBNull.Value : (object)DateTime.Parse(fecJuntaConsti));
                sp.AgregarParametro("fecNomCreditoVeri", string.IsNullOrEmpty(fecNomCreditoVeri) ? DBNull.Value : (object)DateTime.Parse(fecNomCreditoVeri));
                sp.AgregarParametro("fecAprobCtaFinal", string.IsNullOrEmpty(fecAprobCtaFinal) ? DBNull.Value : (object)DateTime.Parse(fecAprobCtaFinal));
                sp.AgregarParametro("userId", user);

                id = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                return id;
            }
            return id;
        }

        public static List<dto.PanelQuiebraGrafica> ListarPanelQuiebraGrafico(int codemp)
        {
            List<dto.PanelQuiebraGrafica> lst = new List<dto.PanelQuiebraGrafica>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Panel_Quiebra_Grafico");
                sp.AgregarParametro("codemp", codemp);
                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.PanelQuiebraGrafica()
                        {
                            Id = ds.Tables[0].Rows[i]["ID"].ToString(),
                            Total = ds.Tables[0].Rows[i]["TOTAL"].ToString(),
                            MtoAsignado = decimal.Parse(string.IsNullOrEmpty(ds.Tables[0].Rows[i]["MONTO"].ToString()) ? "0" : ds.Tables[0].Rows[i]["MONTO"].ToString()).ToString("N2"),
                            Item = ds.Tables[0].Rows[i]["ITEM"].ToString(),
                            Parent = ds.Tables[0].Rows[i]["PARENT"].ToString()
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.PanelQuiebra.ListarPanelQuiebraGrafico", 0);
                return lst;
            }
        }

        public static List<dto.PanelQuiebraPublicados> ListarPanelQuiebraReporteLiquidaciones(int codemp, string where, string sidx, string sord)
        {
            List<dto.PanelQuiebraPublicados> lst = new List<dto.PanelQuiebraPublicados>();
            try
            {
                DateTime fechaPublicacion = new DateTime();
             
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_PanelQuiebra_Diagrama_Liquidaciones_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
               
                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        fechaPublicacion = new DateTime();
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FECPUBLICACION"].ToString(), out fechaPublicacion);
                      
                        lst.Add(
                            new dto.PanelQuiebraPublicados()
                            {
                                QuiebraId = Int32.Parse(ds.Tables[0].Rows[i]["QuiebraId"].ToString()),
                                Pclid = Int32.Parse(ds.Tables[0].Rows[i]["Pclid"].ToString()),
                                FecPublicacion = fechaPublicacion == new DateTime() ? (DateTime?)null : fechaPublicacion,
                                Cliente = ds.Tables[0].Rows[i]["CLIENTE"].ToString(),
                                Deudor = ds.Tables[0].Rows[i]["Deudor"].ToString(),
                                Ctcid = Int32.Parse(ds.Tables[0].Rows[i]["Ctcid"].ToString()),
                                Rut = ds.Tables[0].Rows[i]["Rut"].ToString(),
                                Asegurado = ds.Tables[0].Rows[i]["Asegurado"].ToString(),

                                Dias = Int32.Parse(ds.Tables[0].Rows[i]["Dias"].ToString()),
                                Row = Int32.Parse(ds.Tables[0].Rows[i]["row"].ToString())
                            });

                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.PanelQuiebra.ListarPanelQuiebraReporteLiquidaciones", 0);
                return lst;
            }
        }

        public static List<dto.PanelQuiebraPublicados> ListarPanelQuiebraReporteReorganizaciones(int codemp, string where, string sidx, string sord)
        {
            List<dto.PanelQuiebraPublicados> lst = new List<dto.PanelQuiebraPublicados>();
            try
            {
                DateTime fechaPublicacion = new DateTime();

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_PanelQuiebra_Diagrama_Reorganiaciones_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);

                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        fechaPublicacion = new DateTime();
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FECPUBLICACION"].ToString(), out fechaPublicacion);

                        lst.Add(
                            new dto.PanelQuiebraPublicados()
                            {
                                QuiebraId = Int32.Parse(ds.Tables[0].Rows[i]["QuiebraId"].ToString()),
                                Pclid = Int32.Parse(ds.Tables[0].Rows[i]["Pclid"].ToString()),
                                FecPublicacion = fechaPublicacion == new DateTime() ? (DateTime?)null : fechaPublicacion,
                                Cliente = ds.Tables[0].Rows[i]["CLIENTE"].ToString(),
                                Deudor = ds.Tables[0].Rows[i]["Deudor"].ToString(),
                                Ctcid = Int32.Parse(ds.Tables[0].Rows[i]["Ctcid"].ToString()),
                                Rut = ds.Tables[0].Rows[i]["Rut"].ToString(),
                                Asegurado = ds.Tables[0].Rows[i]["Asegurado"].ToString(),

                                Dias = Int32.Parse(ds.Tables[0].Rows[i]["Dias"].ToString()),
                                Row = Int32.Parse(ds.Tables[0].Rows[i]["row"].ToString())
                            });

                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.PanelQuiebra.ListarPanelQuiebraReporteReorganizaciones", 0);
                return lst;
            }
        }

        public static List<dto.PanelQuiebraGrafica> ListarPanelQuiebraProyeccion(int codemp)
        {
            List<dto.PanelQuiebraGrafica> lst = new List<dto.PanelQuiebraGrafica>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Panel_Quiebra_Proyeccion");
                sp.AgregarParametro("codemp", codemp);
                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.PanelQuiebraGrafica()
                        {
                            Id = ds.Tables[0].Rows[i]["ID"].ToString(),
                            Total = ds.Tables[0].Rows[i]["TOTAL"].ToString(),
                            MtoAsignado = decimal.Parse(string.IsNullOrEmpty(ds.Tables[0].Rows[i]["MONTO"].ToString()) ? "0" : ds.Tables[0].Rows[i]["MONTO"].ToString()).ToString("N2"),
                            Item = ds.Tables[0].Rows[i]["ITEM"].ToString()
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.PanelQuiebra.ListarPanelQuiebraProyeccion", 0);
                return lst;
            }
        }
    }
}
