

Create Procedure Trae_Detalle_Comprobantes_CartCli(@dcc_codemp integer, @dcc_sucid integer, @dcc_tpcid integer, @dcc_numero numeric(15), @idi_idid integer) as
 SELECT detalle_comprobantes.dcc_item,   
 cartera_clientes_documentos_cpbt_doc.ctc_rut,    
cartera_clientes_documentos_cpbt_doc.ctc_nomfant,   
cartera_clientes_documentos_cpbt_doc.tci_nombre,    
cartera_clientes_documentos_cpbt_doc.ccb_numero,    
cartera_clientes_documentos_cpbt_doc.ccb_fecvenc,    
cartera_clientes_documentos_cpbt_doc.ccb_monto,    
detalle_comprobantes.dcc_precio as Saldo,    
detalle_comprobantes.dcc_interes,    
detalle_comprobantes.dcc_honorario,    
detalle_comprobantes.dcc_gastpre,    
detalle_comprobantes.dcc_gastjud 
FROM detalle_comprobantes,    
cartera_clientes_documentos_cpbt_doc,   
 idiomas 
WHERE  detalle_comprobantes.dcc_pclid = cartera_clientes_documentos_cpbt_doc.ccb_pclid  and   
detalle_comprobantes.dcc_ctcid = cartera_clientes_documentos_cpbt_doc.ccb_ctcid  and   
detalle_comprobantes.dcc_ccbid = cartera_clientes_documentos_cpbt_doc.ccb_ccbid  and   
cartera_clientes_documentos_cpbt_doc.tci_idid = idiomas.idi_idid  and   
cartera_clientes_documentos_cpbt_doc.eci_idid = idiomas.idi_idid  and   
cartera_clientes_documentos_cpbt_doc.mci_idid = idiomas.idi_idid  and   
detalle_comprobantes.dcc_codemp = @dcc_codemp and 
detalle_comprobantes.dcc_sucid =@dcc_sucid and 
detalle_comprobantes.dcc_tpcid = @dcc_tpcid and 
detalle_comprobantes.dcc_numero = @dcc_numero and 
idiomas.idi_idid = @idi_idid 
order by ccb_fecvenc desc
