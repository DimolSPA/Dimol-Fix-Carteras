using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Contabilidad.Mantenedores.dto
{
    public class FormasDePago
    {
        public int Codemp { get; set; }
        public int Idioma { get; set; }
        public int IdFP { get; set; }
        public string Nombre { get; set; }
        public int DiasVenc { get; set; }
        public bool IngFV { get; set; }
        public bool IngCuotas { get; set; }
        public string Tipo { get; set; }
    }
}
