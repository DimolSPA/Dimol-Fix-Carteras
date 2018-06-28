

Create Procedure Insertar_Tipos_Vehiculos_Idiomas(@tvi_codemp integer, @tvi_tvcid integer, @tvi_idid integer, @tvi_nombre varchar(50)) as
  INSERT INTO tipos_vehiculos_idiomas  
         ( tvi_codemp,   
           tvi_tvcid,   
           tvi_idid,   
           tvi_nombre )  
  VALUES ( @tvi_codemp,   
           @tvi_tvcid,   
           @tvi_idid,   
           @tvi_nombre )
