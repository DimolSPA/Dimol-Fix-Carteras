-- =============================================
-- Author:		FM
-- Create date: 20140509
-- Description:	TRae Lista Monedas=======
CREATE PROCEDURE [dbo].[_Trae_Lista_Monedas] 
(
	@codemp as integer
)

AS
BEGIN
	-- Declare the return variable here
	SET NOCOUNT ON;
	SELECT mon_codmon
    FROM monedas  
   WHERE ( mon_codemp = @codemp) 
         


END
