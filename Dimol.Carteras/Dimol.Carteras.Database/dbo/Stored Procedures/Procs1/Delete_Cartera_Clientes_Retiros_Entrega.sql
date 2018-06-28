

Create Procedure Delete_Cartera_Clientes_Retiros_Entrega(@cre_codemp integer, @cre_pclid numeric (15), @cre_ctcid numeric (15),
																			@cre_ccbid integer, @cre_fecha datetime) as
  DELETE FROM cartera_clientes_retiros_entrega  
   WHERE ( cartera_clientes_retiros_entrega.cre_codemp = @cre_codemp ) AND  
         ( cartera_clientes_retiros_entrega.cre_pclid = @cre_pclid ) AND  
         ( cartera_clientes_retiros_entrega.cre_ctcid = @cre_ctcid ) AND  
         ( cartera_clientes_retiros_entrega.cre_ccbid = @cre_ccbid ) AND  
         ( cartera_clientes_retiros_entrega.cre_fecha = @cre_fecha )
