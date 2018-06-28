using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Reportes.Tests
{
    public class JudicialTest
    {
        [TestMethod]
        public void TestTraspasoJudicial()
        {
            try
            {
                dto.RecepcionDocumentos obj = new dto.RecepcionDocumentos();
                obj.Codemp = 1;
                obj.Codsuc = 1;
                obj.Pclid = 22;
                obj.TipoCartera = 1;
                obj.FechaDesde = DateTime.Parse("12-01-2012");
                obj.FechaHasta = DateTime.Parse("01-01-2013");
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
        public void TestRecepcionDocumentos()
        {
            try
            {
                dto.RecepcionDocumentos obj = new dto.RecepcionDocumentos();
                obj.Codemp = 1;
                obj.Codsuc = 1;
                obj.Pclid = 22;
                obj.TipoCartera = 1;
                obj.FechaDesde = DateTime.Parse("12-01-2012");
                obj.FechaHasta = DateTime.Parse("01-01-2013");
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
    }
}
