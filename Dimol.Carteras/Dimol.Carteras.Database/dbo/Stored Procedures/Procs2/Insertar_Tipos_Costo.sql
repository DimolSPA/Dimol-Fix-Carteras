

Create Procedure Insertar_Tipos_Costo(@tco_codemp integer, @tco_tcoid integer, @tco_nombre varchar (50), @tco_agrupa smallint) as
  INSERT INTO tipos_costo  
         ( tco_codemp,   
           tco_tcoid,   
           tco_nombre,
           tco_agrupa )  
  VALUES ( @tco_codemp,   
           @tco_tcoid,   
           @tco_nombre,
           @tco_agrupa )
