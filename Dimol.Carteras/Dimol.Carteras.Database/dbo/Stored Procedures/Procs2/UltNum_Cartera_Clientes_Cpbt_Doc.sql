

Create Procedure UltNum_Cartera_Clientes_Cpbt_Doc(@ccb_codemp integer, @ccb_pclid integer, @ccb_ctcid integer) as
select IsNull(Max(ccb_ccbid)+1, 1)
from cartera_clientes_cpbt_doc
where ccb_codemp = @ccb_codemp and
           ccb_pclid = @ccb_pclid and
           ccb_ctcid = @ccb_ctcid
