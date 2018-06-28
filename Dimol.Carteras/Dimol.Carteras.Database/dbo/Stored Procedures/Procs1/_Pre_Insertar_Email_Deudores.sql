create Procedure [dbo].[_Pre_Insertar_Email_Deudores](@codemp integer, @ctcid numeric (15), @email varchar(250), @fecha_envio datetime, @usuario int) as 

declare @existe int = 0

select @existe = count(EMAIL)
FROM [PRE_EMAIL_DEUDORES]
  where [CODEMP] = @codemp
  and [CTCID] = @ctcid
  and EMAIL = @email

if(@existe = 0 )
begin  
	INSERT INTO [PRE_EMAIL_DEUDORES]
           ([CODEMP]
           ,[CTCID]
           ,EMAIL
           ,[FECHA_CREACION]
           ,[USUARIO_CREACION]
           ,[FECHA_ENVIO]
           ,[ESTADO]
           ,[FECHA_ULT_MODIF]
           ,[USUARIO_ULT_MODIF])
     VALUES
           (@codemp
           ,@ctcid
           ,@email
           ,GETDATE()
           ,@usuario
           ,@fecha_envio
           ,'V'
           ,GETDATE()
           ,@usuario)
end
else
begin

UPDATE [PRE_EMAIL_DEUDORES]
   SET [FECHA_ENVIO] = @fecha_envio
      ,[ESTADO] = 'V'
      ,[FECHA_ULT_MODIF] = GETDATE()
      ,[USUARIO_ULT_MODIF] = @usuario
 WHERE [CODEMP] = @codemp
      and [CTCID] = @ctcid
      and EMAIL = @email

end
