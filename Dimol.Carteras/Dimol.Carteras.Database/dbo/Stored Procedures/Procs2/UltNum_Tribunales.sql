

Create Procedure UltNum_Tribunales(@trb_codemp integer) as
select IsNull(Max(trb_trbid)+1, 1)
from tribunales
where trb_codemp = @trb_codemp
