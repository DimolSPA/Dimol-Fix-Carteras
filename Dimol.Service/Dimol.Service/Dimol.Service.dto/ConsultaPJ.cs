using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Service.dto
{
    public class ConsultaPJ
    {
        public string Tipo { get; set; }
        public int Numero { get; set; }
        public int Anio { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string Tribunal { get; set; }
        public string Demandante { get; set; }
        public string Demandado { get; set; }
        public string RutaDemanda { get; set; }
        public string Url { get; set; }
    }
}
