CREATE PROCEDURE [dbo].[_Obtener_Estatus_Documento](
--declare
@documentoId int
)
as
BEGIN
	 select ESTATUS_ID EstatusId 
	 from CAJA_RECEPCION_DOCUMENTOS
	 where documento_id =@documentoId

END
