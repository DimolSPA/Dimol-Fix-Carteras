using System;
using System.ComponentModel;

namespace Dimol.Carteras.Models
{
    public class ComprobanteModel
    {
        public int Pclid { get; set; }
        public int Ctcid { get; set; }
        public int Ccbid { get; set; }
        public int Sbcid { get; set; }
        public int TerceroId { get; set; }
        public int IdContrato { get; set; }
        [DisplayName("Cliente")]
        public string RutCliente { set; get; }
        public string NombreCliente { get; set; }
        [DisplayName("Cliente")]
        public string NombreRutCliente { get; set; }
        [DisplayName("Deudor")]
        public string RutDeudor { set; get; }
        public string NombreFantasia { get; set; }
        [DisplayName("Deudor")]
        public string NombreRutDeudor { get; set; }
        [DisplayName("Tipo Cartera")]
        public string TipoCartera { get; set; }
        [DisplayName("Tipo Documento")]
        public string TipoDocumento { get; set; }
        [DisplayName("Número")]
        public string Numero { get; set; }
        [DisplayName("Fecha Ingreso")]
        public string FechaIngreso { get; set; }
        [DisplayName("Fecha Documento")]
        public string FechaDocumento { get; set; }
        [DisplayName("Fecha Vencimiento")]
        public string FechaVencimiento { get; set; }
        [DisplayName("Fecha Última Gestión")]
        public DateTime FechaUltimaGestion { get; set; }
        [DisplayName("Fecha Plazo")]
        public DateTime? FechaPlazo { get; set; }
        [DisplayName("Fecha Calculo Interes")]
        public DateTime? FechaCalculoInteres { get; set; }
        [DisplayName("Fecha Castigo")]
        public DateTime? FechaCastigo { get; set; }
        [DisplayName("Estado")]
        public string EstadoCpbt { get; set; }
        [DisplayName("Estado Cartera")]
        public string EstadoCartera { get; set; }
        [DisplayName("CodigoCarga")]
        public string CodigoCarga { get; set; }
        [DisplayName("Moneda")]
        public string Moneda { get; set; }
        [DisplayName("Tipo Cambio")]
        public decimal TipoCambio { get; set; }
        [DisplayName("Asegurado")]
        public string Asociado { get; set; }
        [DisplayName("Contrato")]
        public string Contrato { get; set; }
        [DisplayName("MotivoCobranza")]
        public string MotivoCobranza { get; set; }
        [DisplayName("Numero Negocio")]
        public string NumeroNegocio { get; set; }
        [DisplayName("Numero Agrupa Especial")]
        public string NumeroAgrupaEspecial { get; set; }
        [DisplayName("Originales")]
        public bool Originales { get; set; }
        [DisplayName("Antecedentes")]
        public bool Antecedentes { get; set; }
        [DisplayName("Monto Asignado")]
        public decimal Monto { get; set; }
        [DisplayName("Monto Documento")]
        public decimal MontoDocumento { get; set; }
        [DisplayName("Monto Saldo")]
        public decimal Saldo { get; set; }
        [DisplayName("Gasto PreJudicial")]
        public decimal GastoPreJudicial { get; set; }
        [DisplayName("Gasto Judicial")]
        public decimal GastoJudicial { get; set; }
        [DisplayName("Comentario")]
        public string Comentario { get; set; }

        public string NombreRutAsegurado { get; set; }

        public decimal Intereses { get; set; }
        public decimal Honorarios { get; set; }
        public string CalculoHonorarios { get; set; }
        public string NombreBanco { get; set; }
        public string RutGirador { get; set; }
        public string NombreGirador { get; set; }
        
        public string Retent { get; set; } // revisar q diantres es
        
        public string NumeroAgrupa { get; set; }
        public int Carta { get; set; }
        public string Cobrable { get; set; }
        public int Cctid { get; set; } // q es?
        public string SubcarteraRut { get; set; }
        public string SubcarteraNombre { get; set; }
        public string DocumentoOrigen { get; set; }
        public string docant { get; set; } //
        
        public int DiasVencido { get; set; }
        public decimal TotalDeuda { get; set; }
        public decimal Compromiso { get; set; }

        public bool DemandaPendiente { get; set; }
        [DisplayName("Rut Tercero")]
        public string RutTercero { set; get; }
        [DisplayName("Nombre Tercero")]
        public string NombreTercero { get; set; }
        [DisplayName("Id Cuenta")]
        public string IdCuenta { get; set; }
        [DisplayName("Desc. Cuenta")]
        public string DescripcionCuenta { get; set; }

        //DATOS DOCUMENTOS PREVISIONALES
        [DisplayName("Número de Resolución")]
        public string NumResolucion { set; get; }

        [DisplayName("Rut Representante 1")]
        public string RutRepresentante1 { set; get; }
        [DisplayName("Nombre Representante 1")]
        public string NombreRepresentante1 { get; set; }

        [DisplayName("Rut Representante 2")]
        public string RutRepresentante2 { set; get; }
        [DisplayName("Nombre Representante 2")]
        public string NombreRepresentante2 { get; set; }

        [DisplayName("Rut Representante 3")]
        public string RutRepresentante3 { set; get; }
        [DisplayName("Nombre Representante 3")]
        public string NombreRepresentante3 { get; set; }
    }
}