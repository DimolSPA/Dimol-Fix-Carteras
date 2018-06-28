create procedure [dbo].[_Eliminar_Ejecutivo_Mutual] (@idejecutivo int) as

	DELETE FROM EJECUTIVO_CUENTA_MUTUAL 
	WHERE ID_EJECUTIVO = @idejecutivo

	DELETE FROM PROVCLI_EJECUTIVO
	WHERE ID_EJECUTIVO = @idejecutivo
	
	DELETE FROM EJECUTIVO_MUTUAL 
	WHERE ID_EJECUTIVO = @idejecutivo
