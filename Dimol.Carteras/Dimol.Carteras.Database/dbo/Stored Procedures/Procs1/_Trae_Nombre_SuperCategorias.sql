-- =============================================
-- Author:		FM
-- Create date: 20140509
-- Description:	TRae Lista Categorias
-- =============================================
CREATE PROCEDURE [dbo].[_Trae_Nombre_SuperCategorias] 
(
	@codemp as integer,
	@idioma as integer,
	@id as integer
)

AS
BEGIN
	-- Declare the return variable here
	SET NOCOUNT ON;
	select sci_nombre 
	from supercategorias_idioma 
	where (sci_codemp = @codemp) and (sci_idiid = @idioma) and
           (sci_spcid = @id)
         


END
