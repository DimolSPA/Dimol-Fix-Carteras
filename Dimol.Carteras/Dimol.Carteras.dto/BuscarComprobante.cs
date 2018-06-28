using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Carteras.dto
{
    public class BuscarComprobante
    {
        public int IdTipoDocumento { get; set; }
        public int Numero { get; set; }
        public string Rut { get; set; }
        public string NombreFantasia { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroCliente { get; set; }
        public decimal Final { get; set; }
        public string IdEstado { get; set; }
        public int NumFin{ get; set; }
        public decimal Saldo { get; set; }
        public string Estado { get; set; }
        public DateTime FechaEmision { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string RutDeudor { get; set; }
        public string NombreDeudor { get; set; }
        public string Rol { get; set; }
        public string Tribunal { get; set; }
    }
}
