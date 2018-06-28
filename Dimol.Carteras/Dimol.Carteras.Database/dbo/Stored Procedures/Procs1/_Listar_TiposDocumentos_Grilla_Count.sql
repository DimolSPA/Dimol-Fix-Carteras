CREATE PROCEDURE [dbo].[_Listar_TiposDocumentos_Grilla_Count]
(
@codemp int,
@idid int,
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
  (' set @query = @query + 'SELECT tpc_tpcid Idioma, clasificacion.clb_codigo Codigo
	  ,case [CLB_TIPCPBTDOC] when ''C'' then ''Compra''
                when ''V'' then ''Venta''
                when ''T'' then ''Traspaso''
                when ''A'' then ''Ajuste''
                when ''D'' then ''Documento'' end as Tipo
	  ,[TCI_NOMBRE] Nombre
	  	   
	  FROM [dbo].[CLASIFICACION_CPBTDOC] clasificacion, [dbo].[TIPOS_CPBTDOC] tipos, [dbo].[TIPOS_CPBTDOC_IDIOMAS] tiposIdiomas   
	  where tipos.tpc_codemp = clasificacion.clb_codemp and tipos.tpc_clbid = clasificacion.clb_clbid and
	    tiposIdiomas.tci_codemp = tipos.tpc_codemp and tiposIdiomas.tci_tpcid = tipos.tpc_tpcid and
		clasificacion.clb_codemp = ' + CONVERT(VARCHAR,@codemp) +'
		and tiposIdiomas.tci_idid = ' + CONVERT(VARCHAR,@idid) +'
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
