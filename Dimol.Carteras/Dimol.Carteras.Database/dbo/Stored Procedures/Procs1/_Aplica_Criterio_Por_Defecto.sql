CREATE PROCEDURE [dbo].[_Aplica_Criterio_Por_Defecto](
--declare
@codemp int,
@pclid int,
@documentoId int
)
AS
BEGIN
SET NOCOUNT ON;
declare @amount decimal(15,2) = isnull((select MONTO_INGRESO from CAJA_RECEPCION_DOCUMENTOS where DOCUMENTO_ID = @documentoId),0);
declare @sql nvarchar(4000), @condicionId int, @criterioId int
DECLARE @Output TABLE (Id INT NOT NULL, Aplica INT NOT NULL);
declare cur cursor STATIC LOCAL READ_ONLY FORWARD_ONLY for 
	SELECT det1.CONDICION_ID Condicion, crifact.CRITERIO_ID,
		'where ' +
		LTRIM(REPLACE(
		REPLACE(
		STUFF((
		SELECT ' and ' + CONVERT(VARCHAR,@amount) + ' ' + det2.OPERADOR + ' ' + CONVERT(VARCHAR,det2.MONTO_COMPARADOR)
		FROM CAJA_CONDICION_FACTURACION_DETALLE det2
		WHERE det2.CONDICION_ID   = det1.CONDICION_ID
      
		FOR XML PATH('')), 2, 3, ''),'&lt;','<'),'&gt;','>')) AS Donde
	FROM CAJA_CRITERIO_FACTURACION crifact
	join CAJA_CONDICION_FACTURACION condicion
	on crifact.CODEMP = condicion.CODEMP
	and crifact.CONDICION_ID = condicion.CONDICION_ID
	join CAJA_CONDICION_FACTURACION_DETALLE det1
	on condicion.CONDICION_ID = det1.CONDICION_ID
	where crifact.CODEMP = @codemp
	and crifact.PCLID = @pclid
	GROUP BY det1.CONDICION_ID,crifact.CRITERIO_ID
open cur
fetch next from cur into @condicionId, @criterioId, @sql
while (@@FETCH_STATUS = 0)
begin
	declare @aplica int = 0
	EXEC @aplica = dbo._Trae_Aplica_Condicion @sql
	--PRINT 'Condicion: ' + Cast(@condicionId AS VARCHAR) +
	--	' Criterio: ' + Cast(@criterioId AS VARCHAR) +
	--	  ' aplica: ' + Cast(@aplica AS VARCHAR)
	INSERT INTO @Output (Id, Aplica)
    VALUES (@criterioId, @aplica);
fetch next from cur into @condicionId, @criterioId, @sql
end

close cur
deallocate cur

SELECT top 1 Id
FROM   @Output where Aplica = 1;

END
