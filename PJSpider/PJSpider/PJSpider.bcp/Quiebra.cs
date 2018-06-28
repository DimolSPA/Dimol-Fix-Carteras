using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PJSpider.bcp
{
    public class Quiebra
    {
        public static void DescargarQuiebrasHTML(int anio)
        {
            IWebDriver driver = new FirefoxDriver();
            driver.Navigate().GoToUrl(System.Configuration.ConfigurationManager.AppSettings["UrlQuiebras"]);
            try
            {
                while (!driver.Title.Contains("Sistema de"))
                {
                    Dimol.dao.Funciones.InsertarError("Explorador de Causas del Poder Judicial offline.", "", "Explorador Poder Judicial", anio);
                    Thread.Sleep(600000);
                    driver.Navigate().GoToUrl(System.Configuration.ConfigurationManager.AppSettings["UrlPJ"]);
                }

                if (driver.Title.Contains("Sistema de"))
                {
                    string cookies = GetCookies(driver);
                    CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
                    string indicadores = "";
                    string fileName = ConfigurationManager.AppSettings["UrlDetalle"];//"http://civil.poderjudicial.cl/CIVILPORWEB/AtPublicoDAction.do";
                    NameValueCollection parametros = new NameValueCollection();

                    string fechaConsulta = DateTime.Today.ToString("dd/MM/yyyy");
                    int cambiar = 0, rol = 1, limiteCambio = 6;
                    string tipo = "C";
                    bool error = true;
                    Console.WriteLine("Iniciando scanner quiebras");
                    rol = 1;
                    while (cambiar < limiteCambio)
                    {
                        error = true;
                        try
                        {
                            //parametros = new NameValueCollection() {
                            //    { "accion", "" },
                            //    { "nombreQuiebra", "" },
                            //    { "rutQuiebra", "" },
                            //    { "dvQuiebra" ,""},
                            //    { "fechaPublicacionDesde", "" },
                            //    { "fechaPublicacionHasta", "" },
                            //    { "sindico", "0" },
                            //    { "region", "0"},
                            //    { "tregistroQuiebra_tr_", "true" },
                            //    { "tregistroQuiebra_p_", "1" },
                            //    { "tregistroQuiebra_mr_", "20" }
                            //};

                            parametros = new NameValueCollection() {
                                { "id", "" },
                                { "tipo", "quiebra" }
                            };

                            Console.WriteLine("Descargando HTML " + tipo + "-" + rol.ToString() + "-" + anio.ToString());
                            while (error)
                            {
                                try
                                {
                                    indicadores = ConsultarQuiebra(fileName, parametros, cookies);
                                    error = false;
                                }
                                catch (Exception ex)
                                {
                                    Thread.Sleep(600000);
                                    error = true;
                                }
                            }

                            if (indicadores.Contains("textoC"))
                            {
                                Console.WriteLine("Grabando HTML " + tipo + "-" + rol.ToString() + "-" + anio.ToString());
                                //PJSpider.bcp.Causa.InsertarListaRolHTML(tipo, rol, anio, indicadores);
                                cambiar = 0;
                            }
                            else
                            {
                                cambiar++;
                            }
                            rol++;
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    Console.WriteLine("Fin anio " + anio.ToString());
                }
            }
            catch (Exception ex)
            {

            }


            Console.WriteLine("Fin descarga lista de roles");
        }

        public static string ConsultarQuiebra(string fileName, NameValueCollection parametros, string cookie)
        {

            string sContents = string.Empty;
            if (fileName.ToLower().IndexOf("http:") > -1)
            {
                // URL
                System.Net.WebClient wc = new System.Net.WebClient();
                wc.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
                wc.Headers.Add("Accept-Encoding", "gzip, deflate");
                wc.Headers.Add("Accept-Language", "en-US,en;q=0.5");
                wc.Headers.Add("Cache-Control", "max-age=0");
                //wc.Headers.Add("Connection", "keep-alive");
                wc.Headers.Add("Cookie", cookie);
                wc.Headers.Add("Host", "atencion.superir.gob.cl");
                wc.Headers.Add("Referer", "http://atencion.superir.gob.cl/AtencionPublicoWeb/registroQuiebra.do");
                wc.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.3; WOW64; rv:41.0) Gecko/20100101 Firefox/41.0");
                wc.Encoding = Encoding.Default;
                byte[] response = wc.UploadValues(fileName, parametros);
                sContents = System.Text.Encoding.Default.GetString(response);
            }
            else
            {
                // Regular Filename
                System.IO.StreamReader sr = new System.IO.StreamReader(fileName);
                sContents = sr.ReadToEnd();
                sr.Close();
            }
            return sContents;
        }

        public static string GetCookies(IWebDriver driver)
        {
            var _cookies = driver.Manage().Cookies.AllCookies;
            string cookies = "";
            foreach (var cookie in _cookies)
            {
                cookies = cookies + cookie.Name + "=" + cookie.Value + ";";
            }
            return cookies;
        }

    }
}
