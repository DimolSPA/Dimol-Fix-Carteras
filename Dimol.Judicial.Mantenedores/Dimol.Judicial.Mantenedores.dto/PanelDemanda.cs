using System;
using System.Globalization;
using System.Threading;
using System.Xml.Serialization;

namespace Dimol.Judicial.Mantenedores.dto
{
    public class PanelDemanda
    {
        public int Pclid { get; set; }
        public int Ctcid { get; set; }
        public int? Sbcid { get; set; }
        public int Tpcid { get; set; }
    }

    public class PanelDemandaDocumentos
    {
        public int Pclid { get; set; }
        public int Ctcid { get; set; }
        public int Ccbid { get; set; }
        public int? Sbcid { get; set; }
        public int Tpcid { get; set; }
    }

    public class PanelDemandaGet
    {
        public int PanelId { get; set; }
        public string Procesada { get; set; }
        public DateTime FechaAsignacion { get; set; }
        public DateTime FechaAprobacionTraspaso { get; set; }
        public DateTime FechaIngresaJudicial { get; set; }
        public string Cliente { get; set; }
        public string RutDeudor { get; set; }
        public string Deudor { get; set; }
        public string Asegurado { get; set; }
        public string TipoDocumento { get; set; }
        public string Comuna { get; set; }
        public string Region { get; set; }
        public string EncargadoCofeccion { get; set; }
        public int UsridEncargado { get; set; }
        public DateTime? FechaEnvioConfeccion { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public DateTime? FechaIngresoTribunal { get; set; }
        public string Comentarios { get; set; }
        public int Pclid { get; set; }
        public int Ctcid { get; set; }
        public string RutCliente { get; set; }
        public int CountFechaEntrega { get; set; }
        public string Correcciones { get; set; }
        public int CountCorrecciones { get; set; }
        public string Cursodemanda { get; set; }
        public int CountCursodemanda { get; set; }
        public string Responsable { get; set; }

        public string RolNumero { get; set; }
        public string TribunalNombre { get; set; }
    }

    public class PanelDemandaDocumentoAsignar
    {
        public int Ccbid { get; set; }
        public decimal Monto { get; set; }
        public decimal Saldo { get; set; }
    }

    public class PanelDemandaEntesAsignar
    {
        public int Etjid { get; set; }
        public string Nombre { get; set; }
    }

    public class OrgChartPanelDemanda
    {
        public string Id { get; set; }
        public string Total { get; set; }
        public string Item { get; set; }
        public string Parent { get; set; }
    }

    public class PanelDemandaReporte
    {
        public int PanelId { get; set; }
        public string Cliente { get; set; }
        public string RutDeudor { get; set; }
        public string Deudor { get; set; }
        public string Asegurado { get; set; }
        public DateTime? FechaAsignacion { get; set; }
        public DateTime? FechaAprobacionTraspaso { get; set; }
        public DateTime? FechaIngresoTribunal { get; set; }
        public DateTime? IngresoJudicial { get; set; }
        public DateTime? FechaEnvio { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public string Encargado { get; set; }
        public int DiasTranscurso { get; set; }
        public string Correcciones { get; set; }
        public int CountCorrecciones { get; set; }
        public int DiasTranscurso2 { get; set; }
        public string Comentarios { get; set; }
        public int TrackingDemanda { get; set; }
    }

    public class PanelDemandaMasivaExcel
    {
        public string IdRol { get; set; }
        public string Responsable { get; set; }
        public DateTime FechaAsignacion { get; set; }
        public DateTime FechaAprobacionTraspaso { get; set; }
        public DateTime FechaIngresaJudicial { get; set; }
        public string Cliente { get; set; }
        public string RutDeudor { get; set; }
        public string Deudor { get; set; }
        public string Asegurado { get; set; }
        public string TipoDocumento { get; set; }
        public string Comuna { get; set; }
        public string Region { get; set; }
        //public string EncargadoCofeccion { get; set; }
        public DateTime? FechaEnvioConfeccion { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public DateTime? FechaIngresoTribunal { get; set; }
        public string Comentarios { get; set; }
        public string Correcciones { get; set; }
        public int CountCorrecciones { get; set; }
        public int CantidadNoCurso { get; set; }
        public string RolNumero { get; set; }
        public string TribunalNombre { get; set; }
    }

    public class PanelDemandaExcel : PanelDemandaMasivaExcel
    {
        public string EncargadoCofeccion { get; set; }
    }

    public class DocumentosPanel
    {
        public int PanelId { get; set; }
        public int Pclid { get; set; }
        public int Ctcid { get; set; }
        public int Ccbid { get; set; }
        public string TipoDocumento { get; set; }
        public string Numero { get; set; }
        public decimal Monto { get; set; }
        public decimal Saldo { get; set; }
        public string Estado { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public DateTime FechaAsignacion { get; set; }
        public string Asegurado { get; set; }

        //Previsional
        public string NumResolucion { get; set; }
        public DateTime FecResolucion { get; set; }
    }

    public class ConfeccionDemanda
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
        //public decimal Monto { get; set; }
        [XmlIgnore]
        public decimal Monto { get; set; }
        [XmlElement("Monto")]
        public string MontoStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); if ((this.Monto % 1) == 0) { return this.Monto.ToString("N0"); } else { return this.Monto.ToString("N2"); } }
            set { this.Monto = decimal.Parse(value); }
        }
        public int Cuotas { get; set; }
        //public decimal MontoCuota { get; set; }
        [XmlIgnore]
        public decimal MontoCuota { get; set; }
        [XmlElement("MontoCuota")]
        public string MontoCuotaStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); if ((this.MontoCuota % 1) == 0) { return this.MontoCuota.ToString("N0"); } else { return this.MontoCuota.ToString("N2"); } }
            set { this.MontoCuota = decimal.Parse(value); }
        }
        //public decimal MontoPrimeraCuota { get; set; }
        [XmlIgnore]
        public decimal MontoPrimeraCuota { get; set; }
        [XmlElement("MontoPrimeraCuota")]
        public string MontoPrimeraCuotaStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); if ((this.MontoPrimeraCuota % 1) == 0) { return this.MontoPrimeraCuota.ToString("N0"); } else { return this.MontoPrimeraCuota.ToString("N2"); } }
            set { this.MontoPrimeraCuota = decimal.Parse(value); }
        }
        public decimal MontoUltimaCuota { get; set; }
        //public string FechaPrimeraCuota { get; set; }
        [XmlIgnore]
        public DateTime FechaPrimeraCuota { get; set; }
        [XmlElement("FechaPrimeraCuota")]
        public string FechaPrimeraCuotaStr
        {
            get { return this.FechaPrimeraCuota.ToString("d" + " - " + "MMMM" + " - " + "yyyy"); }
            set { this.FechaPrimeraCuota = DateTime.Parse(value); }
        }
        [XmlElement("MesPrimeraCuota")]
        public string MesPrimeraCuotaStr
        {
            get { return this.FechaPrimeraCuota.ToString("MMMM" + " - " + "yyyy"); }
            set { this.FechaPrimeraCuota = DateTime.Parse(value); }
        }

        //public string FechaUltimaCuota { get; set; }
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

        //public decimal Saldo { get; set; }
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
        //public string FechaDemanda { get; set; }
        [XmlIgnore]
        public DateTime FechaDemanda { get; set; }
        [XmlElement("FechaDemanda")]
        public string FechaDemandaStr
        {
            get { return this.FechaDemanda.ToString("d" + " - " + "MMMM" + " - " + "yyyy"); }
            set { this.FechaDemanda = DateTime.Parse(value); }
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

    }
}
