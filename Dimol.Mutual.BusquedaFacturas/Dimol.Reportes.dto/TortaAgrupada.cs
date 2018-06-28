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
    public class TortaAgrupada
    {
        
        public int Codemp { get; set; }
        public int Pclid { get; set; }
        public int Sucid { get; set; }
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
        public List<TortaCliente> lstDocumentos = new List<TortaCliente>();
        public TortaTotales Totales = new TortaTotales();
        public List<TortaPorcentaje> Porcentajes = new List<TortaPorcentaje>();
        public List<TortaRanking> lstRanking = new List<TortaRanking>();

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
    public class TortaEstado
    {
        public int IdEstado { get; set; }
        public string  Estado { get; set; }
        
        [XmlIgnore]
        public int Deudores { get; set; }
        [XmlElement("Deudores")]
        public string DeudoresStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Deudores.ToString("N0"); }
            set { this.Deudores = int.Parse(value); }
        }
        
        [XmlIgnore]
        public int Documentos { get; set; }
        [XmlElement("Documentos")]
        public string DocumentosStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Documentos.ToString("N0"); }
            set { this.Documentos = int.Parse(value); }
        }
        [XmlIgnore]
        public decimal Monto { get; set; }
        [XmlElement("Monto")]
        public string MontoStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Monto.ToString("N2"); }
            set { this.Monto = decimal.Parse(value); }
        }
        [XmlIgnore]
        public decimal Regularizado { get; set; }
        [XmlElement("Regularizado")]
        public string RegularizadoStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Regularizado.ToString("N2"); }
            set { this.Regularizado = decimal.Parse(value); }
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
        public decimal Compromiso { get; set; }
        [XmlElement("Compromiso")]
        public string CompromisoStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Compromiso.ToString("N2"); }
            set { this.Compromiso = decimal.Parse(value); }
        }
    }


    [Serializable]
    public class TortaCliente
    {
        public int IdAgrupa { get; set; }
        public string Agrupa { get; set; }

        public List<TortaEstado> lstEstados = new List<TortaEstado>();
        public TortaTotales SubTotal = new TortaTotales();
    }

    [Serializable]
    public class TortaTotales
    {
        [XmlIgnore]
        public decimal Monto { get; set; }
        [XmlElement("Monto")]
        public string MontoStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Monto.ToString("N2"); }
            set { this.Monto = decimal.Parse(value); }
        }
        [XmlIgnore]
        public decimal Regularizado { get; set; }
        [XmlElement("Regularizado")]
        public string RegularizadoStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Regularizado.ToString("N2"); }
            set { this.Regularizado = decimal.Parse(value); }
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
        public decimal Compromiso { get; set; }
        [XmlElement("Compromiso")]
        public string CompromisoStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Compromiso.ToString("N2"); }
            set { this.Compromiso = decimal.Parse(value); }
        }

        [XmlIgnore]
        public int Deudores { get; set; }
        [XmlElement("Deudores")]
        public string DeudoresStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Deudores.ToString("N0"); }
            set { this.Deudores = int.Parse(value); }
        }

        [XmlIgnore]
        public int Documentos { get; set; }
        [XmlElement("Documentos")]
        public string DocumentosStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Documentos.ToString("N0"); }
            set { this.Documentos = int.Parse(value); }
        }
    }

    [Serializable]
    public class TortaEstadoBruto
    {
        public int Pclid { get; set; }
        public int Ctcid { get; set; }
        public string Rut { get; set; }
        public string Nombre { get; set; }
        public int Ccbid { get; set; }
        public string Estado { get; set; }
        public string EstadoCpbt { get; set; }
        public int CodigoMoneda { get; set; }
        public decimal TipoCambio { get; set; }
        public decimal Asignado { get; set; }
        public decimal Monto { get; set; }
        public decimal Saldo { get; set; }
        public decimal TotalDeuda { get; set; }
        public decimal Compromiso { get; set; }

        public int TipoCartera { get; set; }
        public string NombreMoneda { get; set; }
        public int Agrupa { get; set; }
        public string Prejudicial { get; set; }
    }

    [Serializable]
    public class TortaPorcentaje
    {
        [XmlIgnore]
        public decimal Monto { get; set; }
        [XmlElement("Monto")]
        public string MontoStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Monto.ToString("N2"); }
            set { this.Monto = decimal.Parse(value); }
        }
        [XmlIgnore]
        public decimal Porcentaje { get; set; }
        [XmlElement("Porcentaje")]
        public string PorcentajeStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Porcentaje.ToString("N2"); }
            set { this.Porcentaje = decimal.Parse(value); }
        }
        public string Titulo { get; set; }
    }

    [Serializable]
    public class TortaRanking
    {
        public int Ctcid { get; set; }
        public string Rut { get; set; }
        public string Nombre { get; set; }

        [XmlIgnore]
        public int Documentos { get; set; }
        [XmlElement("Documentos")]
        public string DocumentosStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Documentos.ToString("N0"); }
            set { this.Documentos = int.Parse(value); }
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
        public decimal PorcSaldo { get; set; }
        [XmlElement("PorcSaldo")]
        public string PorcSaldoStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.PorcSaldo.ToString("N2"); }
            set { this.PorcSaldo = decimal.Parse(value); }
        }

    }
}
