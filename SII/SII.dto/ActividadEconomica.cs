using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SII.dto
{
    public class ActividadEconomica
    {
        public int IdRut { get; set; }
        public int Ctcid { get; set; }
        public string CodigoActividad { get; set; }
        public DateTime FechaConsulta { get; set; }
    }
}
