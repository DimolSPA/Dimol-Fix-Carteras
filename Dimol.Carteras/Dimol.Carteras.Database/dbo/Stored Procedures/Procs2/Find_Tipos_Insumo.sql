

Create Procedure Find_Tipos_Insumo(@tpi_codemp integer, @tpi_tipid integer) as
select count(tpi_tipid)
from tipos_insumo
where tpi_codemp  = @tpi_codemp and
          tpi_tipid =  @tpi_tipid
