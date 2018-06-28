CREATE PROCEDURE _Reversar_Imputacion_Documento_Estado(
@codemp int,
@conciliacionId int,
@userId int)
AS
BEGIN

--Cambiar estado
	update CONCILIACION_MOVIMIENTOS_DOCUMENTOS
	set conciliacion_estado_id = 1
	where conciliacion_id = @conciliacionId

END
