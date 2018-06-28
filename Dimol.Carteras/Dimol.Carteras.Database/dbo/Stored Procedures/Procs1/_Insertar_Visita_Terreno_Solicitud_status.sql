CREATE PROCEDURE [dbo].[_Insertar_Visita_Terreno_Solicitud_status](
@solicitudId int,
@userId int,
@estado int)

AS
BEGIN
	DECLARE @existeStatus int = 0;
	
	-- VERIFICAMOS SI EXISTE EL ESTATUS PARA LA SOLICITUD
	set @existeStatus = (select count(solicitud_id) from VISITA_TERRENO_SOLICITUD_ESTATUS 
						where SOLICITUD_ID = @solicitudId and ID_ESTATUS = @estado)
	if (@existeStatus = 0)
	begin
	-- INSERTAR DATOS EN EL HISTORIAL DE SOLICITUD
	INSERT INTO VISITA_TERRENO_SOLICITUD_ESTATUS(SOLICITUD_ID, ID_ESTATUS, USRID_CREACION)
	VALUES (@solicitudId, @estado, @userId)
	end
	

	SELECT @solicitudId solicitud	
	
END
