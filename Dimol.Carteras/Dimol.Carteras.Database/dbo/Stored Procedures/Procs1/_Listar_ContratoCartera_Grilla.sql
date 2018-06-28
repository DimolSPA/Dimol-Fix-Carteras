CREATE PROCEDURE [dbo].[_Listar_ContratoCartera_Grilla]
(
@codemp int,
@idioma int,
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
  (' set @query = @query + 'select cct_codemp, cct_cctid, cct_nombre 
	  ,case cct_tipo when ''1'' then ''Masiva''
                when ''2'' then ''Dura''
                end as Tipo, cct_tipo
	  from contratos_cartera  
	  where cct_codemp = ' + CONVERT(VARCHAR,@codemp) +' 		
    '
   set @query = @query +')as tabla ) as t
  where  row >= ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)

if @where is not null
begin
set @query = @query + @where;
end

select @query
 exec(@query)	
	

END
