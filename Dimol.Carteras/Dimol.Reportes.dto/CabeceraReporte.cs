using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Xml.Serialization;

namespace Dimol.Reportes.dto
{
    [Serializable]
    public class CabeceraReporte
    {
        public string Rut { get; set; }
        public string  Nombre { get; set; }
        public string RutRepresentanteLegal { get; set; }
        public string NombreRepresentanteLegal { get; set; }
        public string Giro { get; set; }
        [XmlIgnore]
        public byte[] LogoArray { get; set; }
        [XmlIgnore]
        public Image  Logo { get; set; }
        public string Sucursal { get; set; }
        public string Pais { get; set; }
        public string Region { get; set; }
        public string Ciudad { get; set; }
        public string Comuna { get; set; }
        public int CodigoPais { get; set; }
        public int CodigoArea { get; set; }
        public int CodigoPostal { get; set; }
        public string Direccion { get; set; }
        public int Telefono { get; set; }
        public int Fax { get; set; }
        public string Email { get; set; }
        public int Codemp { get; set; }
        public int Codsuc { get; set; }
    }
}
