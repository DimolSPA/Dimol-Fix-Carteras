

Create Procedure Update_Tipos_Imagenes_Cpbtdoc(@tpc_codemp integer, @tpc_tpcid integer, @tpc_nombre varchar(150)) as
  UPDATE tipos_imagenes_cpbtdoc  
     SET tpc_nombre = @tpc_nombre
   WHERE ( tipos_imagenes_cpbtdoc.tpc_codemp = @tpc_codemp ) AND  
         ( tipos_imagenes_cpbtdoc.tpc_tpcid = @tpc_tpcid )
