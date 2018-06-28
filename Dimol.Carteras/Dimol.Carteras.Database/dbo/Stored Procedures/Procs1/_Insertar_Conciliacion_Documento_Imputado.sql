CREATE PROCEDURE [dbo].[_Insertar_Conciliacion_Documento_Imputado](
@codemp int,
@conciliacionId int,
@ccbid int,
@estado char(1),
@montoCapital decimal(15,2),
@montoInteres decimal(15,2),
@montoHonorario decimal(15,2),
@montoGastoPre decimal(15,2),
@montoGastoJud decimal(15,2),
@userId int)
AS
BEGIN
	declare @imputacionId int = 0
	--Se crea conciliacion
	SET @imputacionId = (SELECT IsNull(Max(CONCILIACION_DOCUMENTO_IMPUTADO_ID)+1, 1)
						FROM CONCILIACION_DOCUMENTO_IMPUTADO)

	INSERT INTO [CONCILIACION_DOCUMENTO_IMPUTADO]
			   ([CONCILIACION_DOCUMENTO_IMPUTADO_ID]
			   ,[CODEMP]
			   ,[CONCILIACION_ID]
			   ,[CCBID]
			   ,[SALDO]
			   ,[INTERES]
			   ,[HONORARIO]
			   ,[GASTOPRE]
			   ,[GASTOJUD]
			   ,[USRID_REGISTRO]
			   ,[ESTADO])
		 VALUES
			   (@imputacionId
			   ,@codemp
			   ,@conciliacionId
			   ,@ccbid
			   ,@montoCapital
			   ,@montoInteres
			   ,@montoHonorario
			   ,@montoGastoPre
			   ,@montoGastoJud
			   ,@userId
			   ,@estado)
END
