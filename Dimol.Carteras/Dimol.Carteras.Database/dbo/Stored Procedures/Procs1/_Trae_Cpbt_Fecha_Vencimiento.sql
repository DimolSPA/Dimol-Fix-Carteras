-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 13-04-2014
-- Description:	Trae supervisor
-- =============================================
CREATE PROCEDURE [dbo].[_Trae_Cpbt_Fecha_Vencimiento](
	@codemp as integer,
	@estid as integer,
	@pclid as integer,
	@ctcid as integer,
	@fechaVencimiento as date
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
SELECT cartera_clientes_cpbt_doc.ccb_pclid,
cartera_clientes_cpbt_doc.ccb_ctcid, ccb_ccbid, ccb_estcpbt, ccb_fecplazo, ccb_monto, ccb_saldo, ccb_fecvenc
FROM cartera_clientes_cpbt_doc
WHERE  cartera_clientes_cpbt_doc.ccb_codemp = @codemp
and cartera_clientes_cpbt_doc.ccb_estid = @estid
and ccb_pclid = @pclid
and ccb_ctcid = @ctcid
and ccb_fecvenc < @fechaVencimiento
END
