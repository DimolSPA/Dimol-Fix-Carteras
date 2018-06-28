CREATE PROCEDURE [dbo].[_Insertar_Documento_Custodia_Historial](
@codemp int,
@custodiaId int,
@userId int)
AS
BEGIN
	declare @historialId int = 0
	--Se agrega al historial
	SET @historialId = (SELECT IsNull(Max(HISTORIAL_ID)+1, 1)
						FROM DOCUMENTOS_CUSTODIA_HISTORIAL)

	INSERT INTO [dbo].[DOCUMENTOS_CUSTODIA_HISTORIAL]
           ([HISTORIAL_ID]
           ,[CUSTODIA_ID]
           ,[CODEMP]
           ,[NUM_CUENTA]
           ,[PCLID]
           ,[CTCID]
           ,[GESTORID]
           ,[NUM_DOCUMENTO]
           ,[RECIBE]
           ,[BANCO_ID]
           ,[TIPO_ESTADO_BANCO_ID]
           ,[FEC_DOC]
           ,[FEC_PRORROGA]
           ,[MONTO]
           ,[USRID_REGISTRO]
           ,[FEC_REGISTRO])
	SELECT @historialId
      ,[CUSTODIA_ID]
      ,[CODEMP]
      ,[NUM_CUENTA]
      ,[PCLID]
      ,[CTCID]
      ,[GESTORID]
      ,[NUM_DOCUMENTO]
      ,[RECIBE]
      ,[BANCO_ID]
      ,[TIPO_ESTADO_BANCO_ID]
      ,[FEC_DOC]
      ,[FEC_PRORROGA]
      ,[MONTO]
      ,@userId
      ,GETDATE()
  FROM [DOCUMENTOS_CUSTODIA_HISTORIAL] WHERE CODEMP = @codemp and CUSTODIA_ID = @custodiaId
END
