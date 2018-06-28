

Create Procedure Find_Deudores_Mail(@ddm_codemp integer, @ddm_ctcid integer, @ddm_mail varchar(80)) as
select count(ddm_mail)
from deudores_mail
where ddm_codemp = @ddm_codemp and
           ddm_ctcid = @ddm_ctcid and
           ddm_mail = @ddm_mail
