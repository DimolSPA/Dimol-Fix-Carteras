

Create Procedure Find_Empresa_Bancos(@esb_codemp integer, @esb_bcoid integer, @esb_sucid integer, @esb_ctacte varchar(20)) as
select count(esb_ctacte)
from empresa_bancos
where esb_codemp = @esb_codemp and
           esb_bcoid = @esb_bcoid and
           esb_sucid = @esb_sucid and
           esb_ctacte = @esb_ctacte
