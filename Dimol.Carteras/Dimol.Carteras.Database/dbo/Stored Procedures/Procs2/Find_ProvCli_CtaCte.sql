

Create Procedure Find_ProvCli_CtaCte(@pct_codemp integer, @pct_tpcid integer, @pct_pclid integer) as
select count(pct_pclid)
from provcli_ctacte
where pct_codemp = @pct_codemp and
           pct_tpcid = @pct_tpcid and
           pct_pclid = @pct_pclid
