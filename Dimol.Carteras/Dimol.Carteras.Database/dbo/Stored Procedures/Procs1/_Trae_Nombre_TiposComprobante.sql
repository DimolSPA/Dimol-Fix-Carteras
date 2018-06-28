-- =============================================
-- Author:		FM
-- Create date: 20140509
-- Description:	TRae Tipos Documento Caja
-- =============================================
CREATE PROCEDURE [dbo].[_Trae_Nombre_TiposComprobante] 
(
	@codemp as integer,
	@id as integer
)

AS
BEGIN
	-- Declare the return variable here
	SET NOCOUNT ON;
	SELECT clb_codigo
    FROM clasificacion_cpbtdoc  
   WHERE ( clb_codemp = @codemp) and
         (clb_clbid = @id)
         


END
