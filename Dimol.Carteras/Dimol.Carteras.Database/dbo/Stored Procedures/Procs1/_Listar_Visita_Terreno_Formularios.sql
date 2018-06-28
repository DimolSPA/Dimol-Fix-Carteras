CREATE PROCEDURE [dbo].[_Listar_Visita_Terreno_Formularios](
@idCarga int,
@procesado varchar(1))
AS
BEGIN
SELECT fec_envio fechavisita, formularioid, nombre nombreformulario, gestor, cliente deudor,posicion,direccion,
	foto1, foto2, foto3, foto4, estado estadovisita, visita, direccionactual, comentarios,
	direccion1, comuna1, direccion2, comuna2, procesado
FROM VISITA_TERRENO_FORMULARIO
WHERE IDCARGA = @idCarga
AND PROCESADO = @procesado
END
