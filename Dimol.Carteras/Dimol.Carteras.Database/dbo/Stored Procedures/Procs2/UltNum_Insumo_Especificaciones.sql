

Create Procedure UltNum_Insumo_Especificaciones(@ise_codemp integer, @ise_insid integer) as
select IsNull(Max(ise_iseid)+1, 1) 
from insumo_especificaciones
where ise_codemp = @ise_codemp and
           ise_insid = @ise_insid
