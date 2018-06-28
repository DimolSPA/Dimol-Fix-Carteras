
create procedure [dbo].[_Listar_Cuenta_Tipo_Banco] (@tipobanco int, @pclid int) as
 select C.ID_CUENTA_EJECUTIVO, C.CUENTA 
 FROM EJECUTIVO_CUENTA_MUTUAL C with (nolock), PROVCLI_EJECUTIVO P with (nolock) 
 WHERE P.ID_EJECUTIVO = C.ID_EJECUTIVO 
 AND C.ID_TIPO_BANCO = @tipobanco 
 AND P.PCLID = @pclid
