CREATE PROCEDURE [dbo].[_Insertar_Visita_Terreno_Formulario_Solicitud](
@formularioId int, @solicitudId int)
 AS   
BEGIN
	INSERT INTO VISITA_TERRENO_FORMULARIO_SOLICITUD(FORMULARIOID,SOLICITUD_ID)
	VALUES (@formularioId,@solicitudId)
END
