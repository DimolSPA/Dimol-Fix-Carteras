CREATE Procedure [dbo].[Trae_Cartera_Clientes_CpbtDoc](@ccb_codemp integer, @ccb_pclid integer, @ccb_ctcid integer, @ccb_estcpbt char(1), @idi_idid integer) as  

if @ccb_pclid = 424
begin
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
cartera_clientes_documentos_cpbt_doc.pcc_nombre      
FROM cartera_clientes_documentos_cpbt_doc       
WHERE ( cartera_clientes_documentos_cpbt_doc.ccb_codemp = @ccb_codemp ) 
AND             ( cartera_clientes_documentos_cpbt_doc.ccb_pclid = @ccb_pclid ) 
AND             ( cartera_clientes_documentos_cpbt_doc.ccb_ctcid = @ccb_ctcid  
and ccb_estcpbt = @ccb_estcpbt 
and tci_idid =  @idi_idid 
and eci_idid  = @idi_idid 
and mci_idid =  @idi_idid 
and eci_estid > 1 )  
Order by ccb_numero asc
  end 
else if @ccb_pclid = 22
begin
SELECT cartera_clientes_documentos_cpbt_doc.ccb_pclid,              cartera_clientes_documentos_cpbt_doc.ccb_ctcid,              cartera_clientes_documentos_cpbt_doc.pcl_rut,              cartera_clientes_documentos_cpbt_doc.pcl_nombre,              cartera_clientes_documentos_cpbt_doc.tci_nombre,              cartera_clientes_documentos_cpbt_doc.ccb_ccbid,              cartera_clientes_documentos_cpbt_doc.ccb_numero,              cartera_clientes_documentos_cpbt_doc.ccb_fecing,              cartera_clientes_documentos_cpbt_doc.ccb_fecdoc,              cartera_clientes_documentos_cpbt_doc.ccb_fecvenc,              cartera_clientes_documentos_cpbt_doc.ccb_fecultgest,              cartera_clientes_documentos_cpbt_doc.ccb_fecplazo,              cartera_clientes_documentos_cpbt_doc.ccb_feccalcint,              cartera_clientes_documentos_cpbt_doc.ccb_feccast,              cartera_clientes_documentos_cpbt_doc.eci_nombre,              cartera_clientes_documentos_cpbt_doc.ccb_estcpbt,              cartera_clientes_documentos_cpbt_doc.ccb_codmon,              cartera_clientes_documentos_cpbt_doc.ccb_tipcambio,              cartera_clientes_documentos_cpbt_doc.ccb_asignado,              cartera_clientes_documentos_cpbt_doc.ccb_monto,              cartera_clientes_documentos_cpbt_doc.ccb_saldo,              cartera_clientes_documentos_cpbt_doc.ccb_gastjud,              cartera_clientes_documentos_cpbt_doc.ccb_gastotro,              cartera_clientes_documentos_cpbt_doc.ccb_intereses,              cartera_clientes_documentos_cpbt_doc.ccb_honorarios,              cartera_clientes_documentos_cpbt_doc.ccb_calchon,              cartera_clientes_documentos_cpbt_doc.bco_nombre,              cartera_clientes_documentos_cpbt_doc.ccb_rutgir,              cartera_clientes_documentos_cpbt_doc.ccb_nomgir,              cartera_clientes_documentos_cpbt_doc.ccb_comentario,              cartera_clientes_documentos_cpbt_doc.ccb_retent,              cartera_clientes_documentos_cpbt_doc.ccb_numesp,              cartera_clientes_documentos_cpbt_doc.ccb_numagrupa,              cartera_clientes_documentos_cpbt_doc.ccb_carta,              cartera_clientes_documentos_cpbt_doc.ccb_cobrable,              cartera_clientes_documentos_cpbt_doc.ccb_cctid,              cartera_clientes_documentos_cpbt_doc.sbc_rut,              cartera_clientes_documentos_cpbt_doc.sbc_nombre,              cartera_clientes_documentos_cpbt_doc.ccb_docori,              cartera_clientes_documentos_cpbt_doc.ccb_docant,              cartera_clientes_documentos_cpbt_doc.ccb_tipcart ,           datediff(day, ccb_fecvenc, getdate()) as FecVenc,           mon_nombre,            ccb_saldo +   ccb_gastjud +  ccb_gastotro + ccb_intereses + ccb_honorarios as TotDeu,           ccb_compromiso,           cartera_clientes_documentos_cpbt_doc.pcc_nombre      FROM cartera_clientes_documentos_cpbt_doc       WHERE ( cartera_clientes_documentos_cpbt_doc.ccb_codemp = @ccb_codemp ) AND             ( cartera_clientes_documentos_cpbt_doc.ccb_pclid = @ccb_pclid ) AND             ( cartera_clientes_documentos_cpbt_doc.ccb_ctcid = @ccb_ctcid  and ccb_estcpbt = @ccb_estcpbt and tci_idid =  @idi_idid and eci_idid  = @idi_idid and mci_idid =  @idi_idid and eci_estid > 1 )  Order by datediff(day, ccb_fecvenc, getdate()) desc 
end
else
begin
SELECT cartera_clientes_documentos_cpbt_doc.ccb_pclid,              cartera_clientes_documentos_cpbt_doc.ccb_ctcid,              cartera_clientes_documentos_cpbt_doc.pcl_rut,              cartera_clientes_documentos_cpbt_doc.pcl_nombre,              cartera_clientes_documentos_cpbt_doc.tci_nombre,              cartera_clientes_documentos_cpbt_doc.ccb_ccbid,              cartera_clientes_documentos_cpbt_doc.ccb_numero,              cartera_clientes_documentos_cpbt_doc.ccb_fecing,              cartera_clientes_documentos_cpbt_doc.ccb_fecdoc,              cartera_clientes_documentos_cpbt_doc.ccb_fecvenc,              cartera_clientes_documentos_cpbt_doc.ccb_fecultgest,              cartera_clientes_documentos_cpbt_doc.ccb_fecplazo,              cartera_clientes_documentos_cpbt_doc.ccb_feccalcint,              cartera_clientes_documentos_cpbt_doc.ccb_feccast,              cartera_clientes_documentos_cpbt_doc.eci_nombre,              cartera_clientes_documentos_cpbt_doc.ccb_estcpbt,              cartera_clientes_documentos_cpbt_doc.ccb_codmon,              cartera_clientes_documentos_cpbt_doc.ccb_tipcambio,              cartera_clientes_documentos_cpbt_doc.ccb_asignado,              cartera_clientes_documentos_cpbt_doc.ccb_monto,              cartera_clientes_documentos_cpbt_doc.ccb_saldo,              cartera_clientes_documentos_cpbt_doc.ccb_gastjud,              cartera_clientes_documentos_cpbt_doc.ccb_gastotro,              cartera_clientes_documentos_cpbt_doc.ccb_intereses,              cartera_clientes_documentos_cpbt_doc.ccb_honorarios,              cartera_clientes_documentos_cpbt_doc.ccb_calchon,              cartera_clientes_documentos_cpbt_doc.bco_nombre,              cartera_clientes_documentos_cpbt_doc.ccb_rutgir,              cartera_clientes_documentos_cpbt_doc.ccb_nomgir,              cartera_clientes_documentos_cpbt_doc.ccb_comentario,              cartera_clientes_documentos_cpbt_doc.ccb_retent,              cartera_clientes_documentos_cpbt_doc.ccb_numesp,              cartera_clientes_documentos_cpbt_doc.ccb_numagrupa,              cartera_clientes_documentos_cpbt_doc.ccb_carta,              cartera_clientes_documentos_cpbt_doc.ccb_cobrable,              cartera_clientes_documentos_cpbt_doc.ccb_cctid,              cartera_clientes_documentos_cpbt_doc.sbc_rut,              cartera_clientes_documentos_cpbt_doc.sbc_nombre,              cartera_clientes_documentos_cpbt_doc.ccb_docori,              cartera_clientes_documentos_cpbt_doc.ccb_docant,              cartera_clientes_documentos_cpbt_doc.ccb_tipcart ,           datediff(day, ccb_fecvenc, getdate()) as FecVenc,           mon_nombre,            ccb_saldo +   ccb_gastjud +  ccb_gastotro + ccb_intereses + ccb_honorarios as TotDeu,           ccb_compromiso,           cartera_clientes_documentos_cpbt_doc.pcc_nombre      FROM cartera_clientes_documentos_cpbt_doc       WHERE ( cartera_clientes_documentos_cpbt_doc.ccb_codemp = @ccb_codemp ) AND             ( cartera_clientes_documentos_cpbt_doc.ccb_pclid = @ccb_pclid ) AND             ( cartera_clientes_documentos_cpbt_doc.ccb_ctcid = @ccb_ctcid  and ccb_estcpbt = @ccb_estcpbt and tci_idid =  @idi_idid and eci_idid  = @idi_idid and mci_idid =  @idi_idid and eci_estid > 1 )  Order by ccb_fecvenc asc 
end
