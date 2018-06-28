

Create Procedure Delete_Tipos_Vehiculos_Idiomas(@tvi_codemp integer, @tvi_tvcid integer, @tvi_idid integer) as
  DELETE FROM tipos_vehiculos_idiomas  
   WHERE ( tipos_vehiculos_idiomas.tvi_codemp = @tvi_codemp ) AND  
         ( tipos_vehiculos_idiomas.tvi_tvcid = @tvi_tvcid ) AND  
         ( tipos_vehiculos_idiomas.tvi_idid = @tvi_idid )
