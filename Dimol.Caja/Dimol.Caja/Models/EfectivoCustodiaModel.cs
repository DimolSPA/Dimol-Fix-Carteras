using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Dimol.Caja.Models
{
    public class EfectivoCustodiaModel
    {
        [DisplayName("Cliente")]
        [StringLength(1024)]
        public string NombreRutClienteEfectivo { get; set; }
        [DisplayName("pclid")]
        [StringLength(20)]
        public string PclidEfectivo { get; set; }

        [DisplayName("Deudor")]
        [StringLength(1024)]
        public string NombreRutDeudorEfectivo { get; set; }
        [DisplayName("ctcid")]
        [StringLength(20)]
        public string CtcidEfectivo { get; set; }

        [DisplayName("Gestor")]
        [StringLength(1024)]
        public string NombreRutGestorEfectivo { get; set; }
        [DisplayName("gesid")]
        [StringLength(20)]
        public string GesidEfectivo { get; set; }

        [DisplayName("Fecha Ingreso")]
        [StringLength(10)]
        public DateTime? FechaIngreso { get; set; }

        [DisplayName("Monto")]
        public string MontoIngreso { get; set; }
    }
}