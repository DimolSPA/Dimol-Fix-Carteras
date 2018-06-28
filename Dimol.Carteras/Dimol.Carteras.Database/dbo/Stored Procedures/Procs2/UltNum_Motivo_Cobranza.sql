

Create Procedure UltNum_Motivo_Cobranza(@mtc_codemp integer) as 
select IsNull(Max(mtc_mtcid)+1, 1)
from motivo_cobranza
where mtc_codemp = @mtc_codemp
