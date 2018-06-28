using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dimol.Carteras.Models
{
    public class EjecutivosMutualModel
    {
        [DisplayName("Cliente")]
        [StringLength(200)]
        public string NombreRutCliente { get; set; }
        [DisplayName("pclid")]
        [StringLength(20)]
        public string Pclid { get; set; }

        [StringLength(20)]
        public string IdTipoBanco { get; set; }
        [DisplayName("Banco")]
        [StringLength(1024)]
        public string NombreBanco { get; set; }
        
        [StringLength(20)]
        public string IdEjecutivo { get; set; }
        [DisplayName("Ejecutivo")]
        [StringLength(1024)]
        public string NombreEjecutivo { get; set; }
        [DisplayName("Email")]
        [StringLength(1024)]
        public string Email { get; set; }
        [DisplayName("Oficina")]
        [StringLength(1024)]
        public string Oficina { get; set; }

        [StringLength(20)]
        public string IdCuentaEjecutivo { get; set; }
        [DisplayName("Cuenta")]
        [StringLength(1024)]
        public string Cuenta { get; set; }
    }
}