

Create Procedure Find_Giros(@gir_codemp integer, @gir_girid integer) as
select count(gir_girid)
from giros
where gir_codemp = @gir_codemp and
           gir_girid = @gir_girid
