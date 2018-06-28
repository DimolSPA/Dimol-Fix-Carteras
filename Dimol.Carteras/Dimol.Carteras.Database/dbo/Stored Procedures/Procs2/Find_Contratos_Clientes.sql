

Create Procedure Find_Contratos_Clientes(@ctc_codemp integer, @ctc_cctid  integer, @ctc_pclid integer) as
select count(ctc_cctid)
from contratos_clientes
where ctc_codemp  = @ctc_codemp and
           ctc_cctid = @ctc_cctid and
           ctc_pclid = @ctc_pclid
