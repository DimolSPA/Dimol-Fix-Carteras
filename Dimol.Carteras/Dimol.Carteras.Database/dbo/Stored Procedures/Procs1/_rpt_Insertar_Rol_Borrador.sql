CREATE Procedure [dbo].[_rpt_Insertar_Rol_Borrador](
			@codemp int,
			@id_borrador int,
			@rolid int,
			@html text,
			@usrid integer
)

 as   
 
 declare 			@id_version int
 
 select top 1 @id_version =ID_VERSION from BORRADOR_VERSION where CODEMP = @codemp and ID_BORRADOR = @id_borrador order by ID_VERSION desc
  
INSERT INTO [ROL BORRADOR]
           ([CODEMP]
           ,[ID_BORRADOR]
           ,[ID_VERSION]
           ,[ROLID]
           ,[HTML]
           ,[FECHA_CREACION]
           ,[USER_CREACION])
     VALUES
           (@codemp
           ,@id_borrador
           ,@id_version
           ,@rolid
           ,@html
           ,GETDATE()
           ,@usrid)
