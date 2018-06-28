using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dimol.Carteras.Mantenedores.Models
{
    public class MotivoCobranzaModel
    {
        [Display(Name="Id")]
        public int? Id { get; set; }

        [Required]
        [Display(Name="Motivo")]
        public string Nombre { get; set; }

    }

    public class CodigoCargaModel
    {
        [Required]
        [Display(Name = "Id Cliente")]
        public int IdCliente { get; set; }

        [Required]
        [Display(Name = "Id Carga")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nombre Cliente")]
        public string NombreCliente { get; set; }

        [Required]
        [Display(Name = "Código de Carga")]
        public string CodigoCarga { get; set; }

        [Required]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }



    }
}