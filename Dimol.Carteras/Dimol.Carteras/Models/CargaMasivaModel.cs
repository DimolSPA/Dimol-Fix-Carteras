using Dimol.Carteras.dto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dimol.Carteras.Models
{
    public class CargaMasivaModel : CargaMasiva
    {
        //
        // GET: /CargaMasivaModel/

        public int Pclid { get; set; }
        [DisplayName("Cliente")]
        public string RutCliente { set; get; }
        public string NombreCliente { get; set; }
        [DisplayName("Tipo Cartera")]
        public string TipoCartera { get; set; }
        [DisplayName("CodigoCarga")]
        public string CodigoCarga { get; set; }
        [DisplayName("Contrato")]
        public string Contrato { get; set; }
        [DisplayName("Archivo")]
        public string Archivo { get; set; }
        [DisplayName("Carga Judicial")]
        public bool CargaJudicial { get; set; }
        [DisplayName("Archivo Quiebra")]
        public bool ArchivoQuiebra { get; set; }
        [DisplayName("Actualizar Datos")]
        public bool ActualizarDatos { get; set; }
        [DisplayName("Simulacion")]
        public bool Simulacion { get; set; }
    }
}
