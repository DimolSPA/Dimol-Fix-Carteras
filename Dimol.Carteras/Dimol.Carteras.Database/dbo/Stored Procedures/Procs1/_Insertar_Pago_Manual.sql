CREATE PROCEDURE _Insertar_Pago_Manual(
@codemp int,
@pclid NUMERIC(15,0),
@ctcid NUMERIC(15,0),
@fecMovimiento datetime,
@monto decimal(15,2),
@tipoConciliacionId int,
@userId int)
AS
BEGIN
	declare @gestorId int = 43, @numCuenta varchar(60), @numComprobante int, @excelId int = 0, @conciliacionId int = 0, @movimientoId int = 0,
	@tipoMovimientoId int = 1,@tipoMotivoId int = 13, @tipoEstadoId int = 1
	
	set @numCuenta = (select top 1 NUM_CUENTA from TESORERIA_CUENTAS_BANCARIAS where NUM_CUENTA = '1')

	SET @numComprobante = (SELECT IsNull(Max(NUM_COMPROBANTE)+1, 1)
							FROM CONCILIACION_MOVIMIENTOS_DOCUMENTOS)
	-- Se crea la cartola
	SET @excelId = (SELECT IsNull(Max(EXCEL_ROW_ID)+1, 1)
						FROM CARTOLA_BANCO_EXCEL)
			INSERT INTO CARTOLA_BANCO_EXCEL
				(EXCEL_ROW_ID,NUM_CUENTA,FEC_MOVIMIENTO,MONTO,DESCRIPCION,SUCURSAL,NUM_COMPROBANTE_REF,IDCARGA,USRID_REGISTRO, PROCESADO)
				VALUES
				(@excelId, @numCuenta,@fecMovimiento,@monto, UPPER('Pago'), UPPER('Pago'),'', 1, @userId, 'S')
	--Se ingresa el movimiento
	SET @movimientoId = (SELECT IsNull(Max(MOVIMIENTO_ID)+1, 1)
						FROM CARTOLA_MOVIMIENTOS)
			INSERT INTO CARTOLA_MOVIMIENTOS
				(MOVIMIENTO_ID,CODEMP,NUM_CUENTA,FEC_MOVIMIENTO,MONTO,SUCURSAL,NUM_COMPROBANTE_REF,
				TIPO_MOVIMIENTO_BANCO_ID, TIPO_MOTIVO_BANCO_ID, TIPO_ESTADO_BANCO_ID,EXCEL_ROW_ID,USRID_REGISTRO)
				VALUES
				(@movimientoId, @codemp, @numCuenta,@fecMovimiento,@monto, UPPER('Pago'),'',
				@tipoMovimientoId, @tipoMotivoId, @tipoEstadoId, @excelId, @userId)

			
	--Se crea conciliacion
	SET @conciliacionId = (SELECT IsNull(Max(CONCILIACION_ID)+1, 1)
						FROM CONCILIACION_MOVIMIENTOS_DOCUMENTOS)

	INSERT INTO CONCILIACION_MOVIMIENTOS_DOCUMENTOS
        (CONCILIACION_ID,MOVIMIENTO_ID,NUM_COMPROBANTE,CUSTODIA_ID,PCLID,CTCID,GESTORID,CONCILIACION_TIPO_ID,USRID_REGISTRO)
		VALUES
		(@conciliacionId, @movimientoId,@numComprobante, NULL, @pclid, @ctcid, @gestorId,@tipoConciliacionId,@userId)
	
	--Actualizar Movimientos CARTOLA_MOVIMIENTOS
	update CARTOLA_MOVIMIENTOS
	set ESTATUS_ID = 2 --Conciliado
	where MOVIMIENTO_ID = @movimientoId

	--Ingresar al Historial
	exec dbo._Insertar_Cartola_Movimientos_Historial @codemp, @movimientoId, @numcuenta, @userId

	select @conciliacionId ConciliacionId
END
