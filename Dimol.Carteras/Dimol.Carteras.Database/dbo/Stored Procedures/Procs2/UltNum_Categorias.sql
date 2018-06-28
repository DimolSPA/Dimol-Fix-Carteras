

Create Procedure UltNum_Categorias(@cat_codemp integer) as
select IsNull(Max(cat_catid)+1, 1)
from categorias
where cat_codemp = @cat_codemp
