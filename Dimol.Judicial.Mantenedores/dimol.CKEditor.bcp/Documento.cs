using Dimol.dto;
using Dimol.Judicial.Mantenedores.dao;
using Dimol.Judicial.Mantenedores.dto;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Dimol.CKEditor.bcp
{
    public class Documento
    {
        public static List<Combobox> ListarDocumentos(int codemp, string area, string first)
        {
            return Dimol.CKEditor.dao.Documento.ListarDocumentos(codemp, area, first);
        }

        public static dto.BorradorJudicial ReporteCertificados(int codemp, int rolid)
        {
            return dao.BorradorJudicial.ReporteCertificados(codemp, rolid);
        }

        public static string GeneraDocumento(int codemp, int rolid, int idBorrador)
        {
            string ultimoBorrador = "";
            string salida = "";
            dto.BorradorJudicial obj = new dto.BorradorJudicial();

            try
            {
                ultimoBorrador = dao.Documento.TraeHTLMUltimoBorrador(codemp, idBorrador, rolid);

                if (!string.IsNullOrEmpty(ultimoBorrador))
                {
                    salida = ultimoBorrador;
                }
                else
                {
                    Dimol.dto.Combobox template = dao.Documento.TraeXSLTBorrador(codemp, idBorrador);
                    switch (template.Value)
                    {
                        case "CERTIFICADO":
                            obj = dao.BorradorJudicial.ReporteCertificados(codemp, rolid);
                            break;
                        case "AVENIMIENTO":
                            obj = dao.BorradorJudicial.Avenimiento(codemp, rolid);
                            break;
                        case "DEMANDA":
                            obj = dao.BorradorJudicial.Demanda(codemp, rolid);
                            break;
                        case "PREVISIONAL":
                            obj = dao.BorradorJudicial.DemandaPrevisional(codemp, rolid);
                            break;
                    }

                    salida = TransformXSLToHTML(obj, idBorrador);
                }
            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "CKeditor.bcp.Documento.GeneraCertificados", obj.Rolid);
                throw ex;
            }

            return salida;
        }

        public static string GeneraDocumentoDemandaMasiva(int codemp, int IdDM, int TipoBorrador)
        {
            string ultimoBorrador = "";
            string salida = "";
            dto.BorradorJudicial obj = new dto.BorradorJudicial();

            try
            {
                ultimoBorrador = dao.Documento.TraeHTLMUltimoBorradorDemandaMasiva(codemp, TipoBorrador, IdDM);

                if (!string.IsNullOrEmpty(ultimoBorrador))
                {
                    salida = ultimoBorrador;
                }
                else
                {
                    Dimol.dto.Combobox template = dao.Documento.TraeXSLTBorrador(codemp, TipoBorrador);

                    PanelDemandaMasivaDetalle oDemandaDetalle = PanelDemandaMasiva.ObtenerPanelDemandaMasivaDetalle(IdDM);

                    if (oDemandaDetalle.RolId != null)
                    {
                        int RolId = (int)oDemandaDetalle.RolId;

                        switch (template.Value)
                        {
                            case "CERTIFICADO":
                                obj = dao.BorradorJudicial.ReporteCertificados(codemp, RolId);
                                break;
                            case "AVENIMIENTO":
                                obj = dao.BorradorJudicial.Avenimiento(codemp, RolId);
                                break;
                            case "DEMANDA":
                                obj = dao.BorradorJudicial.Demanda(codemp, RolId);
                                break;
                        }
                    } else {
                        switch (template.Value)
                        {
                            case "DEMANDA":
                                obj = dao.BorradorJudicial.DemandaMasivaPorIdPanel(codemp, IdDM);
                                break;
                        }
                    }
                    
                    salida = TransformXSLToHTML(obj, TipoBorrador);
                }
            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "CKeditor.bcp.Documento.GeneraDocumentoDemandaMasiva", obj.Rolid);
                throw ex;
            }

            return salida;
        }

        public static string GeneraDocumentoDemandaPrevisional(int codemp, int IdDP, int TipoBorrador)
        {
            string ultimoBorrador = "";
            string salida = "";
            dto.BorradorJudicial obj = new dto.BorradorJudicial();

            try
            {
                ultimoBorrador = dao.Documento.TraeHTLMUltimoBorradorDemandaPrevisional(codemp, TipoBorrador, IdDP);

                if (!string.IsNullOrEmpty(ultimoBorrador))
                {
                    salida = ultimoBorrador;
                }
                else
                {
                    Dimol.dto.Combobox template = dao.Documento.TraeXSLTBorrador(codemp, TipoBorrador);
                    switch (template.Value)
                    {
                        case "CERTIFICADO":
                            //obj = dao.BorradorJudicial.ReporteCertificados(codemp, IdDP);
                            break;
                        case "AVENIMIENTO":
                            //obj = dao.BorradorJudicial.Avenimiento(codemp, IdDP);
                            break;
                        case "DEMANDA":
                            //obj = dao.BorradorJudicial.Demanda(codemp, IdDP);
                            break;
                        case "PREVISIONAL":
                            PanelDemandaPrevisionalDetalle oDemandaDetalle = PanelDemandaPrevisional.ObtenerPanelDemandaPrevisionalDetalle(IdDP);

                            if (oDemandaDetalle.RolId != null)
                            {
                                obj = dao.BorradorJudicial.DemandaPrevisional(codemp, (int)oDemandaDetalle.RolId);
                            } else {
                                obj = dao.BorradorJudicial.DemandaPrevisionalPorIdPanel(codemp, IdDP);

                                #region Obtención de resoluciones
                                List<DocumentosPanel> ltResoluciones = Judicial.Mantenedores.dao.PanelDemanda.ListarDocumentosPanelPrevisionalId(codemp, IdDP, "", "FEC_RESOLUCION", "DESC", 0, 200);

                                //lst.ListaResoluciones = PanelDemanda.ListarDocumentosPanelPrevisionalId(); //CargaTablaDocumentosXsl(codEmp, panelId); //HACER UN NUEVO METODO Q OBTENGA LOS DOCS POR ID PANEL
                                decimal total = 0;
                                obj.ListaResoluciones = new List<dto.ResolucionParaXsl>();
                                for (int j = 0; j < ltResoluciones.Count; j++)
                                {
                                    //Totales
                                    var objDocumentosPanel = ltResoluciones[j];
                                    total = total + objDocumentosPanel.Monto;

                                    //oResolucionParaXsl
                                    var oResolucionParaXsl = new dto.ResolucionParaXsl();
                                    oResolucionParaXsl.NumResolucion = ltResoluciones[j].NumResolucion;
                                    oResolucionParaXsl.FecResolucion = ltResoluciones[j].FecResolucion.ToString("dd-MMMM-yyyy", new CultureInfo("es-ES"));
                                    oResolucionParaXsl.FecResolucionStr = ltResoluciones[j].FecResolucion.ToString("dd 'de' MMMM 'de' yyyy", new CultureInfo("es-ES"));
                                    oResolucionParaXsl.ListaPeriodos = dao.BorradorJudicial.CargarPeriodosTablaDocumentosXsl(ltResoluciones[j].NumResolucion);
                                    oResolucionParaXsl.MontoTotal = ltResoluciones[j].Monto;
                                    oResolucionParaXsl.MontoTotalStr = ltResoluciones[j].Monto.ToString("0.00").Replace(".00", "");
                                    obj.ListaResoluciones.Add(oResolucionParaXsl);
                                }

                                obj.MontoPrevisional = total;
                                obj.MontoPrevisionalStr = dao.Documento.NumeroEnletras(obj.MontoPrevisional.ToString("N2"));
                                #endregion
                            }

                            break;
                    }

                    salida = TransformXSLToHTML(obj, TipoBorrador);
                }
            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "CKeditor.bcp.Documento.GeneraDocumentoDemandaPrevisional", obj.Rolid);
                throw ex;
            }

            return salida;
        }

        public static string GeneraCertificados(int codemp, int rolid, int idBorrador)
        {
            string ultimoBorrador = "";
            string salida = "";
            dto.BorradorJudicial obj = new dto.BorradorJudicial();

            try
            {
                ultimoBorrador = dao.Documento.TraeHTLMUltimoBorrador(codemp, idBorrador, rolid);

                if (!string.IsNullOrEmpty(ultimoBorrador))
                {
                    salida = ultimoBorrador;
                }
                else
                {
                    obj = ReporteCertificados(codemp, rolid);
                    salida = TransformXSLToHTML(obj, idBorrador);
                }
            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "CKeditor.bcp.Documento.GeneraCertificados", obj.Rolid);
                throw ex;
            }

            return salida;
        }

        public static string TransformXSLToHTML(object data, string xslFileName)
        {
            try
            {
                XmlSerializer xs = new XmlSerializer(data.GetType());
                string xmlString;
                using (StringWriter swr = new StringWriter())
                {
                    xs.Serialize(swr, data);
                    xmlString = swr.ToString();
                }

                var xd = new XmlDocument();
                xd.LoadXml(xmlString);

                var xslt = new System.Xml.Xsl.XslCompiledTransform();
                xslt.Load(xslFileName);
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
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Reportes.bcp.Transformador Excel", 0);
                throw ex;
            }
        }

        public static string TransformXSLToHTML(object data, int idBorrador)
        {
            try
            {
                XmlSerializer xs = new XmlSerializer(data.GetType());
                string xmlString;
                string strXSLT = dao.Documento.TraeXSLTBorrador(1, idBorrador).Text;
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
                
                return sr.ReadToEnd();
            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Reportes.bcp.Transformador Excel", 0);
                throw ex;
            }
        }

        public static int InsertarRolBorrador(int codemp, int rolid, int idBorrador, string html, int usrid)
        {
            return dao.Documento.InsertarRolBorrador(codemp, rolid, idBorrador, html, usrid);
        }

        public static int InsertarBorradorDemandasMasivas(int codemp, int PanelDemandaId, int TipoBorradorId, string HtmlBorrador, int usrid)
        {
            return dao.Documento.InsertarBorradorDemandasMasivas(codemp, PanelDemandaId, TipoBorradorId, HtmlBorrador, usrid);
        }

        public static int InsertarBorradorDemandasPrevisional(int codemp, int PanelDemandaId, int TipoBorradorId, string HtmlBorrador, int usrid)
        {
            return dao.Documento.InsertarBorradorDemandasPrevisional(codemp, PanelDemandaId, TipoBorradorId, HtmlBorrador, usrid);
        }
    }
}