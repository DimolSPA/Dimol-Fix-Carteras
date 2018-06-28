CREATE PROCEDURE _Insertar_Cartola_Banco_Archivo(
@numCuenta varchar(60),
@fecMovimiento datetime,
@monto decimal(15,2),
@descripcion varchar(200),
@sucursal varchar(100),
@numComprobante varchar(100),
@idcarga int,
@userId int)
AS
BEGIN
	declare @excelId int = 0
	--Se agrega al historial
	SET @excelId = (SELECT IsNull(Max(EXCEL_ROW_ID)+1, 1)
						FROM CARTOLA_BANCO_EXCEL)
	INSERT INTO CARTOLA_BANCO_EXCEL
        (EXCEL_ROW_ID,NUM_CUENTA,FEC_MOVIMIENTO,MONTO,DESCRIPCION,SUCURSAL,NUM_COMPROBANTE_REF,IDCARGA,USRID_REGISTRO)
		VALUES
		(@excelId, @numCuenta,@fecMovimiento,@monto, UPPER(@descripcion), UPPER(@sucursal),@numComprobante,@idcarga,@userId)
END

