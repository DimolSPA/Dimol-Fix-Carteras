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
    public class MutualManual
    {
        public int Codemp { get; set; }
        public int Pclid { get; set; }
        public int Ctcid { get; set; }
        public int Sucid { get; set; }
        public int IdReporte { get; set; }
        public int Pagina { get; set; }
        public int Idioma { get; set; }
        public string PathArchivo { get; set; }
        public string Ruta { get; set; }

        public List<MutualManualCliente> lstCli = new List<MutualManualCliente>();

        // public CabeceraReporte Encabezado = new CabeceraReporte();
        // public TituloReporte Titulo = new TituloReporte();
        // public List<ResumenGestionesDetalle> lstDocumentos = new List<ResumenGestionesDetalle>();

        [XmlIgnore]
        public DateTime FechaReporte { get; set; }

        [XmlElement("FechaReporte")]
        public string FechaEmision
        {
            get { return this.FechaReporte.ToString("dd/MM/yyyy HH:mm:ss"); }
            set { this.FechaReporte = DateTime.Parse(value); }
        }

    }
    [Serializable]
    public class MutualManualBruto
    {
        public string Rut { get; set; }
        public string Nombre { get; set; }
        public string Factura { get; set; }

        [XmlIgnore]
        public DateTime Fecha { get; set; }

        [XmlElement("Fecha")]
        public string FechaStr
        {
            get { return this.Fecha.ToString("dd/MM/yyyy"); }
            set { this.Fecha = DateTime.Parse(value); }
        }

        public string Prestacion { get; set; }
        public string Agencia { get; set; }

        [XmlIgnore]
        public decimal Monto { get; set; }
        [XmlElement("Monto")]
        public string MontoStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Monto.ToString("N0"); }
            set { this.Monto = decimal.Parse(value); }
        }
    }

    [Serializable]
    public class MutualManualCliente
    {
        public string Rut { get; set; }
        public string Nombre { get; set; }

        public List<MutualManualDetalle> lstDetalle = new List<MutualManualDetalle>();
        public MutualManualTotales Totales = new MutualManualTotales();
    }

    [Serializable]
    public class MutualManualDetalle
    {
        public string Factura { get; set; }

        [XmlIgnore]
        public DateTime Fecha { get; set; }

        [XmlElement("Fecha")]
        public string FechaStr
        {
            get { return this.Fecha.ToString("dd/MM/yyyy"); }
            set { this.Fecha = DateTime.Parse(value); }
        }

        public string Prestacion { get; set; }
        public string Agencia { get; set; }

        [XmlIgnore]
        public decimal Monto { get; set; }
        [XmlElement("Monto")]
        public string MontoStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Monto.ToString("N0"); }
            set { this.Monto = decimal.Parse(value); }
        }
    }

    [Serializable]
    public class MutualManualTotales
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
}
