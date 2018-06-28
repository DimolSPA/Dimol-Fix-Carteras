using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dimol.Slider.Models
{
    public class GestorCentralModel
    {
        [DisplayName("Anexo")]
        public int Anexo { get; set; }
        [DisplayName("Nombre")]
        [StringLength(100)]
        public string Nombre { get; set; }
        [DisplayName("Cartera")]
        [StringLength(100)]
        public string Cartera { get; set; }
        [DisplayName("Sucursal")]
        [StringLength(100)]
        public string Sucursal { get; set; }

        [DisplayName("Disponible")]
        [StringLength(100)]
        public string Disponible { get; set; }

        public bool Existe { get; set; }
    }
}