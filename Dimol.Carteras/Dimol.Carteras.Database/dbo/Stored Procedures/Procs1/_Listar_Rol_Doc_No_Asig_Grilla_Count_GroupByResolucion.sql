CREATE PROC [dbo].[_Listar_Rol_Doc_No_Asig_Grilla_Count_GroupByResolucion]
(          
	@codemp int,
	@idid int,
	@pclid int,
	@ctcid int,
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
			ccb_monto Monto, 
			ccb_saldo Saldo, 
			cc_ext.FEC_RESOLUCION as FechaResolucion,
			cc_ext.NUM_RESOLUCION as Resolucion
		
			FROM cartera_clientes_cpbt_doc cc
		
			LEFT JOIN CARTERA_CLIENTES_CPBT_DOC_EXTENDIDO cc_ext on 
				  cc.ccb_codemp = cc_ext.CCB_CODEMP 
				  AND cc.ccb_pclid = cc_ext.CCB_PCLID 
				  AND cc.ccb_ctcid = cc_ext.CCB_CTCID 
				  AND cc.ccb_ccbid = cc_ext.CCB_CCBID

			INNER JOIN tipos_cpbtdoc_idiomas tci on
					cc.ccb_codemp = tci.tci_codemp  and  
					cc.ccb_tpcid = tci.tci_tpcid
			INNER JOIN monedas mon on
					cc.ccb_codemp = mon.mon_codemp  and  
					cc.ccb_codmon = mon.mon_codmon
				
			WHERE
				cc.ccb_codemp =  '  + CONVERT(VARCHAR,@codemp) +'
				and cc.ccb_pclid =  '  + CONVERT(VARCHAR,@pclid) +'
				and cc.ccb_ctcid =  '  + CONVERT(VARCHAR,@ctcid) +'
				and cc.ccb_estcpbt = ''J'' 
				and tci_idid = '  + CONVERT(VARCHAR,@idid) +'
				and cc.ccb_ccbid not in (
					SELECT rol_documentos.rdc_ccbid  
					FROM rol_documentos
					WHERE  rol_documentos.rdc_codemp =  '  + CONVERT(VARCHAR,@codemp) +'
					and rol_documentos.rdc_pclid =  '  + CONVERT(VARCHAR,@pclid) +'
					and rol_documentos.rdc_ctcid =  '  + CONVERT(VARCHAR,@ctcid ) +'
				)
			
		) as r
		WHERE r.Resolucion IS NOT NULL
		GROUP by r.Resolucion, r.FechaResolucion' 

if @where is not null
begin
	set @query = @query + @where;
end
  
set @query = @query + ' ) as tabla  ) as t          
where  row > 0'         

exec(@query)
