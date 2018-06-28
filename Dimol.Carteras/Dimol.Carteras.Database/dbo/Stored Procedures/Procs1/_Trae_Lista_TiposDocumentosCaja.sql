-- =============================================
-- Author:		FM
-- Create date: 20140509
-- Description:	TRae Tipos Documento Caja
-- =============================================
CREATE PROCEDURE [dbo].[_Trae_Lista_TiposDocumentosCaja] 
(
	@codemp as integer
)

AS
BEGIN
	-- Declare the return variable here
	SET NOCOUNT ON;
	SELECT view_tipos_cpbtdoc_clasificacion.tpc_tpcid
    FROM view_tipos_cpbtdoc_clasificacion  
   WHERE ( view_tipos_cpbtdoc_clasificacion.clb_tipcpbtdoc = 'D') and  
         ( view_tipos_cpbtdoc_clasificacion.clb_aplica = 'S') and
         ( view_tipos_cpbtdoc_clasificacion.tpc_codemp = @codemp) 
         


END
