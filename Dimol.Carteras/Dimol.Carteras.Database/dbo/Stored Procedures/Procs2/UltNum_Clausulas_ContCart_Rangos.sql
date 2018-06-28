

Create Procedure UltNum_Clausulas_ContCart_Rangos(@clr_codemp integer, @clr_clcid integer) as
select IsNull(Max(clr_clrid)+1, 1)
from clausulas_contcart_rangos
where clr_codemp = @clr_codemp and
           clr_clcid = @clr_clcid
