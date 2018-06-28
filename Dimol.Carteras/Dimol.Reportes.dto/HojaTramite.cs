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
    public class HojaTramite
    {
        public int Codemp { get; set; }    
        public int Pclid { get; set; }
        public int IdSuc { get; set; }
        public int Idioma { get; set; }
        public string PathArchivo { get; set; }
        public int Ctcid { get; set; }
        public string EstadoCpbt { get; set; }
        public string Ccbid { get; set; }

        public int IdReporte { get; set; }
        public int Pagina { get; set; }

        public CabeceraReporte Encabezado = new CabeceraReporte();
        public TituloReporte Titulo = new TituloReporte();        
        public List<HojaTramiteCliente> lstHojaCliente = new List<HojaTramiteCliente>();
        
        [XmlIgnore]
        public DateTime FechaReporte { get; set; }

        [XmlElement("FechaReporte")]
        public string FechaEmision
        {
            get { return this.FechaReporte.ToString("dd/MM/yyyy HH:mm:ss"); }
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


        public string FechaLarga { get; set; }
        public string RutUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string Rol { get; set; }
    }

    [Serializable]
    public class HojaTramiteCliente
    {
        public string RutDeudor { get; set; }
        public string NombreDeudor { get; set; }
        public string Causa { get; set; }
        public string Juzgado { get; set; }

        [XmlIgnore]
        public decimal Monto { get; set; }
        [XmlElement("Monto")]
        public string MontoStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Monto.ToString("N2"); }
            set { this.Monto = decimal.Parse(value); }
        }

        public string Rol { get; set; }
        public string Direccion { get; set; }
        public string Ciudad { get; set; }

        public List<HojaTramiteAsegurado> lstAsegurados = new List<HojaTramiteAsegurado>();
        public List<HojaTramiteDetalle> lstHojaDetalle = new List<HojaTramiteDetalle>();
                
    }

    [Serializable]
    public class HojaTramiteAsegurado
    {
        public string Nombre { get; set; }
    }

    [Serializable]
    public class HojaTramiteDetalle
    {
        public int IdDetalle { get; set; }

        [XmlIgnore]
        public DateTime FechaJudicial { get; set; }

        [XmlElement("FechaJudicial")]
        public string FechaJudicialStr
        {
            get { return this.FechaJudicial.ToString("dd/MM/yyyy"); }
            set { this.FechaJudicial = DateTime.Parse(value); }
        }

        public string Materia { get; set; }
        public string Estado { get; set; }
        public string Comentario { get; set; }

    }

    [Serializable]
    public class HojaTramiteBruto
    {
        public string RutDeudor { get; set; }
        public string NombreDeudor { get; set; }
        public string Causa { get; set; }
        public string Juzgado { get; set; }
        public int CodigoMoneda { get;set; }
        public string RutAsegurado { get; set; }
        public string NombreAsegurado { get; set; }

        [XmlIgnore]
        public decimal Monto { get; set; }
        [XmlElement("Monto")]
        public string MontoStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Monto.ToString("N2"); }
            set { this.Monto = decimal.Parse(value); }
        }

        public string Rol { get; set; }
        public string Direccion { get; set; }
        public string Ciudad { get; set; }

        public int IdDetalle { get; set; }

        [XmlIgnore]
        public DateTime FechaJudicial { get; set; }

        [XmlElement("FechaJudicial")]
        public string FechaJudicialStr
        {
            get { return this.FechaJudicial.ToString("dd/MM/yyyy"); }
            set { this.FechaJudicial = DateTime.Parse(value); }
        }

        public string Materia { get; set; }
        public string Estado { get; set; }
        public string Comentario { get; set; }
    }
}