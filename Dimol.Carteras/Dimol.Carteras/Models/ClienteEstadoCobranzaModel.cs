
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace Dimol.Carteras.Models
{
    public class ClienteEstadoCobranzaModel
    {
        [DisplayName("Cliente")]
        [StringLength(1024)]
        public string RutCliente { get; set; }
        [DisplayName("Cliente")]
        public string NombreRutCliente { get; set; }
        [DisplayName("pclid")]
        [StringLength(20)]
        public string Pclid { get; set; }

        [DisplayName("Tipo Estados")]
        public string lstTipoEstado { get; set; }
    }
}