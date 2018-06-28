CREATE PROCEDURE [dbo].[_Actualizar_Visita_Terreno_Solicitud](
@solicitudId int,
@estado int,
@user int,
@visitada varchar(1))

AS
BEGIN
	declare @fecActualiza datetime = GETDATE();
-- Actualizar estado visita_terreno_solicitud
	UPDATE VISITA_TERRENO_SOLICITUD
	SET ID_ESTATUS = @estado, USRID_CREACION = @user, FEC_CREACION = @fecActualiza, VISITADA = @visitada
	WHERE SOLICITUD_ID = @solicitudId

-- Actualizar estado visita_terreno
	UPDATE VISITA_TERRENO
	SET ID_ESTATUS = @estado, FEC_CREACION = @fecActualiza
	WHERE SOLICITUD_ID = @solicitudId

END
