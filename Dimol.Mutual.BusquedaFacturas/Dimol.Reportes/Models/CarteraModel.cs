using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Dimol.Reportes.Models
{
    public class CarteraModel
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
        [DisplayName("Reporte")]
        [StringLength(200)]
        public string Reporte { get; set; }
        [DisplayName("Tipo Cartera")]
        [StringLength(20)]
        public string TipoCartera { get; set; }
        [DisplayName("Codigo Carga")]
        [StringLength(20)]
        public string CodigoCarga { get; set; }
        [DisplayName("Codigo Carga")]
        [StringLength(20)]
        public string Grupo { get; set; }
        [DisplayName("Fecha Baja")]
        public DateTime? FechaBaja { get; set; }
        [DisplayName("Gestor")]
        [StringLength(20)]
        public string Gestor { get; set; }
        [DisplayName("Fecha Recuperación")]
        public DateTime? FechaDesde { get; set; }
        [DisplayName("Hasta")]
        public DateTime? FechaHasta { get; set; }
        [DisplayName("Agrupa")]
        [StringLength(20)]
        public string Agrupa { get; set; }
        [DisplayName("Estado")]
        [StringLength(20)]
        public string Estado { get; set; }


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
