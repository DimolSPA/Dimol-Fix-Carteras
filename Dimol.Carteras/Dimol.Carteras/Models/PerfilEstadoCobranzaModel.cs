using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Dimol.Carteras.Models
{
    public class PerfilEstadoCobranzaModel
    {
        [DisplayName("Perfil")]
        public string lstPerfil { get; set; }

        [DisplayName("Tipo Estados")]
        public string lstTipoEstado { get; set; }
    }
}