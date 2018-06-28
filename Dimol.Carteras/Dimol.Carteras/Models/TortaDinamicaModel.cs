using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Dimol.Carteras.Models
{
    public class TortaDinamicaModel
    {
        [DisplayName("Cliente")]
        [StringLength(200)]
        public string NombreRutCliente { get; set; }
        [DisplayName("pclid")]
        [StringLength(20)]
        public string Pclid { get; set; }
        [DisplayName("Deudor")]
        [StringLength(1024)]
        public string NombreRutDeudor { get; set; }
        [DisplayName("ctcid")]
        [StringLength(20)]
        public string Ctcid { get; set; }

        [DisplayName("Tipo Cartera")]
        [StringLength(20)]
        public string TipoCartera { get; set; }
        [DisplayName("Código Carga")]
        [StringLength(20)]
        public string CodigoCarga { get; set; }
        [DisplayName("Código Carga")]
        [StringLength(20)]
        public string Grupo { get; set; }

        [DisplayName("Gestor")]
        [StringLength(20)]
        public string Gestor { get; set; }

        [DisplayName("Hasta")]
        public DateTime? FechaHasta { get; set; }
        [DisplayName("Agrupa")]
        [StringLength(20)]
        public string Agrupa { get; set; }
        [DisplayName("Estado")]
        [StringLength(20)]
        public string Estado { get; set; }
        
        [DisplayName("dcdid")]
        [StringLength(20)]
        public string Dcdid { get; set; }
        [DisplayName("Tipo Documento")]
        [StringLength(20)]
        public string TipoDocumento { get; set; }
        
		[DisplayName("Situacion Cartera")]
        [StringLength(20)]
        public string SituacionCartera { get; set; }

        [DisplayName("Estado Documento")]
        [StringLength(20)]
        public string EstadoDocumento { get; set; }

        public int Codemp { get; set; }

        [DisplayName("Documentos")]
        public int? DocsVencidos { get; set; }
    }
}