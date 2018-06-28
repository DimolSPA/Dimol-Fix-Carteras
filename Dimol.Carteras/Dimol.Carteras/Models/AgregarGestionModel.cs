using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Dimol.Carteras.Models
{
    public class AgregarGestionModel
    {
        [DisplayName("Tipo")]
        public int TipoGestion { get; set; }
        [DisplayName("Contacto")]
        public int Contacto { get; set; }
        [DisplayName("Comentario")]
        public string  Comentario { get; set; }
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
        [DisplayName("Fecha")]
        public string FechaHistorial { get; set; }
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

        [StringLength(1024)]
        public string Ids { get; set; }
        [StringLength(1024)]
        public string EstadoCpbt { get; set; }
        public string Documentos { get; set; }
        public int TodosSeleccionados { get; set; }


        //[DisplayName("Cliente")]
        //[StringLength(9)]
        //public string RutCliente { get; set; }
        //[DisplayName("Cliente")]
        //public string NombreRutCliente { get; set; }
        
        //[DisplayName("Nombre Cliente")]
        //[StringLength(1024)]
        //public string NombreCliente { get; set; }
        //[DisplayName("RUT")]
        //[StringLength(1024)]
        //public string Rut { get; set; }
        
        //[DisplayName("Nombre")]
        //[StringLength(1024)]
        //public string Nombre { get; set; }
        //[DisplayName("Apellidos")]
        //[StringLength(1024)]
        //public string ApellidoPaterno { get; set; }
        //[StringLength(1024)]
        //public string ApellidoMaterno { get; set; }
        //[DisplayName("Nombre Fantasia")]
        //[StringLength(1024)]
        //public string NombreFantasia { get; set; }
        //[DisplayName("Teléfono")]
        //[StringLength(1024)]
        //public string Telefono { get; set; }
        //[DisplayName("Email")]
        //[StringLength(1024)]
        //public string Email { get; set; }
        //[DisplayName("Dirección")]
        //[StringLength(1024)]
        //public string Direccion { get; set; }
        //[DisplayName("Rol")]
        //[StringLength(1024)]
        //public string Rol { get; set; }
        //[DisplayName("Situación Cartera")]
        //[StringLength(1024)]
        //public string SituacionCartera { get; set; }
        //[DisplayName("Nro. CPBT")]
        //[StringLength(1024)]
        //public string NumeroCPBT { get; set; }
        //[DisplayName("Tipo")]
        //[StringLength(20)]
        //public string ParticularEmpresa { get; set; }
        //[DisplayName("Tipo Documento")]
        //[StringLength(20)]
        //public string TipoDocumento { get; set; }


    }
}