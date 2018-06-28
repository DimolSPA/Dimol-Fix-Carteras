CREATE procedure [dbo].[_Trae_Cabecera_Comprobante_Estado] (@codemp int, @tpcid int, @numero int) AS 
select top 1 CBE_ESTADO 
from CABACERA_COMPROBANTES_ESTADOS
where CBE_CODEMP = @codemp and 
CBE_TPCID = @tpcid and 
CBE_NUMERO = @numero 
order by CBE_FECHA desc
