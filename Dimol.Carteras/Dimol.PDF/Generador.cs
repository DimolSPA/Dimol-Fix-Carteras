using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using org.apache.fop.apps;
using System.Diagnostics;
using ikvm.lang;
using java.io;
using javax.xml.transform;
using javax.xml.transform.stream;
using javax.xml.transform.sax;


namespace Dimol.PDF
{
    public class Generador
    {
        private string _inputFilePath;
        private string _outputFilePath;

        public Generador()
        {
            _inputFilePath = "";
            _outputFilePath = "";
        }

        public Generador(string input, string output)
        {
            _inputFilePath = input;
            _outputFilePath = output;
        }

        public bool XSLToPDF()
        {
            java.lang.System.setProperty("javax.xml.transform.TransformerFactory", "org.apache.xalan.processor.TransformerFactoryImpl");
            java.lang.System.setProperty("javax.xml.parsers.SAXParserFactory", "org.apache.xerces.jaxp.SAXParserFactoryImpl");
            java.lang.System.setProperty("org.xml.sax.driver", "org.apache.xerces.jaxp.SAXParserFactoryImpl");
            org.apache.xerces.jaxp.SAXParserFactoryImpl sax = new org.apache.xerces.jaxp.SAXParserFactoryImpl();
            java.lang.ClassLoader cl = sax.getClass().getClassLoader();
            java.lang.Thread.currentThread().
            setContextClassLoader(cl);
            String intput = _inputFilePath;
            String output = _outputFilePath;
            File _output = new File(output);
            OutputStream _outputStream = new BufferedOutputStream(new FileOutputStream(_output));
            try
            {
                FopFactory _fopFactory = FopFactory.newInstance();
                Fop _fop = _fopFactory.newFop("application/pdf", _outputStream);
                TransformerFactory _factory = TransformerFactory.newInstance();
                Transformer _transformer = _factory.newTransformer();
                Source _src = new StreamSource(new File(intput));
                Result _res = new SAXResult(_fop.getDefaultHandler());
                _transformer.transform(_src, _res);
                _outputStream.close();
                return true;
            }
            catch (Exception ex)
            {
                _outputStream.close();
                _output.delete();
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Reportes.bcp.Generador", 0);
                return false;
            }
            finally
            {
                //_outputStream.close();
            }
        }
    }
}
