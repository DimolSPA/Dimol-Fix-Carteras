CREATE PROCEDURE [dbo].[_Insertar_Visita_Terreno](
@solicitudId int,
@ctcid int,
@userId int)

AS
BEGIN

declare
	@visitaId int = 0
	
	-- INSERTAR DATOS EN VISITA TERRENO
	SET @visitaId = (SELECT IsNull(Max(ID_VISITA)+1, 1)
						FROM VISITA_TERRENO
						WHERE CTCID = @ctcid)
		
	INSERT INTO VISITA_TERRENO(ID_VISITA, CTCID, SOLICITUD_ID, USRID_CREACION)
	VALUES(@visitaId, @ctcid, @solicitudId, @userId)
	select @solicitudId solicitud
END
