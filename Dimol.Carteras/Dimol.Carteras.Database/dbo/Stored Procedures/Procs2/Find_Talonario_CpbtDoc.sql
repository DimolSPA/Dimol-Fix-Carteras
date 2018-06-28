

Create Procedure Find_Talonario_CpbtDoc(@tac_codemp integer, @tac_tacid integer) as

select count(tac_tacid)
from talonario_cpbtdoc
where tac_codemp = @tac_codemp and
          tac_tacid = @tac_tacid
