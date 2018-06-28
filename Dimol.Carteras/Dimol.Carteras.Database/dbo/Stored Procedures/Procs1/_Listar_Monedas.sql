-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 04-06-2014
-- Description:	Procedimiento para listar telefonos por deudor y cliente para jQgrid
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Monedas]
(
@codemp int
)
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT mon_codmon Id, mon_nombre Descripcion 
from monedas 
 WHERE mon_codemp = @codemp
 order by 1 


END
