using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItinerarioServicios.dto
{
    public class Proceso
    {
        public int Codemp { get; set; }
        public int ProcesoId { get; set; }
        public string DiaSemana { get; set; }
        public int Dia { get; set; }
        public string Nombre { get; set; }
        public string Servidor { get; set; }
        public string Status { get; set; }
    }
}
