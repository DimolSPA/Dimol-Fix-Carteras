

Create Procedure UltNum_Documentos_Diarios(@ddi_codemp integer, @ddi_anio smallint) as
select IsNull(Max(ddi_numdoc)+1, 1)
from documentos_diarios
where ddi_codemp = @ddi_codemp and
           ddi_anio = @ddi_anio
