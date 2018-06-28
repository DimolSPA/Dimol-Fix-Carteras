using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Judicial.Mantenedores.bcp
{
    public class PanelQuiebra
    {
        public static List<dto.LiquidacionRol> ListarRolLiquidacionGrilla(int codemp, int rolId, string where, string sidx, string sord)
        {
            return dao.PanelQuiebra.ListarRolLiquidacionGrilla(codemp, rolId, where, sidx, sord);
        }
        public static dto.PanelQuiebraAvance BuscarDetalleQuiebra(int quiebraId)
        {
            return dao.PanelQuiebra.BuscarDetalleQuiebra(quiebraId);
        }
        public static int InsertUpdateAvancePanelQuiebra(int rolid, int quiebraId, string solicitante, string mtoCostasPersonales, string fecCostasPersonales, string fecIngrSolicitud, string fecNotSolicitud,
                                                        string fecAudienciaIni, string fecAudienciaPrueba, string fecAudienciaFallo, string fecResolLiqui, string fecResolLiquiBC,
                                                        string fecResolReorgBC, string fecVerificacion, string fecAcreditaPoder, string fecJuntaConsti, string fecJuntaDelibe,
                                                        string statusAcuerdo, string fecAcuerdo, string fecVerificaAcredita, string fecNomCreditoVeri, string fecImpugnacion, string fecNomCreditoRec,
                                                        string fecSolAntecedente, string fecRecepAntecedente, string fecEnvAntecedente, string fecEmisionND, string mtoEmisionND,
                                                        string fecRepartos, string MtoRepartos, string fecDevolucion, string pgoCostPersonales, string fecpgoCostPersonales,
                                                        string fecAprobCtaFinal, string fecCertiIncobrable, int user)
        {
            int result = -1;
            result = dao.PanelQuiebra.InsertUpdateAvancePanelQuiebra(quiebraId, solicitante, mtoCostasPersonales, fecCostasPersonales, fecIngrSolicitud, fecNotSolicitud,
                                                        fecAudienciaIni, fecAudienciaPrueba, fecAudienciaFallo, fecResolLiqui, fecResolLiquiBC,
                                                        fecResolReorgBC, fecVerificacion, fecAcreditaPoder, fecJuntaConsti, fecJuntaDelibe,
                                                        statusAcuerdo, fecAcuerdo, fecVerificaAcredita, fecNomCreditoVeri, fecImpugnacion, fecNomCreditoRec,
                                                        fecSolAntecedente, fecRecepAntecedente, fecEnvAntecedente, fecEmisionND, mtoEmisionND,
                                                        fecRepartos, MtoRepartos, fecDevolucion, pgoCostPersonales, fecpgoCostPersonales,
                                                        fecAprobCtaFinal, fecCertiIncobrable, user);
            if (result > 0)
            {
                result = dao.PanelQuiebra.InsertAvancePanelQuiebraReglas(rolid, quiebraId, solicitante, fecResolLiqui, fecJuntaConsti, fecNomCreditoVeri, fecAprobCtaFinal, user);
            }
            return result;
        }
        public static List<dto.PanelQuiebra> ListarPanelQuiebraGrilla(int codemp, int idioma, string where, string sidx, string sord)
        {
            return dao.PanelQuiebra.ListarPanelQuiebraGrilla(codemp, idioma, where, sidx, sord);
        }

        public static int InsertarPanelQuiebra(int codemp, int rolid, int user)
        {
            int panelId = -1;
            int procesoDocumento = -1;
            List<dto.LiquidacionRolQuiebra> lstpanel = dao.PanelQuiebra.ListarLiquidacionRolQuiebra(codemp, rolid);
            foreach (dto.LiquidacionRolQuiebra p in lstpanel)
            {
                //Ingreso panel Quiebra Cabecera/ Llave principal
                panelId = dao.PanelQuiebra.InsertarPanelQuiebra(codemp, rolid, p.Rol, p.Sbcid, p.Cuantia, user);
                if (panelId >= 0)
                {
                    List<dto.LiquidacionRolDocumentos> lstpanelDocumentos = dao.PanelQuiebra.ListarLiquidacionRolDocumentos(codemp, rolid, p.Sbcid);

                    foreach (dto.LiquidacionRolDocumentos doc in lstpanelDocumentos)
                    {
                        //Ingreso panel Quiebra los documentos del panel
                        procesoDocumento = dao.PanelQuiebra.InsertarPanelQuiebraDocumentos(panelId, codemp, doc.Pclid, doc.Ctcid, doc.Ccbid, user);
                    }
                }
            }

            return procesoDocumento;
        }

        public static string getPanelQuiebraInterventor(int rolId)
        {
            string result = string.Empty;

            result = dao.PanelQuiebra.getPanelQuiebraInterventor(rolId);

            return result;
        }
        public static string getPanelQuiebraVeedor(int rolId)
        {
            string result = string.Empty;

            result = dao.PanelQuiebra.getPanelQuiebraVeedor(rolId);

            return result;
        }
        public static string getPanelQuiebraSindico(int rolId)
        {
            string result = string.Empty;

            result = dao.PanelQuiebra.getPanelQuiebraSindico(rolId);

            return result;
        }
        public static int InsertUpdatePanelQuiebraSindico(int codemp, int rolId, string sindico, string veedor, string interventor, int user)
        {
            return dao.PanelQuiebra.InsertUpdatePanelQuiebraSindico(codemp, rolId, sindico, veedor, interventor, user);
        }
        public static int InsertPanelQuiebraReparto(int quiebraId, string fecReparto, string MtoReparto, int user)
        {
            return dao.PanelQuiebra.InsertPanelQuiebraReparto(quiebraId, fecReparto, MtoReparto, user);
        }
        public static List<dto.PanelQuiebraReparto> ListarPanelQuiebraRepartos(int quiebraId, string where, string sidx, string sord)
        {
            return dao.PanelQuiebra.ListarPanelQuiebraRepartos(quiebraId, where, sidx, sord);
        }
        public static List<dto.PanelQuiebraGrafica> ListarPanelQuiebraGrafico(int codemp)
        {
            return dao.PanelQuiebra.ListarPanelQuiebraGrafico(codemp);
        }
        public static List<dto.PanelQuiebraPublicados> ListarPanelQuiebraReporteLiquidaciones(int codemp, string where, string sidx, string sord)
        {
            return dao.PanelQuiebra.ListarPanelQuiebraReporteLiquidaciones(codemp, where, sidx, sord);
        }
        public static List<dto.PanelQuiebraPublicados> ListarPanelQuiebraReporteReorganizaciones(int codemp, string where, string sidx, string sord)
        {
            return dao.PanelQuiebra.ListarPanelQuiebraReporteReorganizaciones(codemp, where, sidx, sord);
        }
        public static string ListarPanelQuiebraProyeccion(int codemp)
        {
            string salida = "";
           
            List<dto.PanelQuiebraGrafica> lstdatos = dao.PanelQuiebra.ListarPanelQuiebraProyeccion(codemp);
            if (lstdatos.Count > 0)
            {
                salida += "<ul class=\"list-group\">";
                foreach (dto.PanelQuiebraGrafica obj in lstdatos)
                {
                    salida += "<li class=\"list-group-item\">";
                    salida += "<label>" + obj.Item + "</label>";
                    salida += "<span class=\"badge badge-info\">" + obj.MtoAsignado + "</span>";
                    salida += "<span class=\"badge badge-info\">" + obj.Total + "</span>";
                    salida += "</li>";
                }
                salida += "</ul>";
            }
            else
            {
                salida = "";
            }


            return salida;
        }
    }

}
