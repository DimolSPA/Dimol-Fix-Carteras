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
    public class CastigoJudicialAsegurado
    {
        public int Codemp { get; set; }
        public int Codsuc { get; set; }
        public int Tpcid { get; set; }
        public string RutDeudor { get; set; }
        public string RutAsegurado { get; set; }
        public int Idioma { get; set; }
        public string RutaOrigen { get; set; }
        public string RutaDestino { get; set; }

        public List<CastigoJudicialAseguradoDetalle> lstDetalle = new List<CastigoJudicialAseguradoDetalle>();
        
        [XmlIgnore]
        public DateTime FechaReporte { get; set; }

        [XmlElement("FechaReporte")]
        public string FechaReporteStr
        {
            get { CultureInfo ci = new CultureInfo("es-CL"); return this.FechaReporte.ToString("dd") + " de " + ci.DateTimeFormat.GetMonthName(this.FechaReporte.Month) + " de " + this.FechaReporte.ToString("yyyy"); }
            set { this.FechaReporte = DateTime.Parse(value); }
        }

        [XmlIgnore]
        public DateTime FechaComprobante { get; set; }

        [XmlElement("FechaComprobante")]
        public string FechaComprobanteStr
        {
            get { CultureInfo ci = new CultureInfo("es-CL"); return ci.DateTimeFormat.GetDayName(this.FechaComprobante.DayOfWeek) + ", " + this.FechaComprobante.ToString("dd") + " " + ci.DateTimeFormat.GetMonthName(this.FechaComprobante.Month) + ", " + this.FechaComprobante.ToString("yyyy"); }
            set { this.FechaComprobante = DateTime.Parse(value); }
        }

        public string NombreDeudor { get; set; }
        public string NombreAsegurado { get; set; }
        
        [XmlIgnore]
        public decimal TotalCastigo { get; set; }
        [XmlElement("TotalCastigo")]
        public string TotalCastigoStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.TotalCastigo.ToString("N0"); }
            set { this.TotalCastigo = decimal.Parse(value); }
        }

    }
      
        
    [Serializable]
    public class CastigoJudicialAseguradoDetalle
    {
        public string TipoCausa { get; set; }
        public string NroComprobante { get; set; }
        public string NombreJuzgado { get; set; }
        public int CodMoneda { get; set; }

        [XmlIgnore]
        public decimal SubTotalCastigo { get; set; }
        [XmlElement("SubTotalCastigo")]
        public string SubTotalCastigoStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.SubTotalCastigo.ToString("N0"); }
            set { this.SubTotalCastigo = decimal.Parse(value); }
        }
        
        public string Rol { get; set; }
        public string TipoDoc { get; set; }
        
    }

    [Serializable]
    public class CastigoJudicialAseguradoBruto
    {

        public string TipoCausa { get; set; }
        public string NroComprobante { get; set; }
        public string NombreJuzgado { get; set; }
        public string Rol { get; set; }
        public string TipoDoc { get; set; }
        public int CodMoneda { get; set; }

        [XmlIgnore]
        public decimal Saldo { get; set; }
        [XmlElement("Saldo")]
        public string SaldoStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Saldo.ToString("N2"); }
            set { this.Saldo = decimal.Parse(value); }
        }

    }
}