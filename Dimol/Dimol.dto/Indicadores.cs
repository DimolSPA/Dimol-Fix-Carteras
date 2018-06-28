using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.dto
{
    public class Indicadores
    {
        public decimal UF { get; set; }
        public decimal UTM { get; set; }
        public decimal DolarObservado { get; set; }
        public decimal IPC { get; set; }
        public string EstadoUF { get; set; }
        public string EstadoDolar { get; set; }
    }
}
