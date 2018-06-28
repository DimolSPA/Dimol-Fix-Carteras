using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Judicial.Mantenedores.dto
{
    public class Demandado
    {
        public int Codemp { get; set; }
        public int Rolid { get; set; }
        public string  Rut { get; set; }
        public string Nombre { get; set; }
        public string RepresentanteLegal { get; set; }
    }
}
