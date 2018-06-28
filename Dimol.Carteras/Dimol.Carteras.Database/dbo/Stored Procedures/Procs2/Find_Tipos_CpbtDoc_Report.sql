

Create Procedure Find_Tipos_CpbtDoc_Report(@trc_codemp integer, @trc_tpcid integer, @trc_trcid integer) as
select count(trc_trcid) 
from Tipos_CpbtDoc_Report
where trc_codemp = @trc_codemp and
           trc_tpcid = @trc_tpcid and
           trc_trcid = @trc_trcid
