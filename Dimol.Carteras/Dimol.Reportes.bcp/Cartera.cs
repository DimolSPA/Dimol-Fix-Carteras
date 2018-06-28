using Dimol.dto;
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
using HiQPdf;

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

        public static string TraeCastigoPrejudicialCliente(dto.CastigoPrejudicialCliente obj)
        {
            obj.Encabezado.Codemp = obj.Codemp;
            obj.Encabezado.Codsuc = obj.Codsuc;
            string ruta = dao.Cartera.RutaReportes(obj.Pagina, obj.IdReporte, obj.Idioma);//@"d:\archivos\reportes\xsl\informe_prejudicial.xsl";//
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

                    objTransformador.TransformXml(doc, ruta, obj.PathArchivo + ".fo");
                    Generador objGenerador = new Generador(obj.PathArchivo + ".fo", obj.PathArchivo);
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

        public static string TraeCastigoMasivo(dto.CastigoPrejudicialCliente obj)
        {
            obj.Encabezado.Codemp = obj.Codemp;
            obj.Encabezado.Codsuc = obj.Codsuc;
            string ruta = @"d:\archivos\reportes\xsl\castigo_masivo_prejudicial.xsl";// dao.Cartera.RutaReportes(obj.Pagina, obj.IdReporte, obj.Idioma);//
            try
            {
                List<dto.CastigoPrejudicialCliente> lstDevDocs = new List<dto.CastigoPrejudicialCliente>();
                dao.CabeceraReporte.TraeEmpresa(obj.Encabezado);
                dao.CastigoPrejudicial.TraeTitulo(obj);
                dao.CastigoPrejudicial.ListarCastigosMasivoDetalle(obj, lstDevDocs);

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

                    objTransformador.TransformXml(doc, ruta, obj.PathArchivo + ".fo");
                    Generador objGenerador = new Generador(obj.PathArchivo + ".fo", obj.PathArchivo);
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

        public static bool TraeCastigoPrejudicialClienteNew(dto.CastigoPrejudicialCliente obj)
        {
            obj.Encabezado.Codemp = obj.Codemp;
            obj.Encabezado.Codsuc = obj.Codsuc;
            bool salida = false;
            string ruta = dao.Cartera.RutaReportes(obj.Pagina, obj.IdReporte, obj.Idioma);//@"d:\archivos\reportes\xsl\informe_prejudicial.xsl";//
            try
            {
                List<dto.CastigoPrejudicialCliente> lstDevDocs = new List<dto.CastigoPrejudicialCliente>();
                dao.CabeceraReporte.TraeEmpresa(obj.Encabezado);
                dao.CastigoPrejudicial.TraeTitulo(obj);
                dao.CastigoPrejudicial.ListarCastigosDetalleNew(obj, lstDevDocs);

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

                    objTransformador.TransformXml(doc, ruta, obj.PathArchivo + ".fo");
                    Generador objGenerador = new Generador(obj.PathArchivo + ".fo", obj.PathArchivo);
                    salida = objGenerador.XSLToPDF();

                }

            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Reportes.bcp.Cartera.TraeDevolucionDocumentosCliente", obj.Pclid);
                throw ex;
            }

            return salida;
        }
        public static bool TraeCastigoPrejudicialClienteNormal(dto.CastigoPrejudicialCliente obj)
        {
            obj.Encabezado.Codemp = obj.Codemp;
            obj.Encabezado.Codsuc = obj.Codsuc;
            bool salida = false;
            string ruta = dao.Cartera.RutaReportes(obj.Pagina, obj.IdReporte, obj.Idioma);//@"d:\archivos\reportes\xsl\informe_prejudicial.xsl";//
            try
            {
                List<dto.CastigoPrejudicialCliente> lstDevDocs = new List<dto.CastigoPrejudicialCliente>();
                dao.CabeceraReporte.TraeEmpresa(obj.Encabezado);
                dao.CastigoPrejudicial.TraeTitulo(obj);
                dao.CastigoPrejudicial.ListarCastigosDetalleNormal(obj, lstDevDocs);

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

                    objTransformador.TransformXml(doc, ruta, obj.PathArchivo + ".fo");
                    Generador objGenerador = new Generador(obj.PathArchivo + ".fo", obj.PathArchivo);
                    salida = objGenerador.XSLToPDF();

                }

            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Reportes.bcp.Cartera.TraeCastigoPrejudicialClienteNormal", obj.Pclid);
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

        public static bool TraeRecepcionDocumentos(dto.RecepcionDocumentos obj)
        {
            bool salida = false;
            obj.Encabezado.Codemp = obj.Codemp;
            obj.Encabezado.Codsuc = obj.Codsuc;
            string ruta = dao.Cartera.RutaReportes(obj.Pagina, obj.IdReporte, obj.Idioma);

            try
            {
                dao.CabeceraReporte.TraeEmpresa(obj.Encabezado);
                dao.RecepcionDocumentos.TraeTitulo(obj);
                dao.RecepcionDocumentos.ListarDocumentosDetalle(obj);
                Transformador objTransformador = new Transformador();
                objTransformador.TransformXml(obj, ruta, obj.PathArchivo + ".fo");
                Generador objGenerador = new Generador(obj.PathArchivo + ".fo", obj.PathArchivo);
                salida = objGenerador.XSLToPDF();

            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Reportes.bcp.Cartera.TraeRecepcionDocumentos", obj.Pclid);
                throw ex;
            }

            return salida;
        }

        public static bool TraeResumenGestiones(dto.ResumenGestiones obj)
        {
            obj.Encabezado.Codemp = obj.Codemp;
            obj.Encabezado.Codsuc = obj.Sucid;
            string ruta = dao.Cartera.RutaReportes(obj.Pagina, obj.IdReporte, obj.Idioma);
#if DEBUG //en local los xsl de los reportes están en C: y no en D:
            ruta = ruta.Replace("D:", "C:");
#endif
            bool salida = false;
            try
            {
                dao.CabeceraReporte.TraeEmpresa(obj.Encabezado);
                dao.ResumenGestiones.TraeTitulo(obj);
                dao.ResumenGestiones.ListarDocumentosDetalle(obj);
                Transformador objTransformador = new Transformador();
                objTransformador.TransformXml(obj, ruta, obj.PathArchivo + ".fo");
                Generador objGenerador = new Generador(obj.PathArchivo + ".fo", obj.PathArchivo);
                salida = objGenerador.XSLToPDF();
            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Reportes.bcp.Cartera.TraeResumenGestiones", obj.Pclid);
                throw ex;
            }
            return salida;
        }

        public static bool TraeResumenGestionesCastigoMasivo(dto.ResumenGestiones obj)
        {
            obj.Encabezado.Codemp = obj.Codemp;
            obj.Encabezado.Codsuc = obj.Sucid;
            string ruta = @"D:\Archivos\Reportes\XSL\resumen_gestiones_castigo.xsl";// dao.Cartera.RutaReportes(obj.Pagina, obj.IdReporte, obj.Idioma);
            bool salida = false;
            try
            {
                dao.CabeceraReporte.TraeEmpresa(obj.Encabezado);
                dao.ResumenGestiones.TraeTitulo(obj);
                dao.ResumenGestiones.ListarDocumentosDetalleCastigoMasivo(obj);
                Transformador objTransformador = new Transformador();
                objTransformador.TransformXml(obj, ruta, obj.PathArchivo + ".fo");
                Generador objGenerador = new Generador(obj.PathArchivo + ".fo", obj.PathArchivo);
                salida = objGenerador.XSLToPDF();
            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Reportes.bcp.Cartera.TraeResumenGestiones", obj.Pclid);
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
            string ruta = dao.Cartera.RutaReportes(obj.Pagina, obj.IdReporte, obj.Idioma);
            //ruta = @"D:\Archivos\Reportes\XSL\liquidacion_dura.xsl";
#if DEBUG //en local los xsl de los reportes están en C: y no en D:
            ruta = ruta.Replace("D:", "C:");
#endif
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
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Reportes.bcp.Cartera.TraeLiquidacionCochaMutual", obj.Ctcid);
                throw ex;
            }
            return salida;
        }

        public static bool TraeLiquidacionCochaMutualParcial(dto.Liquidacion obj)
        {
            bool salida = false;
            obj.Encabezado.Codemp = obj.Codemp;
            obj.Encabezado.Codsuc = obj.Sucid;
            string ruta = dao.Cartera.RutaReportes(obj.Pagina, obj.IdReporte, obj.Idioma);

            try
            {
                dao.CabeceraReporte.TraeEmpresa(obj.Encabezado);
                dao.Liquidacion.TraeTitulo(obj);
                dao.Liquidacion.ListarDocumentosDetalleParcial(obj);
                obj.FechaEmisionCorta = DateTime.Today;
                Transformador objTransformador = new Transformador();
                objTransformador.TransformXml(obj, ruta, obj.PathArchivo + ".fo");// @"D:\liquidacion_cocha_mutual.fo");
                Generador objGenerador = new Generador(obj.PathArchivo + ".fo", obj.PathArchivo);
                salida = objGenerador.XSLToPDF();
            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Reportes.bcp.Cartera.TraeLiquidacionCochaMutualParcial", obj.Ctcid);
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

#if DEBUG //en local los xsl de los reportes están en C: y no en D:
            ruta = ruta.Replace("D:", "C:");
#endif

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
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Reportes.bcp.Cartera.TraeLiquidacionMasiva", obj.Ctcid);
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

#if DEBUG //en local los xsl de los reportes están en C: y no en D:
            ruta = ruta.Replace("D:", "C:");
#endif

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
            string ruta = dao.Cartera.RutaReportes(obj.Pagina, obj.IdReporte, obj.Idioma);
            try
            {
                dao.CabeceraReporte.TraeEmpresa(obj.Encabezado);
                dao.TortaAgrupada.TraeTitulo(obj);
                dao.TortaAgrupada.ListarDocumentosDetalleTodo(obj);
                obj.FechaEmisionCorta = DateTime.Today;
                Transformador objTransformador = new Transformador();
                objTransformador.TransformXml(obj, ruta, obj.PathArchivo + ".fo");
                Generador objGenerador = new Generador(obj.PathArchivo + ".fo", obj.PathArchivo);
                salida = objGenerador.XSLToPDF();
            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Reportes.bcp.Cartera.TraeTortaAgrupada", obj.Pclid);
                throw ex;
            }
            return salida;
        }

        public static bool TraeTortaAgrorama(dto.TortaAgrupada obj)
        {
            bool salida = false;
            obj.Encabezado.Codemp = obj.Codemp;
            obj.Encabezado.Codsuc = obj.Sucid;
            string ruta = dao.Cartera.RutaReportes(obj.Pagina, obj.IdReporte, obj.Idioma);
            try
            {
                dao.CabeceraReporte.TraeEmpresa(obj.Encabezado);
                //dao.Liquidacion.TraeTitulo(obj);
                dao.TortaAgrupada.ListarDocumentosDetalleTodo(obj);
                obj.FechaEmisionCorta = DateTime.Today;
                Transformador objTransformador = new Transformador();
                objTransformador.TransformXml(obj, ruta, obj.PathArchivo + ".fo");// @"D:\liquidacion_cocha_mutual.fo");
                Generador objGenerador = new Generador(obj.PathArchivo + ".fo", obj.PathArchivo);
                salida = objGenerador.XSLToPDF();
            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Reportes.bcp.Cartera.TraeTortaAgrorama", obj.Pclid);
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
                salida = objTransformador.TransformXLS(obj, obj.PathArchivo + "Reportes\\XSL\\liquidacion_cocha_mutual_xls.xsl");
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

        public static bool TraeCastigoJudicial(dto.CastigoJudicialCliente obj)
        {
            bool salida = false;
            obj.Encabezado.Codemp = obj.Codemp;
            obj.Encabezado.Codsuc = obj.Codsuc;
            obj.RutAbogado = Dimol.bcp.Funciones.formatearRut(obj.RutAbogado);
            string ruta = dao.Cartera.RutaReportes(obj.Pagina, obj.IdReporte, obj.Idioma);
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

                    objTransformador.TransformXml(doc, ruta, obj.PathArchivo + ".fo");
                    Generador objGenerador = new Generador(obj.PathArchivo + ".fo", obj.PathArchivo);
                    salida = objGenerador.XSLToPDF();
                }

            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Reportes.bcp.Cartera.TraeDevolucionDocumentosCliente", obj.Pclid);
                throw ex;
            }

            return salida;
        }

        public static bool TraeCastigoJudicialNormal(dto.CastigoJudicialCliente obj)
        {
            bool salida = false;
            obj.Encabezado.Codemp = obj.Codemp;
            obj.Encabezado.Codsuc = obj.Codsuc;
            obj.RutAbogado = Dimol.bcp.Funciones.formatearRut(obj.RutAbogado);
            string ruta = dao.Cartera.RutaReportes(obj.Pagina, obj.IdReporte, obj.Idioma);
            try
            {
                List<dto.CastigoJudicialCliente> lstCastDocs = new List<dto.CastigoJudicialCliente>();
                dao.CabeceraReporte.TraeEmpresa(obj.Encabezado);
                dao.CastigoJudicial.TraeTitulo(obj);
                dao.CastigoJudicial.ListarCastigosDetalleNormal(obj, lstCastDocs);

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

                    objTransformador.TransformXml(doc, ruta, obj.PathArchivo + ".fo");
                    Generador objGenerador = new Generador(obj.PathArchivo + ".fo", obj.PathArchivo);
                    salida = objGenerador.XSLToPDF();
                }

            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Reportes.bcp.Cartera.TraeCastigoJudicialNormal", obj.Pclid);
                throw ex;
            }

            return salida;
        }

        public static bool TraeDevolucionDocumentosCliente(dto.DevolucionDocumentosCliente obj)
        {
            bool salida = false;
            obj.Encabezado.Codemp = obj.Codemp;
            obj.Encabezado.Codsuc = obj.Codsuc;
            string ruta = dao.Cartera.RutaReportes(obj.Pagina, obj.IdReporte, obj.Idioma);
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

                    objTransformador.TransformXml(doc, ruta, obj.PathArchivo + ".fo");
                    Generador objGenerador = new Generador(obj.PathArchivo + ".fo", obj.PathArchivo);
                    salida = objGenerador.XSLToPDF();
                }

            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Reportes.bcp.Cartera.TraeDevolucionDocumentosCliente", obj.Pclid);
                throw ex;
            }

            return salida;
        }

        public static bool TraeDevolucionDocumentosNormal(dto.DevolucionDocumentosCliente obj)
        {
            bool salida = false;
            obj.Encabezado.Codemp = obj.Codemp;
            obj.Encabezado.Codsuc = obj.Codsuc;
            string ruta = dao.Cartera.RutaReportes(obj.Pagina, obj.IdReporte, obj.Idioma);
            try
            {
                List<dto.DevolucionDocumentosCliente> lstDevDocs = new List<dto.DevolucionDocumentosCliente>();
                dao.CabeceraReporte.TraeEmpresa(obj.Encabezado);
                dao.DevolucionDocumentosCliente.TraeTitulo(obj);
                dao.DevolucionDocumentosCliente.ListarDocumentosDetalleNormal(obj, lstDevDocs);

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

                    objTransformador.TransformXml(doc, ruta, obj.PathArchivo + ".fo");
                    Generador objGenerador = new Generador(obj.PathArchivo + ".fo", obj.PathArchivo);
                    salida = objGenerador.XSLToPDF();
                }

            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Reportes.bcp.Cartera.TraeDevolucionDocumentosCliente", obj.Pclid);
                throw ex;
            }

            return salida;
        }

        public static void GeneraPDFSII(int ctcid, string ruta, string fileName)
        {
            try
            {
                // genero estado tributario
                var example_html = dao.Cartera.TraeHtmlSiiDeudor(ctcid).Replace("<br>", "<br/>").Replace("\n</td>", "")
                            .Replace("</tr>\n</table>", "</table>").Replace("<b/> \n<center>", "\n<center>")
                            .Replace("</center>\n<div", "<div").Replace("</br>", "").Replace("stc.html';\">", "stc.html';\" />");

                if (example_html != "")
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
                    PdfHtml html1 = new PdfHtml(location1.X, location1.Y, example_html, "");

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
                    System.IO.File.WriteAllBytes(rutFile, pdfBufferRut); ;
                    docPorRut.Close();
                    document.Close();
                }
            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Reportes.bcp.Cartera.GeneraPDFSII", ctcid);
                throw ex;
            }
            //return salida;
        }

        public static bool TraeLiquidacionMutualLeyParcial(dto.LiquidacionDura obj)
        {
            bool salida = false;
            obj.Encabezado.Codemp = obj.Codemp;
            obj.Encabezado.Codsuc = obj.Sucid;
            string ruta = dao.Cartera.RutaReportes(obj.Pagina, obj.IdReporte, obj.Idioma);
            try
            {
                dao.CabeceraReporte.TraeEmpresa(obj.Encabezado);
                dao.Liquidacion.TraeTitulo(obj);
                dao.Liquidacion.ListarDocumentosDetalleMutualLeyParcial(obj);
                obj.FechaEmisionCorta = DateTime.Today;
                Transformador objTransformador = new Transformador();
                objTransformador.TransformXml(obj, ruta, obj.PathArchivo + ".fo");
                Generador objGenerador = new Generador(obj.PathArchivo + ".fo", obj.PathArchivo);
                salida = objGenerador.XSLToPDF();
            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Reportes.bcp.Cartera.TraeLiquidacionMutualLeyParcial", obj.Ctcid);
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

        public static bool TraeInformePrejudicialOchenta(dto.InformePrejudicial obj)
        {
            bool salida = false;
            obj.Encabezado.Codemp = obj.Codemp;
            obj.Encabezado.Codsuc = obj.Sucid;
            string ruta = dao.Cartera.RutaReportes(obj.Pagina, obj.IdReporte, obj.Idioma);//@"d:\archivos\reportes\xsl\informe_prejudicial.xsl";//
            try
            {
                dao.CabeceraReporte.TraeEmpresa(obj.Encabezado);
                dao.InformePrejudicial.TraeTitulo(obj);
                dao.InformePrejudicial.ListarDocumentosDetalleOchenta(obj);
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

        public static string TraeInformeBajas(dto.InformeBajas obj)
        {
            string salida = "";
            obj.Encabezado.Codemp = obj.Codemp;
            obj.Encabezado.Codsuc = obj.Codsuc;
            string ruta = dao.Cartera.RutaReportes(obj.Pagina, obj.IdReporte, obj.Idioma);

            try
            {
                dao.CabeceraReporte.TraeEmpresa(obj.Encabezado);
                dao.InformeBajas.TraeTitulo(obj);
                dao.InformeBajas.ListarDetalleBajas(obj);

                Transformador objTransformador = new Transformador();
                salida = objTransformador.TransformXLS(obj, ruta);

            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Reportes.bcp.Cartera.TraeInformeBajas", obj.Pclid);
                throw ex;
            }
            return salida;
        }

        #endregion
    }
}
