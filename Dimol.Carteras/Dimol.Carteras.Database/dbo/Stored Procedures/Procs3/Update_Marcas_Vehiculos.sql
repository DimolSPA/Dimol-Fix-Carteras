

Create Procedure Update_Marcas_Vehiculos(@mkv_codemp integer, @mkv_mkvid integer, @mkv_nombre varchar(100)) as
   UPDATE marcas_vehiculos  
     SET mkv_nombre = @mkv_nombre  
   WHERE ( marcas_vehiculos.mkv_codemp = @mkv_codemp ) AND  
         ( marcas_vehiculos.mkv_mkvid = @mkv_mkvid )
