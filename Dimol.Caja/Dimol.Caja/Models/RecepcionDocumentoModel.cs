using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Dimol.Caja.Models
{
    public class RecepcionDocumentoModel
    {
        public string DocumentoId { get; set; }
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

        [DisplayName("Asegurado")]
        [StringLength(1024)]
        public string NombreRutAsegurado { get; set; }
        [DisplayName("sbcid")]
        [StringLength(20)]
        public string Sbcid { get; set; }

        [DisplayName("Fecha Ingreso")]
        [StringLength(10)]
        public DateTime? FechaIngreso { get; set; }

        [DisplayName("REC")]
        //[RegularExpression(@"^\d+$", ErrorMessage = "Debe ingresar un número.")]
        public string NumeroDocumento { get; set; }

        [DisplayName("Moneda")]
        [StringLength(20)]
        public string Moneda { get; set; }

        [DisplayName("Monto del Documento")]
        public string MontoIngreso { get; set; }
    }
}