using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SII.bcp
{
    public class Status
    {
        public static int ConsultarRut(SII.dto.Captcha objCaptcha, dto.Status obj)
        {
            int resultado = 0;
            string urlSubmit = System.Configuration.ConfigurationManager.AppSettings["UrlSubmit"];
            NameValueCollection parametros = new NameValueCollection() { };
            parametros = new NameValueCollection() {
                                { "ACEPTAR", "Efectuar Consulta" },
                                //{ "glosarespuesta", "OK" },
                                //{ "imgCaptcha", null },
                                { "txt_captcha" ,objCaptcha.txtCaptcha},
                                //{ "largoCaptcha", null },
                                //{ "validez", "false" },
                                { "RUT", obj.Rut.ToString() },
                                { "DV", obj.DigitoVerificador},
                                { "OPC", "NOR" },
                                { "PRG", "STC" },
                                { "modo", "AJAX" },
                                { "txt_code", objCaptcha.Codigo.ToString() },
                            };
            obj.Html = SubmitPaginaSSL(urlSubmit, parametros);

            if (obj.Html.Contains("html"))
            {
                obj.ValorCaptcha = objCaptcha.Codigo;
                obj.CodigoCaptcha = objCaptcha.txtCaptcha;
                resultado = dao.Status.InsertarCaptcha(obj);
                if (resultado > 0)
                {
                    resultado = dao.Status.InsertarRutCaptcha(obj);
                }
                
            }
            return resultado;
        }

        public static int ConsultarRutDemonio(SII.dto.Captcha objCaptcha, dto.Status obj)
        {
            int resultado = 0;
            string urlSubmit = System.Configuration.ConfigurationManager.AppSettings["UrlSubmit"];
            NameValueCollection parametros = new NameValueCollection() { };
            parametros = new NameValueCollection() {
                                { "ACEPTAR", "Efectuar Consulta" },
                                //{ "glosarespuesta", "OK" },
                                //{ "imgCaptcha", null },
                                { "txt_captcha" ,objCaptcha.txtCaptcha},
                                //{ "largoCaptcha", null },
                                //{ "validez", "false" },
                                { "RUT", obj.Rut.ToString() },
                                { "DV", obj.DigitoVerificador},
                                { "OPC", "NOR" },
                                { "PRG", "STC" },
                                { "modo", "AJAX" },
                                { "txt_code", objCaptcha.Codigo.ToString() },
                            };
            obj.Html = SubmitPaginaSSL(urlSubmit, parametros);

            if (obj.Html.Contains("html"))
            {
                obj.ValorCaptcha = objCaptcha.Codigo;
                obj.CodigoCaptcha = objCaptcha.txtCaptcha;
                resultado = dao.Status.InsertarCaptcha(obj);
                if (resultado > 0)
                {
                    resultado = dao.Status.InsertarRutCaptcha(obj);
                }

            }
            return resultado;
        }

        public static int ConsultarRutDemonioOld(SII.dto.Captcha objCaptcha, dto.Status obj)
        {
            int resultado = 0;
            string urlSubmit = System.Configuration.ConfigurationManager.AppSettings["UrlSubmit"];
            NameValueCollection parametros = new NameValueCollection() { };
            parametros = new NameValueCollection() {
                                { "ACEPTAR", "    Consultar situación tributaria    " },
                                //{ "glosarespuesta", "OK" },
                                //{ "imgCaptcha", null },
                                { "txt_captcha" ,objCaptcha.txtCaptcha},
                                //{ "largoCaptcha", null },
                                //{ "validez", "false" },
                                { "RUT", obj.Rut.ToString() },
                                { "DV", obj.DigitoVerificador},
                                { "OPC", "NOR" },
                                { "PRG", "STC" },
                                { "modo", "POST" },
                                { "txt_code", objCaptcha.Codigo.ToString() },
                            };
            obj.Html = SubmitPaginaSSL(urlSubmit, parametros);

            if (obj.Html.Contains("html"))
            {
                obj.ValorCaptcha = objCaptcha.Codigo;
                obj.CodigoCaptcha = objCaptcha.txtCaptcha;
                resultado = dao.Status.InsertarCaptcha(obj);
                if (resultado > 0)
                {
                    resultado = dao.Status.InsertarRutCaptcha(obj);
                }

            }
            return resultado;
        }

        public static int ConsultarRutDemonioNew(SII.dto.Captcha objCaptcha, dto.Status obj, List<SII.dto.Combobox> lstTipoActividad, List<SII.dto.Combobox> lstTipoDocumento)
        {
            int resultado = 0;
            string urlSubmit = System.Configuration.ConfigurationManager.AppSettings["UrlSubmit"];
            NameValueCollection parametros = new NameValueCollection() { };
            parametros = new NameValueCollection() {
                                { "ACEPTAR", "    Consultar situación tributaria    " },
                                //{ "glosarespuesta", "OK" },
                                //{ "imgCaptcha", null },
                                { "txt_captcha" ,objCaptcha.txtCaptcha},
                                //{ "largoCaptcha", null },
                                //{ "validez", "false" },
                                { "RUT", obj.Rut.ToString() },
                                { "DV", obj.DigitoVerificador},
                                { "OPC", "NOR" },
                                { "PRG", "STC" },
                                { "modo", "POST" },
                                { "txt_code", objCaptcha.Codigo.ToString() },
                            };

            obj.Html = SubmitPaginaSSL(urlSubmit, parametros);
            obj.ValorCaptcha = objCaptcha.Codigo;
            obj.CodigoCaptcha = objCaptcha.txtCaptcha;

            if (obj.Html.Contains("html"))
            {
                obj.Estado = "V";
                resultado = dao.Status.InsertarCaptcha(obj);
                if (resultado > 0)
                {
                    resultado = dao.Status.InsertarRutCaptcha(obj);
                }
                Procesar.ProcesarRutHTML(obj, lstTipoActividad, lstTipoDocumento);
            }
            else
            {
                obj.Estado = "E";
                resultado = dao.Status.InsertarCaptcha(obj);
                if (resultado > 0)
                {
                    resultado = dao.Status.InsertarRutCaptcha(obj);
                }
            }
                        
            return resultado;
        }

        public static dto.Captcha ObtenerCaptcha()
        {
            HtmlDocument html = new HtmlDocument();
            string JSONdata = "";
            string urlConsulta = System.Configuration.ConfigurationManager.AppSettings["UrlGetCaptcha"];
            dto.Captcha obj = new dto.Captcha();
            NameValueCollection parametros = new NameValueCollection() { };
            parametros = new NameValueCollection() {
                                { "oper", "0" },
                            };
            try
            {
                JSONdata = SubmitPaginaSSL(urlConsulta, parametros);

                DataContractJsonSerializer jsonSer = new DataContractJsonSerializer(typeof(dto.Captcha));

                MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(JSONdata));

                obj = (dto.Captcha)jsonSer.ReadObject(stream);
            } 
            catch(Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "ObtenerCaptcha: " + urlConsulta, 0);
            }

            return obj;
        }

        static string ConsultarPaginaSSL(string url)
        {
            Uri address = new Uri(url);

            ServicePointManager.ServerCertificateValidationCallback += ValidateRemoteCertificate;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;

            using (WebClient webClient = new WebClient())
            {
                var stream = webClient.OpenRead(address);
                using (StreamReader sr = new StreamReader(stream))
                {
                    var page = sr.ReadToEnd();
                    return page.ToString();
                }
            }
        }

        static string SubmitPaginaSSL(string url, NameValueCollection parametros)
        {
            Uri address = new Uri(url);
            string sContents = "";
            ServicePointManager.ServerCertificateValidationCallback += ValidateRemoteCertificate;
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;

            using (WebClient webClient = new WebClient())
            {
                try
                {
                    byte[] response = webClient.UploadValues(address, parametros);

                    sContents = System.Text.Encoding.Default.GetString(response);
                } catch(Exception ex) {
                    Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "SubmitPaginaSSL: " + url, 0);
                }

            }
            return sContents;
        }

        public static string DescargarCaptchaSSL(SII.dto.Captcha objCaptcha)
        {
            Uri address = new Uri(System.Configuration.ConfigurationManager.AppSettings["UrlViewCaptcha"] + "&txtCaptcha=" + objCaptcha.txtCaptcha);
            string sContents = "";
            ServicePointManager.ServerCertificateValidationCallback += ValidateRemoteCertificate;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;

            using (WebClient webClient = new WebClient())
            {
                if (!File.Exists(@"d:\temp\" + objCaptcha.txtCaptcha.Substring(0, objCaptcha.txtCaptcha.Length - 2) + ".png"))
                {
                    webClient.DownloadFile(address, @"d:\temp\" + objCaptcha.txtCaptcha.Substring(0, objCaptcha.txtCaptcha.Length - 2) + ".png");
                }
            }
            return sContents;
        }

        /// <summary>
        /// Certificate validation callback.
        /// </summary>
        private static bool ValidateRemoteCertificate(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors error)
        {
            // If the certificate is a valid, signed certificate, return true.
            if (error == System.Net.Security.SslPolicyErrors.None)
            {
                return true;
            }

            Console.WriteLine("X509Certificate [{0}] Policy Error: '{1}'",
                cert.Subject,
                error.ToString());

            return false;
        }

        public static List<dto.Status> ListarRutporEstado(string estado)
        {
            return dao.Status.ListarRutporEstado(estado);
        }

        public static string BuscarCaptcha(string captcha)
        {
            return dao.Status.BuscarCaptcha(captcha);
        }

        public static List<dto.Status> ListarRutporDemonio(string estado)
        {
            return dao.Status.ListarRutporDemonio(estado);
        }

        public static List<dto.Status> ListarRutporDemonioNew(string estado)
        {
            return dao.Status.ListarRutporDemonioNew(estado);
        }

        public static int DetenerRutporDemonio(string estado)
        {
            return dao.Status.DetenerRutporDemonio(estado);
        }

        public static List<dto.Combobox> ListarActividadEconomica()
        {
            return dao.Status.ListarActividadEconomica();
        }

        public static List<dto.Combobox> ListarTipoDocumento()
        {
            return dao.Status.ListarTipoDocumento();
        }

    }
}
