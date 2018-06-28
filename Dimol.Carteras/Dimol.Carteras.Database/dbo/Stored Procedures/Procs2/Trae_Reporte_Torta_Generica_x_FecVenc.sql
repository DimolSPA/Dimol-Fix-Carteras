

Create Procedure Trae_Reporte_Torta_Generica_x_FecVenc(@ccb_codemp integer, @ccb_pclid integer, @ccb_tipcart integer, @ect_prejud char(1), @idi_idid integer) as
  SELECT  ccb_pclid,   
         pcl_rut,   
         pcl_nombre,   
         ccb_ctcid,   
         ctc_rut,   
         ctc_nomfant,   
         ccb_fecdoc,   
         ccb_fecvenc,   
         ccb_monto,   
         ccb_saldo,
       ccb_numero,
         ccb_tipcambio,
         ccb_ccbid 
          into #Torta   
    FROM cartera_clientes_documentos_cpbt_doc  
   WHERE ( cartera_clientes_documentos_cpbt_doc.ccb_codemp = @ccb_codemp ) AND  
         ( cartera_clientes_documentos_cpbt_doc.ccb_pclid = @ccb_pclid ) AND  
         ( ccb_estcpbt in('F', 'V','J') and tci_idid =  @idi_idid and eci_idid  = @idi_idid and mci_idid =  @idi_idid and ccb_tipcart = @ccb_tipcart and ect_prejud in (@ect_prejud, 'A') and ccb_saldo > 0 )


select rdc_pclid, rdc_ctcid, rdc_ccbid 
    into #RolDoc
	from rol_documentos where rdc_codemp = @ccb_codemp and rdc_pclid = @ccb_pclid


if @ect_prejud = 'P'
   delete from #Torta
   from #Torta, #RolDoc
   where ccb_pclid = rdc_pclid and
         ccb_ctcid = rdc_ctcid and
         ccb_ccbid = rdc_ccbid   

if @ect_prejud = 'J'
   delete from #Torta
   where ccb_estcpbt = 'V'

if @ect_prejud = 'J'
   delete from #Torta
   where ccb_estcpbt = 'V'

if @ect_prejud = 'J'
 delete from #Torta
   where ccb_estcpbt = 'F'  and
         convert(varchar, ccb_pclid) + '_' + convert(varchar, ccb_ctcid) + '_' + convert(varchar, ccb_pclid) not in (
         select convert(varchar, rdc_pclid) + '_' + convert(varchar, rdc_ctcid) + '_' + convert(varchar, rdc_ccbid) from #RolDoc)
       


select * from #Torta
