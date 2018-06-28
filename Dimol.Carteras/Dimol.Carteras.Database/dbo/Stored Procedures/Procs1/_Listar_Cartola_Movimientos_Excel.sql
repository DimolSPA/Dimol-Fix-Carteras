CREATE PROCEDURE _Listar_Cartola_Movimientos_Excel
(
@codemp int,
@numCuenta varchar(60)
)
AS
BEGIN
	SET NOCOUNT ON;
	select 
		ct.NUM_CUENTA NumCuenta,
		ct.FEC_MOVIMIENTO FecMovimiento,
		ct.MONTO Monto,
		cartola.DESCRIPCION Motivo,
		ct.SUCURSAL,
		ct.NUM_COMPROBANTE_REF NumComprobante,
		tm.DESCRIPCION Movimiento,
		mot.DESCRIPCION MotivoSistema,
		est.DESCRIPCION Estado,
		ct.OBSERVACION
	from CARTOLA_MOVIMIENTOS ct
	join TESORERIA_TIPO_MOVIMIENTO_BANCO tm
	on ct.TIPO_MOVIMIENTO_BANCO_ID = tm.TIPO_MOVIMIENTO_BANCO_ID
	join TESORERIA_TIPO_MOTIVO_BANCO mot
	on ct.TIPO_MOTIVO_BANCO_ID = mot.TIPO_MOTIVO_BANCO_ID
	join TESORERIA_TIPO_ESTADO_BANCO est
	on ct.TIPO_ESTADO_BANCO_ID = est.TIPO_ESTADO_BANCO_ID
	join CARTOLA_MOVIMIENTOS_TIPO_ESTATUS sts
	on ct.ESTATUS_ID = sts.ESTATUS_ID
	join CARTOLA_BANCO_EXCEL cartola
	on ct.EXCEL_ROW_ID = cartola.EXCEL_ROW_ID
	where ct.CODEMP = @codemp
	and ct.NUM_CUENTA = @numCuenta
	and ct.ESTATUS_ID = 1
	and ct.TIPO_ESTADO_BANCO_ID != 3
	
END