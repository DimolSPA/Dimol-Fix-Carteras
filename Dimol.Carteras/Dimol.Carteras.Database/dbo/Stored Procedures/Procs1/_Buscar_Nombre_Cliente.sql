-- =============================================
-- Author:		FM
-- Create date: 12-05-2014
-- Description:	Lista regiones segun pais
-- =============================================
CREATE PROCEDURE [dbo].[_Buscar_Nombre_Cliente] (@codemp int, @pclid int)
AS
BEGIN
	SET NOCOUNT ON;
	select PCL_NOMFANT from PROVCLI 
		where PCL_CODEMP = @codemp
		and PCL_PCLID = @pclid

		
	
END