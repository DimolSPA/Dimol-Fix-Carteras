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
    public class TrekkingCartera
    {
        
        public int Codemp { get; set; }
        public int Pclid { get; set; }
        public int Ctcid { get; set; }
        public int Vencidos { get; set; }
        public int Sucid { get; set; }
        public int Pagina { get; set; }
        public int Idioma { get; set; }
        public int TipoCartera { get; set; }
        public string EstadoCpbt { get; set; }
        public string NombreArchivo { get; set; }
        public string PathArchivo { get; set; }
        public string RutBusca { get; set; }
        public int CodGestor { get; set; }
        public string EstadosCartera { get; set; }
        public int CodigoCarga { get; set; }

        public string RutCliente { get; set; }
        public string NombreCliente { get; set; }
        public int IdReporte { get; set; }

        public CabeceraReporte Encabezado = new CabeceraReporte();
        public TituloReporte Titulo = new TituloReporte();
        public List<TrekkingCarteraGestor> lstGestores = new List<TrekkingCarteraGestor>();
        public TrekkingTotales TotalGral = new TrekkingTotales();

        //public List<TortaPorcentaje> Porcentajes = new List<TortaPorcentaje>();
        public List<TrekkingCarteraRanking> lstRanking = new List<TrekkingCarteraRanking>();

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

    }    
        
    [Serializable]
    public class TrekkingCarteraGestor
    {
        public int CodGestor { get; set; }
        public string NombreGestor { get; set; }

        public List<TrekkingCarteraCliente> lstClientes = new List<TrekkingCarteraCliente>();
        public TrekkingTotales TotalGestor = new TrekkingTotales();
        public TrekkingTotales Caso1 = new TrekkingTotales();
        public TrekkingTotales Caso2 = new TrekkingTotales();
        public TrekkingTotales Caso3 = new TrekkingTotales();
        public TrekkingTotales Caso4 = new TrekkingTotales();
                
    }
    
    [Serializable]
    public class TrekkingTotales
    {
        public int NumCasos { get; set; }

        [XmlIgnore]
        public decimal Saldo { get; set; }
        [XmlElement("Saldo")]
        public string SaldoStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Saldo.ToString("N0"); }
            set { this.Saldo = decimal.Parse(value); }
        }        

        [XmlIgnore]
        public decimal PorcCasos { get; set; }
        [XmlElement("PorcCasos")]
        public string PorcCasosStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.PorcCasos.ToString("N2"); }
            set { this.PorcCasos = decimal.Parse(value); }
        }

        [XmlIgnore]
        public decimal PorcSaldo { get; set; }
        [XmlElement("PorcSaldo")]
        public string PorcSaldoStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.PorcSaldo.ToString("N2"); }
            set { this.PorcSaldo = decimal.Parse(value); }
        }

        public int Flag { get; set; }

    }
    
    [Serializable]
    public class TrekkingCarteraBruto
    {
        public int CodGestor { get; set; }
        public string NombreGestor { get; set; }
        public int CodCliente { get; set; }
        public string RutCliente { get; set; }
        public string NombreCliente { get; set; }
        public int CodDeudor { get; set; }
        public string RutDeudor { get; set; }
        public string NombreDeudor { get; set; }
        public int CodigoMoneda { get; set; }

        [XmlIgnore]
        public decimal Saldo { get; set; }
        [XmlElement("Saldo")]
        public string SaldoStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Saldo.ToString("N0"); }
            set { this.Saldo = decimal.Parse(value); }
        }

        public int NumDias { get; set; }
    }

    [Serializable]
    public class TrekkingCarteraCliente
    {
        public int CodCliente { get; set; }
        public string RutCliente { get; set; }
        public string NombreCliente { get; set; }

        public int CasosBloque1 { get; set; }
        [XmlIgnore]
        public decimal SaldoBloque1 { get; set; }
        [XmlElement("SaldoBloque1")]
        public string SaldoBloque1Str
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.SaldoBloque1.ToString("N0"); }
            set { this.SaldoBloque1 = decimal.Parse(value); }
        }

        public int CasosBloque2 { get; set; }
        [XmlIgnore]
        public decimal SaldoBloque2 { get; set; }
        [XmlElement("SaldoBloque2")]
        public string SaldoBloque2Str
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.SaldoBloque2.ToString("N0"); }
            set { this.SaldoBloque2 = decimal.Parse(value); }
        }

        public int CasosBloque3 { get; set; }
        [XmlIgnore]
        public decimal SaldoBloque3 { get; set; }
        [XmlElement("SaldoBloque3")]
        public string SaldoBloque3Str
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.SaldoBloque3.ToString("N0"); }
            set { this.SaldoBloque3 = decimal.Parse(value); }
        }

        public int CasosBloque4 { get; set; }
        [XmlIgnore]
        public decimal SaldoBloque4 { get; set; }
        [XmlElement("SaldoBloque4")]
        public string SaldoBloque4Str
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.SaldoBloque4.ToString("N0"); }
            set { this.SaldoBloque4 = decimal.Parse(value); }
        }

        public int TotalCasos { get; set; }
        [XmlIgnore]
        public decimal TotalSaldo { get; set; }
        [XmlElement("TotalSaldo")]
        public string TotalSaldoStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.TotalSaldo.ToString("N0"); }
            set { this.TotalSaldo = decimal.Parse(value); }
        }

        [XmlIgnore]
        public decimal Actualiza { get; set; }
        [XmlElement("Actualiza")]
        public string ActualizaStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Actualiza.ToString("N2"); }
            set { this.Actualiza = decimal.Parse(value); }
        }

        public int Flag { get; set; }

    }
    
    [Serializable]
    public class TrekkingCarteraRanking
    {
        public int CodGestor { get; set; }
        public string NombreGestor { get; set; }

        public List<TrekkingCarteraDeudor> lstDeudores = new List<TrekkingCarteraDeudor>();
        
    }

    [Serializable]
    public class TrekkingCarteraDeudor
    {
        public int CodDeudor { get; set; }
        public string RutDeudor { get; set; }
        public string NombreDeudor { get; set; }                
        public string Categoria { get; set; }

        public int CodCliente { get; set; }
        public string RutCliente { get; set; }
        public string NombreCliente { get; set; }

        public int NumDocs { get; set; }

        [XmlIgnore]
        public decimal Saldo { get; set; }
        [XmlElement("Saldo")]
        public string SaldoStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Saldo.ToString("N0"); }
            set { this.Saldo = decimal.Parse(value); }
        }

    }
}
