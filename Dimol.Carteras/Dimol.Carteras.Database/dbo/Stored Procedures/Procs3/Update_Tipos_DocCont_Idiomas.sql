

Create Procedure Update_Tipos_DocCont_Idiomas(@tci_codemp integer, @tci_tdcid integer, @tci_idid integer, @tci_nombre varchar (150)) as  
  UPDATE tipos_doccont_idiomas  
     SET tci_nombre = @tci_nombre  
   WHERE ( tipos_doccont_idiomas.tci_codemp = @tci_codemp ) AND  
         ( tipos_doccont_idiomas.tci_tdcid = @tci_tdcid ) AND  
         ( tipos_doccont_idiomas.tci_idid = @tci_idid )
