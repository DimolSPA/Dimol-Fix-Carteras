

Create Procedure Delete_Tipos_Vehiculos(@tvc_codemp integer, @tvc_tvcid integer) as
  DELETE FROM tipos_vehiculos_idiomas  
   WHERE ( tipos_vehiculos_idiomas.tvi_codemp = @tvc_codemp ) AND  
         ( tipos_vehiculos_idiomas.tvi_tvcid = @tvc_tvcid ) 


  DELETE FROM tipos_vehiculos  
   WHERE ( tipos_vehiculos.tvc_codemp = @tvc_codemp ) AND  
         ( tipos_vehiculos.tvc_tvcid = @tvc_tvcid )
