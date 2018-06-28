using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Carteras.dto
{
    public class BuscarEstadoComprobante
    {
        public int IdTipoDocumento { get; set; }
        public int Numero { get; set; }
        public string Rut { get; set; }
        public string NombreFantasia { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroCliente { get; set; }
        public decimal Bruto { get; set; }
        public decimal Retenido { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
        public string Moneda { get; set; }
        public string Contable { get; set; }
        public string Cartera { get; set; }
        public string FinDeuda { get; set; }
        public decimal TipoCambio { get; set; }
        public DateTime FechaContable { get; set; }
        public string Cliente { get; set; }
        public string Usuario { get; set; }
        public int Pclid { get; set; }
        public int Ctcid { get; set; }
        public int Rolid { get; set; }
    }
}
