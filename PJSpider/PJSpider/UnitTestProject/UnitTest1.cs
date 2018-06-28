using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using PJSpider.bcp;
using PJSpider.dto;
using System.Collections.Generic;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestParticion()
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



            Causa.ActualizarPoderJudicialParticion(1, 1, "-1",1,100);

        }

        [TestMethod]
        public void TestRol()
        {


            
            //Causa.ActualizarPoderJudicialRol(1, 1,"", "C" , 16187, 2011, "1º JUZGADO CIVIL DE RANCAGUA");

        }

        [TestMethod]
        public void TestExploraRol()
        {
            List<Dimol.dto.Combobox> lstTribunales = Causa.ListarTribunalesScanner();
            ScannerCausas obj = new ScannerCausas();
            int[] anios = { 2013, 2014, 2015 };

            foreach (int anio in anios)
            {
                foreach (Dimol.dto.Combobox tribunal in lstTribunales)
                {
                    int cambiar = 0;
                    int i = 1;
                    while (cambiar < 3)
                    {
                        obj.RolAnio = 2015;// anio;
                        obj.CodigoTribunal = Int32.Parse(tribunal.Value);
                        obj.NombreTribunal = "2º Juzgado de Letras de Curico";// tribunal.Text;
                        obj.Url = "http://civil.poderjudicial.cl/CIVILPORWEB/";
                        obj.TipoCausa = "C";
                        obj.RolCausa = i;
                        obj.IdCausa = 0;
                        obj.IdCuaderno = 0;
                        try
                        {
                            Causa.ExplorarCausa(obj);
                        }
                        catch (Exception ex)
                        {
                            cambiar++;
                        }
                        
                        i++;
                    }

                }
            }
        }

        [TestMethod]
        public void TestExploraRolOptimizado()
        {
            List<Dimol.dto.Combobox> lstTribunales = Causa.ListarTribunalesScanner();
            ScannerCausas obj = new ScannerCausas();
            int[] anios = { 2013, 2014, 2015, 2016 };
            List<int> lst = new List<int>();
            int max = 0;
            int cambiar = 0;
            int i = 0, j = 0;
            foreach (int anio in anios)
            {
                foreach (Dimol.dto.Combobox tribunal in lstTribunales)
                {
                    lst = Causa.ListarRolesScanner(anio,Int32.Parse(tribunal.Value));
                    if (lst.Count > 0)
                    {
                        for (i = 0; i < lst.Count; i++)
                        {
                            if (i > 1 && lst[i] - lst[i - 1] > 1)
                            {
                                for (j = lst[i - 1]+1; j < lst[i]; j++)
                                {
                                    obj.RolAnio = 2015;// anio;
                                    obj.CodigoTribunal = Int32.Parse(tribunal.Value);
                                    obj.NombreTribunal = "2º Juzgado de Letras de Curico";// tribunal.Text;
                                    obj.Url = "http://civil.poderjudicial.cl/CIVILPORWEB/";
                                    obj.TipoCausa = "C";
                                    obj.RolCausa = j;
                                    obj.IdCausa = 0;
                                    obj.IdCuaderno = 0;
                                    try
                                    {
                                        //Causa.ExplorarCausa(obj);
                                    }
                                    catch (Exception ex)
                                    {
                                        cambiar++;
                                    }
                                }
                            }
                        }
                        max = (int)lst[lst.Count - 1]+1;//(from l in ()lst select l).Max().FirstorDefault();
                    }
                    else
                    {
                        max = 1;
                    }
                    while (cambiar < 5)
                    {
                        obj.RolAnio = 2015;// anio;
                        obj.CodigoTribunal = Int32.Parse(tribunal.Value);
                        obj.NombreTribunal = "2º Juzgado de Letras de Curico";// tribunal.Text;
                        obj.Url = "http://civil.poderjudicial.cl/CIVILPORWEB/";
                        obj.TipoCausa = "C";
                        obj.RolCausa = max;
                        obj.IdCausa = 0;
                        obj.IdCuaderno = 0;
                        try
                        {
                            //Causa.ExplorarCausa(obj);
                            cambiar = 6;
                        }
                        catch (Exception ex)
                        {
                            cambiar++;
                        }

                        max++;
                    }

                }
            }
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

            obj.RolAnio = 2015;
            obj.CodigoTribunal = 259;
            obj.NombreTribunal = "2º Juzgado de Letras de Talca ";
            obj.Url = "http://civil.poderjudicial.cl/CIVILPORWEB/";
            obj.TipoCausa = "C";
            obj.RolCausa = 3654;
            obj.Codemp = 1;
            obj.Rolid = 9594;




            Causa.ActualizarPoderJudicialCliente(1, 1, "-1", 279);

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

        [TestMethod]
        public void DescargarPorRol()
        {
            Causa bcpCausa = new Causa();
            ConsultaCausa obj = new ConsultaCausa();
            obj.RolAnio = 2015;
            obj.CodigoTribunal = 227;
            obj.NombreTribunal = "2º Juzgado de Letras de Talca ";
            obj.Url = "http://civil.poderjudicial.cl/CIVILPORWEB/";
            obj.TipoCausa = "C";
            obj.RolCausa = 3654;
            obj.Codemp = 1;
            obj.Rolid = 9566;




            Causa.ActualizarPoderJudicialRol(1, 1, "", "C", 80, 2016, "Juzgado de Letras y Gar.de Rio Bueno", 9566);

        }

        [TestMethod]
        public void TestExploraRolFecha()
        {
            List<Dimol.dto.Combobox> lstTribunales = Causa.ListarTribunalesScanner();
            ScannerCausas obj = new ScannerCausas();
            int[] anios = { 2013, 2014, 2015 };

            foreach (int anio in anios)
            {
                foreach (Dimol.dto.Combobox tribunal in lstTribunales)
                {
                    int cambiar = 0;
                    int i = 1;
                    while (cambiar < 3)
                    {
                        obj.RolAnio = 2015;// anio;
                        obj.CodigoTribunal = Int32.Parse(tribunal.Value);
                        obj.NombreTribunal = "2º Juzgado de Letras de Curico";// tribunal.Text;
                        obj.Url = "http://civil.poderjudicial.cl/CIVILPORWEB/";
                        obj.TipoCausa = "C";
                        obj.RolCausa = i;
                        obj.IdCausa = 0;
                        obj.IdCuaderno = 0;
                        try
                        {
                            Causa.ExplorarCausaConFecha(obj);
                        }
                        catch (Exception ex)
                        {
                            cambiar++;
                        }

                        i++;
                    }

                }
            }
        }

        [TestMethod]
        public void TestExploraRolActualizaFecha()
        {
            List<Dimol.dto.Combobox> lstTribunales = Causa.ListarTribunalesScanner();
            ScannerCausas obj = new ScannerCausas();
            int[] anios = { 2013, 2014, 2015 };

            List<IndiceScanner> lstRoles = Causa.ListarIndiceScannerFecha();

            foreach (IndiceScanner rol in lstRoles)
            {
                Console.WriteLine("Iniciando Scanner Fecha");

                obj.RolAnio = rol.Anio;
                obj.CodigoTribunal = rol.Tribunal;
                obj.NombreTribunal = lstTribunales.Find(x => x.Value == rol.Tribunal.ToString()).Text;
                obj.Url = "http://civil.poderjudicial.cl/CIVILPORWEB/";
                obj.TipoCausa = rol.TipoCausa;
                obj.RolCausa = rol.Rol;
                obj.IdCausa = rol.IdCausa;
                obj.IdCuaderno = 0;
                try
                {
                    Causa.ExplorarCausaActualizaFecha(obj);
                    Console.WriteLine("Causa: " + obj.TipoCausa + "-" + obj.RolCausa + "-" + obj.RolAnio);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Rol no encontrado: " + obj.TipoCausa + "-" + obj.RolCausa.ToString() + "-" + obj.RolAnio.ToString() + ", Tribunal: " + obj.NombreTribunal);
                }

            }
        }


        [TestMethod]
        public void TestActualizaRolesInternos()
        {
            List<Dimol.dto.Combobox> lstTribunales = Causa.ListarTribunalesScanner();
            ScannerCausas obj = new ScannerCausas();
            int[] anios = { 2013, 2014, 2015 };

            List<IndiceScanner> lstRoles = Causa.ListarIndiceScannerFecha();

            foreach (IndiceScanner rol in lstRoles)
            {
                Console.WriteLine("Iniciando Scanner Fecha");

                obj.RolAnio = rol.Anio;
                obj.CodigoTribunal = rol.Tribunal;
                obj.NombreTribunal = lstTribunales.Find(x => x.Value == rol.Tribunal.ToString()).Text;
                obj.Url = "http://civil.poderjudicial.cl/CIVILPORWEB/";
                obj.TipoCausa = rol.TipoCausa;
                obj.RolCausa = rol.Rol;
                obj.IdCausa = rol.IdCausa;
                obj.IdCuaderno = 0;
                try
                {
                    Causa.ExplorarCausaActualizaFecha(obj);
                    Console.WriteLine("Causa: " + obj.TipoCausa + "-" + obj.RolCausa + "-" + obj.RolAnio);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Rol no encontrado: " + obj.TipoCausa + "-" + obj.RolCausa.ToString() + "-" + obj.RolAnio.ToString() + ", Tribunal: " + obj.NombreTribunal);
                }

            }
        }
    }
}
