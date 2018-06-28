using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Dimol.Caja.Models
{
    public class BusquedaConciliacionAprobadoModel
    {
        [DisplayName("Cliente")]
        [StringLength(1024)]
        public string NombreRutCliente { get; set; }
        [DisplayName("pclid")]
        [StringLength(20)]
        public string Pclid { get; set; }

        [DisplayName("Fecha")]
        [StringLength(10)]
        public DateTime? FechaBusquedaConcilia { get; set; }

        [DisplayName("Número Comprobante")]
        [RegularExpression(@"[0-9]*", ErrorMessage = "Debe ingresar un número.")]
        public string NumeroComprobante { get; set; }

        [DisplayName("Deudor")]
        [StringLength(1024)]
        public string NombreRutDeudorSearch { get; set; }
        [DisplayName("ctcid")]
        [StringLength(20)]
        public string CtcidSearch { get; set; }
    }
}