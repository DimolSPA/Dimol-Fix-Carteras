CREATE PROCEDURE [dbo].[_Actualizar_Visita_Terreno_Formulario](
@formId varchar(500))
 AS   
BEGIN
	UPDATE VISITA_TERRENO_FORMULARIO
	SET PROCESADO = 'S'
	WHERE FORMID = @formId
END
