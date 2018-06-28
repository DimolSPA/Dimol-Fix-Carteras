using System;

namespace Dimol.Judicial.Mantenedores.dto
{
    public class TraspasoJudicialCandidato
    {
        public int Pclid { get; set; }
        public int Ctcid { get; set; }
        public string Cliente { get; set; }
        public string RutDeudor { get; set; }
        public string Deudor { get; set; }
        public decimal MontoTotal { get; set; }
        public int Ccbid { get; set; }
        public string Tipo { get; set; }
        public string Numero { get; set; }
        public decimal Monto { get; set; }
        public decimal Saldo { get; set; }
        public string Estado { get; set; }
        public string EstadoCpbt { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public DateTime FechaAsignacion { get; set; }
        public string Asegurado { get; set; }

        //Previsional
        public string NumResolucion { get; set; }
        public DateTime FecResolucion { get; set; }
    }

    public class TraspasoJudicialPendiente
    {
        public int Tpcid { get; set; }
        public int Numero { get; set; }
        public string Cliente { get; set; }
        public string Tipo { get; set; }
        public string NumeroProveedor { get; set; }
        public DateTime Fecha { get; set; }
    }

    public class TraspasoJudicialHecho
    {
        public int Pclid { get; set; }
        public int Ctcid { get; set; }
        public string Cliente { get; set; }
        public string RutDeudor { get; set; }
        public string Deudor { get; set; }
        public string Fecha { get; set; }
        public int Ccbid { get; set; }
        public string Tipo { get; set; }
        public string Numero { get; set; }
        public decimal Monto { get; set; }
        public decimal Saldo { get; set; }
        public string Estado { get; set; }
        public string EstadoCpbt { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public DateTime FechaAsignacion { get; set; }
        public int Row { get; set; }

        //Previsional
        public string NumResolucion { get; set; }
        public DateTime FecResolucion { get; set; }
    }

    public class DocumentoTraspasar
    {
        public int Ccbid { get; set; }
        public decimal Monto { get; set; }
        public decimal Saldo { get; set; }
    }

    public class DocumentoReversar
    {
        public int Pclid { get; set; }
        public int Ctcid { get; set; }
        public int Ccbid { get; set; }
        public string Tipo { get; set; }
        public string Numero { get; set; }
        public decimal Monto { get; set; }
        public decimal Saldo { get; set; }
        public string UltimoEstado { get; set; }
        public string Estado { get; set; }
        public DateTime FechaVencimiento { get; set; }

    }
}
