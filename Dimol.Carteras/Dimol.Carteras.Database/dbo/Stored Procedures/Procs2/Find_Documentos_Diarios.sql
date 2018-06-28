

Create Procedure Find_Documentos_Diarios(@ddi_codemp integer, @ddi_anio smallint, @ddi_numdoc integer) as
select count(ddi_numdoc) 
from documentos_diarios
where ddi_codemp = @ddi_codemp and
           ddi_anio = @ddi_anio and
           ddi_numdoc = @ddi_numdoc
