-- =============================================
-- Author:		FM
-- Create date: 12-05-2014
-- Description:	Lista regiones segun pais
-- =============================================
CREATE PROCEDURE [dbo].[_Trae_Region] (@codpais int)
AS
BEGIN
	SET NOCOUNT ON;
	Select reg_regid, reg_nombre from region where reg_paiid = @codpais order by reg_orden
END
