

Create Procedure Delete_Tipos_Costo(@tco_codemp integer, @tco_tcoid integer) as

 DELETE FROM tipos_costo_idiomas  
   WHERE ( tipos_costo_idiomas.tci_codemp = @tco_codemp ) AND  
         ( tipos_costo_idiomas.tci_tcoid = @tco_tcoid ) 

  DELETE FROM tipos_costo  
   WHERE ( tipos_costo.tco_codemp = @tco_codemp ) AND  
         ( tipos_costo.tco_tcoid = @tco_tcoid )
