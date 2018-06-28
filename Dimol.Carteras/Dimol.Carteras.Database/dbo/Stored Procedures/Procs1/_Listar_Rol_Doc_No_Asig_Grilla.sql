CREATE PROC [dbo].[_Listar_Rol_Doc_No_Asig_Grilla]      
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

declare @query varchar(7000);          

set @query = '  select * from          
	(select *,ROW_NUMBER() OVER (ORDER BY ' + @sidx + ' ' + @sord+ ') as row from              
	(   
	select * from (

		SELECT
			cc.ccb_ccbid Ccbid,   
			tci.tci_nombre Tipo,   
			cc.ccb_numero Numero,   
			cc.ccb_fecvenc FechaVencimiento, 
			ccb_monto Monto, 
			ccb_saldo Saldo, 
			mon_nombre Moneda,
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
			and cc_ext.NUM_RESOLUCION IS NULL

	) as r
	where 1 = 1 ' 
			
if @where is not null          
begin          
	set @query = @query + @where;          
end   

set @query = @query + ' ) as tabla  ) as t          
where  row > ' + CONVERT(VARCHAR,@inicio) + ' and row <= ' + CONVERT(VARCHAR,@limite)          

exec(@query)
