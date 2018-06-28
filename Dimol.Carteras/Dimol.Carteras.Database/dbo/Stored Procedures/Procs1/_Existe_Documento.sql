-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 11-04-2014
-- Description:	TRae cantidad de compromisos de pago por deudor
-- =============================================
CREATE PROCEDURE [dbo].[_Existe_Documento](
	@codemp as integer,
	@sucid as integer,
	@cccid as integer, 
	@pclid as integer,
	@ctcid as integer,
	@ccbid as integer
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
select count(ccd_codemp) from cartera_clientes_campana_cpbtdoc with (nolock)
where ccd_codemp =@codemp
and ccd_sucid =@sucid
and ccd_cccid =@cccid
and ccd_pclid =@pclid
and ccd_ctcid =@ctcid
and ccd_ccbid =@ccbid
END
