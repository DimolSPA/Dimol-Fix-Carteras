CREATE PROCEDURE _Exist_NumCuenta(
@numCuenta varchar(60)
)
AS
BEGIN
	
	--Verificar si el numero de cuenta existe registrado en el sistema
	declare @existCuenta int = 0;
	set @existCuenta= (select count(NUM_CUENTA) 
							from TESORERIA_CUENTAS_BANCARIAS 
							where NUM_CUENTA = @numCuenta)

	select @existCuenta countCuenta
	
END
