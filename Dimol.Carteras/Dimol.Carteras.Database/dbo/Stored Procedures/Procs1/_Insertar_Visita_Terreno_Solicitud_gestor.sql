CREATE PROCEDURE [dbo].[_Insertar_Visita_Terreno_Solicitud_gestor](
@solicitudId int,
@gestorId int,
@gestor varchar(100),
@telefonoImei varchar(100),
@telefonoNum varchar(9),
@userId int)

AS
BEGIN

	-- INSERTAR DATOS DE GESTOR EN LA SOLICITUD
	INSERT INTO VISITA_TERRENO_SOLICITUD_GESTOR(SOLICITUD_ID, GESID, GESTOR,TELEFONO_IMEI, TELEFONO_NUM, USRID_REGISTRO)
	VALUES (@solicitudId, @gestorId, @gestor, @telefonoImei, @telefonoNum, @userId)

	SELECT @solicitudId solicitud	
	
END
