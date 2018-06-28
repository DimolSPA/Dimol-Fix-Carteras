using PJSpider.dto;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Configuration;

namespace UPJSpider
{
    class Program
    {

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
                string indicadores = "";
                string fileName = "http://civil.poderjudicial.cl/CIVILPORWEB/AtPublicoDAction.do";
                NameValueCollection parametros = new NameValueCollection();
                ScannerHTML obj = PJSpider.bcp.Causa.UltimaListaHTML();
                string fechaConsulta = DateTime.Today.ToString("dd/MM/yyyy");
                int cambiar = 0, anio=0,rol=0, limiteCambio = 6;
                if (obj.Anio != 0)
                {
                    anio = obj.Anio;
                }
                else { 
                    anio = 2015; 
                }
                if (obj.Rol != 0)
                {
                    rol = obj.Rol + 1;
                }
                else
                {
                    rol = 1;
                }
                string tipo = "C";
                Console.WriteLine("Iniciando scanner lista de roles, anio: "+ anio.ToString());
                while (anio > 1900)
                {
                    while (cambiar < limiteCambio)
                    {
                        try
                        {
                            parametros = new NameValueCollection() {
                                { "APE_Materno", "" },
                                { "APE_Paterno", "" },
                                { "COD_Tribunal", "0" },
                                { "ERA_Causa" ,anio.ToString()},
                                { "FEC_Desde", "20/04/2016" },
                                { "FEC_Hasta", "20/04/2016" },
                                { "NOM_Consulta", "" },
                                { "ROL_Causa", rol.ToString()},
                                { "RUC_Dv", "" },
                                { "RUC_Era", "" },
                                { "RUC_Numero", "" },
                                { "RUC_Tribunal", "3" },
                                { "RUT_Consulta", "" },
                                { "RUT_DvConsulta", "" },
                                { "SEL_Litigantes", "0" },
                                { "SeleccionL", "0" },
                                { "TIP_Causa", tipo },
                                { "TIP_Consulta", "1" },
                                { "TIP_Lengueta", "tdUno" },
                                { "irAccionAtPublico", "Consulta" },
                            };
                            Console.WriteLine("Descargando HTML " +tipo+"-"+rol.ToString()+"-"+anio.ToString());
                            indicadores = ConsultarRol(fileName, parametros);
                            if (indicadores.Contains("textoC"))
                            {
                                Console.WriteLine("Grabando HTML " + tipo + "-" + rol.ToString() + "-" + anio.ToString());
                                PJSpider.bcp.Causa.InsertarListaRolHTML(tipo, rol, anio, indicadores);
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
                    anio--;
                    rol = 1;
                    cambiar = 0;
                    Console.WriteLine("Cambio anio "+anio.ToString());
                }

                Console.WriteLine("Fin descarga lista de roles");
            }
            else
            {
                CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
                string indicadores = "";
                string fileName = "http://civil.poderjudicial.cl/CIVILPORWEB/AtPublicoDAction.do";
                NameValueCollection parametros = new NameValueCollection();
                
                string fechaConsulta = DateTime.Today.ToString("dd/MM/yyyy");
                int cambiar = 0, anio = Int32.Parse(args[0]), rol = 0, limiteCambio = 6;
                ScannerHTML obj = PJSpider.bcp.Causa.UltimaListaHTML(anio);
                if (args.Length == 2)
                {
                    limiteCambio = Int32.Parse(args[1]);
                }
                if (obj.Rol != 0 )
                {
                    rol = obj.Rol + 1;
                }
                else
                {
                    rol = 1;
                }
                string tipo = "C";
                Console.WriteLine("Iniciando scanner lista de roles, anio: " + anio.ToString());
                while (anio > 1900)
                {
                    obj = PJSpider.bcp.Causa.UltimaListaHTML(anio);

                    if (obj.Rol != 0)
                    {
                        rol = obj.Rol + 1;
                    }
                    else
                    {
                        rol = 1;
                    }
                    while (cambiar < limiteCambio)
                    {
                        try
                        {
                            parametros = new NameValueCollection() {
                                { "APE_Materno", "" },
                                { "APE_Paterno", "" },
                                { "COD_Tribunal", "0" },
                                { "ERA_Causa" ,anio.ToString()},
                                { "FEC_Desde", "20/04/2016" },
                                { "FEC_Hasta", "20/04/2016" },
                                { "NOM_Consulta", "" },
                                { "ROL_Causa", rol.ToString()},
                                { "RUC_Dv", "" },
                                { "RUC_Era", "" },
                                { "RUC_Numero", "" },
                                { "RUC_Tribunal", "3" },
                                { "RUT_Consulta", "" },
                                { "RUT_DvConsulta", "" },
                                { "SEL_Litigantes", "0" },
                                { "SeleccionL", "0" },
                                { "TIP_Causa", tipo },
                                { "TIP_Consulta", "1" },
                                { "TIP_Lengueta", "tdUno" },
                                { "irAccionAtPublico", "Consulta" },
                            };
                            Console.WriteLine("Descargando HTML " + tipo + "-" + rol.ToString() + "-" + anio.ToString());
                            indicadores = ConsultarRol(fileName, parametros);
                            if (indicadores.Contains("textoC"))
                            {
                                Console.WriteLine("Grabando HTML " + tipo + "-" + rol.ToString() + "-" + anio.ToString());
                                PJSpider.bcp.Causa.InsertarListaRolHTML(tipo, rol, anio, indicadores);
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
                    anio--;
                    rol = 1;
                    cambiar = 0;
                    Console.WriteLine("Cambio anio " + anio.ToString());
                }

                Console.WriteLine("Fin descarga lista de roles");
            }

        }

        public static string DescargaPagina(string fileName)
        {

            string sContents = string.Empty;
            if (fileName.ToLower().IndexOf("http:") > -1)
            {
                // URL
                System.Net.WebClient wc = new System.Net.WebClient();
                wc.Encoding = Encoding.Default;
                return wc.DownloadString(fileName);
                //byte[] response = wc.DownloadData(fileName);
                //sContents = System.Text.Encoding.UTF8.GetString(response);
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

        public static string ConsultarRol(string fileName, NameValueCollection parametros)
        {

            string sContents = string.Empty;
            if (fileName.ToLower().IndexOf("http:") > -1)
            {
                // URL
                System.Net.WebClient wc = new System.Net.WebClient();
                wc.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
                wc.Headers.Add("Accept-Encoding", "gzip, deflate");
                wc.Headers.Add("Accept-Language", "en-US,en;q=0.5");
                wc.Headers.Add("Cookie", ConfigurationManager.AppSettings["Cookie"]); 
                wc.Headers.Add("Host", "civil.poderjudicial.cl");
                wc.Headers.Add("Referer", "http://civil.poderjudicial.cl/CIVILPORWEB/AtPublicoViewAccion.do?tipoMenuATP=1");
                wc.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.3; WOW64; rv:41.0) Gecko/20100101 Firefox/41.0");
                wc.Encoding = Encoding.Default;
                byte[] response = wc.UploadValues(fileName,parametros);
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
    }
}

