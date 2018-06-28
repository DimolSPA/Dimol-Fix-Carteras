-- =============================================
-- Author:		FM
-- Create date: 20140509
-- Description:	TRae Tipos Documento Caja
-- =============================================
CREATE PROCEDURE [dbo].[_Trae_Lista_Cuentas] 
(
	@codemp as integer
)

AS
BEGIN
	-- Declare the return variable here
	SET NOCOUNT ON;
	SELECT PCT_PCTID
    FROM PLAN_CUENTAS  
   WHERE ( PCT_CODEMP = @codemp) 
         


END
