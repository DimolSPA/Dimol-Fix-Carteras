

Create Procedure Insertar_Tipos_Vehiculos(@tvc_codemp integer, @tvc_tvcid integer, @tvc_nombre varchar(50)) as
  INSERT INTO tipos_vehiculos  
         ( tvc_codemp,   
           tvc_tvcid,   
           tvc_nombre )  
  VALUES ( @tvc_codemp,   
           @tvc_tvcid,   
           @tvc_nombre )
