using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Reportes.dto
{
    [Serializable]
    public class TituloReporte
    {
        public string Cliente { get; set; }
        public string RutCliente { get; set; }
        public string Deudor { get; set; }
        public string RutDeudor { get; set; }
    }
}
