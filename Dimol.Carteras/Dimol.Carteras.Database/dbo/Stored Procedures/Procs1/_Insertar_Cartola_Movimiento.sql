CREATE PROCEDURE _Insertar_Cartola_Movimiento(
@codemp int,
@numCuenta varchar(60),
@fecMovimiento datetime,
@monto decimal(15,2),
@sucursal varchar(100),
@numComprobante varchar(100),
@tipoMovimientoId int,
@tipoMotivoId int,
@tipoEstadoId int,
@archivoRowId int,
@userId int)
AS
BEGIN
	declare @movimientoId int = 0
	--Se agrega al historial
	SET @movimientoId = (SELECT IsNull(Max(MOVIMIENTO_ID)+1, 1)
						FROM CARTOLA_MOVIMIENTOS)
	INSERT INTO CARTOLA_MOVIMIENTOS
        (MOVIMIENTO_ID,CODEMP,NUM_CUENTA,FEC_MOVIMIENTO,MONTO,SUCURSAL,NUM_COMPROBANTE_REF,
		TIPO_MOVIMIENTO_BANCO_ID, TIPO_MOTIVO_BANCO_ID, TIPO_ESTADO_BANCO_ID,EXCEL_ROW_ID,USRID_REGISTRO)
		VALUES
		(@movimientoId, @codemp, @numCuenta,@fecMovimiento,@monto, UPPER(@sucursal),@numComprobante,
		@tipoMovimientoId, @tipoMotivoId, @tipoEstadoId, @archivoRowId,@userId)
END




