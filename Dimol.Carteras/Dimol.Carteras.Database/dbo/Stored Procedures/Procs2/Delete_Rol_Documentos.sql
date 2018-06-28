

Create Procedure Delete_Rol_Documentos(@rdc_codemp integer, @rdc_rolid integer, @rdc_pclid numeric (15), @rdc_ctcid numeric (15), @rdc_ccbid integer) as

 DELETE FROM rol_documentos  
    FROM rol_documentos,   
         cartera_clientes_cpbt_doc  
   WHERE ( cartera_clientes_cpbt_doc.ccb_codemp = rol_documentos.rdc_codemp ) and  
         ( cartera_clientes_cpbt_doc.ccb_pclid = rol_documentos.rdc_pclid ) and  
         ( cartera_clientes_cpbt_doc.ccb_ctcid = rol_documentos.rdc_ctcid ) and  
         ( cartera_clientes_cpbt_doc.ccb_ccbid = rol_documentos.rdc_ccbid ) and  
         ( ( rol_documentos.rdc_codemp = @rdc_codemp ) AND  
         ( rol_documentos.rdc_rolid = @rdc_rolid ) AND  
         ( rol_documentos.rdc_pclid = @rdc_pclid ) AND  
         ( rol_documentos.rdc_ctcid = @rdc_ctcid ) AND  
         ( rol_documentos.rdc_ccbid = @rdc_ccbid ) AND  
         ( cartera_clientes_cpbt_doc.ccb_estcpbt = 'J' )   
         )
