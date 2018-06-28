CREATE PROCEDURE [dbo].[_Listar_Rut_Nombre_Deudor_Caja] (@texto varchar(200))
AS
BEGIN
	SET NOCOUNT ON;
	declare @nombre varchar(250) = '%' + @texto + '%'
	declare @rut varchar(20) = @texto + '%'
	select d.CTC_RUT + ' - ' + d.CTC_NOMFANT, d.CTC_CTCID from DEUDORES d  with (nolock) 
	where d.CTC_NOMFANT like @nombre
		or d.CTC_RUT like @rut
END
