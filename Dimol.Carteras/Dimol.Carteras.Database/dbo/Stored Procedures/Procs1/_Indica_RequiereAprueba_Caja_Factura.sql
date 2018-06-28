CREATE PROCEDURE [dbo].[_Indica_RequiereAprueba_Caja_Factura](
@criterioId int
)
as
BEGIN

declare @IndRequiereAprueba varchar(1) = 'N';
set @IndRequiereAprueba =(select REQUIERE_APRUEBA from CAJA_CRITERIO_FACTURACION where CRITERIO_ID = @criterioId);

select  @IndRequiereAprueba result --- N corresponde facturar, S no corresponde facturar

END
