using System;
using System.ComponentModel;

namespace Dimol.Reportes.Models
{
    public class EmailMasivoModel
    {
        [DisplayName("Tipo de cartera")]
        public int TipoCartera { get; set; }

        public string Pclid { get; set; }

        public string NombreRutCliente { get; set; }

        [DisplayName("Fecha de vencimiento")]
        public DateTime FechaVencimiento { get; set; }

        public int FechaOperador { get; set; }

        [DisplayName("¿Con liquidación?")]
        public bool Liquidacion { get; set; } 

        [DisplayName("Template")]
        public string Template { get; set; }

        public string FiltersPattern { get; set; }

        public int TipoLiquidacion { get; set; }

        public decimal MontoDesde { get; set; }

        public decimal MontoHasta { get; set; }

        public string Ctcid { get; set; }

        public string Estados { get; set; }

        public string Gestores { get; set; }

        public bool Test { get; set; }
    }

}