using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Judicial.Mantenedores.dto
{
    public class DemandaAvenimiento
    {
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        public int Cuotas { get; set; }
        public decimal MontoCuota { get; set; }
        public decimal MontoUltimaCuota { get; set; }
        public DateTime FechaPrimeraCuota { get; set; }
        public DateTime FechaUltimaCuota { get; set; }
        public decimal Interes { get; set; }

    }
}
