

Create Procedure Find_Materia_Estados(@mej_codemp integer, @mej_esjid integer, @mej_estid integer) as
select count(mej_codemp)
from materia_estados
where mej_codemp = @mej_codemp and
           mej_esjid = @mej_esjid and
           mej_estid = @mej_estid
