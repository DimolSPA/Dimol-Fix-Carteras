using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Contabilidad.Mantenedores.dto
{
    public class MonedaValor
    {
        public int Codemp { get; set; }
        public string Nombre { get; set; }
        public DateTime Fecha { get; set; }
        public double Valor { get; set; }
        public int codMoneda { get; set; }
        public int Id { get; set; }
    }
}
