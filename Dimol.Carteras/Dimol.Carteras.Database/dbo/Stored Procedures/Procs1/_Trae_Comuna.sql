-- =============================================
-- Author:		FM
-- Create date: 12-05-2014
-- Description:	Lista regiones segun pais
-- =============================================
CREATE PROCEDURE [dbo].[_Trae_Comuna] (@ciuid int)
AS
BEGIN
	SET NOCOUNT ON;
	Select com_comid, com_nombre from comuna where com_ciuid = @ciuid order by com_nombre
END
