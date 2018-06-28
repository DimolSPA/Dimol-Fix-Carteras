

Create Procedure Find_Maxima_Convencional(@mxc_codemp integer, @mxc_mxcid integer) as
select count(mxc_mxcid)
from maxima_convencional
where mxc_codemp = @mxc_codemp and
           mxc_mxcid = @mxc_mxcid
