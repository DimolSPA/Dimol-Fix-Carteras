using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PJSpider.dto
{
    public class RolActualizar
    {
        public int Codemp { get; set; }
        public int Rolid { get; set; }
        public string Numero { get; set; }
        public int Rol { get; set; }
        public int Anio { get; set; }
        public string TipoCausa { get; set; }
        public string Tribunal { get; set; }
        public int IdCausa { get; set; }
        public DateTime  FechaUltHistorial { get; set; }
        public DateTime FechaUltReceptor { get; set; }
        public int IdTribunal { get; set; }
        public string UrlHTML { get; set; }
        public int IdCuaderno { get; set; }

        public string HTML { get; set; }
        public Dictionary<int,string> Cuaderno { get; set; }
        public string Url { get; set; }
        public int TipoCuaderno { get; set; }
        public string Estado { get; set; }
    }
}
