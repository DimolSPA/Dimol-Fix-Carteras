CREATE procedure [dbo].[_Trae_Bancos_Enviar_Email](@codemp int) as 
select IDBANCO, NOMBRE 
from provcli_banco 
WHERE CODEMP = @codemp 
order by POR_DEFECTO desc
