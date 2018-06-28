CREATE Procedure [dbo].[_Insertar_Rol_Actualiza_PoderJudicial](
			@codemp int,
			@rolid int,
			@actualizarPoderJudicial varchar(1)
)
AS   
declare @id int  

set @id = (select COUNT(*) from [ROL_ACTUALIZA_PODERJUDICIAL] where [CODEMP] = @codemp and [ROLID] = @rolid)    

if @id = 0 
begin

	INSERT INTO [dbo].[ROL_ACTUALIZA_PODERJUDICIAL]
			   ([CODEMP]
			   ,[ROLID]
			   ,[FLAG_PODERJUDICIAL])
		 VALUES
			   (@codemp
			   ,@rolid
			   ,@actualizarPoderJudicial)

end
else
begin
	UPDATE ROL_ACTUALIZA_PODERJUDICIAL
	SET FLAG_PODERJUDICIAL = @actualizarPoderJudicial
	WHERE CODEMP = @codemp
	AND ROLID = @rolid
end
