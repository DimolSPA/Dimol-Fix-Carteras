using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Dimol.Caja.Models
{
    public class DocumentoCustodiaModel
    {
        
        [DisplayName("Cliente")]
        [StringLength(1024)]
        public string NombreRutClienteCustodia { get; set; }
        [DisplayName("pclid")]
        [StringLength(20)]
        public string PclidCustodia { get; set; }

        [DisplayName("Deudor")]
        [StringLength(1024)]
        public string NombreRutDeudorCustodia { get; set; }
        [DisplayName("ctcid")]
        [StringLength(20)]
        public string CtcidCustodia { get; set; }

        [DisplayName("Gestor")]
        [StringLength(1024)]
        public string NombreRutGestorCustodia { get; set; }
        [DisplayName("gesid")]
        [StringLength(20)]
        public string GesidCustodia { get; set; }
        
    }
}