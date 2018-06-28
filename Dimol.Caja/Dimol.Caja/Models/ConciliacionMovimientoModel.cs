using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Dimol.Caja.Models
{
    public class ConciliacionMovimientoModel
    {
        public string MovimientoId { get; set; }
        [DisplayName("Cliente")]
        [StringLength(1024)]
        public string NombreRutCliente { get; set; }
        [DisplayName("pclid")]
        [StringLength(20)]
        public string Pclid { get; set; }

        [DisplayName("Deudor")]
        [StringLength(1024)]
        public string NombreRutDeudor { get; set; }
        [DisplayName("ctcid")]
        [StringLength(20)]
        public string Ctcid { get; set; }

        [DisplayName("Gestor")]
        [StringLength(1024)]
        public string NombreRutGestor { get; set; }
        [DisplayName("gesid")]
        [StringLength(20)]
        public string Gesid { get; set; }

        [DisplayName("Fecha")]
        [StringLength(10)]
        public DateTime? Fecha { get; set; }

        [DisplayName("Motivo")]
        public string Motivo { get; set; }

        [DisplayName("Sucursal")]
        public string Sucursal { get; set; }

        [DisplayName("Monto")]
        public string Monto { get; set; }

        [DisplayName("Tipo de Conciliación")]
        public string TipoConciliacion { get; set; }

        [DisplayName("Nº Comprobante")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Debe ingresar un número.")]
        public string NumeroComprobante { get; set; }
    }
}