using System;

namespace Dimol.Carteras.dto
{
    public class Comprobante
    {
        public int Id { get; set; }
        public int Pclid { get; set; }
        public int Ctcid { get; set; }
        public int Sbcid { get; set; }
        public string RutCliente { set; get; }
        public string NombreCliente { get; set; }
        public string TipoCpbtNombre { get; set; }
        public int Ccbid { get; set; }
        public string NumeroCpbt { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime FechaDocumento { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public DateTime FechaUltimaGestion { get; set; }
        public DateTime? FechaPlazo { get; set; }
        public DateTime? FechaCalculoInteres { get; set; }
        public DateTime? FechaCastigo { get; set; }
        public string EstadoCartera { get; set; }
        public string EstadoJudicial { get; set; }
        public string EstadoCpbt { get; set; }
        public int CodigoMoneda { get; set; }
        public decimal TipoCambio { get; set; }
        public decimal MontoAsignado { get; set; }
        public decimal Monto { get; set; }
        public decimal Saldo { get; set; }
        public decimal GastoJudicial { get; set; }
        public decimal GastoOtros { get; set; }
        public decimal Intereses { get; set; }
        public decimal Honorarios { get; set; }
        public string CalculoHonorarios { get; set; }
        public string NombreBanco { get; set; }
        public string RutGirador { get; set; }
        public string NombreGirador { get; set; }
        public string Comentario { get; set; }
        public string Retent { get; set; }
        public string NumeroEspecial { get; set; }
        public string NumeroAgrupa { get; set; }
        public int Carta { get; set; }
        public string Cobrable { get; set; }
        public int Contrato { get; set; }
        public string SubcarteraRut { get; set; }
        public string SubcarteraNombre { get; set; }
        public string Originales { get; set; }
        public string Antecedentes { get; set; }
        public int TipoCartera { get; set; }
        public int DiasVencido { get; set; }
        public string Moneda { get; set; }
        public decimal TotalDeuda { get; set; }
        public decimal Compromiso { get; set; }
        public string CodigoCarga { get; set; }
        public string TipoDocumento { get; set; }
        public string MotivoCobranza { get; set; }
        public int DemandaPendiente  { get; set; }
        public int TerceroId { get; set; }
        public string IdCuenta { get; set; }
        public string DescripcionCuenta { get; set; }
        public decimal SaldoCLP { get; set; }
        public decimal SaldoUSD { get; set; }

        //Previsional
        public string NumeroResolucion { get; set; }
        public DateTime? FechaResolucion { get; set; }
        public string RutRepresentante1 { get; set; }
        public string NombreRepresentante1 { get; set; }
        public string RutRepresentante2 { get; set; }
        public string NombreRepresentante2 { get; set; }
        public string RutRepresentante3 { get; set; }
        public string NombreRepresentante3 { get; set; }
    }

    public class DeudorDocumento
    {
        public string RutCliente { get; set; }
        public string Pclid { get; set; }
        public string NombreCliente { get; set; }
        public string Rut { get; set; }
        public string Ctcid { get; set; }
        public string Ccbid { get; set; }
        public string NombreFantasia { get; set; }
        public string TipoDocumento { get; set; }
        public string Numero { get; set; }
        public DateTime FechaDocumento { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string EstadoCpbt { get; set; }
        public string Estado { get; set; }
        public decimal Monto { get; set; }
        public decimal Saldo { get; set; }
        public string Moneda { get; set; }
        public string Carga { get; set; }
        public string Negocio { get; set; }
    }

    public class DemandaAvenimientoRol
    {
        public int Codemp { get; set; }
        public int Rolid { get; set; }
        public DateTime FechaDemanda { get; set; }
        public int CuotasDemanda { get; set; }
        public decimal MontoDemanda { get; set; }
        public decimal MontoPrimeraCuotaDemanda { get; set; }
        public decimal MontoUltimaCuotaDemanda { get; set; }
        public DateTime FechaPrimeraCuotaDemanda { get; set; }
        public DateTime FechaUltimaCuotaDemanda { get; set; }
        public decimal InteresDemanda { get; set; }

        public DateTime FechaAvenimiento { get; set; }
        public int CuotasAvenimiento { get; set; }
        public decimal MontoAvenimiento { get; set; }
        public decimal MontoPrimeraCuotaAvenimiento { get; set; }
        public decimal MontoUltimaCuotaAvenimiento { get; set; }
        public DateTime FechaPrimeraCuotaAvenimiento { get; set; }
        public DateTime FechaUltimaCuotaAvenimiento { get; set; }
        public decimal InteresAvenimiento { get; set; }

    }

    public class DetalleEstados
    {
        public string Utiliza { get; set; }
        public string Prejudicial { get; set; }
        public string SolicitaFecha { get; set; }
        public string GeneraRetiro { get; set; }
        public string Compromiso { get; set; }
        public int Agrupa { get; set; }
    }

    public class ComprobanteCorto
    {
        public string TipoCpbtNombre { get; set; }
        public int Ccbid { get; set; }
        public string NumeroCpbt { get; set; }
        public string FechaDocumento { get; set; }
        public decimal Monto { get; set; }
        public decimal Saldo { get; set; }
        public decimal Compromiso { get; set; }
        public int id { get; set; }
        public decimal m { get; set; }
    }

    public class TerceroDocumento
    {
        public string Rut { get; set; }
        public int TerceroId { get; set; }
        public string Nombre { get; set; }
    }

    public class ComprobanteCabecera
    {
        public int Pclid { get; set; }
        public int Tpcid { get; set; }
        public string TipoComprobante { get; set; }
        public int Folio { get; set; }
        public string Cliente { get; set; }
        public DateTime FecEmision { get; set; }
        public string CbtEstado { get; set; }
        public string Estado { get; set; }
        public decimal Saldo { get; set; }
        public decimal Neto { get; set; }
        public int Row { get; set; }
    }

    public class ComprobanteCabeceraDetalle
    {
        public string RutDeudor { get; set; }
        public string Deudor { get; set; }
        public string Tipo { get; set; }
        public string Numero { get; set; }
        public DateTime FechaAsignacion { get; set; }
        public decimal Monto { get; set; }
        public decimal Saldo { get; set; }
        public decimal Asignado { get; set; }
        public string UltimoEstado { get; set; }
        public string Estado { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string CbtEstado { get; set; }
        public string Asegurado { get; set; }
        public int Row { get; set; }
        public int Pclid { get; set; }
        public int Ctcid { get; set; }
        public int Ccbid { get; set; }
        public string RolNumero { get; set; }
        public int RolId { get; set; }
    }
}
