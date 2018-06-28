using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Judicial.Mantenedores.dto
{
    public class PanelAvenimiento
    {
        public int RolId { get; set; }
        public int Pclid { get; set; }
        public int Ctcid { get; set; }
        public int TribunalId { get; set; }
        public string Rol { get; set; }
        public string Cliente { get; set; }
        public string Deudor { get; set; }
        public string Tribunal { get; set; }
        public Nullable<System.DateTime> FechaTraspasoAvenimiento { get; set; }
        public int Row { get; set; }
    }
}
