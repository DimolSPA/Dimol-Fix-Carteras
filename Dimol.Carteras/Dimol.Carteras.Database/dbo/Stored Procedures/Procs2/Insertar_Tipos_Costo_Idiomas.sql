

Create Procedure Insertar_Tipos_Costo_Idiomas(@tci_codemp integer, @tci_tcoid integer, @tci_idid integer, @tci_nombre varchar (60)) as
  INSERT INTO tipos_costo_idiomas  
         ( tci_codemp,   
           tci_tcoid,   
           tci_idid,   
           tci_nombre )  
  VALUES ( @tci_codemp,   
           @tci_tcoid,   
           @tci_idid,   
           @tci_nombre )
