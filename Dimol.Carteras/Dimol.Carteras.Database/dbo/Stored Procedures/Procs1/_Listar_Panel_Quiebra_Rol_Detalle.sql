CREATE PROCEDURE [dbo].[_Listar_Panel_Quiebra_Rol_Detalle]
(
@QuiebraId int)
AS
BEGIN
	SET NOCOUNT ON;
	select
	  QUIEBRA_ID QuiebraId,
	  SOLICITANTE,
	  MTO_COSTAS_PERSONALES MtoCostasPersonales,
      FEC_COSTAS_PERSONALES FecCostasPersonales,
      FEC_INGRESO_SOLICITUD FecIngresoSolicitud,
      FEC_NOTIFICACION_SOLICITUD FecNotificacionSolicitud,
      FEC_AUDIENCIA_INICIAL FecAudienciaInicial,
      FEC_AUDIENCIA_PRUEBA FecAudienciaPrueba,
      FEC_AUDIENCIA_FALLO FecAudienciaFallo,
      FEC_RESOLUCION_LIQUIDACION FecResolucionLiquidacion,
      FEC_RESOLUCION_LIQUIDACION_BC FecResolucionLiquidacionBC,
      FEC_RESOLUCION_REORGANIZACION_BC FecResolucionReorganizacionBC,
      FEC_VERIFICACION FecVerificacion,
      FEC_ACREDITACION_PODER FecAcreditacionPoder,
      FEC_JUNTA_CONSTITUTIVA FecJuntaConstitutiva,
      FEC_JUNTA_DELIBERATIVA FecJuntaDeliberativa,
      STATUS_ACUERDO StatusAcuerdo,
      FEC_ACUERDO FecAcuerdo,
      FEC_VERIFICADO_ACREDITADO FecVerificadoAcreditado,
      FEC_NOMINA_CREDITO_VERIFICADO FecNomCreditoVerificado,
      FEC_IMPUGNACION FecImpugnacion,
      FEC_NOMINA_CREDITO_RECONOCIDO FecNomCreditoReconocido,
      FEC_SOLICITUD_ANTECEDENTE FecSolicitudAntecedente,
      FEC_RECEPCION_ANTECEDENTE FecRecepcionAntecedente,
      FEC_ENVIO_ANTECEDENTE FecEnvioAntecedente,
      FEC_EMISION_ND FecEmisionND,
      MTO_EMISION_ND MtoEmision,
      FEC_REPARTOS FecRepartos,
      MTO_REPARTOS MtoRepartos,
      FEC_DEVOLUCION FecDevolucion,
      PGO_COSTAS_PERSONALES PgoCostasPersonales,
      FEC_PGO_COSTAS_PERSONALES FecPgoCostasPersonales,
      FEC_APROBACION_CTAFINAL FecAprobacionCtaFinal,
      FEC_CERTIFICADO_INCOBRABLE FecCertificadoIncobrable
	
from PANEL_QUIEBRA_DETALLE
where QUIEBRA_ID = @QuiebraId

END
