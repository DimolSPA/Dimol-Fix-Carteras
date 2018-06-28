-- =============================================
-- Author:		FM
-- Create date: 20140509
-- Description:	TRae Lista Super Categorias
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Anios_Comision] 
(
	@codemp as integer,
	@codsuc as integer
)

AS
BEGIN
	-- Declare the return variable here
	SET NOCOUNT ON;
	SELECT DISTINCT comisiones.cms_anio  
    FROM comisiones
    WHERE  comisiones.cms_codemp = @codemp
    and comisiones.cms_sucid = @codsuc
    ORDER BY cms_anio DESC    


END
