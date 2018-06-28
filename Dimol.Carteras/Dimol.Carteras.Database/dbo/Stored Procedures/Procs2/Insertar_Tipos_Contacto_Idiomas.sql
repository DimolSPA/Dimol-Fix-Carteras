

Create Procedure Insertar_Tipos_Contacto_Idiomas(@tci_codemp integer, @tci_ticid integer, @tci_idid integer, @tci_nombre varchar (150)) as
  INSERT INTO tipos_contacto_idiomas  
         ( tci_codemp,   
           tci_ticid,   
           tci_idid,   
           tci_nombre )  
  VALUES ( @tci_codemp,   
           @tci_ticid,   
           @tci_idid,   
           @tci_nombre )
