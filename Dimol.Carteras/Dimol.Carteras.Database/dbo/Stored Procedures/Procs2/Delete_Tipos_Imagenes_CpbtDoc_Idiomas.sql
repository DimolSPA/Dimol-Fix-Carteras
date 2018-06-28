

Create Procedure Delete_Tipos_Imagenes_CpbtDoc_Idiomas(@tpc_codemp integer, @tpc_tpcid integer, @tpi_idid integer) as
    DELETE FROM tipos_imagenes_cpbtdoc_idiomas  
   WHERE ( tipos_imagenes_cpbtdoc_idiomas.tpi_codemp = @tpc_codemp ) AND  
         ( tipos_imagenes_cpbtdoc_idiomas.tpi_tpcid = @tpc_tpcid ) AND  
         ( tipos_imagenes_cpbtdoc_idiomas.tpi_idid = @tpi_idid )
