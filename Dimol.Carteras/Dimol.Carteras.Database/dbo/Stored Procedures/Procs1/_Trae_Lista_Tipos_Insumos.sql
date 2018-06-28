-- =============================================
-- Author:		FM
-- Create date: 20140509
-- Description:	TRae Lista Tipos Insumos
-- =============================================
CREATE PROCEDURE [dbo].[_Trae_Lista_Tipos_Insumos] 
(
	@codemp as integer,
	@idioma as integer
)

AS
BEGIN
	-- Declare the return variable here
	SET NOCOUNT ON;
	SELECT tii_tipid
    FROM tipos_insumo_idiomas  
   WHERE ( tii_codemp = @codemp) AND (tii_idid = @idioma)
         


END
