using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Dimol.Caja.Models
{
    public class CriterioFacturacionViewModel
    {
        public string Id { get; set; }
        public string Pclid { get; set; }
        [DisplayName("Cliente")]
        [StringLength(1024)]
        public string NombreRutCliente{ get; set; }

        [DisplayName("Descripción")]
        public string Descripcion { get; set; }

        [DisplayName("Tipo de Cambio")]
        public string CriterioAplicaSimbolo { get; set; }

        [DisplayName("ValorCriterio")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Debe ingresar solo número.")]
        public string ValorCriterio { get; set; }
               
        [DisplayName("Condición")]
        public string Condicion { get; set; }

        [DisplayName("Requiere Aprobación?")]
        public bool AplicaAprobacion { get; set; }

        [DisplayName("Aplica Criterio?")]
        public bool AplicaCriterio { get; set; }

        [DisplayName("No corresponde Facturacion")]
        public bool NoAplicaFactura { get; set; }

        [DisplayName("Imputable?")]
        public bool Imputable { get; set; }

        [DisplayName("Aplica para Remesa?")]
        public bool AplicaRemesa { get; set; }
    }
}