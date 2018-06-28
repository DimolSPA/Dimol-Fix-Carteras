using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Contabilidad.Mantenedores.dto
{
    public class Talonario
    {
        public int Codemp { get; set; }
        public int tac_tacid { get; set; }
        public string tac_nombre { get; set; }
        public int tac_numero { get; set; }
        public bool tpc_talonario { get; set; }
    }
}
