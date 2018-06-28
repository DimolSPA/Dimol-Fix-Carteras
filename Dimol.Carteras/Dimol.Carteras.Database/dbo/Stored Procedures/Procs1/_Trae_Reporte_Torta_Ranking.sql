CREATE Procedure [dbo].[_Trae_Reporte_Torta_Ranking](@ccb_codemp integer, @ccb_pclid integer, @ccb_tipcart integer, @ect_prejud char(1), @idi_idid integer, @rut_cli char(15), @cod_ges int, @estid varchar(8000)) as

SET NOCOUNT ON;

declare @query varchar(8000) = '';

set @query = 'SELECT ccb_pclid,' 
set @query = @query +'ccb_ctcid, '  
set @query = @query +' pcl_rut,'   
set @query = @query +' pcl_nombre,'     
set @query = @query +' ccb_ccbid,' 
set @query = @query +' eci_nombre,'   
set @query = @query +' ccb_estcpbt,'   
set @query = @query +' ccb_codmon,'   
set @query = @query +' ccb_tipcambio,'   
set @query = @query +' ccb_asignado,'   
set @query = @query +' ccb_monto,'   
set @query = @query +' ccb_saldo, ccb_tipcart, mon_nombre,' 
set @query = @query +' ccb_saldo +   ccb_gastjud +  ccb_gastotro + ccb_intereses + ccb_honorarios as TotDeu,'
set @query = @query +' ccb_compromiso,'
set @query = @query +' ect_agrupa,'
set @query = @query +' ect_prejud '
set @query = @query +' into #Torta '  
set @query = @query +' FROM cartera_clientes_documentos_cpbt_doc WITH (NOLOCK) '
set @query = @query +' WHERE ( ccb_codemp = 1 ) AND ' 
set @query = @query +' ( ccb_pclid = 318 ) AND ' 
set @query = @query +' ( ccb_estcpbt in(''F'', ''V'',''J'') and tci_idid =  1 and eci_idid  = 1 and mci_idid =  1 and ccb_tipcart = 1 and ect_prejud in( ''P'', ''A'') )	AND '
set @query = @query +' ect_agrupa not in (5,6,7) '

if @estid <> ''
	begin
		set @query = @query +' and eci_estid in (' + @estid + ') '  
	end	

set @query = @query +'select rdc_pclid, rdc_ctcid, rdc_ccbid '
set @query = @query +'into #RolDoc '
set @query = @query +'from rol_documentos WITH (NOLOCK) '
set @query = @query +'where rdc_codemp = ' + convert(varchar, @ccb_codemp) + ' and rdc_pclid = ' + convert(varchar, @ccb_pclid)

set @query = @query +' if ''' + @ect_prejud + ''' = ''P'' '
set @query = @query +'delete from #Torta '
set @query = @query +'from #Torta, #RolDoc '
set @query = @query +'where ccb_pclid = rdc_pclid and '
set @query = @query +'ccb_ctcid = rdc_ctcid and '
set @query = @query +'ccb_ccbid = rdc_ccbid '  

set @query = @query +' if ''' + @ect_prejud + ''' = ''J'' '
set @query = @query +'delete from #Torta '
set @query = @query +'where ccb_estcpbt = ''V'' '

set @query = @query +'if ''' + @ect_prejud + ''' = ''J'' '
set @query = @query +'delete from #Torta '
set @query = @query +'where ccb_estcpbt = ''F''  and  convert(varchar, ccb_pclid) + ''_'' + convert(varchar, ccb_ctcid) + ''_'' + convert(varchar, ccb_ccbid) not in ('
set @query = @query + ' select convert(varchar, rdc_pclid) + ''_'' + convert(varchar, rdc_ctcid) + ''_'' + convert(varchar, rdc_ccbid) from #RolDoc)'

 set @rut_cli = isnull(@rut_cli , '')       

set @query = @query + ' if ''' + @rut_cli + ''' = ''''  and ' + convert(varchar,@cod_ges) + ' <> 0 '
set @query = @query + ' select * from #Torta ccb '
set @query = @query + ' where ' + convert(varchar, @cod_ges) + ' in  '
set @query = @query + ' (select top 1 gsc_gesid from gestor_cartera '
set @query = @query + ' where '
set @query = @query + ' gsc_pclid = ccb.ccb_pclid and '
set @query = @query + ' gsc_ctcid = ccb.ccb_ctcid and '		
set @query = @query + ' gsc_GESID is not null ) '
		
set @query = @query + ' else if ''' + @rut_cli + ''' <> '''' and ' + convert(varchar, @cod_ges) + ' = 0 '
set @query = @query + ' select * from #Torta where pcl_rut = ''' + @rut_cli + ''''
	
set @query = @query + ' else if ''' + @rut_cli + ''' <> '''' and ' + convert(varchar, @cod_ges) + ' <> 0 '
set @query = @query + ' select * from #Torta ccb '
set @query = @query + ' where ' + convert(varchar, @cod_ges) + ' in ' 
set @query = @query + ' (select top 1 gsc_gesid from gestor_cartera '
set @query = @query + ' where '
set @query = @query + ' gsc_pclid = ccb.ccb_pclid and '
set @query = @query + ' gsc_ctcid = ccb.ccb_ctcid and '		
set @query = @query + ' gsc_GESID is not null ) '
set @query = @query + ' and pcl_rut = ''' + @rut_cli + ''''
set @query = @query + ' else '
set @query = @query + ' select * from #Torta '

exec (@query)
