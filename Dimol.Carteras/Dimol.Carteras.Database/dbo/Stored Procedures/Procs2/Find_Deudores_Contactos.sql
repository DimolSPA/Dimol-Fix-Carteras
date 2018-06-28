

Create Procedure Find_Deudores_Contactos(@ddc_codemp integer, @ddc_ctcid integer, @ddc_ddcid integer) as
select count(ddc_ddcid)
from deudores_contactos
where ddc_codemp = @ddc_codemp and
           ddc_ctcid = @ddc_ctcid and
           ddc_ddcid = @ddc_ddcid
