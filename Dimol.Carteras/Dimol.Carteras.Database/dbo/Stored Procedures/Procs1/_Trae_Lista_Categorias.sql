-- =============================================
-- Author:		FM
-- Create date: 20140509
-- Description:	TRae Lista Categorias
-- =============================================
CREATE PROCEDURE [dbo].[_Trae_Lista_Categorias] 
(
	@codemp as integer,
	@idioma as integer
)

AS
BEGIN
	-- Declare the return variable here
	SET NOCOUNT ON;
	select cai_catid 
	from categorias_idiomas 
	where (cai_codemp = @codemp) and (cai_idiid = @idioma) and
           cai_catid in (select cat_catid 
		   from categorias 
		   where cat_codemp = @codemp and cat_utilizacion in(3,1))
         


END
