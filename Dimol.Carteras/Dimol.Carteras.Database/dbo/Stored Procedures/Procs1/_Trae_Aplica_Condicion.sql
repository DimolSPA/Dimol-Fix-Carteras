CREATE proc [dbo].[_Trae_Aplica_Condicion](
@sqlEntrada nvarchar(4000)
)
as
BEGIN
declare @sql nvarchar(4000), @valorRango   int

SET @sql = N'SELECT @valorRango = count(1) '  + @sqlEntrada + '' 
		

EXECUTE sp_executesql @sql, N'@valorRango int OUTPUT', 
		@valorRango OUTPUT;
	

RETURN @valorRango


END
