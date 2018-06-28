using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dimol.Reportes.dao;
using Dimol.PDF;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using Dimol.bcp;
using Dimol.dto;

namespace Dimol.Reportes.bcp
{
    public class Cartera
    {
        public static List<Combobox> ListarTipoDocumentosDeudor(int codemp, int idioma, string first)
        {
            return new List<Combobox>();//dao.Deudor.ListarTipoDocumentosDeudor(codemp, idioma, first);
        }

        public static List<Combobox> ListarReportes(int modulo, int idioma, string first)
        {
            return dao.Cartera.ListarReportes(modulo, idioma, first);
        }

        #region "Reportes"

        public static string TraePrescripciones(dto.Prescripciones obj)
        {
            string salida = "";
            obj.Encabezado.Codemp = obj.Codemp;
            obj.Encabezado.Codsuc = obj.Codsuc;
            try
            {
                dao.CabeceraReporte.TraeEmpresa(obj.Encabezado);
                dao.Prescripciones.TraeTitulo(obj);
                dao.Prescripciones.ListarDetallePrescripciones(obj);
     
                Transformador objTransformador = new Transformador();

                XmlSerializer xs = new XmlSerializer(obj.GetType());
                string xmlString;
                using (StringWriter swr = new StringWriter())
                {
                    xs.Serialize(swr, obj);
                    xmlString = swr.ToString();
                }

                salida = objTransformador.TransformXLS(obj, obj.PathArchivo + ".xsl");
                System.IO.File.WriteAllText(@"C:\ArchivosSalida\Prescripciones_Detalle.xls", salida);                                     

            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Reportes.bcp.Cartera.TraeLiquidacionAgrorama", obj.Pclid);
                throw ex;
            }
            return salida;
        }

        public static string TraeArbolJudicial(dto.ArbolJudicial obj)
        {
            string salida = "";
            bool val =false;
            obj.Encabezado.Codemp = obj.Codemp;
            obj.Encabezado.Codsuc = obj.Codsuc;
            try
            {
                dao.CabeceraReporte.TraeEmpresa(obj.Encabezado);
                dao.ArbolJudicial.TraeTitulo(obj);
                dao.ArbolJudicial.ListarArbolJudicialDetalle(obj);
                //obj.FechaEmisionCorta = DateTime.Today;
                Transformador objTransformador = new Transformador();

                XmlSerializer xs = new XmlSerializer(obj.GetType());
                string xmlString;
                using (StringWriter swr = new StringWriter())
                {
                    xs.Serialize(swr, obj);
                    xmlString = swr.ToString();
                }

              //  salida = objTransformador.TransformXLS(obj, obj.PathArchivo + ".xsl");
              //  System.IO.File.WriteAllText(@"C:\ArchivosSalida\Arbol_Judicial_Detalle.xls", salida);

                salida = objTransformador.TransformXLS(obj, obj.PathArchivo + "_global.xsl");
                System.IO.File.WriteAllText(@"C:\ArchivosSalida\Arbol_Judicial_Global.xls", salida);

                //salida = objTransformador.TransformXLS(obj, obj.PathArchivo + "_pdf.xsl");
                //System.IO.File.WriteAllText(@"C:\ArchivosSalida\Arbol_Judicial_Pdf.xls", salida);

              //  objTransformador.TransformXml(obj, obj.PathArchivo + "_pdf.xsl", obj.PathArchivo + "_pdf.fo");
              //  Generador objGenerador = new Generador(obj.PathArchivo + "_pdf.fo", obj.PathArchivo + "_pdf.pdf");
              //  val = objGenerador.XSLToPDF();

                objTransformador.TransformXml(obj, obj.PathArchivo + "_pdf2.xsl", obj.PathArchivo + "_pdf2.fo");
                Generador objGenerador = new Generador(obj.PathArchivo + "_pdf2.fo", obj.PathArchivo + "_pdf2.pdf");
                val = objGenerador.XSLToPDF();

            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Reportes.bcp.Cartera.TraeLiquidacionAgrorama", obj.Pclid);
                throw ex;
            }
            return salida;
        }

        public static string TraeTortaRanking(dto.TortaAgrupada obj)
        {
            string salida = "";
            obj.Encabezado.Codemp = obj.Codemp;
            obj.Encabezado.Codsuc = obj.Sucid;
            try
            {
                dao.CabeceraReporte.TraeEmpresa(obj.Encabezado);
                dao.TortaAgrupada.TraeTitulo(obj);
                dao.TortaAgrupada.ListarRankingDetalle(obj);
                obj.FechaEmisionCorta = DateTime.Today;
                Transformador objTransformador = new Transformador();
                salida = objTransformador.TransformXLS(obj, obj.PathArchivo + ".xsl");
                System.IO.File.WriteAllText(@"C:\ArchivosSalida\Ranking_Incidencia_Cartera.xls", salida);
            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Reportes.bcp.Cartera.TraeLiquidacionAgrorama", obj.Pclid);
                throw ex;
            }
            return salida;
        }

