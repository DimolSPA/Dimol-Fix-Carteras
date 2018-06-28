CREATE PROC [dbo].[_Listar_Rol_Doc_Asig_Grilla_Count_GroupByResolucion]
(          
	@codemp int,
	@idid int,
	@rolid int,
	@where varchar(1000),
	@sidx varchar(255),
	@sord varchar(10),
	@inicio int,
	@limite int
)
AS

DECLARE @query varchar(7000);

SET @query = 
	 '  select count(*) count from          
		(select *,ROW_NUMBER() OVER (ORDER BY Tipo asc) as row from              
		(   
			SELECT Resolucion, FechaResolucion, SUM(Monto) AS Monto, SUM(Saldo) AS Saldo FROM (
				SELECT
					rd.rdc_monto Monto,
					rd.rdc_saldo Saldo,
					cc_ext.FEC_RESOLUCION as FechaResolucion,
					cc_ext.NUM_RESOLUCION as Resolucion
	
				FROM rol_documentos rd

				INNER JOIN cartera_clientes_cpbt_doc cc on
					cc.ccb_codemp = rd.rdc_codemp 
					AND cc.ccb_pclid = rd.rdc_pclid 
					AND cc.ccb_ctcid = rd.rdc_ctcid 
					AND cc.ccb_ccbid = rd.rdc_ccbid

				LEFT JOIN CARTERA_CLIENTES_CPBT_DOC_EXTENDIDO cc_ext on 
					cc.ccb_codemp = cc_ext.CCB_CODEMP 
					AND cc.ccb_pclid = cc_ext.CCB_PCLID 
					AND cc.ccb_ctcid = cc_ext.CCB_CTCID 
					AND cc.ccb_ccbid = cc_ext.CCB_CCBID
	
				INNER JOIN tipos_cpbtdoc_idiomas tci on
					cc.ccb_codemp = tci.tci_codemp 
					AND cc.ccb_tpcid = tci.tci_tpcid
	
				INNER JOIN monedas mon on 
					cc.ccb_codmon = mon.MON_CODEMP
					AND cc.ccb_codmon = mon.MON_CODMON
			
				WHERE
					rdc_codemp ='  + CONVERT(VARCHAR,@codemp) +' and
					tci_idid ='  + CONVERT(VARCHAR,@idid) +' and 
					rdc_rolid = '  + CONVERT(VARCHAR,@rolid) +'  
			) as r
			WHERE Resolucion IS NOT NULL
			GROUP by Resolucion, FechaResolucion ' 

if @where is not null
begin
	set @query = @query + @where;
end
  
set @query = @query + ' ) as tabla  ) as t          
where  row > 0'         

exec(@query)
