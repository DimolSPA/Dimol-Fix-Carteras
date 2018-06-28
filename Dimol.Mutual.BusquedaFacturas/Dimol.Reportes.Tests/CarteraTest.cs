using Dimol.PDF;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Reportes.Tests
{
    [TestClass]
    public class CarteraTest
    {
        [TestMethod]
        public void TestPrescripciones()
        {
            try
            {
                dto.Prescripciones obj = new dto.Prescripciones();
                obj.Codemp = 1;
                obj.Pclid = 318;
                obj.TipoCartera = 1;
                obj.Idioma = 1;
                obj.Codsuc = 1;
                obj.DiasPrescrip = 30;              
                obj.FechaReporte = DateTime.Now;
                obj.PathArchivo = obj.PathArchivo = @"C:\ArchivosSalida\prescripciones";
                string ruta = bcp.Cartera.TraePrescripciones(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [TestMethod]
        public void TestArbolJudicial()
        {
            try
            {
                dto.ArbolJudicial obj = new dto.ArbolJudicial();
                obj.Codemp = 1;
                obj.Pclid = 318;
                obj.TipoCartera = 1;                
                obj.Idioma = 1;
                obj.Codsuc = 1;
                obj.Abogados =  new[] { "MAY CONZUELO", "NURY VALERIA", "NATALIA", "FRANCISCO JAVIER", "GUSTAVO IGNACIO", "SANDRA CAROLINA", "ELBA ESTER", "SIN TRIBUNAL" };
                //obj.Abogados = new[] {""};
                obj.FechaDesde = DateTime.Parse("31-07-2016");
                obj.FechaHasta = DateTime.Parse("01-09-2016");
                obj.FechaReporte = DateTime.Now;
                obj.PathArchivo = obj.PathArchivo = @"C:\ArchivosSalida\arbol_judicial";
                string ruta = bcp.Cartera.TraeArbolJudicial(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [TestMethod]
        public void TestTortaRanking()
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
                obj.RutBusca = "";
                obj.CodGestor = 0; //209;
                obj.EstadosCartera = "40, 234";
                obj.FechaReporte = DateTime.Now;
                obj.PathArchivo = obj.PathArchivo = @"C:\ArchivosSalida\torta_ranking_cartera";
                string ruta = bcp.Cartera.TraeTortaRanking(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        [TestMethod]
        public void TestCastigoJudicialAseguradoDOC()
        {
            try
            {
                dto.CastigoJudicialAsegurado obj = new dto.CastigoJudicialAsegurado();
                obj.Codemp = 1;
                obj.Codsuc = 1;
                obj.Tpcid = 32;
                obj.RutDeudor = "76073070K"; //"777389602"; //"761551922"
                obj.RutAsegurado = "799842408"; //"840606007"; //"760089893"
                obj.Idioma = 1;
                obj.RutaOrigen = @"C:\ArchivosSalida\Castigo_Judicial_Asegurado.doc";
                obj.RutaDestino = System.IO.Path.GetTempPath() + "Castigo_Judicial_Asegurado_" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".doc";
                obj.FechaReporte = DateTime.Now;
                string ruta = bcp.Cartera.TraeCastigoJudicialAseguradoDoc(obj);


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [TestMethod]
        public void TestHojaTramite()
        {
            try
            {
                dto.HojaTramite obj = new dto.HojaTramite();
                obj.Codemp = 1;
                obj.Pclid = 86;             
                obj.Idioma = 1;
                obj.IdSuc = 1;
                obj.Ctcid = 1218616;
                obj.EstadoCpbt = "J";              
                obj.FechaReporte = DateTime.Now;
                obj.PathArchivo = obj.PathArchivo = @"C:\ArchivosSalida\hoja_tramite";
                bool ruta = bcp.Cartera.TraeHojaTramiteCliente(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        /* [TestMethod]
         public void TestCastigoPrejudicialAsegurado()
         {
             try
             {
                 dto.CastigoPrejudicialCliente obj = new dto.CastigoPrejudicialCliente();
                 obj.Codemp = 1;
                 obj.Codsuc = 1;
                 obj.Tpcid = 31;
                 obj.Cbcnumero = 426;
                 obj.Idioma = 1;
                 obj.Empresa = "DIMOL LTDA.";
                 obj.FechaReporte = DateTime.Now;
                 string ruta = bcp.Cartera.TraeCastigoPrejudicialAsegurado(obj);

             }
             catch (Exception ex)
             {
                 throw ex;
             }

         } */

        [TestMethod]
        public void TestCastigoPrejudicialCliente()
        {
            try
            {
                dto.CastigoPrejudicialCliente obj = new dto.CastigoPrejudicialCliente();
                obj.Codemp = 1;
                obj.Codsuc = 1;
                obj.Tpcid = 31;
                obj.Cbcnumero = 973;
                obj.Idioma = 1215147;
                obj.Empresa = "DIMOL LTDA.";
                obj.FechaReporte = DateTime.Now;
                string ruta = bcp.Cartera.TraeCastigoPrejudicialCliente(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [TestMethod]
        public void TestCastigoPrejudicialManual()
        {
            try
            {
                dto.CastigoPrejudicialManualCliente obj = new dto.CastigoPrejudicialManualCliente();
                obj.Codemp = 1;
                obj.Codsuc = 1;
                obj.Tpcid = 31;
                obj.CcbPclid = 51;
                obj.Pcsid = 1;
                obj.CcbEstcpbt = "V";
                obj.PccCodigo = 02;

                obj.Cbcnumero = 1804;
                obj.Idioma = 1;
                obj.Empresa = "DIMOL SpA.";
                obj.FechaReporte = DateTime.Now;
                string ruta = bcp.Cartera.TraeCastigoPrejudicialManualCliente(obj);

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
                string ruta = bcp.Cartera.TraeCastigoJudicial(obj);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [TestMethod]
        public void TestCastigoJudicialManual()
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
                string ruta = bcp.Cartera.TraeCastigoJudicialManual(obj);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [TestMethod]
        public void TestCastigoJudicial2()
        {
            try
            {
                dto.CastigoJudicial2 obj = new dto.CastigoJudicial2();
                obj.Codemp = 1;
                obj.Codsuc = 1;
                obj.Tpcid = 32;
                obj.CbcDesde = 994; //971
                obj.CbcHasta = 995; //971
                obj.Idioma = 1;
                obj.RutAbogado = "138315541";
                obj.NombreAbogado = "MAY GUTIERREZ OTTO";
                obj.Empresa = "DIMOL LTDA.";
                obj.FechaReporte = DateTime.Now;
                string ruta = bcp.Cartera.TraeCastigoJudicial2(obj);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /*  [TestMethod]
          public void TestCastigoJudicialAsegurado()
          {
              try
              {
                  dto.CastigoJudicialCliente obj = new dto.CastigoJudicialCliente();
                  obj.Codemp = 1;
                  obj.Codsuc = 1;
                  obj.Tpcid = 32;
                  obj.CbcDesde = 124;
                  obj.CbcHasta = 124;
                  obj.Idioma = 1;
                  obj.RutAbogado = "138315541";
                  obj.NombreAbogado = "MAY GUTIERREZ OTTO";
                  obj.Empresa = "DIMOL LTDA.";
                  obj.FechaReporte = DateTime.Now;
                  string ruta = bcp.Cartera.TraeCastigoJudicialAsegurado(obj);

              }
              catch (Exception ex)
              {
                  throw ex;
              }

          } */

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
                string ruta = bcp.Cartera.TraeDevolucionDocumentosCliente(obj);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [TestMethod]
        public void TestDevolucionDocumentosLista()
        {
            try
            {
                dto.DevolucionDocumentos obj = new dto.DevolucionDocumentos();
                obj.Codemp = 1;
                obj.Codsuc = 1;
                obj.Tpcid = 34;
                obj.Cbcnumero = 105;
                obj.Idioma = 1;
                obj.FechaReporte = DateTime.Now;
                string ruta = bcp.Cartera.TraeDevolucionDocumentosLista(obj);

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
                obj.TipoCartera = 2;
                obj.Ctcid = 1084730;
                obj.Estcpbt = 'J';
                obj.FechaDesde = DateTime.Parse("01-10-2015");
                obj.FechaHasta = DateTime.Parse("31-10-2015");
                obj.Idioma = 1;
                obj.FechaReporte = DateTime.Now;
                obj.FechaRecepcionStr = "01-01-2015";
                obj.NombreUsuario = "Felipe Munoz";
                obj.RutUsuario = "11.111.111-1";
               
                string ruta=bcp.Cartera.TraeRecepcionDocumentos(obj);
               
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [TestMethod]
        public void TestTortaAgrupada()
        {
            try
            {
                dto.TortaAgrupada obj = new dto.TortaAgrupada();
                obj.Codemp = 1;
                obj.Pclid = 556; 
                obj.TipoCartera = 2;
                obj.EstadoCpbt = "P";
                obj.Idioma = 1;
                obj.Sucid = 1;
                obj.CodigoCarga = 0;
                obj.RutBusca = "";
                obj.CodGestor = 0;
                obj.FechaReporte = DateTime.Now;
                obj.PathArchivo = @"C:\ArchivosSalida\torta_agrupada";
                bool ruta = bcp.Cartera.TraeTortaAgrupada(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        [TestMethod]
        public void TestMutualManual()
        {
            try
            {
                dto.MutualManual obj = new dto.MutualManual();
                obj.Codemp = 1;
                obj.Sucid = 1;
                obj.Pclid = 22;
                obj.Ctcid = 9787;
                obj.FechaReporte = DateTime.Now;

                bool ruta = bcp.Cartera.TraeMutualManual(obj);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [TestMethod]
        public void TestMutualManual2()
        {
            try
            {
                dto.MutualManual obj = new dto.MutualManual();
                obj.Codemp = 1;
                obj.Sucid = 1;
                obj.Pclid = 22;
                obj.Ctcid = 9787;
                obj.FechaReporte = DateTime.Now;
                obj.Ruta = @"C:\ArchivosSalida\Archivos_Mutual_Seg\" + DateTime.Now.ToString("dd-MM-yyyy") + @"\";
                obj.PathArchivo = @"C:\ArchivosSalida\mutual_manual_a.xsl";

                bool ruta = bcp.Cartera.TraeMutualManual2(obj);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [TestMethod]
        public void TestBuscarFacturas()
        {
            try
            {
                dto.BuscarFactura obj = new dto.BuscarFactura();

                obj.CarpetaRaiz = @"C:\ArchivosSalida\Archivos_Busq\" + DateTime.Now.ToString("dd-MM-yyyy") + @"\";

                bool ruta = bcp.Cartera.TraeBuscarFacturas(obj);
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
                obj.Ctcid = 9787;
                obj.FechaReporte = DateTime.Now;

                //string ruta = bcp.Cartera.TraeResumenGestiones(obj);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [TestMethod]
        public void TestTrekkingCartera()
        {
            try
            {
                dto.TrekkingCartera obj = new dto.TrekkingCartera();
                obj.Codemp = 1;
                obj.Sucid = 1;
                //obj.Pclid = 22;
                //obj.Ctcid = 9787;
                obj.FechaReporte = DateTime.Now;
                obj.FechaEmisionCorta = DateTime.Now;
                obj.Pagina = 357;
                obj.IdReporte = 21;
                obj.Idioma = 1;
                obj.TipoCartera = 1;
                obj.CodGestor = 257;
                obj.PathArchivo = @"C:\ArchivosSalida\Trekking_Cartera.pdf";

                bool ruta = bcp.Cartera.TraeTrekkingCartera(obj);

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
                obj.Pclid = 86; //259
                obj.Ctcid = 1202330;// 1202065;//7598;
                obj.TipoCartera = 2;
                obj.EstadoCpbt = "V";
                obj.Idioma = 1;
                obj.Sucid = 1;
                obj.FechaReporte = DateTime.Now;
                obj.PathArchivo = @"C:\ArchivosSalida\informe_judicial" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".pdf";
                obj.Pagina = 355;
                obj.IdReporte = 2;

                bool ruta = bcp.Cartera.TraeInformePrejudicial(obj);

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
                obj.EstadoCpbt = "V";
                obj.Idioma = 1;
                obj.Sucid = 1;
                obj.FechaReporte = DateTime.Now;
                obj.PathArchivo = @"d:\archivos\reportes\xsl\informe_judicial_codigo_carga_asegurado_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".pdf";
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
    }
}
