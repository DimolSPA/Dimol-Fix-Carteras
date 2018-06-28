

Create Procedure Delete_Tipos_Imagenes_CpbtDoc(@tpc_codemp integer, @tpc_tpcid integer) as
    DELETE FROM tipos_imagenes_cpbtdoc_idiomas  
   WHERE ( tipos_imagenes_cpbtdoc_idiomas.tpi_codemp = @tpc_codemp ) AND  
         ( tipos_imagenes_cpbtdoc_idiomas.tpi_tpcid = @tpc_tpcid ) 

  DELETE FROM tipos_imagenes_cpbtdoc  
   WHERE ( tipos_imagenes_cpbtdoc.tpc_codemp = @tpc_codemp ) AND  
         ( tipos_imagenes_cpbtdoc.tpc_tpcid = @tpc_tpcid )
