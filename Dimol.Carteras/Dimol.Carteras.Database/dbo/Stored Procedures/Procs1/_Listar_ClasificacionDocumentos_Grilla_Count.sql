CREATE PROCEDURE [dbo].[_Listar_ClasificacionDocumentos_Grilla_Count]
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
  
set @query = '  select count(Codigo) count from
  (select *,ROW_NUMBER() OVER (ORDER BY ' + @sidx + ' ' + @sord+ ') as row from    
  (' set @query = @query + 'SELECT clb_clbid Idioma, clb_codigo Codigo
	  ,case [CLB_TIPCPBTDOC] when ''C'' then ''Compra''
                when ''V'' then ''Venta''
                when ''T'' then ''Traspaso''
                when ''A'' then ''Ajuste''
                when ''D'' then ''Documento'' end as Tipo
	  	  	   
	  FROM [dbo].[CLASIFICACION_CPBTDOC] clasificacion   
	  where 
		clb_codemp = ' + CONVERT(VARCHAR,@codemp) +'
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
