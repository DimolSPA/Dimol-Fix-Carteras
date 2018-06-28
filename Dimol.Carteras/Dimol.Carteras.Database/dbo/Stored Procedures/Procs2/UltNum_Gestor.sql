

Create Procedure UltNum_Gestor(@ges_codemp integer, @ges_sucid integer) as
select IsNull(Max(ges_gesid)+1, 1)
from gestor
where ges_codemp  = @ges_codemp and 
           ges_sucid = @ges_sucid
