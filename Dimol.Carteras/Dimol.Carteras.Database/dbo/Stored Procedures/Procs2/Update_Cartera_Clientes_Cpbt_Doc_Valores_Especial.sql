﻿

Create Procedure Update_Cartera_Clientes_Cpbt_Doc_Valores_Especial(@ccb_codemp integer, @ccb_pclid integer, @ccb_ctcid integer, @ccb_ccbid integer, 
																			                   @ccb_saldo decimal(15,2), @ccb_gastjud decimal(15,2), @ccb_gastotro decimal(15,2), 
																			                   @ccb_intereses decimal(15,2),  @ccb_honorarios decimal(15,2), @ccb_estid integer, 
																						   @ccb_feccalcint datetime, @ccb_estcpbt char(1), @ccb_tipcambio decimal(10,2),
																						   @ccb_codid integer, @ccb_fecvenc datetime ) as
  UPDATE cartera_clientes_cpbt_doc  
     SET ccb_saldo = @ccb_saldo,
			ccb_gastjud = @ccb_gastjud,
			ccb_gastotro = @ccb_gastotro,
			ccb_intereses = @ccb_intereses,
			ccb_honorarios = @ccb_honorarios,
			ccb_estid = @ccb_estid,
			ccb_feccalcint = @ccb_feccalcint,
                ccb_fecultgest = getdate(),
                ccb_estcpbt = @ccb_estcpbt,
                ccb_tipcambio = @ccb_tipcambio, 
                ccb_codid = @ccb_codid,
                ccb_fecvenc = @ccb_fecvenc
   WHERE ( cartera_clientes_cpbt_doc.ccb_codemp = @ccb_codemp ) AND  
         ( cartera_clientes_cpbt_doc.ccb_pclid = @ccb_pclid ) AND  
         ( cartera_clientes_cpbt_doc.ccb_ctcid = @ccb_ctcid ) AND  
         ( cartera_clientes_cpbt_doc.ccb_ccbid = @ccb_ccbid )


  UPDATE cartera_clientes_cpbt_doc  
     SET     ccb_fecplazo = null,
                ccb_compromiso = 0
   WHERE ( cartera_clientes_cpbt_doc.ccb_codemp = @ccb_codemp ) AND  
         ( cartera_clientes_cpbt_doc.ccb_pclid = @ccb_pclid ) AND  
         ( cartera_clientes_cpbt_doc.ccb_ctcid = @ccb_ctcid ) AND  
         ( cartera_clientes_cpbt_doc.ccb_ccbid = @ccb_ccbid and ccb_fecplazo < getdate() )
