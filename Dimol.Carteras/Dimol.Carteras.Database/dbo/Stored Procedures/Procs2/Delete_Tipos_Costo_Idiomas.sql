

Create Procedure Delete_Tipos_Costo_Idiomas(@tci_codemp integer, @tci_tcoid integer, @tci_idid integer) as
  DELETE FROM tipos_costo_idiomas  
   WHERE ( tipos_costo_idiomas.tci_codemp = @tci_codemp ) AND  
         ( tipos_costo_idiomas.tci_tcoid = @tci_tcoid ) AND  
         ( tipos_costo_idiomas.tci_idid = @tci_idid )
