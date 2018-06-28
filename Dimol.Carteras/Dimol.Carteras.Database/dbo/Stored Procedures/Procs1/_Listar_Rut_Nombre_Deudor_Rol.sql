-- =============================================
-- Author:		FM
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Rut_Nombre_Deudor_Rol] (@texto varchar(200))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
declare @nombre varchar(250) = '%' + @texto + '%'
declare @rut varchar(20) = @texto + '%'

 select LTRIM(RTRIM(d.CTC_NUMERO))+LTRIM(RTRIM(d.CTC_DIGITO))  + ' - ' + CTC_NOMFANT
			,d.CTC_CTCID 
   from DEUDORES d with (nolock)
   where LTRIM(RTRIM(convert(char,d.CTC_RUT))) like @rut
   or d.CTC_NOMFANT like @nombre

END
