

Create Procedure Insertar_Tipos_Imagenes_CpbtDoc_Idiomas(@tpc_codemp integer, @tpc_tpcid integer, @tpi_idid integer, @tpc_nombre varchar(150)) as
  INSERT INTO tipos_imagenes_cpbtdoc_idiomas  
         ( tpi_codemp,   
           tpi_tpcid,   
           tpi_idid,   
           tpi_nombre )  
  VALUES ( @tpc_codemp,   
           @tpc_tpcid,   
           @tpi_idid,   
           @tpc_nombre )
