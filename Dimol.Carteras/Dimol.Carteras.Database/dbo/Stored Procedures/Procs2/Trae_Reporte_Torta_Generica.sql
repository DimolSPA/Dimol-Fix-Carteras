

Create Procedure Trae_Reporte_Torta_Generica(@ccb_codemp integer, @ccb_pclid integer, @ccb_tipcart integer, @ect_prejud char(1), @idi_idid integer) as
  SELECT cartera_clientes_documentos_cpbt_doc.ccb_pclid,   
         cartera_clientes_documentos_cpbt_doc.ccb_ctcid,   
         cartera_clientes_documentos_cpbt_doc.pcl_rut,   
         cartera_clientes_documentos_cpbt_doc.pcl_nombre,   
         cartera_clientes_documentos_cpbt_doc.tci_nombre,   
         cartera_clientes_documentos_cpbt_doc.ccb_ccbid,   
         cartera_clientes_documentos_cpbt_doc.ccb_numero,   
         cartera_clientes_documentos_cpbt_doc.ccb_fecing,   
         cartera_clientes_documentos_cpbt_doc.ccb_fecdoc,   
         cartera_clientes_documentos_cpbt_doc.ccb_fecvenc,   
         cartera_clientes_documentos_cpbt_doc.ccb_fecultgest,   
         cartera_clientes_documentos_cpbt_doc.ccb_fecplazo,   
         cartera_clientes_documentos_cpbt_doc.ccb_feccalcint,   
         cartera_clientes_documentos_cpbt_doc.ccb_feccast,   
         cartera_clientes_documentos_cpbt_doc.eci_nombre,   
         cartera_clientes_documentos_cpbt_doc.ccb_estcpbt,   
         cartera_clientes_documentos_cpbt_doc.ccb_codmon,   
         cartera_clientes_documentos_cpbt_doc.ccb_tipcambio,   
         cartera_clientes_documentos_cpbt_doc.ccb_asignado,   
         cartera_clientes_documentos_cpbt_doc.ccb_monto,   
         cartera_clientes_documentos_cpbt_doc.ccb_saldo,   
         cartera_clientes_documentos_cpbt_doc.ccb_gastjud,   
         cartera_clientes_documentos_cpbt_doc.ccb_gastotro,   
         cartera_clientes_documentos_cpbt_doc.ccb_intereses,   
         cartera_clientes_documentos_cpbt_doc.ccb_honorarios,   
         cartera_clientes_documentos_cpbt_doc.ccb_calchon,   
         cartera_clientes_documentos_cpbt_doc.bco_nombre,   
         cartera_clientes_documentos_cpbt_doc.ccb_rutgir,   
         cartera_clientes_documentos_cpbt_doc.ccb_nomgir,   
         cartera_clientes_documentos_cpbt_doc.ccb_comentario,   
         cartera_clientes_documentos_cpbt_doc.ccb_retent,   
         cartera_clientes_documentos_cpbt_doc.ccb_numesp,   
         cartera_clientes_documentos_cpbt_doc.ccb_numagrupa,   
         cartera_clientes_documentos_cpbt_doc.ccb_carta,   
         cartera_clientes_documentos_cpbt_doc.ccb_cobrable,   
         cartera_clientes_documentos_cpbt_doc.ccb_cctid,   
         cartera_clientes_documentos_cpbt_doc.sbc_rut,   
         cartera_clientes_documentos_cpbt_doc.sbc_nombre,   
         cartera_clientes_documentos_cpbt_doc.ccb_docori,   
         cartera_clientes_documentos_cpbt_doc.ccb_docant,   
         cartera_clientes_documentos_cpbt_doc.ccb_tipcart ,
         datediff(day, ccb_fecvenc, getdate()) as FecVenc,
         mon_nombre, 
         ccb_saldo +   ccb_gastjud +  ccb_gastotro + ccb_intereses + ccb_honorarios as TotDeu,
         ccb_compromiso,
         ect_agrupa,
         ect_prejud 
         into #Torta   
    FROM cartera_clientes_documentos_cpbt_doc  
   WHERE ( cartera_clientes_documentos_cpbt_doc.ccb_codemp = @ccb_codemp ) AND  
         ( cartera_clientes_documentos_cpbt_doc.ccb_pclid = @ccb_pclid ) AND  
         ( ccb_estcpbt in('F', 'V','J') and tci_idid =  @idi_idid and eci_idid  = @idi_idid and mci_idid =  @idi_idid and ccb_tipcart = @ccb_tipcart and ect_prejud in( @ect_prejud, 'A') )
         
         


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

if @ect_prejud = 'A'
 delete from #Torta
   where ccb_estcpbt = 'V'  and
         convert(varchar, ccb_pclid) + '_' + convert(varchar, ccb_ctcid) + '_' + convert(varchar, ccb_pclid) not in (
         select convert(varchar, rdc_pclid) + '_' + convert(varchar, rdc_ctcid) + '_' + convert(varchar, rdc_ccbid) from #RolDoc)
       


select * from #Torta
