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
    public class InformeRemesa
    {
        public int Codemp { get; set; }
        public int Sucid { get; set; }
        public int Idioma { get; set; }
        public int Numero { get; set; }
        public string NumeroRemesa { get; set; }
        public int TipoDocumento { get; set; }

        public string RutCliente { get; set; }
        public string NombreCliente { get; set; }
        public int IdReporte { get; set; }
        public int Pagina { get; set; }

        public CabeceraReporte Encabezado = new CabeceraReporte();
        public TituloReporte Titulo = new TituloReporte();
        public List<InformeRemesaDeudor> lstDocumentos = new List<InformeRemesaDeudor>();
        public InformeRemesaTotales Totales = new InformeRemesaTotales();

        [XmlIgnore]
        public DateTime FechaReporte { get; set; }

        [XmlElement("FechaReporte")]
        public string FechaEmision
        {
            get { return this.FechaReporte.ToString("dd/MM/yyyy HH:mm:ss"); }
            set { this.FechaReporte = DateTime.Parse(value); }
        }
        [XmlIgnore]
        public DateTime FechaEmisionCorta { get; set; }

        [XmlElement("FechaEmision")]
        public string FechaEmisionCortaStr
        {
            get { return this.FechaEmisionCorta.ToString("dd/MM/yyyy"); }
            set { this.FechaEmisionCorta = DateTime.Parse(value); }
        }
        public string RutUsuario { get; set; }
        public string NombreUsuario { get; set; }

    }
    [Serializable]
    public class InformeRemesaDeudor
    {
        public int RutDeudor { get; set; }
        public string DvDeudor { get; set; }
        public string RutDeudorFormateado { get; set; }
        public string NombreFantasia { get; set; }
        public string SubCartera { get; set; }
        public string RutSubCartera { get; set; }

        public List<InformeRemesaDetalle> lstDetalles = new List<InformeRemesaDetalle>();
        public InformeRemesaTotales Totales = new InformeRemesaTotales();
    }

    [Serializable]
    public class InformeRemesaDetalle
    {
        [XmlIgnore]
        public DateTime FechaPago { get; set; }
        [XmlElement("FechaPago")]
        public string FechaPagoStr
        {
            get { return this.FechaPago.ToString("dd/MM/yyyy"); }
            set { this.FechaPago = DateTime.Parse(value); }
        }
        public string TipoDocumento { get; set; }
        public int Numero { get; set; }
        
        public string Moneda { get; set; }
        [XmlIgnore]
        public decimal Capital { get; set; }
        [XmlElement("Capital")]
        public string CapitalStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Capital.ToString("N2"); }
            set { this.Capital = decimal.Parse(value); }
        }
        [XmlIgnore]
        public decimal Interes { get; set; }
        [XmlElement("Interes")]
        public string InteresStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Interes.ToString("N2"); }
            set { this.Interes = decimal.Parse(value); }
        }
        [XmlIgnore]
        public decimal Honorario { get; set; }
        [XmlElement("Honorario")]
        public string HonorarioStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Honorario.ToString("N2"); }
            set { this.Honorario = decimal.Parse(value); }
        }
        [XmlIgnore]
        public decimal Gastos { get; set; }
        [XmlElement("Gastos")]
        public string GastosStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Gastos.ToString("N2"); }
            set { this.Gastos = decimal.Parse(value); }
        }
        [XmlIgnore]
        public decimal Recuperado { get; set; }
        [XmlElement("Recuperado")]
        public string RecuperadoStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Recuperado.ToString("N2"); }
            set { this.Recuperado = decimal.Parse(value); }
        }
        [XmlIgnore]
        public decimal UF { get; set; }
        [XmlElement("UF")]
        public string UFStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.UF.ToString("N2"); }
            set { this.UF = decimal.Parse(value); }
        }
        [XmlIgnore]
        public decimal Comision { get; set; }
        [XmlElement("Comision")]
        public string ComisionStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Comision.ToString("N2"); }
            set { this.Comision = decimal.Parse(value); }
        }
        
        public string Negocio { get; set; }
        public string Comentario { get; set; }
    }

    [Serializable]
    public class InformeRemesaTotales
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
        public decimal Interes { get; set; }
        [XmlElement("Interes")]
        public string InteresStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Interes.ToString("N2"); }
            set { this.Interes = decimal.Parse(value); }
        }
        [XmlIgnore]
        public decimal Honorario { get; set; }
        [XmlElement("Honorario")]
        public string HonorarioStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Honorario.ToString("N2"); }
            set { this.Honorario = decimal.Parse(value); }
        }
        [XmlIgnore]
        public decimal Gastos { get; set; }
        [XmlElement("Gastos")]
        public string GastosStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Gastos.ToString("N2"); }
            set { this.Gastos = decimal.Parse(value); }
        }
        [XmlIgnore]
        public decimal Recuperado { get; set; }
        [XmlElement("Recuperado")]
        public string RecuperadoStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Recuperado.ToString("N2"); }
            set { this.Recuperado = decimal.Parse(value); }
        }
        [XmlIgnore]
        public decimal UF { get; set; }
        [XmlElement("UF")]
        public string UFStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.UF.ToString("N2"); }
            set { this.UF = decimal.Parse(value); }
        }
        [XmlIgnore]
        public decimal Comision { get; set; }
        [XmlElement("Comision")]
        public string ComisionStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Comision.ToString("N2"); }
            set { this.Comision = decimal.Parse(value); }
        }
    }
}
