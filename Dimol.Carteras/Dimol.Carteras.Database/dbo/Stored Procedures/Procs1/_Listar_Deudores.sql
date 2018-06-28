-- =============================================
-- Author:		FM
-- Create date: 20140509
-- Description:	TRae Lista ProvCli
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Deudores] 
(
	@codemp as integer
)

AS
BEGIN
	-- Declare the return variable here
	SET NOCOUNT ON;
	SELECT deudores.ctc_nombre,   
           deudores.ctc_numero
                FROM deudores
                WHERE ctc_codemp = @codemp
  
END
