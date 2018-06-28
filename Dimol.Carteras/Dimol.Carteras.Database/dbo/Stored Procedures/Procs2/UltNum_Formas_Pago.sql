

Create Procedure UltNum_Formas_Pago(@frp_codemp integer) as
select IsNull(Max(frp_frpid)+1, 1)
from formas_pago
where frp_codemp = @frp_codemp
