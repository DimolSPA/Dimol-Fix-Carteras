

Create Procedure Find_Cartera_Clientes_Cpbt_Doc_Numero(@ccb_codemp integer, @ccb_pclid integer, @ccb_ctcid integer, @ccb_tpcid  integer, @ccb_numero varchar(30)) as
select count(ccb_ccbid)
from cartera_clientes_cpbt_doc
where ccb_codemp = @ccb_codemp and
           ccb_pclid = @ccb_pclid and
           ccb_ctcid = @ccb_ctcid and
           ccb_tpcid = @ccb_tpcid and
           ccb_numero =  @ccb_numero and
           ccb_estcpbt <> 'X'
