

Create Procedure Update_Tipos_Vehiculos(@tvc_codemp integer, @tvc_tvcid integer, @tvc_nombre varchar(50)) as
  UPDATE tipos_vehiculos  
     SET tvc_nombre = @tvc_nombre  
   WHERE ( tipos_vehiculos.tvc_codemp = @tvc_codemp ) AND  
         ( tipos_vehiculos.tvc_tvcid = @tvc_tvcid )
