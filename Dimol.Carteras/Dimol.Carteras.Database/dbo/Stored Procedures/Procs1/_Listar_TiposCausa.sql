-- =============================================
-- Author:		FM
-- Create date: 20140509
-- Description:	TRae Lista Super Categorias
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_TiposCausa] 
(
	@codemp as integer,
	@idioma as integer
)

AS
BEGIN
	-- Declare the return variable here
	SET NOCOUNT ON;
	select tci_tcaid, tci_nombre 
	from tipos_causa_idiomas 
	where tci_codemp= @codemp
	and tci_idid= @idioma order by tci_nombre
         


END
