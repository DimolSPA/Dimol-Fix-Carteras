-- =============================================
-- Author:		FM
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Rut_Nombre_Cliente] (@texto varchar(200))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
declare @nombre varchar(250) = '%' + @texto + '%'
declare @rut varchar(20) = @texto + '%'
    select PCL_RUT + ' - ' + PCL_NOMFANT, PCL_PCLID from PROVCLI p where p.PCL_NOMFANT like @nombre
    or p.PCL_RUT like @rut
END
