using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Dimol.Reportes.dto;
using Dimol.Reportes.dao;

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
                    xmlString = swr.ToString().Replace("&#x1A;", " ").Replace("&#x1C;", " ");
                }

                var xd = new XmlDocument();
                xd.LoadXml(xmlString);

                var xslt = new System.Xml.Xsl.XslCompiledTransform();
                xslt.Load(xslFileName);
                var stm = new MemoryStream();
                xslt.Transform(xd, null, stm);
                stm.Position = 0;
                var sr = new StreamReader(stm);
                //xtr.Close();
                // return sr.ReadToEnd();

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
                //xtr.Close();
                return sr.ReadToEnd();

                //System.IO.File.WriteAllText(output, sr.ReadToEnd());
            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Reportes.bcp.Transformador Excel", 0);
                throw ex;
            }
        }
    }
}
