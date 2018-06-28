

Create Procedure Update_Tipos_Imagenes_CpbtDoc_Idiomas(@tpc_codemp integer, @tpc_tpcid integer, @tpi_idid integer, @tpi_nombre varchar(150)) as
   UPDATE tipos_imagenes_cpbtdoc_idiomas  
     SET tpi_nombre = @tpi_nombre  
   WHERE ( tipos_imagenes_cpbtdoc_idiomas.tpi_codemp = @tpc_codemp ) AND  
         ( tipos_imagenes_cpbtdoc_idiomas.tpi_tpcid = @tpc_tpcid ) AND  
         ( tipos_imagenes_cpbtdoc_idiomas.tpi_idid = @tpi_idid )
