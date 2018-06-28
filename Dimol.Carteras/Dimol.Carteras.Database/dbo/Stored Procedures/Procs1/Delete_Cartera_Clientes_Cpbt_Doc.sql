

Create Procedure Delete_Cartera_Clientes_Cpbt_Doc(@ccb_codemp integer, @ccb_pclid numeric (15), @ccb_ctcid numeric (15), @ccb_ccbid integer) as  


DELETE FROM cartera_clientes_estados_historial  
   WHERE ( cartera_clientes_estados_historial.ceh_codemp = @ccb_codemp ) AND  
         ( cartera_clientes_estados_historial.ceh_pclid = @ccb_pclid ) AND  
         ( cartera_clientes_estados_historial.ceh_ctcid = @ccb_ctcid ) AND  
         ( cartera_clientes_estados_historial.ceh_ccbid = @ccb_ccbid ) 


DELETE FROM cartera_clientes_cpbt_doc_imagenes  
   WHERE ( cartera_clientes_cpbt_doc_imagenes.cdi_codemp = @ccb_codemp ) AND  
         ( cartera_clientes_cpbt_doc_imagenes.cdi_pclid = @ccb_pclid ) AND  
         ( cartera_clientes_cpbt_doc_imagenes.cdi_ctcid = @ccb_ctcid ) AND  
         ( cartera_clientes_cpbt_doc_imagenes.cdi_ccbid = @ccb_ccbid ) 


  DELETE FROM cartera_clientes_cpbt_doc  
   WHERE ( cartera_clientes_cpbt_doc.ccb_codemp = @ccb_codemp ) AND  
         ( cartera_clientes_cpbt_doc.ccb_pclid = @ccb_pclid ) AND  
         ( cartera_clientes_cpbt_doc.ccb_ctcid = @ccb_ctcid ) AND  
         ( cartera_clientes_cpbt_doc.ccb_ccbid = @ccb_ccbid )
