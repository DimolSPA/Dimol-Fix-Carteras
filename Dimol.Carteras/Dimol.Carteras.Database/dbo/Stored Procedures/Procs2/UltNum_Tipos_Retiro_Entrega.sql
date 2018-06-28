

Create Procedure UltNum_Tipos_Retiro_Entrega(@tre_codemp integer) as
select IsNull(Max(tre_treid)+1, 1)
from tipos_retiro_entrega
where tre_codemp = tre_codemp
