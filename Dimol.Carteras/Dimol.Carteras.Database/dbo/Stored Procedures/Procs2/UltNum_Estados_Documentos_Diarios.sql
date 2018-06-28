

Create Procedure UltNum_Estados_Documentos_Diarios(@edc_codemp integer) as
select IsNull(Max(edc_edcid)+1, 1)
from estados_documentos_diarios
where edc_codemp = @edc_codemp
