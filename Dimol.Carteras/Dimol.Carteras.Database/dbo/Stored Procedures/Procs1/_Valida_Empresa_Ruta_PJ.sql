

CREATE procedure [dbo].[_Valida_Empresa_Ruta_PJ] (@pclid int) as
	select count(PCLID) PCLID
	from USUARIOS_PJ_RUTAS
	where PCLID = @pclid

