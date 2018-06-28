using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Contabilidad.Mantenedores.dto
{
    public class TiposReporte
    {
        public int Codemp { get; set; }
        public int Idioma { get; set; }
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Tipo { get; set; }
        public string Nombre { get; set; }
        public string Reporte { get; set; }
        public string Agrupa { get; set; }
        public string ReportePadre { get; set; }
        public int IdTiposReporte { get; set; }
    }
}
