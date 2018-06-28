using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Finanzas.dto
{
    public class MaximaConvencional
    {
        public int MXC_CODEMP { get; set; }
        public int MXC_MXCID { get; set; }
        public int MXC_TPCID { get; set; }
        public string MXC_TIPO { get; set; }
        public string MXC_APLICA { get; set; }
        public int MXC_CODMON { get; set; }
        public decimal MXC_DESDE { get; set; }
        public decimal MXC_HASTA { get; set; }
        public decimal MXC_VALOR { get; set; }
        public string TCI_NOMBRE { get; set; }

    }
}
