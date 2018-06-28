

Create Procedure Trae_Reporte_Torta_CodigoCliente(@ccb_codemp integer, @ccb_pclid integer, @idi_idid integer, @ccb_tipcart integer) as
  SELECT cartera_clientes_documentos_cpbt_doc.pcl_rut,   
         cartera_clientes_documentos_cpbt_doc.pcl_nomfant,   
         maestra_provcli_estados.mpe_estado,   
         maestra_provcli_estados.mpe_codigo,   
         cartera_clientes_documentos_cpbt_doc.ccb_asignado,   
         cartera_clientes_documentos_cpbt_doc.ccb_monto,   
         cartera_clientes_documentos_cpbt_doc.ccb_saldo,   
         cartera_clientes_documentos_cpbt_doc.sbc_rut,   
         cartera_clientes_documentos_cpbt_doc.sbc_nombre,   
         cartera_clientes_documentos_cpbt_doc.pcc_codigo,   
         cartera_clientes_documentos_cpbt_doc.pcc_nombre,   
         cartera_clientes_documentos_cpbt_doc.ctc_numero,   
         cartera_clientes_documentos_cpbt_doc.ctc_digito,   
         cartera_clientes_documentos_cpbt_doc.ctc_nomfant,
         tci_nombre,
         ccb_numero,
         ccb_codmon,
         ccb_tipcambio   
    FROM cartera_clientes_documentos_cpbt_doc,   
         maestra_provcli_estados,   
         idiomas  
   WHERE ( cartera_clientes_documentos_cpbt_doc.ccb_codemp = maestra_provcli_estados.mpe_codemp ) and  
         ( cartera_clientes_documentos_cpbt_doc.ccb_pclid = maestra_provcli_estados.mpe_pclid ) and  
         ( cartera_clientes_documentos_cpbt_doc.eci_estid = maestra_provcli_estados.mpe_estid ) and  
         ( cartera_clientes_documentos_cpbt_doc.tci_idid = idiomas.idi_idid ) and  
         ( cartera_clientes_documentos_cpbt_doc.eci_idid = idiomas.idi_idid ) and  
         ( cartera_clientes_documentos_cpbt_doc.mci_idid = idiomas.idi_idid ) and  
         ( ( cartera_clientes_documentos_cpbt_doc.ccb_codemp = @ccb_codemp ) AND  
         ( cartera_clientes_documentos_cpbt_doc.ccb_pclid = @ccb_pclid ) AND  
         ( cartera_clientes_documentos_cpbt_doc.ccb_estcpbt <> 'X' ) AND  
         ( idiomas.idi_idid = @idi_idid and ccb_tipcart = @ccb_tipcart )   
         )
