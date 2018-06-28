using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Dimol.Caja.Models
{
    public class ImputacionPorcentajeModel
    {
        [DisplayName("Comprobante")]
        public string INumComprobante { get; set; }

        [DisplayName("Rut Cliente")]
        public string IRutCliente { get; set; }

        [DisplayName("Cliente")]
        public string ICliente { get; set; }

        [DisplayName("Rut Deudor")]
        public string IRutDeudor { get; set; }

        [DisplayName("Deudor")]
        public string IDeudor { get; set; }

        [DisplayName("Monto")]
        public string IMonto { get; set; }

        [DisplayName("Monto Rebajado")]
        public string ISaldo { get; set; }

        [DisplayName("Monto Por Rebajar")]
        public string ISaldoRebajar { get; set; }

        [DisplayName("Capital")]
        public string ICapital { get; set; }

        [DisplayName("Interes")]
        public string IInteres { get; set; }

        [DisplayName("Honorario")]
        public string IHonorario { get; set; }

        [DisplayName("GastoPre")]
        public string IGastoPre { get; set; }

        [DisplayName("GastoJud")]
        public string IGastoJud { get; set; }

        [DisplayName("Capital")]
        public string ICapitalPor { get; set; }

        [DisplayName("Interes")]
        public string IInteresPor { get; set; }

        [DisplayName("Honorario")]
        public string IHonorarioPor { get; set; }

        [DisplayName("Criterio")]
        public string ICriterio { get; set; }

        public int IpclidLiqui { get; set; }
        public int IctcidLiqui { get; set; }
        public int IConciliacionId { get; set; }
        public string Documentos { get; set; }
        public string DocFinalizar { get; set; }
    }
}