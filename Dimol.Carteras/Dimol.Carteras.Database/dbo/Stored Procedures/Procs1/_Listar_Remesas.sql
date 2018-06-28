-- =============================================
-- Author:		FM
-- Create date: 20140509
-- Description:	TRae Tipos Documento Caja
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Remesas] 
(
	@codemp as integer,
	@codsuc as integer, 
	@apl_anio as integer,
	@apl_numapl as integer,
	@api_item as integer

)

AS
BEGIN
	-- Declare the return variable here
	SET NOCOUNT ON;
	Select detalle_comprobantes.dcc_porcfact
    FROM detalle_comprobantes
    WHERE  detalle_comprobantes.dcc_codemp = @codemp
    and detalle_comprobantes.dcc_sucid = @codsuc
    and detalle_comprobantes.dcc_anio = @apl_anio
    and detalle_comprobantes.dcc_numapl = @apl_numapl
    and detalle_comprobantes.dcc_itemapl = @api_item
		   
END
