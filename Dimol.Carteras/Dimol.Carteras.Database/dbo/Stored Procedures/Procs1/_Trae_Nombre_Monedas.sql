-- =============================================
-- Author:		FM
-- Create date: 20140509
-- Description:	TRae Tipos Documento Caja
-- =============================================
CREATE PROCEDURE [dbo].[_Trae_Nombre_Monedas] 
(
	@codemp as integer,
	@id as integer
)

AS
BEGIN
	-- Declare the return variable here
	SET NOCOUNT ON;
	SELECT mon_nombre 
    FROM monedas  
   WHERE ( mon_codemp = @codemp) and
         ( mon_codmon = @id)
         


END
