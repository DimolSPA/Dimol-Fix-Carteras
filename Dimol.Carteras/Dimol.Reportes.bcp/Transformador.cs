using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Dimol.Reportes.bcp
{
    public class Transformador
    {
        public void TransformXml(object data, string xslFileName,string foOutput)
        {
            try
            {
                XmlSerializer xs = new XmlSerializer(data.GetType());
                string xmlString;

                using (StringWriter swr = new StringWriter())
                {
                    xs.Serialize(swr, data);
                    xmlString = swr.ToString().Replace("&#x1A;", " ").Replace("&#x1B;", " ").Replace("&#x1C;", " ").Replace("&#x1D;", " ").Replace("&#x1E;", " ").Replace("&#x1F;", " ");
                }

                var xd = new XmlDocument();
                xd.LoadXml(xmlString);

                var xslt = new System.Xml.Xsl.XslCompiledTransform();
                xslt.Load(xslFileName);

                var stm = new MemoryStream();
                xslt.Transform(xd, null, stm);
                stm.Position = 0;
                var sr = new StreamReader(stm);

                System.IO.File.WriteAllText(foOutput, sr.ReadToEnd());
            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace,"Reportes.bcp.Transformador", 0);
                throw ex;
            }
        }

        public string TransformXLS(object data, string xslFileName)
        {
            try
            {
                XmlSerializer xs = new XmlSerializer(data.GetType());
                string xmlString;
                using (StringWriter swr = new StringWriter())
                {
                    xs.Serialize(swr, data);
                    xmlString = swr.ToString();
                }

                var xd = new XmlDocument();
                xd.LoadXml(xmlString);

                var xslt = new System.Xml.Xsl.XslCompiledTransform();
                xslt.Load(xslFileName);
                var stm = new MemoryStream();
                xslt.Transform(xd, null, stm);
                stm.Position = 0;
                var sr = new StreamReader(stm);
                
                return sr.ReadToEnd();
            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Reportes.bcp.Transformador Excel", 0);
                throw ex;
            }
        }

        public string TransformXSLToHTML(object data, string template)
        {
            try
            {
                XmlSerializer xs = new XmlSerializer(data.GetType());
                string xmlString;
                string strXSLT = template;
                using (StringWriter swr = new StringWriter())
                {
                    xs.Serialize(swr, data);
                    xmlString = swr.ToString();
                }

                var xd = new XmlDocument();
                xd.LoadXml(xmlString);

                var xslt = new System.Xml.Xsl.XslCompiledTransform();
                xslt.Load(new XmlTextReader(new StringReader(strXSLT)));
                var stm = new MemoryStream();
                xslt.Transform(xd, null, stm);
                stm.Position = 0;
                var sr = new StreamReader(stm);
                
                return sr.ReadToEnd();
            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Reportes.bcp.Transformador Excel", 0);
                throw ex;
            }
        }
    }
}