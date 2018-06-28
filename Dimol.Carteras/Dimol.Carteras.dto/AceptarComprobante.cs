using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Carteras.dto
{
    public class AceptarComprobante
    {
        public int IdTipoDocumento { get; set; }
        public int Numero { set; get; }
        public DateTime Fecha { get; set; }
        public string FechaContable { get; set; }
        public string Estado { get; set; }
        public decimal Saldo { get; set; }
    }
}
