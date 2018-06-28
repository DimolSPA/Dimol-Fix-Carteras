

Create Procedure Insertar_Tipos_Contrato_Idiomas(@tci_codemp integer, @tci_ticid integer, @tci_idid integer, @tci_nombre varchar(100)) as
   INSERT INTO tipos_contrato_idiomas  
         ( tci_codemp,   
           tci_ticid,   
           tci_idid,   
           tci_nombre )  
  VALUES ( @tci_codemp,   
           @tci_ticid,   
           @tci_idid,   
           @tci_nombre )
