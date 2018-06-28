using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Carteras.dto
{
    public class RestriccionGestor
    {
        public int Codemp { get; set; }
        public int Usrid { get; set; }
        public int Sucid { get; set; }
        public int Gesid { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public string NombreUsuario { get; set; }
        public string NombreGestor { get; set; }
    }
}
