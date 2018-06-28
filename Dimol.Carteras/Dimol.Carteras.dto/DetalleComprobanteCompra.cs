using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Carteras.dto
{
    public class DetalleComprobanteCompra
    {
        public int Item { get; set; }
        public int Insid { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Abreviado { get; set; }
        public decimal PrecioReal { set; get; }
        public decimal Precio { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Total { get; set; }
        public decimal Neto { get; set; }
        public decimal Impuesto { get; set; }
        public string Retenido { get; set; }
        public string NombreBodega { get; set; }
        public string NombreSectorBodega { get; set; }
        public decimal TotalNeto { get; set; }
        public string ArchivoEstampe { get; set; }
        public string NombreArchivo { get; set; }
        public string FecJud { get; set; }
    }
}
