-- =============================================
-- Author:		FM
-- Create date: 20140509
-- Description:	TRae Tipos Documento Caja
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_CasoJudicial] 
(
	@codemp as integer,
	@ccb_pclid as integer, 
	@ccb_ctcid as integer,
	@ccb_ccbid as integer
)

AS
BEGIN
	-- Declare the return variable here
	SET NOCOUNT ON;
	select count(rdc_pclid) 
	from rol_documentos 
	where rdc_codemp= @codemp
    and rdc_pclid = @ccb_pclid
    and rdc_ctcid= @ccb_ctcid
    and rdc_ccbid= @ccb_ccbid
		   
END
