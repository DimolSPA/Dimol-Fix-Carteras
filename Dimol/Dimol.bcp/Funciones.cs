using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dimol.dao;
using Dimol.dto;
using CYPH;
using System.Xml;
using System.Data.OleDb;
using System.Data;
using System.Configuration;
using System.IO;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Threading;

namespace Dimol.bcp
{
    public class Funciones
    {
        dao.Funciones objFunciones = new dao.Funciones();
        public decimal Configuracion_Num(int clave)
        {
            return objFunciones.ConfiguracionNum(clave);
        }
        public string Configuracion_Str(int clave)
        {
            return objFunciones.ConfiguracionStr(clave);
        }

        public string TraeError(string errNum, int idioma)
        {
            return objFunciones.TraeError(errNum, idioma); 
        }

        public string AppPath()
        {
            return System.AppDomain.CurrentDomain.BaseDirectory.ToString();
        }

        public string Encripta(string password)
        {
            string encrip = "";

            Ucode objUcode = new Ucode();

            encrip = objUcode.Encripta(password);

            return encrip;

        }

        public string Desencripta(string psw_encriptada)
        {
            string result = "";

            Ucode objUcode = new Ucode();

            result = objUcode.Desencripta(psw_encriptada);

            return result;

        }

        public string FechaServer()
        {
            return objFunciones.FechaServer();
        }

        public int ConfiguracionEmpNum(int codemp, int clave)
        {
            return objFunciones.ConfiguracionEmpNum(codemp,clave);
        }

        public string ConfiguracionEmpStr(int codemp, int clave)
        {
            return objFunciones.ConfiguracionEmpStr(codemp, clave);
        }

        public  XmlDocument IndicadoresDiarios()
        {
            return Servicio.IndicadoresDiarios();
        }

