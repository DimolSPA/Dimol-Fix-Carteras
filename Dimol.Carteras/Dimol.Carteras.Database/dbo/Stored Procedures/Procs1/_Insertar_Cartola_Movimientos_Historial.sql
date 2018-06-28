CREATE PROCEDURE _Insertar_Cartola_Movimientos_Historial(
@codemp int,
@movimientoId int,
@num_cuenta varchar(60),
@userId int)
AS
BEGIN
	declare @fecMovimiento datetime,@monto decimal(15,2), @sucursal varchar(100), @numRefComp varchar(100),
	@tipoMovimientoId int, @tipoMotivoId int, @tipoEstadoId int, @estatusId int;

	select @fecMovimiento =FEC_MOVIMIENTO, @monto= MONTO, @sucursal =SUCURSAL, @numRefComp =NUM_COMPROBANTE_REF,
	@tipoMovimientoId = TIPO_MOVIMIENTO_BANCO_ID, @tipoMotivoId = TIPO_MOTIVO_BANCO_ID, @tipoEstadoId= TIPO_ESTADO_BANCO_ID,
	@estatusId =ESTATUS_ID from CARTOLA_MOVIMIENTOS where CODEMP =@codemp and MOVIMIENTO_ID = @movimientoId

	declare @historialId int = 0
	--Se agrega al historial
	SET @historialId = (SELECT IsNull(Max(HISTORIAL_ID)+1, 1)
						FROM CARTOLA_MOVIMIENTOS_HISTORIAL)

	INSERT INTO CARTOLA_MOVIMIENTOS_HISTORIAL
        (HISTORIAL_ID,MOVIMIENTO_ID,CODEMP,NUM_CUENTA,FEC_MOVIMIENTO,MONTO,SUCURSAL,NUM_COMPROBANTE_REF,
		TIPO_MOVIMIENTO_BANCO_ID,TIPO_MOTIVO_BANCO_ID,TIPO_ESTADO_BANCO_ID,ESTATUS_ID,USRID_REGISTRO)
		VALUES
		(@historialId, @movimientoId,@codemp,@num_cuenta, @fecMovimiento, @monto,@sucursal,@numRefComp,
		@tipoMovimientoId,@tipoMotivoId,@tipoEstadoId,@estatusId,@userId)
END

