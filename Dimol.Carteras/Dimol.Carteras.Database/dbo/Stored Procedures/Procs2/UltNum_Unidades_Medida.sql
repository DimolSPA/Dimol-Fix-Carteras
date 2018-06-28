

Create Procedure UltNum_Unidades_Medida as
select IsNull(Max(unm_unmid)+1, 1)
from unidades_medida
