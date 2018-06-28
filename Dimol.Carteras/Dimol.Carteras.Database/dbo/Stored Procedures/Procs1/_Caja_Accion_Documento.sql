CREATE PROCEDURE _Caja_Accion_Documento
(
--declare
@codemp int,
@pclid int,
@ctcid int,
@ccbid int
)
AS
BEGIN
	SET NOCOUNT ON;
	select ccb_numero numero from 
	CARTERA_CLIENTES_CPBT_DOC
	where ccb_codemp = @codemp
	and ccb_pclid = @pclid
	and ccb_ctcid = @ctcid
	and ccb_ccbid = @ccbid

END