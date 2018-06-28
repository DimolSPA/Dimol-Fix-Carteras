

Create Procedure Update_Tipos_Costo(@tco_codemp integer, @tco_tcoid integer, @tco_nombre varchar (50), @tco_agrupa smallint) as
  UPDATE tipos_costo  
     SET tco_codemp = @tco_codemp,   
         tco_tcoid = @tco_tcoid,   
         tco_nombre = @tco_nombre,
         tco_agrupa = @tco_agrupa  
   WHERE ( tipos_costo.tco_codemp = @tco_codemp ) AND  
         ( tipos_costo.tco_tcoid = @tco_tcoid )
