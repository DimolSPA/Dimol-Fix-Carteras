

Create Procedure UltNum_Tipos_Insumo(@tpi_codemp integer) as
select IsNull(Max(tpi_tipid)+1, 1)
from tipos_insumo
where tpi_codemp  = @tpi_codemp
