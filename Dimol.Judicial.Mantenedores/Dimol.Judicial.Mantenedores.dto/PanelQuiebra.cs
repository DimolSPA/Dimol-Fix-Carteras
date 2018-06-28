using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Judicial.Mantenedores.dto
{
    public class PanelQuiebra
    {
        public int QuiebraId { get; set; }
        public int RolId { get; set; }
        public int Sbcid { get; set; }
        public string RolNumero { get; set; }
        public string Cliente { get; set; }
        public string Deudor { get; set; }
        public string RutDeudor { get; set; }
        public string NombreTribunal { get; set; }
        public string Asegurado { get; set; }
        public decimal Cuantia { get; set; }
        public Nullable<System.DateTime> FechaIngresoQuiebra { get; set; }
        public string Materia { get; set; }
        public string Solicitante { get; set; }
        public decimal MtoCostasPersonales { get; set; }
        public Nullable<System.DateTime> FecCostasPersonales { get; set; }
        public Nullable<System.DateTime> FecIngresoSolicitud { get; set; }
        public Nullable<System.DateTime> FecNotificacionSolicitud { get; set; }
        public Nullable<System.DateTime> FecAudienciaInicial { get; set; }
        public Nullable<System.DateTime> FecAudienciaPrueba { get; set; }
        public Nullable<System.DateTime> FecAudienciaFallo { get; set; }
        public Nullable<System.DateTime> FecResolucionLiquidacion { get; set; }
        public Nullable<System.DateTime> FecResolucionLiquidacionBC { get; set; }
        public Nullable<System.DateTime> FecResolucionReorganizacionBC { get; set; }
        public Nullable<System.DateTime> FecVerificacion { get; set; }
        public Nullable<System.DateTime> FecAcreditacionPoder { get; set; }
        public Nullable<System.DateTime> FecJuntaConstitutiva { get; set; }
        public Nullable<System.DateTime> FecJuntaDeliberativa { get; set; }
        public string StatusAcuerdo { get; set; }
        public Nullable<System.DateTime> FecAcuerdo { get; set; }
        public Nullable<System.DateTime> FecVerificadoAcreditado { get; set; }
        public Nullable<System.DateTime> FecNomCreditoVerificado { get; set; }
        public Nullable<System.DateTime> FecImpugnacion { get; set; }
        public Nullable<System.DateTime> FecNomCreditoReconocido { get; set; }
        public Nullable<System.DateTime> FecSolicitudAntecedente { get; set; }
        public Nullable<System.DateTime> FecRecepcionAntecedente { get; set; }
        public Nullable<System.DateTime> FecEnvioAntecedente { get; set; }
        public Nullable<System.DateTime> FecEmisionND { get; set; }
        public decimal MtoEmision { get; set; }
        public Nullable<System.DateTime> FecRepartos { get; set; }
        public decimal MtoRepartos { get; set; }
        public Nullable<System.DateTime> FecDevolucion { get; set; }
        public decimal PgoCostasPersonales { get; set; }
        public Nullable<System.DateTime> FecPgoCostasPersonales { get; set; }
        public Nullable<System.DateTime> FecAprobacionCtaFinal { get; set; }
        public Nullable<System.DateTime> FecCertificadoIncobrable { get; set; }
        public int Row { get; set; } 
    }
    public class LiquidacionRol
    {
        public int Quiebra_Id { get; set; }
        public int RolId { get; set; }
        public string Rol { get; set; }
        public string Nombre { get; set; }
        public decimal Cuantia { get; set; }
        public int Materia { get; set; }
        public int row { get; set; } 
    }
    public class PanelQuiebraAvance
    {
        public int QuiebraId { get; set; }
        public string Solicitante { get; set; }
        public decimal MtoCostasPersonales { get; set; }
        public DateTime FecCostasPersonales { get; set; }
        public DateTime FecIngresoSolicitud { get; set; }
        public DateTime FecNotificacionSolicitud { get; set; }
        public DateTime FecAudienciaInicial { get; set; }
        public DateTime FecAudienciaPrueba { get; set; }
        public DateTime FecAudienciaFallo { get; set; }
        public DateTime FecResolucionLiquidacion { get; set; }
        public DateTime FecResolucionLiquidacionBC { get; set; }
        public DateTime FecResolucionReorganizacionBC { get; set; }
        public DateTime FecVerificacion { get; set; }
        public DateTime FecAcreditacionPoder { get; set; }
        public DateTime FecJuntaConstitutiva { get; set; }
        public DateTime FecJuntaDeliberativa { get; set; }
        public string StatusAcuerdo { get; set; }
        public DateTime FecAcuerdo { get; set; }
        public DateTime FecVerificadoAcreditado { get; set; }
        public DateTime FecNomCreditoVerificado { get; set; }
        public DateTime FecImpugnacion { get; set; }
        public DateTime FecNomCreditoReconocido { get; set; }
        public DateTime FecSolicitudAntecedente { get; set; }
        public DateTime FecRecepcionAntecedente { get; set; }
        public DateTime FecEnvioAntecedente { get; set; }
        public DateTime FecEmisionND { get; set; }
        public decimal MtoEmision { get; set; }
        public DateTime FecRepartos { get; set; }
        public decimal MtoRepartos { get; set; }
        public DateTime FecDevolucion { get; set; }
        public decimal PgoCostasPersonales { get; set; }
        public DateTime FecPgoCostasPersonales { get; set; }
        public DateTime FecAprobacionCtaFinal { get; set; }
        public DateTime FecCertificadoIncobrable { get; set; }
    }
   
	
    public class LiquidacionRolQuiebra
    {
        public string Rol { get; set; }
        public int? Sbcid { get; set; }
        public decimal Cuantia { get; set; }
       
    }

    public class LiquidacionRolDocumentos
    {
        public int Pclid { get; set; }
        public int Ctcid { get; set; }
        public int Ccbid { get; set; }

    }
    public class PanelQuiebraReparto
    {
        public int QuiebraId { get; set; }
        public int RepartoId { get; set; }
        public Nullable<System.DateTime> FecReparto { get; set; }
        public decimal MtoReparto { get; set; }
        public int Row { get; set; } 
    }
    public class PanelQuiebraGrafica
    {
        public string Id { get; set; }
        public string Total { get; set; }
        public string MtoAsignado { get; set; }
        public string Item { get; set; }
        public string Parent { get; set; }
    }
    public class PanelQuiebraPublicados
    {
        public int QuiebraId { get; set; }
        public int Pclid { get; set; }
        public string Cliente { get; set; }
        public int Ctcid { get; set; }
        public string Rut { get; set; }
        public string Deudor { get; set; }
        public string Asegurado { get; set; }
        public Nullable<System.DateTime> FecPublicacion { get; set; }
        public int Dias { get; set; }
        public int Row { get; set; }
       
    }
}
