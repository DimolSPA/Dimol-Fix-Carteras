-- =============================================
-- Author:		FM
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Rut_Nombre_Asegurado] (@texto varchar(200))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
declare @nombre varchar(250) = '%' + @texto + '%'
declare @rut varchar(20) = @texto + '%'

 select LTRIM(RTRIM(sbc.SBC_RUT)) + ' - ' + sbc.SBC_NOMBRE
			,sbc.SBC_SBCID 
   from SUBCARTERAS sbc  with (nolock)
   where LTRIM(RTRIM(convert(char,sbc.SBC_RUT))) like @rut
   or sbc.SBC_NOMBRE like @nombre
END
