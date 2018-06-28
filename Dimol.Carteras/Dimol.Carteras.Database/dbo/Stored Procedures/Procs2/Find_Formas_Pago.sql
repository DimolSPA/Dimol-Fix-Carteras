

Create Procedure Find_Formas_Pago(@frp_codemp integer, @frp_frpid integer) as
select Count(frp_frpid)
from formas_pago
where frp_codemp = @frp_codemp and
           frp_frpid = @frp_frpid
