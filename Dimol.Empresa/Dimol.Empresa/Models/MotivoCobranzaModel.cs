using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace Dimol.Empresa.Models
{
    public class MotivoCobranzaModel
    {
        [Display(Name="Id")]
        public int? Id { get; set; }

        [Required]
        [Display(Name="Motivo")]
        public string Nombre { get; set; }

    }

    public class BuscarEmpleadoModel
    {

        [Display(Name = "Id")]
        public int? Id { get; set; }

        [Display(Name = "RutBuscar")]
        public string Rut { get; set; }

        [Display(Name = "NombreBuscar")]
        public string Nombre { get; set; }

        [Display(Name = "ApellidoPaterno")]
        public string ApellidoPaterno { get; set; }

        [Display(Name = "ApellidoMaterno")]
        public string ApellidoMaterno { get; set; }


        [Display(Name = "Estado")]
        public string Estado { get; set; }

    }

    public class EmpresaModel:dto.Empresa
    {


        [Display(Name = "Rut")]
        public string Rut { get; set; }

        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Display(Name = "RutRepresentanteLegal")]
        public string RutRepresentanteLegal { get; set; }

        [Display(Name = "NombreRepresentanteLegal")]
        public string NombreRepresentanteLegal { get; set; }

        [Display(Name = "Giro")]
        public string Giro { get; set; }

        [Display(Name = "Logo")]
        public string Logo { get; set; }

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