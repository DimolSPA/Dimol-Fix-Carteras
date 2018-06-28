CREATE PROCEDURE [dbo].[_Listar_Clausulas_Grilla_Count]
(
@codemp int,
@idioma int,
@ccl_cctid int,
@where varchar(1000),
@sidx varchar(255),
@sord varchar(10)
)
AS
BEGIN
	SET NOCOUNT ON;

declare @query varchar(7000);
  
set @query = '  select count(ccl_clcid) count from
  (select *,ROW_NUMBER() OVER (ORDER BY ' + @sidx + ' ' + @sord+ ') as row from    
  (' set @query = @query + 'SELECT contratos_cartera_clausulas.ccl_clcid,   
            clausulas_contcart_idiomas.cli_nombre
            FROM contratos_cartera_clausulas,   
            clausulas_contcart_idiomas
            WHERE  contratos_cartera_clausulas.ccl_codemp = clausulas_contcart_idiomas.cli_codemp  and  
            contratos_cartera_clausulas.ccl_clcid = clausulas_contcart_idiomas.cli_clcid  and  
            contratos_cartera_clausulas.ccl_codemp = ' + CONVERT(VARCHAR,@codemp) +'
            and contratos_cartera_clausulas.ccl_cctid = ' + CONVERT(VARCHAR,@ccl_cctid) +'
            and clausulas_contcart_idiomas.cli_idid = ' + CONVERT(VARCHAR,@idioma) +'  
	   		
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
