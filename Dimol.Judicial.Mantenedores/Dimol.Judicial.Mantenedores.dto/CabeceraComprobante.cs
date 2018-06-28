using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Judicial.Mantenedores.dto
{
    public class CabeceraComprobante
    {
        public int Codemp { get; set; }
        public int Codsuc { get; set; }
        public int TipoComprobante { get; set; }
        public int Numero { get; set; }
        public int NumeroProveedor { get; set; }
        public int Pclid { get; set; }
        public DateTime FechaComprobante { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public DateTime FechaEnt { get; set; }
        public int CodigoMoneda { get; set; }
        public decimal TipoCambio { get; set; }
        public int Frpid { get; set; }
        public int Anio { get; set; }
        public int Mes { get; set; }
        public string Glosa { get; set; }
        public decimal PorcentajeDescuento { get; set; }
        public string OrdenCompra { get; set; }
        public string GastoJudicial { get; set; }
        public int Vdeid { get; set; }
        public int Tntid { get; set; }
        public int Tgdid { get; set; }
        public int Ttlid { get; set; }
        public decimal Exento { get; set; }
        public int Pcsid { get; set; }
        public DateTime FechaContrato { get; set; }
        public DateTime FechaOC { get; set; }

    }

    public class CabeceraComprobanteDetalle
    {
        public int Codemp { get; set; }
        public int Codsuc { get; set; }
        public int TipoComprobante { get; set; }
        public int Numero { get; set; }
        public int Item { get; set; }
        public int Insid { get; set; }
        public int Prodid { get; set; }
        public int Pclid { get; set; }
        public int Ctcid { get; set; }
        public int Ccbid { get; set; }
        public decimal PrecioReal { get; set; }
        public decimal Precio { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Saldo { get; set; }
        public decimal Neto { get; set; }
        public decimal Impuesto { get; set; }
        public string Retenido { get; set; }
        public decimal Total { get; set; }
        public decimal Interes { get; set; }
        public decimal Honorario { get; set; }
        public decimal GastoPreJudicial { get; set; }
        public decimal GastoJudicial { get; set; }
        public decimal PorcentajeFactura { get; set; }
        public decimal PorcentajeHonorario { get; set; }
        public int Bodid { get; set; }
        public int Bdsid { get; set; }
        public int Posicion { get; set; }
        public int Tpcidpad { get; set; }
        public long Numeropad { get; set; }
        public int Itempad { get; set; }
        public int BodidDes { get; set; }
        public int BdsidDes { get; set; }
        public int PosicionDes { get; set; }
        public long NumeroSerie { get; set; }
        public string NumeroSerieProveedor { get; set; }
        public decimal Cantebj { get; set; }
        public int Ltpid { get; set; }
        public string Bscid { get; set; }
        public string BscidDes { get; set; }
        public int Anio { get; set; }
        public long NumeroApl { get; set; }
        public int ItemApl { get; set; }
        public decimal ValRem { get; set; }
        public string Comentario { get; set; }
        public int Subitem { get; set; }
        public decimal Exento { get; set; }
    }

    public class CabeceraComprobanteEstado
    {
        public int Codemp { get; set; }
        public int Codsuc { get; set; }
        public int TipoComprobante { get; set; }
        public int Numero { get; set; }
        public string Estado { get; set; }
        public DateTime Fecha { get; set; }
        public int Usuario { get; set; }
        public string IpPC { get; set; }
        public string IpRed { get; set; }
        public string Comentario { get; set; }
    }
}
