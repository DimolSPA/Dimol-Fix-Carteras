-- =============================================
-- Author:		FM
-- Create date: 20140509
-- Description:	TRae Lista ProvCli
-- =============================================
create PROCEDURE [dbo].[_Listar_Entes] 
(
	@codemp as integer
)

AS
BEGIN
	-- Declare the return variable here
	SET NOCOUNT ON;
	SELECT entes_judicial.etj_etjid,   
	provcli.pcl_nomfant
	FROM entes_judicial,   
	provcli
	WHERE  provcli.pcl_codemp = entes_judicial.etj_codemp  and  
	provcli.pcl_pclid = entes_judicial.etj_pclid  and  
	entes_judicial.etj_codemp = @codemp
	UNION
	SELECT entes_judicial.etj_etjid,   
	epl_nombre + ' ' + epl_apepat    
	FROM entes_judicial,   
	empleados
	WHERE  empleados.epl_codemp = entes_judicial.etj_codemp  and  
	empleados.epl_emplid = entes_judicial.etj_emplid  and  
	entes_judicial.etj_codemp = @codemp
	order by pcl_nomfant
  
END
