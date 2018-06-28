using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Caja.dto
{
    public class Tesoreria
    {
    }
    public class CuentaBancaria
    {
        public int CuentaId { get; set; }
        public string NumCuenta { get; set; }
        public string TipoCuenta { get; set; }
        public string Banco { get; set; }
        public decimal MontoConciliar { get; set; }
        public int Row { get; set; }
    }
    public class CartolaMovimiento
    {
        public int MovimientoId { get; set; }
        public string NumCuenta { get; set; }
        public DateTime FecMovimiento { get; set; }
        public decimal Monto { get; set; }
        public string Sucursal { get; set; }
        public string NumComprobante { get; set; }
        public string Movimiento { get; set; }
        public string Motivo { get; set; }
        public string MotivoSistema { get; set; }
        public string MotivoSistemaId { get; set; }
        public string Estado { get; set; }
        public string EstadoId { get; set; }
        public string Observacion { get; set; }
        public int CuentaId { get; set; }
        public int Row { get; set; }
    }
    public class DatosCargaCartola
    {
        public int Id { get; set; }
        public string NumCuenta { get; set; }
        public DateTime FecMovimiento { get; set; }
        public decimal Monto { get; set; }
        public string Motivo { get; set; }
        public string Sucursal { get; set; }
        public string NumComprobante { get; set; }
        public string Mensaje { get; set; }
    }

    public class CartolaMovimientoExcel
    {
        public string NumCuenta { get; set; }
        public DateTime FecMovimiento { get; set; }
        public decimal Monto { get; set; }
        public string Motivo { get; set; }
        public string Sucursal { get; set; }
        public string NumComprobante { get; set; }
        public string Movimiento { get; set; }
        public string Estado { get; set; }
        public string Observacion { get; set;}
    }

    public class DocumentoCustodia
    {
        public int CustodiaId { get; set; }
        public DateTime? FecDoc { get; set; }
        public string RutCliente { get; set; } 
        public string Cliente { get; set; }
        public string RutDeudor { get; set; }
        public string Deudor { get; set; }
        public decimal Monto { get; set; }
        public string Gestor { get; set; }
        public string GiradoA { get; set; }
        public string TipoBanco { get; set; }
        public string NumDocumento { get; set; }
        public string Estado { get; set; }
        public DateTime? FecProrroga { get; set; }
        public string Pclid { get; set; }
        public string Ctcid { get; set; }
        public string GestorId { get; set; }
        public string BancoId { get; set; }
        public string EstadoId { get; set; }
        public string ConciliacionId { get; set; }
        public int Row { get; set; }
    }
    public class DocumentoCustodiaGrid
    {
        public int AID { get; set; }
        public string NumDoc { get; set; }
        public string MontoDoc { get; set; }
        public string FechaDoc { get; set; }
        public string FechaProDoc { get; set; }
    }
    public class MovimientoConciliado
    {
        public int ConciliacionId { get; set; }
        public int MovimientoId { get; set; }
        public int CustodiaId { get; set; }
        public string Pclid { get; set; }
        public string Ctcid { get; set; }
        public string GestorId { get; set; }
        public string NumComprobante { get; set; }
        public string RutCliente { get; set; }
        public string Cliente { get; set; }
        public string RutDeudor { get; set; }
        public string Deudor { get; set; }
        public string MotivoSistema { get; set; }
        public decimal Monto { get; set; }
        public decimal Saldo { get; set; }
        public string Tipoconciliacion { get; set; }
        public string EstadoLiquidacion { get; set; }
        public DateTime FechaConciliacion { get; set; }
        public int Remesa { get; set; }
        public int Row { get; set; }
    }

    public class FormLiquidacion
    {
   
        public string NumComprobante { get; set; }
        public string RutCliente { get; set; }
        public string Cliente { get; set; }
        public string RutDeudor { get; set; }
        public string Deudor { get; set; }
        public decimal Monto { get; set; }
        public decimal Capital { get; set; }
        public decimal Honorario { get; set; }
        public decimal Interes { get; set; }
        public int CapitalPor { get; set; }
        public int InteresPor { get; set; }
        public int HonorarioPor { get; set; }
        public decimal GastoPre { get; set; }
        public decimal GastoJud { get; set; }
        public decimal MontoRebajado { get; set; }
        public string EstadoLiquidacionId { get; set; }
    }

    public class DocumentoDeudor
    {
        public string Pclid { get; set; }
        public string Ctcid { get; set; }
        public string Ccbid { get; set; }
        public string Asegurado { get; set; }
        public string TipoDocumento { get; set; }
        public string Numero { get; set; }
        public string Estado { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string Moneda { get; set; }
        public decimal Monto { get; set; }
        public decimal Saldo { get; set; }
        public decimal Intereses { get; set; }
        public decimal Honorarios { get; set; }
        public decimal GastoJudicial { get; set; }
        public decimal GastoPrejudicial { get; set; }
        public decimal TotalDeuda { get; set; }
        public int Row { get; set; }
    }
    public class DocumentoImputado
    {
        public string Pclid { get; set; }
        public string Ctcid { get; set; }
        public string Ccbid { get; set; }
        public string Asegurado { get; set; }
        public string TipoDocumento { get; set; }
        public string Numero { get; set; }
        public string Estado { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string Moneda { get; set; }
        public decimal Monto { get; set; }
        public decimal Saldo { get; set; }
        public string CapitalDebitado { get; set; }
        public decimal Intereses { get; set; }
        public string InteresDebitado { get; set; }
        public decimal Honorarios { get; set; }
        public string HonorarioDebitado { get; set; }
        public decimal GastoJudicial { get; set; }
        public string PagoJudDebitado { get; set; }
        public decimal GastoPrejudicial { get; set; }
        public string PagoPreDebitado { get; set; }
        public decimal TotalDeuda { get; set; }
        public string IndicaImputado { get; set; }
    }

    public class DocumentoPorImputar
    {
        public string Ccbid { get; set; }
        public string Numero { get; set; }
        public string Estado { get; set; }
        public string CapitalDebitado { get; set; }
        public string InteresDebitado { get; set; }
        public string HonorarioDebitado { get; set; }
        public string PagoJudDebitado { get; set; }
        public string PagoPreDebitado { get; set; }
        public string IndicaImputado { get; set; }
    }
    public class DocumentoPorFinalizar
    {
        public string Ccbid { get; set; }
        public string Numero { get; set; }
        public string Saldo { get; set; }
        public string Estado { get; set; }
    }

    public class MovimientoConciliadoAprobado
    {
        public int ConciliacionId { get; set; }
        public int MovimientoId { get; set; }
        public int CustodiaId { get; set; }
        public string Pclid { get; set; }
        public string Ctcid { get; set; }
        public string GestorId { get; set; }
        public string NumComprobante { get; set; }
        public string RutCliente { get; set; }
        public string Cliente { get; set; }
        public string RutDeudor { get; set; }
        public string Deudor { get; set; }
        public decimal Capital { get; set; }
        public decimal Interes { get; set; }
        public decimal Honorarios { get; set; }
        public decimal OtrosGastos { get; set; }
        public decimal MontoRecuperado { get; set; }
        public DateTime FechaConciliacion { get; set; }
        public int Row { get; set; }
    }

    public class ComprobanteRemesa
    {
        public int ImputacionId { get; set; }
        public int ConciliacionId { get; set; }
        public string Ccbid { get; set; }
        public string Pclid { get; set; }
        public string Ctcid { get; set; }
        public string NumComprobante { get; set; }
        public string Deudor { get; set; }
        public string Tipo {get; set;}
        public string NumDocumento { get; set; }
        public decimal Capital { get; set; }
        public decimal Interes { get; set; }
        public decimal Honorario { get; set; }
        public decimal RecuperadoGasto { get; set; }
        public decimal TotalRecuperado { get; set; }
        public string PorCapital { get; set; }
        public string PorInteres { get; set; }
        public string PorHonorario { get; set; }
        public decimal GananciaCapital { get; set; }
        public decimal GananciaInteres { get; set; }
        public decimal GananciaHonorario { get; set; }
        public decimal TotalGanancia { get; set; }
        public decimal TotalCliente { get; set; }
        public decimal Anticipo { get; set; }
        public int DocumentoId { get; set; }
        public decimal AnticipoDebitado { get; set; }
        public int ConciliacionTipoId { get; set; }
        public string ConciliacionTipo { get; set; }
    }

    public class Remesa
    {
        public int Id { get; set; }
        public string Cliente { get; set; }
        public decimal CapitalRecuperado { get; set; }
        public decimal InteresRecuperado { get; set; }
        public decimal HonorarioRecuperado { get; set; }
        public decimal Capital { get; set; }
        public decimal Interes { get; set; }
        public decimal Honorario { get; set; }
        public decimal TotalFactura { get; set; }
        public decimal TotalDimol { get; set; }
        public DateTime FechaRemesa { get; set; }
    }
    public class EfectivoCustodia
    {
        public int CustodiaId { get; set; }
        public DateTime? FecDoc { get; set; }
        public string RutCliente { get; set; }
        public string Cliente { get; set; }
        public string RutDeudor { get; set; }
        public string Deudor { get; set; }
        public decimal Monto { get; set; }
        public string Gestor { get; set; }
        public string GiradoA { get; set; }
        public string TipoBanco { get; set; }
        public string NumDocumento { get; set; }
        public string Estado { get; set; }
        public DateTime? FecProrroga { get; set; }
        public string Pclid { get; set; }
        public string Ctcid { get; set; }
        public string GestorId { get; set; }
        public string BancoId { get; set; }
        public string EstadoId { get; set; }
        public string ConciliacionId { get; set; }
        public string NumComprobante { get; set; }
        public string MotivoSistema { get; set; }
        public decimal Saldo { get; set; }
        public string Tipoconciliacion { get; set; }
        public string EstadoLiquidacion { get; set; }
        public DateTime FechaConciliacion { get; set; }
        public string EstadoConciliacionId { get; set; }
        public int Row { get; set; }
    }

    public class DocumentoCustodiaProtestado
    {
        public int CustodiaId { get; set; }
        public DateTime? FecDoc { get; set; }
        public string RutCliente { get; set; }
        public string Cliente { get; set; }
        public string RutDeudor { get; set; }
        public string Deudor { get; set; }
        public decimal Monto { get; set; }
        public string Gestor { get; set; }
        public string GiradoA { get; set; }
        public string TipoBanco { get; set; }
        public string NumDocumento { get; set; }
        public string Estado { get; set; }
        public DateTime? FecProrroga { get; set; }
        public string Pclid { get; set; }
        public string Ctcid { get; set; }
        public string GestorId { get; set; }
        public int Row { get; set; }
    }
    public class DocumentoCustodiaReporte
    {
        public string NumDoc { get; set; }
        public string Banco { get; set; }
        public string GiradoA { get; set; }
        public DateTime FechaDoc { get; set; }
        public decimal MontoDoc { get; set; }
        
    }
    public class Pago
    {
        public int ConciliacionId { get; set; }
        public int MovimientoId { get; set; }
        public int CustodiaId { get; set; }
        public string Pclid { get; set; }
        public string Ctcid { get; set; }
        public string GestorId { get; set; }
        public string NumComprobante { get; set; }
        public string RutCliente { get; set; }
        public string Cliente { get; set; }
        public string RutDeudor { get; set; }
        public string Deudor { get; set; }
        public string Ccbid { get; set; }
        public string Moneda { get; set; }
        public string TipoDocumento { get; set; }
        public string TipoCambio { get; set; }
        public string Numero { get; set; }
        public decimal Asignado { get; set; }
        public decimal Capital { get; set; }
        public decimal Interes { get; set; }
        public decimal Honorario { get; set; }
        public decimal GastoPre { get; set; }
        public decimal GastoJud { get; set; }
        public string RutAsegurado { get; set; }
        public string Asegurado { get; set; }
        public DateTime FecCancela { get; set; }
        public string Gestor { get; set; }
        public string TipoConciliacion { get; set; }
        public DateTime FechaAsignado { get; set; }
        public int Row { get; set; }
    }
}
