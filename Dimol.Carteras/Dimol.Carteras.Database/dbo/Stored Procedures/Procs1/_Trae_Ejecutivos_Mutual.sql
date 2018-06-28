CREATE procedure [dbo].[_Trae_Ejecutivos_Mutual] (@pclid int) as 
 SELECT E.ID_EJECUTIVO, E.NOMBRE  
 from EJECUTIVO_MUTUAL E with (nolock), PROVCLI_EJECUTIVO P with (nolock) 
 where E.ID_EJECUTIVO = P.ID_EJECUTIVO
 AND P.PCLID = @pclid
 order by E.ID_EJECUTIVO
