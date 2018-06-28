

Create  Procedure Update_Tipos_Informes(@tif_codemp integer, @tif_tifid integer, @tif_nombre varchar (80)) as  
   UPDATE tipos_informes  
     SET tif_nombre = @tif_nombre  
   WHERE ( tipos_informes.tif_codemp = @tif_codemp ) AND  
         ( tipos_informes.tif_tifid = @tif_tifid )
