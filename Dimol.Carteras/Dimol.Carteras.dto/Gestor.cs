using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Carteras.dto
{
    public class Gestor
    {
        public string GesId { get; set; }
        public string IdEmpleado { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Estado { get; set; }
        public int IdTipoCartera { get; set; }
        public int IdGrupo { get; set; }
        public string IndRemoto { get; set; }
        public string IndTerreno { get; set; }
        public string TelefonoTerreno { get; set; }
        public string TelefonoImei { get; set; }
    }
}
