

Create Procedure Find_Subcarteras(@sbc_codemp integer, @sbc_sbcid integer) as
select count(sbc_sbcid)
from subcarteras
where sbc_codemp = @sbc_codemp and
           sbc_sbcid = @sbc_sbcid
