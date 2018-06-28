using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Contabilidad.Mantenedores.dto
{
    public class PeriodoContable
    {
        public int Codemp { get; set; }
        public int Ano { get; set; }
        public bool Habilitado { get; set; }
        public bool Finalizado { get; set; }
        public int IdPeriodo { get; set; }
    }
}
