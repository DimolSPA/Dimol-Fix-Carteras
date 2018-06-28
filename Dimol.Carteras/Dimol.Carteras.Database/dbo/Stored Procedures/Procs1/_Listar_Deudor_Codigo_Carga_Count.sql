create procedure [dbo].[_Listar_Deudor_Codigo_Carga_Count] (@codemp int, @pclid int, @ctcid int, @estcpbt varchar(1), @codcarga int) as
select count(CCB_CCBID) CUENTA
from CARTERA_CLIENTES_CPBT_DOC with (nolock)
where CCB_CTCID = @ctcid and 
CCB_CODID = @codcarga and 
CCB_ESTCPBT = @estcpbt and 
CCB_PCLID = @pclid and 
CCB_CODEMP = @codemp
