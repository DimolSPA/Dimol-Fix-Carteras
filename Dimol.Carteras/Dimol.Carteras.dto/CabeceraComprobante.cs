using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Carteras.dto
{
    public class CabeceraComprobante
    {
        public int Pclid { get; set; }
        public int Ctcid { get; set; }
        public int Ccbid { get; set; }
        public int CabeceraId { get; set; }
        public string RutCliente { set; get; }
        public string NombreCliente { get; set; }
        public int TipoComprobante { get; set; }
        public string TipoComprobanteDesc { get; set; }
        public string Sucursal { get; set; }
        public string Numero { get; set; }
        public string NumeroOC { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime FechaDocumento { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public DateTime FechaOrdenCompra { get; set; }
        public DateTime FechaEntrega { get; set; }
        public DateTime FechaContabilizacion { get; set; }
        public string Moneda { get; set; }
        public decimal TipoCambio { get; set; }
        public string FormaPago { get; set; }
        public string TipoGasto { get; set; }
        public string MotivoCobranza { get; set; }
        public string Estado { get; set; }
        public string Glosa { get; set; }

        public string Tipcpbtdoc { get; set; }
        public int Tipprod { get; set; }
        public string Costos { get; set; }
        public string Selcpbt { get; set; }
        public string Cartcli { get; set; }
        public string Contable { get; set; }
        public string Selapl { get; set; }
        public string Aplica { get; set; }
        public string Cptoctbl { get; set; }
        public string Findeuda { get; set; }
        public string Cancela { get; set; }
        public int Libcompra { get; set; }
        public string Cambiodoc { get; set; }
        public string Remesa { get; set; }
        public string Forpag { get; set; }
        public int Tipdig { get; set; }
        public string Ordcomp { get; set; }
        public int Clbid { get; set; }
        public string Sinimp { get; set; }

        public int Codsuc { get; set; }
        public int Codemp { get; set; }

        public string Tipo { get; set; }
        public string PJ { get; set; }
        public string Pag { get; set; }

        public decimal Neto { get; set; }
        public decimal Impuestos { get; set; }
        public decimal Retenido { get; set; }
        public decimal Descuento { get; set; }
        public decimal Final { get; set; }
        public decimal Saldo { get; set; }
        public decimal Exento { get; set; }

        public string NumeroRol { get; set; }
        public string TipoRol { get; set; }
        public string NombreDeudor { get; set; }
        public string RutDeudor { get; set; }
        public string NombreSucursal { get; set; }
        public int Rolid { get; set; }
        public string Tribunal { get; set; }
        public string NombreTribunal { get; set; }
        public string Modo { get; set; }
        public string  Asegurados { get; set; }

        public string GastoJudicial { get; set; }
        public string DetalleGlosa { get; set; }

        public List<DetalleCabeceraComprobante> DetalleCC = new List<DetalleCabeceraComprobante>();
        public List<DetalleCabeceraComprobante> DetalleC = new List<DetalleCabeceraComprobante>();
        public List<DetalleCabeceraComprobante> DetalleV = new List<DetalleCabeceraComprobante>();

    }
}
