using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Dimol.Judicial.Mantenedores.Models
{
    public class RolModel : dto.Rol
    {
        [DisplayName("Cliente")]
        public string NombreRutCliente { get; set; }

        [DisplayName("Tipo Rol")]
        public string TipoRol { get; set; }

        [DisplayName("Rol")]
        public string Rol { get; set; }

        [DisplayName("Tribunal")]
        public string Tribunal { get; set; }
        public string TribunalSelect { get; set; }

        [DisplayName("Tipo Causa")]
        public string TipoCausa { get; set; }

        [DisplayName("Deudor")]
        public string NombreRutDeudor { get; set; }

        [DisplayName("Nombre Deudor")]
        public string NombreDeudor { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Fecha Ingreso")]
        public new string FechaIngreso { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Fecha Rol")]
        public new string FechaRol { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Fecha Demanda")]
        public new string FechaDemanda { get; set; }

        [DisplayName("Materia Judicial")]
        public string MateriaJudicial { get; set; }

        [DisplayName("Estado")]
        public string Estado { get; set; }

        [DisplayName("Comentario")]
        public string Comentario { get; set; }

        [DisplayName("Bloquear Rol")]
        public bool BloquearRol { get; set; }

        [DisplayName("Proceso Quiebra")]
        public bool ProcesoQuiebra { get; set; }

        [DisplayName("Proveedor Cliente")]
        public string ProvCli { get; set; }

        [DisplayName("Deudor")]
        public string Deudor { get; set; }

        [DisplayName("Actualizar desde Poder Judicial")]
        public bool ActualizarRolPoderJudicial { get; set; }

        public int Pclid { get; set; }
        public int Ctcid { get; set; }
        public int Rolid { get; set; }
        public bool Quiebra { get; set; }

        public string ComboQuiebra { get; set; }

        public string DocumentosAsignar { get; set; }
        public string DocumentosEliminar { get; set; }

        public int EstadoHistorial { get; set; }
        public int MateriaHistorial { get; set; }
        public DateTime? FechaHistorial { get; set; }
        public string ComentarioHistorial { get; set; }

        public string ListaEntes { get; set; }
        public int NuevoEnte { get; set; }

        public string ListaDemandados { get; set; }
        public string DemandadoRut { get; set; }
        public string DemandadoNombre { get; set; }
        public bool DemandadoRepresentanteLegal { get; set; }

        //Avenimiento Demanda
        [DisplayName("Fecha Avenimiento")]
        public string FechaAvenimiento { get; set; }
        [DisplayName("Monto")]
        public string MontoAvenimiento { get; set; }
        [DisplayName("Cuotas")]
        public int CuotasAvenimiento { get; set; }
        [DisplayName("Monto Cuota")]
        public string MontoCuotaAvenimiento { get; set; }
        [DisplayName("Monto Ult. Cuota")]
        public string MontoUltimaCuotaAvenimiento { get; set; }
        [DisplayName("Fecha 1era Cuota")]
        public string FechaPrimeraCuotaAvenimiento { get; set; }
        [DisplayName("Fecha Ult. Cuota")]
        public string FechaUltimaCuotaAvenimiento { get; set; }
        [DisplayName("Interes")]
        public string InteresAvenimiento { get; set; }

        [DisplayName("Fecha Demanda")]
        public string FechaDemandaAve { get; set; }
        [DisplayName("Monto")]
        public string MontoDemanda { get; set; }
        [DisplayName("Cuotas")]
        public int CuotasDemanda { get; set; }
        [DisplayName("Monto Cuota")]
        public string MontoCuotaDemanda { get; set; }
        [DisplayName("Monto Ult. Cuota")]
        public string MontoUltimaCuotaDemanda { get; set; }
        [DisplayName("Fecha 1era Cuota")]
        public string FechaPrimeraCuotaDemanda { get; set; }
        [DisplayName("Fecha Ult. Cuota")]
        public string FechaUltimaCuotaDemanda { get; set; }
        [DisplayName("Interes")]
        public string InteresDemanda { get; set; }

        [DisplayName("Borradores")]
        public int Borradores { get; set; }

        [AllowHtml]
        public string HTMLBorrador { get; set; }

        public string TotalMontoAsignado { get; set; }
        public string TotalSaldoAsignado { get; set; }
        public string TotalMontoPorAsignar { get; set; }
        public string TotalSaldoPorAsignar { get; set; }

        [DisplayName("Estado Adm.")]
        public string EstAdm { get; set; }

        [AllowHtml]
        public string CSSEstAdm {
            get { return EstAdm == "Archivada" ? "proceso-on" : "proceso-off"; }
        }

        public string CSSBloqueo
        {
            get { return !BloquearRol ? "proceso-on" : "proceso-off"; }
        }

        public string Archivo { get; set; }

        [DisplayName("Rut")]
        public string RutDeudor { get; set; }

        [DisplayName("Liquidador")]
        public string Sindico { get; set; }

        [DisplayName("Veedor")]
        public string Veedor { get; set; }

        [DisplayName("Interventor")]
        public string Interventor { get; set; }
    }
}