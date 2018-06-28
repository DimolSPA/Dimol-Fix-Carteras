

Create Procedure Insertar_Tipos_Imagenes_Cpbtdoc(@tpc_codemp integer, @tpc_tpcid integer, @tpc_nombre varchar(150)) as
  INSERT INTO tipos_imagenes_cpbtdoc  
         ( tpc_codemp,   
           tpc_tpcid,   
           tpc_nombre )  
  VALUES ( @tpc_codemp,   
           @tpc_tpcid,   
           @tpc_nombre )
