using HtmlAgilityPack;
using SII.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SII.bcp
{
    public class Procesar
    {
        public static List<dto.Status> ListarRutporProcesar(string estado)
        {
            return dao.Status.ListarRutporProcesar(estado);
        }


        public static void ProcesarRutHTML(dto.Status s, List<SII.dto.Combobox> lstTipoActividad, List<SII.dto.Combobox> lstTipoDocumento)
        {
            HtmlDocument html = new HtmlDocument();
            //ScannerCausas objCausa = new ScannerCausas();
            string format = "dd/MM/yyyy";
            string fecha;
            DateTime fechaConsulta = DateTime.Now;
            //List<SII.dto.Combobox> lstTipoActividad = dao.Status.ListarActividadEconomica();
            SII.dto.Combobox tipoActividad = new Combobox();
            SII.dto.Combobox tipoDocumento = new Combobox();
            Cabecera objCab = new Cabecera();
            objCab.IdRut = s.IdRut;
            objCab.Ctcid = s.Ctcid;
            objCab.Rut = s.Rut;
            objCab.DV = s.DigitoVerificador;
            objCab.Fecha = fechaConsulta;
            try
            {
                if (!s.Html.Contains("no existe en las Bases de Datos del Servicio"))
                {
                    objCab.Registrado = "SI";
                    if (!s.Html.Contains("Contribuyente presenta Inicio de Actividades: NO"))
                    {
                        objCab.InicioActividades = "SI";
                        html.LoadHtml(s.Html);
                        try
                        {
                            string documento = "",  anio = "", nombre = "";
                            int i = 1, testAnio = 0;
                            // extrae nombre y rut
                            bool recuperado = false;

                            HtmlNode contenedor = html.DocumentNode.SelectSingleNode("//html/body/div");
                            HtmlNodeCollection spanCollection = html.DocumentNode.SelectNodes("//span");

                            foreach (HtmlNode span in spanCollection)
                            {
                                //fecha consulta
                                if (span.InnerText.Contains("Fecha de realizaci"))
                                {
                                    fecha = span.InnerText.Split(new char[] { ':' }, 2)[1].Replace("hrs\n", "");
                                    if (!string.IsNullOrEmpty(fecha))
                                    {
                                        objCab.FechaConsulta = DateTime.ParseExact(fecha.Trim(), "dd-MM-yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                                    }
                                    else
                                    {
                                        objCab.FechaConsulta = new DateTime();
                                    }
                                }
                                //HtmlNode inicio = div.NextSibling.NextSibling.NextSibling;
                                //Inicio de Actividades
                                if (span.InnerText.Contains("Contribuyente presenta Inicio de Actividades"))
                                {
                                    objCab.InicioActividades = span.InnerText.Split(':')[1].Trim();
                                }
                                //inicio = inicio.NextSibling.NextSibling;
                                //Fecha Inicio de Actividades
                                if (span.InnerText.Contains("Fecha de Inicio de Actividades"))
                                {
                                    fecha = span.InnerText.Split(':')[1].Replace("hrs", "");
                                    if (!string.IsNullOrEmpty(fecha))
                                    {
                                        objCab.FechaInicioActividades = DateTime.ParseExact(fecha.Trim(), "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                                    }
                                    else
                                    {
                                        objCab.FechaInicioActividades = new DateTime();
                                    }
                                }
                                //inicio = inicio.NextSibling.NextSibling;
                                //impuestos en moneda extranjera
                                if (span.InnerText.Contains("impuestos en moneda extranjera"))
                                {
                                    objCab.ImpuestoMonedaExtranjera = span.InnerText.Split(':')[1].Trim();
                                }
                                //inicio = inicio.NextSibling.NextSibling;
                                //Contribuyente es EMPRESA DE MENOR TAMANO PRO-PYME
                                if (span.InnerText.Contains("Contribuyente es EMPRESA DE MENOR TAMA"))
                                {
                                    objCab.MenorProPyme = span.InnerText.Split(':')[1].Trim();
                                }
                                //Emision
                                if (span.Attributes["style"].Value=="font-size:11pt;color:red;")
                                {
                                    objCab.Emision = span.InnerText.Trim();
                                }
                                if ( span.Attributes["style"].Value == "font-size:9pt;color:red;")
                                {
                                    objCab.Emision = objCab.Emision + "\n" + span.InnerText.Trim();
                                }
                            }

                            foreach (HtmlNode div in contenedor.SelectNodes("div"))
                            {
                                // nombre o razon social
                                if (div.InnerText.Contains("Nombre o Raz"))
                                {
                                    objCab.NombreRazonSocial = div.NextSibling.InnerText.Trim();
                                }
                                // rut
                                if (div.InnerText.Contains("RUT Contribuyente"))
                                {
                                    string strRut = div.NextSibling.InnerText.Trim();
                                    objCab.Rut = Int32.Parse(strRut.Split('-')[0]);
                                    objCab.DV = strRut.Split('-')[1];
                                }
                                // observaciones
                                if (div.InnerText.Contains("Observaci"))
                                {
                                    objCab.Observacion = div.NextSibling.InnerText.Trim();
                                }
                            }

                            // extrae actividad economica
                            string actividad = "", categoria = "", iva= "";
                            ActividadEconomica objAct = new ActividadEconomica();
                            HtmlNode tblActividad = html.DocumentNode.SelectSingleNode("//table[@width='750']");
                            HtmlNodeCollection rowsAct = tblActividad.ChildNodes;
                            foreach (HtmlNode row in rowsAct)
                            {
                                i = 1;
                                if (row.Name != "#text")
                                {
                                    HtmlNodeCollection cells = row.SelectNodes("td");
                                    foreach (HtmlNode cell in cells)
                                    {
                                        switch (i)
                                        {
                                            case 1:
                                                actividad = cell.InnerText.Replace("\t", "").Replace("\r", "").Replace("\n", "").Trim();
                                                break;
                                            case 2:
                                                objAct.CodigoActividad = cell.InnerText.Replace("\t", "").Replace("\r", "").Replace("\n", "").Trim();
                                                break;
                                            case 3:
                                                categoria = cell.InnerText.Replace("\t", "").Replace("\r", "").Replace("\n", "").Trim();
                                                break;
                                            case 4:
                                                iva = cell.InnerText.Replace("\t", "").Replace("\r", "").Replace("\n", "").Trim();
                                                break;
                                        }
                                        i++;
                                    }
                                    int e=0;
                                    if (Int32.TryParse(objAct.CodigoActividad, out e))
                                    {
                                        tipoActividad = lstTipoActividad.Find(x => x.Value.Contains(objAct.CodigoActividad));
                                        if (tipoActividad == null)
                                        {
                                            dao.Status.InsertarTipoActividadEconomica(objAct.CodigoActividad, actividad, iva, categoria == "Primera" ? 1 : 2, "SI");
                                            lstTipoActividad = dao.Status.ListarActividadEconomica();
                                        }
                                        objAct.IdRut = s.IdRut;
                                        objAct.Ctcid = s.Ctcid;
                                        objAct.FechaConsulta = fechaConsulta;
                                        dao.Status.InsertarActividadEconomica(objAct);
                                    }
                                }

                            }

                            try
                            {
                                // extrae timbraje 
                                HtmlNode tblTimbraje = html.DocumentNode.SelectSingleNode("//table[@width='590']");
                                HtmlNodeCollection rows = tblTimbraje.ChildNodes;
                                foreach (HtmlNode row in rows)
                                {
                                    i = 1;
                                    if (row.Name != "#text")
                                    {
                                        HtmlNodeCollection cells = row.SelectNodes("td");
                                        foreach (HtmlNode cell in cells)
                                        {
                                            switch (i)
                                            {
                                                case 1:
                                                    documento = cell.InnerText.Replace("\t", "").Replace("\r", "").Replace("\n", "").Trim();
                                                    break;
                                                case 2:
                                                    anio = cell.InnerText;
                                                    break;
                                            }
                                            i++;
                                        }
                                        tipoDocumento = lstTipoDocumento.Find(x => x.Text == documento);
                                        if (tipoDocumento == null)
                                        {
                                            dao.Status.InsertarTipoDocumento(documento);
                                            lstTipoDocumento = dao.Status.ListarTipoDocumento();
                                        }
                                        tipoDocumento = lstTipoDocumento.Find(x => x.Text == documento);
                                        if (tipoDocumento != null && Int32.TryParse(anio, out testAnio))
                                        {
                                            dao.Status.InsertarTimbraje(s.Ctcid, Int32.Parse(tipoDocumento.Value), testAnio, fechaConsulta);
                                        }
                                    }
                                }
                            } catch
                            {
                                Console.WriteLine("Rut sin timbraje:" + s.Rut);
                                dao.Status.MarcarRutLeido(s.IdRut, "ST");
                            }
                            Console.WriteLine("Rut:" + s.Rut);
                            int exito = dao.Status.InsertarCabecera(objCab);
                            if (exito > 0)
                            {
                                dao.Status.MarcarRutLeido(s.IdRut, "P");
                            }
                        }
                        catch (Exception ex)
                        {
                            //Dimol.dao.Funciones.InsertarError("HTML con error al extraer los datos", ex.StackTrace, ex.Message, s.Anio);
                            Console.WriteLine("Error Rut :" + s.Rut);
                            dao.Status.MarcarRutLeido(s.IdRut, "M");
                            //Dimol.dao.Funciones.InsertarError("HTML con error al extraer los datos", "Rol: " + s.TipoCausa + "-" + s.Rol.ToString() + "-" + s.Anio.ToString(), "Procesa Rol HTML", s.Anio);
                            //dao.Causa.ActualizarRolHTMLFecha(s.IdCausa, s.IdCuaderno, "M");
                        }
                    }
                    else
                    {
                        objCab.InicioActividades = "NO";
                        html.LoadHtml(s.Html);
                        try
                        {
                            string documento = "", anio = "", nombre = "";
                            int i = 1, testAnio = 0;
                            // extrae nombre y rut
                            bool recuperado = false;

                            HtmlNode contenedor = html.DocumentNode.SelectSingleNode("//html/body/div");
                            HtmlNodeCollection spanCollection = html.DocumentNode.SelectNodes("//span");

                            foreach (HtmlNode span in spanCollection)
                            {
                                //fecha consulta
                                if (span.InnerText.Contains("Fecha de realizaci"))
                                {
                                    fecha = span.InnerText.Split(new char[] { ':' }, 2)[1].Replace("hrs\n", "");
                                    if (!string.IsNullOrEmpty(fecha))
                                    {
                                        objCab.FechaConsulta = DateTime.ParseExact(fecha.Trim(), "dd-MM-yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                                    }
                                    else
                                    {
                                        objCab.FechaConsulta = new DateTime();
                                    }
                                }
                                //HtmlNode inicio = div.NextSibling.NextSibling.NextSibling;
                                //Inicio de Actividades
                                if (span.InnerText.Contains("Contribuyente presenta Inicio de Actividades"))
                                {
                                    objCab.InicioActividades = span.InnerText.Split(':')[1].Trim();
                                }
                                //inicio = inicio.NextSibling.NextSibling;
                                //Fecha Inicio de Actividades
                                if (span.InnerText.Contains("Fecha de Inicio de Actividades"))
                                {
                                    fecha = span.InnerText.Split(':')[1].Replace("hrs", "");
                                    if (!string.IsNullOrEmpty(fecha))
                                    {
                                        objCab.FechaInicioActividades = DateTime.ParseExact(fecha.Trim(), "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                                    }
                                    else
                                    {
                                        objCab.FechaInicioActividades = new DateTime();
                                    }
                                }
                                //inicio = inicio.NextSibling.NextSibling;
                                //impuestos en moneda extranjera
                                if (span.InnerText.Contains("impuestos en moneda extranjera"))
                                {
                                    objCab.ImpuestoMonedaExtranjera = span.InnerText.Split(':')[1].Trim();
                                }
                                //inicio = inicio.NextSibling.NextSibling;
                                //Contribuyente es EMPRESA DE MENOR TAMANO PRO-PYME
                                if (span.InnerText.Contains("Contribuyente es EMPRESA DE MENOR TAMA"))
                                {
                                    objCab.MenorProPyme = span.InnerText.Split(':')[1].Trim();
                                }
                                //Emision
                                if (span.Attributes["style"].Value == "font-size:11pt;color:red;")
                                {
                                    objCab.Emision = span.InnerText.Trim();
                                }
                                if (span.Attributes["style"].Value == "font-size:9pt;color:red;")
                                {
                                    objCab.Emision = objCab.Emision + "\n" + span.InnerText.Trim();
                                }
                            }

                            foreach (HtmlNode div in contenedor.SelectNodes("div"))
                            {
                                // nombre o razon social
                                if (div.InnerText.Contains("Nombre o Raz"))
                                {
                                    objCab.NombreRazonSocial = div.NextSibling.InnerText.Trim();
                                }
                                // rut
                                if (div.InnerText.Contains("RUT Contribuyente"))
                                {
                                    string strRut = div.NextSibling.InnerText.Trim();
                                    objCab.Rut = Int32.Parse(strRut.Split('-')[0]);
                                    objCab.DV = strRut.Split('-')[1];
                                }
                                // observaciones
                                if (div.InnerText.Contains("Observaci"))
                                {
                                    objCab.Observacion = div.NextSibling.InnerText.Trim();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            //Dimol.dao.Funciones.InsertarError("HTML con error al extraer los datos", ex.StackTrace, ex.Message, s.Anio);
                            Console.WriteLine("Error Rut :" + s.Rut);
                            dao.Status.MarcarRutLeido(s.IdRut, "M");
                            //Dimol.dao.Funciones.InsertarError("HTML con error al extraer los datos", "Rol: " + s.TipoCausa + "-" + s.Rol.ToString() + "-" + s.Anio.ToString(), "Procesa Rol HTML", s.Anio);
                            //dao.Causa.ActualizarRolHTMLFecha(s.IdCausa, s.IdCuaderno, "M");
                        }

                        dao.Status.InsertarCabecera(objCab);
                        Console.WriteLine("Rut sin inicio de actividades:" + s.Rut);
                        dao.Status.MarcarRutLeido(s.IdRut, "SIA");
                    }
                }
                else
                {
                    objCab.Registrado = "NO";
                    dao.Status.InsertarCabecera(objCab);
                    Console.WriteLine("Rut no esta registrado en SII:" + s.Rut);
                    dao.Status.MarcarRutLeido(s.IdRut, "NR");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Rut :" + s.Rut);
                dao.Status.MarcarRutLeido(s.IdRut, "M");
                //Console.WriteLine("Rol :" + s.TipoCausa + "-" + s.Rol.ToString() + "-" + s.Anio.ToString() + ", ID Tribunal: " + s.Tribunal.ToString());
                //Dimol.dao.Funciones.InsertarError("HTML con error", "Rol: " + s.TipoCausa + "-" + s.Rol.ToString() + "-" + s.Anio.ToString(), "Procesa Rol HTML", s.Anio);
                //dao.Causa.ActualizarRolHTMLFecha(s.IdCausa, s.IdCuaderno, "M");
            }
        }

    }
}
