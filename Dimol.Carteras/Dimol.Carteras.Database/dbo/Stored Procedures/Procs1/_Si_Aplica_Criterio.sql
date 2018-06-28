CREATE PROCEDURE [dbo].[_Si_Aplica_Criterio](
--declare
@documentoId int,
@criterioId int
)
as
BEGIN

declare @amount decimal(15,2) = 0,
@sqlEntrada nvarchar(4000), @condicionId int = 0;
set @condicionId =isnull((select CONDICION_ID from CAJA_CRITERIO_FACTURACION where CRITERIO_ID = @criterioId),0);
set @amount = (select MONTO_INGRESO from CAJA_RECEPCION_DOCUMENTOS where DOCUMENTO_ID = @documentoId);

if (@condicionId > 0) --Existen condiciones que aplican
begin
set @sqlEntrada =
(SELECT 
	'where ' +
	LTRIM(REPLACE(
	REPLACE(
	STUFF((
	SELECT ' and ' + CONVERT(VARCHAR,@amount) + ' ' + t2.OPERADOR + ' ' + CONVERT(VARCHAR,t2.MONTO_COMPARADOR)
	FROM CAJA_CONDICION_FACTURACION_DETALLE t2
	WHERE t2.CONDICION_ID   = t1.CONDICION_ID
      
	FOR XML PATH('')), 2, 3, ''),'&lt;','<'),'&gt;','>')) AS Donde
FROM CAJA_CONDICION_FACTURACION_DETALLE t1
where t1.CONDICION_ID = @condicionId
GROUP BY t1.CONDICION_ID)

declare @sql nvarchar(4000), @valorRango   int

SET @sql = N'SELECT @valorRango = count(1) '  + @sqlEntrada + '' 
		
--print @sql
EXECUTE sp_executesql @sql, N'@valorRango int OUTPUT', 
		@valorRango OUTPUT;
	
select  @valorRango result --- 0 No cumple, 1 Si cumple
end
else
begin
select 2 result -- No tiene condiciones
end
END
