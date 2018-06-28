-- =============================================
-- Author:		FM
-- Create date: 20140509
-- Description:	TRae Lista Tipos Insumos
-- =============================================
CREATE PROCEDURE [dbo].[_Trae_Nombre_Tipos_Insumos] 
(
	@codemp as integer,
	@idioma as integer,
	@id as integer
)

AS
BEGIN
	-- Declare the return variable here
	SET NOCOUNT ON;
	SELECT tii_nombre
    FROM tipos_insumo_idiomas  
   WHERE ( tii_codemp = @codemp) AND (tii_idid = @idioma) and (tii_tipid = @id)
         


END
