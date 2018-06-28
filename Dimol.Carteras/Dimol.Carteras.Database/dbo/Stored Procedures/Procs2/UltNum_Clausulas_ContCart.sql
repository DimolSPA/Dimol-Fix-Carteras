

Create procedure UltNum_Clausulas_ContCart(@clc_codemp integer) as
select IsNull(Max(clc_clcid)+1, 1) 
from clausulas_contcart
where clc_codemp = @clc_codemp
