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
    public class ReporteCancelacion
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

        public string RutDeudor { get; set; }
        public string NombreDeudor { get; set; }

        public CabeceraReporte Encabezado = new CabeceraReporte();
        public TituloReporte Titulo = new TituloReporte();
        public List<CancelacionCliente> ListaCliente = new List<CancelacionCliente>();
        public CancelacionTotales Totales = new CancelacionTotales();

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
    public class CancelacionCliente
    {
        public string RutCliente { get; set; }
        public string NombreCliente { get; set; }
        public List<CancelacionClienteFecha> ListaFecha = new List<CancelacionClienteFecha>();

    }

    [Serializable]
    public class CancelacionClienteFecha
    {
        [XmlIgnore]
        public DateTime FechaCancelacion { get; set; }
        [XmlElement("FechaCancelacion")]
        public string FechaCancelacionStr
        {
            get { return this.FechaCancelacion.ToString("dd-MM-yyyy"); }
            set { this.FechaCancelacion = DateTime.Parse(value); }
        }
        public List<CancelacionClienteTipo> ListaTipo = new List<CancelacionClienteTipo>();
        public CancelacionTotales TotalFecha = new CancelacionTotales();

    }

    [Serializable]
    public class CancelacionClienteTipo
    {
        public string Tipo { get; set; }
        public List<CancelacionClienteNumero> ListaNumero = new List<CancelacionClienteNumero>();
        public CancelacionTotales TotalTipo = new CancelacionTotales();
    }

    [Serializable]
    public class CancelacionClienteNumero
    {
        public string Numero { get; set; }
        public List<CancelacionDetalle> ListaDocumentos = new List<CancelacionDetalle>();
        public CancelacionTotales TotalNumero = new CancelacionTotales();
    }


    [Serializable]
    public class CancelacionDetalle
    {
        public string TipoDocumento { get; set; }
        public string Numero { get; set; }
       
        [XmlIgnore]
        public decimal Capital { get; set; }
        [XmlElement("Capital")]
        public string CapitalStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Capital.ToString("N2"); }
            set { this.Capital = decimal.Parse(value); }
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
        public decimal GastoPrejudicial { get; set; }
        [XmlElement("GastoPrejudicial")]
        public string GastoPrejudicialStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.GastoPrejudicial.ToString("N2"); }
            set { this.GastoPrejudicial = decimal.Parse(value); }
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
        public decimal Total { get; set; }
        [XmlElement("Total")]
        public string TotalStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Total.ToString("N2"); }
            set { this.Total = decimal.Parse(value); }
        }       
    }

    [Serializable]
    public class CancelacionTotales
    {
        [XmlIgnore]
        public decimal Capital { get; set; }
        [XmlElement("Capital")]
        public string CapitalStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Capital.ToString("N2"); }
            set { this.Capital = decimal.Parse(value); }
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
        public decimal GastoPrejudicial { get; set; }
        [XmlElement("GastoPrejudicial")]
        public string GastoPrejudicialStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.GastoPrejudicial.ToString("N2"); }
            set { this.GastoPrejudicial = decimal.Parse(value); }
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
        public decimal Total { get; set; }
        [XmlElement("Total")]
        public string TotalStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Total.ToString("N2"); }
            set { this.Total = decimal.Parse(value); }
        }       
    }
}
