

Create Procedure UltNum_Bancos(@bco_codemp integer) as
select IsNull(Max(bco_bcoid)+1, 1)
from bancos
where bco_codemp = @bco_codemp
