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
    public class BuscarFactura
    {
        public int Codemp { get; set; }
        public int Pclid { get; set; }
        public int Ctcid { get; set; }
        public int Sucid { get; set; }
        public int IdReporte { get; set; }
        public int Pagina { get; set; }
        public int Idioma { get; set; }
        public string PathArchivo { get; set; }
        public string CarpetaRaiz { get; set; }

        public List<BuscarFacturaCliente> lstCli = new List<BuscarFacturaCliente>();

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
    public class BuscarFacturaBruto
    {
        public decimal Rut { get; set; }
        public string Dv { get; set; }
        public string Facturas { get; set; }
               
        public string Cruce { get; set; }
        public string SS { get; set; }
        public string MILANO { get; set; }
        public string Nombre1 { get; set; }

        public decimal Div { get; set; }
        public decimal Referencia { get; set; }
        public decimal M { get; set; }
        public decimal Ano { get; set; }
    }

    [Serializable]
    public class BuscarFacturaCliente
    {
        public string Rut { get; set; }
        public string Nombre { get; set; }

        public List<BuscarFacturaDetalle> lstDetalle = new List<BuscarFacturaDetalle>();
        public BuscarFacturaTotales Totales = new BuscarFacturaTotales();
    }

    [Serializable]
    public class BuscarFacturaDetalle
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
    public class BuscarFacturaTotales
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
