
CREATE procedure [dbo].[_Trae_Cartera_Documento_Cpbt_Json] 
(@ccb_pclid int, 
@ccb_ctcid int,
@ccb_ccbid int) as 

select ccb_numero, 
case ccb_codmon when 1 then 'CLP' when 3 then 'DOLAR' end as ccb_codmon, 
ccb_saldo, 
(select top 1 pcc_codigo from PROVCLI_CODIGO_CARGA where pcc_codid = ccb_codid and pcc_pclid = @ccb_pclid) ccb_codid   
from CARTERA_CLIENTES_CPBT_DOC 
where ccb_pclid = @ccb_pclid 
and ccb_ctcid = @ccb_ctcid
and ccb_ccbid = @ccb_ccbid 
