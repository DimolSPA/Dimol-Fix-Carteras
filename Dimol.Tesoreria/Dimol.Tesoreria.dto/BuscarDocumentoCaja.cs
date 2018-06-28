using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Tesoreria.dto
{
    public class BuscarDocumentoCaja
    {
        public int Anio { get; set; }
        public string NumeroDocumento { get; set; }
        public string TipoMovimiento { get; set; }
        public string TipoDocumento { get; set; }
        public decimal Monto { get; set; }
        public string NombreCliente { get; set; }
        public string NombreDeudor { get; set; }
        public string NumeroCuenta { get; set; }
        public string Empleado { get; set; }
        public string Estado { get; set; }

    }
}
