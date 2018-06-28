

Create Procedure Find_Tipos_Costo(@tco_codemp integer, @tco_tcoid integer) as
  SELECT count(tipos_costo.tco_tcoid)  
    FROM tipos_costo  
   WHERE ( tipos_costo.tco_codemp = @tco_codemp ) AND  
         ( tipos_costo.tco_tcoid = @tco_tcoid )
