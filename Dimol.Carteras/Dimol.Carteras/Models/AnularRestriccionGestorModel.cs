using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Dimol.Carteras.Models
{
    public class AnularRestriccionGestorModel
    {
        [Required]
        [DisplayName("Id Usuario")]
        public int Usrid { get; set; }
        [Required]
        [DisplayName("Id Sucursal")]
        public string Sucid { get; set; }
        [DisplayName("Id Gestor")]
        [Required]
        public int Gesid { get; set; }
        [DisplayName("Fecha Desde")]
        [Required]
        public DateTime Desde { get; set; }
        [DisplayName("Hasta")]
        [Required]
        public DateTime Hasta { get; set; }
        [DisplayName("Autoriza")]
        [StringLength(1024)]
        public string NombreUsuario { get; set; }
        [DisplayName("Gestor")]
        [StringLength(1024)]
        public string NombreGestor { get; set; }
        
    }
}