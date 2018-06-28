CREATE PROCEDURE [dbo].[_Listar_Nombre_Gestor] (@texto varchar(200))
AS
BEGIN
	SET NOCOUNT ON;
declare @nombre varchar(250) = '%' + @texto + '%'


 select g.GES_NOMBRE
			,g.GES_GESID
   from GESTOR g  with (nolock)
   where g.GES_NOMBRE like @nombre
END
