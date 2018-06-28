

Create Procedure Find_Tipos_Tribunal(@ttb_codemp integer, @ttb_ttbid integer) as
select count(ttb_ttbid)
from tipos_tribunal
where ttb_codemp = @ttb_codemp and
           ttb_ttbid = @ttb_ttbid
