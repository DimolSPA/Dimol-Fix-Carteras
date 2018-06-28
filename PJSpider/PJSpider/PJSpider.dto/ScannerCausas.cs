using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PJSpider.dto
{
    public class ScannerCausas
    {
        public int RolCausa { get; set; }
        public int RolAnio { get; set; }
        public int CodigoTribunal { get; set; }
        public string NombreTribunal { get; set; }
        public string TipoCausa { get; set; }
        public string Url { get; set; }
        //Datos extraidos del poder judicial
        public int TipoConsulta { get; set; }
        public int TipoCuaderno { get; set; }
        public int IdCuaderno { get; set; }
        public int IdCausa { get; set; }
        public int TipoInforme { get; set; }

        public DateTime FechaUltHistorial { get; set; }
        public DateTime FechaUltReceptor { get; set; }

        public string RutaDemanda { get; set; }

        public DateTime FechaIngreso { get; set; }

        public List<TablaLitigantes> Litigantes = new List<TablaLitigantes>();
    }
}
