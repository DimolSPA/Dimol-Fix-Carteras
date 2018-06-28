CREATE PROCEDURE [dbo].[_Guardar_Caja_Recepcion_Documentos_Factura](
--declare
@documentoId int,
@numFact varchar(20),
@observaciones varchar(500),
@userId int
)
as
BEGIN
	declare @historialCriterio int = 0;

	UPDATE CAJA_RECEPCION_DOCUMENTOS
	SET NUM_FACT = @numFact, OBSERVACIONES_FACTURA = @observaciones
	WHERE DOCUMENTO_ID = @documentoId

	exec dbo._Insertar_Caja_Recepcion_Documento_Historial @documentoId, 1, null, @userId

END
