using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Finanzas.dto
{
    public class ClausulaContratoCartera
    {
        public int clc_clcid { get; set; }
        public string clc_nombre { get; set; }
        public string tipo { get; set; }
        public int clc_tipo { get; set; }
        public string Area { get; set; }
        public string TipoPorId { get; set; }
        public string clc_porcmon { get; set; }
        public string TipoAplicacionPorId { get; set; }
        public decimal clc_valor { get; set; }
        public bool clc_rango { get; set; }
        public bool ValorFijo { get; set; }
        public bool Capital { get; set; }
        public bool Interes { get; set; }
        public bool Honorario { get; set; }
        public bool GastoPrejudicial { get; set; }
        public bool GastoJudicial { get; set; }
        public bool AnulaMaximaConvencional { get; set; }
        public bool Clonar { get; set; }
        public string NombreClonar { get; set; }
        public string clc_codmon { get; set; }
        public string TipoRango { get; set; }
        public string tip_rango { get; set; }
    }
}
