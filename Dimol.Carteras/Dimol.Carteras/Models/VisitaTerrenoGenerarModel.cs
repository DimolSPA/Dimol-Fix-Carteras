using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Dimol.Carteras.Models
{
    public class VisitaTerrenoGenerarModel
    {
        [DisplayName("Gestores de Terreno")]
        public string lstGestoresTerreno { get; set; }
        public string passGeoGestion { get; set;}
        public string userGeoGestion{get; set;}
    }
}