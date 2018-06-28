using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Dimol.Carteras.Models
{
    public class BuscarDeudorModel:dto.BuscarDeudor
    {
        [DisplayName("Cliente")]
        [StringLength(9)]
        public string RutCliente { get; set; }
        [DisplayName("Cliente")]
        public string NombreRutCliente { get; set; }
        [DisplayName("pclid")]
        [StringLength(20)]
        public string Pclid { get; set; }
        [DisplayName("Nombre Cliente")]
        [StringLength(1024)]
        public string NombreCliente { get; set; }
        [DisplayName("RUT")]
        [StringLength(1024)]
        public string Rut { get; set; }
        [DisplayName("ctcid")]
        [StringLength(20)]
        public string Ctcid { get; set; }
        [DisplayName("Nombre")]
        [StringLength(1024)]
        public string Nombre { get; set; }
        [DisplayName("Apellidos")]
        [StringLength(1024)]
        public string ApellidoPaterno { get; set; }
        [StringLength(1024)]
        public string ApellidoMaterno { get; set; }
        [DisplayName("Nombre Fantasia")]
        [StringLength(1024)]
        public string NombreFantasia { get; set; }
        [DisplayName("Teléfono")]
        [StringLength(1024)]
        public string Telefono { get; set; }
        [DisplayName("Email")]
        [StringLength(1024)]
        public string Email { get; set; }
        [DisplayName("Dirección")]
        [StringLength(1024)]
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
        [StringLength(20)]
        public string ParticularEmpresa { get; set; }
        [DisplayName("Tipo Documento")]
        [StringLength(20)]
        public string TipoDocumento { get; set; }

        [DisplayName("Sbcid")]
        [StringLength(20)]
        public string Sbcid { get; set; }

        [DisplayName("Asegurado")]
        public string NombreRutAsegurado { get; set; }
    }
}