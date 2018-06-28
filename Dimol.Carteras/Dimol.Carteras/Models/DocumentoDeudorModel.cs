using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Dimol.Carteras.Models
{
    public class DocumentoDeudorModel
    {
        [DisplayName("Cliente")]
        [StringLength(200)]
        public string NombreRutCliente { get; set; }
        [DisplayName("pclid")]
        [StringLength(20)]
        public string Pclid { get; set; }
        [DisplayName("Deudor")]
        [StringLength(1024)]
        public string NombreRutDeudor { get; set; }
        [DisplayName("ctcid")]
        [StringLength(20)]
        public string Ctcid { get; set; }
        [DisplayName("dcdid")]
        [StringLength(20)]
        public string Dcdid { get; set; }
        [DisplayName("Tipo Documento")]
        [StringLength(20)]
        public string TipoDocumento { get; set; }
        [DisplayName("Archivo")]
        [StringLength(1024)]
        public string Archivo { get; set; }
        [DisplayName("URL")]
        [StringLength(1024)]
        public string Ruta { get; set; }
        public int Codemp { get; set; }

    }
}