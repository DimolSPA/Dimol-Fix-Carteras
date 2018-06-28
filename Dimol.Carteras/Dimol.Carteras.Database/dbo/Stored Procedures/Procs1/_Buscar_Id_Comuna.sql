-- =============================================
-- Author:		FM
-- Create date: 12-05-2014
-- Description:	Lista regiones segun pais
-- =============================================
create PROCEDURE [dbo].[_Buscar_Id_Comuna] (@nombre varchar(200))
AS
BEGIN
	SET NOCOUNT ON;
	Select com_comid from comuna where COM_NOMBRE = UPPER(@nombre) order by com_nombre
END
