using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace Dimol.Carteras.Models
{
    public class SubCarteraModel
    {
        [DisplayName("sbcid")]
        [StringLength(20)]
        public string Sbcid { get; set; }
        [Required]
        [DisplayName("RUT")]
        [StringLength(9)]
        public string Rut { get; set; }
        [Required]
        [DisplayName("Nombre")]
        [StringLength(1024)]
        public string Nombre { get; set; }
        [DisplayName("Teléfono")]
        [StringLength(1024)]
        public string Telefono { get; set; }
        [DisplayName("Dirección")]
        [StringLength(1024)]
        [Required]
        public string Direccion { get; set; }

        [DisplayName("Región")]
        [Required]
        public int Region { get; set; }
        [DisplayName("Ciudad")]
        [Required]
        public int Ciudad { get; set; }
        [DisplayName("País")]
        [Required]
        public int Pais { get; set; }
        [DisplayName("Comuna")]
        [Required]
        public int Comuna { get; set; }

    }
}