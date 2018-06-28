CREATE PROCEDURE _Actualizar_Efectivo_Custodia_Movimiento_Conciliacion(
--declare
@codemp int,
@movimientoId int,
@custodiaId int,
@conciliacionId int,
@userId int
)
AS
BEGIN
	declare @num_cuenta varchar(60);
	--Actualizar Movimiento CONCILIACION_MOVIMIENTOS_DOCUMENTOS
	update CONCILIACION_MOVIMIENTOS_DOCUMENTOS
	set MOVIMIENTO_ID = @movimientoId
	where CONCILIACION_ID = @conciliacionId and CUSTODIA_ID = @custodiaId

	-- Se concilia el movimiento
	update CARTOLA_MOVIMIENTOS
	set ESTATUS_ID = 2
	where CODEMP = @codemp and MOVIMIENTO_ID = @movimientoId

	set @num_cuenta = (select NUM_CUENTA from CARTOLA_MOVIMIENTOS where CODEMP = @codemp and MOVIMIENTO_ID = @movimientoId)

	exec dbo._Insertar_Cartola_Movimientos_Historial @codemp, @movimientoId, @num_cuenta, @userId
		
END
