using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Judicial.Mantenedores.dto
{
    public class Mandatos
    {
        public int Codemp { get; set; }
        public int IdCliente { get; set; }
        public int IdNotaria { get; set; }
        public string NumeroRepertorio { get; set; }
        public string RutCliente { get; set; }
        public string NombreCliente { get; set; }
        public string NombreNotaria { get; set; }
        public string FechaAsignacion { get; set; }
        public string FechaVencimiento { get; set; }
        public string Indefinido { get; set; }
    }
}
