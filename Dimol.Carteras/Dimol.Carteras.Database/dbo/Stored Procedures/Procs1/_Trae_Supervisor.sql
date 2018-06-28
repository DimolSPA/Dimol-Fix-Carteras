-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 11-04-2014
-- Description:	Trae suérvisor
-- =============================================
create PROCEDURE [dbo].[_Trae_Supervisor](
	@codemp as integer,
	@sucid as integer,
	@gesid as integer 
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
SELECT DISTINCT gestor.ges_gesid
FROM grupos_cobranza,
gestor,
grupo_cobranza_gestor
WHERE  grupos_cobranza.grc_codemp = gestor.ges_codemp  
and grupos_cobranza.grc_sucid = gestor.ges_sucid  
and grupos_cobranza.grc_emplid = gestor.ges_emplid  
and grupo_cobranza_gestor.gcg_codemp = grupos_cobranza.grc_codemp  
and grupo_cobranza_gestor.gcg_sucid = grupos_cobranza.grc_sucid  
and grupo_cobranza_gestor.gcg_grcid = grupos_cobranza.grc_grcid
and grupos_cobranza.grc_codemp = @codemp
and grupos_cobranza.grc_sucid = @sucid
and grupo_cobranza_gestor.gcg_gesid = @gesid
END
