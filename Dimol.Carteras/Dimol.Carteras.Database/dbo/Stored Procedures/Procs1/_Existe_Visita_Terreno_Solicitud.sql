CREATE PROCEDURE [dbo].[_Existe_Visita_Terreno_Solicitud](
@solicitudId int)

AS
BEGIN
	DECLARE @existeSolicitud int = 0;
	
	-- VERIFICAMOS SI LA SOLICITUD
	set @existeSolicitud = (select count(id_visita) from VISITA_TERRENO where solicitud_id = @solicitudId)
	-- SI NO EXISTE, ENVIAMOS -1
	if (@existeSolicitud = 0)
	begin
	SELECT -1 solicitud
	end
	else
	-- SI EXISTE ENVIAMOS LA SOLICITUD ID
	begin
	SELECT @solicitudId solicitud
	end
	
END
