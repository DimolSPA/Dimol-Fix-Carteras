CREATE PROCEDURE _Insertar_Conciliacion_Movimiento(
@codemp int,
@movimientoId int,
@numComprobante int,
@custodiaId int,
@pclid NUMERIC(15,0),
@ctcid NUMERIC(15,0),
@gestorId int,
@tipoConciliacionId int,
@num_cuenta varchar(60),
@userId int)
AS
BEGIN
	
	
	--El NUM_COMPROBANTE, puede ser un campo ingresado por el usuario o generado por el sistema
	if (@numComprobante is null)
	begin
		SET @numComprobante = (SELECT IsNull(Max(NUM_COMPROBANTE)+1, 1)
							FROM CONCILIACION_MOVIMIENTOS_DOCUMENTOS)
	end
	--el CUSTODIA_ID, puede venir null si es una conciliacion sin documentos registrados en DOCUMENTOS_CUSTODIA

	declare @conciliacionId int = 0
	--Se crea conciliacion
	SET @conciliacionId = (SELECT IsNull(Max(CONCILIACION_ID)+1, 1)
						FROM CONCILIACION_MOVIMIENTOS_DOCUMENTOS)

	INSERT INTO CONCILIACION_MOVIMIENTOS_DOCUMENTOS
        (CONCILIACION_ID,MOVIMIENTO_ID,NUM_COMPROBANTE,CUSTODIA_ID,PCLID,CTCID,GESTORID,CONCILIACION_TIPO_ID,USRID_REGISTRO)
		VALUES
		(@conciliacionId, @movimientoId,@numComprobante,@custodiaId, @pclid, @ctcid, @gestorId,@tipoConciliacionId,@userId)
	
	--Actualizar Movimientos CARTOLA_MOVIMIENTOS
	update CARTOLA_MOVIMIENTOS
	set ESTATUS_ID = 2 --Conciliado
	where MOVIMIENTO_ID = @movimientoId

	--Ingresar al Historial
	exec dbo._Insertar_Cartola_Movimientos_Historial @codemp, @movimientoId, @num_cuenta, @userId

	select @conciliacionId ConciliacionId
END