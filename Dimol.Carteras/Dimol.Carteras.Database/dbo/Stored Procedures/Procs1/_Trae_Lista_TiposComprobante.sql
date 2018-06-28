-- =============================================
-- Author:		FM
-- Create date: 20140509
-- Description:	TRae Tipos Documento Caja
-- =============================================
CREATE PROCEDURE [dbo].[_Trae_Lista_TiposComprobante] 
(
	@codemp as integer
)

AS
BEGIN
	-- Declare the return variable here
	SET NOCOUNT ON;
	SELECT clb_clbid
    FROM clasificacion_cpbtdoc  
   WHERE ( clb_codemp = @codemp) 
         


END
