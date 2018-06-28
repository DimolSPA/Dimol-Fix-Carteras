using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Dimol.Reportes.dto
{
    public class Liquidacion
    {
        public int Codemp { get; set; }
        public int Pclid { get; set; }
        public int Ctcid { get; set; }
        public int Sucid { get; set; }
        public int Idioma { get; set; }
        public int TipoCartera { get; set; }
        public string EstadoCpbt { get; set; }
        public string NombreArchivo { get; set; }
        public string PathArchivo { get; set; }

        public string RutCliente { get; set; }
        public string NombreCliente { get; set; }

        public CabeceraReporte Encabezado = new CabeceraReporte();
        public TituloReporte Titulo = new TituloReporte();
        public List<LiquidacionDeudor> lstDocumentos = new List<LiquidacionDeudor>();
        public LiquidacionTotales Totales = new LiquidacionTotales();

        [XmlIgnore]
        public DateTime FechaReporte { get; set; }

        [XmlElement("FechaReporte")]
        public string FechaEmision
        {
            get { return this.FechaReporte.ToString("dd-MM-yyyy HH:mm:ss"); }
            set { this.FechaReporte = DateTime.Parse(value); }
        }
        [XmlIgnore]
        public DateTime FechaEmisionCorta { get; set; }
        [XmlElement("FechaEmision")]
        public string FechaEmisionCortaStr
        {
            get { return this.FechaEmisionCorta.ToString("dd-MM-yyyy"); }
            set { this.FechaEmisionCorta = DateTime.Parse(value); }
        }
        public string RutUsuario { get; set; }
        public string NombreUsuario { get; set; }

        public int IdReporte { get; set; }
        public int Pagina { get; set; }

    }
    [Serializable]
    public class LiquidacionDeudor
    {
        public int RutDeudor { get; set; }
        public string DvDeudor { get; set; }
        public string RutDeudorFormateado { get; set; }
        public string NombreFantasia { get; set; }
        public string Ciudad { get; set; }
        public string Comuna { get; set; }
        public string Region { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string CodigoPostal { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string Gestor { get; set; }
        public string SubCartera { get; set; }
        public string RutSubCartera { get; set; }
        
        public List<LiquidacionDetalle> lstDetallesPesos = new List<LiquidacionDetalle>();
        public List<LiquidacionDetalle> lstDetallesDolares = new List<LiquidacionDetalle>();
        public List<LiquidacionDetalle> lstDetallesUF = new List<LiquidacionDetalle>();
        public LiquidacionTotales TotalesPesos = new LiquidacionTotales();
        public LiquidacionTotales TotalesDolar = new LiquidacionTotales();
        public LiquidacionTotales TotalesUF = new LiquidacionTotales();
    }

    [Serializable]
    public class LiquidacionDetalle
    {
        public string TipoDocumento { get; set; }
        public string Numero { get; set; }
        [XmlIgnore]
        public DateTime FechaEmision { get; set; }
        [XmlElement("FechaEmision")]
        public string FechaEmisionStr
        {
            get { return this.FechaEmision.ToString("dd-MM-yyyy"); }
            set { this.FechaEmision = DateTime.Parse(value); }
        }
        [XmlIgnore]
        public DateTime FechaVencimiento { get; set; }
        [XmlElement("FechaVencimiento")]
        public string FechaVencimientoStr
        {
            get { return this.FechaVencimiento.ToString("dd-MM-yyyy"); }
            set { this.FechaVencimiento = DateTime.Parse(value); }
        }
        public int DiasVencido { get; set; }

        [XmlIgnore]
        public decimal Monto { get; set; }
        [XmlElement("Monto")]
        public string MontoStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Monto.ToString("N2"); }
            set { this.Monto = decimal.Parse(value); }
        }
        [XmlIgnore]
        public decimal Saldo { get; set; }
        [XmlElement("Saldo")]
        public string SaldoStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Saldo.ToString("N2"); }
            set { this.Saldo = decimal.Parse(value); }
        }
       
        public string Negocio { get; set; }
        public string Moneda { get; set; }
       
    }

    [Serializable]
    public class LiquidacionTotales
    {
        [XmlIgnore]
        public decimal Monto { get; set; }
        [XmlElement("Monto")]
        public string MontoStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Monto.ToString("N2"); }
            set { this.Monto = decimal.Parse(value); }
        }
        [XmlIgnore]
        public decimal Saldo { get; set; }
        [XmlElement("Saldo")]
        public string SaldoStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Saldo.ToString("N2"); }
            set { this.Saldo = decimal.Parse(value); }
        }
        public int CantidadDocumentos { get; set; }
    }
}
