-- =============================================
-- Author:		Rodrigo Garrido
-- Create date: 26-08-2014
-- Description:	Procedimiento para listar categorias para jQgrid
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Categorias]
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
  (' set @query = @query + 'SELECT cat_codemp codemp, cat_catid id, cat_nombre nombre, cat_utilizacion utilizacion 
  from categorias 
  where cat_codemp = '+ CONVERT(VARCHAR,@codemp)+'
   '
   
   set @query = @query +')as tabla ) as t
  where  row > ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)

if @where is not null
begin
set @query = @query + @where;
end

select @query
 exec(@query)	
	

END
