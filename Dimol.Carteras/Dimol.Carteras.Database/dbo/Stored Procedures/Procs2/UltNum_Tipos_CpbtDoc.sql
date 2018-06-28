

Create Procedure UltNum_Tipos_CpbtDoc(@tpc_codemp integer) as
select IsNull(Max(tpc_tpcid)+1, 1) 
from tipos_cpbtdoc
where tpc_codemp = @tpc_codemp
