using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Empresa.dto
{
    public class Moneda
    {
        public int CodEmp { get; set; }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Simbolo { get; set; }
        public string MonedaDefault { get; set; }
        public string Porcentaje { get; set; }
        public int Decimales { get; set; }
    }
}
