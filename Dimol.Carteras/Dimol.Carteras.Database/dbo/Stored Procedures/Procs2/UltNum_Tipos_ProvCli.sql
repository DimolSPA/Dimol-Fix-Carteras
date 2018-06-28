

Create Procedure UltNum_Tipos_ProvCli(@tpc_codemp integer) as
select IsNull(Max(tpc_tpcid)+1, 1)  
from tipos_provcli
where tpc_codemp = @tpc_codemp
