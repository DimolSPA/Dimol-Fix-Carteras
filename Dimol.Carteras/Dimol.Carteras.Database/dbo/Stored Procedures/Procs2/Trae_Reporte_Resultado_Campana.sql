

Create Procedure Trae_Reporte_Resultado_Campana(@ccc_codemp integer, @ccc_sucid integer, @ccc_cccid integer, @idid integer) as
  SELECT cartera_clientes_campana.ccc_nombre,   
         cartera_clientes_campana.ccc_fecini,   
         cartera_clientes_campana.ccc_fecfinreal,   
         cartera_clientes_campana.ccc_descripcion,   
         cartera_clientes_documentos_cpbt_doc.ctc_rut,   
         cartera_clientes_documentos_cpbt_doc.ctc_nomfant,   
         cartera_clientes_campana_cpbtdoc.ccd_estid,   
         cartera_clientes_documentos_cpbt_doc.eci_estid,   
         cartera_clientes_documentos_cpbt_doc.ccb_compromiso,   
         gestor.ges_nombre,   
         cartera_clientes_documentos_cpbt_doc.pcl_rut,   
         cartera_clientes_documentos_cpbt_doc.pcl_nombre, tci_nombre, ccb_numero, ccd_estado  
    FROM cartera_clientes_campana,   
         cartera_clientes_campana_cpbtdoc,   
         cartera_clientes_documentos_cpbt_doc,   
         gestor,   
         gestor_cartera  
   WHERE ( cartera_clientes_campana_cpbtdoc.ccd_codemp = cartera_clientes_campana.ccc_codemp ) and  
         ( cartera_clientes_campana_cpbtdoc.ccd_sucid = cartera_clientes_campana.ccc_sucid ) and  
         ( cartera_clientes_campana_cpbtdoc.ccd_cccid = cartera_clientes_campana.ccc_cccid ) and  
         ( cartera_clientes_campana_cpbtdoc.ccd_codemp = cartera_clientes_documentos_cpbt_doc.ccb_codemp ) and  
         ( cartera_clientes_campana_cpbtdoc.ccd_pclid = cartera_clientes_documentos_cpbt_doc.ccb_pclid ) and  
         ( cartera_clientes_campana_cpbtdoc.ccd_ctcid = cartera_clientes_documentos_cpbt_doc.ccb_ctcid ) and  
         ( cartera_clientes_campana_cpbtdoc.ccd_ccbid = cartera_clientes_documentos_cpbt_doc.ccb_ccbid ) and  
         ( gestor_cartera.gsc_codemp = gestor.ges_codemp ) and  
         ( gestor_cartera.gsc_sucid = gestor.ges_sucid ) and  
         ( gestor_cartera.gsc_gesid = gestor.ges_gesid ) and  
         ( cartera_clientes_campana_cpbtdoc.ccd_codemp = gestor_cartera.gsc_codemp ) and  
         ( cartera_clientes_campana_cpbtdoc.ccd_sucid = gestor_cartera.gsc_sucid ) and  
         ( cartera_clientes_campana_cpbtdoc.ccd_pclid = gestor_cartera.gsc_pclid ) and  
         ( cartera_clientes_campana_cpbtdoc.ccd_ctcid = gestor_cartera.gsc_ctcid ) and  
         ( ( cartera_clientes_campana.ccc_codemp = @ccc_codemp ) AND  
         ( cartera_clientes_campana.ccc_sucid = @ccc_sucid ) AND  
         ( cartera_clientes_campana.ccc_cccid = @ccc_cccid ) AND  
         ( cartera_clientes_documentos_cpbt_doc.tci_idid = @idid ) AND  
         ( cartera_clientes_documentos_cpbt_doc.eci_idid = @idid ) AND  
         ( cartera_clientes_documentos_cpbt_doc.mci_idid = @idid )   
         )
