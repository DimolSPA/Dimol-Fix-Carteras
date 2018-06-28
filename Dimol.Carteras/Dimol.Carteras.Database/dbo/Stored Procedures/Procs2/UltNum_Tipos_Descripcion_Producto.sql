

Create Procedure UltNum_Tipos_Descripcion_Producto(@tdp_codemp integer) as
select IsNull(Max(tdp_tpdid)+1, 1)
from tipos_descripcion_producto
where tdp_codemp = @tdp_codemp
