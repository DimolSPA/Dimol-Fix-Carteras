

Create Procedure UltNum_ProvCli_Codigo_Carga(@pcc_codemp integer, @pcc_pclid integer) as
select IsNull(Max(pcc_codid)+1, 1)
from provcli_codigo_carga
where pcc_codemp = @pcc_codemp  and
           pcc_pclid = @pcc_pclid
