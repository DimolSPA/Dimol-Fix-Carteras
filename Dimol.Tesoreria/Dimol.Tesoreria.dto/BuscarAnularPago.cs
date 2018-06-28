using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Tesoreria.dto
{
    public class BuscarAnularPago
    {
        public int Anio { get; set; } 
        public int NumeroAplicacion { get; set; }
        public int Item { get; set; }
        public string NombreCliente { get; set; }
        public string NombreDeudor { get; set; }
        public string TipoDocumento { get; set; }
        public int NumeroCuenta { get; set; }
        public int NumeroDocumento { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Capital { get; set; }
        public decimal Interes { get; set; }
        public decimal Honorario { get; set; }
        public decimal GastoPrejudicial { get; set; }
        public decimal GastoJudicial { get; set; }
        public decimal Total { get; set; }
        public string Gestor { get; set; }
        public string Vendedor { get; set; }
    }
}
