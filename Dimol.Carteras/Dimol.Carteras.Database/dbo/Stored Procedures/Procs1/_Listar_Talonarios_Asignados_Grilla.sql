CREATE PROCEDURE [dbo].[_Listar_Talonarios_Asignados_Grilla]
(
@codemp int,
@idTal int,
@idSuc int,
@where varchar(1000),
@sidx varchar(255),
@sord varchar(10)
)
AS
BEGIN
	SET NOCOUNT ON;

declare @query varchar(7000);
  
set @query = '  select * from
  (select *,ROW_NUMBER() OVER (ORDER BY ' + @sidx + ' ' + @sord+ ') as row from    
  (' set @query = @query + 'SELECT view_tipos_cpbtdoc_clasificacion.tpc_tpcid as id, 
  view_tipos_cpbtdoc_clasificacion.tpc_nombre as nombre, view_tipos_cpbtdoc_clasificacion.tpc_talonario as tipo
FROM view_tipos_cpbtdoc_clasificacion
WHERE  view_tipos_cpbtdoc_clasificacion.tpc_codemp = ' + CONVERT(VARCHAR,@codemp) +'
          and view_tipos_cpbtdoc_clasificacion.tpc_talonario = ''S''
            and view_tipos_cpbtdoc_clasificacion.tpc_tpcid in   
			(SELECT tipos_cpbtdoc_talonario.tct_tpcid  
            FROM tipos_cpbtdoc_talonario
            WHERE  tipos_cpbtdoc_talonario.tct_codemp = ' + CONVERT(VARCHAR,@codemp) +'
            and tipos_cpbtdoc_talonario.tct_tacid = ' + CONVERT(VARCHAR,@idTal) +'
            and tipos_cpbtdoc_talonario.tct_sucid = ' + CONVERT(VARCHAR,@idSuc) +')
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
