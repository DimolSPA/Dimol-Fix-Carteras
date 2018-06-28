create Procedure [dbo].[_Pre_Eliminar_SMS_Deudores](@codemp integer, @ctcid numeric (15), @numero numeric(12),@usuario int) as 

begin

UPDATE [PRE_SMS_DEUDORES]
   SET [ESTADO] = 'N'
      ,[FECHA_ULT_MODIF] = GETDATE()
      ,[USUARIO_ULT_MODIF] = @usuario
 WHERE [CODEMP] = @codemp
      and [CTCID] = @ctcid
      and [NUMERO] = @numero

end
