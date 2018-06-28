CREATE PROCEDURE [dbo].[_Listar_Panel_Demanda_Usuarios] (@texto varchar(200),@codemp int,@sucursal int)
AS
BEGIN
	
	SET NOCOUNT ON;
declare @nombre varchar(250) = '%' + @texto + '%'

    select u.USR_NOMBRE NOMBRE, u.USR_USRID  USRID
	from USUARIOS u with(nolock)
	where u.USR_CODEMP = @codemp 
	and u.USR_SUCID = @sucursal
	and  u.USR_NOMBRE like @nombre
   and u.USR_ESTADO = 'H'
END
