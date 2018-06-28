CREATE PROCEDURE [dbo].[_Insertar_Conciliacion_Archivo_Comprobante](
@conciliacionId int,
@movimientoId int,
@pclid NUMERIC(15,0),
@ctcid NUMERIC(15,0),
@rutaArchivo varchar(400),
@userId int)
AS
BEGIN
	declare @imagenId int = 0, @numComprobante int;
	SET @numComprobante = (SELECT NUM_COMPROBANTE
							FROM CONCILIACION_MOVIMIENTOS_DOCUMENTOS where  CONCILIACION_ID = @conciliacionId)
	--Se crea conciliacion
	SET @imagenId = (SELECT IsNull(Max(IMAGEN_ID)+1, 1)
						FROM CONCILIACION_MOVIMIENTOS_ARCHIVOS)

	INSERT INTO CONCILIACION_MOVIMIENTOS_ARCHIVOS
        (IMAGEN_ID,CONCILIACION_ID,MOVIMIENTO_ID,NUM_COMPROBANTE,PCLID,CTCID,PATH_ARCHIVO,USRID_REGISTRO)
		VALUES
		(@imagenId, @conciliacionId, @movimientoId,@numComprobante, @pclid, @ctcid, @rutaArchivo,@userId)

END
