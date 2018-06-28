-- =============================================
-- Author:		FM
-- Create date: 20140509
-- Description:	TRae Lista Super Categorias
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Medidas] 
(
	@idioma as integer
)

AS
BEGIN
	-- Declare the return variable here
	SET NOCOUNT ON;
	select um.unm_unmid as ID, umi.umi_nombre as Nombre
	from unidades_medida um, unidades_medida_idiomas umi
	where umi.umi_unmid = um.unm_unmid  and  um.unm_agrupa = 1 and umi_idiid = @idioma 
         


END
