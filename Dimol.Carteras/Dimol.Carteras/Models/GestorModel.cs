using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Dimol.Carteras.Models
{
    public class GestorModel
    {
        public string GesId { get; set; }

        [DisplayName("Empleado")]
        [Required]
        public string LstEmpleado { get; set; }

        [DisplayName("Nombre")]
        [Required]
        public string Nombre { get; set; }

        [DisplayName("Teléfono")]
        //[StringLength(9, ErrorMessage = "El número telefónico debe contener 9 dígitos", MinimumLength = 9)]
        public string Telefono { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "La dirección de Email es requerida")]
        //[RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$", ErrorMessage = "Por favor ingrese una dirección de email válida")]
        [EmailAddress(ErrorMessage = "Por favor ingrese una dirección de email válida")]
        public string Email { get; set; }
        //[Required]
        //[DataType(DataType.EmailAddress)]
        //[EmailAddress]
        //public string Email { get; set; }

        [DisplayName("Tipo de Cartera")]
        public string LstTipoCartera { get; set; }

        [DisplayName("Grupo de Cobranza")]
        public string LstGrupoCobranza { get; set; }

        [DisplayName("Gestor Remoto")]
        [Required]
        public new bool GestorRemoto { get; set; }

        [DisplayName("Activo")]
        [Required]
        public new bool GestorActivo { get; set; }

        [DisplayName("Visita Terreno")]
        [Required]
        public new bool GestorTerreno { get; set; }

        [DisplayName("Teléfono Terreno")]
        public string TelefonoTerreno { get; set; }

        [DisplayName("Teléfono Imei")]
        public string TelefonoImei { get; set; }

    }
}