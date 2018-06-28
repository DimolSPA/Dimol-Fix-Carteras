

Create Procedure Find_Cartera_Clientes_Cpbt_Doc(@ccb_codemp integer, @ccb_pclid integer, @ccb_ctcid integer, @ccb_ccbid  integer) as
select count(ccb_ccbid)
from cartera_clientes_cpbt_doc
where ccb_codemp = @ccb_codemp and
           ccb_pclid = @ccb_pclid and
           ccb_ctcid = @ccb_ctcid and
           ccb_ccbid = @ccb_ccbid
