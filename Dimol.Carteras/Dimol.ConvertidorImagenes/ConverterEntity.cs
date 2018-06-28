using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.ConvertidorImagenes
{
    public class ConverterEntity
    {
        public int Codemp { get; set; }
        public int Pclid { get; set; }
        public int Ctcid { get; set; }
        public int Ccbid { get; set; }
        public int Cdid { get; set; }
        public byte[] ByteArray { get; set; }
        public Image Imagen { set; get; }
        public string NombreArchivoImagen { get; set; }
    }

    public class EmpladoConverterEntity
    {
        public int Codemp { get; set; }
        public int Sucid { get; set; }
        public int Emplid { get; set; }
        public int Usrid { get; set; }
        public byte[] ByteArray { get; set; }
        public Image Imagen { set; get; }
        public string NombreArchivoImagen { get; set; }
    }

}
