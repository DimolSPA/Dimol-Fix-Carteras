CREATE PROCEDURE [dbo].[_Listar_ClausulasContratoCartera_Grilla_Count]
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
  
set @query = '  select count(clc_clcid) count from
  (select *,ROW_NUMBER() OVER (ORDER BY ' + @sidx + ' ' + @sord+ ') as row from    
  (' set @query = @query + 'select clc_clcid, clc_nombre 
	  ,case [clc_tipo] when ''1'' then ''Sin Etiqueta''
                when ''2'' then ''Sin Etiqueta''
                when ''3'' then ''Sin Etiqueta''
                when ''4'' then ''Sin Etiqueta''
                when ''5'' then ''Sin Etiqueta''
				when ''6'' then ''Sin Etiqueta'' end as Tipo, clc_tipo
	  ,case [clc_prejud] when ''P'' then ''Prejudicial''
                when ''J'' then ''Judicial''
                when ''A'' then ''Ambas'' end as Area
	  from clausulas_contcart 
	  where clc_codemp = ' + CONVERT(VARCHAR,@codemp) +' 
		
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
