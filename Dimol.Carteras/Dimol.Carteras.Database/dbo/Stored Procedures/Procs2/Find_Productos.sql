

Create Procedure Find_Productos(@pdt_codemp integer, @pdt_prodid numeric(15)) as
select count(pdt_prodid)
from productos
where pdt_codemp = @pdt_codemp and
           pdt_prodid = @pdt_prodid
