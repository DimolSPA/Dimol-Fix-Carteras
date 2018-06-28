

Create Procedure Update_Cartera_Clientes_Cpbt_Doc_AccEst(@ccb_codemp integer, @ccb_pclid numeric (15), @ccb_ctcid numeric (15),  @ccb_estcpbt char(1) ) as  
  UPDATE cartera_clientes_cpbt_doc
 Set  ccb_fecultgest = getdate()
   WHERE ( cartera_clientes_cpbt_doc.ccb_codemp = @ccb_codemp ) AND  
         ( cartera_clientes_cpbt_doc.ccb_pclid = @ccb_pclid ) AND  
         ( cartera_clientes_cpbt_doc.ccb_ctcid = @ccb_ctcid ) AND  
         ( cartera_clientes_cpbt_doc.ccb_estcpbt = @ccb_estcpbt )
