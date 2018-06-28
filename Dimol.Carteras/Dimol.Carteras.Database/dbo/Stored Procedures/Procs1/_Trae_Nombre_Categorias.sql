-- =============================================
-- Author:		FM
-- Create date: 20140509
-- Description:	TRae Lista Categorias
-- =============================================
CREATE PROCEDURE [dbo].[_Trae_Nombre_Categorias] 
(
	@codemp as integer,
	@idioma as integer,
	@id as integer
)

AS
BEGIN
	-- Declare the return variable here
	SET NOCOUNT ON;
	select cai_nombre 
	from categorias_idiomas 
	where (cai_codemp = @codemp) and (cai_idiid = @idioma) and
           cai_catid = @id
         


END
