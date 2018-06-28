using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Dimol.Contabilidad.Mantenedores.Models
{

    public class GridModel
    {
        public string  GridSelect { get; set; }
        public string  GridData { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}",
            ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }

    }

    public class TalonarioModel : dto.Talonario
    {
        [DisplayName("Talonario")]
        public string Talonarios { get; set; }

        [DisplayName("Nombre")]
        public string Nombre { get; set; }

        [DisplayName("Ultimo Numero")]
        public string UltimoNumero { get; set; }

       
    }
}