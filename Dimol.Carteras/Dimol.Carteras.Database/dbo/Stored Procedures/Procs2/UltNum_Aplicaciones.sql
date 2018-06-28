

Create Procedure UltNum_Aplicaciones(@apl_codemp integer, @apl_anio smallint) as
select IsNull(Max(apl_numapl)+1, 1)
from aplicaciones
where apl_codemp = @apl_codemp and
           apl_anio = @apl_anio
