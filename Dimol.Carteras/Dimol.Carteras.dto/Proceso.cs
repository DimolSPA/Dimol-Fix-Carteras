using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Carteras.dto
{
    public class Proceso
    {
        public int Codemp { get; set; }
        public int IdProceso { get; set; }
        public string Nombre { get; set; }
        public string  Descripcion { get; set; }
        public string Servidor { get; set; }
        public string UsuarioIngreso { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public bool Running { get; set; }
    }
}
