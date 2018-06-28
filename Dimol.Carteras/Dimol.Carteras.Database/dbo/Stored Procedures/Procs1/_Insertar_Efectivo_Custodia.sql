CREATE PROCEDURE [dbo].[_Insertar_Efectivo_Custodia](
@codemp int,
@pclid NUMERIC(15,0),
@ctcid NUMERIC(15,0),
@gestorId int,
@recibe varchar(100),
@bancoId int,
@fecDoc datetime,
@monto decimal(15,2),
@userId int)
AS
BEGIN
	declare @custodiaId int = 0, @historialId int = 0, @conciliacionId int = 0, @numComprobante int = 0
	--Se agrega documento Custodia
	SET @custodiaId = (SELECT IsNull(Max(CUSTODIA_ID)+1, 1)
						FROM DOCUMENTOS_CUSTODIA)
	INSERT INTO [dbo].[DOCUMENTOS_CUSTODIA]
           ([CUSTODIA_ID],[CODEMP],[NUM_CUENTA] ,[PCLID],[CTCID],[GESTORID]
           ,[NUM_DOCUMENTO],[RECIBE],[BANCO_ID],[TIPO_ESTADO_BANCO_ID],[FEC_DOC]
           ,[FEC_PRORROGA],[MONTO],[USRID_REGISTRO],[TIPODOCUMENTO])
     VALUES
           (@custodiaId ,@codemp,NULL,@pclid ,@ctcid,@gestorId
           ,NULL,@recibe,@bancoId,2,@fecDoc
           ,NULL,@monto,@userId, 2)
	--se agrega al historial
	SET @historialId = (SELECT IsNull(Max(HISTORIAL_ID)+1, 1)
						FROM DOCUMENTOS_CUSTODIA_HISTORIAL)
	INSERT INTO [dbo].[DOCUMENTOS_CUSTODIA_HISTORIAL]
           ([HISTORIAL_ID],[CUSTODIA_ID] ,[CODEMP],[NUM_CUENTA],[PCLID],[CTCID],[GESTORID]
           ,[NUM_DOCUMENTO],[RECIBE],[BANCO_ID],[TIPO_ESTADO_BANCO_ID],[FEC_DOC],[FEC_PRORROGA]
           ,[MONTO],[USRID_REGISTRO])
     VALUES
           (@historialId,@custodiaId,@codemp,NULL,@pclid,@ctcid,@gestorId
           ,NULL,@recibe,@bancoId,2,@fecDoc,NULL
           ,@monto,@userId)
	--se ingresa conciliacion sin movimiento, es efectivo
	
	--Se crea conciliacion
	SET @conciliacionId = (SELECT IsNull(Max(CONCILIACION_ID)+1, 1)
						FROM CONCILIACION_MOVIMIENTOS_DOCUMENTOS)
	SET @numComprobante = (SELECT IsNull(Max(NUM_COMPROBANTE)+1, 1)
							FROM CONCILIACION_MOVIMIENTOS_DOCUMENTOS)
	INSERT INTO CONCILIACION_MOVIMIENTOS_DOCUMENTOS
        (CONCILIACION_ID,MOVIMIENTO_ID,NUM_COMPROBANTE,CUSTODIA_ID,PCLID,CTCID,GESTORID,CONCILIACION_TIPO_ID, CONCILIACION_ESTADO_ID,USRID_REGISTRO)
		VALUES
		(@conciliacionId, NULL,@numComprobante,@custodiaId, @pclid, @ctcid, @gestorId,1,1,@userId)
END
