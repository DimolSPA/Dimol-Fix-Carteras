-- =============================================
-- Author:		FM
-- Create date: 20140509
-- Description:	TRae Lista Categorias
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Medidas_Peso] 
(
	@idioma as integer
)

AS
BEGIN
	-- Declare the return variable here
	SET NOCOUNT ON;
	SELECT unidades_medida.unm_unmid as ID, unidades_medida_idiomas.umi_nombre as Nombre
	FROM unidades_medida, unidades_medida_idiomas
	WHERE  unidades_medida_idiomas.umi_unmid = unidades_medida.unm_unmid  and  
	unidades_medida.unm_agrupa = 3 And umi_idiid = @idioma
         


END
