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
    public class LiquidacionDura
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
        public List<LiquidacionDuraDeudor> lstDocumentos = new List<LiquidacionDuraDeudor>();
        public LiquidacionDuraTotales Totales = new LiquidacionDuraTotales();

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
    public class LiquidacionDuraDeudor
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

        [XmlIgnore]
        public decimal CambioDolar { get; set; }
        [XmlElement("CambioDolar")]
        public string CambioDolarStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.CambioDolar.ToString("N2"); }
            set { this.CambioDolar = decimal.Parse(value); }
        }
        [XmlIgnore]
        public decimal CambioUF { get; set; }
        [XmlElement("CambioUF")]
        public string CambioUFStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.CambioUF.ToString("N2"); }
            set { this.CambioUF = decimal.Parse(value); }
        }
        

        public List<LiquidacionDuraAsegurado> lstAsegurados = new List<LiquidacionDuraAsegurado>();
    }

    [Serializable]
    public class LiquidacionDuraAsegurado
    {
        public string SubCartera { get; set; }
        public string RutSubCartera { get; set; }
        public string IdDivision { get; set; }
        public string Division { get; set; }
        public string Tasa { get; set; }

        public List<LiquidacionDuraDetalle> lstDetallesPesos = new List<LiquidacionDuraDetalle>();
        public List<LiquidacionDuraDetalle> lstDetallesDolares = new List<LiquidacionDuraDetalle>();
        public List<LiquidacionDuraDetalle> lstDetallesUF = new List<LiquidacionDuraDetalle>();
        public LiquidacionDuraTotales TotalesPesos = new LiquidacionDuraTotales();
        public LiquidacionDuraTotales TotalesDolar = new LiquidacionDuraTotales();
        public LiquidacionDuraTotales TotalesUF = new LiquidacionDuraTotales();
        public LiquidacionDuraTotales Totales = new LiquidacionDuraTotales();
    }

    [Serializable]
    public class LiquidacionDuraDetalle
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
        [XmlIgnore]
        public decimal Intereses { get; set; }
        [XmlElement("Intereses")]
        public string InteresesStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Intereses.ToString("N2"); }
            set { this.Intereses = decimal.Parse(value); }
        }
        [XmlIgnore]
        public decimal Honorarios { get; set; }
        [XmlElement("Honorarios")]
        public string HonorariosStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Honorarios.ToString("N2"); }
            set { this.Honorarios = decimal.Parse(value); }
        }
        [XmlIgnore]
        public decimal Gasto { get; set; }
        [XmlElement("Gasto")]
        public string GastoStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Gasto.ToString("N2"); }
            set { this.Gasto = decimal.Parse(value); }
        }
        [XmlIgnore]
        public decimal Total { get; set; }
        [XmlElement("Total")]
        public string TotalStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Total.ToString("N2"); }
            set { this.Total = decimal.Parse(value); }
        }
        public string Negocio { get; set; }
        public string Moneda { get; set; }
        public string Estado { get; set; }
        public string Motivo { get; set; }
       
    }

    [Serializable]
    public class LiquidacionDuraTotales
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
        [XmlIgnore]
        public decimal Intereses { get; set; }
        [XmlElement("Intereses")]
        public string InteresesStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Intereses.ToString("N2"); }
            set { this.Intereses = decimal.Parse(value); }
        }
        [XmlIgnore]
        public decimal Honorarios { get; set; }
        [XmlElement("Honorarios")]
        public string HonorariosStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Honorarios.ToString("N2"); }
            set { this.Honorarios = decimal.Parse(value); }
        }
        [XmlIgnore]
        public decimal Gasto { get; set; }
        [XmlElement("Gasto")]
        public string GastoStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Gasto.ToString("N2"); }
            set { this.Gasto = decimal.Parse(value); }
        }
        [XmlIgnore]
        public decimal Total { get; set; }
        [XmlElement("Total")]
        public string TotalStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Total.ToString("N2"); }
            set { this.Total = decimal.Parse(value); }
        }
        [XmlIgnore]
        public decimal GastoJudicial { get; set; }
        [XmlElement("GastoJudicial")]
        public string GastoJudicialStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.GastoJudicial.ToString("N2"); }
            set { this.GastoJudicial = decimal.Parse(value); }
        }
        [XmlIgnore]
        public decimal GastoPreJudicial { get; set; }
        [XmlElement("GastoPreJudicial")]
        public string GastoPreJudicialStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.GastoPreJudicial.ToString("N2"); }
            set { this.GastoPreJudicial = decimal.Parse(value); }
        }
    }
}
