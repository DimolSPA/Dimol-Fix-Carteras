CREATE PROCEDURE [dbo].[_Actualizar_Visita_Terreno_Formulario_Procesado](
@formularioId int, @procesado varchar(1))
 AS   
BEGIN
	UPDATE VISITA_TERRENO_FORMULARIO
	SET PROCESADO = @procesado
	WHERE FORMULARIOID = @formularioId
END
