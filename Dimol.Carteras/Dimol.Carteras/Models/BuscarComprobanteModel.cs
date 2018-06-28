using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Dimol.Carteras.Models
{
    public class BuscarComprobanteModel
    {
        [DisplayName("Receptor")]
        [StringLength(1024)]
        public string RutCliente { get; set; }
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
        //[DisplayName("Nombre")]
        //[StringLength(1024)]
        //public string Nombre { get; set; }
        //[DisplayName("Apellidos")]
        //[StringLength(1024)]
        //public string ApellidoPaterno { get; set; }
        //[StringLength(1024)]
        //public string ApellidoMaterno { get; set; }
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
        [DisplayName("Número")]
        [StringLength(1024)]
        public string Numero { get; set; }
        [DisplayName("Tipo")]
        [StringLength(20)]
        public string ParticularEmpresa { get; set; }
        [DisplayName("Tipo Documento")]
        [StringLength(20)]
        public string TipoDocumento { get; set; }

        [DisplayName("Número Interno")]
        [StringLength(1024)]
        public string NumeroInterno { get; set; }
        [DisplayName("Estado")]
        [StringLength(1024)]
        public string Estado { get; set; }
        [DisplayName("Moneda")]
        [StringLength(1024)]
        public string Moneda { get; set; }
        [DisplayName("Desde")]
        [StringLength(10)]
        public DateTime? FechaEmisionDesde { get; set; }
        [DisplayName("Hasta")]
        [StringLength(10)]
        public DateTime? FechaEmisionHasta { get; set; }
        [DisplayName("Desde")]
        [StringLength(10)]
        public DateTime? FechaVencimientoDesde { get; set; }
        [DisplayName("Hasta")]
        [StringLength(10)]
        public DateTime? FechaVencimientoHasta { get; set; }

        [DisplayName("Desde")]
        [StringLength(30)]
        public string MontoDesde { get; set; }
        [DisplayName("Hasta")]
        [StringLength(30)]
        public string MontoHasta { get; set; }
        [DisplayName("Producto")]
        [StringLength(1024)]
        public string Producto { get; set; }
        [DisplayName("Comentario")]
        [StringLength(20)]
        public string Comentario { get; set; }
        [StringLength(2)]
        public string Tipo { set; get; }
        [StringLength(1)]
        public string Cartera { set; get; }

        [DisplayName("Deudor")]
        [StringLength(1024)]
        public string NombreRutDeudor { get; set; }

        [DisplayName("Tribunal")]
        public string Tribunal { get; set; }
        public int Rolid { get; set; }

        [DisplayName("Tipo Rol")]
        public string TipoRol { get; set; }
        [DisplayName("Año")]
        public int Anio { get; set; }

        public string FechaDesdeEmision { get; set; }
        public string FechaHastaEmision { get; set; }
    }
}