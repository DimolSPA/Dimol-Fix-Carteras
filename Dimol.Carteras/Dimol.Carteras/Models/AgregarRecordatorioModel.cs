using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dimol.Carteras.Models
{
    public class AgregarRecordatorioModel
    {
        [DisplayName("Tipo")]
        public string TipoForm { get; set; }
        [DisplayName("Email")]
        [StringLength(250)]
        public string EmailRecordatorio { get; set; }
        [DisplayName("Teléfono")]
        [StringLength(12)]
        public string TelefonoRecordatorio { get; set; }
        [DisplayName("Fecha Envio")]
        public DateTime FechaEnvioRecordatorio { get; set; }
       

        [DisplayName("pclid")]
        [StringLength(20)]
        public string Pclid { get; set; }
        [DisplayName("ctcid")]
        [StringLength(20)]
        public string Ctcid { get; set; }

    }
}