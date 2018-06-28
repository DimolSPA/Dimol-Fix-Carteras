

Create Procedure Delete_Tipos_Causa_Idiomas(@tci_codemp integer, @tci_tcaid integer, @tci_idid integer) as
  DELETE FROM tipos_causa_idiomas  
   WHERE ( tipos_causa_idiomas.tci_codemp = @tci_codemp ) AND  
         ( tipos_causa_idiomas.tci_tcaid = @tci_tcaid ) AND  
         ( tipos_causa_idiomas.tci_idid = @tci_idid )
