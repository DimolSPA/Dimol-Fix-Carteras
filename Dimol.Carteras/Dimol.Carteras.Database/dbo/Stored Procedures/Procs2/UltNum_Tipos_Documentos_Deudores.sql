

Create Procedure UltNum_Tipos_Documentos_Deudores(@tdd_codemp integer) as
select IsNull(Max(tdd_tddid)+1, 1)
from Tipos_Documentos_Deudores
where tdd_codemp = @tdd_codemp
