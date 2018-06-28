CREATE PROCEDURE [dbo].[_Listar_Visita_Terreno_Detalle_Telefonos](@idVisita int, @idVisitaDetalle int)
 AS   
 BEGIN
	SELECT 
     vtdt.NUMERO
	FROM VISITA_TERRENO_DETALLE_TELEFONOS vtdt
	WHERE vtdt.ID_VISITA = @idVisita
	AND vtdt.ID_VISITA_DETALLE = @idVisitaDetalle
END
