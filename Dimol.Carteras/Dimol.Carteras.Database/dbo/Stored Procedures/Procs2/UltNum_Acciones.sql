

Create Procedure UltNum_Acciones(@acc_codemp integer) as
select IsNull(Max(acc_accid)+1, 1)
from acciones
where acc_codemp = @acc_codemp
