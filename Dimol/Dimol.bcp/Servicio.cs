using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Dimol.bcp
{
    public class Servicio
    {
        public static XmlDocument IndicadoresDiarios()
        {
            string url = @"http://www.indicadoresdeldia.cl/webservice/indicadores.xml";
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.Load(url);

            }
            catch (Exception ex)
            {
            }
            return xmlDoc;
        }

        public static string DescargaPagina(string fileName)
        {

            string sContents = string.Empty;
            if (fileName.ToLower().IndexOf("http:") > -1)
            {
                // URL
                System.Net.WebClient wc = new System.Net.WebClient();
                byte[] response = wc.DownloadData(fileName);
                sContents = System.Text.Encoding.ASCII.GetString(response);
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
