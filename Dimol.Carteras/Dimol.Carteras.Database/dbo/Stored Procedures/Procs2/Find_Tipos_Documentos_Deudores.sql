

Create Procedure Find_Tipos_Documentos_Deudores(@tdd_codemp integer, @tdd_tddid integer) as
select count(tdd_tddid)
from Tipos_Documentos_Deudores
where tdd_codemp = @tdd_codemp and
           tdd_tddid = @tdd_tddid
