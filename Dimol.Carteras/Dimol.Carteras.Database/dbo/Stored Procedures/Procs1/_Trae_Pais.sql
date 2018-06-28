-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[_Trae_Pais]
AS
BEGIN
	SET NOCOUNT ON;
	Select pai_paiid, pai_nombre from pais order by pai_nombre
END
