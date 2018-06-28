using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace Dimol.Finanzas.Models
{
    public class GridModel
    {
        public string  GridSelect { get; set; }
        public string  GridData { get; set; }
    }

    public class ComisionModel:dto.Comision
    {
        [DisplayName("Año")]
        public int Anio { get; set; }

        [DisplayName("Mes")]
        public int Mes { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Desde")]
        public new string Desde { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Hasta")]
        public new string Hasta { get; set; }
    }

    public class ClausulaContratoModel : dto.ClausulaContratoCartera
    {
        [DisplayName("Tipo")]
        public string Tipo { get; set; }

        [DisplayName("Nombre")]
        public string Nombre { get; set; }

        [DisplayName("Tipo Aplicacion")]
        public string TipoAplicacion { get; set; }

        [DisplayName("Valor")]
        public string Valor { get; set; }

        [DisplayName("Area")]
        public string Area { get; set; }

        [DisplayName("Valor Fijo")]
        public bool ValorFijo { get; set; }

        [DisplayName("Capital")]
        public bool Capital { get; set; }

        [DisplayName("Interes")]
        public bool Interes { get; set; }

        [DisplayName("Honorario")]
        public bool Honorario { get; set; }

        [DisplayName("Gasto Prejudicial")]
        public bool GastoPrejudicial { get; set; }

        [DisplayName("Gasto Judicial")]
        public bool GastoJudicial { get; set; }

        [DisplayName("Anula Maxima Convencional")]
        public bool AnulaMaximaConvencional { get; set; }

        [DisplayName("Rango")]
        public bool Rango { get; set; }

        [DisplayName("Clonar")]
        public bool Clonar { get; set; }

        [DisplayName("Tipo")]
        public string TipoRango { get; set; }

        [DisplayName("Nombre")]
        public string NombreClonar { get; set; }

        public int id { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Desde")]
        public new string Desde { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Hasta")]
        public new string Hasta { get; set; }
    }

    public class ContratoCarteraModel : dto.ContratoCartera
    {
        [DisplayName("Nombre")]
        public string Nombre { get; set; }

        [DisplayName("Tipo")]
        public string Tipo { get; set; }

        [DisplayName("Clonar")]
        public bool Clonar { get; set; }

        [DisplayName("Nombre a Clonar")]
        public string NombreClonar { get; set; }

        public string GridSelect { get; set; }
        public string GridData { get; set; }

        public int idCCT { get; set; }

        public int idCCT2 { get; set; }

        [DisplayName("Nombre")]
        public string ClausulasTodas2 { get; set; }
    }

    public class ClausulaModel : dto.Clausula
    {
        [DisplayName("Nombre")]
        public string Nombre { get; set; }

        [DisplayName("Tipo")]
        public string Tipo { get; set; }

        [DisplayName("Clonar")]
        public bool Clonar { get; set; }

        [DisplayName("Nombre a Clonar")]
        public string NombreClonar { get; set; }

        public string GridSelect { get; set; }
        public string GridData { get; set; }

        public int idCCT { get; set; }
    }

    

}