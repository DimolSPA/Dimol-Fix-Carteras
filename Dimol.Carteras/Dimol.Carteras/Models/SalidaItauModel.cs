using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Dimol.Carteras.Models
{
    public class SalidaItauModel
    {
        public int Pclid { get; set; }
        [DisplayName("Cliente")]
        public string RutCliente { set; get; }
        public string NombreCliente { get; set; }
        [DisplayName("Fecha Desde")]
        public string FechaDesde { get; set; }
        [DisplayName("Fecha Hasta")]
        public string FechaHasta { get; set; }
        public int TipoArchivo { get; set; }
    }
}