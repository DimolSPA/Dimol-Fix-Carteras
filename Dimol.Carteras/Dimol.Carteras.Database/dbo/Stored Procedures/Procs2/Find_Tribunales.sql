

Create Procedure Find_Tribunales(@trb_codemp integer, @trb_trbid integer) as
select count(trb_trbid)
from tribunales
where trb_codemp = @trb_codemp and
           trb_trbid = @trb_trbid
