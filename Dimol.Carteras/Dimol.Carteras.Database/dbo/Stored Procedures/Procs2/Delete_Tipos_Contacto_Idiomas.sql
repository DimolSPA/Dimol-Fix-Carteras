

Create Procedure Delete_Tipos_Contacto_Idiomas(@tci_codemp integer, @tci_ticid integer, @tci_idid integer) as  
  DELETE FROM tipos_contacto_idiomas  
   WHERE ( tipos_contacto_idiomas.tci_codemp = @tci_codemp ) AND  
         ( tipos_contacto_idiomas.tci_ticid = @tci_ticid ) AND  
         ( tipos_contacto_idiomas.tci_idid = @tci_idid )
