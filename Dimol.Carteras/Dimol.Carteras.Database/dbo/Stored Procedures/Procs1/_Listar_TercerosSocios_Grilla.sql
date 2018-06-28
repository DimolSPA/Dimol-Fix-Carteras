CREATE PROCEDURE [dbo].[_Listar_TercerosSocios_Grilla]
(
@codemp int,
@pclid int,
@ctcid int,
@where varchar(1000),
@sidx varchar(255),
@sord varchar(10),
@inicio int,
@limite int
)
AS
BEGIN
	SET NOCOUNT ON;
declare @query varchar(7000);
set @query = 'select * from
  (select *,ROW_NUMBER() OVER (ORDER BY ' + @sidx + ' ' + @sord+ ') as row from    
  (	' 
  
set @query = @query + 'select distinct docTerceros.terceroid, docTerceros.rut, docTerceros.Nombre
FROM CARTERA_CLIENTES_CPBT_DOC_TERCEROS docTerceros
JOIN CARTERA_CLIENTES_CPBT_DOC cpbt
ON docTerceros.TERCEROID = cpbt.TERCEROID
  WHERE CCB_CODEMP = '+CONVERT(VARCHAR,@codemp) + '
  AND CCB_PCLID = '+CONVERT(VARCHAR,@pclid) + '
  AND CCB_CTCID = '+CONVERT(VARCHAR,@ctcid)
set @query = @query + ') as tabla  ) as t
  where  row > ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)

if @where is not null
begin
set @query = @query + @where;
end

-- select @query
 exec(@query)	
	

END