        public static bool TraeHojaTramiteCliente(dto.HojaTramite obj)
        {
            bool salida = false;
            obj.Encabezado.Codemp = obj.Codemp;
            obj.Encabezado.Codsuc = obj.IdSuc;
            try
            {
                dao.CabeceraReporte.TraeEmpresa(obj.Encabezado);
                dao.HojaTramite.TraeTitulo(obj);
                dao.HojaTramite.ListarTramitesDetalle(obj);

                string[] meses = { "", "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
                obj.FechaLarga = DateTime.Today.Day.ToString() + " de " + meses[DateTime.Today.Month] + ", " + DateTime.Today.ToString("yyyy");
                
                Transformador objTransformador = new Transformador();

                objTransformador.TransformXml(obj, obj.PathArchivo + ".xsl", obj.PathArchivo + ".fo");
                Generador objGenerador = new Generador(obj.PathArchivo + ".fo", obj.PathArchivo + ".pdf");
                salida = objGenerador.XSLToPDF();
            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Reportes.bcp.Cartera.TraeLiquidacionAgrorama", obj.Pclid);
                throw ex;
            }
            return salida;
        }

            public static string TraeCastigoPrejudicialAsegurado(dto.CastigoPrejudicialCliente obj)
            {
                obj.Encabezado.Codemp = obj.Codemp;
                obj.Encabezado.Codsuc = obj.Codsuc;

                try
                {
                    List<dto.CastigoPrejudicialCliente> lstDevDocs = new List<dto.CastigoPrejudicialCliente>();
                    dao.CabeceraReporte.TraeEmpresa(obj.Encabezado);
                    dao.CastigoPrejudicial.TraeTitulo(obj);
                    dao.CastigoPrejudicial.ListarCastigosDetalle(obj, lstDevDocs);

                    foreach (dto.CastigoPrejudicialCliente doc in lstDevDocs)
                    {
                        Transformador objTransformador = new Transformador();

                        XmlSerializer xs = new XmlSerializer(doc.GetType());
                        string xmlString;
                        using (StringWriter swr = new StringWriter())
                        {
                            xs.Serialize(swr, doc);
                            xmlString = swr.ToString();
                        }

                        objTransformador.TransformXml(doc, @"C:\ArchivosSalida\castigo_prejudicial_asegurado.xsl", @"C:\ArchivosSalida\castigo_prejudicial_asegurado_" + doc.Cbcnumero + "_" + doc.NumRegistro + ".fo");
                        Generador objGenerador = new Generador(@"C:\ArchivosSalida\castigo_prejudicial_asegurado_" + doc.Cbcnumero + "_" + doc.NumRegistro + ".fo", @"C:\ArchivosSalida\castigo_prejudicial_asegurado_" + doc.Cbcnumero + "_" + doc.NumRegistro + ".pdf");
                        objGenerador.XSLToPDF();
                    }

                }
                catch (Exception ex)
                {
                    Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Reportes.bcp.Cartera.TraeCastigoPrejudicialAsegurado", obj.Pclid);
                    throw ex;
                }

                return "";
            }

        public static string TraeCastigoPrejudicialCliente(dto.CastigoPrejudicialCliente obj)
        {
            obj.Encabezado.Codemp = obj.Codemp;
            obj.Encabezado.Codsuc = obj.Codsuc;

            try
            {
                List<dto.CastigoPrejudicialCliente> lstDevDocs = new List<dto.CastigoPrejudicialCliente>();
                dao.CabeceraReporte.TraeEmpresa(obj.Encabezado);
                dao.CastigoPrejudicial.TraeTitulo(obj);
                dao.CastigoPrejudicial.ListarCastigosDetalle(obj, lstDevDocs);

                foreach (dto.CastigoPrejudicialCliente doc in lstDevDocs)
                {
                    Transformador objTransformador = new Transformador();

                    XmlSerializer xs = new XmlSerializer(doc.GetType());
                    string xmlString;
                    using (StringWriter swr = new StringWriter())
                    {
                        xs.Serialize(swr, doc);
                        xmlString = swr.ToString();
                    }

                    objTransformador.TransformXml(doc, @"C:\ArchivosSalida\castigo_prejudicial_cliente.xsl", @"C:\ArchivosSalida\castigo_prejudicial_cliente_" + doc.Cbcnumero + "_" + doc.NumRegistro + ".fo");
                    Generador objGenerador = new Generador(@"C:\ArchivosSalida\castigo_prejudicial_cliente_" + doc.Cbcnumero + "_" + doc.NumRegistro + ".fo", @"C:\ArchivosSalida\castigo_prejudicial_cliente_" + doc.Cbcnumero + "_" + doc.NumRegistro + ".pdf");
                    objGenerador.XSLToPDF();
                }

            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Reportes.bcp.Cartera.TraeDevolucionDocumentosCliente", obj.Pclid);
                throw ex;
            }

            return "";
        }

        public static string TraeCastigoPrejudicialManualCliente(dto.CastigoPrejudicialManualCliente obj)
        {
            obj.Encabezado.Codemp = obj.Codemp;
            obj.Encabezado.Codsuc = obj.Codsuc;

            try
            {
                List<dto.CastigoPrejudicialManualCliente> lstDevDocs = new List<dto.CastigoPrejudicialManualCliente>();
                dao.CabeceraReporte.TraeEmpresa(obj.Encabezado);
                dao.CastigoPrejudicialManual.TraeTitulo(obj);
                dao.CastigoPrejudicialManual.ListarCastigosDetalle(obj, lstDevDocs);

                foreach (dto.CastigoPrejudicialManualCliente doc in lstDevDocs)
                {
                    Transformador objTransformador = new Transformador();

                    XmlSerializer xs = new XmlSerializer(doc.GetType());
                    string xmlString;
                    using (StringWriter swr = new StringWriter())
                    {
                        xs.Serialize(swr, doc);
                        xmlString = swr.ToString();
                    }

                    objTransformador.TransformXml(doc, @"C:\ArchivosSalida\castigo_prejudicial_manual_cliente.xsl", @"C:\ArchivosSalida\castigo_prejudicial_cliente_" + doc.Cbcnumero + "_" + doc.NumRegistro + ".fo");
                    Generador objGenerador = new Generador(@"C:\ArchivosSalida\castigo_prejudicial_cliente_" + doc.Cbcnumero + "_" + doc.NumRegistro + ".fo", @"C:\ArchivosSalida\castigo_prejudicial_cliente_" + doc.Cbcnumero + "_" + doc.NumRegistro + ".pdf");
                    objGenerador.XSLToPDF();
                }

            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Reportes.bcp.Cartera.TraeCastigoPrejudicialManualCliente", obj.Pclid);
                throw ex;
            }

            return "";
        }

        public static string TraeCastigoJudicial(dto.CastigoJudicialCliente obj)
        {
            obj.Encabezado.Codemp = obj.Codemp;
            obj.Encabezado.Codsuc = obj.Codsuc;
            obj.RutAbogado = Dimol.bcp.Funciones.formatearRut(obj.RutAbogado);

            try
            {
                List<dto.CastigoJudicialCliente> lstCastDocs = new List<dto.CastigoJudicialCliente>();
                dao.CabeceraReporte.TraeEmpresa(obj.Encabezado);
                dao.CastigoJudicial.TraeTitulo(obj);
                dao.CastigoJudicial.ListarCastigosDetalle(obj, lstCastDocs);

                foreach (dto.CastigoJudicialCliente doc in lstCastDocs)
                {
                    Transformador objTransformador = new Transformador();

                    XmlSerializer xs = new XmlSerializer(doc.GetType());
                    string xmlString;
                    using (StringWriter swr = new StringWriter())
                    {
                        xs.Serialize(swr, doc);
                        xmlString = swr.ToString();
                    }
                    
                    objTransformador.TransformXml(doc, @"C:\ArchivosSalida\castigo_cliente_judicial.xsl", @"C:\ArchivosSalida\castigo_cliente_judicial_" + doc.CbcDesde + "_" + doc.NumRegistro + ".fo");
                    Generador objGenerador = new Generador(@"C:\ArchivosSalida\castigo_cliente_judicial_" + doc.CbcDesde + "_" + doc.NumRegistro + ".fo", @"C:\ArchivosSalida\castigo_cliente_judicial_" + doc.CbcDesde + "_" + doc.NumRegistro + ".pdf");
                    objGenerador.XSLToPDF();
                }

            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Reportes.bcp.Cartera.TraeDevolucionDocumentosCliente", obj.Pclid);
                throw ex;
            }

            return "";
        }

        public static string TraeCastigoJudicialManual(dto.CastigoJudicialCliente obj)
        {
            obj.Encabezado.Codemp = obj.Codemp;
            obj.Encabezado.Codsuc = obj.Codsuc;
            obj.RutAbogado = Dimol.bcp.Funciones.formatearRut(obj.RutAbogado);

            try
            {
                List<dto.CastigoJudicialCliente> lstCastDocs = new List<dto.CastigoJudicialCliente>();
                dao.CabeceraReporte.TraeEmpresa(obj.Encabezado);
                dao.CastigoJudicial.TraeTitulo(obj);
                dao.CastigoJudicial.ListarCastigosDetalleManual(obj, lstCastDocs);

                foreach (dto.CastigoJudicialCliente doc in lstCastDocs)
                {
                    Transformador objTransformador = new Transformador();

                    XmlSerializer xs = new XmlSerializer(doc.GetType());
                    string xmlString;
                    using (StringWriter swr = new StringWriter())
                    {
                        xs.Serialize(swr, doc);
                        xmlString = swr.ToString();
                    }

                    objTransformador.TransformXml(doc, @"C:\ArchivosSalida\castigo_cliente_judicial.xsl", @"C:\ArchivosSalida\castigo_cliente_judicial_" + doc.CbcDesde + "_" + doc.NumRegistro + ".fo");
                    Generador objGenerador = new Generador(@"C:\ArchivosSalida\castigo_cliente_judicial_" + doc.CbcDesde + "_" + doc.NumRegistro + ".fo", @"C:\ArchivosSalida\castigo_cliente_judicial_" + doc.CbcDesde + "_" + doc.NumRegistro + ".pdf");
                    objGenerador.XSLToPDF();
                }

            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Reportes.bcp.Cartera.TraeCastigoJudicialManual", obj.Pclid);
                throw ex;
            }

            return "";
        }

        public static string TraeCastigoJudicial2(dto.CastigoJudicial2 obj)
        {
            obj.Encabezado.Codemp = obj.Codemp;
            obj.Encabezado.Codsuc = obj.Codsuc;
            obj.RutAbogado = Dimol.bcp.Funciones.formatearRut(obj.RutAbogado);

            try
            {
                
                dao.CabeceraReporte.TraeEmpresa(obj.Encabezado);
                dao.CastigoJudicial2.TraeTitulo(obj);
                dao.CastigoJudicial2.ListarCastigosDetalle(obj);

              
                    Transformador objTransformador = new Transformador();

                    XmlSerializer xs = new XmlSerializer(obj.GetType());
                    string xmlString;
                    using (StringWriter swr = new StringWriter())
                    {
                        xs.Serialize(swr, obj);
                        xmlString = swr.ToString();
                    }

                    objTransformador.TransformXml(obj, @"C:\ArchivosSalida\castigo_cliente_judicial2.xsl", @"C:\ArchivosSalida\castigo_cliente_judicial2_" + obj.CbcDesde + ".fo");
                    Generador objGenerador = new Generador(@"C:\ArchivosSalida\castigo_cliente_judicial2_" + obj.CbcDesde + ".fo", @"C:\ArchivosSalida\castigo_cliente_judicial2_" + obj.CbcDesde + ".pdf");
                    objGenerador.XSLToPDF();               

            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Reportes.bcp.Cartera.TraeDevolucionDocumentosCliente", obj.Pclid);
                throw ex;
            }

            return "";
        }

        public static string TraeCastigoJudicialAsegurado(dto.CastigoJudicialCliente obj)
        {
            obj.Encabezado.Codemp = obj.Codemp;
            obj.Encabezado.Codsuc = obj.Codsuc;
            obj.RutAbogado = Dimol.bcp.Funciones.formatearRut(obj.RutAbogado);

            try
            {
                List<dto.CastigoJudicialCliente> lstCastDocs = new List<dto.CastigoJudicialCliente>();
                dao.CabeceraReporte.TraeEmpresa(obj.Encabezado);
                dao.CastigoJudicial.TraeTitulo(obj);
                dao.CastigoJudicial.ListarCastigosDetalle(obj, lstCastDocs);

                foreach (dto.CastigoJudicialCliente doc in lstCastDocs)
                {
                    Transformador objTransformador = new Transformador();

                    XmlSerializer xs = new XmlSerializer(doc.GetType());
                    string xmlString;
                    using (StringWriter swr = new StringWriter())
                    {
                        xs.Serialize(swr, doc);
                        xmlString = swr.ToString();
                    }
                    
                    objTransformador.TransformXml(doc, @"C:\ArchivosSalida\castigo_asegurado_judicial.xsl", @"C:\ArchivosSalida\castigo_asegurado_judicial_" + doc.CbcDesde + "_" + doc.NumRegistro + ".fo");
                    Generador objGenerador = new Generador(@"C:\ArchivosSalida\castigo_asegurado_judicial_" + doc.CbcDesde + "_" + doc.NumRegistro + ".fo", @"C:\ArchivosSalida\castigo_asegurado_judicial_" + doc.CbcDesde + "_" + doc.NumRegistro + ".pdf");
                    objGenerador.XSLToPDF();
                }

            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Reportes.bcp.Cartera.TraeDevolucionDocumentosCliente", obj.Pclid);
                throw ex;
            }

            return "";
        }

        public static string TraeCastigoJudicialAseguradoDoc(dto.CastigoJudicialAsegurado obj)
        {
            dao.CastigoJudicial.ListarCastigosAseguradoDetalle(obj);
            return "";
        }

        public static string TraeDevolucionDocumentosCliente(dto.DevolucionDocumentosCliente obj)
        {
            obj.Encabezado.Codemp = obj.Codemp;
            obj.Encabezado.Codsuc = obj.Codsuc;

            try
            {
                List<dto.DevolucionDocumentosCliente> lstDevDocs = new List<dto.DevolucionDocumentosCliente>();
                dao.CabeceraReporte.TraeEmpresa(obj.Encabezado);
                dao.DevolucionDocumentosCliente.TraeTitulo(obj);
                dao.DevolucionDocumentosCliente.ListarDocumentosDetalle(obj, lstDevDocs);

                foreach (dto.DevolucionDocumentosCliente doc in lstDevDocs)
                {                    
                    Transformador objTransformador = new Transformador();

                    XmlSerializer xs = new XmlSerializer(doc.GetType());
                    string xmlString;
                    using (StringWriter swr = new StringWriter())
                    {
                        xs.Serialize(swr, doc);
                        xmlString = swr.ToString();
                    }
                   
                    objTransformador.TransformXml(doc, @"C:\ArchivosSalida\devolucion_documentos_cliente.xsl", @"C:\ArchivosSalida\devolucion_documentos_cliente_" + doc.Cbcnumero + "_" + doc.NumRegistro + ".fo");
                    Generador objGenerador = new Generador(@"C:\ArchivosSalida\devolucion_documentos_cliente_" + doc.Cbcnumero + "_" + doc.NumRegistro + ".fo", @"C:\ArchivosSalida\devolucion_documentos_cliente_" + doc.Cbcnumero + "_" + doc.NumRegistro + ".pdf");
                    objGenerador.XSLToPDF();
                }

            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Reportes.bcp.Cartera.TraeDevolucionDocumentosCliente", obj.Pclid);
                throw ex;
            }

            return "";
        }

        public static string TraeDevolucionDocumentosLista(dto.DevolucionDocumentos obj)
        {
            obj.Encabezado.Codemp = obj.Codemp;
            obj.Encabezado.Codsuc = obj.Codsuc;

            try
            {
                dao.CabeceraReporte.TraeEmpresa(obj.Encabezado);
                dao.DevolucionDocumentos.TraeTitulo(obj);
                dao.DevolucionDocumentos.ListarDocumentosDetalle(obj);
                Transformador objTransformador = new Transformador();

                XmlSerializer xs = new XmlSerializer(obj.GetType());
                string xmlString;
                using (StringWriter swr = new StringWriter())
                {
                    xs.Serialize(swr, obj);
                    xmlString = swr.ToString();
                }

                objTransformador.TransformXml(obj, @"C:\ArchivosSalida\devolucion_documentos_lista.xsl", @"C:\ArchivosSalida\devolucion_documentos_lista.fo");
                Generador objGenerador = new Generador(@"C:\ArchivosSalida\devolucion_documentos_lista.fo", @"C:\ArchivosSalida\devolucion_documentos_lista.pdf");
                objGenerador.XSLToPDF();

            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Reportes.bcp.Cartera.TraeDevolucionDocumentosLista", obj.Pclid);
                throw ex;
            }

            return "";
        }

        public static string TraeRecepcionDocumentos(dto.RecepcionDocumentos obj)
        {
            obj.Encabezado.Codemp = obj.Codemp;
            obj.Encabezado.Codsuc = obj.Codsuc;

            try
            {
                dao.CabeceraReporte.TraeEmpresa(obj.Encabezado);
                dao.RecepcionDocumentos.TraeTitulo(obj);
                dao.RecepcionDocumentos.ListarDocumentosDetalle(obj);
                Transformador objTransformador = new Transformador();

                XmlSerializer xs = new XmlSerializer(obj.GetType());
                string xmlString;
                using (StringWriter swr = new StringWriter())
                {
                    xs.Serialize(swr, obj);
                    xmlString = swr.ToString();
                }

                objTransformador.TransformXml(obj, @"C:\ArchivosSalida\recepcion_documentos.xsl", @"C:\ArchivosSalida\recepcion_documentos.fo");
                Generador objGenerador = new Generador(@"C:\ArchivosSalida\recepcion_documentos.fo", @"C:\ArchivosSalida\recepcion_documentos.pdf");
                objGenerador.XSLToPDF();

            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Reportes.bcp.Cartera.TraeRecepcionDocumentos", obj.Pclid);
                throw ex;
            }

            return "";
        }

        public static bool TraeBuscarFacturas(dto.BuscarFactura obj)
        {
          
            bool salida = false;
            try
            {
                dao.BuscarFactura.ListarDocumentosDetalle(obj);                         
            }
            catch (Exception ex)
            {                
                throw ex;
            }
            return salida;
        }

        public static bool TraeMutualManual(dto.MutualManual obj)
        {
            //obj.Encabezado.Codemp = obj.Codemp;
            //obj.Encabezado.Codsuc = obj.Sucid;
            //string ruta = dao.Cartera.RutaReportes(obj.Pagina, obj.IdReporte, obj.Idioma);
            bool salida = false;
            string ruta = @"C:\ArchivosSalida\Archivos_Mutual\" + DateTime.Now.ToString("dd-MM-yyyy") + @"\";
            try
            {
                //dao.CabeceraReporte.TraeEmpresa(obj.Encabezado);
                //dao.ResumenGestiones.TraeTitulo(obj);
                dao.MutualManual.ListarDocumentosDetalle(obj);

                /*  string archOrigen = @"C:\ArchivosSalida\trekking_cartera_general.pdf";
                  string archDestino = @"D:\Archivos\trekk.pdf";

                  System.IO.File.Copy(archOrigen, archDestino, true);
                  */

                if (!Directory.Exists(ruta))
                {
                    DirectoryInfo raiz = Directory.CreateDirectory(ruta);
                    raiz = Directory.CreateDirectory(ruta + @"Fo\");
                }

                 foreach (dto.MutualManualCliente objCli in obj.lstCli)
                 {
                     string path = ruta + objCli.Rut.Replace("-", "");
                     DirectoryInfo di = Directory.CreateDirectory(path);

                     Transformador objTransformador = new Transformador();
                     objTransformador.TransformXml(objCli, @"C:\ArchivosSalida\mutual_manual.xsl", ruta + @"Fo\mutual_manual" + objCli.Rut.Replace("-", "") + ".fo");
                     Generador objGenerador = new Generador(ruta + @"Fo\mutual_manual" + objCli.Rut.Replace("-", "") + ".fo", path + @"\detalle.pdf");
                     salida = objGenerador.XSLToPDF();
                 }

            }
            catch (Exception ex)
            {
                //Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Reportes.bcp.Cartera.TraeMutualManual", obj.Pclid);
                throw ex;
            }
            return salida;
        }

        public static bool TraeMutualManual2(dto.MutualManual obj)
        {

            bool salida = false;
            
            try
            {
                dao.MutualManual.ListarDocumentosDetalle(obj);

                if (!Directory.Exists(obj.Ruta))
                {
                    DirectoryInfo raiz = Directory.CreateDirectory(obj.Ruta);
                    raiz = Directory.CreateDirectory(obj.Ruta + @"Fo\");
                }

                foreach (dto.MutualManualCliente objCli in obj.lstCli)
                {
                    string path = obj.Ruta + objCli.Rut.Replace("-", "");
                    DirectoryInfo di = Directory.CreateDirectory(path);

                    Transformador objTransformador = new Transformador();
                    objTransformador.TransformXml(objCli, obj.PathArchivo, obj.Ruta + @"Fo\mutual_manual" + objCli.Rut.Replace("-", "") + ".fo");
                    Generador objGenerador = new Generador(obj.Ruta + @"Fo\mutual_manual" + objCli.Rut.Replace("-", "") + ".fo", path + @"\detalle0.pdf");
                    salida = objGenerador.XSLToPDF();

                    System.IO.File.Delete(obj.Ruta + @"Fo\mutual_manual" + objCli.Rut.Replace("-", "") + ".fo");
                }

                System.IO.Directory.Delete(obj.Ruta + @"Fo\");

            }
            catch (Exception ex)
            {
                //Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Reportes.bcp.Cartera.TraeMutualManual", obj.Pclid);
                throw ex;
            }
            return salida;
        }

        public static bool TraeResumenGestiones(dto.ResumenGestiones obj)
        {
            obj.Encabezado.Codemp = obj.Codemp;
            obj.Encabezado.Codsuc = obj.Sucid;
            string ruta = dao.Cartera.RutaReportes(obj.Pagina, obj.IdReporte, obj.Idioma);
            bool salida = false;
            try
            {
                dao.CabeceraReporte.TraeEmpresa(obj.Encabezado);
                dao.ResumenGestiones.TraeTitulo(obj);
                dao.ResumenGestiones.ListarDocumentosDetalle(obj);
                Transformador objTransformador = new Transformador();
                objTransformador.TransformXml(obj, ruta, obj.PathArchivo + ".fo");
                Generador objGenerador = new Generador(obj.PathArchivo + ".fo", obj.PathArchivo);
                salida=objGenerador.XSLToPDF();
            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Reportes.bcp.Cartera.TraeResumenGestiones", obj.Pclid);
                throw ex;
            }
            return salida;
        }

        public static bool TraeTrekkingCartera(dto.TrekkingCartera obj)
        {
            obj.Encabezado.Codemp = obj.Codemp;
            obj.Encabezado.Codsuc = obj.Sucid;
            string ruta = dao.Cartera.RutaReportes(obj.Pagina, obj.IdReporte, obj.Idioma);
            bool salida = false;
            try
            {
                dao.CabeceraReporte.TraeEmpresa(obj.Encabezado);
                dao.TrekkingCartera.TraeTitulo(obj);
                dao.TrekkingCartera.ListarCarteraDetalle(obj);
                Transformador objTransformador = new Transformador();
                objTransformador.TransformXml(obj, ruta, obj.PathArchivo + ".fo");
                Generador objGenerador = new Generador(obj.PathArchivo + ".fo", obj.PathArchivo);
                salida = objGenerador.XSLToPDF();
            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Reportes.bcp.Cartera.TraeTrekkingCartera", obj.Pclid);
                throw ex;
            }
            return salida;
        }

        public static string TraeInformeRemesa(dto.InformeRemesa obj)
        {
            obj.Encabezado.Codemp = obj.Codemp;
            obj.Encabezado.Codsuc = obj.Sucid;
            try
            {
                dao.CabeceraReporte.TraeEmpresa(obj.Encabezado);
                dao.InformeRemesa.TraeTitulo(obj);
                dao.InformeRemesa.ListarDocumentosDetalle(obj);
                Transformador objTransformador = new Transformador();
                objTransformador.TransformXml(obj, @"d:\informe_remesa.xsl", @"D:\informe_remesa.fo");
                Generador objGenerador = new Generador(@"D:\informe_remesa.fo", @"D:\informe_remesa.pdf");
                objGenerador.XSLToPDF();
            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Reportes.bcp.Cartera.TraeInformeRemesa", obj.Numero);
                throw ex;
            }
            return "";
        }

        public static bool TraeLiquidacionCochaMutual(dto.Liquidacion obj)
        {
            bool salida = false;
            obj.Encabezado.Codemp = obj.Codemp;
            obj.Encabezado.Codsuc = obj.Sucid;
            try
            {
                dao.CabeceraReporte.TraeEmpresa(obj.Encabezado);
                dao.Liquidacion.TraeTitulo(obj);
                dao.Liquidacion.ListarDocumentosDetalleTodo(obj);
                obj.FechaEmisionCorta = DateTime.Today;
                Transformador objTransformador = new Transformador();
                objTransformador.TransformXml(obj, @"d:\liquidacion_cocha_mutual.xsl", obj.PathArchivo + ".fo");// @"D:\liquidacion_cocha_mutual.fo");
                Generador objGenerador = new Generador(obj.PathArchivo + ".fo", obj.PathArchivo);
                salida = objGenerador.XSLToPDF();
            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Reportes.bcp.Cartera.TraeLiquidacionCochaMutual", obj.Ctcid);
                throw ex;
            }
            return salida;
        }

        public static bool TraeLiquidacionMutual(dto.Liquidacion obj)
        {
            bool salida = false;
            obj.Encabezado.Codemp = obj.Codemp;
            obj.Encabezado.Codsuc = obj.Sucid;
            string ruta = dao.Cartera.RutaReportes(obj.Pagina, obj.IdReporte, obj.Idioma);
            try
            {
                dao.CabeceraReporte.TraeEmpresa(obj.Encabezado);
                dao.Liquidacion.TraeTitulo(obj);
                dao.Liquidacion.ListarDocumentosDetalleTodo(obj);
                obj.FechaEmisionCorta = DateTime.Today;
                Transformador objTransformador = new Transformador();
                objTransformador.TransformXml(obj, ruta, obj.PathArchivo + ".fo");// @"D:\liquidacion_cocha_mutual.fo");
                Generador objGenerador = new Generador(obj.PathArchivo + ".fo", obj.PathArchivo);
                salida = objGenerador.XSLToPDF();
            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Reportes.bcp.Cartera.TraeLiquidacionMutual", obj.Ctcid);
                throw ex;
            }
            return salida;
        }

        public static bool TraeLiquidacionMasiva(dto.Liquidacion obj)
        {
            bool salida = false;
            obj.Encabezado.Codemp = obj.Codemp;
            obj.Encabezado.Codsuc = obj.Sucid;
            string ruta = dao.Cartera.RutaReportes(obj.Pagina, obj.IdReporte, obj.Idioma);
            try
            {
                dao.CabeceraReporte.TraeEmpresa(obj.Encabezado);
                dao.Liquidacion.TraeTitulo(obj);
                dao.Liquidacion.ListarDocumentosDetalle(obj);
                obj.FechaEmisionCorta = DateTime.Today;
                Transformador objTransformador = new Transformador();
                objTransformador.TransformXml(obj, ruta, obj.PathArchivo + ".fo");// @"D:\liquidacion_cocha_mutual.fo");
                Generador objGenerador = new Generador(obj.PathArchivo + ".fo", obj.PathArchivo);
                salida = objGenerador.XSLToPDF();
            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Reportes.bcp.Cartera.TraeLiquidacionAgrorama", obj.Ctcid);
                throw ex;
            }
            return salida;
        }

        public static bool TraeLiquidacionDura(dto.LiquidacionDura obj)
        {
            bool salida = false;
            obj.Encabezado.Codemp = obj.Codemp;
            obj.Encabezado.Codsuc = obj.Sucid;
            string ruta = dao.Cartera.RutaReportes(obj.Pagina, obj.IdReporte, obj.Idioma);
            try
            {
                dao.CabeceraReporte.TraeEmpresa(obj.Encabezado);
                dao.Liquidacion.TraeTitulo(obj);
                dao.Liquidacion.ListarDocumentosDetalleDura(obj);
                obj.FechaEmisionCorta = DateTime.Today;
                Transformador objTransformador = new Transformador();
                objTransformador.TransformXml(obj, ruta, obj.PathArchivo + ".fo");// @"D:\liquidacion_cocha_mutual.fo");
                Generador objGenerador = new Generador(obj.PathArchivo + ".fo", obj.PathArchivo);
                salida = objGenerador.XSLToPDF();
            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Reportes.bcp.Cartera.TraeLiquidacionDura", obj.Ctcid);
                throw ex;
            }
            return salida;
        }

        public static bool TraeLiquidacionAgrorama(dto.Liquidacion obj)
        {
            bool salida = false;
            obj.Encabezado.Codemp = obj.Codemp;
            obj.Encabezado.Codsuc = obj.Sucid;
            try
            {
                dao.CabeceraReporte.TraeEmpresa(obj.Encabezado);
                dao.Liquidacion.TraeTitulo(obj);
                dao.Liquidacion.ListarDocumentosDetalleTodo(obj);
                obj.FechaEmisionCorta = DateTime.Today;
                Transformador objTransformador = new Transformador();
                objTransformador.TransformXml(obj, @"d:\liquidacion_agrorama.xsl", obj.PathArchivo + ".fo");// @"D:\liquidacion_cocha_mutual.fo");
                Generador objGenerador = new Generador(obj.PathArchivo + ".fo", obj.PathArchivo);
                salida = objGenerador.XSLToPDF();
            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Reportes.bcp.Cartera.TraeLiquidacionAgrorama", obj.Ctcid);
                throw ex;
            }
            return salida;
        }

        public static bool TraeTortaAgrupada(dto.TortaAgrupada obj)
        {
            bool salida = false;
            obj.Encabezado.Codemp = obj.Codemp;
            obj.Encabezado.Codsuc = obj.Sucid;
            try
            {
                dao.CabeceraReporte.TraeEmpresa(obj.Encabezado);
                dao.TortaAgrupada.TraeTitulo(obj);
                dao.TortaAgrupada.ListarDocumentosDetalleTodo(obj);
                obj.FechaEmisionCorta = DateTime.Today;
                Transformador objTransformador = new Transformador();
                objTransformador.TransformXml(obj, obj.PathArchivo + ".xsl", obj.PathArchivo + ".fo");
                Generador objGenerador = new Generador(obj.PathArchivo + ".fo", obj.PathArchivo + ".pdf");
                salida = objGenerador.XSLToPDF();
            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Reportes.bcp.Cartera.TraeLiquidacionAgrorama", obj.Pclid );
                throw ex;
            }
            return salida;
        }

        public static string TraeLiquidacionCochaMutualXLS(dto.Liquidacion obj)
        {
            string salida = "";
            obj.Encabezado.Codemp = obj.Codemp;
            obj.Encabezado.Codsuc = obj.Sucid;
            try
            {
                dao.CabeceraReporte.TraeEmpresa(obj.Encabezado);
                dao.Liquidacion.TraeTitulo(obj);
                dao.Liquidacion.ListarDocumentosDetalleTodo(obj);
                obj.FechaEmisionCorta = DateTime.Today;
                Transformador objTransformador = new Transformador();
                salida = objTransformador.TransformXLS(obj, obj.PathArchivo +"Reportes\\XSL\\liquidacion_cocha_mutual_xls.xsl");
            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Reportes.bcp.Cartera.TraeLiquidacionCochaMutual", obj.Ctcid);
                throw ex;
            }
            return salida;
        }

        public static bool TraeReporteCancelacion(dto.ReporteCancelacion obj)
        {
            bool salida = false;
            obj.Encabezado.Codemp = obj.Codemp;
            obj.Encabezado.Codsuc = obj.Sucid;
            string ruta = dao.Cartera.RutaReportes(obj.Pagina, obj.IdReporte, obj.Idioma);
            try
            {
                dao.CabeceraReporte.TraeEmpresa(obj.Encabezado);
                dao.ReporteCancelacion.TraeTitulo(obj);
                dao.ReporteCancelacion.ListarDocumentosReporteCancelacion(obj);
                obj.FechaEmisionCorta = DateTime.Today;
                Transformador objTransformador = new Transformador();
                objTransformador.TransformXml(obj, ruta, obj.PathArchivo + ".fo");// @"D:\liquidacion_cocha_mutual.fo");
                Generador objGenerador = new Generador(obj.PathArchivo + ".fo", obj.PathArchivo);
                salida = objGenerador.XSLToPDF();
            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Reportes.bcp.Cartera.ReporteCancelacion", obj.Ctcid);
                throw ex;
            }
            return salida;
        }

        public static bool TraeLiquidacionMutualLey(dto.LiquidacionDura obj)
        {
            bool salida = false;
            obj.Encabezado.Codemp = obj.Codemp;
            obj.Encabezado.Codsuc = obj.Sucid;
            string ruta = dao.Cartera.RutaReportes(obj.Pagina, obj.IdReporte, obj.Idioma);
            try
            {
                dao.CabeceraReporte.TraeEmpresa(obj.Encabezado);
                dao.Liquidacion.TraeTitulo(obj);
                dao.Liquidacion.ListarDocumentosDetalleMutualLey(obj);
                obj.FechaEmisionCorta = DateTime.Today;
                Transformador objTransformador = new Transformador();
                objTransformador.TransformXml(obj, ruta, obj.PathArchivo + ".fo");
                Generador objGenerador = new Generador(obj.PathArchivo + ".fo", obj.PathArchivo);
                salida = objGenerador.XSLToPDF();
            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Reportes.bcp.Cartera.TraeLiquidacionDura", obj.Ctcid);
                throw ex;
            }
            return salida;
        }

        public static bool TraeInformePrejudicial(dto.InformePrejudicial obj)
        {
            bool salida = false;
            obj.Encabezado.Codemp = obj.Codemp;
            obj.Encabezado.Codsuc = obj.Sucid;
            string ruta = dao.Cartera.RutaReportes(obj.Pagina, obj.IdReporte, obj.Idioma);//@"d:\archivos\reportes\xsl\informe_prejudicial.xsl";//
            try
            {
                dao.CabeceraReporte.TraeEmpresa(obj.Encabezado);
                dao.InformePrejudicial.TraeTitulo(obj);
                dao.InformePrejudicial.ListarDocumentosDetalle(obj);
                obj.FechaEmisionCorta = DateTime.Today;
                Transformador objTransformador = new Transformador();
                objTransformador.TransformXml(obj, ruta, obj.PathArchivo + ".fo");// @"D:\liquidacion_cocha_mutual.fo");
                Generador objGenerador = new Generador(obj.PathArchivo + ".fo", obj.PathArchivo);
                salida = objGenerador.XSLToPDF();
            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Reportes.bcp.Cartera.TraeLiquidacionDura", obj.Ctcid);
                throw ex;
            }
            return salida;
        }

        public static bool TraeInformePrejudicialAsegurado(dto.InformePrejudicial obj)
        {
            bool salida = false;
            obj.Encabezado.Codemp = obj.Codemp;
            obj.Encabezado.Codsuc = obj.Sucid;
            string ruta = dao.Cartera.RutaReportes(obj.Pagina, obj.IdReporte, obj.Idioma);//@"d:\archivos\reportes\xsl\informe_prejudicial.xsl";//
            try
            {
                dao.CabeceraReporte.TraeEmpresa(obj.Encabezado);
                dao.InformePrejudicial.TraeTitulo(obj);
                dao.InformePrejudicial.ListarDocumentosDetalleAsegurado(obj);
                obj.FechaEmisionCorta = DateTime.Today;
                Transformador objTransformador = new Transformador();
                objTransformador.TransformXml(obj, ruta, obj.PathArchivo + ".fo");// @"D:\liquidacion_cocha_mutual.fo");
                Generador objGenerador = new Generador(obj.PathArchivo + ".fo", obj.PathArchivo);
                salida = objGenerador.XSLToPDF();
            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Reportes.bcp.Cartera.TraeLiquidacionDura", obj.Ctcid);
                throw ex;
            }
            return salida;
        }

        public static bool TraeInformePrejudicialCodigoCarga(dto.InformePrejudicial obj)
        {
            bool salida = false;
            obj.Encabezado.Codemp = obj.Codemp;
            obj.Encabezado.Codsuc = obj.Sucid;
            string ruta = dao.Cartera.RutaReportes(obj.Pagina, obj.IdReporte, obj.Idioma);//@"d:\archivos\reportes\xsl\informe_prejudicial_codigo_carga.xsl";//
            try
            {
                dao.CabeceraReporte.TraeEmpresa(obj.Encabezado);
                dao.InformePrejudicial.TraeTitulo(obj);
                dao.InformePrejudicial.ListarDocumentosDetalleAseguradoCodigoCarga(obj);
                obj.FechaEmisionCorta = DateTime.Today;
                Transformador objTransformador = new Transformador();
                objTransformador.TransformXml(obj, ruta, obj.PathArchivo + ".fo");// @"D:\liquidacion_cocha_mutual.fo");
                Generador objGenerador = new Generador(obj.PathArchivo + ".fo", obj.PathArchivo);
                salida = objGenerador.XSLToPDF();
            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Reportes.bcp.Cartera.TraeLiquidacionDura", obj.Ctcid);
                throw ex;
            }
            return salida;
        }
        #endregion
    }
}
