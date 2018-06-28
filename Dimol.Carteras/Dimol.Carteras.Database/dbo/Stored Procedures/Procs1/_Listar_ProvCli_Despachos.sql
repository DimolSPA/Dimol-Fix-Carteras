-- =============================================
-- Author:		FM
-- Create date: 20140509
-- Description:	TRae Lista Super Categorias
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_ProvCli_Despachos] 
(
	@codemp as integer
)

AS
BEGIN
	-- Declare the return variable here
	SET NOCOUNT ON;
	select provcli_despachos.pcd_pcdid,provcli_despachos.pcd_pclid,provcli.pcl_nomfant, provcli_despachos.pcd_nombre
	from provcli, provcli_despachos
	where provcli_despachos.pcd_codemp = provcli.pcl_codemp  and  
		  provcli_despachos.pcd_pclid = ProvCli.pcl_pclid and 
		  pcd_codemp = @codemp order by pcl_nomfant, pcd_nombre
         


END
