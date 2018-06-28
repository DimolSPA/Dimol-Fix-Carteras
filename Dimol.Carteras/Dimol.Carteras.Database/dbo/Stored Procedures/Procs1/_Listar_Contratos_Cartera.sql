-- =============================================
-- Author:		FM
-- Create date: 20140509
-- Description:	TRae Lista Categorias
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Contratos_Cartera] 
(
	@codemp as integer
)

AS
BEGIN
	-- Declare the return variable here
	SET NOCOUNT ON;
	select cct_cctid as ID, cct_nombre as Nombre
	from  contratos_cartera 
	where cct_codemp = @codemp order by cct_nombre
         


END
