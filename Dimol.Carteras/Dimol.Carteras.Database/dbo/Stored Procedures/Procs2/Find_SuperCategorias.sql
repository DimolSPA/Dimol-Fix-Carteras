

Create Procedure Find_SuperCategorias(@spc_codemp integer, @spc_spcid integer) as
select count(spc_spcid)
from supercategorias
where spc_codemp = @spc_codemp and
           spc_spcid = @spc_spcid
