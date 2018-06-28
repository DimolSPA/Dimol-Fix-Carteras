

Create Procedure Update_Tipos_Costo_Idiomas(@tci_codemp integer, @tci_tcoid integer, @tci_idid integer, @tci_nombre varchar (60)) as
  UPDATE tipos_costo_idiomas  
     SET tci_codemp = @tci_codemp,   
         tci_tcoid = @tci_tcoid,   
         tci_idid = @tci_idid,   
         tci_nombre = @tci_nombre  
   WHERE ( tipos_costo_idiomas.tci_codemp = @tci_codemp ) AND  
         ( tipos_costo_idiomas.tci_tcoid = @tci_tcoid ) AND  
         ( tipos_costo_idiomas.tci_idid = @tci_idid )
