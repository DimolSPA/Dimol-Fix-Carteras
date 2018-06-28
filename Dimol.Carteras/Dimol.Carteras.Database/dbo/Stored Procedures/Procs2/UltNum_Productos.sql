

Create Procedure UltNum_Productos(@pdt_codemp integer) as
select IsNull(Max(pdt_prodid)+1, 1)
from productos
where pdt_codemp = @pdt_codemp
