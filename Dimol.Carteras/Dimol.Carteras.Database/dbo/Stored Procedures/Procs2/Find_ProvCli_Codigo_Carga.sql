

Create Procedure Find_ProvCli_Codigo_Carga(@pcc_codemp integer, @pcc_pclid integer, @pcc_codid integer) as
select count(pcc_codid)
from provcli_codigo_carga
where pcc_codemp = @pcc_codemp  and
           pcc_pclid = @pcc_pclid and
           pcc_codid = @pcc_codid
