

Create Procedure Insertar_Marcas_Vehiculos(@mkv_codemp integer, @mkv_mkvid integer, @mkv_nombre varchar(100)) as
  INSERT INTO marcas_vehiculos  
         ( mkv_codemp,   
           mkv_mkvid,   
           mkv_nombre )  
  VALUES ( @mkv_codemp,   
           @mkv_mkvid,   
           @mkv_nombre )
