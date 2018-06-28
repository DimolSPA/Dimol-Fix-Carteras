using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Dimol.Carteras.Models
{
    public class ContactoTelefonoMailModel
    {
        [DisplayName("Teléfono")]
        [StringLength(20)]
        public string Telefono { get; set; }
        [DisplayName("Tipo Teléfono")]
        [StringLength(20)]
        public string TipoTelefono { get; set; }
        [DisplayName("Estado")]
        [StringLength(20)]
        public string EstadoTelefono { get; set; }
        [DisplayName("Tipo Contacto")]
        [StringLength(20)]
        public string TipoContacto { get; set; }
        [DisplayName("EstadoContacto")]
        [StringLength(20)]
        public string EstadoContacto { get; set; }
        [DisplayName("ctcid")]
        [StringLength(20)]
        public string Ctcid { get; set; }
        [DisplayName("pclid")]
        [StringLength(20)]
        public string Pclid { get; set; }
        [StringLength(20)]
        public string PclidEmail { get; set; }
        [DisplayName("Nombre Contacto")]
        [StringLength(1024)]
        public string NombreContacto { get; set; }
        [DisplayName("Email")]
        [StringLength(1024)]
        public string Email { get; set; }

        [DisplayName("Email Masivo")]
        public bool EmailMasivo { get; set; }
        [DisplayName("Tipo Email")]
        [StringLength(20)]
        public string TipoEmail { get; set; }


        [DisplayName("Estado Dirección")]
        public int EstadoDireccion { get; set; }
        [DisplayName("Dirección")]
        [StringLength(1024)]
        [Required]
        public string DireccionContacto { get; set; }
        [DisplayName("Región")]
        [Required]
        public int IdRegion { get; set; }
        [DisplayName("Ciudad")]
        [Required]
        public int IdCiudad { get; set; }
        [DisplayName("País")]
        [Required]
        public int IdPais { get; set; }
        [DisplayName("Comuna")]
        [Required]
        public int IdComuna { get; set; }

        [DisplayName("Tipo Email")]
        [StringLength(20)]
        public string TipoForm{ get; set; }
        public int Ddcid { get; set; }

        [DisplayName("Anexo")]
        [StringLength(10)]
        public string Anexo { get; set; }

        [DisplayName("Tipo Dirección")]
        [StringLength(20)]
        public string TipoDireccion { get; set; }


    }
}