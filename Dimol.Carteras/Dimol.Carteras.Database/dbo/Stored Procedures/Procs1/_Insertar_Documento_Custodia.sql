CREATE PROCEDURE _Insertar_Documento_Custodia(
@codemp int,
@numCuenta varchar(60),
@pclid NUMERIC(15,0),
@ctcid NUMERIC(15,0),
@gestorId int,
@numDocumento varchar(20),
@recibe varchar(100),
@bancoId int,
@fecDoc datetime,
@monto decimal(15,2),
@userId int)
AS
BEGIN
	declare @custodiaId int = 0, @historialId int = 0
	--Se agrega documento Custodia
	SET @custodiaId = (SELECT IsNull(Max(CUSTODIA_ID)+1, 1)
						FROM DOCUMENTOS_CUSTODIA)
	INSERT INTO [dbo].[DOCUMENTOS_CUSTODIA]
           ([CUSTODIA_ID],[CODEMP],[NUM_CUENTA] ,[PCLID],[CTCID],[GESTORID]
           ,[NUM_DOCUMENTO],[RECIBE],[BANCO_ID],[TIPO_ESTADO_BANCO_ID],[FEC_DOC]
           ,[FEC_PRORROGA],[MONTO],[USRID_REGISTRO])
     VALUES
           (@custodiaId ,@codemp,@numCuenta,@pclid ,@ctcid,@gestorId
           ,@numDocumento,@recibe,@bancoId,2,@fecDoc
           ,NULL,@monto,@userId)
	--se agrega al historial
	SET @historialId = (SELECT IsNull(Max(HISTORIAL_ID)+1, 1)
						FROM DOCUMENTOS_CUSTODIA_HISTORIAL)
	INSERT INTO [dbo].[DOCUMENTOS_CUSTODIA_HISTORIAL]
           ([HISTORIAL_ID],[CUSTODIA_ID] ,[CODEMP],[NUM_CUENTA],[PCLID],[CTCID],[GESTORID]
           ,[NUM_DOCUMENTO],[RECIBE],[BANCO_ID],[TIPO_ESTADO_BANCO_ID],[FEC_DOC],[FEC_PRORROGA]
           ,[MONTO],[USRID_REGISTRO])
     VALUES
           (@historialId,@custodiaId,@codemp,@numCuenta,@pclid,@ctcid,@gestorId
           ,@numDocumento,@recibe,@bancoId,2,@fecDoc,NULL
           ,@monto,@userId)
END




