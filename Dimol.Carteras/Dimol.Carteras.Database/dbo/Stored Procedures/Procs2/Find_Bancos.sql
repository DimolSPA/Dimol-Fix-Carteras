

Create Procedure Find_Bancos(@bco_codemp integer, @bco_bcoid integer) as
select count(bco_bcoid)
from bancos
where bco_codemp = @bco_codemp and
           bco_bcoid = @bco_bcoid
