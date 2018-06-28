using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dimol.Judicial.Mantenedores.Models
{
    public class TraspasoJudicialModel
    {
        [DisplayName("Fecha")]
        public string FechaTraspaso { get; set; }
        [StringLength(1024)]
        public List<String> Ids { get; set; }
        [StringLength(1024)]
        public string EstadoCpbt { get; set; }
        public string Documentos { get; set; }
        [DisplayName("Reporte")]
        public int Reporte { get; set; }

        [DisplayName("Tipo")]
        public int TipoGestion { get; set; }
        [DisplayName("Contacto")]
        public int Contacto { get; set; }
        [DisplayName("Comentario")]
        public string Comentario { get; set; }
        [DisplayName("Cambiar Estado")]
        public bool CambiaEstado { get; set; }
        [DisplayName("Estado")]
        public int ResultadoLlamado { get; set; }
        [DisplayName("Telefono")]
        public string TelefonoHistorial { get; set; }
        public bool MostrarTelefono { get; set; }


        [DisplayName("Agrupa")]
        public int Agrupa { get; set; }
        [DisplayName("Estado")]
        public int TipoEstado { get; set; }
        [DisplayName("Estados X Documentos")]
        public bool EstadosXDocumentos { get; set; }
        
        public bool MostrarFecha { get; set; }


        [DisplayName("pclid")]
        [StringLength(20)]
        public string Pclid { get; set; }
        [DisplayName("ctcid")]
        [StringLength(20)]
        public string Ctcid { get; set; }
        [DisplayName("tipo")]
        [StringLength(1)]
        public string Tipo { get; set; }

        public int TodosSeleccionados { get; set; }

    }
}