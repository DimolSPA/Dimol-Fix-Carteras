

Create procedure Find_Clausulas_ContCart(@clc_codemp integer, @clc_clcid integer) as
select count(clc_clcid)
from clausulas_contcart
where clc_codemp = @clc_codemp and
           clc_clcid = @clc_clcid
