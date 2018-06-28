CREATE PROCEDURE [dbo].[_Get_Ganancia_Honorario](
@pclid numeric(15,0),
@diasvencido int,
@region int,
@codigocarga int)
As
BEGIN
		SET NOCOUNT ON;
	declare @query nvarchar(4000),@valor decimal(15,2) = 0;
	set @query = N'SELECT @valor = HONORARIO
	FROM TESORERIA_CRITERIO_GANANCIA_PORCENTAJE 
	WHERE PCLID = '+ CONVERT(VARCHAR,@pclid) + ''
if @region = 6
begin
set @query = @query + ' and CODIGOREGION =' + CONVERT(VARCHAR,@region) + ''
end
else
begin 
set @query = @query + ' and CODIGOREGION is null'
end

if @codigocarga is not null
begin
set @query = @query + ' and CODIGOCARGA =' + CONVERT(VARCHAR,@codigocarga) + ''
end
else
begin 
set @query = @query + ' and CODIGOCARGA is null'
end

if @diasvencido is not null
begin
set @query = @query  + ' and (HASTADIASVENCIDO is not null and ' + CONVERT(VARCHAR,@diasvencido) + ' <=  HASTADIASVENCIDO) and (DESDEDIASVENCIDO is not null and ' + CONVERT(VARCHAR,@diasvencido) + '>= DESDEDIASVENCIDO)'
set @query = @query  + ' or (HASTADIASVENCIDO is null and PCLID = '+ CONVERT(VARCHAR,@pclid) + '' 
	if @region = 6
	begin
	set @query = @query + ' and CODIGOREGION =' + CONVERT(VARCHAR,@region) + ')'
	end
	else
	begin 
	set @query = @query + ' and CODIGOREGION is null)'
	end
end
else
begin
set @query = @query  + ' and (HASTADIASVENCIDO is null and DESDEDIASVENCIDO is null)'
end
EXECUTE sp_executesql @query, N'@valor int OUTPUT', 
		@valor OUTPUT;
	

Return @valor
END
