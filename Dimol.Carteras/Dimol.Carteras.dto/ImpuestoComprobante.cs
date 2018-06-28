using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Carteras.dto
{
    public class ImpuestoComprobante
    {
        public string Nombre { get; set; }
        public string Retenido { get; set; }
        public decimal Porcentaje { get; set; }
    }
}
