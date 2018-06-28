-- =============================================
-- Author:		FM
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Rut_Cliente] (@rut varchar(20))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
declare @like varchar(25) = @rut + '%'
    select PCL_RUT,p.PCL_PCLID from PROVCLI p where p.PCL_RUT like @like
END
