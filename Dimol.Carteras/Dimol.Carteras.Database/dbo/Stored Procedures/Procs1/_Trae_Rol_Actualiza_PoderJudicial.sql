CREATE PROCEDURE [dbo].[_Trae_Rol_Actualiza_PoderJudicial](
			@codemp int,
			@rolid int
)
AS   

BEGIN
declare @flag varchar(1)  

set @flag = (select FLAG_PODERJUDICIAL from [ROL_ACTUALIZA_PODERJUDICIAL] where [CODEMP] = @codemp and [ROLID] = @rolid) 
select  @flag actualizaPoderJudicial
END
