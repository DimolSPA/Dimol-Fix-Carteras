
CREATE  FUNCTION [dbo].[_Trae_Interes_Aplicaciones] 
(
	@codemp INT,
	@ctcid INT,
	@pclid INT,
	@ccbid INT
)
RETURNS decimal(15,2)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @interesesAplicaciones  decimal(15,2)
	DECLARE @interesesReversas  decimal(15,2)
	-- Add the T-SQL statements to compute the return value here
	select @interesesAplicaciones = sum(aplicaciones_items.api_interes)       
	from aplicaciones_items 
	join dbo.aplicaciones 
	on aplicaciones_items.api_codemp = dbo.aplicaciones.apl_codemp 
	and dbo.aplicaciones_items.api_sucid = dbo.aplicaciones.apl_sucid 
	and dbo.aplicaciones_items.api_anio = dbo.aplicaciones.apl_anio 
	and dbo.aplicaciones_items.api_numapl = dbo.aplicaciones.apl_numapl
	where aplicaciones_items.API_CODEMP = @codemp
	and aplicaciones_items.API_CTCID = @ctcid
	and aplicaciones_items.API_PCLID = @pclid
	and aplicaciones_items.API_CCBID = @ccbid
	and aplicaciones.apl_accion = -1


	select @interesesReversas = sum(aplicaciones_items.api_interes)       
	from aplicaciones_items 
	join dbo.aplicaciones 
	on aplicaciones_items.api_codemp = dbo.aplicaciones.apl_codemp 
	and dbo.aplicaciones_items.api_sucid = dbo.aplicaciones.apl_sucid 
	and dbo.aplicaciones_items.api_anio = dbo.aplicaciones.apl_anio 
	and dbo.aplicaciones_items.api_numapl = dbo.aplicaciones.apl_numapl
	where aplicaciones_items.API_CODEMP = @codemp
	and aplicaciones_items.API_CTCID = @ctcid
	and aplicaciones_items.API_PCLID = @pclid
	and aplicaciones_items.API_CCBID = @ccbid
	and aplicaciones.apl_accion = 1
	
	-- Return the result of the function
	RETURN isnull(@interesesAplicaciones, 0) - isnull(@interesesReversas, 0)

END
