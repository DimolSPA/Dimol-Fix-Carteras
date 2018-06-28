

Create Procedure Delete_Contratos_Clientes(@ctc_codemp integer, @ctc_cctid integer, @ctc_pclid numeric (15)) as
  DELETE FROM contratos_clientes  
   WHERE ( contratos_clientes.ctc_codemp = @ctc_codemp ) AND  
         ( contratos_clientes.ctc_cctid = @ctc_cctid ) AND  
         ( contratos_clientes.ctc_pclid = @ctc_pclid )
