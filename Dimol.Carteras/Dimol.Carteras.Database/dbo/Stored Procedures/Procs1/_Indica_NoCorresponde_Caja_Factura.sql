CREATE PROCEDURE [dbo].[_Indica_NoCorresponde_Caja_Factura](
@criterioId int
)
as
BEGIN

declare @IndFacturadoNoCorresponde varchar(1) = 'N';
set @IndFacturadoNoCorresponde =(select FACTURADO_NOCORRESPONDE from CAJA_CRITERIO_FACTURACION where CRITERIO_ID = @criterioId);

select  @IndFacturadoNoCorresponde result --- N corresponde facturar, S no corresponde facturar

END
