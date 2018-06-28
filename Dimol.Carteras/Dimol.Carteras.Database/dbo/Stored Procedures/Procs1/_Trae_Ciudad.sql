-- =============================================
-- Author:		FM
-- Create date: 12-05-2014
-- Description:	Lista regiones segun pais
-- =============================================
CREATE PROCEDURE [dbo].[_Trae_Ciudad] (@regid int)
AS
BEGIN
	SET NOCOUNT ON;
	Select ciu_ciuid, ciu_nombre from ciudad where ciu_regid = @regid order by ciu_nombre
END
