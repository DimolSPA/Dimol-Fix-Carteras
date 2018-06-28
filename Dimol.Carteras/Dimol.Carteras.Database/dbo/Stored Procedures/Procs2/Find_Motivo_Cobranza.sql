

Create Procedure Find_Motivo_Cobranza(@mtc_codemp integer, @mtc_mtcid integer) as 
select count(mtc_mtcid)
from motivo_cobranza
where mtc_codemp = @mtc_codemp and
           mtc_mtcid = @mtc_mtcid
