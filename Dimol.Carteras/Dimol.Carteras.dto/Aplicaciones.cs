using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Carteras.dto
{
    public class Aplicaciones
    {
        public int CodigoEmpresa { get; set; }
        public int CodigoSucursal { get; set; }
        public int Anio { get; set; }
        public int Mes { get; set; }
        public int NumeroAplicacion { get; set; }
        public int Tipo { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public DateTime? FechaAplicacion { get; set; }
        public int Accion { get; set; }
        public int IdUsuario { get; set; }
    }

    public class AplicaionesItems
    {
        public int CodigoEmpresa { get; set; }
        public int CodigoSucursal { get; set; }
        public int Anio { get; set; }
        public int NumeroAplicacion { get; set; }
        public int Item { get; set; }
        public int? AnioDocumento { get; set; }
        public int? NumeroDocumento { get; set; }
        public int? AnioDocumento2 { get; set; }
        public int? NumeroDocumento2 { get; set; }
        public int? TipoDocumento { get; set; }
        public int? Numero { get; set; }
        public int? TipoDocumento2 { get; set; }
        public int? Numero2 { get; set; }
        public int? IdCliente { get; set; }
        public int? IdDeudor { get; set; }
        public int? Ccbid { get; set; }
        public decimal Capital { get; set; }
        public decimal Interes { get; set; }
        public decimal Honorario { get; set; }
        public decimal GastoPrejudicial { get; set; }
        public decimal GastoJudicial { get; set; }
        public int? IdGestor { get; set; }
        public int? IdVendedor { get; set; }
        public bool Remesa { get; set; }
    }
}
