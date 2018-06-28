using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Dimol.Caja.Models
{
    public class ConciliacionCustodiaModel
    {
        public string CustodiaId { get; set; }
        [DisplayName("Cliente")]
        [StringLength(1024)]
        public string NombreRutClienteCC { get; set; }
        [DisplayName("pclid")]
        [StringLength(20)]
        public string PclidCC { get; set; }

        [DisplayName("Deudor")]
        [StringLength(1024)]
        public string NombreRutDeudorCC { get; set; }
        [DisplayName("ctcid")]
        [StringLength(20)]
        public string CtcidCC { get; set; }

        [DisplayName("Gestor")]
        [StringLength(1024)]
        public string NombreRutGestorCC { get; set; }
        [DisplayName("gesid")]
        [StringLength(20)]
        public string GesidCC { get; set; }

        [DisplayName("Monto")]
        public string MontoDocumento { get; set; }

        [DisplayName("Nº de Documento")]
        public string NumeroDocumento { get; set; }

        [DisplayName("Fecha Doc.")]
        [StringLength(10)]
        public DateTime? FechaDocumento { get; set; }

        [DisplayName("Fecha Prorroga")]
        [StringLength(10)]
        public DateTime? FechaProrroga { get; set; }

        [DisplayName("Estado")]
        public string EstadoDocumento { get; set; }

        [DisplayName("Tipo de Banco")]
        public string Banco { get; set; }

        [DisplayName("Girado a")]
        public string GiroA { get; set; }

        [DisplayName("Tipo de Conciliación")]
        public string TipoConciliacion { get; set; }

        [DisplayName("Nº Comprobante")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Debe ingresar un número.")]
        public string NumComprobanteDoc { get; set; }


        [DisplayName("Cuenta Bancaria")]
        public string Cuenta { get; set; }
    }
}