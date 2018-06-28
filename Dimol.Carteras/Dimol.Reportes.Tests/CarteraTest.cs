using Dimol.PDF;
using HiQPdf;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Reportes.Tests
{
    [TestClass]
    public class CarteraTest
    {
        [TestMethod]
        public void TestTortaAgrupada()
        {
            try
            {
                dto.TortaAgrupada obj = new dto.TortaAgrupada();
                obj.Codemp = 1;
                obj.Pclid = 318;
                obj.TipoCartera = 1;
                obj.EstadoCpbt = "P";
                obj.Idioma = 1;
                obj.Sucid = 1;
                //obj.RutBusca = "";
                obj.CodGestor = 209;
                obj.FechaReporte = DateTime.Now;
                obj.PathArchivo = obj.PathArchivo = @"D:\Archivos\Reportes\XSL\torta_agrorama";
                bool ruta = bcp.Cartera.TraeTortaAgrupada(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        [TestMethod]
        public void TestRecepcionDocumentos()
        {
            try
            {
                dto.RecepcionDocumentos obj = new dto.RecepcionDocumentos();
                obj.Codemp = 1;
                obj.Codsuc = 1;
                obj.Pclid = 90;
                obj.TipoCartera = 1;
                obj.Ctcid = 1084730;
                obj.Estcpbt = "J";
                obj.FechaDesde = DateTime.Parse("01-01-2000");
                obj.FechaHasta = DateTime.Parse("10-18-2016");
                obj.Idioma = 1;
                obj.FechaReporte = DateTime.Now;
                obj.FechaRecepcionStr = "01-01-2015";
                obj.NombreUsuario = "Felipe Munoz";
                obj.RutUsuario = "11.111.111-1";

                //bool ruta = bcp.Cartera.TraeRecepcionDocumentos(obj);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [TestMethod]
        public void TestResumenGestiones()
        {
            try
            {
                dto.ResumenGestiones obj = new dto.ResumenGestiones();
                obj.Codemp = 1;
                obj.Sucid = 1;
                obj.Pclid = 22;
                obj.Ctcid = 1206598;
                obj.FechaReporte = DateTime.Now;
                obj.Idioma = 1;
                obj.Pagina = 355;
                obj.IdReporte = 4;
                obj.PathArchivo = @"C:\ArchivosSalida\resumen_gestiones.pdf";

                bool ruta = bcp.Cartera.TraeResumenGestiones(obj);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [TestMethod]
        public void TestGeneraPDFSII()
        {
            try
            {               
                bcp.Cartera.GeneraPDFSII(2349, "C:\\ArchivosSalida\\", 2349 + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".pdf");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [TestMethod]
        public void TestInformeRemesa()
        {
            try
            {
                dto.InformeRemesa obj = new dto.InformeRemesa();
                obj.Codemp = 1;
                obj.Sucid = 1;
                obj.Idioma = 1;
                obj.TipoDocumento = 42;
                obj.Numero = 1317;
                obj.FechaReporte = DateTime.Now;

                string ruta = bcp.Cartera.TraeInformeRemesa(obj);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [TestMethod]
        public void TestLiquidacionCochaMutual()
        {
            try
            {
                dto.Liquidacion obj = new dto.Liquidacion();
                obj.Codemp = 1;                
                obj.Pclid = 22;
                obj.Ctcid = 1203973;// 1202065;//7598;
                obj.TipoCartera = 1;
                obj.EstadoCpbt = "V";
                obj.Idioma = 1;
                obj.Sucid = 1;
                obj.FechaReporte = DateTime.Now;
                obj.PathArchivo = @"d:\liquidacion_cocha_mutual.pdf";

                bool ruta = bcp.Cartera.TraeLiquidacionCochaMutual(obj);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [TestMethod]
        public void TestLiquidacionDura()
        {
            try
            {
                dto.LiquidacionDura obj = new dto.LiquidacionDura();
                obj.Codemp = 1;
                obj.Pclid = 86;
                obj.Ctcid = 1220329;// 1202065;//7598;
                obj.TipoCartera = 2;
                obj.EstadoCpbt = "V";
                obj.Idioma = 1;
                obj.Sucid = 1;
                obj.Pagina = 355;
                obj.IdReporte = 2;
                obj.FechaReporte = DateTime.Now;
                obj.PathArchivo = @"C:\ArchivosSalida\liquidacion_dura.pdf";

                bool ruta = bcp.Cartera.TraeLiquidacionDura(obj);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        

        [TestMethod]
        public void TestGenerico()
        {
            try
            {
                dto.ResumenGestiones obj = new dto.ResumenGestiones();
                obj.Codemp = 1;
                obj.Sucid = 1;
                obj.Pclid = 22;
                obj.Ctcid = 9787;
                obj.FechaReporte = DateTime.Now;

                bool ruta = bcp.Cartera.TraeResumenGestiones(obj);


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [TestMethod]
        public void TestLiquidacionCochaMutualXLS()
        {
            try
            {
                dto.Liquidacion obj = new dto.Liquidacion();
                obj.Codemp = 1;
                obj.Pclid = 22;
                obj.Ctcid = 1203973;// 1202065;//7598;
                obj.TipoCartera = 1;
                obj.EstadoCpbt = "V";
                obj.Idioma = 1;
                obj.Sucid = 1;
                obj.FechaReporte = DateTime.Now;
                obj.PathArchivo = @"d:\liquidacion_cocha_mutual_xls.xls";

               // bool ruta = bcp.Cartera.TraeLiquidacionCochaMutualXLS(obj);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [TestMethod]
        public void TestReporteCancelacion()
        {
            try
            {
                dto.ReporteCancelacion obj = new dto.ReporteCancelacion();
                obj.Codemp = 1;
                obj.Pclid = 22;
                obj.Ctcid = 10070;// 1202065;//7598;
                obj.TipoCartera = 1;
                obj.EstadoCpbt = "V";
                obj.Idioma = 1;
                obj.Sucid = 1;
                obj.FechaReporte = DateTime.Now;
                obj.PathArchivo = @"d:\Reporte_cancelacion.pdf";
                obj.Pagina = 355;
                obj.IdReporte = 5;

                bool ruta = bcp.Cartera.TraeReporteCancelacion(obj);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [TestMethod]
        public void TestLiquidacionMutualLey()
        {
            try
            {
                dto.LiquidacionDura obj = new dto.LiquidacionDura();
                obj.Codemp = 1;
                obj.Pclid = 559;
                obj.Ctcid = 141425;// 1202065;//7598;
                obj.TipoCartera = 1;
                obj.EstadoCpbt = "V";
                obj.Idioma = 1;
                obj.Sucid = 1;
                obj.FechaReporte = DateTime.Now;
                obj.PathArchivo = @"d:\archivos\reportes\xsl\liquidacion_mutual_ley.pdf";
                obj.Pagina = 355;
                obj.IdReporte = 5;

                bool ruta = bcp.Cartera.TraeLiquidacionMutualLey(obj);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [TestMethod]
        public void TestInformePrejudicial()
        {
            try
            {
                dto.InformePrejudicial obj = new dto.InformePrejudicial();
                obj.Codemp = 1;
                obj.Pclid = 605;//86; //259
                obj.Ctcid = 1202330;// 1202065;//7598;
                obj.TipoCartera = 2;
                obj.EstadoCpbt = "V";
                obj.Idioma = 1;
                obj.Sucid = 1;
                obj.FechaReporte = DateTime.Now;
                obj.PathArchivo = @"d:\archivos\reportes\xsl\informe_prejudicial_achs_"+DateTime.Now.ToString("yyyyMMddhhmmss")+".pdf";
                obj.Pagina = 357;
                obj.IdReporte = 6;

                bool ruta = bcp.Cartera.TraeInformePrejudicial(obj);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [TestMethod]
        public void TestInformePrejudicialOchenta()
        {
            try
            {
                dto.InformePrejudicial obj = new dto.InformePrejudicial();
                obj.Codemp = 1;
                obj.Pclid = 86; //259
                obj.Ctcid = 1202330;// 1202065;//7598;
                obj.TipoCartera = 2;
                obj.EstadoCpbt = "V";
                obj.Idioma = 1;
                obj.Sucid = 1;
                obj.FechaReporte = DateTime.Now;
                obj.PathArchivo = @"d:\archivos\reportes\xsl\informe_prejudicial_ochenta_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".pdf";
                obj.Pagina = 357;
                obj.IdReporte = 22;

                bool ruta = bcp.Cartera.TraeInformePrejudicialOchenta(obj);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [TestMethod]
        public void TestInformePrejudicialCodigoCarga()
        {
            try
            {
                dto.InformePrejudicial obj = new dto.InformePrejudicial();
                obj.Codemp = 1;
                obj.Pclid = 86; //259
                obj.Ctcid = 1202330;// 1202065;//7598;
                obj.TipoCartera = 2;
                obj.EstadoCpbt = "V";
                obj.Idioma = 1;
                obj.Sucid = 1;
                obj.FechaReporte = DateTime.Now;
                obj.PathArchivo = @"d:\archivos\reportes\xsl\informe_judicial_codigo_carga" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".pdf";
                obj.Pagina = 355;
                obj.IdReporte = 2;
                obj.Codid = 5;

                bool ruta = bcp.Cartera.TraeInformePrejudicialCodigoCarga(obj);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [TestMethod]
        public void TestInformeJudicial()
        {
            try
            {
                dto.InformeJudicial obj = new dto.InformeJudicial();
                obj.Codemp = 1;
                obj.Pclid = 90; //259
                obj.Ctcid = 1202330;// 1202065;//7598;
                obj.TipoCartera = 2;
                obj.EstadoCpbt = "J";
                obj.Idioma = 1;
                obj.Sucid = 1;
                obj.FechaReporte = DateTime.Now;
                obj.PathArchivo = @"d:\archivos\reportes\xsl\informe_judicial_continental_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".pdf";
                obj.Pagina = 358;
                obj.IdReporte = 1;

                bool ruta = bcp.Judicial.TraeInformeJudicial(obj);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [TestMethod]
        public void TestInformeJudicialOchenta()
        {
            try
            {
                dto.InformeJudicial obj = new dto.InformeJudicial();
                obj.Codemp = 1;
                obj.Pclid = 90; //259
                obj.Ctcid = 1202330;// 1202065;//7598;
                obj.TipoCartera = 2;
                obj.EstadoCpbt = "V";
                obj.Idioma = 1;
                obj.Sucid = 1;
                obj.FechaReporte = DateTime.Now;
                obj.PathArchivo = @"d:\archivos\reportes\xsl\informe_judicial_ochenta_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".pdf";
                obj.Pagina = 358;
                obj.IdReporte = 16;

                bool ruta = bcp.Judicial.TraeInformeJudicialOchenta(obj);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [TestMethod]
        public void TestInformeJudicialCodigoCarga()
        {
            try
            {
                dto.InformePrejudicial obj = new dto.InformePrejudicial();
                obj.Codemp = 1;
                obj.Pclid = 86; //259
                obj.Ctcid = 1202330;// 1202065;//7598;
                obj.TipoCartera = 2;
                obj.EstadoCpbt = "V";
                obj.Idioma = 1;
                obj.Sucid = 1;
                obj.FechaReporte = DateTime.Now;
                obj.PathArchivo = @"d:\archivos\reportes\xsl\informe_judicial_codigo_carga" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".pdf";
                obj.Pagina = 355;
                obj.IdReporte = 2;
                obj.Codid = 5;

                bool ruta = bcp.Cartera.TraeInformePrejudicialCodigoCarga(obj);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [TestMethod]
        public void TestCastigoJudicial()
        {
            try
            {
                dto.CastigoJudicialCliente obj = new dto.CastigoJudicialCliente();
                obj.Codemp = 1;
                obj.Codsuc = 1;
                obj.Tpcid = 32;
                obj.CbcDesde = 971;
                obj.CbcHasta = 971;
                obj.Idioma = 1;
                obj.RutAbogado = "138315541";
                obj.NombreAbogado = "MAY GUTIERREZ OTTO";
                obj.Empresa = "DIMOL LTDA.";
                obj.FechaReporte = DateTime.Now;
                bool ruta = bcp.Cartera.TraeCastigoJudicial(obj);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [TestMethod]
        public void TestCastigoJudicialNormal()
        {
            try
            {
                dto.CastigoJudicialCliente obj = new dto.CastigoJudicialCliente();
                obj.Codemp = 1;
                obj.Codsuc = 1;
                obj.Tpcid = 32;
                obj.CbcDesde = 1833;
                obj.CbcHasta = 1833;
                obj.Ctcid = 1218599;
                obj.Idioma = 1;
                obj.RutAbogado = "138315541";
                obj.NombreAbogado = "MAY GUTIERREZ OTTO";
                obj.Empresa = "DIMOL LTDA.";
                obj.FechaReporte = DateTime.Now;
                obj.PathArchivo = obj.PathArchivo = @"C:\Reportes\castigo_Judicial.pdf";
                obj.Pagina = 359;
                obj.IdReporte = 4;
                bool ruta = bcp.Cartera.TraeCastigoJudicialNormal(obj);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [TestMethod]
        public void TestCastigoPrejudicialCliente()
        {
            try
            {
                dto.CastigoPrejudicialCliente obj = new dto.CastigoPrejudicialCliente();
                obj.Codemp = 1;
                obj.Codsuc = 1;
                obj.Tpcid = 31;
                obj.Cbcnumero = 1878;
                obj.Idioma = 1;
                obj.Empresa = "DIMOL SpA";
                obj.FechaReporte = DateTime.Now;
                obj.PathArchivo = obj.PathArchivo = @"C:\Reportes\castigo_judicial.pdf";
                obj.Pagina = 359;
                obj.IdReporte = 1;
                bool ruta = bcp.Cartera.TraeCastigoPrejudicialClienteNew(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [TestMethod]
        public void TestCastigoPrejudicialClienteNormal()
        {
            try
            {
                dto.CastigoPrejudicialCliente obj = new dto.CastigoPrejudicialCliente();
                obj.Codemp = 1;
                obj.Codsuc = 1;
                obj.Tpcid = 31;
                obj.Cbcnumero = 1878;
                obj.Ctcid = 1222228;
                obj.Idioma = 1;
                obj.Empresa = "DIMOL SpA";
                obj.FechaReporte = DateTime.Now;
                obj.PathArchivo = obj.PathArchivo = @"C:\Reportes\castigo_prejudicial.pdf";
                obj.Pagina = 359;
                obj.IdReporte = 1;
                bool ruta = bcp.Cartera.TraeCastigoPrejudicialClienteNormal(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [TestMethod]
        public void TestDevolucionDocumentosCliente()
        {
            try
            {
                dto.DevolucionDocumentosCliente obj = new dto.DevolucionDocumentosCliente();
                obj.Codemp = 1;
                obj.Codsuc = 1;
                obj.Tpcid = 34;
                obj.Cbcnumero = 105;
                obj.Idioma = 1;
                obj.FechaReporte = DateTime.Now;
                bool ruta = bcp.Cartera.TraeDevolucionDocumentosCliente(obj);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [TestMethod]
        public void TestDevolucionDocumentosClienteNormal()
        {
            try
            {
                dto.DevolucionDocumentosCliente obj = new dto.DevolucionDocumentosCliente();
                obj.Codemp = 1;
                obj.Codsuc = 1;
                obj.Tpcid = 34;
                obj.Cbcnumero = 105;
                obj.Idioma = 1;
                obj.FechaReporte = DateTime.Now;
                bool ruta = bcp.Cartera.TraeDevolucionDocumentosNormal(obj);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [TestMethod]
        public void GeneraCastigoIndividual()
        {
            // create an empty PDF document
            PdfDocument document = new PdfDocument();
            // set a demo serial number
            document.SerialNumber = "5q6Pt7aC-gKqPhJSH-lJ/X1sjW-xtfG1Mbf-397G1dfI-19TI39/f-3w==";
            document.Pages.Remove(0);
            PdfDocument docPorRut = new PdfDocument();
            docPorRut.SerialNumber = "5q6Pt7aC-gKqPhJSH-lJ/X1sjW-xtfG1Mbf-397G1dfI-19TI39/f-3w==";
            bool resumen = false;
            string ruta = "";
            // add a page to document

            string ubicacionRut = "";
            try
            {
                ubicacionRut = @"\\10.0.1.238\Usuarios\castigos" + "\\" + "76034737" + "K";
                if (!System.IO.Directory.Exists(ubicacionRut))
                {
                    System.IO.Directory.CreateDirectory(ubicacionRut);
                }
                string[] fileEntries = Directory.GetFiles(ubicacionRut);
                // genero carta castigo
                dto.CastigoPrejudicialCliente objCliente = new dto.CastigoPrejudicialCliente();
                objCliente.Codemp = 1;
                objCliente.Codsuc = 1;
                objCliente.Tpcid = 31;
                objCliente.Cbcnumero = 4177;
                objCliente.Ctcid = 1202128;
                objCliente.Idioma = 1;
                objCliente.Empresa = "DIMOL LTDA.";
                objCliente.FechaReporte = DateTime.Now;
                objCliente.Pagina = 359;
                objCliente.IdReporte = 1;
                objCliente.PathArchivo = ubicacionRut + "\\" + "76034737" + "K" + "_castigo.pdf";
                if (System.IO.File.Exists(objCliente.PathArchivo))
                {
                    File.Delete(objCliente.PathArchivo);

                }
                ruta = bcp.Cartera.TraeCastigoPrejudicialCliente(objCliente);
                System.IO.File.Delete(objCliente.PathArchivo + ".fo");
                PdfDocument document1 = PdfDocument.FromFile(objCliente.PathArchivo);
            }
            catch
            {

            }
        }
    }
}
