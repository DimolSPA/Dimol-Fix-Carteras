using System;

namespace Dimol.Reportes.dto
{
    public class DeudorDocumento
    {
        public string RutCliente { get; set; }
        public string Pclid { get; set; }
        public string NombreCliente { get; set; }
        public string Rut { get; set; }
        public string Ctcid { get; set; }
        public string Ccbid { get; set; }
        public string NombreFantasia { get; set; }
        public string TipoDocumento { get; set; }
        public string Numero { get; set; }
        public DateTime FechaDocumento { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string EstadoCpbt { get; set; }
        public string Estado { get; set; }
        public decimal Monto { get; set; }
        public decimal Saldo { get; set; }
        public string Moneda { get; set; }
    }
}