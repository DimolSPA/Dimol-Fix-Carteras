

Create Procedure Insertar_Tipos_CpbtDoc_Idiomas(@tci_codemp integer, @tci_tpcid integer, @tci_idid integer, @tci_nombre varchar(100)) as
  INSERT INTO tipos_cpbtdoc_idiomas  
         ( tci_codemp,   
           tci_tpcid,   
           tci_idid,   
           tci_nombre )  
  VALUES ( @tci_codemp,   
           @tci_tpcid,   
           @tci_idid,   
           @tci_nombre )
