

Create Procedure Update_Tipos_Causa_Idiomas(@tci_codemp integer, @tci_tcaid integer, @tci_idid integer, @tci_nombre varchar (800)) as  
  UPDATE tipos_causa_idiomas  
     SET tci_codemp = @tci_codemp,   
         tci_tcaid = @tci_tcaid,   
         tci_idid = @tci_idid,   
         tci_nombre = @tci_nombre  
   WHERE ( tipos_causa_idiomas.tci_codemp = @tci_codemp ) AND  
         ( tipos_causa_idiomas.tci_tcaid = @tci_tcaid ) AND  
         ( tipos_causa_idiomas.tci_idid = @tci_idid )
