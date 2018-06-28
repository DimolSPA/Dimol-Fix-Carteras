

Create Procedure Delete_Tipos_DocCont_Idiomas(@tci_codemp integer, @tci_tdcid integer, @tci_idid integer) as  
  DELETE FROM tipos_doccont_idiomas  
   WHERE ( tipos_doccont_idiomas.tci_codemp = @tci_codemp ) AND  
         ( tipos_doccont_idiomas.tci_tdcid = @tci_tdcid ) AND  
         ( tipos_doccont_idiomas.tci_idid = @tci_idid )
