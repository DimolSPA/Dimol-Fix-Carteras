using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Dimol.Judicial.Mantenedores.Models
{
    public class AgregarAvanceDemandaModel
    {
       
        [DisplayName("Encargado de Confección")]
        [StringLength(1024)]
        public string NombreUsuario { get; set; }
        [DisplayName("usrid")]
        [StringLength(20)]
        public string usrid { get; set; }
        [DisplayName("Correccion en la Demanda")]
        public bool EnviaFechaEntrega { get; set; }
        public int GetPerfil { get; set; }
        public int GetUsuario { get; set; }
    }
}