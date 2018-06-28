CREATE PROCEDURE [dbo].[_Listar_Visita_Terreno_Formulario_ById](
@formId varchar(500))

AS
BEGIN
	SELECT fec_envio fechavisita, formularioid, nombre nombreformulario, gestor, cliente deudor,posicion,direccion,
	foto1, foto2, foto3, foto4, estado estadovisita, visita, direccionactual, comentarios,
	direccion1, comuna1, direccion2, comuna2, procesado
FROM VISITA_TERRENO_FORMULARIO
WHERE FORMID = @formId
AND PROCESADO = 'N'
	
END
