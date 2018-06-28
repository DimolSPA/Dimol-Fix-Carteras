

Create Procedure Delete_Tipos_Causa(@tca_codemp integer, @tca_tcaid integer) as

  DELETE FROM tipos_causa_idiomas  
   WHERE ( tipos_causa_idiomas.tci_codemp = @tca_codemp ) AND  
         ( tipos_causa_idiomas.tci_tcaid = @tca_tcaid ) 

  DELETE FROM tipos_causa  
   WHERE ( tipos_causa.tca_codemp = @tca_codemp ) AND  
         ( tipos_causa.tca_tcaid = @tca_tcaid )
