using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Carteras.dto
{
    public class Recordatorio
    {
        public int Codemp { get; set; }
        public int Ctcid { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public DateTime FechaEnvio { get; set; }
        public int Usrid { get; set; }
        public string Estado { get; set; }
        public string Tipo { get; set; }

        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
    }

}
