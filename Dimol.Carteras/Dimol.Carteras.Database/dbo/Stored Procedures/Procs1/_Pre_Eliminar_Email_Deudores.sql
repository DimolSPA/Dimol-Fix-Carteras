create Procedure [dbo].[_Pre_Eliminar_Email_Deudores](@codemp integer, @ctcid numeric (15), @email varchar(250),  @usuario int) as 


begin

UPDATE [PRE_EMAIL_DEUDORES]
   SET [ESTADO] = 'N'
      ,[FECHA_ULT_MODIF] = GETDATE()
      ,[USUARIO_ULT_MODIF] = @usuario
 WHERE [CODEMP] = @codemp
      and [CTCID] = @ctcid
      and EMAIL = @email

end
