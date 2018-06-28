using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dimol.Carteras.Models
{
    public class MoverCarteraModel
    {
        [DisplayName("Cliente")]
        [StringLength(500)]
        public string NombreRutCliente { get; set; }
        [DisplayName("pclid")]
        [StringLength(20)]
        public string Pclid { get; set; }
        [Required]
        [DisplayName("Deudor")]
        [StringLength(500)]
        public string NombreRutDeudor { get; set; }
        [DisplayName("ctcid")]
        [StringLength(20)]
        public string Ctcid { get; set; }
        [Required]
        [DisplayName("Comentario")]
        [StringLength(1024)]
        public string Comentario { get; set; }

        [Required]
        [DisplayName("Estado")]
        public string Estado { get; set; }
        public string Ids { get; set; }

        [Required]
        [DisplayName("Estado Documento")]
        public string EstadoDocumento { get; set; }

    }
}