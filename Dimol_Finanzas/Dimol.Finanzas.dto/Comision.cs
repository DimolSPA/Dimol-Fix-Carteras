using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Finanzas.dto
{
    public class Comision
    {
        public int Codemp { get; set; }
        public int CodSuc { get; set; }
        public int cms_anio { get; set; }
        public int cms_mes { get; set; }
        public string pcl_nomfant { get; set; }
        public string tci_nombre { get; set; }
        public string ddi_numcta { get; set; }
        public string FecCanc { get; set; }
        public decimal Capital { get; set; }
        public decimal Interes { get; set; }
        public decimal Honorario { get; set; }
        public decimal Total { get; set; }
        public decimal PorFact { get; set; }
        public string ctc_rut { get; set; }
        public string ctc_nomfant { get; set; }
        public string ges_nombre { get; set; }
        public decimal ComTotal { get; set; }
        
    }
}
