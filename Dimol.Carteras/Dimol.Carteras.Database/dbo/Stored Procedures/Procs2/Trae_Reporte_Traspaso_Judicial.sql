

Create Procedure Trae_Reporte_Traspaso_Judicial(@cbc_codemp integer, @cbc_sucid integer, @cbc_tpcid integer, @desde numeric(15), @hasta numeric(15), @idi_idid integer) as
  SELECT tipos_cpbtdoc_idiomas.tci_nombre,   
         cabacera_comprobantes.cbc_numero,   
         cabacera_comprobantes.cbc_feccpbt,   
         cabacera_comprobantes.cbc_tipcambio,   
         cartera_clientes_documentos_cpbt_doc.pcl_rut,   
         cartera_clientes_documentos_cpbt_doc.pcl_nomfant,   
         cartera_clientes_documentos_cpbt_doc.ctc_rut,   
         cartera_clientes_documentos_cpbt_doc.ctc_nomfant,   
         cartera_clientes_documentos_cpbt_doc.tci_nombre,   
         cartera_clientes_documentos_cpbt_doc.ccb_numero,   
         cartera_clientes_documentos_cpbt_doc.ccb_monto,   
         detalle_comprobantes.dcc_saldo,   
         detalle_comprobantes.dcc_porcfact,   
         cartera_clientes_documentos_cpbt_doc.sbc_rut,   
         cartera_clientes_documentos_cpbt_doc.sbc_nombre,   
         cartera_clientes_documentos_cpbt_doc.pcc_nombre,   
         cartera_clientes_documentos_cpbt_doc.ccb_numesp,
         ccb_codmon,
         mon_nombre,
         cabacera_comprobantes.cbc_final,       
         detalle_comprobantes.dcc_precio,
         cartera_clientes_documentos_cpbt_doc.ccb_fecdoc,   
         cartera_clientes_documentos_cpbt_doc.ccb_fecvenc
    FROM cabacera_comprobantes,   
         detalle_comprobantes,   
         cartera_clientes_documentos_cpbt_doc,   
         tipos_cpbtdoc_idiomas,   
         idiomas  
   WHERE ( detalle_comprobantes.dcc_codemp = cabacera_comprobantes.cbc_codemp ) and  
         ( detalle_comprobantes.dcc_sucid = cabacera_comprobantes.cbc_sucid ) and  
         ( detalle_comprobantes.dcc_tpcid = cabacera_comprobantes.cbc_tpcid ) and  
         ( detalle_comprobantes.dcc_numero = cabacera_comprobantes.cbc_numero ) and  
         ( detalle_comprobantes.dcc_codemp = cartera_clientes_documentos_cpbt_doc.ccb_codemp ) and  
         ( detalle_comprobantes.dcc_pclid = cartera_clientes_documentos_cpbt_doc.ccb_pclid ) and  
         ( detalle_comprobantes.dcc_ctcid = cartera_clientes_documentos_cpbt_doc.ccb_ctcid ) and  
         ( detalle_comprobantes.dcc_ccbid = cartera_clientes_documentos_cpbt_doc.ccb_ccbid ) and  
         ( cabacera_comprobantes.cbc_codemp = tipos_cpbtdoc_idiomas.tci_codemp ) and  
         ( cabacera_comprobantes.cbc_tpcid = tipos_cpbtdoc_idiomas.tci_tpcid ) and  
         ( tipos_cpbtdoc_idiomas.tci_idid = idiomas.idi_idid ) and  
         ( cartera_clientes_documentos_cpbt_doc.tci_idid = idiomas.idi_idid ) and  
         ( cartera_clientes_documentos_cpbt_doc.eci_idid = idiomas.idi_idid ) and  
         ( cartera_clientes_documentos_cpbt_doc.mci_idid = idiomas.idi_idid ) and  
         ( ( cabacera_comprobantes.cbc_codemp = @cbc_codemp ) AND  
         ( cabacera_comprobantes.cbc_sucid = @cbc_sucid ) AND  
         ( cabacera_comprobantes.cbc_tpcid = @cbc_tpcid ) AND  
         ( cabacera_comprobantes.cbc_numero >= @desde ) AND  
         ( cabacera_comprobantes.cbc_numero <= @hasta ) AND  
         ( cabacera_comprobantes.cbt_estado in ( 'A','F' ) ) AND  
         ( idiomas.idi_idid = @idi_idid )   
         )
