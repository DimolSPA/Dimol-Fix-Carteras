

Create Procedure Find_Tipos_Descripcion_Producto(@tdp_codemp integer, @tdp_tpdid integer) as
select count(tdp_tpdid)
from tipos_descripcion_producto
where tdp_codemp = @tdp_codemp and
           tdp_tpdid = @tdp_tpdid
