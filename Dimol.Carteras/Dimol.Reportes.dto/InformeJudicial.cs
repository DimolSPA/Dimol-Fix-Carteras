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
    public class InformeJudicial
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
        public int Codid { get; set; }
        public string CodigoCarga { get; set; }
        public string RutCliente { get; set; }
        public string NombreCliente { get; set; }

        public CabeceraReporte Encabezado = new CabeceraReporte();
        public TituloReporte Titulo = new TituloReporte();
        //public List<InformeJudicialDeudor> lstDocumentos = new List<InformeJudicialDeudor>();
        public List<InformeJudicialCodigoCarga> lstCodigoCarga = new List<InformeJudicialCodigoCarga>();
        public InformeJudicialTotales Totales = new InformeJudicialTotales();

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
        public string FechaLarga { get; set; }

    }
    
    [Serializable]
    public class InformeJudicialCodigoCarga
    {
        public int Codid { get; set; }
        public string CodigoCarga { get; set; }

        public List<InformeJudicialAsegurado> lstAsegurados = new List<InformeJudicialAsegurado>();
        public InformeJudicialTotales Totales = new InformeJudicialTotales();
        public List<InformeJudicialDeudor> lstDeudores = new List<InformeJudicialDeudor>();
    }
    
    [Serializable]
    public class InformeJudicialAsegurado
    {
        public string SubCartera { get; set; }
        public string RutSubCartera { get; set; }

        public List<InformeJudicialDeudor> lstDeudores = new List<InformeJudicialDeudor>();
        public InformeJudicialTotales Totales = new InformeJudicialTotales();
        public List<InformeJudicialCausa> lstCausas = new List<InformeJudicialCausa>();
    }
    
    [Serializable]
    public class InformeJudicialDeudor
    {
        public string RutDeudor { get; set; }
        public string DvDeudor { get; set; }
        public string RutDeudorFormateado { get; set; }
        public string NombreFantasia { get; set; }

        public List<InformeJudicialCausa> lstCausas = new List<InformeJudicialCausa>();
        public InformeJudicialTotales Totales = new InformeJudicialTotales();
        public List<InformeJudicialAsegurado> lstAsegurados = new List<InformeJudicialAsegurado>();
    }

    [Serializable]
    public class InformeJudicialCausa
    {
        public string Ciudad { get; set; }
        public string Region { get; set; }
        public string Estado { get; set; }
        public string Resumen { get; set; }

        public string Causa { get; set; }
        public string Juzgado { get; set; }
        public string NumeroRol { get; set; }
        public string Materia { get; set; }

        public string Sii { get; set; }
        public int PoderJud { get; set; }
        public int Terreno { get; set; }

        [XmlIgnore]
        public DateTime UltimaGestion { get; set; }
        [XmlElement("UltimaGestion")]
        public string UltimaGestionStr
        {
            get { return this.UltimaGestion.ToString("dd-MM-yyyy"); }
            set { this.UltimaGestion = DateTime.Parse(value); }
        }
        public int Ctcid { get; set; }
        public int Pclid { get; set; }

        public List<InformeJudicialDetalle> lstDetallesPesos = new List<InformeJudicialDetalle>();
        public List<InformeJudicialDetalle> lstDetallesDolares = new List<InformeJudicialDetalle>();
        public List<InformeJudicialDetalle> lstDetallesUF = new List<InformeJudicialDetalle>();
        public InformeJudicialTotales TotalesPesos = new InformeJudicialTotales();
        public InformeJudicialTotales TotalesDolar = new InformeJudicialTotales();
        public InformeJudicialTotales TotalesUF = new InformeJudicialTotales();
        public InformeJudicialTotales Totales = new InformeJudicialTotales();
        //public List<InformeJudicialAsegurado> lstAsegurados = new List<InformeJudicialAsegurado>();
    }
    
    [Serializable]
    public class InformeJudicialDetalle
    {
        public string TipoDocumento { get; set; }
        public string Numero { get; set; }
        [XmlIgnore]
        public DateTime FechaVencimiento { get; set; }
        [XmlElement("FechaVencimiento")]
        public string FechaVencimientoStr
        {
            get { return this.FechaVencimiento.ToString("dd-MM-yyyy"); }
            set { this.FechaVencimiento = DateTime.Parse(value); }
        }
        public string Moneda { get; set; }
        public string UltimoEstado { get; set; }
        [XmlIgnore]
        public decimal TipoCambio { get; set; }
        [XmlElement("TipoCambio")]
        public string TipoCambioStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.TipoCambio.ToString("N2"); }
            set { this.TipoCambio = decimal.Parse(value); }
        }
        [XmlIgnore]
        public decimal Capital { get; set; }
        [XmlElement("Capital")]
        public string CapitalStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Capital.ToString("N2"); }
            set { this.Capital = decimal.Parse(value); }
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
    }

    [Serializable]
    public class InformeJudicialTotales
    {
        public int Cantidad { get; set; }
        public int NroHijos { get; set; }
        [XmlIgnore]
        public decimal Capital { get; set; }
        [XmlElement("Capital")]
        public string CapitalStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Capital.ToString("N2"); }
            set { this.Capital = decimal.Parse(value); }
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
    }
}
