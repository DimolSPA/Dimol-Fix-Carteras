CREATE PROCEDURE [dbo].[_Listar_Visita_Terreno_Detalle_Fotos](@idVisita int, @idVisitaDetalle int)
 AS   
 BEGIN
	SELECT
	 vtdf.PATH_IMAGEN rutaArchivo
     ,  ' Foto: ' + convert(varchar,[ID_FOTO]) texto
	FROM VISITA_TERRENO_DETALLE_FOTOS vtdf
	WHERE vtdf.ID_VISITA = @idVisita
	AND vtdf.ID_VISITA_DETALLE = @idVisitaDetalle
	AND vtdf.RUTA_IMAGEN IS NOT NULL
END
