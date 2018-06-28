using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Dimol.Tesoreria.Models
{
    public class BuscarCajaModel
    {
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
        [DisplayName("Tipo Documento")]
        [StringLength(20)]
        public string TipoDocumento { get; set; }
        [DisplayName("Tipo")]
        [StringLength(20)]
        public string Tipo { get; set; }
        [DisplayName("Empleado")]
        [StringLength(20)]
        public string Empleado { get; set; }

        [DisplayName("Número Documento")]
        [StringLength(20)]
        public string Numero { get; set; }

        [DisplayName("Monto Desde")]
        [RegularExpression(@"[0-9]*\,?[0-9]+", ErrorMessage = "Monto debe ser un número.")]
        public string MontoDesde { get; set; }
        [DisplayName("Hasta")]
        [RegularExpression(@"[0-9]*\,?[0-9]+", ErrorMessage = "Monto debe ser un número.")]
        public string MontoHasta { get; set; }
    }
}
