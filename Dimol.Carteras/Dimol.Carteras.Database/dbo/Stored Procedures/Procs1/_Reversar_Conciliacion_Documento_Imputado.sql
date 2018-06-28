CREATE PROCEDURE _Reversar_Conciliacion_Documento_Imputado(
@codemp int,
@conciliacionId int,
@userId int)
AS
BEGIN

--Eliminar imputacion
	delete CONCILIACION_DOCUMENTO_IMPUTADO
	where conciliacion_id = @conciliacionId

END