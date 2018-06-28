CREATE PROCEDURE [dbo].[_Insertar_Visita_Terreno_ClientePoint_gestor](
@clientePointId varchar(100),
@solicitudId int,
@gestorId int,
@telefonoImei varchar(100),
@userId int)

AS
BEGIN

	-- INSERTAR DATOS CLIENTEPOINT GESTOR
	INSERT INTO VISITA_TERRENO_CLIENTEPOINT_GESTOR(CLIENTEPOINT_ID, SOLICITUD_ID, GESID, TELEFONO_IMEI, USRID_REGISTRO)
	VALUES (@clientePointId, @solicitudId, @gestorId, @telefonoImei, @userId)

	SELECT 1 pointId	
	
END
