using System;

namespace Dimol.Judicial.Mantenedores.dto
{
    public class RolCarga
    {
        public int IdCliente { get; set; }
        public string Cliente { get; set; }
        public string RutCliente { get; set; }
        public string TipoRol { get; set; }
        public string Rol { get; set; }
        public int IdTribunal { get; set; }
        public string Tribunal { get; set; }
        public int IdTipoCausa { get; set; }
        public string TipoCausa { get; set; }
        public string RutDeudor { get; set; }
        public string NombreDeudor { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime FechaRol { get; set; }
        public DateTime FechaDemanda { get; set; }
        public int IdMateriaJudicial { get; set; }
        public string MateriaJudicial { get; set; }
        public int IdEstado { get; set; }
        public string Estado { get; set; }
        public string Comentario { get; set; }
        public bool BloquearRol { get; set; }
        public bool ProcesoQuiebra { get; set; }
        public bool Quiebra { get; set; }
        public int Pclid { get; set; }
        public int Ctcid { get; set; }
        public int Rolid { get; set; }

        public bool ActualizarRolPoderJudicial { get; set; }
        public int IdCompetencia { get; set; }

        public dto.DemandaAvenimiento Avenimiento = new DemandaAvenimiento();
        public dto.DemandaAvenimiento Demanda = new DemandaAvenimiento();
    }
}