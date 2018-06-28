

Create Procedure Update_Tipos_Vehiculos_Idiomas(@tvi_codemp integer, @tvi_tvcid integer, @tvi_idid integer, @tvi_nombre varchar(50)) as
  UPDATE tipos_vehiculos_idiomas  
     SET tvi_nombre = @tvi_nombre  
   WHERE ( tipos_vehiculos_idiomas.tvi_codemp = @tvi_codemp ) AND  
         ( tipos_vehiculos_idiomas.tvi_tvcid = @tvi_tvcid ) AND  
         ( tipos_vehiculos_idiomas.tvi_idid = @tvi_idid )
