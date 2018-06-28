

create Procedure Find_Insumos(@ins_codemp integer, @ins_insid numeric(15)) as
select count(ins_insid)
from insumos
where ins_codemp = @ins_codemp and
           ins_insid =@ins_insid
