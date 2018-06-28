

Create Procedure Find_Tipos_Vehiculos(@tvc_codemp integer, @tvc_tvcid integer) as
  SELECT count(tipos_vehiculos.tvc_codemp)  
    FROM tipos_vehiculos  
   WHERE ( tipos_vehiculos.tvc_codemp = @tvc_codemp ) AND  
         ( tipos_vehiculos.tvc_tvcid = @tvc_tvcid )
