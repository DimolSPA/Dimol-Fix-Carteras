-- =============================================
-- Author:		FM
-- Create date: 20140509
-- Description:	TRae Lista ProvCli
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_ProvCli] 
(
	@codemp as integer
)

AS
BEGIN
	-- Declare the return variable here
	SET NOCOUNT ON;
	SELECT distinct entes_judicial.etj_etjid, provcli.pcl_nomfant
            FROM entes_judicial,   
            provcli
            WHERE  provcli.pcl_codemp = entes_judicial.etj_codemp  and  
            provcli.pcl_pclid = entes_judicial.etj_pclid  and  
            entes_judicial.etj_codemp = @codemp
            order by pcl_nomfant
  
END
