

Create Procedure UltNum_Clasificacion_CpbtDoc(@clb_codemp integer) as
select IsNull(Max(clb_clbid)+1, 1)  
from clasificacion_cpbtdoc
where clb_codemp = @clb_codemp
