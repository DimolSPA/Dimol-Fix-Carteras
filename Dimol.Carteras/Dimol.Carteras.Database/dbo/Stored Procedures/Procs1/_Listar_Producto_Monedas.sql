-- =============================================
-- Author:		FM
-- Create date: 20140509
-- Description:	TRae Lista Categorias
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Producto_Monedas] 
(
	@codemp as integer
)

AS
BEGIN
	-- Declare the return variable here
	SET NOCOUNT ON;
	SELECT DISTINCT monedas.mon_nombre as Nombre, productos_moneda.pdm_codmon as ID,productos_moneda.pdm_precio
    FROM productos_moneda, monedas
    WHERE  monedas.mon_codemp = productos_moneda.pdm_codemp  and  
	monedas.mon_codmon = productos_moneda.pdm_codmon  and  
	productos_moneda.pdm_codemp = @codemp
         


END
