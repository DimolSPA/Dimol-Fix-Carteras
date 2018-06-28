

Create Procedure UltNum_Talonario_CpbtDoc(@tac_codemp integer) as
select IsNull(Max(tac_tacid)+1, 1) 
from talonario_cpbtdoc
where tac_codemp = @tac_codemp
