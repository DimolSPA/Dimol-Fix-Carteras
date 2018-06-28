CREATE PROCEDURE [dbo].[_Guardar_Panel_Quiebra_Rol_Detalle_Reglas]
(@rolId int,
@quiebraId int,
@solicitante varchar(2),
@fecResolLiqui datetime, --Resolucion de liquidacion
@fecJuntaConsti datetime,-- Junta Constitutiva
@fecNomCreditoVeri datetime, --Nómina de créditos verificados
@fecAprobCtaFinal datetime, -- Aprobación de cuenta final
@userId int)
AS
BEGIN
	declare @quiebraIdIndex int, @solicitantePordefecto varchar(2) = 'S', @countquiebras int = 0;
	set @countquiebras = (Select count(QUIEBRA_ID) from PANEL_QUIEBRA where ROLID = @rolId and QUIEBRA_ID != @quiebraId)
	IF (@solicitante = 'S')
	begin
		set @solicitantePordefecto = 'N'
	end
	else
	begin
		set @solicitantePordefecto = ''
	end
	IF (@countquiebras > 0)
	BEGIN
	declare curAplicaReglas cursor for
	Select QUIEBRA_ID from PANEL_QUIEBRA where ROLID = @rolId and QUIEBRA_ID != @quiebraId
	open curAplicaReglas
	fetch next from curAplicaReglas into @quiebraIdIndex
	while (@@FETCH_STATUS = 0)
	begin
		declare @existQuiebraId int = 0;
		
		set @existQuiebraId = (select count(quiebra_id) from PANEL_QUIEBRA_DETALLE where QUIEBRA_ID = @quiebraIdIndex)
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
			VALUES(@quiebraIdIndex, @solicitantePordefecto,0, NULL, NULL,NULL,
				NULL, NULL, NULL, @fecResolLiqui, NULL,
				NULL, NULL, NULL, @fecJuntaConsti, NULL,
				NULL, NULL, NULL, @fecNomCreditoVeri, NULL, NULL,
				NULL, NULL, NULL, NULL, 0,
				NULL, 0, NULL, 0, NULL,
				@fecAprobCtaFinal, NULL,@userId)
			
		end 
		else
		begin
			IF (@solicitante = 'S')
			BEGIN
				UPDATE PANEL_QUIEBRA_DETALLE
				SET SOLICITANTE = 'N', 
				FEC_RESOLUCION_LIQUIDACION=@fecResolLiqui,
				FEC_JUNTA_CONSTITUTIVA=@fecJuntaConsti,
				FEC_NOMINA_CREDITO_VERIFICADO =@fecNomCreditoVeri, 
				FEC_APROBACION_CTAFINAL=@fecAprobCtaFinal
				WHERE QUIEBRA_ID = @quiebraIdIndex
			END
			ELSE
			BEGIN
				UPDATE PANEL_QUIEBRA_DETALLE
				SET FEC_RESOLUCION_LIQUIDACION=@fecResolLiqui,
				FEC_NOMINA_CREDITO_VERIFICADO =@fecNomCreditoVeri, 
				FEC_APROBACION_CTAFINAL=@fecAprobCtaFinal
				WHERE QUIEBRA_ID = @quiebraIdIndex
			END
		end
		fetch next from curAplicaReglas into @quiebraIdIndex
	end
	close curAplicaReglas
	deallocate curAplicaReglas
	END
	ELSE
	BEGIN
		UPDATE PANEL_QUIEBRA_DETALLE
		SET FEC_RESOLUCION_LIQUIDACION=@fecResolLiqui,
		FEC_NOMINA_CREDITO_VERIFICADO =@fecNomCreditoVeri, 
		FEC_APROBACION_CTAFINAL=@fecAprobCtaFinal
		WHERE QUIEBRA_ID = @quiebraId
	END
END
