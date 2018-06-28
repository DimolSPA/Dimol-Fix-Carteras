-- =============================================
-- Author:		FM
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Nombre_Cliente] (@nombre varchar(200))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
declare @like varchar(250) = '%' + @nombre + '%'
    select PCL_NOMFANT, PCL_PCLID from PROVCLI p where p.PCL_NOMFANT like @like
END
