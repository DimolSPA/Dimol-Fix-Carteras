using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Contabilidad.Mantenedores.dto
{
    public class PeriodoContableMes
    {
        public int Codemp { get; set; }
        public int Ano { get; set; }
        public string Mes { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fin { get; set; }
        public int APEInicio { get; set; }
        public int APEFin { get; set; }
        public int INGInicio { get; set; }
        public int INGFin { get; set; }
        public int EGREInicio { get; set; }
        public int EGREFin { get; set; }
        public int TRASInicio { get; set; }
        public int TRASFin { get; set; }
        public bool Habilitado { get; set; }
        public bool Finalizado { get; set; }
        public int IdPeriodoMensual { get; set; }


    }
}
