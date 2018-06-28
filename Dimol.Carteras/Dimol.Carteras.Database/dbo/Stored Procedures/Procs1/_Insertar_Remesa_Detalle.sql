CREATE PROCEDURE [dbo].[_Insertar_Remesa_Detalle](
@remesaId int,
@imputacionId int,
@conciliacionId int,
@codemp int,
@ccbid int,
@pclid int,
@ctcid int,
@numcomprobante int,
@capitalrecuperado decimal(15,2),
@interesrecuperado decimal(15,2),
@honorariorecuperado decimal(15,2),
@porcapital numeric(3,0),
@porinteres numeric(3,0),
@porhonorario numeric(3,0),
@capital decimal(15,2),
@interes decimal(15,2),
@honorario decimal(15,2),
@userId int)
AS
BEGIN
declare @idRemesaDetalle int = 0
	set @idRemesaDetalle = (SELECT IsNull(Max(REMESA_DETALLE_ID)+1, 1)
						FROM REMESA_DETALLE)
	INSERT INTO REMESA_DETALLE(REMESA_DETALLE_ID, REMESA_ID, IMPUTACION_ID, CONCILIACION_ID,
	CODEMP, CCBID,PCLID, CTCID, NUMCOMPROBANTE,CAPITALRECUPERADO, INTERESRECUPERADO, 
	HONORARIORECUPERADO, PORCAPITAL, PORINTERES,PORHONORARIO, CAPITALGANANCIA, INTERESGANANCIA,HONORARIOGANANCIA,USRID_REGISTRO)
	VALUES(@idRemesaDetalle, @remesaId, @imputacionId,@conciliacionId, 
	@codemp, @ccbid,@pclid,@ctcid,@numcomprobante,@capitalrecuperado, @interesrecuperado,
	@honorariorecuperado, @porcapital, @porinteres, @porhonorario,@capital,@interes,@honorario, @userId)


END
