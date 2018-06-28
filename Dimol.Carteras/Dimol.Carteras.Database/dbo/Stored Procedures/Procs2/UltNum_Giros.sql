

Create Procedure UltNum_Giros(@gir_codemp integer) as
select IsNull(Max(gir_girid)+1, 1)
from giros
where gir_codemp = @gir_codemp
