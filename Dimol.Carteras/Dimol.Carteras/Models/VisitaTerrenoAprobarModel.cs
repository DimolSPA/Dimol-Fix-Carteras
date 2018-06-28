using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Dimol.Carteras.Models
{
    public class VisitaTerrenoAprobarModel
    {
        [DisplayName("Cliente")]
        [StringLength(9)]
        public string RutCliente { get; set; }

        [DisplayName("Cliente")]
        public string NombreRutCliente { get; set; }

        [DisplayName("pclid")]
        [StringLength(20)]
        public string Pclid { get; set; }

        [DisplayName("Nombre Cliente")]
        [StringLength(1024)]
        public string NombreCliente { get; set; }

        [DisplayName("Pais")]
        public string Pais { get; set; }

        [DisplayName("Region")]
        public string Region { get; set; }

        [DisplayName("Ciudad")]
        public string Ciudad { get; set; }

        [DisplayName("Comuna")]
        public string Comuna { get; set; }

        [DisplayName("Monto mayor a")]
        public decimal Monto { get; set; }

        [DisplayName("En Liquidación")]
        public bool Quiebra { get; set; }

        [DisplayName("En Proceso de Quiebra")]
        public bool PreQuiebra { get; set; }

        [DisplayName("Visitas Solicitadas")]
        public bool Solicitud { get; set; }


        [DisplayName("Gestores de Terreno")]
        public string lstGestoresTerreno { get; set; }
    }
}