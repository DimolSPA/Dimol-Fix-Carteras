CREATE PROCEDURE [dbo].[_Insertar_Caja_Recepcion_Documento_Historial](
@documentoId int,
@codemp int,
@estatus int,
@userId int)
AS
BEGIN
	declare @historialDocumentoId int = 0, @rec varchar(20), @pclid int, @ctcid int, @sbcid int, 
	@codmon int, @montoIngreso decimal(15,2),@montoFacturar decimal(15,2), 
	@criterio int, @observaciones varchar(500), @numfact varchar(20);

	select @rec= REC,@pclid= PCLID, @ctcid= CTCID, @sbcid= SBCID, @estatus= ESTATUS_ID,
		@codmon= CODMON,@montoIngreso = MONTO_INGRESO, @montoFacturar= MONTO_FACTURAR, 
		@criterio= CRITERIO_ID, @observaciones= OBSERVACIONES, @numfact= NUM_FACT
	from CAJA_RECEPCION_DOCUMENTOS where DOCUMENTO_ID = @documentoId
	--Se agrega al historial
	SET @historialDocumentoId = (SELECT IsNull(Max(HISTORIAL_ID)+1, 1)
						FROM CAJA_RECEPCION_DOCUMENTOS_HISTORIAL)
	INSERT INTO CAJA_RECEPCION_DOCUMENTOS_HISTORIAL
        (HISTORIAL_ID,DOCUMENTO_ID,CODEMP,REC,PCLID,CTCID,SBCID,CODMON,MONTO_INGRESO,
		ESTATUS_ID,CRITERIO_ID,MONTO_FACTURAR,OBSERVACIONES,NUM_FACT,USRID_REGISTRO)
		VALUES
		(@historialDocumentoId, @documentoId,@codemp,@rec,@pclid,@ctcid,@sbcid,@codmon,@montoIngreso,
		@estatus,@criterio, @montoFacturar, @observaciones, @numfact, @userId)
END
