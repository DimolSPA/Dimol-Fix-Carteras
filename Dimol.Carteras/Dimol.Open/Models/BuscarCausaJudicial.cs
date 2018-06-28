using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Dimol.Open.Models
{
    public class BuscarCausaJudicial
    {
        [DisplayName("Ingrese Rut")]        
        public int RutDeudor { get; set; }
        [DisplayName("Deudor")]
        [StringLength(1024)]
        public string NombreRutDeudor { get; set; }

        [DisplayName("DV Deudor")]
        [StringLength(1024)]
        public string DvDeudor { get; set; }

        [DisplayName("Cliente")]
        [StringLength(1024)]
        public string RutCliente { get; set; }
        [DisplayName("Cliente")]
        public string NombreRutCliente { get; set; }
        [DisplayName("pclid")]
        [StringLength(20)]
        public string Pclid { get; set; }

        [DisplayName("Cliente")]
        [StringLength(1024)]
        public string RutClienteLogo { get; set; }
        [DisplayName("Cliente")]
        public string NombreRutClienteLogo { get; set; }
        [DisplayName("pclid")]
        [StringLength(20)]
        public string PclidLogo { get; set; }
        //[DisplayName("RUT")]
        //[StringLength(1024)]
        //public string Rut { get; set; }

        [DisplayName("ctcid")]
        [StringLength(20)]
        public string Ctcid { get; set; }

        [DisplayName("Activo")]
        public string Activo { get; set; }

        [DisplayName("Administrador")]
        public string Perfil { get; set; }
    }
}