using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Dimol.Carteras.Models
{
    public class VisitaTerrenoCarteraGestor
    {
        public string CarteraId { get; set; }

        [DisplayName("Gestor")]
        [Required]
        public string LstGestor { get; set; }

        [DisplayName("Nombre Cartera")]
        [Required]
        public string CarteraNombre { get; set; }

        [DisplayName("Descripcion")]
        public string Descripcion { get; set; }
        public string passGeoGestion { get; set; }
        public string userGeoGestion { get; set; }
    }
}