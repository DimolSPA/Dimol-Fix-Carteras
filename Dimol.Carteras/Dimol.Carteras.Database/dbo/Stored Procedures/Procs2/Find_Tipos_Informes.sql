

Create procedure Find_Tipos_Informes(@tif_codemp integer, @tif_tifid integer) as
  SELECT count(tipos_informes.tif_codemp)  
    FROM tipos_informes  
   WHERE ( tipos_informes.tif_codemp = @tif_codemp ) AND  
         ( tipos_informes.tif_tifid = @tif_tifid )
