

Create Procedure UltNum_Subcarteras(@sbc_codemp integer) as
select IsNull(Max(sbc_sbcid)+1, 1)
from subcarteras
where sbc_codemp = @sbc_codemp
