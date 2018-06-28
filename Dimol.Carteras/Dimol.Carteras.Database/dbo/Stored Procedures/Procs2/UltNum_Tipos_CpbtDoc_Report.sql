

Create Procedure UltNum_Tipos_CpbtDoc_Report(@trc_codemp integer, @trc_tpcid integer) as
select IsNull(Max(trc_trcid)+1, 1) 
from Tipos_CpbtDoc_Report
where trc_codemp = @trc_codemp and
           trc_tpcid = @trc_tpcid
