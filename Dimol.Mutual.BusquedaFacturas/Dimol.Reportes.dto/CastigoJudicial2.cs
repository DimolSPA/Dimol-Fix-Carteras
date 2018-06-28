﻿using System;
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
    public class CastigoJudicial2
    {
        public int Codemp { get; set; }
        public int Codsuc { get; set; }
        public int Tpcid { get; set; }
        public int Cbcnumero { get; set; }
        public int Idioma { get; set; }
        public int CbcDesde { get; set; }
        public int CbcHasta { get; set; }
        public string RutAbogado { get; set; }
        public string NombreAbogado { get; set; }
        public string Empresa { get; set; }

        public CabeceraReporte Encabezado = new CabeceraReporte();
        public TituloReporte Titulo = new TituloReporte();
        
        public List<CastigoJudicialDeudor2> lstDocsDeudor = new List<CastigoJudicialDeudor2>();
        public CastigoJudicialTotales2 Totales = new CastigoJudicialTotales2();

        //public List<dto.CastigoJudicialCliente2> lstCastDocs = new List<dto.CastigoJudicialCliente2>();

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

        public string RutUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public int Pclid { get; set; }
        public int Pcsid { get; set; }
    }

    [Serializable]
    public class CastigoJudicialCliente2
    {
        public int Codemp { get; set; }
        public int Codsuc { get; set; }
        public int Tpcid { get; set; }
        public int CbcDesde { get; set; }
        public int CbcHasta { get; set; }
        public int Idioma { get; set; }
        public int NumRegistro { get; set; }
        public string RutAbogado { get; set; }
        public string NombreAbogado { get; set; }
        public string Empresa { get; set; }        
        
        public CabeceraReporte Encabezado = new CabeceraReporte();
        public TituloReporte Titulo = new TituloReporte();
        public CabeceraSucursalCliente SucursalCliente = new CabeceraSucursalCliente();
        public CastigoJudicialDeudor2 Deudor = new CastigoJudicialDeudor2();
        public CastigoJudicialTotales2 Totales = new CastigoJudicialTotales2();

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
            get { CultureInfo ci = new CultureInfo("es-CL"); return ci.DateTimeFormat.GetDayName(this.FechaComprobante.DayOfWeek) + " " + this.FechaComprobante.ToString("dd") + " de " + ci.DateTimeFormat.GetMonthName(this.FechaComprobante.Month) + " de " + this.FechaComprobante.ToString("yyyy"); }
            set { this.FechaComprobante = DateTime.Parse(value); }
        }

        public string RutUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public int Pclid { get; set; }
        public int Pcsid { get; set; }
    }

    [Serializable]
    public class CastigoJudicialDeudor2
    {
        public string RutDeudor { get; set; }
        public string NombreDeudor { get; set; }
        public string Direccion { get; set; }
        public string Tribunal { get; set; }
        public string Rol { get; set; }
        public int CbcNumProvCli { get; set; }
        public string NombreAsegurado { get; set; }

        public string RutUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public int Pclid { get; set; }
        public int Pcsid { get; set; }
        public int Codemp { get; set; }

        public CabeceraSucursalCliente lstSucursalCliente = new CabeceraSucursalCliente();
        public List<CabeceraMotivoCastigo> lstMotivos = new List<CabeceraMotivoCastigo>();
        public List<CastigoJudicialDetalle2> lstDocs = new List<CastigoJudicialDetalle2>();
        public CastigoJudicialDeudorTotales2 Totales = new CastigoJudicialDeudorTotales2();
    }

    [Serializable]
    public class CastigoJudicialDeudorTotales2
    {
        public int Cantidad { get; set; }

        [XmlIgnore]
        public decimal Total { get; set; }
        [XmlElement("Total")]
        public string TotalStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Total.ToString("N2"); }
            set { this.Total = decimal.Parse(value); }
        }
    }

    [Serializable]
    public class CastigoJudicialDetalle2
    {
        public string TipoDocumento { get; set; }
        public string NroDocumento { get; set; }
        public int DeudorCtcId { get; set; }

        public int NumRegistro { get; set; }

        [XmlIgnore]
        public DateTime FechaEmision { get; set; }
        [XmlElement("FechaEmision")]
        public string FechaEmisionStr
        {
            get { return this.FechaEmision.ToString("dd/MM/yyyy"); }
            set { this.FechaEmision = DateTime.Parse(value); }
        }

        [XmlIgnore]
        public DateTime FechaVcto { get; set; }
        [XmlElement("FechaVcto")]
        public string FechaVctoStr
        {
            get { return this.FechaVcto.ToString("dd/MM/yyyy"); }
            set { this.FechaVcto = DateTime.Parse(value); }
        }

        public string Moneda { get; set; }

        [XmlIgnore]
        public decimal Monto { get; set; }
        [XmlElement("Monto")]
        public string MontoStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Monto.ToString("N2"); }
            set { this.Monto = decimal.Parse(value); }
        }

        [XmlIgnore]
        public decimal Saldo { get; set; }
        [XmlElement("Saldo")]
        public string SaldoStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Saldo.ToString("N2"); }
            set { this.Saldo = decimal.Parse(value); }
        }

        public string Negocio { get; set; }
        public string DocOrigen { get; set; }
        public string DocCantidad { get; set; }
    }

    [Serializable]
    public class CastigoJudicialTotales2
    {
        [XmlIgnore]
        public decimal Total { get; set; }
        [XmlElement("Total")]
        public string TotalStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Total.ToString("N2"); }
            set { this.Total = decimal.Parse(value); }
        }
    }

    [Serializable]
    public class CastigoJudicialBruto2
    {
        public string CtcRut { get; set; }
        public string CtcNombre { get; set; }
        public string TipoDoc { get; set; }
        public string NroDoc { get; set; }
        public string Monto { get; set; }
        public string Saldo { get; set; }
        public DateTime FechaEmision { get; set; }
        public DateTime FechaVcto { get; set; }
        public string Negocio { get; set; }
        public string Moneda { get; set; }
        public int DetalleCtcId { get; set; }
        public string Ori { get; set; }
        public string Ant { get; set; }
        public string Direccion { get; set; }
        public string Tribunal { get; set; }
        public string Rol { get; set; }
        public int CbcNumProvCli { get; set; }
        public string NombreAsegurado { get; set; }

        public int Pclid { get; set; }
        public int Pcsid { get; set; }
        public string RutUsuario { get; set; }
        public string NombreUsuario { get; set; }
    }
}