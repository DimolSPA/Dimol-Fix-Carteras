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
