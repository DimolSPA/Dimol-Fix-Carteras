

Create Procedure Update_Cartera_Clientes_Cpbt_Doc_Hon(@ccb_codemp integer, @ccb_pclid integer, @ccb_ctcid integer, @ccb_ccbid integer, @ccb_honorarios decimal(15,2)) as
  UPDATE cartera_clientes_cpbt_doc  
     SET ccb_honorarios = @ccb_honorarios,
             ccb_calchon = 'S'   
   WHERE ( cartera_clientes_cpbt_doc.ccb_codemp = @ccb_codemp ) AND  
         ( cartera_clientes_cpbt_doc.ccb_pclid = @ccb_pclid ) AND  
         ( cartera_clientes_cpbt_doc.ccb_ctcid = @ccb_ctcid ) AND  
         ( cartera_clientes_cpbt_doc.ccb_ccbid = @ccb_ccbid )
