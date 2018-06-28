

Create Procedure UltNum_Tipos_Imagenes_CpbtDoc(@tpc_codemp integer) as
  select IsNull(Max(tpc_tpcid)+1, 1)
from Tipos_Imagenes_CpbtDoc
where tpc_codemp = @tpc_codemp
