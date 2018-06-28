

Create Procedure Update_Cartera_Clientes_Cpbt_Doc_Carta(@ccb_codemp integer, @ccb_pclid numeric (15), @ccb_ctcid numeric (15), @ccb_ccbid integer, @ccb_estid integer, @ccb_carta smallint ) as  
  UPDATE cartera_clientes_cpbt_doc
 Set ccb_estid = @ccb_estid,
                 ccb_carta = @ccb_carta,
                 ccb_fecultgest = getdate()
   WHERE ( cartera_clientes_cpbt_doc.ccb_codemp = @ccb_codemp ) AND  
         ( cartera_clientes_cpbt_doc.ccb_pclid = @ccb_pclid ) AND  
         ( cartera_clientes_cpbt_doc.ccb_ctcid = @ccb_ctcid ) AND  
         ( cartera_clientes_cpbt_doc.ccb_ccbid = @ccb_ccbid )
