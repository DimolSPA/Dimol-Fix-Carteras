CREATE PROCEDURE [dbo].[_Tiene_CriterioFacturacion](
--declare
@documentoId int
)
as
BEGIN
	 select count(criterio_id) CountCriterio 
	 from CAJA_CRITERIO_FACTURACION
	 where pclid = (select pclid from CAJA_RECEPCION_DOCUMENTOS
	 where documento_id =@documentoId)

END
