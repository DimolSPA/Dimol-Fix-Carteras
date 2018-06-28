

Create Procedure Insertar_Tipos_DocCont_Idiomas(@tci_codemp integer, @tci_tdcid integer,@tci_idid integer,  @tci_nombre varchar(150)) as
     INSERT INTO tipos_doccont_idiomas  
         ( tci_codemp,   
           tci_tdcid,   
           tci_idid,   
           tci_nombre )  
  VALUES ( @tci_codemp,   
           @tci_tdcid,   
           @tci_idid,   
           @tci_nombre )
