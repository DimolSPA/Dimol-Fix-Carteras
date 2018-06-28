create Procedure [dbo].[_Pre_Insertar_SMS_Deudores](@codemp integer, @ctcid numeric (15), @numero numeric(12), @fecha_envio datetime, @usuario int) as 

declare @existe int = 0

select @existe = count([NUMERO])
FROM [PRE_SMS_DEUDORES]
  where [CODEMP] = @codemp
  and [CTCID] = @ctcid
  and numero = @numero

if(@existe = 0 )
begin  
	INSERT INTO [PRE_SMS_DEUDORES]
           ([CODEMP]
           ,[CTCID]
           ,[NUMERO]
           ,[FECHA_CREACION]
           ,[USUARIO_CREACION]
           ,[FECHA_ENVIO]
           ,[ESTADO]
           ,[FECHA_ULT_MODIF]
           ,[USUARIO_ULT_MODIF])
     VALUES
           (@codemp
           ,@ctcid
           ,@numero
           ,GETDATE()
           ,@usuario
           ,@fecha_envio
           ,'V'
           ,GETDATE()
           ,@usuario)
end
else
begin

UPDATE [PRE_SMS_DEUDORES]
   SET [FECHA_ENVIO] = @fecha_envio
      ,[ESTADO] = 'V'
      ,[FECHA_ULT_MODIF] = GETDATE()
      ,[USUARIO_ULT_MODIF] = @usuario
 WHERE [CODEMP] = @codemp
      and [CTCID] = @ctcid
      and [NUMERO] = @numero

end
