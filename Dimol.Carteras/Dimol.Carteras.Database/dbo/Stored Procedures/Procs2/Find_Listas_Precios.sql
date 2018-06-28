

Create Procedure Find_Listas_Precios(@ltp_codemp integer, @ltp_ltpid integer) as
select count(ltp_ltpid)
from listas_precios
where ltp_codemp = @ltp_codemp and
           ltp_ltpid = @ltp_ltpid
