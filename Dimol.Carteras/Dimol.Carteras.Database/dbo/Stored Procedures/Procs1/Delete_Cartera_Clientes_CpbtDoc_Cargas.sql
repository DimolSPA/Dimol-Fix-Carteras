

Create Procedure Delete_Cartera_Clientes_CpbtDoc_Cargas(@ccb_codemp integer, @ccb_pclid integer, @ccb_estid integer) as
  DELETE cartera_clientes_estados_historial  
    FROM cartera_clientes_estados_historial,   
         cartera_clientes_cpbt_doc  
   WHERE ( cartera_clientes_cpbt_doc.ccb_codemp = cartera_clientes_estados_historial.ceh_codemp ) and  
         ( cartera_clientes_cpbt_doc.ccb_pclid = cartera_clientes_estados_historial.ceh_pclid ) and  
         ( cartera_clientes_cpbt_doc.ccb_ctcid = cartera_clientes_estados_historial.ceh_ctcid ) and  
         ( cartera_clientes_cpbt_doc.ccb_ccbid = cartera_clientes_estados_historial.ceh_ccbid ) and  
         ( ( cartera_clientes_cpbt_doc.ccb_codemp = @ccb_codemp ) AND  
         ( cartera_clientes_cpbt_doc.ccb_pclid = @ccb_pclid ) AND  
         ( cartera_clientes_cpbt_doc.ccb_estid = @ccb_estid ) AND  
         ( cartera_clientes_cpbt_doc.ccb_estcpbt = 'V' )   
         )   

  DELETE FROM cartera_clientes_cpbt_doc  
   WHERE ( cartera_clientes_cpbt_doc.ccb_codemp =  @ccb_codemp ) AND  
         ( cartera_clientes_cpbt_doc.ccb_pclid =  @ccb_pclid ) AND  
         ( cartera_clientes_cpbt_doc.ccb_estid = @ccb_estid ) AND  
         ( cartera_clientes_cpbt_doc.ccb_estcpbt = 'V' )
