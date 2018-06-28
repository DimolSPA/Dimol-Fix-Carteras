

Create Procedure Find_Marcas_Vehiculos(@mkv_codemp integer, @mkv_mkvid integer) as
  SELECT Count(marcas_vehiculos.mkv_codemp)  
    FROM marcas_vehiculos  
   WHERE ( marcas_vehiculos.mkv_codemp = @mkv_codemp ) AND  
         ( marcas_vehiculos.mkv_mkvid = @mkv_mkvid )
