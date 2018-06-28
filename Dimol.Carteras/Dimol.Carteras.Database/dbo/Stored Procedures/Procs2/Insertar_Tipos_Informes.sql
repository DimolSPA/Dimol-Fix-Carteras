

 Create Procedure Insertar_Tipos_Informes(@tif_codemp integer, @tif_tifid integer, @tif_nombre varchar (80)) as
  INSERT INTO tipos_informes  
         ( tif_codemp,   
           tif_tifid,   
           tif_nombre )  
  VALUES ( @tif_codemp,   
           @tif_tifid,   
           @tif_nombre )
