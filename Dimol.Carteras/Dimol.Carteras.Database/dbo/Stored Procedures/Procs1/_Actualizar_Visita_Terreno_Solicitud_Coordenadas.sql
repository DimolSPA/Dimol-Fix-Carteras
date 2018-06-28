CREATE PROCEDURE [dbo].[_Actualizar_Visita_Terreno_Solicitud_Coordenadas](
@solicitudId INT,
@latitud NUMERIC(12,9),
@longitud NUMERIC(12,9)
)

AS
BEGIN
	
	UPDATE VISITA_TERRENO_SOLICITUD
	SET LATITUD = @latitud, LONGITUD = @longitud
	WHERE SOLICITUD_ID = @solicitudId

END
