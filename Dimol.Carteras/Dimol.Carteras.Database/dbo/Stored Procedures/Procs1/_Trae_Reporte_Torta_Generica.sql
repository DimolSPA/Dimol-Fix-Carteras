CREATE Procedure [dbo].[_Trae_Reporte_Torta_Generica](@ccb_codemp integer, @ccb_pclid integer, @ccb_tipcart integer, @ccb_estcpbt char(1), @idi_idid integer, @cod_ges int, @cod_carga int) as

  SELECT ccb_pclid, 
		ccb_ctcid,   
         pcl_rut,   
         pcl_nombre,     
         ccb_ccbid, 
         eci_nombre,   
         ccb_estcpbt,   
         ccb_codmon,   
         ccb_tipcambio,   
         ccb_asignado,   
         ccb_monto,   
         ccb_saldo,    
         ccb_tipcart,
         mon_nombre, 
         ccb_saldo +   ccb_gastjud +  ccb_gastotro + ccb_intereses + ccb_honorarios as TotDeu,
         ccb_compromiso,
         ect_agrupa,
         ect_prejud,
		 pcc_codigo, 
		 isnull(pcc_nombre, '') pcc_nombre  
         into #Torta   
    FROM cartera_clientes_documentos_cpbt_doc WITH (NOLOCK) 
   WHERE ( ccb_codemp = @ccb_codemp ) AND  
         ( ccb_pclid = @ccb_pclid ) AND  
         ( ccb_estcpbt in('V','J') and tci_idid =  @idi_idid and eci_idid  = @idi_idid and mci_idid =  @idi_idid and ccb_tipcart = @ccb_tipcart )	AND 
		 ect_agrupa not in (5,6,7)

		 		 
	if (@ccb_estcpbt = 'V' or @ccb_estcpbt = 'J')
		begin
			delete from #Torta where ccb_estcpbt <> @ccb_estcpbt 
		end   
         
set @cod_carga = isnull(@cod_carga, 0)   

if @cod_carga <> 0
		begin 
		 select ccb_pclid, 
		ccb_ctcid,   
         pcl_rut,   
         pcl_nombre,     
         ccb_ccbid, 
         eci_nombre,   
         ccb_estcpbt,   
         ccb_codmon,   
         ccb_tipcambio,   
         ccb_asignado,   
         ccb_monto,   
         ccb_saldo,    
         ccb_tipcart,
         mon_nombre, 
         TotDeu,
         ccb_compromiso,
         ect_agrupa,
         ect_prejud,
		 pcc_codigo, 
		 isnull(pcc_nombre, '') pcc_nombre 
		 into #temp from #torta where pcc_codigo = @cod_carga
		 truncate table #torta
		 insert into #torta select * from #temp
		 drop table #temp
		end
else
		begin
			select ccb_pclid, 
			ccb_ctcid,   
			 pcl_rut,   
			 pcl_nombre,     
			 ccb_ccbid, 
			 eci_nombre,   
			 ccb_estcpbt,   
			 ccb_codmon,   
			 ccb_tipcambio,   
			 ccb_asignado,   
			 ccb_monto,   
			 ccb_saldo,    
			 ccb_tipcart,
			 mon_nombre, 
			 TotDeu,
			 ccb_compromiso,
			 ect_agrupa,
			 ect_prejud,
			 pcc_codigo, 
			 '' pcc_nombre 
			 into #tmp from #torta 
			 truncate table #torta
			 insert into #torta select * from #tmp
			 drop table #tmp
		end


if @ccb_pclid = 0 and @cod_ges <> 0
	select *, 
	isnull((select top 1 ges_nombre from gestor where ges_gesid = @cod_ges), '') nom_gestor   
	from #Torta ccb
	where @cod_ges in  
		(select top 1 gsc_gesid from gestor_cartera 
		where 
		gsc_pclid = ccb.ccb_pclid and 
		gsc_ctcid = ccb.ccb_ctcid and		
		gsc_GESID is not null)
		
else if @ccb_pclid <> 0 and @cod_ges = 0
	select *, '' nom_gestor 
	from #Torta where ccb_pclid = @ccb_pclid
	
else if @ccb_pclid <> 0 and @cod_ges <> 0
	select *, 
	isnull((select top 1 ges_nombre from gestor where ges_gesid = @cod_ges), '') nom_gestor  
	from #Torta ccb
	where @cod_ges in  
		(select top 1 gsc_gesid from gestor_cartera 
		where 
		gsc_pclid = ccb.ccb_pclid and 
		gsc_ctcid = ccb.ccb_ctcid and		
		gsc_GESID is not null)
		and ccb_pclid = @ccb_pclid 
else
	select *, '' nom_gestor 
	from #Torta
