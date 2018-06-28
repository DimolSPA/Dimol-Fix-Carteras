using System;
using System.Collections.Generic;

namespace Dimol.Carteras.dto
{
    public class SitrelCarga
    {
        public int Codemp { get; set; }
        public int Pclid { get; set; }
        public int IdCarga { get; set; }
        public DateTime FechaCarga { get; set; }
        public string Estado { get; set; }
        public string Error { get; set; }

        public string RutCliente { set; get; }
        public string NombreCliente { get; set; }
        public string TipoCartera { get; set; }
        public string CodigoCarga { get; set; }
        public string Contrato { get; set; }

        public List<SitrelArchivo> Archivos = new List<SitrelArchivo>();
        public List<SitrelDeudor> Deudores = new List<SitrelDeudor>();
        public List<SitrelDeudorDireccion> Direcciones = new List<SitrelDeudorDireccion>();
        public List<SitrelDeudorTelefono> Telefonos = new List<SitrelDeudorTelefono>();
        public List<SitrelDeudorEmail> Email = new List<SitrelDeudorEmail>();
        public List<SitrelOperacion> Operaciones = new List<SitrelOperacion>();
        public List<SitrelCuota> Cuotas = new List<SitrelCuota>();
        public List<SitrelPago> Pagos = new List<SitrelPago>();

        public List<ErrorCarga> ListaErrores = new List<ErrorCarga>();
    }

    public class SitrelArchivo
    {
        public int Codemp { get; set; }
        public int Pclid { get; set; }
        public int IdCarga { get; set; }
        public int CodigoArchivo { get; set; }
        public DateTime FechaArchivo { get; set; }
        public string NombreArchivo { get; set; }
        public string Estado { get; set; }
        public string Error { get; set; }
    }

    public class SitrelDeudor
    {
        public int Codemp { get; set; }
        public int Pclid { get; set; }
        public int Ctcid { get; set; }
        public int IdCarga { get; set; }
        public string   Rut { get; set; }
        public string  DigitoVerificador { get; set; }
        public string TipoPersona { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string NombreCompleto { get; set; }
        public string RazonSocial { get; set; }
        public string NombreFantasia { get; set; }
        public string Sexo { get; set; }
        public string SegmentoDeudor { get; set; }
        public string CuentaCorriente { get; set; }
        public string Origen { get; set; }
        public string Enviado { get; set; }
    }

    public class SitrelDeudorDireccion
    {
        public int Codemp { get; set; }
        public int Pclid { get; set; }
        public int Ctcid { get; set; }
        public int IdCarga { get; set; }
        public string Rut { get; set; }
        public string Direccion { get; set; }
        public string Ciudad { get; set; }
        public int Region { get; set; }
        public string Comuna { get; set; }
        public string TipoDireccion { get; set; }
        public string TipoPersona { get; set; }
        public string Origen { get; set; }
        public string Enviado { get; set; }
    }

    public class SitrelDeudorTelefono
    {
        public int Codemp { get; set; }
        public int Pclid { get; set; }
        public int Ctcid { get; set; }
        public int IdCarga { get; set; }
        public string Rut { get; set; }
        public string Numero { get; set; }
        public string CodigoArea { get; set; }
        public string Anexo { get; set; }
        public string TipoTelefono { get; set; }
        public string Origen { get; set; }
        public string Enviado { get; set; }
    }

    public class SitrelDeudorEmail
    {
        public int Codemp { get; set; }
        public int Pclid { get; set; }
        public int Ctcid { get; set; }
        public int IdCarga { get; set; }
        public string Rut { get; set; }
        public string Email { get; set; }
        public string Origen { get; set; }
        public string Enviado { get; set; }
    }

    public class SitrelOperacion
    {
        public int Codemp { get; set; }
        public int Pclid { get; set; }
        public int IdCarga { get; set; }
        public string Rut { get; set; }
        public string NumeroOperacion { get; set; }
        public string CodigoProducto { get; set; }
        public string NombreProducto { get; set; }
        public string TipoDeudor { get; set; }
        public string Moneda { get; set; }
        public decimal MontoMora { get; set; }
        public decimal SaldoInsoluto { get; set; }
        public decimal MontoOperacion { get; set; }
        public decimal DeudaTotal { get; set; }
        public int DiasMora { get; set; }
        public string EjecutivoCuenta { get; set; }
        public decimal MontoTotalInteres { get; set; }
        public string EstadoProducto { get; set; }
        public int FechaUltimoPago { get; set; }
        public string Campania { get; set; }
        public string Accion { get; set; }
        public string Contacto { get; set; }
        public string Respuesta { get; set; }
        public string Glosa { get; set; }
        public DateTime FechaGestion { get; set; }
        public string CodigoSucursal { get; set; }
        public string NombreSucursal { get; set; }
        public string DireccionSucursal { get; set; }
        public string TelefonoSucursal { get; set; }
        public int FechaVencimiento { get; set; }
        public string NombreEstrategia { get; set; }
        public string TipoPersona { get; set; }
    }

    public class SitrelCuota
    {
        public int Codemp { get; set; }
        public int Pclid { get; set; }
        public int IdCarga { get; set; }
        public string Rut { get; set; }
        public string NumeroOperacion { get; set; }
        public string Producto { get; set; }
        public int NumeroCuota { get; set; }
        public int FechaVencimiento { get; set; }
        public decimal MontoDetalle { get; set; }
        public decimal Capital { get; set; }
        public decimal Intereses { get; set; }
        public decimal Gastos { get; set; }
        public int DiasMora { get; set; }
    }

    public class SitrelPago
    {
        public int Codemp { get; set; }
        public int Pclid { get; set; }
        public int IdCarga { get; set; }
        public string Rut { get; set; }
        public string NumeroOperacion { get; set; }
        public string CodigoProducto { get; set; }
        public int FechaPago { get; set; }
        public decimal MontoPago { get; set; }
    }

    public class Direccion
    {
        public int Codemp { get; set; }
        public int Ctcid { get; set; }
        public string Calle { get; set; }
        public string Ciudad { get; set; }
        public string Region { get; set; }
        public string Comuna { get; set; }
        public string IdComuna { get; set; }
        public string TipoDireccion { get; set; }
        public string Pais { get; set; }
        public string Origen { get; set; }
        public string Enviado { get; set; }
    } 
}