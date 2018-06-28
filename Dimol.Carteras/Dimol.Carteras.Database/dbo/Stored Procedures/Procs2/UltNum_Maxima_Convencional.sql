

Create Procedure UltNum_Maxima_Convencional(@mxc_codemp integer) as
select IsNull(Max(mxc_mxcid)+1, 1) 
from maxima_convencional
where mxc_codemp = @mxc_codemp
