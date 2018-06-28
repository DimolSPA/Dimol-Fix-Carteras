using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Linq;
using System.Web;

namespace Dimol.Slider.Models
{
    public class TareaModel
    {
        [DisplayName("Id")]
        public int Id { get; set; }
        [DisplayName("Nombre")]
        [StringLength(150)]
        public string Nombre { get; set; }
        [DisplayName("Observacion")]
        [StringLength(500)]
        public string Observacion { get; set; }

        [DisplayName("Fecha de Ingreso")]
        public DateTime FechaIngreso { get; set; }
        [DisplayName("Fecha de Tarea")]
        public DateTime FechaTarea { get; set; }

        [DisplayName("Activa")]
        public int Activa { get; set; }
        [DisplayName("Completa")]
        public int Completa { get; set; }

    }
}