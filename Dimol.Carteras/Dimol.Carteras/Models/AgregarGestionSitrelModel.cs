using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Dimol.Carteras.Models
{
    public class AgregarGestionSitrelModel
    {
        [DisplayName("Tipo")]
        public string AccionSitrel { get; set; }
        [DisplayName("Contacto")]
        public string ContactoSitrel { get; set; }
        [DisplayName("Nombre Contacto")]
        public string NombreContactoSitrel { get; set; }
        [DisplayName("Teléfono Contacto")]
        public string TelefonoContactoSitrel { get; set; }
        [DisplayName("Estado")]
        public string RespuestaSitrel { get; set; }
        [DisplayName("Monto Gestión")]
        public decimal MontoGestionSitrel { get; set; }
        [DisplayName("Comentario")]
        public string ComentarioSitrel { get; set; }

        [DisplayName("Fecha Compromiso")]
        public string FechaCompromisoSitrel { get; set; }
        [DisplayName("Monto Compromiso")]
        public decimal MontoCompromisoSitrel { get; set; }

        [DisplayName("Fecha")]
        public string FechaProgramadaSitrel { get; set; }
        [DisplayName("Hora")]
        public int HoraProgramadaSitrel { get; set; }
        public int MinutoProgramadoSitrel { get; set; }

        [DisplayName("pclid")]
        [StringLength(20)]
        public string Pclid { get; set; }
        [DisplayName("ctcid")]
        [StringLength(20)]
        public string Ctcid { get; set; }
        [DisplayName("tipo")]
        [StringLength(1)]
        public string Tipo { get; set; }


        public string CodigoEmpresa { get; set; }

        
        [DisplayName("Estado")]
        public int ResultadoLlamado { get; set; }
        [DisplayName("Teléfono")]
        public string TelefonoHistorial { get; set; }
        public bool MostrarTelefono { get; set; }


        [DisplayName("Agrupa")]
        public int Agrupa { get; set; }
        [DisplayName("Estado")]
        public int TipoEstado { get; set; }
        [DisplayName("Estados X Documentos")]
        public bool EstadosXDocumentos { get; set; }
        [DisplayName("Fecha")]
        public string FechaHistorial { get; set; }
        public bool MostrarFecha { get; set; }


        [DisplayName("Cambiar Estado")]
        public bool CambiaEstado { get; set; }

        [StringLength(1024)]
        public string Ids { get; set; }
        [StringLength(1024)]
        public string EstadoCpbt { get; set; }
        public string Documentos { get; set; }
        public int TodosSeleccionados { get; set; }



    }
}