        public static DataSet CargarExcel(string rutaArchivo)
        {
            string[] archivo = rutaArchivo.Split('.');
            bcp.Funciones obj = new Funciones();
            string conStr = "";
            if (archivo[archivo.Length-1].ToLower() == "xls")
            {
                conStr = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source='" 
                    + ConfigurationManager.AppSettings["RutaArchivos"] + obj.Configuracion_Str(15) + "\\"+rutaArchivo + "'"
                    + "; Extended Properties ='Excel 8.0;IMEX=1;HDR=No'";
            }
            else if (archivo[archivo.Length - 1].ToLower() == "xlsx")
            {
                conStr = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" 
                    + ConfigurationManager.AppSettings["RutaArchivos"] + obj.Configuracion_Str(15) + "\\"+rutaArchivo
                    + "; Extended Properties ='Excel 12.0 Xml;IMEX=1;HDR=No'";
                  //+"; Extended Properties ='Excel 8.0;IMEX=1;HDR=No'";
            }
            conStr = string.Format(conStr, rutaArchivo);
            OleDbConnection con = new OleDbConnection(conStr);
            
            try
            {
                con.Open();
                DataTable dt = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string sheetname = dt.Rows[0]["TABLE_NAME"].ToString();
                OleDbDataAdapter adp = new OleDbDataAdapter("Select * from [Datos$]", con);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Dispose();
            }
        }
        public static DataSet CargarExcel(string rutaArchivo, string path)
        {
            string[] archivo = rutaArchivo.Split('.');
            bcp.Funciones obj = new Funciones();
            string conStr = "";
            if (archivo[archivo.Length - 1].ToLower() == "xls")
            {
                conStr = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source='"
                    + path  + "\\" + rutaArchivo + "'"
                    + "; Extended Properties ='Excel 8.0;IMEX=1;HDR=No'";
            }
            else if (archivo[archivo.Length - 1].ToLower() == "xlsx")
            {
                conStr = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source="
                    + path  + "\\" + rutaArchivo
                    + "; Extended Properties ='Excel 8.0;IMEX=1;HDR=No'";
            }
            conStr = string.Format(conStr, rutaArchivo);
            OleDbConnection con = new OleDbConnection(conStr);

          
            try
            {
                con.Open();
                DataTable dt = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string sheetname = dt.Rows[0]["TABLE_NAME"].ToString();
                OleDbDataAdapter adp = new OleDbDataAdapter("Select * from [" + sheetname + "]", con);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                con.Dispose();
            }
        }
        public static bool ValidaRut(string rut)
        {
            try
            {
                if (rut.Length > 10)
                {
                    return false;
                }
                rut = rut.Trim().TrimStart('0');
                string dv = rut.Substring(rut.Length - 1, 1).ToUpper();
                int Rut = Int32.Parse(rut.Substring(0, rut.Length - 1));

                int Digito;
                int Contador;
                int Multiplo;
                int Acumulador;
                String RutDigito;

                Contador = 2;
                Acumulador = 0;

                while (Rut != 0)
                {
                    Multiplo = (Rut % 10) * Contador;
                    Acumulador = Acumulador + Multiplo;
                    Rut = Rut / 10;
                    Contador = Contador + 1;
                    if (Contador == 8)
                    {
                        Contador = 2;
                    }
                }

                Digito = 11 - (Acumulador % 11);
                RutDigito = Digito.ToString().Trim();
                if (Digito == 10)
                {
                    RutDigito = "K";
                }
                if (Digito == 11)
                {
                    RutDigito = "0";
                }
                if (RutDigito == dv.ToUpper())
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool CreaCarpetas(string ruta)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(ruta);
                if( !dir.Exists){
                    Directory.CreateDirectory(ruta);
                }
            } catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public static dto.Indicadores DescargaContenidoPagina(string url) 
        {

            string indicadores = "";
            Indicadores obj = new Indicadores();
            HtmlDocument html = new HtmlDocument();
            try
            { 
                indicadores = Servicio.DescargaPagina(url); 
            }
            catch (Exception ex)
            {
                Exception exx = new Exception("El servicio de datos no esta disponible.",ex);
                throw exx; 
            }

            try
            {
                // Example of loading your HTML into an HtmlDocument object
                
                html.LoadHtml(indicadores);
            }
            catch (Exception ex)
            {
                Exception exx = new Exception("No se pudo crear el documento HTML.", ex);
                throw exx;
            }

            try
            {
                foreach (HtmlNode td in html.DocumentNode.SelectNodes("//*[text()[contains(., 'UF')]]"))
                {
                    HtmlNode tr = td.ParentNode.ParentNode.ParentNode;
                    foreach (HtmlNode newTd in tr.ChildNodes)
                    {
                        if (newTd.InnerText.Contains("$"))
                        {
                            obj.UF = decimal.Parse(newTd.InnerText.Replace("$", "").Replace("&nbsp;", "").Replace(".", "").Replace(",", ".").Trim());
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                Exception exx = new Exception("UF no esta disponible.", ex);
                throw exx;
            }
            try
            {
                foreach (HtmlNode td in html.DocumentNode.SelectNodes("//*[text()[contains(., 'UTM')]]"))
                {
                    HtmlNode tr = td.ParentNode.ParentNode.ParentNode;
                    foreach (HtmlNode newTd in tr.ChildNodes)
                    {
                        if (newTd.InnerText.Contains("$"))
                        {
                            obj.UTM = decimal.Parse(newTd.InnerText.Replace("$", "").Replace("&nbsp;", "").Replace(".", "").Replace(",", ".").Trim());
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                Exception exx = new Exception("UTM no esta disponible.", ex);
                throw exx;
            }

            try
            {
                foreach (HtmlNode td in html.DocumentNode.SelectNodes("//*[text()[contains(., 'IPC')]]"))
                {
                    HtmlNode tr = td.ParentNode.ParentNode.ParentNode;
                    foreach (HtmlNode newTd in tr.ChildNodes)
                    {
                        if (newTd.InnerText.Contains("%"))
                        {
                            obj.IPC = decimal.Parse(newTd.InnerText.Replace("%", "").Replace("&nbsp;", "").Replace(".", "").Replace(",", ".").Trim());
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                Exception exx = new Exception("IPC no esta disponible.", ex);
                throw exx;
            }

            try
            {
                foreach (HtmlNode td in html.DocumentNode.SelectNodes("//*[text()[contains(., 'Observado')]]"))
                {
                    HtmlNode tr = td.ParentNode.ParentNode.ParentNode;
                    foreach (HtmlNode newTd in tr.ChildNodes)
                    {
                        if (newTd.InnerText.Contains("$"))
                        {
                            obj.DolarObservado = decimal.Parse(newTd.InnerText.Replace("$", "").Replace("&nbsp;", "").Replace(".", "").Replace(",", ".").Trim());
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                Exception exx = new Exception("Dolar no esta disponible.", ex);
                throw exx;
            }
            return obj;
        }

        public static dto.Indicadores DescargaContenidoPaginaBC(string url)
        {
            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
            string indicadores = "";
            Indicadores obj = new Indicadores();
            HtmlDocument html = new HtmlDocument();
            try
            {
                indicadores = Servicio.DescargaPagina(url);
            }
            catch (Exception ex)
            {
                Exception exx = new Exception("El servicio de datos no esta disponible.", ex);
                throw exx;
            }

            try
            {
                // Example of loading your HTML into an HtmlDocument object

                html.LoadHtml(indicadores);
            }
            catch (Exception ex)
            {
                Exception exx = new Exception("No se pudo crear el documento HTML.", ex);
                throw exx;
            }

            try
            {
                foreach (HtmlNode td in html.DocumentNode.SelectNodes("//*[text()[contains(., '(UF)')]]"))
                {
                    HtmlNode tr = td.ParentNode.ParentNode;
                    foreach (HtmlNode newTd in tr.ChildNodes)
                    {
                        if (newTd.Attributes.Count > 0)
                        {
                            foreach (HtmlAttribute att in newTd.Attributes)
                            {
                                if (att.Value =="valor")
                                {
                                    foreach (HtmlNode newChild in newTd.ChildNodes)
                                    {
                                        if (newChild.Name == "label")
                                        {
                                            obj.UF = currentCulture.ToString().Contains("en-") ? decimal.Parse(newChild.InnerText.Replace(".", "").Replace(",", ".").Trim()) : decimal.Parse(newChild.InnerText);
                                        }
                                    }
                                }
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Exception exx = new Exception("UF no esta disponible.", ex);
                throw exx;
            }
            try
            {
                foreach (HtmlNode td in html.DocumentNode.SelectNodes("//*[text()[contains(., 'lar observado')]]"))
                {
                    HtmlNode tr = td.ParentNode.ParentNode;
                    foreach (HtmlNode newTd in tr.ChildNodes)
                    {
                        if (newTd.Attributes.Count > 0)
                        {
                            foreach (HtmlAttribute att in newTd.Attributes)
                            {
                                if (att.Value == "valor")
                                {
                                    foreach (HtmlNode newChild in newTd.ChildNodes)
                                    {
                                        if (newChild.Name == "label")
                                        {
                                            obj.DolarObservado = currentCulture.ToString().Contains("en-") ? decimal.Parse(newChild.InnerText.Replace(".", "").Replace(",", ".").Trim()) : decimal.Parse(newChild.InnerText);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Exception exx = new Exception("Dolar no esta disponible.", ex);
                throw exx;
            }
            try
            {
                foreach (HtmlNode td in html.DocumentNode.SelectNodes("//*[text()[contains(., 'UTM')]]"))
                {
                    HtmlNode tr = td.ParentNode.ParentNode.ParentNode;
                    foreach (HtmlNode newTd in tr.ChildNodes)
                    {
                        if (newTd.InnerText.Contains("$"))
                        {
                            obj.UTM = currentCulture.ToString().Contains("en-") ? decimal.Parse(newTd.InnerText.Replace("$", "").Replace("&nbsp;", "").Replace(".", "").Replace(",", ".").Trim()) : decimal.Parse(newTd.InnerText);
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                Exception exx = new Exception("UTM no esta disponible.", ex);
                throw exx;
            }

            try
            {
                foreach (HtmlNode td in html.DocumentNode.SelectNodes("//*[text()[contains(., 'IPC')]]"))
                {
                    HtmlNode tr = td.ParentNode.ParentNode.ParentNode;
                    foreach (HtmlNode newTd in tr.ChildNodes)
                    {
                        if (newTd.InnerText.Contains("%"))
                        {
                            obj.IPC = currentCulture.ToString().Contains("en-") ? decimal.Parse(newTd.InnerText.Replace("%", "").Replace("&nbsp;", "").Replace(".", "").Replace(",", ".").Trim()) : decimal.Parse(newTd.InnerText);
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                Exception exx = new Exception("IPC no esta disponible.", ex);
                throw exx;
            }

            
            return obj;
        }

        public static decimal DescargaUTMBC(string url)
        {
            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
            string indicadores = "";
            int anio = DateTime.Now.Year;
            int mes = DateTime.Now.Month;
            string strMes = bcp.Funciones.nombreMeses(mes);
            HtmlDocument html = new HtmlDocument();
            decimal utm = 0;
            try
            {
                indicadores = Servicio.DescargaPagina(url);
            }
            catch (Exception ex)
            {
                Exception exx = new Exception("El servicio de datos no esta disponible.", ex);
                throw exx;
            }

            try
            {
                // Example of loading your HTML into an HtmlDocument object

                html.LoadHtml(indicadores);
            }
            catch (Exception ex)
            {
                Exception exx = new Exception("No se pudo crear el documento HTML.", ex);
                throw exx;
            }

            try
            {
                foreach (HtmlNode td in html.DocumentNode.SelectNodes("//*[text()[contains(., '" + anio.ToString() + "')]]"))
                {
                    HtmlNode tr = td.ParentNode;
                    foreach (HtmlNode newTd in tr.ChildNodes)
                    {
                        if (newTd.ChildNodes.Count > 0)
                        {
                            foreach (HtmlNode newChild in newTd.ChildNodes)
                            {
                                foreach (HtmlAttribute att in newChild.Attributes)
                                {
                                    if (att.Name == "id" && att.Value.Contains(strMes))
                                    {

                                        utm = currentCulture.ToString().Contains("en-") ? decimal.Parse(newChild.InnerText.Replace(".", "").Replace(",", ".").Trim()) : decimal.Parse(newTd.InnerText);
                                        return utm;
                                    }
                                }
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Exception exx = new Exception("UTM no esta disponible.", ex);
                throw exx;
            }
          

            return utm;
        }

        public static string formatearRut(string rut)
        {
            int cont = 0;
            string format;
            if (rut.Length == 0)
            {
                return "";
            }
            else
            {
                rut = rut.Replace(".", "");
                rut = rut.Replace("-", "");
                format = "-" + rut.Substring(rut.Length - 1);
                for (int i = rut.Length - 2; i >= 0; i--)
                {
                    format = rut.Substring(i, 1) + format;
                    cont++;
                    if (cont == 3 && i != 0)
                    {
                        format = "." + format;
                        cont = 0;
                    }
                }
                return format;
            }
        }

        public static string nombreMeses(int mes)
        {
            CultureInfo cultura = new CultureInfo("es-cl");
            var qry = from m in cultura.DateTimeFormat.MonthNames
                            select cultura.TextInfo.ToTitleCase(m);
            int i=0;
            foreach (string strMes in qry)
            {
                if(i == mes-1)
                {
                    return strMes;
                }

                i++;
            }
            return "";
        }

        public static void TraeDolarUFHoy(int codemp, dto.Indicadores obj)
        {
            dao.Funciones.TraeDolarUFHoy(codemp, obj);
        }

        public static void InsertarError( string mensaje, string stacktrace, string pagina, int usuario)
        {
             dao.Funciones.InsertarError( mensaje, stacktrace, pagina, usuario);
        }

        public string TraeEtiqueta(string clave, int idioma)
        {
            dao.Funciones obj = new dao.Funciones();
            return obj.TraeEtiqueta(clave, idioma);
        }

        public static int InsertaMonedaValor(int codemp, int codmon, DateTime fecha, decimal valorMoneda)
        {
            return dao.Funciones.InsertaMonedaValor(codemp, codmon, fecha, valorMoneda);
        }

        public static string Digito(int rut)
        {
            int suma = 0;
            int multiplicador = 1;
            while (rut != 0)
            {
                multiplicador++;
                if (multiplicador == 8)
                    multiplicador = 2;
                suma += (rut % 10) * multiplicador;
                rut = rut / 10;
            }
            suma = 11 - (suma % 11);
            if (suma == 11)
                return "0";
            else if (suma == 10)
                return "K";
            else
                return suma.ToString();
        }
    }
}
