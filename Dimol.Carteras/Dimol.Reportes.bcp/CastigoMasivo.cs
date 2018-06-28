using HiQPdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Reportes.bcp
{
    public class CastigoMasivo
    {
        public static List<dto.CastigoMasivo> ListarRutHtml()
        {
            return dao.CastigoMasivo.ListarRutHtml();
        }

        public static List<dto.CastigoMasivo> ListarRutHtmlComplementaria()
        {
            return dao.CastigoMasivo.ListarRutHtmlComplementaria();
        }

        public static List<dto.CastigoMasivo> ListarRutHtmlComplementariaMarzo2018()
        {
            return dao.CastigoMasivo.ListarRutHtmlComplementariaMarzo2018();
        }

        public static void GeneraPDFporRutHiQPdf(List<dto.CastigoMasivo> lst)
        {
            var example_html = lst[0].Html.Replace("<br>", "<br/>").Replace("\n</td>", "")
                            .Replace("</tr>\n</table>", "</table>").Replace("<b/> \n<center>", "\n<center>")
                            .Replace("</center>\n<div", "<div").Replace("</br>", "").Replace("stc.html';\">", "stc.html';\" />");

            // create the HTML to PDF converter
            HtmlToPdf htmlToPdfConverter = new HtmlToPdf();

            // set a demo serial number
            htmlToPdfConverter.SerialNumber = "YCgJMTAE-BiwJAhIB-EhlWTlBA-UEBRQFBA-U1FOUVJO-WVlZWQ==";

            // set browser width
            htmlToPdfConverter.BrowserWidth = 1200;

            // set browser height if specified, otherwise use the default
            //if (formCollection["textBoxBrowserHeight"].Length > 0)
            htmlToPdfConverter.BrowserHeight = 0;

            // set HTML Load timeout
            htmlToPdfConverter.HtmlLoadedTimeout = 60;

            // set wait time
            htmlToPdfConverter.WaitBeforeConvert = 2;

            // set PDF page size and orientation
            htmlToPdfConverter.Document.PageSize = PdfPageSize.A4;
            htmlToPdfConverter.Document.PageOrientation = PdfPageOrientation.Portrait;

            // convert URL to a PDF memory buffer
            string url = "";
            byte[] pdfBuffer = htmlToPdfConverter.ConvertHtmlToMemory(lst[0].Html, url);

            // send the PDF document to browser
            //FileResult fileResult = new FileContentResult(pdfBuffer, "application/pdf");
            //fileResult.FileDownloadName = "HtmlToPdf.pdf";

            var testFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "HtmlToPdf.pdf");
            System.IO.File.WriteAllBytes(testFile, pdfBuffer);

        }

        public static void GeneraPDFporRutHiQPdf2(List<dto.CastigoMasivo> lst, int pagina, string ubicacion)
        {

            // create an empty PDF document
            PdfDocument document = new PdfDocument();
            // set a demo serial number
            document.SerialNumber = "5q6Pt7aC-gKqPhJSH-lJ/X1sjW-xtfG1Mbf-397G1dfI-19TI39/f-3w==";
            document.Pages.Remove(0);
            PdfDocument docPorRut = new PdfDocument();
            docPorRut.SerialNumber = "5q6Pt7aC-gKqPhJSH-lJ/X1sjW-xtfG1Mbf-397G1dfI-19TI39/f-3w==";

            // add a page to document

            string ubicacionRut = "";
            try
            {
                ubicacionRut = ubicacion + "\\" + lst[0].Rut + lst[0].Dv;
                if (!System.IO.Directory.Exists(ubicacionRut))
                {
                    System.IO.Directory.CreateDirectory(ubicacionRut);
                }
                // genero carta castigo
                dto.CastigoPrejudicialCliente objCliente = new dto.CastigoPrejudicialCliente();
                objCliente.Codemp = 1;
                objCliente.Codsuc = 1;
                objCliente.Tpcid = 31;
                objCliente.Cbcnumero = lst[0].Numero;
                objCliente.Ctcid = lst[0].Ctcid;
                objCliente.Idioma = 1;
                objCliente.Empresa = "DIMOL LTDA.";
                objCliente.FechaReporte = DateTime.Now;
                objCliente.Pagina = pagina;
                objCliente.IdReporte = 1;
                objCliente.PathArchivo = ubicacionRut + "\\" + lst[0].Rut + lst[0].Dv + "_castigo.pdf";
                string ruta = bcp.Cartera.TraeCastigoPrejudicialCliente(objCliente);
                System.IO.File.Delete(objCliente.PathArchivo + ".fo");

                PdfDocument document1 = PdfDocument.FromFile(objCliente.PathArchivo);

                // genero resumen gestiones
                dto.ResumenGestiones objResumen = new dto.ResumenGestiones();
                objResumen.Codemp = 1;
                objResumen.Sucid = 1;
                objResumen.Pclid = 559;
                objResumen.Ctcid = lst[0].Ctcid;
                objResumen.FechaReporte = DateTime.Now;
                objResumen.Pagina = pagina;
                objResumen.IdReporte = 2;
                objResumen.Idioma = 1;
                objResumen.PathArchivo = ubicacionRut + "\\" + lst[0].Rut + lst[0].Dv + "_resumen.pdf";
                bool resumen = bcp.Cartera.TraeResumenGestiones(objResumen);
                System.IO.File.Delete(objResumen.PathArchivo + ".fo");

                PdfDocument document2 = PdfDocument.FromFile(objResumen.PathArchivo);

                // genero estado tributario
                var example_html = lst[0].Html.Replace("<br>", "<br/>").Replace("\n</td>", "")
                            .Replace("</tr>\n</table>", "</table>").Replace("<b/> \n<center>", "\n<center>")
                            .Replace("</center>\n<div", "<div").Replace("</br>", "").Replace("stc.html';\">", "stc.html';\" />");
                // layout the HTML from URL 1
                System.Drawing.PointF location1 = System.Drawing.PointF.Empty;
                PdfHtml html1 = new PdfHtml(location1.X, location1.Y, example_html, "");

                //document.AddDocument(document1);
                //document.AddDocument(document2);

                PdfPage page1 = document.AddPage(PdfPageSize.Letter, new PdfDocumentMargins(2), PdfPageOrientation.Portrait);
                html1.WaitBeforeConvert = 2;
                PdfLayoutInfo html1LayoutInfo = page1.Layout(html1);
                //document.AddPageAtIndex(document.Pages.Count,page1);

                docPorRut.AddDocument(document1);
                docPorRut.AddDocument(document2);
                docPorRut.AddPageAtIndex(docPorRut.Pages.Count, page1);

                // write the PDF document to a memory buffer
                byte[] pdfBufferRut = docPorRut.WriteToMemory();

                var rutFile = Path.Combine(ubicacionRut + "\\", lst[0].Rut + lst[0].Dv + ".pdf");
                System.IO.File.WriteAllBytes(rutFile, pdfBufferRut);
                ;
                docPorRut.Close();
                PdfDocument documentx3 = new PdfDocument();
                documentx3 = PdfDocument.FromFile(rutFile);
                document.AddDocument(0, documentx3);
                document.Pages.Remove(document.Pages.Count - 1);
                PdfPage page2 = null;
                System.Drawing.PointF location2 = System.Drawing.PointF.Empty;
                //byte[] pdfBuffer = document.WriteToMemory();

                var testFile = ubicacion + "\\" + "castigo_masivo_imprimible.pdf";
                for (int i = 1; i < lst.Count; i++)
                {
                    ubicacionRut = ubicacion + "\\" + lst[i].Rut + lst[i].Dv;
                    docPorRut = new PdfDocument();
                    docPorRut.SerialNumber = "5q6Pt7aC-gKqPhJSH-lJ/X1sjW-xtfG1Mbf-397G1dfI-19TI39/f-3w==";
                    if (!System.IO.Directory.Exists(ubicacionRut))
                    {
                        System.IO.Directory.CreateDirectory(ubicacionRut);
                    }
                    // genero carta castigo
                    objCliente = new dto.CastigoPrejudicialCliente();
                    objCliente.Codemp = 1;
                    objCliente.Codsuc = 1;
                    objCliente.Tpcid = 31;
                    objCliente.Cbcnumero = lst[i].Numero;
                    objCliente.Ctcid = lst[i].Ctcid;
                    objCliente.Idioma = 1;
                    objCliente.Empresa = "DIMOL LTDA.";
                    objCliente.FechaReporte = DateTime.Now;
                    objCliente.Pagina = pagina;
                    objCliente.IdReporte = 1;
                    objCliente.PathArchivo = ubicacionRut + "\\" + lst[i].Rut + lst[i].Dv + "_castigo.pdf";
                    if (!System.IO.File.Exists(objCliente.PathArchivo))
                    {
                        ruta = bcp.Cartera.TraeCastigoPrejudicialCliente(objCliente);
                        System.IO.File.Delete(objCliente.PathArchivo + ".fo");
                    }

                    document1 = PdfDocument.FromFile(objCliente.PathArchivo);

                    // genero resumen gestiones
                    objResumen = new dto.ResumenGestiones();
                    objResumen.Codemp = 1;
                    objResumen.Sucid = 1;
                    objResumen.Pclid = 559;
                    objResumen.Ctcid = lst[i].Ctcid;
                    objResumen.FechaReporte = DateTime.Now;
                    objResumen.Pagina = pagina;
                    objResumen.IdReporte = 2;
                    objResumen.Idioma = 1;
                    objResumen.PathArchivo = ubicacionRut + "\\" + lst[i].Rut + lst[i].Dv + "_resumen.pdf";
                    if (!System.IO.File.Exists(objResumen.PathArchivo))
                    {
                        resumen = bcp.Cartera.TraeResumenGestiones(objResumen);
                        System.IO.File.Delete(objResumen.PathArchivo + ".fo");
                    }
                    document2 = PdfDocument.FromFile(objResumen.PathArchivo);

                    // genero estado tributario
                    example_html = lst[i].Html.Replace("<br>", "<br/>").Replace("\n</td>", "")
                            .Replace("</tr>\n</table>", "</table>").Replace("<b/> \n<center>", "\n<center>")
                            .Replace("</center>\n<div", "<div").Replace("</br>", "").Replace("stc.html';\">", "stc.html';\" />");
                    page2 = document.AddPage(PdfPageSize.Letter, new PdfDocumentMargins(5), PdfPageOrientation.Portrait);
                    location2 = System.Drawing.PointF.Empty;
                    PdfHtml html2 = new PdfHtml(location2.X, location2.Y, example_html, "");
                    html2.WaitBeforeConvert = 2;
                    page2.Layout(html2);
                    //document.AddPageAtIndex(document.Pages.Count, page2);
                    //document.AddPage(page2);

                    docPorRut.AddDocument(document1);
                    docPorRut.AddDocument(document2);
                    docPorRut.AddPageAtIndex(docPorRut.Pages.Count, page2);
                    // write the PDF document to a memory buffer
                    pdfBufferRut = docPorRut.WriteToMemory();

                    rutFile = Path.Combine(ubicacionRut + "\\", lst[i].Rut + lst[i].Dv + ".pdf");
                    System.IO.File.WriteAllBytes(rutFile, pdfBufferRut);

                    docPorRut.Close();
                    document.Pages.Remove(document.Pages.Count - 1);
                    documentx3 = PdfDocument.FromFile(rutFile);
                    document.AddDocument(documentx3);

                    //if (counter % 10 == 0)
                    //{
                    //    pdfBuffer = document.WriteToMemory();

                    //    testFile = ubicacion + "\\" + "castigo_masivo_imprimible_"+counter.ToString()+".pdf";
                    //    System.IO.File.WriteAllBytes(testFile, pdfBuffer);
                    //    document.Close();
                    //    document = new PdfDocument();
                    //    document.SerialNumber = "5q6Pt7aC-gKqPhJSH-lJ/X1sjW-xtfG1Mbf-397G1dfI-19TI39/f-3w==";
                    //}
                    //counter++;

                }

                // write the PDF document to a memory buffer
                byte[] pdfBuffer = document.WriteToMemory();

                testFile = ubicacion + "\\" + "castigo_masivo_imprimible_" + lst.Count.ToString() + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".pdf";
                System.IO.File.WriteAllBytes(testFile, pdfBuffer);

            }
            finally
            {
                document.Close();
            }

        }

        public static void GeneraPDFporRutComplementaria(List<dto.CastigoMasivo> lst, int pagina, string ubicacion)
        {
            // create an empty PDF document
            PdfDocument document = new PdfDocument();
            // set a demo serial number
            document.SerialNumber = "5q6Pt7aC-gKqPhJSH-lJ/X1sjW-xtfG1Mbf-397G1dfI-19TI39/f-3w==";
            document.Pages.Remove(0);
            PdfDocument docPorRut = new PdfDocument();
            docPorRut.SerialNumber = "5q6Pt7aC-gKqPhJSH-lJ/X1sjW-xtfG1Mbf-397G1dfI-19TI39/f-3w==";
            bool resumen = false;
            string ruta = "";
            // add a page to document

            string ubicacionRut = "";
            try
            {
                ubicacionRut = ubicacion + "\\" + lst[0].Rut + lst[0].Dv;
                if (!System.IO.Directory.Exists(ubicacionRut))
                {
                    System.IO.Directory.CreateDirectory(ubicacionRut);
                }
                string[] fileEntries = Directory.GetFiles(ubicacionRut);
                // genero carta castigo
                dto.CastigoPrejudicialCliente objCliente = new dto.CastigoPrejudicialCliente();
                objCliente.Codemp = 1;
                objCliente.Codsuc = 1;
                objCliente.Tpcid = 31;
                objCliente.Cbcnumero = lst[0].Numero;
                objCliente.Ctcid = lst[0].Ctcid;
                objCliente.Idioma = 1;
                objCliente.Empresa = "DIMOL LTDA.";
                objCliente.FechaReporte = DateTime.Now;
                objCliente.Pagina = pagina;
                objCliente.IdReporte = 1;
                objCliente.PathArchivo = ubicacionRut + "\\" + lst[0].Rut + lst[0].Dv + "_castigo.pdf";
                if (System.IO.File.Exists(objCliente.PathArchivo))
                {
                    File.Delete(objCliente.PathArchivo);

                }
                ruta = bcp.Cartera.TraeCastigoPrejudicialCliente(objCliente);
                System.IO.File.Delete(objCliente.PathArchivo + ".fo");
                PdfDocument document1 = PdfDocument.FromFile(objCliente.PathArchivo);

                // genero resumen gestiones
                dto.ResumenGestiones objResumen = new dto.ResumenGestiones();
                objResumen.Codemp = 1;
                objResumen.Sucid = 1;
                objResumen.Pclid = 579;
                objResumen.Ctcid = lst[0].Ctcid;
                objResumen.FechaReporte = DateTime.Now;
                objResumen.Pagina = pagina;
                objResumen.IdReporte = 2;
                objResumen.Idioma = 1;
                objResumen.PathArchivo = ubicacionRut + "\\" + lst[0].Rut + lst[0].Dv + "_resumen.pdf";
                if (System.IO.File.Exists(objResumen.PathArchivo))
                {
                    File.Delete(objResumen.PathArchivo);

                }
                resumen = bcp.Cartera.TraeResumenGestiones(objResumen);
                System.IO.File.Delete(objResumen.PathArchivo + ".fo");
                PdfDocument document2 = PdfDocument.FromFile(objResumen.PathArchivo);

                // genero estado tributario
                var example_html = lst[0].Html.Replace("<br>", "<br/>").Replace("\n</td>", "")
                            .Replace("</tr>\n</table>", "</table>").Replace("<b/> \n<center>", "\n<center>")
                            .Replace("</center>\n<div", "<div").Replace("</br>", "").Replace("stc.html';\">", "stc.html';\" />");
                // layout the HTML from URL 1
                System.Drawing.PointF location1 = System.Drawing.PointF.Empty;
                PdfHtml html1 = new PdfHtml(location1.X, location1.Y, example_html, "");

                //document.AddDocument(document1);
                //document.AddDocument(document2);

                PdfPage page1 = document.AddPage(PdfPageSize.Letter, new PdfDocumentMargins(2), PdfPageOrientation.Portrait);
                html1.WaitBeforeConvert = 2;
                PdfLayoutInfo html1LayoutInfo = page1.Layout(html1);
                //document.AddPageAtIndex(document.Pages.Count,page1);

                docPorRut.AddDocument(document1);
                docPorRut.AddDocument(document2);
                docPorRut.AddPageAtIndex(docPorRut.Pages.Count, page1);

                // Process the list of files found in the directory.

                PdfDocument factura = new PdfDocument();
                foreach (string fileName in fileEntries)
                {
                    if (!(fileName.Contains("_resumen") || fileName.Contains("_castigo") || fileName.Contains(lst[0].Rut + lst[0].Dv + ".pdf")))
                    {
                        factura = PdfDocument.FromFile(fileName);
                        docPorRut.AddPage(factura.Pages[0]);
                    }
                }

                // write the PDF document to a memory buffer
                byte[] pdfBufferRut = docPorRut.WriteToMemory();

                var rutFile = Path.Combine(ubicacionRut + "\\", lst[0].Rut + lst[0].Dv + ".pdf");
                if (System.IO.File.Exists(rutFile))
                {
                    File.Delete(rutFile);

                }
                System.IO.File.WriteAllBytes(rutFile, pdfBufferRut); ;
                docPorRut.Close();
                document1.Close();
                document2.Close();
                if (System.IO.File.Exists(ubicacionRut + "\\" + lst[0].Rut + lst[0].Dv + "_castigo.pdf"))
                {
                    File.Delete(ubicacionRut + "\\" + lst[0].Rut + lst[0].Dv + "_castigo.pdf");

                }
                if (System.IO.File.Exists(ubicacionRut + "\\" + lst[0].Rut + lst[0].Dv + "_resumen.pdf"))
                {
                    File.Delete(ubicacionRut + "\\" + lst[0].Rut + lst[0].Dv + "_resumen.pdf");

                }
                PdfDocument documentx3 = new PdfDocument();
                documentx3 = PdfDocument.FromFile(rutFile);
                document.AddDocument(0, documentx3);
                document.Pages.Remove(document.Pages.Count - 1);
                PdfPage page2 = null;
                System.Drawing.PointF location2 = System.Drawing.PointF.Empty;
                //byte[] pdfBuffer = document.WriteToMemory();



                var testFile = ubicacion + "\\" + "castigo_masivo_imprimible.pdf";
                for (int i = 1; i < lst.Count; i++)
                {
                    ubicacionRut = ubicacion + "\\" + lst[i].Rut + lst[i].Dv;
                    docPorRut = new PdfDocument();
                    docPorRut.SerialNumber = "5q6Pt7aC-gKqPhJSH-lJ/X1sjW-xtfG1Mbf-397G1dfI-19TI39/f-3w==";
                    if (!System.IO.Directory.Exists(ubicacionRut))
                    {
                        System.IO.Directory.CreateDirectory(ubicacionRut);
                    }
                    fileEntries = Directory.GetFiles(ubicacionRut);
                    // genero carta castigo
                    objCliente = new dto.CastigoPrejudicialCliente();
                    objCliente.Codemp = 1;
                    objCliente.Codsuc = 1;
                    objCliente.Tpcid = 31;
                    objCliente.Cbcnumero = lst[i].Numero;
                    objCliente.Ctcid = lst[i].Ctcid;
                    objCliente.Idioma = 1;
                    objCliente.Empresa = "DIMOL LTDA.";
                    objCliente.FechaReporte = DateTime.Now;
                    objCliente.Pagina = pagina;
                    objCliente.IdReporte = 1;
                    objCliente.PathArchivo = ubicacionRut + "\\" + lst[i].Rut + lst[i].Dv + "_castigo.pdf";
                    if (!System.IO.File.Exists(objCliente.PathArchivo))
                    {
                        File.Delete(objCliente.PathArchivo);

                    }
                    ruta = bcp.Cartera.TraeCastigoPrejudicialCliente(objCliente);
                    System.IO.File.Delete(objCliente.PathArchivo + ".fo");
                    document1 = PdfDocument.FromFile(objCliente.PathArchivo);

                    // genero resumen gestiones
                    objResumen = new dto.ResumenGestiones();
                    objResumen.Codemp = 1;
                    objResumen.Sucid = 1;
                    objResumen.Pclid = 579;
                    objResumen.Ctcid = lst[i].Ctcid;
                    objResumen.FechaReporte = DateTime.Now;
                    objResumen.Pagina = pagina;
                    objResumen.IdReporte = 2;
                    objResumen.Idioma = 1;
                    objResumen.PathArchivo = ubicacionRut + "\\" + lst[i].Rut + lst[i].Dv + "_resumen.pdf";
                    if (System.IO.File.Exists(objResumen.PathArchivo))
                    {
                        File.Delete(objResumen.PathArchivo);

                    }
                    resumen = bcp.Cartera.TraeResumenGestiones(objResumen);
                    System.IO.File.Delete(objResumen.PathArchivo + ".fo");
                    document2 = PdfDocument.FromFile(objResumen.PathArchivo);

                    // genero estado tributario
                    example_html = lst[i].Html.Replace("<br>", "<br/>").Replace("\n</td>", "")
                            .Replace("</tr>\n</table>", "</table>").Replace("<b/> \n<center>", "\n<center>")
                            .Replace("</center>\n<div", "<div").Replace("</br>", "").Replace("stc.html';\">", "stc.html';\" />");
                    page2 = document.AddPage(PdfPageSize.Letter, new PdfDocumentMargins(5), PdfPageOrientation.Portrait);
                    location2 = System.Drawing.PointF.Empty;
                    PdfHtml html2 = new PdfHtml(location2.X, location2.Y, example_html, "");
                    html2.WaitBeforeConvert = 2;
                    page2.Layout(html2);
                    //document.AddPageAtIndex(document.Pages.Count, page2);
                    //document.AddPage(page2);

                    docPorRut.AddDocument(document1);
                    docPorRut.AddDocument(document2);
                    docPorRut.AddPageAtIndex(docPorRut.Pages.Count, page2);

                    // Process the list of files found in the directory.

                    //PdfDocument factura = new PdfDocument();
                    foreach (string fileName in fileEntries)
                    {
                        if (!(fileName.Contains("_resumen") || fileName.Contains("_castigo") || fileName.Contains(lst[i].Rut + lst[i].Dv + ".pdf")))
                        {
                            factura = PdfDocument.FromFile(fileName);
                            docPorRut.AddPage(factura.Pages[0]);
                        }

                    }

                    // write the PDF document to a memory buffer
                    pdfBufferRut = docPorRut.WriteToMemory();
                    rutFile = Path.Combine(ubicacionRut + "\\", lst[i].Rut + lst[i].Dv + ".pdf");
                    if (System.IO.File.Exists(rutFile))
                    {
                        File.Delete(rutFile);

                    }
                    System.IO.File.WriteAllBytes(rutFile, pdfBufferRut); ;
                    docPorRut.Close();
                    document1.Close();
                    document2.Close();
                    if (System.IO.File.Exists(ubicacionRut + "\\" + lst[i].Rut + lst[i].Dv + "_castigo.pdf"))
                    {
                        File.Delete(ubicacionRut + "\\" + lst[i].Rut + lst[i].Dv + "_castigo.pdf");

                    }
                    if (System.IO.File.Exists(ubicacionRut + "\\" + lst[i].Rut + lst[i].Dv + "_resumen.pdf"))
                    {
                        File.Delete(ubicacionRut + "\\" + lst[i].Rut + lst[i].Dv + "_resumen.pdf");

                    }
                    document.Pages.Remove(document.Pages.Count - 1);
                    documentx3 = PdfDocument.FromFile(rutFile);
                    document.AddDocument(documentx3);

                    //if (counter % 10 == 0)
                    //{
                    //    pdfBuffer = document.WriteToMemory();

                    //    testFile = ubicacion + "\\" + "castigo_masivo_imprimible_"+counter.ToString()+".pdf";
                    //    System.IO.File.WriteAllBytes(testFile, pdfBuffer);
                    //    document.Close();
                    //    document = new PdfDocument();
                    //    document.SerialNumber = "5q6Pt7aC-gKqPhJSH-lJ/X1sjW-xtfG1Mbf-397G1dfI-19TI39/f-3w==";
                    //}
                    //counter++;
                }

                // write the PDF document to a memory buffer
                byte[] pdfBuffer = document.WriteToMemory();
                //ubicacion = "d:\\";
                testFile = ubicacion + "\\" + "castigo_masivo_imprimible_" + lst.Count.ToString() + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".pdf";
                System.IO.File.WriteAllBytes(testFile, pdfBuffer);

            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "GeneraPDFporRutComplementaria: ", 309);
            }
            finally
            {
                document.Close();
            }

        }

        public static void GeneraPDFporRutSinCertificado(List<dto.CastigoMasivo> lst, int pagina, string ubicacion)
        {

            // create an empty PDF document
            PdfDocument document = new PdfDocument();
            // set a demo serial number
            document.SerialNumber = "5q6Pt7aC-gKqPhJSH-lJ/X1sjW-xtfG1Mbf-397G1dfI-19TI39/f-3w==";
            document.Pages.Remove(0);
            PdfDocument docPorRut = new PdfDocument();
            docPorRut.SerialNumber = "5q6Pt7aC-gKqPhJSH-lJ/X1sjW-xtfG1Mbf-397G1dfI-19TI39/f-3w==";
            bool resumen = false;
            string ruta = "";
            // add a page to document

            string ubicacionRut = "";
            try
            {
                ubicacionRut = ubicacion + "\\" + lst[0].Rut + lst[0].Dv;
                if (!System.IO.Directory.Exists(ubicacionRut))
                {
                    System.IO.Directory.CreateDirectory(ubicacionRut);
                }
                string[] fileEntries = Directory.GetFiles(ubicacionRut);

                // genero carta castigo
                dto.CastigoPrejudicialCliente objCliente = new dto.CastigoPrejudicialCliente();
                objCliente.Codemp = 1;
                objCliente.Codsuc = 1;
                objCliente.Tpcid = 31;
                objCliente.Cbcnumero = lst[0].Numero;
                objCliente.Ctcid = lst[0].Ctcid;
                objCliente.Idioma = 1;
                objCliente.Empresa = "DIMOL LTDA.";
                objCliente.FechaReporte = DateTime.Now;
                objCliente.Pagina = pagina;
                objCliente.IdReporte = 1;
                objCliente.PathArchivo = ubicacionRut + "\\" + lst[0].Rut + lst[0].Dv + "_castigo.pdf";
                if (System.IO.File.Exists(objCliente.PathArchivo))
                {
                    File.Delete(objCliente.PathArchivo);

                }
                //ruta = bcp.Cartera.TraeCastigoPrejudicialCliente(objCliente);
                //System.IO.File.Delete(objCliente.PathArchivo + ".fo");
                PdfDocument document1 = PdfDocument.FromFile(ubicacionRut+"\\"+ "detalle0.pdf");

                // genero resumen gestiones
                dto.ResumenGestiones objResumen = new dto.ResumenGestiones();
                objResumen.Codemp = 1;
                objResumen.Sucid = 1;
                objResumen.Pclid = 318;
                objResumen.Ctcid = lst[0].Ctcid;
                objResumen.FechaReporte = DateTime.Now;
                objResumen.Pagina = pagina;
                objResumen.IdReporte = 2;
                objResumen.Idioma = 1;
                objResumen.PathArchivo = ubicacionRut + "\\" + lst[0].Rut + lst[0].Dv + "_resumen.pdf";
                if (System.IO.File.Exists(objResumen.PathArchivo))
                {
                    File.Delete(objResumen.PathArchivo);

                }
                resumen = bcp.Cartera.TraeResumenGestiones(objResumen);
                System.IO.File.Delete(objResumen.PathArchivo + ".fo");
                PdfDocument document2 = PdfDocument.FromFile(objResumen.PathArchivo);

                // genero estado tributario
                var example_html = lst[0].Html.Replace("<br>", "<br/>").Replace("\n</td>", "")
                            .Replace("</tr>\n</table>", "</table>").Replace("<b/> \n<center>", "\n<center>")
                            .Replace("</center>\n<div", "<div").Replace("</br>", "").Replace("stc.html';\">", "stc.html';\" />");
                // layout the HTML from URL 1
                System.Drawing.PointF location1 = System.Drawing.PointF.Empty;
                PdfHtml html1 = new PdfHtml(location1.X, location1.Y, example_html, "");

                //document.AddDocument(document1);
                //document.AddDocument(document2);

                PdfPage page1 = document.AddPage(PdfPageSize.Letter, new PdfDocumentMargins(2), PdfPageOrientation.Portrait);
                html1.WaitBeforeConvert = 2;
                PdfLayoutInfo html1LayoutInfo = page1.Layout(html1);
                //document.AddPageAtIndex(document.Pages.Count,page1);

                docPorRut.AddDocument(document1);
                docPorRut.AddDocument(document2);
                docPorRut.AddPageAtIndex(docPorRut.Pages.Count, page1);

                // Process the list of files found in the directory.

                PdfDocument factura = new PdfDocument();
                foreach (string fileName in fileEntries)
                {
                    if (!(fileName.Contains("_resumen") || fileName.Contains("_castigo") || fileName.Contains(lst[0].Rut + lst[0].Dv + ".pdf" )|| fileName.Contains("detalle0")))
                    {
                        if (!fileName.Contains("detalle"))
                        {
                            factura = PdfDocument.FromFile(fileName);
                            docPorRut.AddPage(factura.Pages[0]);
                        } else
                        {
                            factura = PdfDocument.FromFile(fileName);
                            docPorRut.AddDocument(factura);
                        }

                    }
                }

                // write the PDF document to a memory buffer
                byte[] pdfBufferRut = docPorRut.WriteToMemory();

                var rutFile = Path.Combine(ubicacionRut + "\\", lst[0].Rut + lst[0].Dv + ".pdf");
                if (System.IO.File.Exists(rutFile))
                {
                    File.Delete(rutFile);

                }
                System.IO.File.WriteAllBytes(rutFile, pdfBufferRut); ;
                docPorRut.Close();
                //document1.Close();
                document2.Close();
                if (System.IO.File.Exists(ubicacionRut + "\\" + lst[0].Rut + lst[0].Dv + "_castigo.pdf"))
                {
                    File.Delete(ubicacionRut + "\\" + lst[0].Rut + lst[0].Dv + "_castigo.pdf");

                }
                if (System.IO.File.Exists(ubicacionRut + "\\" + lst[0].Rut + lst[0].Dv + "_resumen.pdf"))
                {
                    File.Delete(ubicacionRut + "\\" + lst[0].Rut + lst[0].Dv + "_resumen.pdf");

                }
                //PdfDocument documentx3 = new PdfDocument();
                //documentx3 = PdfDocument.FromFile(rutFile);
                //document.AddDocument(0, documentx3);
                //document.Pages.Remove(document.Pages.Count - 1);
                PdfPage page2 = null;
                System.Drawing.PointF location2 = System.Drawing.PointF.Empty;
                //byte[] pdfBuffer = document.WriteToMemory();



                var testFile = ubicacion + "\\" + "castigo_masivo_imprimible.pdf";
                for (int i = 1; i < lst.Count; i++)
                {
                    ubicacionRut = ubicacion + "\\" + lst[i].Rut + lst[i].Dv;
                    docPorRut = new PdfDocument();
                    docPorRut.SerialNumber = "5q6Pt7aC-gKqPhJSH-lJ/X1sjW-xtfG1Mbf-397G1dfI-19TI39/f-3w==";
                    if (!System.IO.Directory.Exists(ubicacionRut))
                    {
                        System.IO.Directory.CreateDirectory(ubicacionRut);
                    }
                    fileEntries = Directory.GetFiles(ubicacionRut);
                    // genero carta castigo
                    objCliente = new dto.CastigoPrejudicialCliente();
                    objCliente.Codemp = 1;
                    objCliente.Codsuc = 1;
                    objCliente.Tpcid = 31;
                    objCliente.Cbcnumero = lst[i].Numero;
                    objCliente.Ctcid = lst[i].Ctcid;
                    objCliente.Idioma = 1;
                    objCliente.Empresa = "DIMOL LTDA.";
                    objCliente.FechaReporte = DateTime.Now;
                    objCliente.Pagina = pagina;
                    objCliente.IdReporte = 1;
                    objCliente.PathArchivo = ubicacionRut + "\\" + lst[i].Rut + lst[i].Dv + "_castigo.pdf";
                    if (!System.IO.File.Exists(objCliente.PathArchivo))
                    {
                        File.Delete(objCliente.PathArchivo);

                    }
                    //ruta = bcp.Cartera.TraeCastigoPrejudicialCliente(objCliente);
                    //System.IO.File.Delete(objCliente.PathArchivo + ".fo");
                    //document1 = PdfDocument.FromFile(objCliente.PathArchivo);
                    document1 = PdfDocument.FromFile(ubicacionRut + "\\" + "detalle0.pdf");

                    // genero resumen gestiones
                    objResumen = new dto.ResumenGestiones();
                    objResumen.Codemp = 1;
                    objResumen.Sucid = 1;
                    objResumen.Pclid = 318;
                    objResumen.Ctcid = lst[i].Ctcid;
                    objResumen.FechaReporte = DateTime.Now;
                    objResumen.Pagina = pagina;
                    objResumen.IdReporte = 2;
                    objResumen.Idioma = 1;
                    objResumen.PathArchivo = ubicacionRut + "\\" + lst[i].Rut + lst[i].Dv + "_resumen.pdf";
                    if (System.IO.File.Exists(objResumen.PathArchivo))
                    {
                        File.Delete(objResumen.PathArchivo);

                    }
                    resumen = bcp.Cartera.TraeResumenGestiones(objResumen);
                    System.IO.File.Delete(objResumen.PathArchivo + ".fo");
                    document2 = PdfDocument.FromFile(objResumen.PathArchivo);

                    // genero estado tributario
                    example_html = lst[i].Html.Replace("<br>", "<br/>").Replace("\n</td>", "")
                            .Replace("</tr>\n</table>", "</table>").Replace("<b/> \n<center>", "\n<center>")
                            .Replace("</center>\n<div", "<div").Replace("</br>", "").Replace("stc.html';\">", "stc.html';\" />");
                    page2 = document.AddPage(PdfPageSize.Letter, new PdfDocumentMargins(5), PdfPageOrientation.Portrait);
                    location2 = System.Drawing.PointF.Empty;
                    PdfHtml html2 = new PdfHtml(location2.X, location2.Y, example_html, "");
                    html2.WaitBeforeConvert = 2;
                    page2.Layout(html2);
                    //document.AddPageAtIndex(document.Pages.Count, page2);
                    //document.AddPage(page2);

                    docPorRut.AddDocument(document1);
                    docPorRut.AddDocument(document2);
                    docPorRut.AddPageAtIndex(docPorRut.Pages.Count, page2);

                    // Process the list of files found in the directory.

                    //PdfDocument factura = new PdfDocument();
                    foreach (string fileName in fileEntries)
                    {
                        if (!(fileName.Contains("_resumen") || fileName.Contains("_castigo") || fileName.Contains(lst[i].Rut + lst[i].Dv + ".pdf") || fileName.Contains("detalle0")))
                        {
                            if (!fileName.Contains("detalle"))
                            {
                                factura = PdfDocument.FromFile(fileName);
                                docPorRut.AddPage(factura.Pages[0]);
                            }
                            else
                            {
                                factura = PdfDocument.FromFile(fileName);
                                docPorRut.AddDocument(factura);
                            }
                        }

                    }

                    // write the PDF document to a memory buffer
                    pdfBufferRut = docPorRut.WriteToMemory();
                    rutFile = Path.Combine(ubicacionRut + "\\", lst[i].Rut + lst[i].Dv + ".pdf");
                    if (System.IO.File.Exists(rutFile))
                    {
                        File.Delete(rutFile);

                    }
                    System.IO.File.WriteAllBytes(rutFile, pdfBufferRut); ;
                    docPorRut.Close();
                    document1.Close();
                    document2.Close();
                    if (System.IO.File.Exists(ubicacionRut + "\\" + lst[i].Rut + lst[i].Dv + "_castigo.pdf"))
                    {
                        File.Delete(ubicacionRut + "\\" + lst[i].Rut + lst[i].Dv + "_castigo.pdf");

                    }
                    if (System.IO.File.Exists(ubicacionRut + "\\" + lst[i].Rut + lst[i].Dv + "_resumen.pdf"))
                    {
                        File.Delete(ubicacionRut + "\\" + lst[i].Rut + lst[i].Dv + "_resumen.pdf");

                    }
                    //document.Pages.Remove(document.Pages.Count - 1);
                    //documentx3 = PdfDocument.FromFile(rutFile);
                    //document.AddDocument(documentx3);

                    //if (counter % 10 == 0)
                    //{
                    //    pdfBuffer = document.WriteToMemory();

                    //    testFile = ubicacion + "\\" + "castigo_masivo_imprimible_"+counter.ToString()+".pdf";
                    //    System.IO.File.WriteAllBytes(testFile, pdfBuffer);
                    //    document.Close();
                    //    document = new PdfDocument();
                    //    document.SerialNumber = "5q6Pt7aC-gKqPhJSH-lJ/X1sjW-xtfG1Mbf-397G1dfI-19TI39/f-3w==";
                    //}
                    //counter++;
                }

                // write the PDF document to a memory buffer
                //byte[] pdfBuffer = document.WriteToMemory();
                ////ubicacion = "d:\\";
                //testFile = ubicacion + "\\" + "castigo_masivo_imprimible_" + lst.Count.ToString() + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".pdf";
                //System.IO.File.WriteAllBytes(testFile, pdfBuffer);

            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "GeneraPDFporRutComplementaria: ", 309);
            }
            finally
            {
                document.Close();
            }

        }

        public static void GeneraPDFDevolucion(List<dto.CastigoMasivo> lst, int pagina, string ubicacion)
        {

            // create an empty PDF document
            PdfDocument document = new PdfDocument();
            // set a demo serial number
            document.SerialNumber = "5q6Pt7aC-gKqPhJSH-lJ/X1sjW-xtfG1Mbf-397G1dfI-19TI39/f-3w==";
            document.Pages.Remove(0);
            PdfDocument docPorRut = new PdfDocument();
            docPorRut.SerialNumber = "5q6Pt7aC-gKqPhJSH-lJ/X1sjW-xtfG1Mbf-397G1dfI-19TI39/f-3w==";
            bool resumen = false;
            string ruta = "";
            // add a page to document

            string ubicacionRut = "";
            try
            {
                ubicacionRut = ubicacion + "\\" + lst[0].Rut + lst[0].Dv;
                if (!System.IO.Directory.Exists(ubicacionRut))
                {
                    System.IO.Directory.CreateDirectory(ubicacionRut);
                }
                string[] fileEntries = Directory.GetFiles(ubicacionRut);

                // genero carta castigo
                dto.CastigoPrejudicialCliente objCliente = new dto.CastigoPrejudicialCliente();
                objCliente.Codemp = 1;
                objCliente.Codsuc = 1;
                objCliente.Tpcid = 31;
                objCliente.Cbcnumero = lst[0].Numero;
                objCliente.Ctcid = lst[0].Ctcid;
                objCliente.Idioma = 1;
                objCliente.Empresa = "DIMOL LTDA.";
                objCliente.FechaReporte = DateTime.Now;
                objCliente.Pagina = pagina;
                objCliente.IdReporte = 1;
                objCliente.PathArchivo = ubicacionRut + "\\" + lst[0].Rut + lst[0].Dv + "_castigo.pdf";
                if (System.IO.File.Exists(objCliente.PathArchivo))
                {
                    File.Delete(objCliente.PathArchivo);

                }
                //ruta = bcp.Cartera.TraeCastigoPrejudicialCliente(objCliente);
                //System.IO.File.Delete(objCliente.PathArchivo + ".fo");
                PdfDocument document1 = PdfDocument.FromFile(ubicacionRut + "\\" + "detalle0.pdf");

                // genero resumen gestiones
                dto.ResumenGestiones objResumen = new dto.ResumenGestiones();
                objResumen.Codemp = 1;
                objResumen.Sucid = 1;
                objResumen.Pclid = 318;
                objResumen.Ctcid = lst[0].Ctcid;
                objResumen.FechaReporte = DateTime.Now;
                objResumen.Pagina = pagina;
                objResumen.IdReporte = 2;
                objResumen.Idioma = 1;
                objResumen.PathArchivo = ubicacionRut + "\\" + lst[0].Rut + lst[0].Dv + "_resumen.pdf";
                if (System.IO.File.Exists(objResumen.PathArchivo))
                {
                    File.Delete(objResumen.PathArchivo);

                }
                resumen = bcp.Cartera.TraeResumenGestiones(objResumen);
                System.IO.File.Delete(objResumen.PathArchivo + ".fo");
                PdfDocument document2 = PdfDocument.FromFile(objResumen.PathArchivo);

                // genero estado tributario
                var example_html = lst[0].Html.Replace("<br>", "<br/>").Replace("\n</td>", "")
                            .Replace("</tr>\n</table>", "</table>").Replace("<b/> \n<center>", "\n<center>")
                            .Replace("</center>\n<div", "<div").Replace("</br>", "").Replace("stc.html';\">", "stc.html';\" />");
                // layout the HTML from URL 1
                System.Drawing.PointF location1 = System.Drawing.PointF.Empty;
                PdfHtml html1 = new PdfHtml(location1.X, location1.Y, example_html, "");

                //document.AddDocument(document1);
                //document.AddDocument(document2);

                PdfPage page1 = document.AddPage(PdfPageSize.Letter, new PdfDocumentMargins(2), PdfPageOrientation.Portrait);
                html1.WaitBeforeConvert = 2;
                PdfLayoutInfo html1LayoutInfo = page1.Layout(html1);
                //document.AddPageAtIndex(document.Pages.Count,page1);

                docPorRut.AddDocument(document1);
                docPorRut.AddDocument(document2);
                docPorRut.AddPageAtIndex(docPorRut.Pages.Count, page1);

                // Process the list of files found in the directory.

                PdfDocument factura = new PdfDocument();
                foreach (string fileName in fileEntries)
                {
                    if (!(fileName.Contains("_resumen") || fileName.Contains("_castigo") || fileName.Contains(lst[0].Rut + lst[0].Dv + ".pdf") || fileName.Contains("detalle0")))
                    {
                        if (!fileName.Contains("detalle"))
                        {
                            factura = PdfDocument.FromFile(fileName);
                            docPorRut.AddPage(factura.Pages[0]);
                        }
                        else
                        {
                            factura = PdfDocument.FromFile(fileName);
                            docPorRut.AddDocument(factura);
                        }

                    }
                }

                // write the PDF document to a memory buffer
                byte[] pdfBufferRut = docPorRut.WriteToMemory();

                var rutFile = Path.Combine(ubicacionRut + "\\", lst[0].Rut + lst[0].Dv + ".pdf");
                if (System.IO.File.Exists(rutFile))
                {
                    File.Delete(rutFile);

                }
                System.IO.File.WriteAllBytes(rutFile, pdfBufferRut); ;
                docPorRut.Close();
                //document1.Close();
                document2.Close();
                if (System.IO.File.Exists(ubicacionRut + "\\" + lst[0].Rut + lst[0].Dv + "_castigo.pdf"))
                {
                    File.Delete(ubicacionRut + "\\" + lst[0].Rut + lst[0].Dv + "_castigo.pdf");

                }
                if (System.IO.File.Exists(ubicacionRut + "\\" + lst[0].Rut + lst[0].Dv + "_resumen.pdf"))
                {
                    File.Delete(ubicacionRut + "\\" + lst[0].Rut + lst[0].Dv + "_resumen.pdf");

                }
                //PdfDocument documentx3 = new PdfDocument();
                //documentx3 = PdfDocument.FromFile(rutFile);
                //document.AddDocument(0, documentx3);
                //document.Pages.Remove(document.Pages.Count - 1);
                PdfPage page2 = null;
                System.Drawing.PointF location2 = System.Drawing.PointF.Empty;
                //byte[] pdfBuffer = document.WriteToMemory();



                var testFile = ubicacion + "\\" + "castigo_masivo_imprimible.pdf";
                for (int i = 1; i < lst.Count; i++)
                {
                    ubicacionRut = ubicacion + "\\" + lst[i].Rut + lst[i].Dv;
                    docPorRut = new PdfDocument();
                    docPorRut.SerialNumber = "5q6Pt7aC-gKqPhJSH-lJ/X1sjW-xtfG1Mbf-397G1dfI-19TI39/f-3w==";
                    if (!System.IO.Directory.Exists(ubicacionRut))
                    {
                        System.IO.Directory.CreateDirectory(ubicacionRut);
                    }
                    fileEntries = Directory.GetFiles(ubicacionRut);
                    // genero carta castigo
                    objCliente = new dto.CastigoPrejudicialCliente();
                    objCliente.Codemp = 1;
                    objCliente.Codsuc = 1;
                    objCliente.Tpcid = 31;
                    objCliente.Cbcnumero = lst[i].Numero;
                    objCliente.Ctcid = lst[i].Ctcid;
                    objCliente.Idioma = 1;
                    objCliente.Empresa = "DIMOL LTDA.";
                    objCliente.FechaReporte = DateTime.Now;
                    objCliente.Pagina = pagina;
                    objCliente.IdReporte = 1;
                    objCliente.PathArchivo = ubicacionRut + "\\" + lst[i].Rut + lst[i].Dv + "_castigo.pdf";
                    if (!System.IO.File.Exists(objCliente.PathArchivo))
                    {
                        File.Delete(objCliente.PathArchivo);

                    }
                    //ruta = bcp.Cartera.TraeCastigoPrejudicialCliente(objCliente);
                    //System.IO.File.Delete(objCliente.PathArchivo + ".fo");
                    //document1 = PdfDocument.FromFile(objCliente.PathArchivo);
                    document1 = PdfDocument.FromFile(ubicacionRut + "\\" + "detalle0.pdf");

                    // genero resumen gestiones
                    objResumen = new dto.ResumenGestiones();
                    objResumen.Codemp = 1;
                    objResumen.Sucid = 1;
                    objResumen.Pclid = 318;
                    objResumen.Ctcid = lst[i].Ctcid;
                    objResumen.FechaReporte = DateTime.Now;
                    objResumen.Pagina = pagina;
                    objResumen.IdReporte = 2;
                    objResumen.Idioma = 1;
                    objResumen.PathArchivo = ubicacionRut + "\\" + lst[i].Rut + lst[i].Dv + "_resumen.pdf";
                    if (System.IO.File.Exists(objResumen.PathArchivo))
                    {
                        File.Delete(objResumen.PathArchivo);

                    }
                    resumen = bcp.Cartera.TraeResumenGestiones(objResumen);
                    System.IO.File.Delete(objResumen.PathArchivo + ".fo");
                    document2 = PdfDocument.FromFile(objResumen.PathArchivo);

                    // genero estado tributario
                    example_html = lst[i].Html.Replace("<br>", "<br/>").Replace("\n</td>", "")
                            .Replace("</tr>\n</table>", "</table>").Replace("<b/> \n<center>", "\n<center>")
                            .Replace("</center>\n<div", "<div").Replace("</br>", "").Replace("stc.html';\">", "stc.html';\" />");
                    page2 = document.AddPage(PdfPageSize.Letter, new PdfDocumentMargins(5), PdfPageOrientation.Portrait);
                    location2 = System.Drawing.PointF.Empty;
                    PdfHtml html2 = new PdfHtml(location2.X, location2.Y, example_html, "");
                    html2.WaitBeforeConvert = 2;
                    page2.Layout(html2);
                    //document.AddPageAtIndex(document.Pages.Count, page2);
                    //document.AddPage(page2);

                    docPorRut.AddDocument(document1);
                    docPorRut.AddDocument(document2);
                    docPorRut.AddPageAtIndex(docPorRut.Pages.Count, page2);

                    // Process the list of files found in the directory.

                    //PdfDocument factura = new PdfDocument();
                    foreach (string fileName in fileEntries)
                    {
                        if (!(fileName.Contains("_resumen") || fileName.Contains("_castigo") || fileName.Contains(lst[i].Rut + lst[i].Dv + ".pdf") || fileName.Contains("detalle0")))
                        {
                            if (!fileName.Contains("detalle"))
                            {
                                factura = PdfDocument.FromFile(fileName);
                                docPorRut.AddPage(factura.Pages[0]);
                            }
                            else
                            {
                                factura = PdfDocument.FromFile(fileName);
                                docPorRut.AddDocument(factura);
                            }
                        }

                    }

                    // write the PDF document to a memory buffer
                    pdfBufferRut = docPorRut.WriteToMemory();
                    rutFile = Path.Combine(ubicacionRut + "\\", lst[i].Rut + lst[i].Dv + ".pdf");
                    if (System.IO.File.Exists(rutFile))
                    {
                        File.Delete(rutFile);

                    }
                    System.IO.File.WriteAllBytes(rutFile, pdfBufferRut); ;
                    docPorRut.Close();
                    document1.Close();
                    document2.Close();
                    if (System.IO.File.Exists(ubicacionRut + "\\" + lst[i].Rut + lst[i].Dv + "_castigo.pdf"))
                    {
                        File.Delete(ubicacionRut + "\\" + lst[i].Rut + lst[i].Dv + "_castigo.pdf");

                    }
                    if (System.IO.File.Exists(ubicacionRut + "\\" + lst[i].Rut + lst[i].Dv + "_resumen.pdf"))
                    {
                        File.Delete(ubicacionRut + "\\" + lst[i].Rut + lst[i].Dv + "_resumen.pdf");

                    }
                    //document.Pages.Remove(document.Pages.Count - 1);
                    //documentx3 = PdfDocument.FromFile(rutFile);
                    //document.AddDocument(documentx3);

                    //if (counter % 10 == 0)
                    //{
                    //    pdfBuffer = document.WriteToMemory();

                    //    testFile = ubicacion + "\\" + "castigo_masivo_imprimible_"+counter.ToString()+".pdf";
                    //    System.IO.File.WriteAllBytes(testFile, pdfBuffer);
                    //    document.Close();
                    //    document = new PdfDocument();
                    //    document.SerialNumber = "5q6Pt7aC-gKqPhJSH-lJ/X1sjW-xtfG1Mbf-397G1dfI-19TI39/f-3w==";
                    //}
                    //counter++;
                }

                // write the PDF document to a memory buffer
                //byte[] pdfBuffer = document.WriteToMemory();
                ////ubicacion = "d:\\";
                //testFile = ubicacion + "\\" + "castigo_masivo_imprimible_" + lst.Count.ToString() + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".pdf";
                //System.IO.File.WriteAllBytes(testFile, pdfBuffer);

            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "GeneraPDFporRutComplementaria: ", 309);
            }
            finally
            {
                document.Close();
            }

        }

        public static void GeneraPDFPrevisionalMarzo2018(List<dto.CastigoMasivo> lst, int pagina, string ubicacion)
        {
            // create an empty PDF document
            PdfDocument document = new PdfDocument();
            // set a demo serial number
            document.SerialNumber = "5q6Pt7aC-gKqPhJSH-lJ/X1sjW-xtfG1Mbf-397G1dfI-19TI39/f-3w==";
            document.Pages.Remove(0);
            PdfDocument docPorRut = new PdfDocument();
            docPorRut.SerialNumber = "5q6Pt7aC-gKqPhJSH-lJ/X1sjW-xtfG1Mbf-397G1dfI-19TI39/f-3w==";
            bool resumen = false;
            string ruta = "";
            // add a page to document

            string ubicacionRut = "";
            try
            {
                ubicacionRut = ubicacion + "\\" + lst[0].Rut + lst[0].Dv;
                if (!System.IO.Directory.Exists(ubicacionRut))
                {
                    System.IO.Directory.CreateDirectory(ubicacionRut);
                }
                string[] fileEntries = Directory.GetFiles(ubicacionRut);
                // genero carta castigo
                dto.CastigoPrejudicialCliente objCliente = new dto.CastigoPrejudicialCliente();
                objCliente.Codemp = 1;
                objCliente.Codsuc = 1;
                objCliente.Tpcid = 31;
                objCliente.Cbcnumero = lst[0].Numero;
                objCliente.Ctcid = lst[0].Ctcid;
                objCliente.Idioma = 1;
                objCliente.Empresa = "DIMOL SpA.";
                objCliente.FechaReporte = DateTime.Now;
                objCliente.Pagina = pagina;
                objCliente.IdReporte = 1;
                objCliente.PathArchivo = ubicacionRut + "\\" + lst[0].Rut + lst[0].Dv + "_castigo.pdf";
                if (System.IO.File.Exists(objCliente.PathArchivo))
                {
                    File.Delete(objCliente.PathArchivo);

                }
                ruta = bcp.Cartera.TraeCastigoMasivo(objCliente);
                System.IO.File.Delete(objCliente.PathArchivo + ".fo");
                PdfDocument document1 = PdfDocument.FromFile(objCliente.PathArchivo);

                // genero resumen gestiones
                dto.ResumenGestiones objResumen = new dto.ResumenGestiones();
                objResumen.Codemp = 1;
                objResumen.Sucid = 1;
                objResumen.Pclid = 559;
                objResumen.Ctcid = lst[0].Ctcid;
                objResumen.FechaReporte = DateTime.Now;
                objResumen.Pagina = pagina;
                objResumen.IdReporte = 2;
                objResumen.Idioma = 1;
                objResumen.PathArchivo = ubicacionRut + "\\" + lst[0].Rut + lst[0].Dv + "_resumen.pdf";
                if (System.IO.File.Exists(objResumen.PathArchivo))
                {
                    File.Delete(objResumen.PathArchivo);

                }
                resumen = bcp.Cartera.TraeResumenGestionesCastigoMasivo(objResumen);
                System.IO.File.Delete(objResumen.PathArchivo + ".fo");
                PdfDocument document2 = PdfDocument.FromFile(objResumen.PathArchivo);

                // genero estado tributario
                var example_html = lst[0].Html.Replace("<br>", "<br/>").Replace("\n</td>", "")
                            .Replace("</tr>\n</table>", "</table>").Replace("<b/> \n<center>", "\n<center>")
                            .Replace("</center>\n<div", "<div").Replace("</br>", "").Replace("stc.html';\">", "stc.html';\" />");
                // layout the HTML from URL 1
                System.Drawing.PointF location1 = System.Drawing.PointF.Empty;
                PdfHtml html1 = new PdfHtml(location1.X, location1.Y, example_html, "");

                //document.AddDocument(document1);
                //document.AddDocument(document2);

                PdfPage page1 = document.AddPage(PdfPageSize.Letter, new PdfDocumentMargins(2), PdfPageOrientation.Portrait);
                html1.WaitBeforeConvert = 2;
                PdfLayoutInfo html1LayoutInfo = page1.Layout(html1);
                //document.AddPageAtIndex(document.Pages.Count,page1);

                docPorRut.AddDocument(document1);
                docPorRut.AddDocument(document2);
                docPorRut.AddPageAtIndex(docPorRut.Pages.Count, page1);

                // Process the list of files found in the directory.

                //PdfDocument factura = new PdfDocument();
                //foreach (string fileName in fileEntries)
                //{
                //    if (!(fileName.Contains("_resumen") || fileName.Contains("_castigo") || fileName.Contains(lst[0].Rut + lst[0].Dv + ".pdf")))
                //    {
                //        factura = PdfDocument.FromFile(fileName);
                //        docPorRut.AddPage(factura.Pages[0]);
                //    }
                //}

                // write the PDF document to a memory buffer
                byte[] pdfBufferRut = docPorRut.WriteToMemory();

                var rutFile = Path.Combine(ubicacionRut + "\\", lst[0].Rut + lst[0].Dv + ".pdf");
                if (System.IO.File.Exists(rutFile))
                {
                    File.Delete(rutFile);

                }
                System.IO.File.WriteAllBytes(rutFile, pdfBufferRut); ;
                docPorRut.Close();
                document1.Close();
                document2.Close();
                if (System.IO.File.Exists(ubicacionRut + "\\" + lst[0].Rut + lst[0].Dv + "_castigo.pdf"))
                {
                    File.Delete(ubicacionRut + "\\" + lst[0].Rut + lst[0].Dv + "_castigo.pdf");

                }
                if (System.IO.File.Exists(ubicacionRut + "\\" + lst[0].Rut + lst[0].Dv + "_resumen.pdf"))
                {
                    File.Delete(ubicacionRut + "\\" + lst[0].Rut + lst[0].Dv + "_resumen.pdf");

                }
                PdfDocument documentx3 = new PdfDocument();
                documentx3 = PdfDocument.FromFile(rutFile);
                //document.AddDocument(0, documentx3);
                //document.Pages.Remove(document.Pages.Count - 1);
                PdfPage page2 = null;
                System.Drawing.PointF location2 = System.Drawing.PointF.Empty;
                //byte[] pdfBuffer = document.WriteToMemory();



                var testFile = ubicacion + "\\" + "castigo_masivo_imprimible.pdf";
                for (int i = 1; i < lst.Count; i++)
                {
                    try
                    {
                        ubicacionRut = ubicacion + "\\" + lst[i].Rut + lst[i].Dv;
                        docPorRut = new PdfDocument();
                        docPorRut.SerialNumber = "5q6Pt7aC-gKqPhJSH-lJ/X1sjW-xtfG1Mbf-397G1dfI-19TI39/f-3w==";
                        if (!System.IO.Directory.Exists(ubicacionRut))
                        {
                            System.IO.Directory.CreateDirectory(ubicacionRut);
                        }
                        fileEntries = Directory.GetFiles(ubicacionRut);
                        // genero carta castigo
                        objCliente = new dto.CastigoPrejudicialCliente();
                        objCliente.Codemp = 1;
                        objCliente.Codsuc = 1;
                        objCliente.Tpcid = 31;
                        objCliente.Cbcnumero = lst[i].Numero;
                        objCliente.Ctcid = lst[i].Ctcid;
                        objCliente.Idioma = 1;
                        objCliente.Empresa = "DIMOL SpA.";
                        objCliente.FechaReporte = DateTime.Now;
                        objCliente.Pagina = pagina;
                        objCliente.IdReporte = 1;
                        objCliente.PathArchivo = ubicacionRut + "\\" + lst[i].Rut + lst[i].Dv + "_castigo.pdf";
                        if (!System.IO.File.Exists(objCliente.PathArchivo))
                        {
                            File.Delete(objCliente.PathArchivo);

                        }
                        ruta = bcp.Cartera.TraeCastigoMasivo(objCliente);
                        System.IO.File.Delete(objCliente.PathArchivo + ".fo");
                        document1 = PdfDocument.FromFile(objCliente.PathArchivo);

                        // genero resumen gestiones
                        objResumen = new dto.ResumenGestiones();
                        objResumen.Codemp = 1;
                        objResumen.Sucid = 1;
                        objResumen.Pclid = 559;
                        objResumen.Ctcid = lst[i].Ctcid;
                        objResumen.FechaReporte = DateTime.Now;
                        objResumen.Pagina = pagina;
                        objResumen.IdReporte = 2;
                        objResumen.Idioma = 1;
                        objResumen.PathArchivo = ubicacionRut + "\\" + lst[i].Rut + lst[i].Dv + "_resumen.pdf";
                        if (System.IO.File.Exists(objResumen.PathArchivo))
                        {
                            File.Delete(objResumen.PathArchivo);

                        }
                        resumen = bcp.Cartera.TraeResumenGestionesCastigoMasivo(objResumen);
                        System.IO.File.Delete(objResumen.PathArchivo + ".fo");
                        document2 = PdfDocument.FromFile(objResumen.PathArchivo);

                        // genero estado tributario
                        example_html = lst[i].Html.Replace("<br>", "<br/>").Replace("\n</td>", "")
                                .Replace("</tr>\n</table>", "</table>").Replace("<b/> \n<center>", "\n<center>")
                                .Replace("</center>\n<div", "<div").Replace("</br>", "").Replace("stc.html';\">", "stc.html';\" />");
                        page2 = document.AddPage(PdfPageSize.Letter, new PdfDocumentMargins(5), PdfPageOrientation.Portrait);
                        location2 = System.Drawing.PointF.Empty;
                        PdfHtml html2 = new PdfHtml(location2.X, location2.Y, example_html, "");
                        html2.WaitBeforeConvert = 2;
                        page2.Layout(html2);
                        //document.AddPageAtIndex(document.Pages.Count, page2);
                        //document.AddPage(page2);

                        docPorRut.AddDocument(document1);
                        docPorRut.AddDocument(document2);
                        docPorRut.AddPageAtIndex(docPorRut.Pages.Count, page2);

                        // Process the list of files found in the directory.

                        //PdfDocument factura = new PdfDocument();
                        //foreach (string fileName in fileEntries)
                        //{
                        //    if (!(fileName.Contains("_resumen") || fileName.Contains("_castigo") || fileName.Contains(lst[i].Rut + lst[i].Dv + ".pdf")))
                        //    {
                        //        factura = PdfDocument.FromFile(fileName);
                        //        docPorRut.AddPage(factura.Pages[0]);
                        //    }

                        //}

                        // write the PDF document to a memory buffer
                        pdfBufferRut = docPorRut.WriteToMemory();
                        rutFile = Path.Combine(ubicacionRut + "\\", lst[i].Rut + lst[i].Dv + ".pdf");
                        if (System.IO.File.Exists(rutFile))
                        {
                            File.Delete(rutFile);

                        }
                        System.IO.File.WriteAllBytes(rutFile, pdfBufferRut); ;
                        docPorRut.Close();
                        document1.Close();
                        document2.Close();
                        if (System.IO.File.Exists(ubicacionRut + "\\" + lst[i].Rut + lst[i].Dv + "_castigo.pdf"))
                        {
                            File.Delete(ubicacionRut + "\\" + lst[i].Rut + lst[i].Dv + "_castigo.pdf");

                        }
                        if (System.IO.File.Exists(ubicacionRut + "\\" + lst[i].Rut + lst[i].Dv + "_resumen.pdf"))
                        {
                            File.Delete(ubicacionRut + "\\" + lst[i].Rut + lst[i].Dv + "_resumen.pdf");

                        }
                        document.Pages.Remove(document.Pages.Count - 1);
                        documentx3 = PdfDocument.FromFile(rutFile);
                        //document.AddDocument(documentx3);

                        //if (counter % 10 == 0)
                        //{
                        //    pdfBuffer = document.WriteToMemory();

                        //    testFile = ubicacion + "\\" + "castigo_masivo_imprimible_"+counter.ToString()+".pdf";
                        //    System.IO.File.WriteAllBytes(testFile, pdfBuffer);
                        //    document.Close();
                        //    document = new PdfDocument();
                        //    document.SerialNumber = "5q6Pt7aC-gKqPhJSH-lJ/X1sjW-xtfG1Mbf-397G1dfI-19TI39/f-3w==";
                        //}
                        //counter++;
                    }
                    catch (Exception ex)
                    {
                        Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "GeneraPDFporRutComplementariaMarzo2018: ", 309);
                    }
                }

                // write the PDF document to a memory buffer
                //byte[] pdfBuffer = document.WriteToMemory();
                //ubicacion = "d:\\";
                testFile = ubicacion + "\\" + "castigo_masivo_imprimible_" + lst.Count.ToString() + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".pdf";
                //System.IO.File.WriteAllBytes(testFile, pdfBuffer);

            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "GeneraPDFporRutComplementariaMarzo2018: ", 309);
            }
            finally
            {
                document.Close();
            }

        }

    }
}
