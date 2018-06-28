CREATE PROCEDURE [dbo].[_Listar_TiposReporte_Grilla]
(
@codemp int,
@idid int,
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
  (' set @query = @query + 'SELECT [TPC_CODEMP] Codemp        
	  ,[TRC_TRCID] Idioma
	  ,[TRC_TPCID] Id         
	  ,[CLB_CODIGO] Codigo
	  ,case [CLB_TIPCPBTDOC] when ''C'' then ''Compra''
                when ''V'' then ''Venta''
                when ''T'' then ''Traspaso''
                when ''A'' then ''Ajuste''
                when ''D'' then ''Documento'' end as Tipo
	  ,[TRC_NOMBRE] Nombre
	  ,[TRC_REPORTE] Reporte
	  ,[dbo].[_Trae_Agrupa_TiposReporte] ([TPC_TPCID],[TRC_TRCID],[TPC_CODEMP]) Agrupa
	  ,[TRC_REPPAD] ReportePadre
	  ,CONVERT(varchar(5), TRC_TRCID) + CONVERT(varchar(5), TRC_TPCID) IdTiposReporte 
	  FROM [dbo].[CLASIFICACION_CPBTDOC] clasificacion, [dbo].[TIPOS_CPBTDOC] tipos, [dbo].[TIPOS_CPBTDOC_IDIOMAS] tiposIdiomas, [dbo].[TIPOS_CPBTDOC_REPORT] tiposReportes   
	  where tipos.tpc_codemp = clasificacion.clb_codemp and tipos.tpc_clbid = clasificacion.clb_clbid and
	    tiposIdiomas.tci_codemp = tipos.tpc_codemp and tiposIdiomas.tci_tpcid = tipos.tpc_tpcid and
		trc_codemp = tipos.tpc_codemp and trc_tpcid = tipos.tpc_tpcid and clasificacion.clb_codemp = ' + CONVERT(VARCHAR,@codemp) +'
		and tiposIdiomas.tci_idid = ' + CONVERT(VARCHAR,@idid) +'
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
