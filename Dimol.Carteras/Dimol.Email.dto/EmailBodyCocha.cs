using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Xml.Serialization;

namespace Dimol.Email.dto
{
    public class EmailBodyCocha
    {        
        public string Header { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }                  
    }

    [Serializable]
    public class DocumentoCocha
    {
        public string Tipo { get; set; }        
        public string Moneda { get; set; }
        public string Cliente { get; set; }
        public string RutCliente { get; set; }
        public string Deudor { get; set; }
        public string RutDeudor { get; set; }
        public string Numero { get; set; }
        public string Cuenta { get; set; }
        public string Banco { get; set; }
        public decimal Monto { get; set; }
        
        [XmlIgnore]
        public decimal Saldo { get; set; }
        [XmlElement("Saldo")]
        public string SaldoStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Saldo.ToString("N0"); }
            set { this.Saldo = decimal.Parse(value); }
        }

        [XmlIgnore]
        public decimal SaldoDolar { get; set; }
        [XmlElement("SaldoDolar")]
        public string SaldoDolarStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.SaldoDolar.ToString("N2"); }
            set { this.SaldoDolar = decimal.Parse(value); }
        }

        [XmlIgnore]
        public DateTime FechaMail { get; set; }

        [XmlElement("FechaMail")]
        public string FechaCorreo
        {
            get { return this.FechaMail.ToString("dd-MM-yy"); }
            set { this.FechaMail = DateTime.Parse(value); }
        }

        public List<CochaCpbt> DocPesos = new List<CochaCpbt>();
        public List<CochaCpbtDolar> DocDolares = new List<CochaCpbtDolar>();

        public int CodigoCarga { get; set; }
        public string Comentario { get; set; }
    }

    public class CochaCpbt
    {
        public string Numero { get; set; }
        public string TipoMoneda { get; set; }

        [XmlIgnore]
        public decimal Saldo { get; set; }
        [XmlElement("Saldo")]
        public string SaldoStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Saldo.ToString("N0"); }
            set { this.Saldo = decimal.Parse(value); }
        }

        public int CodigoCarga { get; set; }
    }

    public class CochaCpbtDolar
    {
        public string Numero { get; set; }
        public string TipoMoneda { get; set; }

        [XmlIgnore]
        public decimal Saldo { get; set; }
        [XmlElement("Saldo")]
        public string SaldoStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Saldo.ToString("N2"); }
            set { this.Saldo = decimal.Parse(value); }
        }

        public int CodigoCarga { get; set; }
    }

    [Serializable]
    public class DocumentoMutualPagos
    {
        public string Tipo { get; set; }
        public string Moneda { get; set; }
        public string Cliente { get; set; }
        public string RutCliente { get; set; }
        public string Deudor { get; set; }
        public string RutDeudor { get; set; }
        public string Numero { get; set; }
        public string Cuenta { get; set; }
        public string Banco { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string NombreGestor { get; set; }
        public decimal Monto { get; set; }

        [XmlIgnore]
        public decimal Saldo { get; set; }
        [XmlElement("Saldo")]
        public string SaldoStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Saldo.ToString("N0"); }
            set { this.Saldo = decimal.Parse(value); }
        }

        [XmlIgnore]
        public DateTime FechaMail { get; set; }

        [XmlElement("FechaMail")]
        public string FechaCorreo
        {
            get { return this.FechaMail.ToString("dd-MM-yy"); }
            set { this.FechaMail = DateTime.Parse(value); }
        }

        public List<CochaCpbt> DocPesos = new List<CochaCpbt>();
        public List<CochaCpbtDolar> DocDolares = new List<CochaCpbtDolar>();

        public int CodigoCarga { get; set; }
        public string Comentario { get; set; }
    }
}
