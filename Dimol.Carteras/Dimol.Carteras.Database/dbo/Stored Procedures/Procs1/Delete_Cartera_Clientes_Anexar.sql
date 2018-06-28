

Create Procedure Delete_Cartera_Clientes_Anexar(@cca_codemp integer, @cca_ccaid integer, @cca_pclid numeric (15), @cca_ctcid numeric (15)) as  
  DELETE FROM cartera_clientes_anexar  
   WHERE ( cartera_clientes_anexar.cca_codemp = @cca_codemp ) AND  
         ( cartera_clientes_anexar.cca_ccaid = @cca_ccaid ) AND  
         ( cartera_clientes_anexar.cca_pclid = @cca_pclid ) AND  
         ( cartera_clientes_anexar.cca_ctcid = @cca_ctcid )
