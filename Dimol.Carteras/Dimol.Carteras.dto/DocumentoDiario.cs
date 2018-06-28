using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Carteras.dto
{
    public class DocumentoDiario
    {
        public int CodigoEmpresa { get; set; }
        public int CodigoSucursal { get; set; }
        public int Anio { get; set; }
        public int NumeroDocumento { get; set; }
        public int TipoDocumento { get; set; }
        public string TipoMovimiento { get; set; }
        public int? IdCliente { get; set; }
        public string Propio { get; set; }
        public int? IdBanco { get; set; }
        public string CuentaCorriente { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime FechaDocumento{ get; set; }
        public DateTime FechaVencimiento { get; set; }
        public int EstadoDocumento { get; set; }
        public string NumeroCuenta { get; set; }
        public decimal Monto { get; set; }
        public decimal Saldo { get; set; }
        public int CodigoMoneda { get; set; }
        public decimal TipoCambio { get; set; }
        public string Titular { get; set; }
        public string RutPagador { get; set; }
        public string NombrePagador { get; set; }
        public int? IdDeudor { get; set; }
        public string Empleado { get; set; }
        public int? IdEmpleado { get; set; }
        public string Custodia { get; set; }
        public string DocumentoEmpresa { get; set; }
        public string PagDir { get; set; }
        public int VecesDepositado { get; set; }
        public DateTime? FechaDeposito { get; set; }
        public bool Depositar { get; set; }
        public string  RutDeposito { get; set; }
        public string  NombreDeposito { get; set; }
        public string NumeroDeposito { get; set; }
        public DateTime? FecDep { get; set; }
        public bool Pendiente { get; set; }
        public int? AnioNegociacion { get; set; }
        public int? IdNegociacion { get; set; }
        public string Comentario { get; set; }
    }
}
