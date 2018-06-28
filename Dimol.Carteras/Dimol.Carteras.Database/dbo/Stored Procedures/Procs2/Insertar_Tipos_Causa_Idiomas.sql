

Create Procedure Insertar_Tipos_Causa_Idiomas(@tci_codemp integer, @tci_tcaid integer, @tci_idid integer, @tci_nombre varchar (800)) as  
  INSERT INTO tipos_causa_idiomas  
         ( tci_codemp,   
           tci_tcaid,   
           tci_idid,   
           tci_nombre )  
  VALUES ( @tci_codemp,   
           @tci_tcaid,   
           @tci_idid,   
           @tci_nombre )
