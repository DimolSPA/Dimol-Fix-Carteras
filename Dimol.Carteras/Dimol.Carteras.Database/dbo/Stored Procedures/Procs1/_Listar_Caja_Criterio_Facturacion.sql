CREATE PROCEDURE [dbo].[_Listar_Caja_Criterio_Facturacion]
(
@codemp int,
@pclid int
)
AS
BEGIN
	SET NOCOUNT ON;
	
	select ccf.CRITERIO_ID, ccf.DESCRIPCION from
	CAJA_CRITERIO_FACTURACION ccf
	where ccf.CODEMP = @codemp
	and ccf.PCLID = @pclid

end
