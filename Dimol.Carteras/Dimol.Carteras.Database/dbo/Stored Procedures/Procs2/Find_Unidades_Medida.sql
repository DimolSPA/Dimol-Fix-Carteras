

Create Procedure Find_Unidades_Medida(@unm_unmid integer) as
select count(unm_unmid)
from unidades_medida
where unm_unmid = @unm_unmid
