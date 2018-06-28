-- =============================================
-- Author:		FM
-- Create date: 20140509
-- Description:	TRae Lista Talonarios
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Talonarios] 
(
	@codemp as integer
)

AS
BEGIN
	-- Declare the return variable here
	SET NOCOUNT ON;
	SELECT tac_tacid, tac_nombre from talonario_cpbtdoc 
                where tac_codemp= @codemp
               order by tac_nombre
  
END
