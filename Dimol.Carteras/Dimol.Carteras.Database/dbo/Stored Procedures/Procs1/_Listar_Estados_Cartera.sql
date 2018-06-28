-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 04-06-2014
-- Description:	Procedimiento para listar telefonos por deudor y cliente para jQgrid
-- =============================================
create PROCEDURE [dbo].[_Listar_Estados_Cartera]
(
@codemp int,
@idioma integer
)
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT eci_estid Id,    
           eci_nombre Descripcion
           FROM estados_cartera_idiomas
           WHERE eci_codemp = @codemp
           and eci_idid =  @idioma	

END
