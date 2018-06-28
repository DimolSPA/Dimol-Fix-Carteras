

Create Procedure Delete_Tipos_Contrato(@tic_codemp integer, @tic_ticid integer) as  

 DELETE FROM tipos_contrato_idiomas  
   WHERE ( tipos_contrato_idiomas.tci_codemp = @tic_codemp ) AND  
         ( tipos_contrato_idiomas.tci_ticid = @tic_ticid )  

  DELETE FROM tipos_contrato  
   WHERE ( tipos_contrato.tic_codemp = @tic_codemp ) AND  
         ( tipos_contrato.tic_ticid = @tic_ticid )
