using HtmlAgilityPack;
using PJSpider.dto;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace PJSpyFecha2
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
                string indicadores = "";
                Console.WriteLine("Iniciando descarga");
                List<ScannerHTML> lst = PJSpider.bcp.Causa.ListarRolesHTML();
                Console.WriteLine("Roles a descargar: " + lst.Count);
                //string fileName = "http://civil.poderjudicial.cl/CIVILPORWEB/ConsultaDetalleAtPublicoAccion.do?TIP_Consulta=1&TIP_Cuaderno=1&CRR_IdCuaderno=12580547&ROL_Causa=476&TIP_Causa=C&ERA_Causa=2013&CRR_IdCausa=10111725&COD_Tribunal=2&TIP_Informe=1&";
                //Indicadores obj = new Indicadores();
                while (lst.Count > 0)
                {
                    foreach (ScannerHTML s in lst)
                    {
                        try
                        {
                            indicadores = DescargaPagina(s.Url);
                            PJSpider.bcp.Causa.InsertarPoderJudicialRolHTML(s.IdCausa, s.IdCuaderno, s.TipoCausa, s.Rol, s.Anio, s.Tribunal, indicadores);
                        }
                        catch (Exception ex)
                        {
                            Exception exx = new Exception("El servicio de datos no esta disponible.", ex);
                            throw exx;
                        }
                    }
                    lst = PJSpider.bcp.Causa.ListarRolesHTML();
                    Console.WriteLine("Roles a descargar: " + lst.Count);
                }
                Console.WriteLine("Fin descarga");
            }
            else
            {
                CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
                string indicadores = "";
                Console.WriteLine("Iniciando descarga");
                List<ScannerHTML> lst = PJSpider.bcp.Causa.ListarRolesHTMLOrden("A");
                Console.WriteLine("Roles a descargar: " + lst.Count);
                //string fileName = "http://civil.poderjudicial.cl/CIVILPORWEB/ConsultaDetalleAtPublicoAccion.do?TIP_Consulta=1&TIP_Cuaderno=1&CRR_IdCuaderno=12580547&ROL_Causa=476&TIP_Causa=C&ERA_Causa=2013&CRR_IdCausa=10111725&COD_Tribunal=2&TIP_Informe=1&";
                //Indicadores obj = new Indicadores();
                while (lst.Count > 0)
                {
                    foreach (ScannerHTML s in lst)
                    {
                        try
                        {
                            indicadores = DescargaPagina(s.Url);
                            PJSpider.bcp.Causa.InsertarPoderJudicialRolHTML(s.IdCausa, s.IdCuaderno, s.TipoCausa, s.Rol, s.Anio, s.Tribunal, indicadores);
                        }
                        catch (Exception ex)
                        {
                            Exception exx = new Exception("El servicio de datos no esta disponible.", ex);
                            throw exx;
                        }
                    }
                    lst = PJSpider.bcp.Causa.ListarRolesHTML();
                    Console.WriteLine("Roles a descargar: " + lst.Count);
                }
                Console.WriteLine("Fin descarga");
                //PJSpider.bcp.Causa.ActualizaFechaIngresoCausa();
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
    }
}
