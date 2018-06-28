

Create Procedure Find_Tipos_CpbtDoc(@tpc_codemp integer, @tpc_tpcid integer) as
select count(tpc_tpcid)
from tipos_cpbtdoc
where tpc_codemp = @tpc_codemp and tpc_tpcid = @tpc_tpcid
