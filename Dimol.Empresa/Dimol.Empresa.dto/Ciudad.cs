using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Empresa.dto
{
    public class Ciudad
    {
        public int IdPais { get; set; }
        public string NombrePais { get; set; }
        public int IdRegion { get; set; }
        public string NombreRegion { get; set; }
        public int IdCiudad { get; set; }
        public string NombreCiudad { get; set; }
        public int CodigoArea { get; set; }
    }
}
