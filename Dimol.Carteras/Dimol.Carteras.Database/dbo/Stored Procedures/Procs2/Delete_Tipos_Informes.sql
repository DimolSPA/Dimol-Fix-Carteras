

Create Procedure Delete_Tipos_Informes(@tif_codemp integer, @tif_tifid integer) as 

  DELETE FROM tipos_informes_idiomas  
   WHERE ( tipos_informes_idiomas.tfi_codemp = @tif_codemp ) AND  
         ( tipos_informes_idiomas.tfi_tifid = @tif_tifid ) 

  DELETE FROM tipos_informes  
   WHERE ( tipos_informes.tif_codemp = @tif_codemp ) AND  
         ( tipos_informes.tif_tifid = @tif_tifid )
