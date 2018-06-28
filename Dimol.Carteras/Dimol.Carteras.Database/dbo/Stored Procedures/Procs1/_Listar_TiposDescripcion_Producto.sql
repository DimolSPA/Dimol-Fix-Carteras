-- =============================================
-- Author:		FM
-- Create date: 20140509
-- Description:	TRae Lista Tipos Insumos
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_TiposDescripcion_Producto] 
(
	@codemp as integer,
	@idioma as integer
)

AS
BEGIN
	-- Declare the return variable here
	SET NOCOUNT ON;
	SELECT tpi_tpdid as ID, tpi_nombre as NOMBRE
    FROM tipos_descripcion_producto_idioma
   WHERE ( tpi_codemp = @codemp) and (tpi_idiid = @idioma)
         


END
