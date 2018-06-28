-- =============================================
-- Author:		FM
-- Create date: 20140509
-- Description:	TRae Lista Super Categorias
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Tribunales] 
(
	@codemp as integer
)

AS
BEGIN
	-- Declare the return variable here
	SET NOCOUNT ON;
	select trb_trbid, trb_nombre 
	from tribunales 
	where trb_codemp= @codemp order by trb_nombre
         


END
