CREATE procedure [dbo].[_Trae_Cuenta_O_Ejecutivo] (@ejecutivo int, @cuenta int, @pclid int) as
SET NOCOUNT ON;

declare @query varchar(8000) = '';
set @query = 'SELECT C.ID_CUENTA_EJECUTIVO, C.CUENTA, C.ID_TIPO_BANCO, B.NOMBRE, E.EMAIL, E.OFICINA ' 
set @query = @query + 'from EJECUTIVO_MUTUAL E  with (nolock) '
set @query = @query + 'LEFT JOIN EJECUTIVO_CUENTA_MUTUAL C  with (nolock) '
set @query = @query + 'ON C.ID_EJECUTIVO = E.ID_EJECUTIVO '
set @query = @query + 'LEFT JOIN TIPO_BANCO B  with (nolock) '
set @query = @query + 'ON B.ID_TIPO_BANCO = C.ID_TIPO_BANCO '
set @query = @query + 'LEFT JOIN PROVCLI_EJECUTIVO P with (nolock) '
set @query = @query + 'ON P.ID_EJECUTIVO = E.ID_EJECUTIVO '
set @query = @query + 'WHERE P.PCLID = ' + convert(varchar, @pclid) 

if @ejecutivo > 0
	begin
		set @query = @query + 'AND E.ID_EJECUTIVO = ' + convert(varchar, @ejecutivo) 
	end

if @cuenta > 0 
	begin
		set @query = @query + 'AND C.ID_CUENTA_EJECUTIVO = ' + convert(varchar, @cuenta) 
	end

exec (@query)
