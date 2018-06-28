CREATE PROCEDURE [dbo].[_Guardar_Caja_Recepcion_Documentos_Criterio](
--declare
@documentoId int,
@criterioId int,
@montoFacturar decimal(15,2),
@observaciones varchar(500),
@userId int
)
as
BEGIN
	declare @historialCriterio int = 0;

	UPDATE CAJA_RECEPCION_DOCUMENTOS
	SET CRITERIO_ID = @criterioId, MONTO_FACTURAR = @montoFacturar, OBSERVACIONES = @observaciones
	WHERE DOCUMENTO_ID = @documentoId

	--Agrega al historial de criterios
	SET @historialCriterio = (SELECT IsNull(Max(HISTORIAL_CRITERIO_ID)+1, 1)
							FROM CAJA_RECEPCION_DOCUMENTOS_HISTORIAL_CRITERIO) 
	INSERT INTO CAJA_RECEPCION_DOCUMENTOS_HISTORIAL_CRITERIO
           (HISTORIAL_CRITERIO_ID,DOCUMENTO_ID,ID_CRITERIO,MONTO_FACTURAR,USRID_CREACION)
     VALUES
           (@historialCriterio, @documentoId,@criterioId, @montoFacturar,@userId)

END
