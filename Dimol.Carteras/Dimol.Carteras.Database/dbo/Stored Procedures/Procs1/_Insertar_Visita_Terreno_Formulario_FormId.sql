CREATE PROCEDURE [dbo].[_Insertar_Visita_Terreno_Formulario_FormId](
@formularioId VARCHAR(500))
 AS   
BEGIN
	INSERT INTO VISITA_TERRENO_FORMULARIO_FORMID(FORMID)
	VALUES (@formularioId)
	select @formularioId formId
END
