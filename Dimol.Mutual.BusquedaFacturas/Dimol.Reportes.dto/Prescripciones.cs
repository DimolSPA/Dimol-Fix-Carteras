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
    public class Prescripciones
    {
        public int Codemp { get; set; }
        public int Codsuc { get; set; }
        public int Pclid { get; set; }
        public int Idioma { get; set; }
        public int TipoCartera { get; set; }
        public int Ctcid { get; set; }
        public char Estcpbt { get; set; }
        public string PathArchivo { get; set; }
        public int DiasPrescrip { get; set; }

        public CabeceraReporte Encabezado = new CabeceraReporte();
        public TituloReporte Titulo = new TituloReporte();
        public List<PrescripcionesBruto> lstPrescr = new List<PrescripcionesBruto>();
        public PrescripcionesTotales Total = new PrescripcionesTotales();

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
    public class PrescripcionesTotales
    {        
        [XmlIgnore]
        public decimal Total { get; set; }
        [XmlElement("Total")]
        public string TotalStr 
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Total.ToString("N0"); }
            set { this.Total = decimal.Parse(value); }
        }
    }

    [Serializable]
    public class PrescripcionesBruto
    {
        public string RutCliente { get; set; }
        public string NombreCliente { get; set; }
        public string RutDeudor { get; set; }
        public string NombreDeudor { get; set; }
        public string Rol { get; set; }
        public string Tribunal { get; set; }
        public string TipoCausa { get; set; }
        
        [XmlIgnore]
        public DateTime FechaJudicial { get; set; }
        [XmlElement("FechaJudicial")]
        public string FechaJudicialStr
        {
            get { return this.FechaJudicial.ToString("dd/MM/yyyy"); }
            set { this.FechaJudicial = DateTime.Parse(value); }
        }

        public string MateriaJudicial { get; set; }
        public string EstadoRol { get; set; }
        public string Numero { get; set; }

        [XmlIgnore]
        public DateTime FechaVencimiento { get; set; }
        [XmlElement("FechaVencimiento")]
        public string FechaVencimientoStr
        {
            get { return this.FechaVencimiento.ToString("dd/MM/yyyy"); }
            set { this.FechaVencimiento = DateTime.Parse(value); }
        }

        [XmlIgnore]
        public DateTime FechaPrescripcion { get; set; }
        [XmlElement("FechaPrescripcion")]
        public string FechaPrescripcionStr
        {
            get { return this.FechaPrescripcion.ToString("dd/MM/yyyy"); }
            set { this.FechaPrescripcion = DateTime.Parse(value); }
        }

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

        public string CodigoCarga { get; set; }
        public string Asegurado { get; set; }
        public string Abogado { get; set; }      
        
    }
}
