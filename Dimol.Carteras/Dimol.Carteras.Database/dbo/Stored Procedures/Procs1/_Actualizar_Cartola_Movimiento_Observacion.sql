CREATE PROCEDURE [dbo].[_Actualizar_Cartola_Movimiento_Observacion](
@codemp int,
@movimientoId int,
@cuentaId int,
@estadoId int,
@observacion varchar(100),
@userId int)
AS
BEGIN
	declare @num_cuenta varchar(60);
	set  @num_cuenta = (select NUM_CUENTA
										from TESORERIA_CUENTAS_BANCARIAS 
										where CUENTA_ID = @cuentaId )
	--Actualizar Movimientos CARTOLA_MOVIMIENTOS
	update CARTOLA_MOVIMIENTOS
	set TIPO_ESTADO_BANCO_ID = @estadoId, OBSERVACION = @observacion
	where CODEMP = @codemp and MOVIMIENTO_ID = @movimientoId

	--Ingresar al Historial
	exec dbo._Insertar_Cartola_Movimientos_Historial @codemp, @movimientoId, @num_cuenta, @userId
	
END

