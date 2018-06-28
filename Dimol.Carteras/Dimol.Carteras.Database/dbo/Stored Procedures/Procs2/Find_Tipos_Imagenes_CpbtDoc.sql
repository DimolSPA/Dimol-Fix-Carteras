

Create Procedure Find_Tipos_Imagenes_CpbtDoc(@tpc_codemp integer, @tpc_tpcid integer) as
  select count(tpc_tpcid)
from Tipos_Imagenes_CpbtDoc
where tpc_codemp = @tpc_codemp and
           tpc_tpcid = @tpc_tpcid
