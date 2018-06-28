

Create Procedure Insertar_Tipos_Traslado(@ttl_codemp integer, @ttl_ttlid integer, @ttl_nombre varchar(80), @ttl_codigo varchar(5)) as
  INSERT INTO tipos_traslado  
         ( ttl_codemp,   
           ttl_ttlid,   
           ttl_nombre,   
           ttl_codigo )  
  VALUES ( @ttl_codemp,   
           @ttl_ttlid,   
           @ttl_nombre,   
           @ttl_codigo )
