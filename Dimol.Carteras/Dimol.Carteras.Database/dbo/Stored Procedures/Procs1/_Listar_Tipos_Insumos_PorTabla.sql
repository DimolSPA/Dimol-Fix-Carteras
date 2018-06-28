CREATE PROCEDURE [dbo].[_Listar_Tipos_Insumos_PorTabla]
(
@codemp int,
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
  
set @query = '  select * from
  (select *,ROW_NUMBER() OVER (ORDER BY ' + @sidx + ' ' + @sord+ ') as row from    
  (' set @query = @query + 'select tii_tipid as ID, TII_NOMBRE as NOMBRE
	  FROM [dbo].[tipos_insumo_idiomas]
	  where tii_codemp = ' + CONVERT(VARCHAR,@codemp)

   set @query = @query +')as tabla ) as t
  where  row >= ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)

if @where is not null
begin
set @query = @query + @where;
end

select @query
 exec(@query)	
	

END
