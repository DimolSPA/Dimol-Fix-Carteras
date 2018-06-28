using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace Dimol.Carteras.Models
{
    public class ModelBuscarDocumentosDeudores
    {
        [DisplayName("RUT")]
        [StringLength(1024)]
        public string NombreRutDeudor { get; set; }
        [DisplayName("ctcid")]
        public int Ctcid { get; set; }
    }
}