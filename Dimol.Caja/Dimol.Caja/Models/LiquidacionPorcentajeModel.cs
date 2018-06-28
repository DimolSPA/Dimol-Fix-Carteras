using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Dimol.Caja.Models
{
    public class LiquidacionPorcentajeModel
    {
        [DisplayName("Comprobante")]
        public string NumComprobante { get; set; }

        [DisplayName("Rut Cliente")]
        public string RutCliente { get; set; }

        [DisplayName("Cliente")]
        public string Cliente { get; set; }

        [DisplayName("Rut Deudor")]
        public string RutDeudor { get; set; }

        [DisplayName("Deudor")]
        public string Deudor { get; set; }

        [DisplayName("Monto")]
        public string Monto { get; set; }

        [DisplayName("Monto Rebajado")]
        public string Saldo { get; set; }

        [DisplayName("Monto Por Rebajar")]
        public string SaldoRebajar { get; set; }

        [DisplayName("Capital")]
        public string Capital { get; set; }

        [DisplayName("Interes")]
        public string Interes { get; set; }

        [DisplayName("Honorario")]
        public string Honorario { get; set; }

        [DisplayName("GastoPre")]
        public string GastoPre { get; set; }

        [DisplayName("GastoJud")]
        public string GastoJud { get; set; }

        [DisplayName("Capital")]
        public string CapitalPor { get; set; }

        [DisplayName("Interes")]
        public string InteresPor { get; set; }

        [DisplayName("Honorario")]
        public string HonorarioPor { get; set; }

        [DisplayName("Criterio")]
        public string Criterio { get; set; }

        public int pclidLiqui { get; set; }
        public int ctcidLiqui { get; set; }
        public int conciliacionId { get; set; }
        public string EstadoLiquidacionId { get; set; }
        public string Docs { get; set; }
    }
}