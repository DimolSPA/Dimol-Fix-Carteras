create procedure [dbo].[_Trae_Tipo_Banco_Mutual] (@pclid int) as
 select B.ID_TIPO_BANCO, B.NOMBRE 
 from TIPO_BANCO B with (nolock), EJECUTIVO_CUENTA_MUTUAL C with (nolock), PROVCLI_EJECUTIVO P with (nolock)
 where C.ID_TIPO_BANCO = B.ID_TIPO_BANCO
 AND C.ID_EJECUTIVO = P.ID_EJECUTIVO 
 AND P.PCLID = @pclid
 group by B.ID_TIPO_BANCO, B.NOMBRE
 order by B.ID_TIPO_BANCO
