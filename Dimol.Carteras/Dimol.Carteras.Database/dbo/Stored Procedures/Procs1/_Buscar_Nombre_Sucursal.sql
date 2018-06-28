-- =============================================
-- Author:		FM
-- Create date: 12-05-2014
-- Description:	Lista regiones segun pais
-- =============================================
CREATE PROCEDURE [dbo].[_Buscar_Nombre_Sucursal] (@codemp int, @pclid int, @pcsid int)
AS
BEGIN
	SET NOCOUNT ON;
	if @codemp =1 and @pclid = 424 and @pcsid=1
		begin
		select 'TALCA - 14 ORIENTE'
		end
		else
		select PCS_NOMBRE
		from PROVCLI_SUCURSAL 
		where PCS_CODEMP = @codemp
		and PCS_PCLID = @pclid
		and PCS_PCSID = @pcsid
		
	
END
