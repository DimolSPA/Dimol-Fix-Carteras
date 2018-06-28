using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Xml.Serialization;

namespace Dimol.CKEditor.dto
{
    [Serializable]
    public class BorradorJudicial
    {
        public int Pclid { get; set; }
        public int Ctcid { get; set; }
        public int Rolid { get; set; }

        public string TipoRol { get; set; }
        public string Rol { get; set; }
        public int Anio { get; set; }
        public string Cliente { get; set; }
        public string RutCliente { get; set; }
        public string RutDeudor { get; set; }
        public string NombreDeudor { get; set; }

        public string PaisDeudor { get; set; }
        public string RegionDeudor { get; set; }
        public string CiudadDeudor { get; set; }
        public string ComunaDeudor { get; set; }
        public string CodigoPostalDeudor { get; set; }

        public string DireccionDeudor { get; set; }

        public string RutEmpleado { get; set; }
        public string NombreEmpleado { get; set; }
        public string PaternoEmpleado { get; set; }
        public string MaternoEmpleado { get; set; }
        public string TelefonoEmpleado { get; set; }
        public string MailEmpleado { get; set; }

        public string Tribunal { get; set; }

        public DateTime Fecha { get; set; }

        [XmlIgnore]
        public decimal Monto { get; set; }
        [XmlElement("Monto")]
        public string MontoStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); if ((this.Monto % 1) == 0) { return this.Monto.ToString("N0"); } else { return this.Monto.ToString("N2"); } }
            set { this.Monto = decimal.Parse(value); }
        }
        public int Cuotas { get; set; }

        [XmlIgnore]
        public decimal MontoCuota { get; set; }
        [XmlElement("MontoCuota")]
        public string MontoCuotaStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); if ((this.MontoCuota % 1) == 0) { return this.MontoCuota.ToString("N0"); } else { return this.MontoCuota.ToString("N2"); } }
            set { this.MontoCuota = decimal.Parse(value); }
        }

        [XmlIgnore]
        public decimal MontoPrimeraCuota { get; set; }
        [XmlElement("MontoPrimeraCuota")]
        public string MontoPrimeraCuotaStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); if ((this.MontoPrimeraCuota % 1) == 0) { return this.MontoPrimeraCuota.ToString("N0"); } else { return this.MontoPrimeraCuota.ToString("N2"); } }
            set { this.MontoPrimeraCuota = decimal.Parse(value); }
        }

        [XmlIgnore]
        public decimal MontoUltimaCuota { get; set; }
        [XmlElement("MontoUltimaCuota")]
        public string MontoUltimaCuotaStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); if ((this.MontoUltimaCuota % 1) == 0) { return this.MontoUltimaCuota.ToString("N0"); } else { return this.MontoUltimaCuota.ToString("N2"); } }
            set { this.MontoUltimaCuota = decimal.Parse(value); }
        }

        [XmlIgnore]
        public DateTime FechaPrimeraCuota { get; set; }
        [XmlElement("FechaPrimeraCuota")]
        public string FechaPrimeraCuotaStr
        {
            get { return this.FechaPrimeraCuota.ToString("d" + " - " + "MMMM" + " - " + "yyyy"); }
            set { this.FechaPrimeraCuota = DateTime.Parse(value); }
        }
        [XmlElement("FechaPrimeraCuotaAvenimiento")]
        public string FechaPrimeraCuotaStrAvenimiento
        {
            get { return this.FechaPrimeraCuota.ToString("d" + "' de '" + "MMMM" + "' de '" + "yyyy"); }
            set { }
        }
        [XmlElement("MesPrimeraCuota")]
        public string MesPrimeraCuotaStr
        {
            get { return this.FechaPrimeraCuota.ToString("MMMM" + " - " + "yyyy"); }
            set { this.FechaPrimeraCuota = DateTime.Parse(value); }
        }

        [XmlIgnore]
        public DateTime FechaUltimaCuota { get; set; }
        [XmlElement("FechaUltimaCuota")]
        public string FechaUltimaCuotaStr
        {
            get { return this.FechaUltimaCuota.ToString("d" + " - " + "MMMM" + " - " + "yyyy"); }
            set { this.FechaUltimaCuota = DateTime.Parse(value); }
        }
        [XmlIgnore]
        public decimal Interes { get; set; }
        [XmlElement("Interes")]
        public string InteresStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Interes.ToString("N2"); }
            set { this.Interes = decimal.Parse(value); }
        }

        public string PaisEmpresa { get; set; }
        public string RegionEmpresa { get; set; }
        public string CiudadEmpresa { get; set; }
        public string ComunaEmpresa { get; set; }
        public string CodigoPostalEmpresa { get; set; }

        public string NombreFantasiaDeudor { get; set; }
        public string PaternoDeudor { get; set; }
        public string MaternoDeudor { get; set; }

        public string NombreCliente { get; set; }
        public string PaternoCliente { get; set; }
        public string MaternoCliente { get; set; }

        public string RepresentanteLegal { get; set; }
        public string RutRepresentanteLegal { get; set; }
        public string Numero { get; set; }

        [XmlIgnore]
        public decimal Saldo { get; set; }
        [XmlElement("Saldo")]
        public string SaldoStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); if ((this.Saldo % 1) == 0) { return this.Saldo.ToString("N0"); } else { return this.Saldo.ToString("N2"); } }
            set { this.Saldo = decimal.Parse(value); }
        }
        public string DireccionEmpresa { get; set; }
        public string TelefonoEmpresa { get; set; }
        [XmlIgnore]
        public DateTime FechaVencimiento { get; set; }
        [XmlElement("FechaVencimiento")]
        public string FechaVencimientoStr
        {
            get { return this.FechaVencimiento.ToString("d" + " - " + "MMMM" + " - " + "yyyy"); }
            set { this.FechaVencimiento = DateTime.Parse(value); }
        }
        public DateTime FechaRol { get; set; }

        [XmlIgnore]
        public DateTime FechaDemanda { get; set; }
        [XmlElement("FechaDemanda")]
        public string FechaDemandaStr
        {
            get { return this.FechaDemanda.ToString("d" + " - " + "MMMM" + " - " + "yyyy"); }
            set { this.FechaDemanda = DateTime.Parse(value); }
        }
        public string FechaDemandaStrCorto
        {
            get
            {
                return this.FechaDemanda.ToString("d" + "' de '" + "MMMM");
            }
            set { }
        }
        public string MateriaJudicial { get; set; }
        public string Estado { get; set; }
        public string TipoCausa { get; set; }

        public int DiaPago { get; set; }
        public string MontoAvenimientoEscrito { get; set; }
        public string MontoCuotaAvenimientoEscrito { get; set; }

        public string MontoDemandaEscrito { get; set; }
        public string MontoPCuotaDemandaEscrito { get; set; }
        public string MontoUCuotaDemandaEscrito { get; set; }
        public string MontoSaldoDemandaEscrito { get; set; }

        /* BORRADORES PREVISIONALES
        ********************************/
        public List<ResolucionParaXsl> ListaResoluciones { get; set; }
        public string Procedimiento { get; set; }
        public string Cuantia { get; set; }
        public string AbogPatrocinante { get; set; }
        public string RunAbogPatrocinante { get; set; }
        public string NumContrato { get; set; }

        public string NumResolucion { get; set; }
        public string FecResolucion { get; set; }
        public string RutRepresentante1 { get; set; }
        public string NombRepresentante1 { get; set; }
        public string RutRepresentante2 { get; set; }
        public string NombRepresentante2 { get; set; }
        public string RutRepresentante3 { get; set; }
        public string NombRepresentante3 { get; set; }

        public DateTime FechaMandato { get; set; }
        public string FechaMandatoStr
        {
            get { return this.FechaMandato.ToString("d" + " de " + "MMMM" + " de " + "yyyy"); }
            set { this.FechaMandato = DateTime.Parse(value); }
        }

        public decimal MontoPrevisional { get; set; }
        public string MontoPrevisionalStr { get; set; }

        public BorradorJudicial()
        {
            AbogPatrocinante = "MAY GUTIERREZ OTTO";
            RunAbogPatrocinante = "13.831.554-1";
            FechaMandato = DateTime.ParseExact("2017-01-01 00:00:00.000", "yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
        }
    }

    [Serializable]
    public class ResolucionParaXsl
    {
        public string NumResolucion { get; set; }
        public string FecResolucion { get; set; }
        public string FecResolucionStr { get; set; }
        public List<PeriodosParaXsl> ListaPeriodos { get; set; }
        public decimal MontoTotal { get; set; }
        public string MontoTotalStr { get; set; }
    }

    [Serializable]
    public class PeriodosParaXsl
    {
        public string Periodo { get; set; }
    }
}