

Create Procedure Find_Tipos_Retiro_Entrega(@tre_codemp integer, @tre_treid integer) as
select count(tre_treid)
from tipos_retiro_entrega
where tre_codemp = tre_codemp and
           tre_treid = tre_treid
