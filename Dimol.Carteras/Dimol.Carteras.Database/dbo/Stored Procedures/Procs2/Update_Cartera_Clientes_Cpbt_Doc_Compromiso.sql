

Create Procedure Update_Cartera_Clientes_Cpbt_Doc_Compromiso(@ccb_codemp integer, @ccb_pclid integer, @ccb_ctcid integer, @ccb_ccbid integer, @ccb_compromiso decimal(15,2), @ccb_fecplazo datetime) as
  UPDATE cartera_clientes_cpbt_doc  
     SET ccb_compromiso = @ccb_compromiso,
            ccb_fecplazo = @ccb_fecplazo    
   WHERE ( cartera_clientes_cpbt_doc.ccb_codemp = @ccb_codemp ) AND  
         ( cartera_clientes_cpbt_doc.ccb_pclid = @ccb_pclid ) AND  
         ( cartera_clientes_cpbt_doc.ccb_ctcid = @ccb_ctcid ) AND  
         ( cartera_clientes_cpbt_doc.ccb_ccbid = @ccb_ccbid )
