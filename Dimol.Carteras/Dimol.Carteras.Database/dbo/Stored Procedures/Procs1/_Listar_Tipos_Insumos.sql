-- =============================================
-- Author:		FM
-- Create date: 20140509
-- Description:	TRae Lista Tipos Insumos
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Tipos_Insumos] 
(
	@codemp as integer
)

AS
BEGIN
	-- Declare the return variable here
	SET NOCOUNT ON;
	SELECT tii_tipid as ID, TII_NOMBRE as NOMBRE
    FROM tipos_insumo_idiomas  
   WHERE ( tii_codemp = @codemp) 
         


END
