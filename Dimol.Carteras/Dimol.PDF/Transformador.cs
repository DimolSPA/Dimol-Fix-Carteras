using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Dimol.PDF
{
    public class Transformador
    {
        public void TransformXml(object data, string xslFileName)
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
                // return sr.ReadToEnd();

                System.IO.File.WriteAllText(@"D:\path.fo", sr.ReadToEnd());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void TransformObject(XmlDocument xd, string xslFileName, string foOutput)
        {
            try
            {
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
                throw ex;
            }
        }
    }
}
