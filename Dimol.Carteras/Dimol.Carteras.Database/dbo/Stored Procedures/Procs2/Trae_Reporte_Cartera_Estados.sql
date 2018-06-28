

Create Procedure Trae_Reporte_Cartera_Estados(@ccb_codemp integer, @ccb_pclid integer, @ccb_tipcart smallint, @eci_estid integer, @idi_idid integer) as
  SELECT cartera_clientes_documentos_cpbt_doc.pcl_rut,   
         cartera_clientes_documentos_cpbt_doc.pcl_nomfant,   
         cartera_clientes_documentos_cpbt_doc.tci_nombre,   
         cartera_clientes_documentos_cpbt_doc.ccb_numero,   
         cartera_clientes_documentos_cpbt_doc.ccb_fecing,   
         cartera_clientes_documentos_cpbt_doc.ccb_fecdoc,   
         cartera_clientes_documentos_cpbt_doc.ccb_fecvenc,   
         cartera_clientes_documentos_cpbt_doc.ctc_numero,   
         cartera_clientes_documentos_cpbt_doc.ctc_digito,   
         cartera_clientes_documentos_cpbt_doc.ctc_nomfant,   
         cartera_clientes_documentos_cpbt_doc.eci_nombre,   
         cartera_clientes_documentos_cpbt_doc.ccb_asignado,   
         cartera_clientes_documentos_cpbt_doc.ccb_monto,   
         cartera_clientes_documentos_cpbt_doc.ccb_saldo,   
         cartera_clientes_documentos_cpbt_doc.pcc_nombre,   
         datediff("d", ccb_fecvenc, getdate()) as DiasVenc  
    FROM cartera_clientes_documentos_cpbt_doc,   
         idiomas  
   WHERE ( cartera_clientes_documentos_cpbt_doc.tci_idid = idiomas.idi_idid ) and  
         ( cartera_clientes_documentos_cpbt_doc.eci_idid = idiomas.idi_idid ) and  
         ( cartera_clientes_documentos_cpbt_doc.mci_idid = idiomas.idi_idid ) and  
         ( ( cartera_clientes_documentos_cpbt_doc.ccb_codemp = @ccb_codemp ) AND  
         ( cartera_clientes_documentos_cpbt_doc.ccb_pclid = @ccb_pclid ) AND  
         ( cartera_clientes_documentos_cpbt_doc.ccb_tipcart = @ccb_tipcart ) AND  
         ( cartera_clientes_documentos_cpbt_doc.ccb_estcpbt in ( 'V','J' ) ) AND  
         ( cartera_clientes_documentos_cpbt_doc.eci_estid = @eci_estid ) AND  
         ( idiomas.idi_idid = @idi_idid )   
         )
