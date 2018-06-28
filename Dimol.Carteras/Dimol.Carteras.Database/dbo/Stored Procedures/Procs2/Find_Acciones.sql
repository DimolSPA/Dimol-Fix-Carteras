

Create Procedure Find_Acciones(@acc_codemp integer, @acc_accid integer) as
select count(acc_accid)
from acciones
where acc_codemp = @acc_codemp and
           acc_accid = @acc_accid
