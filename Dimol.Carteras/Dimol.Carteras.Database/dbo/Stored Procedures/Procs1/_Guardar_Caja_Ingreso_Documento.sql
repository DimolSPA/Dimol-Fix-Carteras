CREATE PROCEDURE [dbo].[_Guardar_Caja_Ingreso_Documento](
@documentoId int,
@codemp int,
@rec varchar(20),
@pclid numeric(15,0), -- 
@ctcid numeric(15,0), -- 
@sbcid int, --
@codmon int,
@valorIngreso decimal(15,2), -- 
@estatus int,
@userId int)
AS
BEGIN
	declare @existDocumentoId int = 0, @historialStatus int = 0, @historialDocumentoId int = 0, @montoIngreso decimal(15,2)= @valorIngreso;
	set @existDocumentoId = (select count(DOCUMENTO_ID) from CAJA_RECEPCION_DOCUMENTOS where DOCUMENTO_ID = @documentoId)

	-- Se calcula monto Ingreso, de acuerdo al tipo de moneda
	if (@codmon = 2)--UF
	begin
	--set @montoIngreso = (select MNV_VALOR from MONEDAS_VALORES where MNV_CODMON = 2 and MNV_FECHA = dateadd(day,datediff(day,1,GETDATE()),0))
		set @montoIngreso = (select top 1 MNV_VALOR from MONEDAS_VALORES where MNV_CODMON = 2 order by MNV_FECHA desc)
		set @montoIngreso =ROUND((@montoIngreso * @valorIngreso),0)
	end
	else
	begin
		if (@codmon = 3)--DOLAR
		begin
			set @montoIngreso = (select top 1 MNV_VALOR from MONEDAS_VALORES where MNV_CODMON = 3 order by MNV_FECHA desc)
		set @montoIngreso =ROUND((@montoIngreso * @valorIngreso),0)
		end
	end

	if @existDocumentoId = 0
	begin
	-- INSERTAR DATOS EN CAJA_RECEPCION_DOCUMENTOS
		SET @documentoId = (SELECT IsNull(Max(DOCUMENTO_ID)+1, 1)
							FROM CAJA_RECEPCION_DOCUMENTOS)

		INSERT INTO CAJA_RECEPCION_DOCUMENTOS
				   (DOCUMENTO_ID,
				   CODEMP,
				   REC,
				   PCLID,
				   CTCID,
				   SBCID,
				   CODMON,
				   MONTO_INGRESO,
				   USRID_REGISTRO,
				   VALOR_INGRESO,
				   SALDO)
			 VALUES
				   (@documentoId
				   ,@codemp
				   ,@rec
				   ,@pclid
				   ,@ctcid
				   ,@sbcid
				   ,@codmon
				   ,@montoIngreso
				   ,@userId,
				    @valorIngreso,
					@montoIngreso)
		--Se agrega al historial de Estatus
		SET @historialStatus = (SELECT IsNull(Max(HISTORIAL_ESTATUS_ID)+1, 1)
							FROM CAJA_RECEPCION_DOCUMENTOS_HISTORIAL_ESTATUS)
		INSERT INTO CAJA_RECEPCION_DOCUMENTOS_HISTORIAL_ESTATUS
			   (HISTORIAL_ESTATUS_ID, DOCUMENTO_ID,ESTATUS_ID,USRID_CREACION)
		 VALUES
			   (@historialStatus,@documentoId,@estatus,@userId)
		--Se agrega al historial
		SET @historialDocumentoId = (SELECT IsNull(Max(HISTORIAL_ID)+1, 1)
							FROM CAJA_RECEPCION_DOCUMENTOS_HISTORIAL)
		INSERT INTO CAJA_RECEPCION_DOCUMENTOS_HISTORIAL
           (HISTORIAL_ID,DOCUMENTO_ID,CODEMP,REC,PCLID,CTCID,SBCID,CODMON,MONTO_INGRESO,
		   ESTATUS_ID,USRID_REGISTRO)
		 VALUES
			(@historialDocumentoId, @documentoId,@codemp,@rec,@pclid,@ctcid,@sbcid,@codmon,@montoIngreso,
			@estatus,@userId)

	select @documentoId documentoId

	end
	else
	begin
		UPDATE CAJA_RECEPCION_DOCUMENTOS
		SET REC =@rec, PCLID = @pclid, CTCID = @ctcid, SBCID = @sbcid, CODMON = @codmon, 
		MONTO_INGRESO= @montoIngreso, VALOR_INGRESO = @valorIngreso, SALDO = @montoIngreso
		WHERE  DOCUMENTO_ID = @documentoId
		
		--Se agrega al historial
		SET @historialDocumentoId = (SELECT IsNull(Max(HISTORIAL_ID)+1, 1)
							FROM CAJA_RECEPCION_DOCUMENTOS_HISTORIAL)
		INSERT INTO CAJA_RECEPCION_DOCUMENTOS_HISTORIAL
           (HISTORIAL_ID,DOCUMENTO_ID,CODEMP,REC,PCLID,CTCID,SBCID,CODMON,MONTO_INGRESO,
		   ESTATUS_ID,USRID_REGISTRO)
		 VALUES
			(@historialDocumentoId, @documentoId,@codemp,@rec,@pclid,@ctcid,@sbcid,@codmon,@montoIngreso,
			@estatus,@userId)

		select @documentoId documentoId
	end
END
