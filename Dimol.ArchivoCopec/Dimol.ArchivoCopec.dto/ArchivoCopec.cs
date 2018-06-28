using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.ArchivoCopec.dto
{
    public class ArchivoCopec
    {
        public string CuentaInterna { get; set; }
        public string RutCliente { get; set; }
        public string TipoPersona { get; set; }
        public string RazonSocial { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string TipoCliente { get; set; }
        public string TotalDeuda { get; set; }
        public string MonedaTotalDeuda { get; set; }
        public string FechaEnvio { get; set; }
        public string TipoCobranzaDocumento { get; set; }
        public string TipoJuicio { get; set; }
        public string Rol { get; set; }
        public string Tribunal { get; set; }
        public string EtapaJuicio { get; set; }
        public string EstadoJuicio { get; set; }
        public string UltimaGestion { get; set; }
        public string FechaUltimaGestion { get; set; }
        public string RutAbogado { get; set; }
        public string NombreAbogado { get; set; }
        public string FechaInicioJuicio { get; set; }
        public string FechaNotificacion { get; set; }
        public string FechaEmbargo { get; set; }
        public string FechaRemate { get; set; }
        public string FechaIncitacion { get; set; }
        public string FechaFinJuicio { get; set; }
        public string RutEjecutivo { get; set; }
        public string NombreEjecutivo { get; set; }
        public string ObservacionesCopec { get; set; }
        public string ObservacionesOficina { get; set; }
        public string Localidad { get; set; }
        public string NumeroCuota { get; set; }
        public string Sociedad { get; set; }
        public string Anio { get; set; }
        public string NumeroDocumento { get; set; }
        public string RolPadre { get; set; }
        public string TribunalPadre { get; set; }
    }

    public class ArchivoCopecGestiones
    {
        public string RutCliente { get; set; }
        public string Rol { get; set; }
        public string Tribunal { get; set; }
        public string GestionRealizada { get; set; }
        public string FechaGestion { get; set; }
        public string RutGestion { get; set; }
    }
}
