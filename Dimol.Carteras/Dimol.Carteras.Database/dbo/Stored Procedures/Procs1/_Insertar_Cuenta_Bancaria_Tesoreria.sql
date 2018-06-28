
CREATE PROCEDURE _Insertar_Cuenta_Bancaria_Tesoreria(
@codemp int,
@numCuenta varchar(60),
@bancoId int,
@tipoCuentaId int,
@userId int
)
AS
BEGIN
declare @cuentaId int = 0
	--Se crea Cuenta
	SET @cuentaId = (SELECT IsNull(Max(CUENTA_ID)+1, 1)
						FROM TESORERIA_CUENTAS_BANCARIAS)
	
	INSERT INTO TESORERIA_CUENTAS_BANCARIAS
        (CUENTA_ID, NUM_CUENTA,CODEMP,BANCO_ID,TIPO_CUENTA_ID,USRID_REGISTRO)
		VALUES
		(@cuentaId,@numCuenta, @codemp,@bancoId,@tipoCuentaId,@userId)
	
	select @cuentaId cuentaId
	
END
