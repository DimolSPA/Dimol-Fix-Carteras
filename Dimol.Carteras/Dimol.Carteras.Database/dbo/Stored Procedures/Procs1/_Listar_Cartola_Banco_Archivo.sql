CREATE PROCEDURE [dbo].[_Listar_Cartola_Banco_Archivo](
@idCarga int)
AS
BEGIN
	select 
		cartola.EXCEL_ROW_ID id,
		cartola.NUM_CUENTA NumCuenta,
		cartola.FEC_MOVIMIENTO FecMovimiento,
		cartola.MONTO Monto,
		cartola.DESCRIPCION Motivo,
		cartola.SUCURSAL,
		cartola.NUM_COMPROBANTE_REF NumComprobante
		
	from CARTOLA_BANCO_EXCEL cartola
	where cartola.IDCARGA = @idCarga
	and PROCESADO = 'N'
END
