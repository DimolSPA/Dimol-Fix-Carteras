using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Dimol.Caja.Models
{
    public class CajaModel
    {
        [DisplayName("Comprobante")]
        [StringLength(20)]
        public string TipoDocumento { get; set; }
        [DisplayName("Tipo")]
        [StringLength(20)]
        public string Tipo { get; set; }
        [DisplayName("Número Interno")]
        [RegularExpression(@"[0-9]*", ErrorMessage = "Debe ingresar un número.")]
        public string NumeroInterno { get; set; }
        [DisplayName("Fecha Ingreso")]
        [StringLength(10)]
        public DateTime? FechaIngreso { get; set; }

        [DisplayName("Cliente")]
        [StringLength(1024)]
        public string NombreRutCliente { get; set; }
        [DisplayName("pclid")]
        [StringLength(20)]
        public string Pclid { get; set; }
        [DisplayName("Deudor")]
        [StringLength(1024)]
        public string NombreRutDeudor { get; set; }
        [DisplayName("ctcid")]
        [StringLength(20)]
        public string Ctcid { get; set; }

        [DisplayName("Fecha Documento")]
        [StringLength(10)]
        public DateTime? FechaDocumento { get; set; }
        [DisplayName("Número Documento")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Debe ingresar un número.")]
        public string NumeroDocumento { get; set; }


        [DisplayName("Documento Empleado")]
        public bool FlagEmpleado { get; set; }
        [DisplayName("Empleado")]
        [StringLength(20)]
        public string Empleado { get; set; }

        [DisplayName("Estado")]
        [StringLength(20)]
        public string Estado { get; set; }

        [DisplayName("Documento Empresa")]
        public bool FlagEmpresa { get; set; }

        [DisplayName("Depositar")]
        public bool Depositar { get; set; }

        [DisplayName("Moneda")]
        [StringLength(20)]
        public string Moneda { get; set; }
        [DisplayName("Monto")]
        [RegularExpression(@"[0-9]*\,?[0-9]+", ErrorMessage = "Monto debe ser un número.")]
        public string Monto { get; set; }
        [DisplayName("Tipo Cambio")]
        [RegularExpression(@"[0-9]*\,?[0-9]+", ErrorMessage = "Tipo de cambio debe ser un número.")]
        public string TipoCambio { get; set; }
        [DisplayName("Saldo")]
        [RegularExpression(@"[0-9]*\,?[0-9]+", ErrorMessage = "Saldo debe ser un número.")]
        public string Saldo { get; set; }

        [DisplayName("Negociación")]
        [StringLength(20)]
        public string Negociacion { get; set; }

        [DisplayName("Tipo")]
        [StringLength(20)]
        public string TipoCanc { get; set; }


        [DisplayName("Fotos")]
        public string Archivo { get; set; }

        //Negociacion
        [DisplayName("Cliente")]
        [StringLength(1024)]
        public string NombreRutClienteNeg { get; set; }
        [DisplayName("pclid")]
        [StringLength(20)]
        public string PclidNeg { get; set; }
        [DisplayName("Deudor")]
        [StringLength(1024)]
        public string NombreRutDeudorNeg { get; set; }
        [DisplayName("ctcid")]
        [StringLength(20)]
        public string CtcidNeg { get; set; }

        [DisplayName("Negociación")]
        [StringLength(20)]
        public string NegociacionNeg { get; set; }
    }
}