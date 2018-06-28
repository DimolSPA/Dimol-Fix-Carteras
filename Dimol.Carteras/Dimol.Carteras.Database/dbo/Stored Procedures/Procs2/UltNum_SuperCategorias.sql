

Create Procedure UltNum_SuperCategorias(@spc_codemp integer) as
select IsNull(Max(spc_spcid)+1, 1)
from supercategorias
where spc_codemp = @spc_codemp
