using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Caja.dto
{
    public class CriterioFacturacion
    {
        public int Id { get; set; }
        public int Pclid { get; set; }
        public string Cliente { get; set; }
        public string Descripcion { get; set; }
        public bool NoAplicaFactura { get; set; }
        public bool AplicaAprobacion { get; set; }
        public bool AplicaCriterio { get; set; }
        public int? SimboloId { get; set; }
        public string CriterioAplicaSimbolo { get; set; }
        public int? ValorCriterio { get; set; }
        public bool Imputable { get; set; }
        public int? CondicionId { get; set; }
        public string Condicion { get; set; }
        public bool AplicaRemesa { get; set; }
        public int Row { get; set; }
    }
}
