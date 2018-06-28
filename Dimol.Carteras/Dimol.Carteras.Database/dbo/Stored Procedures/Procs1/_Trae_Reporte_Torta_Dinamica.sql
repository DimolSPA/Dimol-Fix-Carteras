
CREATE Procedure [dbo].[_Trae_Reporte_Torta_Dinamica](
	@ccb_codemp int,
	@ccb_pclid int,
	@ccb_tipcart int,
	@ccb_estcpbt char(1),
	@idi_idid int,
	@cod_ges int,
	@cod_carga int,
	--@dias_vencidos int = null
	@dias_vencidos int
) AS
  DECLARE @sql nvarchar(max)

  SET @sql = '
	  SELECT ccb_pclid, 
			ccb_ctcid,
			ctc_rut,
			ctc_nomfant,   
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
			ccb_saldo + ccb_gastjud + ccb_gastotro + ccb_intereses + ccb_honorarios as TotDeu,
			ccb_compromiso,
			ect_agrupa,
			ect_prejud,
			pcc_codigo, 
			isnull(pcc_nombre, '''') pcc_nombre, 
			(select count(1) from CARTERA_CLIENTES_ESTADOS_ACCIONES WITH (NOLOCK) 
			where CEA_CODEMP = CCB_CODEMP and CEA_PCLID = CCB_PCLID 
			and CEA_CTCID = CCB_CTCID and CONVERT(date, CEA_FECHA) = CONVERT(date, getdate())) acciones,
			(select count(1) from CARTERA_CLIENTES_ESTADOS_HISTORIAL WITH (NOLOCK) 
			where CEH_CODEMP = CCB_CODEMP and CEH_PCLID = CCB_PCLID 
			and CEH_CTCID = CCB_CTCID and CONVERT(date, CEH_FECHA) = CONVERT(date, getdate())) historial,
			DIAS_VENCIDO
			into #Torta   
	  FROM cartera_clientes_documentos_cpbt_doc WITH (NOLOCK) 
	  WHERE ( ccb_codemp = ' + convert(char, @ccb_codemp) + ' ) AND  
			( ccb_pclid = '  + convert(char, @ccb_pclid) + ') AND  
			(
				ccb_estcpbt IN (''V'', ''J'') AND
				tci_idid = ' + convert(char, @idi_idid) + ' AND
				eci_idid = ' + convert(char, @idi_idid) + ' AND 
				mci_idid = ' + convert(char, @idi_idid) + ' AND 
				ccb_tipcart = ' + convert(char, @ccb_tipcart) + '
			) AND 
			ect_agrupa not in (5,6,7)
  ';
  
  IF @dias_vencidos IS NOT NULL
    BEGIN
	  IF @dias_vencidos = 1 
        BEGIN 
		  -- 30 días
          SET @sql = @sql + ' AND DIAS_VENCIDO >= 0 AND DIAS_VENCIDO <= 30'
        END 
      ELSE 
        BEGIN
		  -- Todos los vencidos
          SET @sql = @sql + ' AND DIAS_VENCIDO >= 0'
        END
    END
  
  --print @sql
  --exec sp_executesql @sql
  --exec(@sql)

SET @sql = @sql + '
  if (''' + @ccb_estcpbt + ''' = ''V'' or ''' + @ccb_estcpbt + ''' = ''J'')
	begin
		delete from #Torta where ccb_estcpbt <> ''' + @ccb_estcpbt + ''' 
	end         

DECLARE @cod_carga_AUX int
set @cod_carga_AUX = isnull(' + convert(char, @cod_carga) + ', 0)   

if @cod_carga_AUX <> 0
		begin 
		 select ccb_pclid, 
		ccb_ctcid,
		ctc_rut,
		ctc_nomfant,      
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
		 isnull(pcc_nombre, '''') pcc_nombre,
		 acciones,
		 historial,
		 DIAS_VENCIDO
		 into #temp from #torta where pcc_codigo = @cod_carga_AUX
		 truncate table #torta
		 insert into #torta select * from #temp
		 drop table #temp
		end
else
		begin
			select ccb_pclid, 
			ccb_ctcid,  
			ctc_rut,
			ctc_nomfant,    
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
			 '''' pcc_nombre,
			 acciones,
			 historial,
			 DIAS_VENCIDO
			 into #tmp from #torta 
			 truncate table #torta
			 insert into #torta select * from #tmp
			 drop table #tmp
		end


if '  + convert(char, @ccb_pclid) + ' = 0 and ' + convert(char, @cod_ges) + ' <> 0
	select *, 
	isnull((select top 1 ges_nombre from gestor where ges_gesid = ' + convert(char, @cod_ges) + '), '''') nom_gestor   
	from #Torta ccb
	where ' + convert(char, @cod_ges) + ' in  
		(select top 1 gsc_gesid from gestor_cartera 
		where 
		gsc_pclid = ccb.ccb_pclid and 
		gsc_ctcid = ccb.ccb_ctcid and		
		gsc_GESID is not null)
		
else if '  + convert(char, @ccb_pclid) + ' <> 0 and ' + convert(char, @cod_ges) + ' = 0
	select *, '''' nom_gestor 
	from #Torta where ccb_pclid = '  + convert(char, @ccb_pclid) + '
	
else if '  + convert(char, @ccb_pclid) + ' <> 0 and ' + convert(char, @cod_ges) + ' <> 0
	select *, 
	isnull((select top 1 ges_nombre from gestor where ges_gesid = ' + convert(char, @cod_ges) + '), '''') nom_gestor  
	from #Torta ccb
	where ' + convert(char, @cod_ges) + ' in  
		(select top 1 gsc_gesid from gestor_cartera 
		where 
		gsc_pclid = ccb.ccb_pclid and 
		gsc_ctcid = ccb.ccb_ctcid and		
		gsc_GESID is not null)
		and ccb_pclid = '  + convert(char, @ccb_pclid) + ' 
else
	select *, '''' nom_gestor 
	from #Torta
'

-- print @sql
exec(@sql)