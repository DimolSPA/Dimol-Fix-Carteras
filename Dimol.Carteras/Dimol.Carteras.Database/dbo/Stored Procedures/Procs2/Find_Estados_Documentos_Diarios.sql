

Create Procedure Find_Estados_Documentos_Diarios(@edc_codemp integer, @edc_edcid integer) as
select count(edc_edcid)
from estados_documentos_Diarios
where edc_codemp = @edc_codemp and
           edc_edcid = @edc_edcid
