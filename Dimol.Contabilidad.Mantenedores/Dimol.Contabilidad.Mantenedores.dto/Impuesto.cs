using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Contabilidad.Mantenedores.dto
{
    public class Impuesto
    {
        public int Codemp { get; set; }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string NombreCorto { get; set; }
        public int idPlanDeCuentas { get; set; }
        public string NombreCC { get; set; }
        public bool Retenido { get; set; }
        public decimal Monto { get; set; }
        
    }
}
