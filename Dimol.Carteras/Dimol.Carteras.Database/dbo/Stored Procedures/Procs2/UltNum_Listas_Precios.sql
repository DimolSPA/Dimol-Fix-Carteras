

Create Procedure UltNum_Listas_Precios(@ltp_codemp integer) as
select IsNull(Max(ltp_ltpid)+1, 1) 
from listas_precios
where ltp_codemp = @ltp_codemp
