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
    [Serializable]
    public class RecepcionDocumentos
    {
        public int Codemp { get; set; }
        public int Codsuc { get; set; }
        public int Pclid { get; set; }
        public int Idioma { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public int TipoCartera { get; set; }
        public int Ctcid { get; set; }
        public char Estcpbt { get; set; }

        public CabeceraReporte Encabezado = new CabeceraReporte();
        public TituloReporte Titulo = new TituloReporte();
        public List<RecepcionDocumentosPorIngreso> lstDocsPorIngr = new List<RecepcionDocumentosPorIngreso>();

        [XmlIgnore]
        public DateTime FechaReporte { get; set; }

        [XmlElement("FechaReporte")]
        public string FechaEmision
        {
            get { return this.FechaReporte.ToString("dd/MM/yyyy HH:mm:ss"); }
            set { this.FechaReporte = DateTime.Parse(value); }
        }

        public string RutUsuario { get; set; }
        public string NombreUsuario { get; set; }
        [XmlIgnore]
        public DateTime FechaRecepcion { get; set; }

        [XmlElement("FechaRecepcion")]
        public string FechaRecepcionStr
        {
            get { return this.FechaRecepcion.ToString("dd/MM/yyyy"); }
            set { this.FechaRecepcion = DateTime.Parse(value); }
        }

    }
    [Serializable]
    public class RecepcionDocumentosDetalle
    {
        public string RutCliente { get; set; }
        public string NombreCliente { get; set; }
        public int RutDeudor { get; set; }
        public string DvDeudor { get; set; }
        public string RutDeudorFormateado { get; set; }
        //public string NombreFantasia  { get; set; }
        public string TipoDocumento { get; set; }
        public string Numero { get; set; }
        [XmlIgnore]
        public DateTime FechaDocumento { get; set; }
        [XmlElement("FechaDocumento")]
        public string FechaDocumentoStr
        {
            get { return this.FechaDocumento.ToString("dd/MM/yyyy"); }
            set { this.FechaDocumento = DateTime.Parse(value); }
        }
        [XmlIgnore]
        public DateTime FechaVencimiento { get; set; }
        [XmlElement("FechaVencimiento")]
        public string FechaVencimientoStr
        {
            get { return this.FechaVencimiento.ToString("dd/MM/yyyy"); }
            set { this.FechaVencimiento = DateTime.Parse(value); }
        }
        public string  Moneda { get; set; }
        [XmlIgnore]
        public decimal Monto { get; set; }
        [XmlElement("Monto")]
        public string MontoStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Monto.ToString("N2"); }
            set { this.Monto = decimal.Parse(value); }
        }
        [XmlIgnore]
        public decimal Asignado { get; set; }
        [XmlElement("Asignado")]
        public string AsignadoStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Asignado.ToString("N2"); }
            set { this.Asignado = decimal.Parse(value); }
        }
        public string RutSubCartera { get; set; }
        public string  SubCartera { get; set; }
        public string CodigoCarga { get; set; }
        public string MotivoCobranza { get; set; }
        public string DocumentoOriginal { get; set; }
        public decimal Saldo { get; set; }
        public decimal Diferencia { get; set; }

        [XmlIgnore]
        public DateTime FechaIngreso { get; set; }
        [XmlElement("FechaIngreso")]
        public string FechaIngresoStr
        {
            get { return this.FechaIngreso.ToString("dd/MM/yyyy"); }
            set { this.FechaIngreso = DateTime.Parse(value); }
        }
    }

    [Serializable]
    public class RecepcionDocumentosTotales
    {
        [XmlIgnore]
        public decimal Total { get; set; }
        [XmlElement("Total")]
        public string TotalStr 
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Total.ToString("N2"); }
            set { this.Total = decimal.Parse(value); }
        }
    }

    [Serializable]
    public class RecepcionDocumentosPorIngreso
    {
        [XmlIgnore]
        public DateTime FechaIngreso { get; set; }
        [XmlElement("FechaIngreso")]
        public string FechaIngresoStr
        {
            get { CultureInfo ci = new CultureInfo("es-CL"); return this.FechaIngreso.ToString("dd") + " de " + ci.DateTimeFormat.GetMonthName(this.FechaIngreso.Month) + " de " + this.FechaIngreso.ToString("yyyy"); }
            set { this.FechaIngreso = DateTime.Parse(value); }
        }

        public List<RecepcionDocumentosDetalle> lstDocumentos = new List<RecepcionDocumentosDetalle>();
        public RecepcionDocumentosTotales Totales = new RecepcionDocumentosTotales();
    }
}
