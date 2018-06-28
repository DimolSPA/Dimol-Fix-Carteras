
create procedure [dbo].[_Trae_Ruta_Logo_PJ](@usuario varchar(30)) as 
	select ruta 
	from USUARIOS_PJ u, USUARIOS_PJ_RUTAS ur
	where u.PCLID = ur.PCLID
	and u.LOGIN = @usuario
