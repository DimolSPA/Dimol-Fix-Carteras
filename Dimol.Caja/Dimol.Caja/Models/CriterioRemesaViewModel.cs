using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Dimol.Caja.Models
{
    public class CriterioRemesaViewModel
    {
        public string Id { get; set; }
        public string Pclid { get; set; }
        [DisplayName("Días Vencimiento Desde")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Debe ingresar solo número.")]
        public string Desde { get; set; }

        [DisplayName("Días Vencimiento Hasta")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Debe ingresar solo número.")]
        public string Hasta { get; set; }

        [DisplayName("Tipo de Región")]
        public string TipoRegion { get; set; }

        [DisplayName("Código de Carga")]
        public string CodigoCarga { get; set; }

        [DisplayName("Tipo de Cambio")]
        public string TipoCambioCapital { get; set; }
        public string TipoCambioCapitalId { get; set; }

        [DisplayName("Capital")]
        [StringLength(3)]
        [RegularExpression(@"^\d+$", ErrorMessage = "Debe ingresar solo número.")]
        public string Capital { get; set; }

        [DisplayName("Interés")]
        [StringLength(3)]
        [RegularExpression(@"^\d+$", ErrorMessage = "Debe ingresar solo número.")]
        public string Interes { get; set; }

        [DisplayName("Honorario")]
        [StringLength(3)]
        [RegularExpression(@"^\d+$", ErrorMessage = "Debe ingresar solo número.")]
        public string Honorario { get; set; }

        [DisplayName("Tipo de Conciliación")]
        public string TipoConciliacion { get; set; }

        public bool IsFacturacion { get; set; }

        [DisplayName("Condición de Anticipo")]
        public string CondicionAnticipo { get; set; }
    }
}