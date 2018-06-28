CREATE PROCEDURE [dbo].[_Listar_ContratoCartera_Grilla_Count]
(
@codemp int,
@idioma int,
@where varchar(1000),
@sidx varchar(255),
@sord varchar(10)
)
AS
BEGIN
	SET NOCOUNT ON;

declare @query varchar(7000);
  
set @query = '  select count(cct_cctid) count from
  (select *,ROW_NUMBER() OVER (ORDER BY ' + @sidx + ' ' + @sord+ ') as row from    
  (' set @query = @query + 'select cct_cctid, cct_nombre 
	  ,case cct_tipo when ''1'' then ''Masiva''
                when ''2'' then ''Dura''
                end as Tipo, cct_tipo
	  from contratos_cartera  
	  where cct_codemp = ' + CONVERT(VARCHAR,@codemp) +'
		
    '
   set @query = @query +')as tabla ) as t
  where  row > 0 '

if @where is not null
begin
set @query = @query + @where;
end

select @query
 exec(@query)	
	

END
