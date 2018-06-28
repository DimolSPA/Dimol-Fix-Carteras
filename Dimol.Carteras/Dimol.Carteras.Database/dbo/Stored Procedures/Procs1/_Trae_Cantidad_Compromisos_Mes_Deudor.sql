-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 11-04-2014
-- Description:	TRae cantidad de compromisos de pago por deudor
-- =============================================
CREATE PROCEDURE [dbo].[_Trae_Cantidad_Compromisos_Mes_Deudor](
	@codemp as integer,
	@pclid as integer,
	@ctcid as integer,
	@ccbid as integer,
	@estid as integer,
	@gesid as integer 
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
SELECT count(ceh_ctcid) as Tot  
FROM cartera_clientes_estados_historial
WHERE  cartera_clientes_estados_historial.ceh_codemp = @codemp
and cartera_clientes_estados_historial.ceh_pclid = @pclid
and cartera_clientes_estados_historial.ceh_ctcid = @ctcid
and cartera_clientes_estados_historial.ceh_ccbid = @ccbid
and cartera_clientes_estados_historial.ceh_estid = @estid
and cartera_clientes_estados_historial.ceh_gesid = @gesid
and datepart(year,cartera_clientes_estados_historial.ceh_fecha) = datepart(year, getdate())
and datepart(MONTH,cartera_clientes_estados_historial.ceh_fecha) = datepart(MONTH, getdate())
END
