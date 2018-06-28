using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using PJSpider.bcp;
using PJSpider.dto;

namespace UnitTestProject
{
    [TestClass]
    public class MultithreadTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            Causa bcpCausa = new Causa();
            ConsultaCausa obj = new ConsultaCausa();
            //obj.RolAnio = 2015;
            //obj.CodigoTribunal = 259;
            //obj.NombreTribunal = "1º Juzgado Civil de Santiago";
            //obj.Url = "http://civil.poderjudicial.cl/CIVILPORWEB/";
            //obj.TipoCausa = "C";
            //obj.RolCausa = 14659;

            obj.RolAnio = 2014;
            obj.CodigoTribunal = 259;
            obj.NombreTribunal = "2º Juzgado de Letras de Talca ";
            obj.Url = "http://civil.poderjudicial.cl/CIVILPORWEB/";
            obj.TipoCausa = "C";
            obj.RolCausa = 1156;
            obj.Codemp = 1;
            obj.Rolid = 6041;

        }

        [TestMethod]
        public void DescargarCopec()
        {
            Causa bcpCausa = new Causa();
            ConsultaCausa obj = new ConsultaCausa();
            //obj.RolAnio = 2015;
            //obj.CodigoTribunal = 259;
            //obj.NombreTribunal = "1º Juzgado Civil de Santiago";
            //obj.Url = "http://civil.poderjudicial.cl/CIVILPORWEB/";
            //obj.TipoCausa = "C";
            //obj.RolCausa = 14659;

            obj.RolAnio = 2014;
            obj.CodigoTribunal = 259;
            obj.NombreTribunal = "2º Juzgado de Letras de Talca ";
            obj.Url = "http://civil.poderjudicial.cl/CIVILPORWEB/";
            obj.TipoCausa = "C";
            obj.RolCausa = 1156;
            obj.Codemp = 1;
            obj.Rolid = 6041;




            Causa.ActualizarPoderJudicialCliente(1, 1, "-238,-75", 86);

        }

        [TestMethod]
        public void DescargarCoopeuch()
        {
            Causa bcpCausa = new Causa();
            ConsultaCausa obj = new ConsultaCausa();
            //obj.RolAnio = 2015;
            //obj.CodigoTribunal = 259;
            //obj.NombreTribunal = "1º Juzgado Civil de Santiago";
            //obj.Url = "http://civil.poderjudicial.cl/CIVILPORWEB/";
            //obj.TipoCausa = "C";
            //obj.RolCausa = 14659;

            obj.RolAnio = 2014;
            obj.CodigoTribunal = 259;
            obj.NombreTribunal = "2º Juzgado de Letras de Talca ";
            obj.Url = "http://civil.poderjudicial.cl/CIVILPORWEB/";
            obj.TipoCausa = "C";
            obj.RolCausa = 1156;
            obj.Codemp = 1;
            obj.Rolid = 6041;




            Causa.ActualizarPoderJudicialCliente(1, 1, "238,75", 279);

        }

        [TestMethod]
        public void DescargarBat()
        {
            Causa bcpCausa = new Causa();
            ConsultaCausa obj = new ConsultaCausa();
            //obj.RolAnio = 2015;
            //obj.CodigoTribunal = 259;
            //obj.NombreTribunal = "1º Juzgado Civil de Santiago";
            //obj.Url = "http://civil.poderjudicial.cl/CIVILPORWEB/";
            //obj.TipoCausa = "C";
            //obj.RolCausa = 14659;

            obj.RolAnio = 2014;
            obj.CodigoTribunal = 259;
            obj.NombreTribunal = "2º Juzgado de Letras de Talca ";
            obj.Url = "http://civil.poderjudicial.cl/CIVILPORWEB/";
            obj.TipoCausa = "C";
            obj.RolCausa = 1156;
            obj.Codemp = 1;
            obj.Rolid = 6041;




            Causa.ActualizarPoderJudicialCliente(1, 1, "238,75", 79);

        }

        [TestMethod]
        public void TestMethod2()
        {
            // Create a new instance of the Firefox driver.
            IWebDriver driver = new FirefoxDriver();
            driver.Navigate().GoToUrl("http://civil.poderjudicial.cl/CIVILPORWEB/");

            driver.SwitchTo().Frame(driver.FindElement(By.Name("body")));

            // Find the text input element by its name
            IWebElement tipoCausa = driver.FindElement(By.Name("TIP_Causa"));
            IWebElement rol = driver.FindElement(By.Name("ROL_Causa"));
            IWebElement anio = driver.FindElement(By.Name("ERA_Causa"));
            IWebElement tribunal = driver.FindElement(By.Name("COD_Tribunal"));

            // Enter something to search for
            rol.SendKeys("14659");
            anio.SendKeys("2015");
            tipoCausa.SendKeys("C");
            tribunal.SendKeys("259");

            IWebElement btnBuscar = driver.FindElement(By.XPath("//a[@onclick='AtPublicoPpalForm.irAccionAtPublico.click();']"));

            // Now submit the form. WebDriver will find the form for us from the element
            btnBuscar.Click();

            // Google's search is rendered dynamically with JavaScript.
            // Wait for the page to load, timeout after 10 seconds
            //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            //wait.Until((d) => { 
            //    d.SwitchTo().Frame(driver.FindElement(By.Name("body")));
            //    return d.FindElement(By.XPath("//a[contains(.,'http://civil.poderjudicial.cl/CIVILPORWEB/ConsultaDetalleAtPublicoAccion.do?')]")); 
            //});
            ////http://civil.poderjudicial.cl/CIVILPORWEB/ConsultaDetalleAtPublicoAccion.do?TIP_Consulta=1&TIP_Cuaderno=49&CRR_IdCuaderno=17977992&ROL_Causa=14659&TIP_Causa=C&ERA_Causa=2015&CRR_IdCausa=14238003&COD_Tribunal=259&TIP_Informe=1&

            IWebElement urlCausa = driver.FindElement(By.XPath("//a[ contains(@href,'ConsultaDetalleAtPublicoAccion.do')]"));
            string url = urlCausa.GetAttribute("href");
            //// Should see: "Cheese - Google Search"
            //System.Console.WriteLine("Page title is: " + driver.Title);

            //Close the browser
            driver.Quit();
        }
    }
}
