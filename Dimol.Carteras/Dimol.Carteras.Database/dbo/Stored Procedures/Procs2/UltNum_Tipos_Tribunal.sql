

Create Procedure UltNum_Tipos_Tribunal(@ttb_codemp integer) as
select IsNull(Max(ttb_ttbid)+1, 1) 
from tipos_tribunal
where ttb_codemp = @ttb_codemp
