using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Caja.dto
{
    public class CriterioRemesa
    {
        public int Id { get; set; }
        public int? DesdeDiasVencido { get; set; }
        public int? HastaDiasVencido { get; set; }
        public string RegionMetropolitana { get; set; }
        public int? CodigoCarga { get; set; }
        public string CodigoDeCarga { get; set; }
        public string TipoCambioCapital { get; set; }
        public int SimboloId { get; set; }
        public int Capital { get; set; }
        public int Interes { get; set; }
        public int Honorario { get; set; }
        public int TipoConciliacionId { get; set; }
        public string TipoConciliacion { get; set; }
        public int? ConcicionId { get; set; }
        public string CondicionAnticipo { get; set; }
        public string IsAnticipo { get; set; }
        public int Row { get; set; }
    }
}
