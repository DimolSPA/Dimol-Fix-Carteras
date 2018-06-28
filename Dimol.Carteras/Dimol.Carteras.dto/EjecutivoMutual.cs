using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Carteras.dto
{
    public class EjecutivoMutual
    {
        
        public int IdTipoBanco { get; set; }
        public string NombreBanco { get; set; }
        public int IdEjecutivo { get; set; }
        public string NombreEjecutivo { get; set; }
        public string Email { get; set; }
        public string Oficina { get; set; }
        public int IdCuentaEjecutivo { get; set; }
        public string Cuenta { get; set; }
    }
}
