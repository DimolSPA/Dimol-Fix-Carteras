CREATE PROCEDURE [dbo].[_Listar_Visita_Terreno_Detalle_GPS](@idVisita int, @idVisitaDetalle int)
 AS   
 BEGIN
	SELECT
	  gps.ID_VISITA,
	  gps.ID_VISITA_DETALLE,
	  gps.LATITUD,
	  gps.LONGITUD,
	  gps.ALTITUD,
	  gps.DIRECCION,
	  gps.COMUNA,
	  gps.CIUDAD
	FROM VISITA_TERRENO_DETALLE_GPS gps
	WHERE gps.ID_VISITA = @idVisita
	AND gps.ID_VISITA_DETALLE = @idVisitaDetalle
END
