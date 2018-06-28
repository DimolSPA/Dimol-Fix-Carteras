create procedure [dbo].[_Eliminar_Cuenta_Ejecutivo] (@cuenta int) as
 DELETE FROM EJECUTIVO_CUENTA_MUTUAL 
 WHERE ID_CUENTA_EJECUTIVO = @cuenta
