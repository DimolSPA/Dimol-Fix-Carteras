CREATE PROCEDURE [dbo].[_Listar_Rut_Nombre_Tipo_Cliente] (@texto varchar(200),@codemp int)
AS
BEGIN
	
	SET NOCOUNT ON;
declare @nombre varchar(250) = '%' + @texto + '%'
declare @rut varchar(20) = @texto + '%'
    select PCL_RUT + ' - ' + PCL_NOMFANT, PCL_PCLID 
	from PROVCLI p with(nolock)
	where p.PCL_CODEMP = @codemp 
	and p.PCL_TPCID IN (1,3)
	and  p.PCL_NOMFANT like @nombre
    or p.PCL_RUT like @rut
END
