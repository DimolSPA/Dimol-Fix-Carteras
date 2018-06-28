using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Dimol.Carteras.Models
{
    public class DeudorModel:dto.Deudor
    {
        [DisplayName("Cliente")]
        [StringLength(9)]
        public string RutCliente { get; set; }
        [DisplayName("pclid")]
        [StringLength(20)]
        public string Pclid { get; set; }
        [DisplayName("Nombre Cliente")]
        [StringLength(1024)]
        public string NombreCliente { get; set; }
        [Required]
        [DisplayName("RUT")]
        [StringLength(1024)]
        public new string Rut { get; set; }
        [DisplayName("ctcid")]
        [StringLength(20)]
        public string Ctcid { get; set; }
        [Required]
        [DisplayName("Nombre")]
        [StringLength(1024)]
        public new string Nombre { get; set; }
        [DisplayName("Apellidos")]
        [StringLength(1024)]
        [Required]
        public string ApellidoPaterno { get; set; }
        [StringLength(1024)]
        public string ApellidoMaterno { get; set; }
        [DisplayName("Nombre Fantasia")]
        [StringLength(1024)]
        public new string NombreFantasia { get; set; }
        [DisplayName("Teléfono")]
        [StringLength(1024)]
        public string Telefono { get; set; }
        [DisplayName("Email")]
        [StringLength(1024)]
        public string Email { get; set; }
        [DisplayName("Dirección")]
        [StringLength(1024)]
        [Required]
        public string Direccion { get; set; }
        [DisplayName("Rol")]
        [StringLength(1024)]
        public string Rol { get; set; }
        [DisplayName("Situación Cartera")]
        [StringLength(1024)]
        public string SituacionCartera { get; set; }
        [DisplayName("Nro. CPBT")]
        [StringLength(1024)]
        public string NumeroCPBT { get; set; }
        [DisplayName("Tipo")]
        [Required]
        public new string ParticularEmpresa { get; set; }

        [DisplayFormat( DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Fecha Ingreso")]
        [StringLength(20)]
        public new string FechaIngreso { get; set; }
        [DisplayName("Sociedad")]
        public new int IdSociedad { get; set; }
        
        //[DisplayName("")]
        //[StringLength(1024)]
        //public new string Sociedad { get; set; }
        //[DisplayName("Estado Dirección")]
        [Required]
        public new int EstadoDireccion { get; set; }
        [DisplayName("Quiebra")]
        [Required]
        public new bool Quiebra { get; set; }
        [DisplayName("Nacional")]
        [Required]
        public new bool NacionalExtranjero { get; set; }
        [DisplayName("Región")]
        [Required]
        public new int IdRegion { get; set; }
        [DisplayName("Ciudad")]
        [Required]
        public new int IdCiudad { get; set; }
        [DisplayName("País")]
        [Required]
        public new int IdPais { get; set; }
        [DisplayName("Comuna")]
        [Required]
        public new int IdComuna { get; set; }

        [DisplayName("Para Quiebra")]
        [Required]
        public new bool SolicitaQuiebra { get; set; }
    }
}