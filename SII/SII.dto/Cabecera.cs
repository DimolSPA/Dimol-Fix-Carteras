using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SII.dto
{
    public class Cabecera
    {
        public int IdRut { get; set; }
        public int Rut { get; set; }
        public string DV { get; set; }
        public string NombreRazonSocial { get; set; }
        public DateTime FechaConsulta { get; set; }
        public string InicioActividades { get; set; }
        public DateTime FechaInicioActividades { get; set; }
        public string ImpuestoMonedaExtranjera { get; set; }
        public string MenorProPyme { get; set; }
        public string Registrado { get; set; }
        public int Ctcid { get; set; }
        public string Emision { get; set; }
        public string Observacion { get; set; }
        public DateTime Fecha { get; set; }
    }
}
