using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Caja.dto
{
    public class CriterioImputacion
    {
        public int Pclid { get; set; }
        public string Cliente { get; set; }
        public int Capital { get; set; }
        public int Interes { get; set; }
        public int Honorario { get; set; }
        public int Row { get; set; }
    }
}
