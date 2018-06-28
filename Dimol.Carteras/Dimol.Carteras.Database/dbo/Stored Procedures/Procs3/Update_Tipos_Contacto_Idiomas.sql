

Create Procedure Update_Tipos_Contacto_Idiomas(@tci_codemp integer, @tci_ticid integer, @tci_idid integer, @tci_nombre varchar (150)) as  
  UPDATE tipos_contacto_idiomas  
     SET tci_nombre = @tci_nombre  
   WHERE ( tipos_contacto_idiomas.tci_codemp = @tci_codemp ) AND  
         ( tipos_contacto_idiomas.tci_ticid = @tci_ticid ) AND  
         ( tipos_contacto_idiomas.tci_idid = @tci_idid )
