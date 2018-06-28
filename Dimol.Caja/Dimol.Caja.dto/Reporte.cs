using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Caja.dto
{
    public class Reporte
    {
    }
    public class ReporteCabecera
    {
        public string NumComprobante { get; set; }
        public DateTime FecDoc { get; set; }
        public string RutCliente { get; set; }
        public string Cliente { get; set; }
        public string RutDeudor { get; set; }
        public string Deudor { get; set; }
        public decimal Monto { get; set; }
        public string Gestor { get; set; }
      
    }
    public class ReporteImputacion
    {
        public DateTime Fecha { get; set; }
        public string TipoDocumento { get; set; }
        public decimal Capital { get; set; }
        public decimal Interes { get; set; }
        public decimal Honorario { get; set; }
        public decimal GastoPreJudicial { get; set; }
        public decimal GastoJudicial { get; set; }
    }
    public class ReporteImputacionDetail
    {
        public DateTime Fecha { get; set; }
        public string TipoDocumento { get; set; }
        public string NumDocumento { get; set; }
        public decimal Capital { get; set; }
        public decimal Interes { get; set; }
        public decimal Honorario { get; set; }
        public decimal GastoPreJudicial { get; set; }
        public decimal GastoJudicial { get; set; }
        public decimal Total { get; set; }
    }
}
