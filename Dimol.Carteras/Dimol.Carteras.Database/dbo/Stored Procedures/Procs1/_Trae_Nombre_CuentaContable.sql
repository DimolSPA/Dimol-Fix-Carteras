-- =============================================
-- Author:		FM
-- Create date: 20140509
-- Description:	TRae Nombre Cuentas Contables
-- =============================================
CREATE PROCEDURE [dbo].[_Trae_Nombre_CuentaContable] 
(
	@codemp as integer,
	@id as integer
)

AS
BEGIN
	-- Declare the return variable here
	SET NOCOUNT ON;
	SELECT PCT_NOMBRE
    FROM PLAN_CUENTAS  
   WHERE ( PCT_CODEMP = @codemp) and (PCT_PCTID = @id) ORDER BY PCT_NOMBRE
         


END
