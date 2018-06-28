

Create Procedure Delete_Cartera_Clientes(@ctc_codemp integer, @ctc_pclid numeric (15), @ctc_ctcid numeric (15)) as  
  DELETE FROM cartera_clientes  
   WHERE ( cartera_clientes.ctc_codemp = @ctc_codemp ) AND  
         ( cartera_clientes.ctc_pclid = @ctc_pclid ) AND  
         ( cartera_clientes.ctc_ctcid = @ctc_ctcid )
