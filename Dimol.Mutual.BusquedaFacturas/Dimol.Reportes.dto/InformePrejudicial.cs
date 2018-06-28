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
    public class InformePrejudicial
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
        public List<InformePrejudicialDeudor> lstDocumentos = new List<InformePrejudicialDeudor>();
        public InformePrejudicialTotales Totales = new InformePrejudicialTotales();
        public List<InformePrejudicialAsegurado> lstAsegurados = new List<InformePrejudicialAsegurado>();

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
    public class InformePrejudicialDeudor
    {
        public string RutDeudor { get; set; }
        public string DvDeudor { get; set; }
        public string RutDeudorFormateado { get; set; }
        public string NombreFantasia { get; set; }
        public string Ciudad { get; set; }
        public string Comuna { get; set; }
        public string Region { get; set; }
        public string Pais { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string CodigoPostal { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string Gestion { get; set; }
        public string Resumen { get; set; }
        [XmlIgnore]
        public DateTime UltimaGestion { get; set; }
        [XmlElement("UltimaGestion")]
        public string UltimaGestionStr
        {
            get { return this.UltimaGestion.ToString("dd-MM-yyyy"); }
            set { this.UltimaGestion = DateTime.Parse(value); }
        }
        public decimal Ctcid { get; set; }
        public decimal Pclid { get; set; }
        //public List<InformePrejudicialAsegurado> lstAsegurados = new List<InformePrejudicialAsegurado>();
        public List<InformePrejudicialNegocio> lstNegocios = new List<InformePrejudicialNegocio>();
        public InformePrejudicialTotales Totales = new InformePrejudicialTotales();
    }

    [Serializable]
    public class InformePrejudicialAsegurado
    {
        public string SubCartera { get; set; }
        public string RutSubCartera { get; set; }

        public List<InformePrejudicialDeudor> lstDocumentos = new List<InformePrejudicialDeudor>();
        public InformePrejudicialTotales Totales = new InformePrejudicialTotales();
    }

    [Serializable]
    public class InformePrejudicialNegocio
    {
        public string Negocio { get; set; }

        public List<InformePrejudicialDetalle> lstDetallesPesos = new List<InformePrejudicialDetalle>();
        public List<InformePrejudicialDetalle> lstDetallesDolares = new List<InformePrejudicialDetalle>();
        public List<InformePrejudicialDetalle> lstDetallesUF = new List<InformePrejudicialDetalle>();
        public InformePrejudicialTotales TotalesPesos = new InformePrejudicialTotales();
        public InformePrejudicialTotales TotalesDolar = new InformePrejudicialTotales();
        public InformePrejudicialTotales TotalesUF = new InformePrejudicialTotales();
        public InformePrejudicialTotales Totales = new InformePrejudicialTotales();
    }

    [Serializable]
    public class InformePrejudicialDetalle
    {
        public string TipoDocumento { get; set; }
        public string Numero { get; set; }
        [XmlIgnore]
        public DateTime FechaIngreso { get; set; }
        [XmlElement("FechaIngreso")]
        public string FechaIngresoStr
        {
            get { return this.FechaIngreso.ToString("dd-MM-yyyy"); }
            set { this.FechaIngreso = DateTime.Parse(value); }
        }
        [XmlIgnore]
        public DateTime FechaVencimiento { get; set; }
        [XmlElement("FechaVencimiento")]
        public string FechaVencimientoStr
        {
            get { return this.FechaVencimiento.ToString("dd-MM-yyyy"); }
            set { this.FechaVencimiento = DateTime.Parse(value); }
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
        public decimal Asignado { get; set; }
        [XmlElement("Asignado")]
        public string AsignadoStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Asignado.ToString("N2"); }
            set { this.Asignado = decimal.Parse(value); }
        }
        [XmlIgnore]
        public decimal Abono { get; set; }
        [XmlElement("Abono")]
        public string AbonoStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Abono.ToString("N2"); }
            set { this.Abono = decimal.Parse(value); }
        }
        [XmlIgnore]
        public decimal Saldo { get; set; }
        [XmlElement("Saldo")]
        public string SaldoStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Saldo.ToString("N2"); }
            set { this.Saldo = decimal.Parse(value); }
        }
        //public string Negocio { get; set; }
        public string Moneda { get; set; }
        public string UltimoEstado { get; set; }
    }

    [Serializable]
    public class InformePrejudicialTotales
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
        public decimal Gasto { get; set; }
        [XmlElement("Gasto")]
        public string GastoStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Gasto.ToString("N2"); }
            set { this.Gasto = decimal.Parse(value); }
        }
        [XmlIgnore]
        public decimal Asignado { get; set; }
        [XmlElement("Asignado")]
        public string AsignadoStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Asignado.ToString("N2"); }
            set { this.Asignado = decimal.Parse(value); }
        }
        [XmlIgnore]
        public decimal Abono { get; set; }
        [XmlElement("Abono")]
        public string AbonoStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Abono.ToString("N2"); }
            set { this.Abono = decimal.Parse(value); }
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
        public decimal Total { get; set; }
        [XmlElement("Total")]
        public string TotalStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Total.ToString("N2"); }
            set { this.Total = decimal.Parse(value); }
        }

        public int Cantidad { get; set; }
    }
}
