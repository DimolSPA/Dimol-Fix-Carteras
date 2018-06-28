

create Procedure UltNum_Tipos_Causa(@tca_codemp integer) as
select IsNull(Max(tca_tcaid)+1, 1)
from tipos_causa
where tca_codemp = @tca_codemp
