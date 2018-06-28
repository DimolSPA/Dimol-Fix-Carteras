CREATE Procedure [dbo].[_Insertar_Rol_Poder_Judicial](
			@codemp int,
			@rolid int,
			@tipo varchar(1),
			@id_causa integer,
			@tribunal int
)

 as   
declare @id int  

set @id = (select COUNT(*) from [ROL_PODER_JUDICIAL] where [RPJ_CODEMP] = @codemp and [RPJ_ROLID] = @rolid)    

if @id = 0 
begin        
 
INSERT INTO [ROL_PODER_JUDICIAL]
     VALUES
           (@codemp
           ,@rolid
           ,@tipo
           ,@id_causa
           ,GETDATE()
           ,@tribunal)
           
end
--else
--begin
--UPDATE [ROL_PODER_JUDICIAL]
--   SET [RPJ_ID_CAUSA] = @id_causa
--      ,[RPJ_FECH_ULT_ACT] = GETDATE()
-- WHERE [RPJ_CODEMP] = @codemp and [RPJ_ROLID] = @rolid
--end