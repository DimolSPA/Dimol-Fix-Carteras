CREATE PROCEDURE [dbo].[_Insertar_Rol_Actualiza_PoderJudicial_Historial](
			@codemp int,
			@rolid int,
			@actualizarPoderJudicial varchar(1),
			@userId int
)
AS   

BEGIN
declare @flag varchar(1)  

set @flag = (select top 1 FLAG_PODERJUDICIAL from [ROL_ACTUALIZA_PODERJUDICIAL_HISTORIAL] where [CODEMP] = @codemp and [ROLID] = @rolid
										order by [FEC_REGISTRO] desc) 
if @flag != @actualizarPoderJudicial or @flag is null
	begin
		INSERT INTO [dbo].[ROL_ACTUALIZA_PODERJUDICIAL_HISTORIAL]
				   ([CODEMP]
				   ,[ROLID]
				   ,[FLAG_PODERJUDICIAL]
				   ,[USERID]
				   ,[FEC_REGISTRO])
			 VALUES
				   (@codemp
				   ,@rolid
				   ,@actualizarPoderJudicial
				   ,@userId
				   ,GETDATE())
	end
END
