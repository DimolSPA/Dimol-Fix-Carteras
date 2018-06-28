using System;

namespace Dimol.Judicial.Mantenedores.dto
{
    public class PanelDemandaPrevisionalDetalle
    {
        public int PanelId { get; set; }
        public int UsrEncargado { get; set; }
        public DateTime FechaEnvio { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public DateTime? FechaIngresoTribunal { get; set; }

        public string RolAdjudicado { get; set; }
        public int? RolId { get; set; }
        public string Comentarios { get; set; }
        public int UsrIdRegistro { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
