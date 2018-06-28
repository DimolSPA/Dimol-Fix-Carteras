

create Procedure UltNum_Insumos(@ins_codemp integer) as
select IsNull(Max(ins_insid)+1, 1)
from insumos
where ins_codemp = @ins_codemp
