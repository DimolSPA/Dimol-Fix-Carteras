

Create Procedure Update_Tipos_Contrato_Idiomas(@tci_codemp integer, @tci_ticid integer, @tci_idid integer, @tci_nombre varchar(80)) as
  UPDATE tipos_contrato_idiomas  
     SET tci_nombre = @tci_nombre  
   WHERE ( tipos_contrato_idiomas.tci_codemp = @tci_codemp ) AND  
         ( tipos_contrato_idiomas.tci_ticid = @tci_ticid ) AND  
         ( tipos_contrato_idiomas.tci_idid = @tci_idid )
