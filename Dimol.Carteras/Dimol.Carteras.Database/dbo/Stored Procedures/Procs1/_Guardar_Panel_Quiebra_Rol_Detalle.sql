CREATE PROCEDURE [dbo].[_Guardar_Panel_Quiebra_Rol_Detalle]
(@quiebraId int,@solicitante varchar(2),@mtoCostasPersonales DECIMAL (15,2),@fecCostasPersonales datetime,@fecIngrSolicitud datetime,@fecNotSolicitud datetime,
@fecAudienciaIni datetime,@fecAudienciaPrueba datetime,@fecAudienciaFallo datetime,@fecResolLiqui datetime,@fecResolLiquiBC datetime,
@fecResolReorgBC datetime,@fecVerificacion datetime,@fecAcreditaPoder datetime,@fecJuntaConsti datetime,@fecJuntaDelibe datetime,
@statusAcuerdo varchar(1), @fecAcuerdo datetime, @fecVerificaAcredita datetime,@fecNomCreditoVeri datetime, @fecImpugnacion datetime, @fecNomCreditoRec Datetime,
@fecSolAntecedente datetime, @fecRecepAntecedente datetime, @fecEnvAntecedente datetime, @fecEmisionND datetime, @mtoEmisionND DECIMAL (15,2),
@fecRepartos datetime, @MtoRepartos DECIMAL (15,2), @fecDevolucion datetime, @pgoCostPersonales DECIMAL (15,2), @fecpgoCostPersonales datetime,
@fecAprobCtaFinal datetime, @fecCertiIncobrable datetime,@userId int)
AS
BEGIN
	declare @existQuiebraId int = 0;
	set @existQuiebraId = (select count(quiebra_id) from PANEL_QUIEBRA_DETALLE where QUIEBRA_ID = @quiebraId)
	if @existQuiebraId = 0
	begin
		INSERT INTO PANEL_QUIEBRA_DETALLE(
			QUIEBRA_ID,SOLICITANTE, MTO_COSTAS_PERSONALES,FEC_COSTAS_PERSONALES,FEC_INGRESO_SOLICITUD,FEC_NOTIFICACION_SOLICITUD,
			FEC_AUDIENCIA_INICIAL,FEC_AUDIENCIA_PRUEBA,FEC_AUDIENCIA_FALLO,FEC_RESOLUCION_LIQUIDACION,FEC_RESOLUCION_LIQUIDACION_BC,
			FEC_RESOLUCION_REORGANIZACION_BC,FEC_VERIFICACION,FEC_ACREDITACION_PODER,FEC_JUNTA_CONSTITUTIVA,FEC_JUNTA_DELIBERATIVA,
			STATUS_ACUERDO,FEC_ACUERDO,FEC_VERIFICADO_ACREDITADO,FEC_NOMINA_CREDITO_VERIFICADO,FEC_IMPUGNACION,FEC_NOMINA_CREDITO_RECONOCIDO,
			FEC_SOLICITUD_ANTECEDENTE, FEC_RECEPCION_ANTECEDENTE, FEC_ENVIO_ANTECEDENTE,FEC_EMISION_ND,MTO_EMISION_ND,
			FEC_REPARTOS, MTO_REPARTOS, FEC_DEVOLUCION, PGO_COSTAS_PERSONALES, FEC_PGO_COSTAS_PERSONALES,
			FEC_APROBACION_CTAFINAL, FEC_CERTIFICADO_INCOBRABLE, USRID_REGISTRO)
		VALUES(@quiebraId, @solicitante,@mtoCostasPersonales, @fecCostasPersonales, @fecIngrSolicitud,@fecNotSolicitud,
			@fecAudienciaIni, @fecAudienciaPrueba, @fecAudienciaFallo, @fecResolLiqui, @fecResolLiquiBC,
			@fecResolReorgBC, @fecVerificacion, @fecAcreditaPoder, @fecJuntaConsti, @fecJuntaDelibe,
			@statusAcuerdo, @fecAcuerdo, @fecVerificaAcredita, @fecNomCreditoVeri, @fecImpugnacion, @fecNomCreditoRec,
			@fecSolAntecedente, @fecRecepAntecedente, @fecEnvAntecedente, @fecEmisionND, @mtoEmisionND,
			@fecRepartos, @MtoRepartos, @fecDevolucion, @pgoCostPersonales, @fecpgoCostPersonales,
			@fecAprobCtaFinal, @fecCertiIncobrable,@userId)
	
	end
	else
	begin 
		UPDATE PANEL_QUIEBRA_DETALLE
		SET SOLICITANTE = @solicitante, MTO_COSTAS_PERSONALES=@mtoCostasPersonales,
		FEC_COSTAS_PERSONALES=@fecCostasPersonales,FEC_INGRESO_SOLICITUD=@fecIngrSolicitud,
		FEC_NOTIFICACION_SOLICITUD=@fecNotSolicitud,FEC_AUDIENCIA_INICIAL=@fecAudienciaIni,
		FEC_AUDIENCIA_PRUEBA=@fecAudienciaPrueba,FEC_AUDIENCIA_FALLO=@fecAudienciaFallo,
		FEC_RESOLUCION_LIQUIDACION=@fecResolLiqui,FEC_RESOLUCION_LIQUIDACION_BC=@fecResolLiquiBC,
		FEC_RESOLUCION_REORGANIZACION_BC=@fecResolReorgBC,FEC_VERIFICACION=@fecVerificacion,
		FEC_ACREDITACION_PODER=@fecAcreditaPoder,FEC_JUNTA_CONSTITUTIVA=@fecJuntaConsti,
		FEC_JUNTA_DELIBERATIVA=@fecJuntaDelibe, STATUS_ACUERDO=@statusAcuerdo,
		FEC_ACUERDO=@fecAcuerdo, FEC_VERIFICADO_ACREDITADO=@fecVerificaAcredita,
		FEC_NOMINA_CREDITO_VERIFICADO =@fecNomCreditoVeri, FEC_IMPUGNACION=@fecImpugnacion,
		FEC_NOMINA_CREDITO_RECONOCIDO =@fecNomCreditoRec,FEC_SOLICITUD_ANTECEDENTE=@fecSolAntecedente,
		FEC_RECEPCION_ANTECEDENTE=@fecRecepAntecedente,FEC_ENVIO_ANTECEDENTE=@fecEnvAntecedente,
		FEC_EMISION_ND=@fecEmisionND, MTO_EMISION_ND=@mtoEmisionND,
		FEC_REPARTOS=@fecRepartos, MTO_REPARTOS=@MtoRepartos,
		FEC_DEVOLUCION=@fecDevolucion, PGO_COSTAS_PERSONALES=@pgoCostPersonales,
		FEC_PGO_COSTAS_PERSONALES=@fecpgoCostPersonales,FEC_APROBACION_CTAFINAL=@fecAprobCtaFinal,
		FEC_CERTIFICADO_INCOBRABLE=@fecCertiIncobrable
		WHERE QUIEBRA_ID = @quiebraId

		
	end

END
