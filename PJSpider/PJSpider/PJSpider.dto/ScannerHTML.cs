using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PJSpider.dto
{
    public class ScannerHTML
    {
        public int IdCausa { get; set; }
        public int IdCuaderno { get; set; }
        public string TipoCausa { get; set; }
        public int Rol { get; set; }
        public int Anio { get; set; }
        public string NombreTribunal { get; set; }
        public int Tribunal { get; set; }
        public string Url { get; set; }
        public string Estado { get; set; }
        public string HTML { get; set; }

        //agregado para super bot
        public int Codemp { get; set; }
        public int Rolid { get; set; }

    }
}
