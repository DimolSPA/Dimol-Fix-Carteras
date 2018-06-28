using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Empresa.dto
{
    public class Empresa
    {
        public int CodEmp { get; set; }
        public string Rut { get; set; }
        public string Nombre { get; set; }
        public string RutRepresentanteLegal { get; set; }
        public string NombreRepresentanteLegal { get; set; }
        public string Giro { get; set; }
        public string Logo { get; set; }
    }
}
