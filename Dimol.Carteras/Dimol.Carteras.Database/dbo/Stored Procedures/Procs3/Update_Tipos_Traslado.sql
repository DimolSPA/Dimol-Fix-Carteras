

Create Procedure Update_Tipos_Traslado(@ttl_codemp integer, @ttl_ttlid integer, @ttl_nombre varchar(80), @ttl_codigo varchar(5)) as
   UPDATE tipos_traslado  
     SET ttl_nombre = @ttl_nombre,   
         ttl_codigo = @ttl_codigo  
   WHERE ( tipos_traslado.ttl_codemp = @ttl_codemp ) AND  
         ( tipos_traslado.ttl_ttlid = @ttl_ttlid )
