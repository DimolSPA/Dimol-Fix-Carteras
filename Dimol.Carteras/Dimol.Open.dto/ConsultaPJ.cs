using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Open.dto
{
    public class ConsultaPJ
    {
        public string Tipo { get; set; }
        public int Numero { get; set; }
        public int Anio { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string Tribunal { get; set; }
        public string Demandante { get; set; }
        public string Demandado { get; set; }
        public string RutaDemanda { get; set; }
        public string Url { get; set; }
    }

    public class UserPJ
    {
        public int Userid { get; set; }
        public string Nombre { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int Pclid { get; set; }
        public string RutCliente { get; set; }
        public int Activa { get; set; }
        public string Admin { get; set; }
        public int Left { get; set; }
        public int Right { get; set; }
    }
}
