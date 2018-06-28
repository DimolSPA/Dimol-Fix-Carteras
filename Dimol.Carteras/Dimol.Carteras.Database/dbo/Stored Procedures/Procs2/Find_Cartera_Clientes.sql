

Create Procedure Find_Cartera_Clientes(@ctc_codemp integer, @ctc_pclid integer, @ctc_ctcid integer) as
select count(ctc_ctcid)
from cartera_clientes
where ctc_codemp = @ctc_codemp and
           ctc_pclid = @ctc_pclid and
           ctc_ctcid = @ctc_ctcid
