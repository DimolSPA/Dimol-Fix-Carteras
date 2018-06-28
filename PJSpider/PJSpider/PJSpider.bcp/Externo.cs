using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using PJSpider.dto;
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
    public class Externo
    {
        #region "Descargar Roles Externos"

        public static void DescargarRolesExternos(int inicio, int termino, int anio)
        {
            Dimol.dao.Funciones.InsertarError("Inicio Demonio Interno", "", "Procesa Rol Interno HTML", 0);
            FirefoxBinary binary = new FirefoxBinary(System.Configuration.ConfigurationManager.AppSettings["PathFirefox"]);
            FirefoxProfile profile = new FirefoxProfile();
            IWebDriver driver = new FirefoxDriver(binary, profile);
            driver.Navigate().GoToUrl(System.Configuration.ConfigurationManager.AppSettings["UrlPJ"]);
            bool resultado = false;
            //Lista de roles a revisar
            int indice = 0, cambiar = 0,max =1;
            List<RolActualizar> lstRoles = new List<RolActualizar>();
            List<Dimol.dto.Combobox> lstTribunales = Causa.ListarTribunalesScannerRango(inicio, termino);
            List<DatoTipo> lstTipoCuaderno = dao.Causa.ListarTipoCuaderno();
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            foreach (Dimol.dto.Combobox trib in lstTribunales)
            {
                lstRoles = dao.Causa.ListarRolesExternos(anio, Int32.Parse(trib.Value));
                foreach (RolActualizar obj in lstRoles)
                {
                    Console.WriteLine(obj.Rol.ToString() + "-" + obj.IdTribunal);
                    if (indice + 1 < obj.Rol)
                    {
                        while (indice + 1 < obj.Rol)
                        {
                            Console.WriteLine("Pendiente: " + obj.Rol.ToString() + "-" + obj.IdTribunal);
                            obj.Rol = indice + 1;
                            DescargarListaHTML(obj, driver, lstTipoCuaderno);
                            indice++;
                        }

                    }
                    else
                    {
                        indice++;
                    }                  
                }

                max = indice;
                // si es el primero o llegamos al ultimo
                while (cambiar < 5)
                {
                    RolActualizar obj = new RolActualizar();
                    obj.Anio = anio;
                    obj.IdTribunal = Int32.Parse(trib.Value);
                    obj.Tribunal = trib.Text;
                    obj.Url = "http://civil.poderjudicial.cl/CIVILPORWEB/";
                    obj.TipoCausa = "C";
                    obj.Rol = max;
                    obj.IdCausa = 0;
                    obj.IdCuaderno = 0;
                    try
                    {
                        resultado = DescargarListaHTML(obj, driver, lstTipoCuaderno);
                        Console.WriteLine("Causa: " + obj.TipoCausa + "-" + obj.Rol + "-" + obj.Anio);
                        if (!resultado)
                        {
                            cambiar++;
                        }
                        else
                        {
                            cambiar = 0;
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        cambiar++;
                        Console.WriteLine("Rol no encontrado: " + obj.TipoCausa + "-" + obj.Rol.ToString() + "-" + obj.Anio.ToString() + ", Tribunal: " + obj.Tribunal);
                    }

                    max++;
                }
                cambiar = 0;
                indice = 0;
            }
            //List<RolActualizar> lstRoles = new List<RolActualizar>();
            //Dimol.dao.Funciones.InsertarError("Generando lista de roles para actualizar", "Estados: " + estados, "Bot Poder Judicial", 0);
            //lstRoles = dao.Causa.ListarRolesActualizarDemonio(1, 1, "-1", inicio, termino);
            //List<DatoTipo> lstTipoCuaderno = dao.Causa.ListarTipoCuaderno();

            

            //Descargar lista y rol
            //foreach (RolActualizar s in lstRoles)
            //{
            //    DescargarListaHTML(s, driver, lstTipoCuaderno);
            //}

            Dimol.dao.Funciones.InsertarError("Fin Demonio Interno", "", "Procesa Rol Interno HTML", 0);
            //Close the browser
            driver.Quit();
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

        public static bool DescargarListaHTML(RolActualizar s, IWebDriver driver, List<DatoTipo> lstTipoCuaderno)
        {

            string cookies = GetCookies(driver);
            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
            string indicadores = "";
            string fileName = ConfigurationManager.AppSettings["UrlLista"];//"http://civil.poderjudicial.cl/CIVILPORWEB/AtPublicoDAction.do";
            NameValueCollection parametros = new NameValueCollection();
            string fechaConsulta = DateTime.Today.ToString("dd/MM/yyyy");
            bool resultado = true;
            Console.WriteLine("Descargando roles, numero: " + s.Rol.ToString() + ", anio: " + s.Anio.ToString());
            try
            {
                parametros = new NameValueCollection() {
                                { "APE_Materno", "" },
                                { "APE_Paterno", "" },
                                { "COD_Tribunal", s.IdTribunal.ToString() },
                                { "ERA_Causa" , s.Anio.ToString()},
                                { "FEC_Desde", "20/04/2016" },
                                { "FEC_Hasta", "20/04/2016" },
                                { "NOM_Consulta", "" },
                                { "ROL_Causa", s.Rol.ToString()},
                                { "RUC_Dv", "" },
                                { "RUC_Era", "" },
                                { "RUC_Numero", "" },
                                { "RUC_Tribunal", "3" },
                                { "RUT_Consulta", "" },
                                { "RUT_DvConsulta", "" },
                                { "SEL_Litigantes", "0" },
                                { "SeleccionL", "0" },
                                { "TIP_Causa", s.TipoCausa ?? "C"},
                                { "TIP_Consulta", "1" },
                                { "TIP_Lengueta", "tdUno" },
                                { "irAccionAtPublico", "Consulta" },
                            };
                Console.WriteLine("Descargando HTML " + s.TipoCausa + "-" + s.Rol.ToString() + "-" + s.Anio.ToString());

                try
                {
                    indicadores = Causa.ConsultarRol(fileName, parametros, cookies);
                    s.HTML = indicadores;
                    if (indicadores.Contains("textoC"))
                    {
                        Console.WriteLine("Grabando HTML " + s.TipoCausa + "-" + s.Rol.ToString() + "-" + s.Anio.ToString());
                        //dao.Causa.InsertarListaRolHTML(s.TipoCausa, s.Rol, s.Anio, indicadores);

                        resultado = DescargarRolListaHTML(s, lstTipoCuaderno);// lstTipoCuaderno)
                    }
                    else
                    {
                        resultado = false;
                    }

                }
                catch (Exception ex)
                {
                    Thread.Sleep(20000); //un minuto de espera
                    resultado = false;
                }


                if (indicadores.Contains("textoC"))
                {
                    Console.WriteLine("Grabando HTML " + s.TipoCausa + "-" + s.Rol.ToString() + "-" + s.Anio.ToString());
                    PJSpider.bcp.Causa.InsertarListaRolHTML(s.TipoCausa, s.Rol, s.Anio, indicadores);
                    resultado = true;
                }
                else
                {
                    resultado = false;
                }
            }
            catch (Exception ex)
            {
                resultado = false;
            }
            return resultado;
            //Console.WriteLine("Fin roles, numero: " + numero.ToString() + ", anio: " + anio.ToString());
            //Console.WriteLine("Fin descarga lista de roles");
        }

        public static bool DescargarRolListaHTML(RolActualizar s, List<DatoTipo> lstTipoCuaderno)
        {
            HtmlDocument html = new HtmlDocument();
            HtmlDocument htmlR = new HtmlDocument();
            string htmlCuaderno = "", urlCuaderno = "";
            int idTribunal = 0;
            int tipoCuaderno = 0;
            bool resultado = true;
            try
            {
                html.LoadHtml(s.HTML);

                var myTable = html.DocumentNode
                                .Descendants()
                                .Where(e => e.Attributes.Contains("id"))
                                .SingleOrDefault(e => e.Attributes["id"].Value == "contentCellsAddTabla");
                int columna = 1;
                string linea = "", htmlRol = "", attributeValue = "", url = "";
                foreach (HtmlNode row in myTable.SelectNodes("tbody//tr"))
                {
                    columna = 1;
                    linea = "";
                    idTribunal = 0;
                    foreach (HtmlNode cell in row.SelectNodes("td"))
                    {
                        switch (columna)
                        {
                            case 1:
                                foreach (HtmlNode node in cell.SelectNodes("a"))
                                {
                                    attributeValue = "http://civil.poderjudicial.cl" + node.GetAttributeValue("href", "");
                                    if (!string.IsNullOrEmpty(attributeValue))
                                    {
                                        url = attributeValue;
                                        string queryString = url.Remove(0, url.IndexOf('?') + 1);
                                        string[] parametros = queryString.Split('&');
                                        string[] parametro;
                                        foreach (string st in parametros)
                                        {
                                            parametro = st.Split('=');
                                            switch (parametro[0])
                                            {
                                                case "CRR_IdCausa":
                                                    s.IdCausa = Int32.Parse(parametro[1]);
                                                    break;
                                                case "COD_Tribunal":
                                                    idTribunal = Int32.Parse(parametro[1]);
                                                    break;
                                                case "CRR_IdCuaderno":
                                                    s.IdCuaderno = Int32.Parse(parametro[1]);
                                                    break;
                                                default:
                                                    break;
                                            }
                                        }
                                    }
                                    linea = linea + attributeValue + "|";
                                }
                                break;
                        }
                        columna++;
                    }
                    try
                    {
                        if (s.IdTribunal == idTribunal)
                        {
                            htmlRol = Causa.DescargaPagina(url);
                            if (htmlRol.Contains("textoC"))
                            {
                                dao.Causa.InsertarRolPoderJudicial(s.Codemp, s.Rolid, s.TipoCausa, s.IdCausa, s.IdTribunal);
                                dao.Causa.InsertarRolHTMLURL(s.IdCausa, s.IdCuaderno, s.TipoCausa, s.Rol, s.Anio, s.IdTribunal, htmlRol, "L", url);
                                htmlR.LoadHtml(htmlRol);
                                foreach (HtmlNode cuaderno in htmlR.DocumentNode.SelectNodes("//select[@name='CRR_Cuaderno']"))
                                {
                                    tipoCuaderno = 0;
                                    string idCuaderno = "";
                                    string descCuaderno = "";
                                    long test = 0;
                                    foreach (HtmlNode option in cuaderno.SelectNodes("option"))
                                    {
                                        idCuaderno = option.GetAttributeValue("value", "");
                                        if (string.IsNullOrEmpty(option.InnerHtml))
                                        {
                                            descCuaderno = option.NextSibling.InnerHtml;
                                        }
                                        else
                                        {
                                            descCuaderno = option.InnerHtml;
                                        }

                                        if (Int64.TryParse(idCuaderno, out test) && !string.IsNullOrEmpty(descCuaderno))
                                        {

                                            if (lstTipoCuaderno.Find(x => x.Nombre == descCuaderno) == null)
                                            {
                                                dao.Causa.InsertarPoderJudicialTipoCuaderno(Int32.Parse(idCuaderno), descCuaderno);
                                                lstTipoCuaderno = dao.Causa.ListarTipoCuaderno();
                                                //dao.Causa.InsertarCuadernoHTML(s.IdCausa, Int32.Parse(idCuaderno), descCuaderno, idCuaderno == s.IdCuaderno.ToString() ? "N" : "S");
                                            }

                                            tipoCuaderno = lstTipoCuaderno.Find(x => x.Nombre == descCuaderno).Id;
                                            dao.Causa.InsertarCuadernoHTMLFull(s.IdCausa, Int32.Parse(idCuaderno), descCuaderno, idCuaderno == s.IdCuaderno.ToString() ? "N" : "S", tipoCuaderno);
                                            urlCuaderno = "http://civil.poderjudicial.cl/CIVILPORWEB/ConsultaDetalleAtPublicoAccion.do?TIP_Consulta=1&TIP_Cuaderno=" + tipoCuaderno + "&CRR_IdCuaderno=" + idCuaderno + "&ROL_Causa=" + s.Rol + "&TIP_Causa=" + s.TipoCausa + "&ERA_Causa=" + s.Anio + "&CRR_IdCausa=" + s.IdCausa + "&COD_Tribunal=" + s.IdTribunal + "&TIP_Informe=1&";
                                            htmlCuaderno = Causa.DescargaPagina(urlCuaderno);
                                            if (htmlRol.Contains("textoC"))
                                            {
                                                dao.Causa.InsertarRolHTMLURL(s.IdCausa, Int32.Parse(idCuaderno), s.TipoCausa, s.Rol, s.Anio, s.IdTribunal, htmlCuaderno, "L", urlCuaderno);
                                            }
                                            s.IdCuaderno = Int32.Parse(idCuaderno);
                                            s.HTML = htmlCuaderno;
                                            resultado = ProcesarRolHTMLDemonio(s, lstTipoCuaderno);

                                        }

                                    }
                                }
                            }

                            else
                            {
                                resultado = false;
                                //PJSpider.bcp.Causa.InsertarRolHTMLURL(s.IdCausa, s.IdCuaderno, s.TipoCausa, s.Rol, s.Anio, s.Tribunal, "", "M", url);
                            }
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        dao.Causa.InsertarRolHTMLURL(s.IdCausa, s.IdCuaderno, s.TipoCausa, s.Rol, s.Anio, s.IdTribunal, "", "M", url);
                        Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Procesa Lista HTML Poder Judicial", s.Anio);
                        resultado = false;
                        throw ex;
                        
                    }
                    //Console.WriteLine("Rol :" + s.TipoCausa + "-" + s.Rol.ToString() + "-" + s.Anio.ToString() + ", ID Tribunal: " + s.Tribunal.ToString());
                }
                //dao.Causa.MarcarLeidaListaRolHTML(s.TipoCausa, s.Rol, s.Anio, "P");
                //resultado = true;
            }
            catch (Exception ex)
            {
                dao.Causa.MarcarLeidaListaRolHTML(s.TipoCausa, s.Rol, s.Anio, "M");
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Error cargar Lista HTML Poder Judicial", s.Anio);
                //throw ex;
                resultado = false;
            }
            return resultado;
        }

        public static bool ProcesarRolHTMLDemonio(RolActualizar s, List<DatoTipo> lstTipoCuaderno)
        {
            Console.WriteLine("Inicio carga roles desde HTML");
            //List<ScannerHTML> lst = PJSpider.dao.Causa.ListarCargaRolHTMLDemonio(anio, estado);
            //Console.WriteLine("Lista de roles: " + lst.Count.ToString());
            ScannerCausas objCausa = new ScannerCausas();
            HtmlDocument html = new HtmlDocument();
            string fecha = "";
            string format = "dd/MM/yyyy";
            bool resultado = false;
            fecha = "";
            if (s.HTML.Contains("Participante"))
            {
                try
                {
                    html.LoadHtml(s.HTML);
                    try
                    {
                        // Grabar rol
                        objCausa.IdCausa = s.IdCausa;
                        objCausa.IdCuaderno = s.IdCuaderno;
                        objCausa.TipoCausa = s.TipoCausa;
                        objCausa.RolCausa = s.Rol;
                        objCausa.RolAnio = s.Anio;
                        objCausa.CodigoTribunal = s.IdTribunal;
                        string[] ruta = new string[] { "", "" };
                        foreach (HtmlNode td in html.DocumentNode.SelectNodes("//td[contains(text(),'Texto Demanda')]"))
                        {
                            foreach (HtmlNode node in td.SelectNodes("img"))
                            {
                                if (node.GetAttributeValue("src", "").Contains("/CIVILPORWEB/img/Comun/DocVacio.JPG"))
                                {
                                    ruta = new string[] { "", "" };
                                }
                                else if (node.GetAttributeValue("src", "").Contains("/CIVILPORWEB/img/Comun/edit.jpg"))
                                {
                                    ruta = node.GetAttributeValue("onclick", "").Split('\'');
                                }
                                else if (node.GetAttributeValue("src", "").Contains("/CIVILPORWEB/img/Comun/generarpdf.gif"))
                                {
                                    ruta = node.GetAttributeValue("onclick", "").Split('\'');
                                }
                            }
                        }
                        foreach (HtmlNode td in html.DocumentNode.SelectNodes("//*[text()[contains(., 'F. Ing :')]]"))
                        {
                            fecha = td.InnerText.Replace("F. Ing :", "").Replace(" \r\n\t\t", "");
                            if (!string.IsNullOrEmpty(fecha))
                            {
                                objCausa.FechaIngreso = DateTime.ParseExact(fecha.Trim(), format, CultureInfo.InvariantCulture);
                            }
                            else
                            {
                                objCausa.FechaIngreso = new DateTime();
                            }
                        }
                        if (!string.IsNullOrEmpty(ruta[1]))
                        {
                            objCausa.RutaDemanda = "http://civil.poderjudicial.cl" + ruta[1];
                        }
                        else
                        {
                            objCausa.RutaDemanda = null;
                        }

                        dao.Causa.InsertarRolHTML(objCausa.IdCausa, objCausa.TipoCausa, objCausa.RolCausa, objCausa.RolAnio, objCausa.CodigoTribunal, objCausa.RutaDemanda, objCausa.FechaIngreso);

                        // Grabar cuadernos
                        HtmlNode.ElementsFlags.Remove("option");
                        string idCuaderno = "", descCuaderno = "";
                        long test = 0;
                        foreach (HtmlNode cuaderno in html.DocumentNode.SelectNodes("//select[@name='CRR_Cuaderno']"))
                        {
                            foreach (HtmlNode option in cuaderno.SelectNodes("option"))
                            {
                                idCuaderno = option.GetAttributeValue("value", "");
                                if (string.IsNullOrEmpty(option.InnerHtml))
                                {
                                    descCuaderno = option.NextSibling.InnerHtml;
                                }
                                else
                                {
                                    descCuaderno = option.InnerHtml;
                                }

                                if (Int64.TryParse(idCuaderno, out test) && !string.IsNullOrEmpty(descCuaderno))
                                {
                                    dao.Causa.InsertarCuadernoHTML(objCausa.IdCausa, Int32.Parse(idCuaderno), descCuaderno, idCuaderno == s.IdCuaderno.ToString() ? "N" : "S");
                                }

                            }
                        }

                        // Grabar Litigantes
                        int i = 1;
                        TablaLitigantes tbl = new TablaLitigantes();
                        foreach (HtmlNode litigantes in html.DocumentNode.SelectNodes("//div[@id='Litigantes']"))
                        {
                            foreach (HtmlNode table in litigantes.SelectNodes("table"))
                            {
                                foreach (HtmlNode row in table.SelectNodes("tbody/tr"))
                                {
                                    i = 1;
                                    tbl = new TablaLitigantes();
                                    foreach (HtmlNode cell in row.SelectNodes("td"))
                                    {
                                        switch (i)
                                        {
                                            case 1:
                                                tbl.Participante = cell.InnerText;
                                                break;
                                            case 2:
                                                tbl.Rut = cell.InnerText;
                                                break;
                                            case 3:
                                                tbl.Persona = cell.InnerText;
                                                break;
                                            case 4:
                                                tbl.NombreRazonSocial = cell.InnerText;
                                                break;
                                        }
                                        if (tbl.Participante == "Participante")
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            i++;
                                        }

                                    }
                                    if (tbl.Participante != "Participante")
                                    {
                                        dao.Causa.InsertarLitiganteHTML(objCausa.IdCausa, objCausa.IdCuaderno, tbl.Participante, tbl.Rut, tbl.Persona, tbl.NombreRazonSocial);
                                    }
                                }
                            }
                        }
                        Console.WriteLine("Rol :" + s.TipoCausa + "-" + s.Rol.ToString() + "-" + s.Anio.ToString() + ", ID Tribunal: " + s.Tribunal.ToString());
                        dao.Causa.ActualizarRolHTMLFecha(s.IdCausa, s.IdCuaderno, "P");
                        resultado = true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Rol :" + s.TipoCausa + "-" + s.Rol.ToString() + "-" + s.Anio.ToString() + ", ID Tribunal: " + s.Tribunal.ToString());
                        Dimol.dao.Funciones.InsertarError("HTML con error al extraer los datos", "Rol: " + s.TipoCausa + "-" + s.Rol.ToString() + "-" + s.Anio.ToString(), "Procesa Rol HTML", s.Anio);
                        dao.Causa.ActualizarRolHTMLFecha(s.IdCausa, s.IdCuaderno, "M");
                        resultado = false;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Rol :" + s.TipoCausa + "-" + s.Rol.ToString() + "-" + s.Anio.ToString() + ", ID Tribunal: " + s.Tribunal.ToString());
                    Dimol.dao.Funciones.InsertarError("HTML con error", "Rol: " + s.TipoCausa + "-" + s.Rol.ToString() + "-" + s.Anio.ToString(), "Procesa Rol HTML", s.Anio);
                    dao.Causa.ActualizarRolHTMLFecha(s.IdCausa, s.IdCuaderno, "M");
                    resultado = false;
                }
            }
            else
            {
                resultado = false;
            }
            return resultado;
            //Console.WriteLine("Fin carga roles desde HTML");
            //Console.ReadLine();
        }

        public static void ProcesarRolInternoHTML(RolActualizar s, List<DatoTipo> lstTipoCuaderno)
        {
            HtmlDocument html = new HtmlDocument();
            ScannerCausas objCausa = new ScannerCausas();
            string format = "dd/MM/yyyy";
            string fecha;
            try
            {
                html.LoadHtml(s.HTML);
                try
                {
                    // Grabar rol
                    objCausa.IdCausa = s.IdCausa;
                    objCausa.IdCuaderno = s.IdCuaderno;
                    objCausa.TipoCausa = s.TipoCausa;
                    objCausa.RolCausa = s.Rol;
                    objCausa.RolAnio = s.Anio;
                    objCausa.CodigoTribunal = s.IdTribunal;
                    objCausa.FechaUltHistorial = s.FechaUltHistorial;
                    string[] ruta = new string[] { "", "" };

                    HtmlNodeCollection rowsTextoDemanda = html.DocumentNode.SelectNodes("//td[contains(text(),'Texto Demanda')]");
                    if (rowsTextoDemanda != null)
                    {
                        foreach (HtmlNode td in rowsTextoDemanda)
                        {
                            foreach (HtmlNode node in td.SelectNodes("img"))
                            {
                                if (node.GetAttributeValue("src", "").Contains("/CIVILPORWEB/img/Comun/DocVacio.JPG"))
                                {
                                    ruta = new string[] { "", "" };
                                }
                                else if (node.GetAttributeValue("src", "").Contains("/CIVILPORWEB/img/Comun/edit.jpg"))
                                {
                                    ruta = node.GetAttributeValue("onclick", "").Split('\'');
                                }
                                else if (node.GetAttributeValue("src", "").Contains("/CIVILPORWEB/img/Comun/generarpdf.gif"))
                                {
                                    ruta = node.GetAttributeValue("onclick", "").Split('\'');
                                }
                            }
                        }
                    }

                    HtmlNodeCollection rowsFIng = html.DocumentNode.SelectNodes("//*[text()[contains(., 'F. Ing :')]]");
                    if (rowsFIng != null)
                    {
                        foreach (HtmlNode td in rowsFIng)
                        {
                            fecha = td.InnerText.Replace("F. Ing :", "").Replace(" \r\n\t\t", "");
                            if ((!string.IsNullOrEmpty(fecha.Trim())) && (fecha.Trim() != "null"))
                            {
                                objCausa.FechaIngreso = DateTime.ParseExact(fecha.Trim(), format, CultureInfo.InvariantCulture);
                            }
                            else
                            {
                                objCausa.FechaIngreso = new DateTime();
                            }
                        }
                    }
                    else
                    {
                        objCausa.FechaIngreso = new DateTime();
                    }

                    if (!string.IsNullOrEmpty(ruta[1]))
                    {
                        objCausa.RutaDemanda = "http://civil.poderjudicial.cl" + ruta[1];
                    }
                    else
                    {
                        objCausa.RutaDemanda = null;
                    }

                    dao.Causa.InsertarRolHTML(objCausa.IdCausa, objCausa.TipoCausa, objCausa.RolCausa, objCausa.RolAnio, objCausa.CodigoTribunal, objCausa.RutaDemanda, objCausa.FechaIngreso);

                    // Grabar cuadernos
                    HtmlNode.ElementsFlags.Remove("option");
                    string idCuaderno = "", descCuaderno = "";
                    long test = 0;
                    int tipoCuaderno = 0;

                    HtmlNodeCollection rowsCuadernos = html.DocumentNode.SelectNodes("//select[@name='CRR_Cuaderno']");
                    if (rowsCuadernos != null)
                    {
                        foreach (HtmlNode cuaderno in html.DocumentNode.SelectNodes("//select[@name='CRR_Cuaderno']"))
                        {
                            foreach (HtmlNode option in cuaderno.SelectNodes("option"))
                            {
                                idCuaderno = option.GetAttributeValue("value", "");
                                if (string.IsNullOrEmpty(option.InnerHtml))
                                {
                                    descCuaderno = option.NextSibling.InnerHtml;
                                }
                                else
                                {
                                    descCuaderno = option.InnerHtml;
                                }

                                if (Int64.TryParse(idCuaderno, out test) && !string.IsNullOrEmpty(descCuaderno))
                                {

                                    if (lstTipoCuaderno.Find(x => x.Nombre == descCuaderno) == null)
                                    {
                                        dao.Causa.InsertarCuadernoHTML(objCausa.IdCausa, Int32.Parse(idCuaderno), descCuaderno, idCuaderno == s.IdCuaderno.ToString() ? "N" : "S");
                                    }
                                    else
                                    {
                                        tipoCuaderno = lstTipoCuaderno.Find(x => x.Nombre == descCuaderno).Id;
                                        dao.Causa.InsertarCuadernoHTMLFull(objCausa.IdCausa, Int32.Parse(idCuaderno), descCuaderno, idCuaderno == s.IdCuaderno.ToString() ? "N" : "S", tipoCuaderno);
                                    }

                                }

                            }
                        }
                    }


                    // Grabar historial
                    HtmlNode divHistorial = html.DocumentNode.SelectSingleNode("//div[@id='Historia']");
                    HtmlNode tablaHistorial = divHistorial;
                    if (divHistorial != null)
                    {

                        foreach (HtmlNode chNode in divHistorial.ChildNodes)
                        {
                            if (chNode.Name == "table")
                            {
                                if (chNode.Attributes["bgcolor"] != null && chNode.Attributes["bgcolor"].Value == "white")
                                {
                                    tablaHistorial = chNode;
                                }
                            }
                        }
                    }

                    //HtmlNode divEscritos = html.DocumentNode.SelectSingleNode("//div[@id='Escritos']");
                    //HtmlNode tablaEscritos = divEscritos;
                    //foreach (HtmlNode chNode in divEscritos.ChildNodes)
                    //{
                    //    if (chNode.Name == "table")
                    //    {
                    //        if (chNode.Attributes["width"] != null && chNode.Attributes["width"].Value == "620")
                    //        {
                    //            tablaEscritos = chNode;
                    //        }
                    //    }
                    //}

                    //if (tablaEscritos.ChildNodes.Count > 0)
                    //{
                    //    //InsertarTipoCuaderno(objCausa.IdCausa, objCausa.IdCuaderno,, "S");
                    //}
                    int i = 1, j = 0;
                    string[] fechaTramite = { };
                    string fechaFinal = "";

                    //List<List<string>> table = divHistorial.SelectSingleNode("./table[2]")
                    //.Descendants("tr")
                    //.Skip(1)
                    //.Where(tr => tr.Elements("td").Count() > 1)
                    //.Select(tr => tr.Elements("td").Select(td => td.InnerText.Trim()).ToList())
                    //.ToList();
                    if (tablaHistorial != null)
                    {
                        HtmlNodeCollection rows = tablaHistorial.SelectNodes("tbody/tr");
                        if (rows != null)
                        {
                            foreach (HtmlNode row in rows)
                            {
                                HtmlNodeCollection cells = row.SelectNodes("td");
                                TablaHistorial tbl = new TablaHistorial();
                                foreach (HtmlNode cell in cells)
                                {
                                    switch (i)
                                    {
                                        case 1:
                                            tbl.Folio = cell.InnerText.Replace("\t", "").Replace("\r", "").Replace("\n", "").Trim();
                                            break;
                                        case 2:
                                            ruta = new string[] { "", "" };
                                            if (cell.SelectSingleNode("img[@src='/CIVILPORWEB/img/Comun/generarpdf.gif']") != null)
                                            {
                                                HtmlNode btnPDF = cell.SelectSingleNode("img[@src='/CIVILPORWEB/img/Comun/generarpdf.gif']");
                                                ruta = btnPDF.Attributes["onclick"].Value.Split('\'');
                                            }
                                            else if (cell.SelectSingleNode("img[@src='/CIVILPORWEB/img/Comun/edit.jpg']") != null)
                                            {
                                                HtmlNode btnWord = cell.SelectSingleNode("img[@src='/CIVILPORWEB/img/Comun/edit.jpg']");
                                                ruta = btnWord.Attributes["onclick"].Value.Split('\'');
                                            }
                                            if (!string.IsNullOrEmpty(ruta[1]))
                                            {
                                                tbl.RutaDocumento = "http://civil.poderjudicial.cl" + ruta[1];
                                            }
                                            break;
                                        case 3:
                                            //tbl.Etapa = cell.InnerText.Replace("\t", "").Replace("\r", "").Replace("\n", "").Trim();
                                            break;
                                        case 4:
                                            tbl.Etapa = cell.InnerText.Replace("\t", "").Replace("\r", "").Replace("\n", "").Trim();
                                            break;
                                        case 5:
                                            tbl.Tramite = cell.InnerText.Replace("\t", "").Replace("\r", "").Replace("\n", "").Trim();
                                            break;
                                        case 6:
                                            tbl.DescTramite = cell.InnerText.Replace("\t", "").Replace("\r", "").Replace("\n", "").Trim();
                                            break;
                                        case 7:
                                            fechaTramite = cell.InnerText.Replace("\t", "").Replace("\r", "").Replace("\n", "").Trim().Split('(');
                                            if (fechaTramite.Length > 1)
                                            {
                                                fechaFinal = fechaTramite[1].Replace(")", "");
                                                //Dimol.dao.Funciones.InsertarError("HTML con error al extraer los datos", cell.InnerText, CultureInfo.CurrentCulture.DisplayName, s.Anio);
                                            }
                                            else
                                            {
                                                fechaFinal = fechaTramite[0];
                                            }

                                            if (fechaFinal != string.Empty)
                                            {
                                                tbl.FechaTramite = DateTime.ParseExact(fechaFinal, format, null);
                                            }
                                            else
                                            {
                                                tbl.FechaTramite = new DateTime();
                                            }

                                            break;
                                        case 8:
                                            tbl.Foja = cell.InnerText.Replace("\t", "").Replace("\r", "").Replace("\n", "").Trim();
                                            break;
                                    }
                                    i++;
                                }
                                i = 1;

                                if ((objCausa.FechaUltHistorial <= tbl.FechaTramite) || (tbl.FechaTramite == new DateTime())) //Se maneja esta condición para no romper el ciclo
                                {
                                    j = 0;
                                    dao.Causa.InsertarHistorialPoderJudicial(objCausa.IdCausa, objCausa.IdCuaderno, tbl.Folio, tbl.RutaDocumento, tbl.Etapa, tbl.Tramite, tbl.DescTramite, tbl.FechaTramite, Int32.Parse(tbl.Foja));
                                }
                                else
                                {
                                    if (j > 2)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        j++;
                                    }
                                }
                            }
                        }
                    }

                    Console.WriteLine("Rol :" + s.TipoCausa + "-" + s.Rol.ToString() + "-" + s.Anio.ToString() + ", ID Tribunal: " + s.Tribunal.ToString());
                    dao.Causa.ActualizarRolHTMLFecha(s.IdCausa, s.IdCuaderno, "P");
                }
                catch (Exception ex)
                {
                    Dimol.dao.Funciones.InsertarError("HTML con error al extraer los datos", ex.StackTrace, ex.Message, s.Anio);
                    Console.WriteLine("Rol :" + s.TipoCausa + "-" + s.Rol.ToString() + "-" + s.Anio.ToString() + ", ID Tribunal: " + s.Tribunal.ToString());
                    Dimol.dao.Funciones.InsertarError("HTML con error al extraer los datos", "Rol: " + s.TipoCausa + "-" + s.Rol.ToString() + "-" + s.Anio.ToString(), "Procesa Rol HTML", s.Anio);
                    dao.Causa.ActualizarRolHTMLFecha(s.IdCausa, s.IdCuaderno, "M");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Rol :" + s.TipoCausa + "-" + s.Rol.ToString() + "-" + s.Anio.ToString() + ", ID Tribunal: " + s.Tribunal.ToString());
                Dimol.dao.Funciones.InsertarError("HTML con error", "Rol: " + s.TipoCausa + "-" + s.Rol.ToString() + "-" + s.Anio.ToString(), "Procesa Rol HTML", s.Anio);
                dao.Causa.ActualizarRolHTMLFecha(s.IdCausa, s.IdCuaderno, "M");
            }
        }



        public static void ActualizarRolesInternos(int anio)
        {
            int indice = 0;
            List<RolActualizar> lstRoles = new List<RolActualizar>();
            List<Dimol.dto.Combobox> lstTribunales = Causa.ListarTribunalesScanner();
            foreach (Dimol.dto.Combobox trib in lstTribunales)
            {
                lstRoles = dao.Causa.ListarRolesExternos(anio, Int32.Parse(trib.Value));
                foreach (RolActualizar obj in lstRoles)
                {
                    Console.WriteLine(obj.Rol.ToString() + "-" + obj.IdTribunal);
                    if (indice + 1 < obj.Rol)
                    {
                        Console.WriteLine("Pendiente: " + obj.Rol.ToString() + "-" + obj.IdTribunal);
                    }

                }


            }

            int inicio = 0;
            string htmlRol = "";
            //inicio = (particion - 1) * cantidad;
            //ConsultaCausa obj = new ConsultaCausa();
            //List<RolActualizar> lstRoles = new List<RolActualizar>();
            List<DatoTipo> lstTipoCuaderno = dao.Causa.ListarTipoCuaderno();
            //Dimol.dao.Funciones.InsertarError("Generando lista de roles para actualizar", "Estados: " + estados, "Actualizar Roles Internos", particion);
            // Genero la lista de roles a actualizar
            lstRoles = dao.Interno.ListarRolesInternos();

            foreach (RolActualizar s in lstRoles)
            {
                HtmlDocument html = new HtmlDocument();
                ScannerCausas objCausa = new ScannerCausas();
                string format = "dd/MM/yyyy";
                string fecha;
                try
                {
                    html.LoadHtml(s.HTML);
                    try
                    {
                        // Grabar rol
                        objCausa.IdCausa = s.IdCausa;
                        objCausa.IdCuaderno = s.IdCuaderno;
                        objCausa.TipoCausa = s.TipoCausa;
                        objCausa.RolCausa = s.Rol;
                        objCausa.RolAnio = s.Anio;
                        objCausa.CodigoTribunal = s.IdTribunal;
                        string[] ruta = new string[] { "", "" };
                        foreach (HtmlNode td in html.DocumentNode.SelectNodes("//td[contains(text(),'Texto Demanda')]"))
                        {
                            foreach (HtmlNode node in td.SelectNodes("img"))
                            {
                                if (node.GetAttributeValue("src", "").Contains("/CIVILPORWEB/img/Comun/DocVacio.JPG"))
                                {
                                    ruta = new string[] { "", "" };
                                }
                                else if (node.GetAttributeValue("src", "").Contains("/CIVILPORWEB/img/Comun/edit.jpg"))
                                {
                                    ruta = node.GetAttributeValue("onclick", "").Split('\'');
                                }
                                else if (node.GetAttributeValue("src", "").Contains("/CIVILPORWEB/img/Comun/generarpdf.gif"))
                                {
                                    ruta = node.GetAttributeValue("onclick", "").Split('\'');
                                }
                            }
                        }
                        foreach (HtmlNode td in html.DocumentNode.SelectNodes("//*[text()[contains(., 'F. Ing :')]]"))
                        {
                            fecha = td.InnerText.Replace("F. Ing :", "").Replace(" \r\n\t\t", "");
                            if (!string.IsNullOrEmpty(fecha))
                            {
                                objCausa.FechaIngreso = DateTime.ParseExact(fecha.Trim(), format, CultureInfo.InvariantCulture);
                            }
                            else
                            {
                                objCausa.FechaIngreso = new DateTime();
                            }
                        }
                        if (!string.IsNullOrEmpty(ruta[1]))
                        {
                            objCausa.RutaDemanda = "http://civil.poderjudicial.cl" + ruta[1];
                        }
                        else
                        {
                            objCausa.RutaDemanda = null;
                        }

                        dao.Causa.InsertarRolHTML(objCausa.IdCausa, objCausa.TipoCausa, objCausa.RolCausa, objCausa.RolAnio, objCausa.CodigoTribunal, objCausa.RutaDemanda, objCausa.FechaIngreso);

                        // Grabar cuadernos
                        HtmlNode.ElementsFlags.Remove("option");
                        string idCuaderno = "", descCuaderno = "";
                        long test = 0;
                        int tipoCuaderno = 0;
                        foreach (HtmlNode cuaderno in html.DocumentNode.SelectNodes("//select[@name='CRR_Cuaderno']"))
                        {
                            foreach (HtmlNode option in cuaderno.SelectNodes("option"))
                            {
                                idCuaderno = option.GetAttributeValue("value", "");
                                if (string.IsNullOrEmpty(option.InnerHtml))
                                {
                                    descCuaderno = option.NextSibling.InnerHtml;
                                }
                                else
                                {
                                    descCuaderno = option.InnerHtml;
                                }

                                if (Int64.TryParse(idCuaderno, out test) && !string.IsNullOrEmpty(descCuaderno))
                                {

                                    if (lstTipoCuaderno.Find(x => x.Nombre == descCuaderno) == null)
                                    {
                                        dao.Causa.InsertarCuadernoHTML(objCausa.IdCausa, Int32.Parse(idCuaderno), descCuaderno, idCuaderno == s.IdCuaderno.ToString() ? "N" : "S");
                                    }
                                    else
                                    {
                                        tipoCuaderno = lstTipoCuaderno.Find(x => x.Nombre == descCuaderno).Id;
                                        dao.Causa.InsertarCuadernoHTMLFull(objCausa.IdCausa, Int32.Parse(idCuaderno), descCuaderno, idCuaderno == s.IdCuaderno.ToString() ? "N" : "S", tipoCuaderno);
                                    }

                                }

                            }
                        }

                        // Grabar historial
                        HtmlNode divHistorial = html.DocumentNode.SelectSingleNode("//div[@id='Historia']");
                        HtmlNode tablaHistorial = divHistorial;
                        foreach (HtmlNode chNode in divHistorial.ChildNodes)
                        {
                            if (chNode.Name == "table")
                            {
                                if (chNode.Attributes["bgcolor"] != null && chNode.Attributes["bgcolor"].Value == "white")
                                {
                                    tablaHistorial = chNode;
                                }
                            }
                        }
                        HtmlNode divEscritos = html.DocumentNode.SelectSingleNode("//div[@id='Escritos']");
                        HtmlNode tablaEscritos = divEscritos;
                        foreach (HtmlNode chNode in divEscritos.ChildNodes)
                        {
                            if (chNode.Name == "table")
                            {
                                if (chNode.Attributes["width"] != null && chNode.Attributes["width"].Value == "620")
                                {
                                    tablaEscritos = chNode;
                                }
                            }
                        }

                        //if (tablaEscritos.ChildNodes.Count > 0)
                        //{
                        //    //InsertarTipoCuaderno(objCausa.IdCausa, objCausa.IdCuaderno,, "S");
                        //}
                        int i = 1;
                        HtmlNodeCollection rows = tablaHistorial.SelectNodes("tbody/tr");
                        foreach (HtmlNode row in rows)
                        {
                            HtmlNodeCollection cells = row.SelectNodes("td");
                            TablaHistorial tbl = new TablaHistorial();
                            foreach (HtmlNode cell in cells)
                            {
                                switch (i)
                                {
                                    case 1:
                                        tbl.Folio = cell.InnerText.Replace("\r\n", "").Trim();
                                        break;
                                    case 2:
                                        ruta = new string[] { "", "" };
                                        if (cell.SelectSingleNode("img[@src='/CIVILPORWEB/img/Comun/generarpdf.gif']") != null)
                                        {
                                            HtmlNode btnPDF = cell.SelectSingleNode("img[@src='/CIVILPORWEB/img/Comun/generarpdf.gif']");
                                            ruta = btnPDF.Attributes["onclick"].Value.Split('\'');
                                        }
                                        else if (cell.SelectSingleNode("img[@src='/CIVILPORWEB/img/Comun/edit.jpg']") != null)
                                        {
                                            HtmlNode btnWord = cell.SelectSingleNode("img[@src='/CIVILPORWEB/img/Comun/edit.jpg']");
                                            ruta = btnWord.Attributes["onclick"].Value.Split('\'');
                                        }
                                        if (!string.IsNullOrEmpty(ruta[1]))
                                        {
                                            tbl.RutaDocumento = "http://civil.poderjudicial.cl" + ruta[1];
                                        }
                                        break;
                                    case 3:
                                        tbl.Etapa = cell.InnerText.Replace("\r\n", "").Trim();
                                        break;
                                    case 4:
                                        tbl.Tramite = cell.InnerText.Replace("\r\n", "").Trim();
                                        break;
                                    case 5:
                                        tbl.DescTramite = cell.InnerText.Replace("\r\n", "").Trim();
                                        break;
                                    case 6:
                                        tbl.FechaTramite = DateTime.ParseExact(cell.InnerText.Replace("\r\n", "").Trim(), format, CultureInfo.InvariantCulture);
                                        break;
                                    case 7:
                                        tbl.Foja = cell.InnerText.Replace("\r\n", "").Trim();
                                        break;
                                }
                                i++;
                            }

                            int fer = 5;

                            dao.Causa.InsertarHistorialPoderJudicial(objCausa.IdCausa, objCausa.IdCuaderno, tbl.Folio, tbl.RutaDocumento, tbl.Etapa, tbl.Tramite, tbl.DescTramite, tbl.FechaTramite, Int32.Parse(tbl.Foja));
                            //if (objCausa.FechaUltHistorial < tbl.FechaTramite)
                            //{
                            //objCausa.TablaHistorial.Add(tbl);
                            //InsertarHistorialPoderJudicial(objCausa.IdCausa, objCausa.IdCuaderno, tbl.Folio, tbl.RutaDocumento, tbl.Etapa, tbl.Tramite, tbl.DescTramite, tbl.FechaTramite, Int32.Parse(tbl.Foja));
                            //}
                            //else
                            //{
                            //    break;
                            //}
                        }

                        // Grabar Litigantes
                        //int i = 1;
                        TablaLitigantes tblLitigantes = new TablaLitigantes();
                        foreach (HtmlNode litigantes in html.DocumentNode.SelectNodes("//div[@id='Litigantes']"))
                        {
                            foreach (HtmlNode table in litigantes.SelectNodes("table"))
                            {
                                foreach (HtmlNode row in table.SelectNodes("tbody/tr"))
                                {
                                    i = 1;
                                    tblLitigantes = new TablaLitigantes();
                                    foreach (HtmlNode cell in row.SelectNodes("td"))
                                    {
                                        switch (i)
                                        {
                                            case 1:
                                                tblLitigantes.Participante = cell.InnerText;
                                                break;
                                            case 2:
                                                tblLitigantes.Rut = cell.InnerText;
                                                break;
                                            case 3:
                                                tblLitigantes.Persona = cell.InnerText;
                                                break;
                                            case 4:
                                                tblLitigantes.NombreRazonSocial = cell.InnerText;
                                                break;
                                        }
                                        if (tblLitigantes.Participante == "Participante")
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            i++;
                                        }

                                    }
                                    if (tblLitigantes.Participante != "Participante")
                                    {
                                        dao.Causa.InsertarLitiganteHTML(objCausa.IdCausa, objCausa.IdCuaderno, tblLitigantes.Participante, tblLitigantes.Rut, tblLitigantes.Persona, tblLitigantes.NombreRazonSocial);
                                    }
                                }
                            }
                        }

                        Console.WriteLine("Rol :" + s.TipoCausa + "-" + s.Rol.ToString() + "-" + s.Anio.ToString() + ", ID Tribunal: " + s.IdTribunal.ToString());
                        dao.Causa.ActualizarRolHTMLFecha(s.IdCausa, s.IdCuaderno, "P");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Rol :" + s.TipoCausa + "-" + s.Rol.ToString() + "-" + s.Anio.ToString() + ", ID Tribunal: " + s.IdTribunal.ToString());
                        Dimol.dao.Funciones.InsertarError("HTML con error al extraer los datos", "Rol: " + s.TipoCausa + "-" + s.Rol.ToString() + "-" + s.Anio.ToString(), "Procesa Rol HTML", s.Anio);
                        dao.Causa.ActualizarRolHTMLFecha(s.IdCausa, s.IdCuaderno, "M");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Rol :" + s.TipoCausa + "-" + s.Rol.ToString() + "-" + s.Anio.ToString() + ", ID Tribunal: " + s.IdTribunal.ToString());
                    Dimol.dao.Funciones.InsertarError("HTML con error", "Rol: " + s.TipoCausa + "-" + s.Rol.ToString() + "-" + s.Anio.ToString(), "Procesa Rol HTML", s.Anio);
                    dao.Causa.ActualizarRolHTMLFecha(s.IdCausa, s.IdCuaderno, "M");
                }
            }
            //Dimol.dao.Funciones.InsertarError("Fin de roles internos para actualizar", "Estados: " + estados, "Actualizar Roles Internos", particion);
        }

        #endregion
    }
}
