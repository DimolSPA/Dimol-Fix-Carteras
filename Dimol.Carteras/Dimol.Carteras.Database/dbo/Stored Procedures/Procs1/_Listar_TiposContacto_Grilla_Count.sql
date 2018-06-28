-- =============================================
-- Author:		Rodrigo Garrido
-- Create date: 24-09-2014
-- Description:	Procedimiento para listar giros para jQgrid
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_TiposContacto_Grilla_Count]
(
@codemp int,
@where varchar(1000),
@sidx varchar(255),
@sord varchar(10)
)
AS
BEGIN
	SET NOCOUNT ON;

declare @query varchar(7000);
  
set @query = '  select count(id) count from
  (select *,ROW_NUMBER() OVER (ORDER BY ' + @sidx + ' ' + @sord+ ') as row from    
  (' set @query = @query + 'select tic_codemp codemp, tic_ticid id, tic_nombre nombre
  from tipos_contacto 
  where tic_codemp = '+ CONVERT(VARCHAR,@codemp)+'
   '
   
   set @query = @query +')as tabla ) as t
  where  row >= 0' 

if @where is not null
begin
set @query = @query + @where;
end

select @query
 exec(@query)	
	

END
