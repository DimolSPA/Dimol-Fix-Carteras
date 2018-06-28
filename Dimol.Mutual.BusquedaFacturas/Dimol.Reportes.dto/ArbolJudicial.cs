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
    public class ArbolJudicial
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
        public string PathArchivo { get; set; }
        public string[] Abogados { get; set; }

        public CabeceraReporte Encabezado = new CabeceraReporte();
        public TituloReporte Titulo = new TituloReporte();
        public List<ArbolJudicialBruto> lstArbol = new List<ArbolJudicialBruto>();
        public List<ArbolJudicialAbogado> lstArbolAbogado = new List<ArbolJudicialAbogado>();
        public List<ArbolJudicialCategoria> lstCategoria = new List<ArbolJudicialCategoria>();
        public ArbolJudicialTotales Total = new ArbolJudicialTotales();

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
    public class ArbolJudicialAbogado
    {
        public string Nombre { get; set; }

        public List<ArbolJudicialCategoria> lstCateg = new List<ArbolJudicialCategoria>();
        public ArbolJudicialTotales Tot = new ArbolJudicialTotales();
    }

    [Serializable]
    public class ArbolJudicialCategoria
    {
        public string Nombre { get; set; }

        public List<ArbolJudicialDetalle> lstDetalle = new List<ArbolJudicialDetalle>();
        public ArbolJudicialTotales Totales = new ArbolJudicialTotales();
    }

    [Serializable]
    public class ArbolJudicialDetalle
    {
        public string RutCliente { get; set; }
        public string NombreCliente { get; set; }
        public string RutDeudor { get; set; }
        public string NombreDeudor { get; set; }
        public string Rol { get; set; }
        public string Tribunal { get; set; }        
        public string Logica { get; set; }
        public string Categoria { get; set; }
        public string Abogado { get; set; }
        public string Asegurado { get; set; }
        public string TipoCausa { get; set; }
        public string MateriaJudicial { get; set; }
        public string EstadoRol { get; set; }
        public int DiasSinGestion { get; set; }

        [XmlIgnore]
        public decimal Saldo { get; set; }
        [XmlElement("Saldo")]
        public string SaldoStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Saldo.ToString("N2"); }
            set { this.Saldo = decimal.Parse(value); }
        }

        [XmlIgnore]
        public DateTime FechaJudicial { get; set; }
        [XmlElement("FechaJudicial")]
        public string FechaJudicialStr
        {
            get { return this.FechaJudicial.ToString("dd/MM/yyyy"); }
            set { this.FechaJudicial = DateTime.Parse(value); }
        }

        public int Cuenta { get; set; }        
    }

    [Serializable]
    public class ArbolJudicialTotales
    {
        public int CuentaConMov { get; set; }

        [XmlIgnore]
        public decimal TotalConMov { get; set; }
        [XmlElement("TotalConMov")]
        public string TotalConMovStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.TotalConMov.ToString("N0"); }
            set { this.TotalConMov = decimal.Parse(value); }
        }

        public int CuentaSinMov { get; set; }

        [XmlIgnore]
        public decimal TotalSinMov { get; set; }
        [XmlElement("TotalSinMov")]
        public string TotalSinMovStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.TotalSinMov.ToString("N0"); }
            set { this.TotalSinMov = decimal.Parse(value); }
        }

        public int CuentaTotal { get; set; }

        [XmlIgnore]
        public decimal Total { get; set; }
        [XmlElement("Total")]
        public string TotalStr 
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Total.ToString("N0"); }
            set { this.Total = decimal.Parse(value); }
        }

        [XmlIgnore]
        public decimal PorcTotalConMov { get; set; }
        [XmlElement("PorcTotalConMov")]
        public string PorcTotalConMovStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.PorcTotalConMov.ToString("N0"); }
            set { this.PorcTotalConMov = decimal.Parse(value); }
        }
    }

    [Serializable]
    public class ArbolJudicialBruto
    {
        public string RutCliente { get; set; }
        public string NombreCliente { get; set; }
        public string RutDeudor { get; set; }
        public string NombreDeudor { get; set; }
        public string Numero { get; set; }

        [XmlIgnore]
        public decimal Demandado { get; set; }
        [XmlElement("Demandado")]
        public string DemandadoStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Demandado.ToString("N2"); }
            set { this.Demandado = decimal.Parse(value); }
        }

        [XmlIgnore]
        public decimal Saldo { get; set; }
        [XmlElement("Saldo")]
        public string SaldoStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Saldo.ToString("N2"); }
            set { this.Saldo = decimal.Parse(value); }
        }

        public string MateriaJudicial { get; set; }
        public string EstadoRol { get; set; }
        public string Rol { get; set; }
        
        [XmlIgnore]
        public DateTime FechaJudicial { get; set; }
        [XmlElement("FechaJudicial")]
        public string FechaJudicialStr
        {
            get { return this.FechaJudicial.ToString("dd/MM/yyyy"); }
            set { this.FechaJudicial = DateTime.Parse(value); }
        }

        public string Tribunal { get; set; }
        public string CodigoCarga { get; set; }

        [XmlIgnore]
        public DateTime FechaAsignacion { get; set; }
        [XmlElement("FechaAsignacion")]
        public string FechaAsignacionStr
        {
            get { return this.FechaAsignacion.ToString("dd/MM/yyyy"); }
            set { this.FechaAsignacion = DateTime.Parse(value); }
        }

        public string Moneda { get; set; }
        public int Valido { get; set; }
        public string Abogado { get; set; }
        public string Categoria { get; set; }
        public int Cuenta { get; set; }
        public string Logica { get; set; }
        public string Asegurado { get; set; }
        public string TipoCausa { get; set; }
        public int DiasSinGestion { get; set; }

    }
}
