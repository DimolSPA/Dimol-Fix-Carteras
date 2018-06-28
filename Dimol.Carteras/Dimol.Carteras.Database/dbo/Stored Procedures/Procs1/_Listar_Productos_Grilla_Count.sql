CREATE PROCEDURE [dbo].[_Listar_Productos_Grilla_Count]
(
@codemp int,
@superCategoria int,
@categoria int,
@codigo varchar(255),
@nombre varchar(255),
@estado varchar(255),
@tipo int,
@where varchar(1000),
@sidx varchar(255),
@sord varchar(10)
)
AS
BEGIN
	SET NOCOUNT ON;

declare @query varchar(7000);
  
set @query = '  select count(ID) count from
  (select *,ROW_NUMBER() OVER (ORDER BY ' + @sidx + ' ' + @sord+ ') as row from    
  (' set @query = @query + 'select pdt_prodid as ID, pdt_codfisico as CODIGO, pdt_nombre as NOMBRE
	  ,pdt_estado as ESTADO
	  
	  FROM [dbo].[productos]
	  where pdt_codemp = ' + CONVERT(VARCHAR,@codemp)

	  	
	  if @superCategoria is not null and @superCategoria != 0
		begin
			set @query = @query + ' and pdt_catid in (select scc_catid from supcat_categorias where scc_codemp='+ CONVERT(VARCHAR,@codemp) + ' and scc_spcid='+ CONVERT(VARCHAR,@superCategoria) + ')';
		end
		
	  if @categoria is not null and @categoria != 0
		begin
			set @query = @query + ' and pdt_catid='+ CONVERT(VARCHAR,@categoria);
		end
		
	  if @codigo is not null and @codigo != 0
		begin
			set @query = @query + ' and pdt_codfisico like ''%' +@codigo + '%''';
		end
		
	   if @nombre is not null and @nombre != 0
		begin
			set @query = @query + ' and pdt_nombre like ''%' +@nombre + '%''';
		end
		
		if @estado is not null and @estado != 0
		begin
			set @query = @query + ' and pdt_estado='+ CONVERT(VARCHAR,@estado);
		end

		if @tipo is not null and @tipo != 0
		begin
			set @query = @query + ' and pdt_tipo='+ CONVERT(VARCHAR,@tipo);
		end	
    
   set @query = @query +')as tabla ) as t
  where  row >= 0'

if @where is not null
begin
set @query = @query + @where;
end

select @query
 exec(@query)	
	

END
