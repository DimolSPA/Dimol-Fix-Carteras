using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dimol.Carteras.Models
{
    public class CastigoDevolucionModel
    {
        [DisplayName("Cliente")]
        public string NombreRutCliente { get; set; }
        public int Pclid { get; set; }
        [DisplayName("RUT")]
        [StringLength(1024)]
        public string NombreRutDeudor { get; set; }
        [DisplayName("ctcid")]
        public int Ctcid { get; set; }
        [DisplayName("Estado")]
        [StringLength(1024)]
        public string Estado { get; set; }
        [DisplayName("Tipo")]
        public string Tpo { get; set; }


        public string Cartera { get; set; }
        public string Tipo { get; set; }

        public string Accion {
            get
            {
                switch (Tipo) {
                    case "C": 
                        return "Castigar";
                    case  "D":
                        return "Devolver";
                    default:
                        return "";
                } 
            }
        }
        public string DocumentosSeleccionados { get; set; }
        public string Ids { get; set; }
        public string IdMotivos { get; set; }
        [DisplayName("Tipo Comprobante")]
        public int TipoComprobante { get; set; }


        public int Ccbid { get; set; }
        [DisplayName("Número Interno")]
        public int CabeceraId { get; set; }
        [DisplayName("Receptor")]
        public string RutCliente { set; get; }
        public string NombreCliente { get; set; }
        [DisplayName("Tipo")]
        public string TipoComprobanteDesc { get; set; }
        [DisplayName("Sucursal")]
        public string Sucursal { get; set; }
        [DisplayName("Número")]
        public string Numero { get; set; }
        [DisplayName("Número OC")]
        public string NumeroOC { get; set; }
        [DisplayName("Fecha Ingreso")]
        public DateTime FechaIngreso { get; set; }
        [DisplayName("Fecha Documento")]
        public DateTime FechaDocumento { get; set; }
        [DisplayName("Fecha Vencimiento")]
        public DateTime FechaVencimiento { get; set; }
        [DisplayName("Fecha OC")]
        public DateTime FechaOrdenCompra { get; set; }
        [DisplayName("Fecha Entrega")]
        public DateTime FechaEntrega { get; set; }
        [DisplayName("Fecha Contab.")]
        public DateTime FechaContabilizacion { get; set; }
        [DisplayName("Moneda")]
        public string Moneda { get; set; }
        [DisplayName("Tipo Cambio")]
        public decimal TipoCambio { get; set; }
        [DisplayName("Forma de Pago")]
        public string FormaPago { get; set; }
        [DisplayName("Tipo Gasto")]
        public string TipoGasto { get; set; }
        [DisplayName("MotivoCobranza")]
        public string MotivoCobranza { get; set; }
        [DisplayName("Glosa")]
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

        public string PJ { get; set; }
        public string Pag { get; set; }

        public int Item { get; set; }
        public int Insid { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Monto { get; set; }
        public string Cantidad { get; set; }
        [DisplayName("Imp. Retenido")]
        public bool ImpuestoRetenido { get; set; }

        [DisplayName("Tribunal")]
        public string Tribunal { get; set; }
        [DisplayName("Tribunal")]
        public string NombreTribunal { get; set; }
        [DisplayName("Tipo Rol")]
        public string TipoRol { get; set; }
        [DisplayName("Rol")]
        public string Rol { get; set; }
        [DisplayName("Año")]
        public int Anio { get; set; }
        public int Rolid { get; set; }

        [DisplayName("Total Bruto")]
        public string Subtotal { get; set; }
        [DisplayName("Descuento")]
        public string Descuento { get; set; }
        [DisplayName("Neto")]
        public string Neto { get; set; }
        [DisplayName("Impuesto")]
        public string Impuestos { get; set; }
        [DisplayName("Imp. Retenido")]
        public string Retenido { get; set; }
        [DisplayName("Total")]
        public string Total { get; set; }
        [DisplayName("Final")]
        public string Final { get; set; }
        [DisplayName("Saldo")]
        public string Saldo { get; set; }
        [DisplayName("Exeno")]
        public string Exento { get; set; }

        public string Modo { get; set; }

        [DisplayName("Cliente")]
        public string RutNombreCliente { get; set; }
        public int PclidCliente { get; set; }
        [DisplayName("Deudor")]
        public string RutNombreDeudor { get; set; }
        [DisplayName("Asegurados")]
        public string Asegurados { get; set; }
        
    }
}