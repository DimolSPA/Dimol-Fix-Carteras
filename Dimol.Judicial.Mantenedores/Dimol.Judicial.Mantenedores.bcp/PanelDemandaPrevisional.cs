﻿using HiQPdf;

namespace Dimol.Judicial.Mantenedores.bcp
{
    public class PanelDemandaPrevisional
    {
        #region "Borradores"
        public static Dimol.dto.Combobox HistoriaBorradorDemandasPrevisional(int codemp, int IdDP, int TipoBorrador)
        {
            return dao.PanelDemandaPrevisional.HistoriaBorradorDemandasPrevisional(codemp, IdDP, TipoBorrador);
        }
        #endregion

        #region PDF
        public static byte[] GeneraPDFPorHtml(string Html)
        {
            Html = Html.Replace("<br>", "<br/>").Replace("\n</td>", "")
                            .Replace("</tr>\n</table>", "</table>").Replace("<b/> \n<center>", "\n<center>")
                            .Replace("</center>\n<div", "<div").Replace("</br>", "").Replace("stc.html';\">", "stc.html';\" />")
                            .Replace("font-size: 12px;", "font-size: 14pt;");

            HtmlToPdf htmlToPdfConverter = new HtmlToPdf();
            htmlToPdfConverter.SerialNumber = "5q6Pt7aC-gKqPhJSH-lJ/X1sjW-xtfG1Mbf-397G1dfI-19TI39/f-3w==";
            htmlToPdfConverter.BrowserWidth = 1200;
            htmlToPdfConverter.BrowserHeight = 0;
            htmlToPdfConverter.HtmlLoadedTimeout = 60;
            htmlToPdfConverter.WaitBeforeConvert = 2;
            htmlToPdfConverter.Document.PageSize = PdfPageSize.A4;
            htmlToPdfConverter.Document.PageOrientation = PdfPageOrientation.Portrait;
            htmlToPdfConverter.Document.Margins = new PdfMargins(20);

            //Convert URL to a PDF memory buffer
            string url = "";
            byte[] ByteFile = htmlToPdfConverter.ConvertHtmlToMemory(Html, url);

            return ByteFile;
        }
        #endregion
    }
}
