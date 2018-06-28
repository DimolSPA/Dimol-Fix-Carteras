using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Carteras.dto
{
    public class DetalleCabeceraComprobante
    {
        public int Codsuc { get; set; }
        public int Codemp { get; set; }
        public int TipoComprobante { get; set; }
        public int CabeceraId { get; set; }
        public int Item { get; set; }
        public int Insid { get; set; }
        public int Prodid { get; set; }
        public int Pclid { get; set; }
        public int Ctcid { get; set; }
        public int Ccbid { get; set; }
        public decimal PrecioReal { set; get; }
        public decimal Precio { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Saldo { get; set; }
        public decimal Neto { get; set; }
        public decimal Impuesto { get; set; }
        public string Retenido { get; set; }
        public decimal Total { get; set; }
        public decimal Interes { get; set; }
        public decimal Honorario { get; set; }
        public decimal GastoPrejudicial { get; set; }
        public decimal GastoJudicial { get; set; }
        public decimal PorcFact { get; set; }
        public decimal PorcHono { get; set; }
        public int Bodid { get; set; }
        public int Bdsid { get; set; }
        public int Posicion { get; set; }

        public int Tpcidpad { get; set; }
        public int Numeropad { get; set; }
        public int Itempad { get; set; }
        public int Bodiddes { get; set; }
        public int Bdsiddes{ get; set; }
        public int Posiciondes { get; set; }
        public int NumeroSerie { get; set; }
        public string NumeroSerieProv { get; set; }

        public int Cantebj { get; set; }

        public string Ltpid { get; set; }
        public string Bscid { get; set; }
        public string Bsciddes { get; set; }


        public string Anio { get; set; }
        public string NumApl { get; set; }
        public string ItemApl { get; set; }
        public string ValRem { get; set; }
        public string Comentario { get; set; }
        public string Subitem { get; set; }
        public decimal Exento { get; set; }

        public int Rolid { get; set; }
        public decimal Monto { get; set; }
        public string Nombre { get; set; }

        public string EstadoCpbt { get; set; }
    }
}
