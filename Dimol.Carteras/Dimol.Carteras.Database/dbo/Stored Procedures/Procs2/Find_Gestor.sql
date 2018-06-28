

Create Procedure Find_Gestor(@ges_codemp integer, @ges_sucid integer, @ges_gesid integer) as
select count(ges_gesid)
from gestor
where ges_codemp  = @ges_codemp and 
           ges_sucid = @ges_sucid and
           ges_gesid = @ges_gesid
