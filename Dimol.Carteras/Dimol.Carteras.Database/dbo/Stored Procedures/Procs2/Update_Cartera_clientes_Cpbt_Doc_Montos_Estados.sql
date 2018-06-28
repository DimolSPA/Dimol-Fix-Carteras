

Create Procedure Update_Cartera_clientes_Cpbt_Doc_Montos_Estados(@ccb_codemp integer, @ccb_pclid integer, @ccb_ctcid numeric(15), @ccb_ccbid integer,
																						  @ccb_estid integer, @ccb_estcpbt char(1), @ccb_saldo decimal(15,2), @ccb_gastjud decimal(15,2),
																						  @ccb_gastotro decimal(15,2), @ccb_intereses decimal(15,2), @ccb_honorarios decimal(15,2)) as 	
  UPDATE cartera_clientes_cpbt_doc  
     SET ccb_estid = @ccb_estid,   
         ccb_estcpbt = @ccb_estcpbt,   
         ccb_saldo = ccb_saldo + @ccb_saldo,   
         ccb_gastjud = ccb_gastjud + @ccb_gastjud,   
         ccb_gastotro = ccb_gastotro + @ccb_gastotro,   
         ccb_intereses = ccb_intereses + @ccb_intereses,   
         ccb_honorarios = ccb_honorarios + @ccb_honorarios  
   WHERE ( cartera_clientes_cpbt_doc.ccb_codemp = @ccb_codemp ) AND  
         ( cartera_clientes_cpbt_doc.ccb_pclid = @ccb_pclid ) AND  
         ( cartera_clientes_cpbt_doc.ccb_ctcid = @ccb_ctcid ) AND  
         ( cartera_clientes_cpbt_doc.ccb_ccbid = @ccb_ccbid)
