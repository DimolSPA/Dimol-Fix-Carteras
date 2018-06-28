CREATE PROCEDURE [dbo].[_Traspasar_Comercial_Caja_Documento](
@documentoId int,
@codemp int,
@estatus int,
@userId int)
AS
BEGIN
	declare @existDocumentoId int = 0, @historialStatus int = 0, @historialDocumentoId int = 0;
	
	set @existDocumentoId = (select count(DOCUMENTO_ID) from CAJA_RECEPCION_DOCUMENTOS where DOCUMENTO_ID = @documentoId)

	if @existDocumentoId > 0
	begin
		UPDATE CAJA_RECEPCION_DOCUMENTOS
		SET ESTATUS_ID = @estatus
		WHERE  DOCUMENTO_ID = @documentoId
	
		--Se agrega al historial de Estatus
		SET @historialStatus = (SELECT IsNull(Max(HISTORIAL_ESTATUS_ID)+1, 1)
							FROM CAJA_RECEPCION_DOCUMENTOS_HISTORIAL_ESTATUS) 
		INSERT INTO CAJA_RECEPCION_DOCUMENTOS_HISTORIAL_ESTATUS
			   (HISTORIAL_ESTATUS_ID, DOCUMENTO_ID,ESTATUS_ID,USRID_CREACION)
		 VALUES
			   (@historialStatus,@documentoId,@estatus,@userId)
		--Se agrega al historial
		exec dbo._Insertar_Caja_Recepcion_Documento_Historial @documentoId,@codemp,@estatus,@userId
	end
	
END
