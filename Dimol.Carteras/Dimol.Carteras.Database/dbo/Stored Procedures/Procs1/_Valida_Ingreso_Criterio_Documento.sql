CREATE PROCEDURE [dbo].[_Valida_Ingreso_Criterio_Documento](
@documentoId int
)
AS
BEGIN
select ISNULL(CRITERIO_ID,0) CriterioId
from CAJA_RECEPCION_DOCUMENTOS
where DOCUMENTO_ID = @documentoId
END
