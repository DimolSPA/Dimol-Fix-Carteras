using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Dimol.Caja.Models
{
    public class RemesaModel
    {
        [DisplayName("Cliente")]
        [StringLength(1024)]
        public string NombreRutCliente { get; set; }
        [DisplayName("pclid")]
        [StringLength(20)]
        public string Pclid { get; set; }

        public string IdsConciliacion { get; set; }
    }
}