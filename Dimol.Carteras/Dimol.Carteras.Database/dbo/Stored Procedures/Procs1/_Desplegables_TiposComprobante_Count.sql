CREATE PROCEDURE [dbo].[_Desplegables_TiposComprobante_Count]
(
@codemp int,
@idid int
)
AS
BEGIN
	SET NOCOUNT ON;

declare @query varchar(7000);
  
set @query = 'SELECT count(tci_tpcid) 
	  	   
	  FROM clasificacion_cpbtdoc,tipos_cpbtdoc,tipos_cpbtdoc_idiomas
	  WHERE tipos_cpbtdoc.tpc_codemp = clasificacion_cpbtdoc.clb_codemp  and
	    tipos_cpbtdoc.tpc_clbid = clasificacion_cpbtdoc.clb_clbid  and
		tipos_cpbtdoc_idiomas.tci_codemp = tipos_cpbtdoc.tpc_codemp  and
		tipos_cpbtdoc_idiomas.tci_tpcid = tipos_cpbtdoc.tpc_tpcid  and
		tpc_talonario = ''S'' and
		clasificacion_cpbtdoc.clb_codemp = ' + CONVERT(VARCHAR,@codemp) +'
		and tipos_cpbtdoc_idiomas.tci_idid = ' + CONVERT(VARCHAR,@idid) +'
    '
   

select @query
 exec(@query)	
	

END
