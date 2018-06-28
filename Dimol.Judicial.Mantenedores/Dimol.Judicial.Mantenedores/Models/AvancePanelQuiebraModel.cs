using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Dimol.Judicial.Mantenedores.Models
{
    public class AvancePanelQuiebraModel
    {
        public int QuiebraId { get; set; }
        [DisplayName("Síndico")]
        public string Sindico { get; set; }
        [DisplayName("Nota de Débito")]
        public string NotaDebito { get; set; }
        [DisplayName("Liquidación de Costas")]
        public string LiquidacionCostas { get; set; }
        [DisplayName("Solicitante")]
        public string Solicitante { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Fecha Solicitud de Quiebra")]
        public new string FecSolicitudQuiebra { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Publicación Solicitud en BC")]
        public new string FecSolicitudBC { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Fecha Notificación")]
        public new string FecNotificacion { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Fecha Publicación Sentencia")]
        public new string FecPublicacionSentencia { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Fecha Publicación Reconocido")]
        public new string FecPublicacionReconocido { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Fecha Solicitud Antecedentes")]
        public new string FecSolAntecedente { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Fecha Nota Débito")]
        public new string FecNotaDebito { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Fecha Cuenta Final")]
        public new string FecCtaFinal { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Fecha Nota DébitoCastigo")]
        public new string FecCastigo { get; set; }

        [DisplayName("Fecha Nota Débito")]
        public  string Comentarios { get; set; }
    }
}