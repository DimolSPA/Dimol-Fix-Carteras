using Dimol.dto;
using Dimol.PDF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Reportes.bcp
{
    public class Judicial
    {
        #region "Informe Judicial"
        public static bool TraeInformeJudicial(dto.InformeJudicial obj)
        {
            bool salida = false;
            obj.Encabezado.Codemp = obj.Codemp;
            obj.Encabezado.Codsuc = obj.Sucid;
            string ruta = dao.Cartera.RutaReportes(obj.Pagina, obj.IdReporte, obj.Idioma);//;//
            try
            {
                dao.CabeceraReporte.TraeEmpresa(obj.Encabezado);
                dao.InformeJudicial.TraeTitulo(obj);
                dao.InformeJudicial.ListarDocumentosInformeJudicial(obj);
                obj.FechaEmisionCorta = DateTime.Today;
                Transformador objTransformador = new Transformador();
                objTransformador.TransformXml(obj, ruta, obj.PathArchivo + ".fo");// @"D:\liquidacion_cocha_mutual.fo");
                Generador objGenerador = new Generador(obj.PathArchivo + ".fo", obj.PathArchivo);
                salida = objGenerador.XSLToPDF();
            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Reportes.bcp.Judicial.TraeLiquidacionDura", obj.Ctcid);
                throw ex;
            }
            return salida;
        }

        public static bool TraeInformeJudicialOchenta(dto.InformeJudicial obj)
        {
            bool salida = false;
            obj.Encabezado.Codemp = obj.Codemp;
            obj.Encabezado.Codsuc = obj.Sucid;
            string ruta = dao.Cartera.RutaReportes(obj.Pagina, obj.IdReporte, obj.Idioma);//;//
            try
            {
                dao.CabeceraReporte.TraeEmpresa(obj.Encabezado);
                dao.InformeJudicial.TraeTitulo(obj);
                dao.InformeJudicial.ListarDocumentosInformeJudicialOchenta(obj);
                obj.FechaEmisionCorta = DateTime.Today;
                Transformador objTransformador = new Transformador();
                objTransformador.TransformXml(obj, ruta, obj.PathArchivo + ".fo");// @"D:\liquidacion_cocha_mutual.fo");
                Generador objGenerador = new Generador(obj.PathArchivo + ".fo", obj.PathArchivo);
                salida = objGenerador.XSLToPDF();
            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Reportes.bcp.Judicial.TraeLiquidacionDura", obj.Ctcid);
                throw ex;
            }
            return salida;
        }

        public static List<Combobox> ListarAbogados(int codemp, int sucid)
        {
            return Dimol.Reportes.dao.ArbolJudicial.ListarAbogados(codemp, sucid);
        }

        public static int ListarAbogadosCount(int codemp, int sucid)
        {
            return Dimol.Reportes.dao.ArbolJudicial.ListarAbogadosCount(codemp, sucid);
        }

        public static string TraePrescripciones(dto.Prescripciones obj)
        {
            string salida = "";
            obj.Encabezado.Codemp = obj.Codemp;
            obj.Encabezado.Codsuc = obj.Codsuc;
            string ruta = dao.Cartera.RutaReportes(obj.Pagina, obj.IdReporte, obj.Idioma);

            try
            {
                dao.CabeceraReporte.TraeEmpresa(obj.Encabezado);
                dao.Prescripciones.TraeTitulo(obj);
                dao.Prescripciones.ListarDetallePrescripciones(obj);

                Transformador objTransformador = new Transformador();             
                salida = objTransformador.TransformXLS(obj, ruta);                

            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Reportes.bcp.Judicial.TraePrescripciones", obj.Pclid);
                throw ex;
            }
            return salida;
        }

        public static string TraeArbolJudicialXLS(dto.ArbolJudicial obj)
        {
            string salida = "";
            
            obj.Encabezado.Codemp = obj.Codemp;
            obj.Encabezado.Codsuc = obj.Codsuc;
            string ruta = dao.Cartera.RutaReportes(obj.Pagina, obj.IdReporte, obj.Idioma);

            try
            {
                dao.CabeceraReporte.TraeEmpresa(obj.Encabezado);
                dao.ArbolJudicial.TraeTitulo(obj);
                dao.ArbolJudicial.ListarArbolJudicialDetalle(obj);
                
                Transformador objTransformador = new Transformador();

                salida = objTransformador.TransformXLS(obj, ruta);
                
            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Reportes.bcp.Judicial.TraeArbolJudicial", obj.Pclid);
                throw ex;
            }
            return salida;
        }

        public static bool TraeArbolJudicial(dto.ArbolJudicial obj)
        {
            
            bool val = false;
            obj.Encabezado.Codemp = obj.Codemp;
            obj.Encabezado.Codsuc = obj.Codsuc;
            string ruta = dao.Cartera.RutaReportes(obj.Pagina, obj.IdReporte, obj.Idioma);

            try
            {
                dao.CabeceraReporte.TraeEmpresa(obj.Encabezado);
                dao.ArbolJudicial.TraeTitulo(obj);
                dao.ArbolJudicial.ListarArbolJudicialDetalle(obj);
                
                Transformador objTransformador = new Transformador();
                               
                objTransformador.TransformXml(obj, ruta.Substring(0, ruta.Length - 4) + "_pdf.xsl", obj.PathArchivo + ".fo");
                Generador objGenerador = new Generador(obj.PathArchivo + ".fo", obj.PathArchivo);
                val = objGenerador.XSLToPDF();

            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Reportes.bcp.Judicial.TraeArbolJudicial", obj.Pclid);
                throw ex;
            }
            return val;
        }

        public static bool TraeHojaTramiteCliente(dto.HojaTramite obj)
        {
            bool salida = false;
            obj.Encabezado.Codemp = obj.Codemp;
            obj.Encabezado.Codsuc = obj.IdSuc;
            string ruta = dao.Cartera.RutaReportes(obj.Pagina, obj.IdReporte, obj.Idioma);
            try
            {
                dao.CabeceraReporte.TraeEmpresa(obj.Encabezado);
                dao.HojaTramite.TraeTitulo(obj);
                dao.HojaTramite.ListarTramitesDetalle(obj);

                string[] meses = { "", "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
                obj.FechaLarga = DateTime.Today.Day.ToString() + " de " + meses[DateTime.Today.Month] + ", " + DateTime.Today.ToString("yyyy");

                Transformador objTransformador = new Transformador();
                objTransformador.TransformXml(obj, ruta, obj.PathArchivo + ".fo");
                Generador objGenerador = new Generador(obj.PathArchivo + ".fo", obj.PathArchivo);
                salida = objGenerador.XSLToPDF();
            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Reportes.bcp.Judicial.TraeHojaTramiteCliente", obj.Pclid);
                throw ex;
            }
            return salida;
        }

        public static bool TraeHojaTramiteClienteParcial(dto.HojaTramite obj)
        {
            bool salida = false;
            obj.Encabezado.Codemp = obj.Codemp;
            obj.Encabezado.Codsuc = obj.IdSuc;
            string ruta = ""; // dao.Cartera.RutaReportes(obj.Pagina, obj.IdReporte, obj.Idioma);
            try
            {
                dao.CabeceraReporte.TraeEmpresa(obj.Encabezado);
                dao.HojaTramite.TraeTitulo(obj);
                dao.HojaTramite.ListarTramitesDetalleParcial(obj);

                string[] meses = { "", "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
                obj.FechaLarga = DateTime.Today.Day.ToString() + " de " + meses[DateTime.Today.Month] + ", " + DateTime.Today.ToString("yyyy");

                Transformador objTransformador = new Transformador();
                objTransformador.TransformXml(obj, ruta, obj.PathArchivo + ".fo");
                Generador objGenerador = new Generador(obj.PathArchivo + ".fo", obj.PathArchivo);
                salida = objGenerador.XSLToPDF();
            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Reportes.bcp.Judicial.TraeHojaTramiteCliente", obj.Pclid);
                throw ex;
            }
            return salida;
        }

        public static bool TraeInformeJudicialCodigoCarga(dto.InformeJudicial obj)
        {
            bool salida = false;
            obj.Encabezado.Codemp = obj.Codemp;
            obj.Encabezado.Codsuc = obj.Sucid;
            string ruta = dao.Cartera.RutaReportes(obj.Pagina, obj.IdReporte, obj.Idioma);//@"d:\archivos\reportes\xsl\informe_prejudicial_codigo_carga.xsl";//
            try
            {
                dao.CabeceraReporte.TraeEmpresa(obj.Encabezado);
                dao.InformeJudicial.TraeTitulo(obj);
                dao.InformeJudicial.ListarDocumentosCodigoCarga(obj);
                obj.FechaEmisionCorta = DateTime.Today;
                Transformador objTransformador = new Transformador();
                objTransformador.TransformXml(obj, ruta, obj.PathArchivo + ".fo");// @"D:\liquidacion_cocha_mutual.fo");
                Generador objGenerador = new Generador(obj.PathArchivo + ".fo", obj.PathArchivo);
                salida = objGenerador.XSLToPDF();
            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Reportes.bcp.Judicial.TraeLiquidacionDura", obj.Ctcid);
                throw ex;
            }
            return salida;
        }

        public static bool TraeInformeJudicialIncluyeAsegurado(dto.InformeJudicial obj)
        {
            bool salida = false;
            obj.Encabezado.Codemp = obj.Codemp;
            obj.Encabezado.Codsuc = obj.Sucid;
            string ruta = dao.Cartera.RutaReportes(obj.Pagina, obj.IdReporte, obj.Idioma);//;//
            try
            {
                dao.CabeceraReporte.TraeEmpresa(obj.Encabezado);
                dao.InformeJudicial.TraeTitulo(obj);
                dao.InformeJudicial.ListarDocumentosIncluyeAsegurado(obj);
                obj.FechaEmisionCorta = DateTime.Today;
                Transformador objTransformador = new Transformador();
                objTransformador.TransformXml(obj, ruta, obj.PathArchivo + ".fo");// @"D:\liquidacion_cocha_mutual.fo");
                Generador objGenerador = new Generador(obj.PathArchivo + ".fo", obj.PathArchivo);
                salida = objGenerador.XSLToPDF();
            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Reportes.bcp.Judicial.TraeLiquidacionDura", obj.Ctcid);
                throw ex;
            }
            return salida;
        }


        public static bool TraeInformeJudicialQuiebra(dto.InformeJudicial obj)
        {
            bool salida = false;
            obj.Encabezado.Codemp = obj.Codemp;
            obj.Encabezado.Codsuc = obj.Sucid;
            string ruta = dao.Cartera.RutaReportes(obj.Pagina, obj.IdReporte, obj.Idioma);//@"d:\archivos\reportes\xsl\informe_prejudicial_codigo_carga.xsl";//
            try
            {
                dao.CabeceraReporte.TraeEmpresa(obj.Encabezado);
                dao.InformeJudicial.TraeTitulo(obj);
                dao.InformeJudicial.ListarDocumentosQuiebra(obj);
                obj.FechaEmisionCorta = DateTime.Today;
                Transformador objTransformador = new Transformador();
                objTransformador.TransformXml(obj, ruta, obj.PathArchivo + ".fo");// @"D:\liquidacion_cocha_mutual.fo");
                Generador objGenerador = new Generador(obj.PathArchivo + ".fo", obj.PathArchivo);
                salida = objGenerador.XSLToPDF();
            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Reportes.bcp.Judicial.TraeLiquidacionDura", obj.Ctcid);
                throw ex;
            }
            return salida;
        }

        #endregion
    }
}
