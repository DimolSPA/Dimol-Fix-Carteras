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
    public class InformeBajas
    {
        public int Codemp { get; set; }
        public int Codsuc { get; set; }
        public int Pclid { get; set; }
        public int Idioma { get; set; }
        public int TipoCartera { get; set; }
        public int Ctcid { get; set; }
        public char Estcpbt { get; set; }
        public string PathArchivo { get; set; }

        public int IdReporte { get; set; }
        public int Pagina { get; set; }

        public CabeceraReporte Encabezado = new CabeceraReporte();
        public TituloReporte Titulo = new TituloReporte();
        public List<InformeBajasBruto> lstBajas = new List<InformeBajasBruto>();
        public InformeBajasTotales Total = new InformeBajasTotales();

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
    public class InformeBajasTotales
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
    public class InformeBajasBruto
    {
        [XmlIgnore]
        public DateTime FechaReclamo { get; set; }
        [XmlElement("FechaReclamo")]
        public string FechaReclamoStr
        {
            get { return this.FechaReclamo.ToString("dd/MM/yyyy"); }
            set { this.FechaReclamo = DateTime.Parse(value); }
        }

        public string Rut { get; set; }
        public string Empresa { get; set; }
        public string Factura { get; set; }

        [XmlIgnore]
        public decimal Monto { get; set; }
        [XmlElement("Monto")]
        public string MontoStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Monto.ToString("N0"); }
            set { this.Monto = decimal.Parse(value); }
        }

        public string Gestor { get; set; }

        [XmlIgnore]
        public DateTime FechaPago { get; set; }
        [XmlElement("FechaPago")]
        public string FechaPagoStr
        {
            get { return this.FechaPago.ToString("dd/MM/yyyy"); }
            set { this.FechaPago = DateTime.Parse(value); }
        }

        public string Banco { get; set; }   
        public string Cuenta { get; set; }
        public string Comentario { get; set; }
        public string Historial { get; set; }
    }
}
