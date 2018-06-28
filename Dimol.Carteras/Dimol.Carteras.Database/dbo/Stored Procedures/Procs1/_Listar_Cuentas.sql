-- =============================================
-- Author:		FM
-- Create date: 20140509
-- Description:	TRae Tipos Documento Caja
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Cuentas] 
(
	@codemp as integer
)

AS
BEGIN
	-- Declare the return variable here
	SET NOCOUNT ON;
	SELECT PCT_PCTID as ID, PCT_NOMBRE as NOMBRE
    FROM PLAN_CUENTAS  
   WHERE ( PCT_CODEMP = @codemp) order by PCT_NOMBRE
         


END
