

Create Procedure UltNum_Deudores_Contactos(@ddc_codemp integer, @ddc_ctcid integer) as
select IsNull(Max(ddc_ddcid)+1, 1)
from deudores_contactos
where ddc_codemp = @ddc_codemp and
           ddc_ctcid = @ddc_ctcid
